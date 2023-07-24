/*=======================================================================
  Copyright (C) Microsoft Corporation.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace DiskOutputCache {

    /*
     * Cache is a singleton objects that keeps track of cached urls.
     * Cache retrieves and validates configuration and creates
     * Tracker objects for each configured url.
     * 
     * Module calls into Cache on every request to lookup the Tracker
     * 
     */

    class Cache : IRegisteredObject {
        // statics related to cache initialization
        static object _initLock = new object();
        static Exception _initError;
        static Cache _theCache;

        // config settings
        TimeSpan _fileRemovalDelay;
        TimeSpan _fileValidationDelay;
        TimeSpan _fileScavangingDelay;
        string _location;
        int _varyByLimit;

        // table of trackers per path
        Dictionary<string, Tracker> _trackers = new Dictionary<string, Tracker>(StringComparer.OrdinalIgnoreCase);

        // scavanging
        Timer _scavangingTimer;
        LinkedList<ScavangerEntry> _fileRemovalList = new LinkedList<ScavangerEntry>();

        // shutdown
        volatile bool _shuttingDown;

        // helper class to hold the data file name and the removal time
        class ScavangerEntry {
            string _filename;
            DateTime _utcDelete;

            internal ScavangerEntry(string filename, DateTime utcDelete) {
                _filename = filename;
                _utcDelete = utcDelete;
            }

            internal string Filename { get { return _filename; } }
            internal DateTime UtcDelete { get { return _utcDelete; } }
        }

        internal static TimeSpan FileValidationDelay {
            get { return _theCache._fileValidationDelay; }
        }

        internal static TimeSpan FileRemovalDelay {
            get { return _theCache._fileRemovalDelay; }
        }

        internal static int VaryByLimit {
            get { return _theCache._varyByLimit; }
        }

        internal static string Location {
            get { return _theCache._location; }
        }

        internal static bool ShuttingDown {
            get { return _theCache._shuttingDown; }
        }

        public static void EnsureInitialized() {
            lock (_initLock) {
                if (_theCache == null) {
                    _theCache = new Cache();

                    try {
                        _theCache.Startup();
                    }
                    catch (Exception e) {
                        _initError = e;
                        throw;
                    }

                    HostingEnvironment.RegisterObject(_theCache);
                }

                CheckInitialized();
            }
        }

        static void CheckInitialized() {
            if (_theCache == null) {
                throw new InvalidOperationException("Cache is not initialized");
            }

            if (_initError != null) {
                throw new InvalidOperationException("Cache failed to initialize", _initError);
            }
        }

        public static Tracker Lookup(HttpContext context) {
            CheckInitialized();
            return _theCache.LookupTracker(context);
        }

        public static void ScheduleFileDeletion(string filename) {
            _theCache.AddToRemovalList(filename);
            _theCache.ScheduleScavanger();
        }

        void IRegisteredObject.Stop(bool immediate) {
            if (immediate) {
                // wait for scavanging to complete
                while (!Tracker.ScavangingCompleted) {
                    Thread.Sleep(50);
                }

                HostingEnvironment.UnregisterObject(this);
            }
            else {
                _shuttingDown = true;

                Cleanup();

                // delay unregistration if the initial scavanging is still in progress
                if (Tracker.ScavangingCompleted) {
                    HostingEnvironment.UnregisterObject(this);
                }
            }
        }

        void Startup() {
            DiskOutputCacheSettingsSection config = (DiskOutputCacheSettingsSection)
                WebConfigurationManager.GetWebApplicationSection("diskOutputCacheSettings");

            if (config == null) {
                // no config - the list of URLs is empty
                return;
            }

            // remember deletion delay
            _fileRemovalDelay = config.FileRemovalDelay;

            // remember validation delay
            _fileValidationDelay = config.FileValidationDelay;

            // remember scavanging delay
            _fileScavangingDelay = config.FileScavangingDelay;

            // vary by limit (max count of entries per url)
            _varyByLimit = config.VaryByLimitPerUrl;

            // calculate location
            if (string.IsNullOrEmpty(config.Location)) {
                // the default location is in temporary asp.net files
                _location = Path.Combine(HttpRuntime.CodegenDir, "DiskOutputCache");

                if (!Directory.Exists(_location)) {
                    Directory.CreateDirectory(_location);
                }
            }
            else {
                _location = HttpContext.Current.Server.MapPath(config.Location);

                if (!Directory.Exists(_location)) {
                    throw new InvalidDataException(string.Format("Invalid location '{0}'", _location));
                }
            }

            // make sure we can write to the location
            try {
                string testFile = Path.Combine(_location, "test" + DateTime.UtcNow.ToFileTime() + ".txt");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
            }
            catch {
                throw new InvalidDataException(string.Format("Invalid location '{0}' -- failed to write", _location));
            }

            // read settings for individual URls
            foreach (CachedUrlsElement e in config.CachedUrls) {
                // normalize path

                string path = e.Path;

                if (!VirtualPathUtility.IsAppRelative(path) && !VirtualPathUtility.IsAbsolute(path)) {
                    throw new InvalidDataException(string.Format("Invalid path '{0}', absolute or app-relative path expected", path));
                }

                path = VirtualPathUtility.ToAbsolute(path);

                // create file path prefix for this path
                string relPathPrefix = VirtualPathUtility.ToAppRelative(path);
                if (relPathPrefix.StartsWith("~/")) {
                    relPathPrefix = relPathPrefix.Substring(2);
                }
                relPathPrefix = relPathPrefix.Replace('.', '_');
                relPathPrefix = relPathPrefix.Replace('/', '_');

                // list of verbs
                string[] verbs = ParseStringList(e.Verbs);
                if (verbs.Length == 0) {
                    throw new InvalidDataException(string.Format("Invalid list of verbs '{0}'", e.Verbs));
                }

                // vary-by
                string[] varyBy = ParseStringList(e.VaryBy);

                // remember the tracker object
                _trackers[path] = new Tracker(path, Path.Combine(_location, relPathPrefix), e.Duration, 
                    verbs, varyBy, e.EmptyQueryStringOnly, e.EmptyPathInfoOnly, e.ServeFromMemory);
            }

            if (CheckIfNeedToScavangeFilesThisTimeOnAppDomainStarted()) {
                // go through all temp files to figure out what needs to be timed out
                // this is done on app domain startup, but not every time
                Tracker.ScavangeFilesOnAppDomainStartup(_location);
            }
        }

        void Cleanup() {
            Timer t = _scavangingTimer;
            if (t != null) {
                t.Dispose();
            }
        }

        Tracker LookupTracker(HttpContext context) {
            HttpRequest request = context.Request;
            Tracker tracker;

            if (_trackers.TryGetValue(request.FilePath, out tracker)) {
                return tracker.FindTrackerForRequest(request);
            }

            // not cacheable
            return null;
        }

        bool CheckIfNeedToScavangeFilesThisTimeOnAppDomainStarted() {
            // keep the timestamp of the last disk scavanging on disk
            string scavangingTimestampFilename = Path.Combine(_location, "scavanger.timestamp");

            // try to read it from disk
            try {
                if (File.Exists(scavangingTimestampFilename)) {
                    DateTime lastScavangingUtc = DateTime.FromBinary(long.Parse(File.ReadAllText(scavangingTimestampFilename)));

                    if (DateTime.UtcNow < (lastScavangingUtc + _fileScavangingDelay)) {
                        // no need to scavange too soon
                        return false;
                    }
                }
            }
            catch {
            }

            // update the timestamp on disk
            try {
                File.WriteAllText(scavangingTimestampFilename, DateTime.UtcNow.ToBinary().ToString());
            }
            catch {
            }

            return true;
        }

        void AddToRemovalList(string filename) {
            ScavangerEntry entry = new ScavangerEntry(filename, DateTime.UtcNow.Add(_fileRemovalDelay));

            lock (_fileRemovalList) {
                _fileRemovalList.AddLast(entry);
            }
        }

        void ScheduleScavanger() {
            if (_scavangingTimer != null) {
                // one timer at a time
                return;
            }

            int timeout = (int)(_fileRemovalDelay.TotalMilliseconds * 1.1);

            lock (this) {
                if (_scavangingTimer == null) {
                    _scavangingTimer = new Timer(ScavangerCallback, null, timeout, Timeout.Infinite);
                }
            }
        }

        void ScavangerCallback(object state) {
            int numDeleted = 0, numSkipped = 0;

            try {
                DateTime utcNow = DateTime.UtcNow;
                List<string> filesToBeDeleted = new List<string>();

                lock (_fileRemovalList) {
                    LinkedListNode<ScavangerEntry> next = _fileRemovalList.First;

                    while (next != null) {
                        LinkedListNode<ScavangerEntry> current = next;
                        next = current.Next;

                        if (utcNow > current.Value.UtcDelete) {
                            filesToBeDeleted.Add(current.Value.Filename);
                            _fileRemovalList.Remove(current);
                            numDeleted++;
                        }
                        else {
                            numSkipped++;
                        }
                    }
                }

                // delete the files outside of the lock
                foreach (string filename in filesToBeDeleted) {
                    try {
                        File.Delete(filename);
                    }
                    catch {
                        // move to the next file if one cannot be deleted
                    }
                }
            }
            finally {
                _scavangingTimer = null;
            }

            if (numSkipped > 0) {
                ScheduleScavanger();
            }
        }

        static string[] ParseStringList(string listAsString) {
            string[] list = listAsString.Trim().Split(',');
            List<string> result = new List<string>(list.Length);

            foreach (string elem in list) {
                string s = elem.Trim();
                if (s.Length > 0) {
                    result.Add(s);
                }
            }

            return result.ToArray();
        }
    }
}
