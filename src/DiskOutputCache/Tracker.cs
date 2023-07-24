/*=======================================================================
  Copyright (C) Microsoft Corporation.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Xml;

namespace DiskOutputCache {

    /*
     * Tracker represents individual cacheable url
     * 
     * On disk the information is stored in 2 files: .info and .data
     * Each file has page url prefix, plus a unique hash-code to avoid
     * conflicts.
     * 
     * Tracker is responsible for capturing and serving cached data.
     * 
     * To implement support for vary-by cachining each top-level Tracker
     * associated with a path keeps a dictionary of vary-by trackers
     * 
     */

    class Tracker {
        // configuration parameters
        string _path;
        TimeSpan _duration;
        string[] _verbs;
        string[] _varyBy;
        bool _emptyQueryStringOnly;
        bool _emptyPathInfoOnly;
        bool _serveFromMemory;

        // current state of the Tracker
        string _filenamePrefix; // without the extension or the random part
        string _dataFilename;
        string _infoFilename;
        volatile bool _triedToLoadCachedResponse;
        volatile bool _cachedResponseLoaded;
        byte[] _cachedResponseBytes;
        DateTime _cachedResponseExpiry;
        long _cachedResponseHash;
        DateTime _nextResponseValidationTime;

        // data related to capturing response
        ResponseFilter _capturingFilter;
        HttpResponse _capturingResponse;
        ManualResetEvent _capturingEvent;

        // support for vary-by
        Tracker _parent;    // when not-null indicates child tracker
        string _varyById;   // unique vary-by key for this tracker
        Dictionary<string, Tracker> _varyByTrackers;

        // strings
        const string InfoFileExt = ".info";
        const string DataFileExt = ".data";
        const string TempFileExt = ".temp";
        const string InfoTagName = "diskOutputCacheItem";

        // static to indicate the completion of scavanging on app domain startup
        static volatile bool _scavangingCompleted;

        // ctor just stores config - doesn't go to disk to read cached response
        public Tracker(string path, string filePrefix, TimeSpan duration, string[] verbs, string[] varyBy,
            bool emptyQueryStringOnly, bool emptyPathInfoOnly, bool serveFromMemory) {

            _path = path;
            _duration = duration;
            _verbs = (string[])verbs.Clone();
            _varyBy = (string[])varyBy.Clone();
            _emptyQueryStringOnly = emptyQueryStringOnly;
            _emptyPathInfoOnly = emptyPathInfoOnly;
            _serveFromMemory = serveFromMemory;
            _varyById = String.Empty;
            _filenamePrefix = filePrefix;

            _capturingEvent = new ManualResetEvent(true);
            _varyByTrackers = new Dictionary<string, Tracker>();
        }

        // private ctor to create a child tracker for a specific vary-by value
        Tracker(Tracker parent, string varyById) {
            _parent = parent;
            _path = parent._path;
            _duration = parent._duration;
            _verbs = parent._verbs;
            _varyBy = parent._varyBy;
            _serveFromMemory = parent._serveFromMemory;
            _varyById = varyById;
            _filenamePrefix = string.Format("{0}_q_{1:x8}", parent._filenamePrefix, varyById.GetHashCode());

            _capturingEvent = new ManualResetEvent(true);
            _varyByTrackers = null; // no need for sub-sub-trackers
        }

        // finds and matches a tracker (this a or sub-tracker in varyby cases) for a HttpRequest
        public Tracker FindTrackerForRequest(HttpRequest request) {
            // path
            if (String.Compare(_path, request.FilePath, StringComparison.OrdinalIgnoreCase) != 0) {
                return null;
            }

            // verbs
            bool verbMatchFound = String.Compare(_verbs[0], request.HttpMethod, StringComparison.OrdinalIgnoreCase) == 0;

            if (!verbMatchFound && _verbs.Length > 1) {
                for (int i = 1; i < _verbs.Length; i++) {
                    if (String.Compare(_verbs[i], request.HttpMethod, StringComparison.OrdinalIgnoreCase) == 0) {
                        verbMatchFound = true;
                        break;
                    }
                }
            }

            if (!verbMatchFound) {
                return null;
            }

            // path info
            if (_emptyPathInfoOnly && !string.IsNullOrEmpty(request.PathInfo)) {
                return null;
            }

            // query string
            if (_emptyQueryStringOnly && request.QueryString.Count > 0) {
                return null;
            }
            
            // in non-vary-by case return this tracker
            if (_varyBy == null || _varyBy.Length == 0) {
                return this;
            }

            // get the child tracker corresponding to vary-by key for this request
            Tracker childTracker = null;
            string varyByKey = CalculateVaryByKey(_varyBy, request);

            lock (_varyByTrackers) {
                if (!_varyByTrackers.TryGetValue(varyByKey, out childTracker)) {
                    // create a new child tracker if not exceeding the limit already
                    if (_varyByTrackers.Count < Cache.VaryByLimit) {
                        childTracker = new Tracker(this, varyByKey);
                        _varyByTrackers.Add(varyByKey, childTracker);
                    }
                }
            }

            return childTracker;
        }

        public bool TrySendResponseOrStartResponseCapture(HttpResponse response) {
            byte[] responseData = null;
            string responseFile = null;

            // loop while trying to either send or capture the response
            // (the loop is needed for cases when another thread does the capture)
            for (; ; ) {

                lock (this) {
                    // attempt to find the cached response on disk (only once)
                    if (!_triedToLoadCachedResponse) {
                        LookForCachedResponseOnDisk();
                        _triedToLoadCachedResponse = true;
                    }

                    if (_cachedResponseLoaded && ValidateLoadedCachedResponse()) {
                        // serve the cached response if validated
                        if (_serveFromMemory) {
                            responseData = _cachedResponseBytes;
                        }
                        else {
                            responseFile = _dataFilename;
                        }

                        // send the response outside of the lock
                        break;
                    }

                    // couldn't send the response - try to capture it under lock
                    // (don't attempt to capture the same response from 2 threads at the same time)
                    if (_capturingResponse == null) {
                        // generate new file name
                        string filename = string.Format("{0}_{1:x8}{2}",
                            _filenamePrefix, Guid.NewGuid().ToString().GetHashCode(), TempFileExt);

                        _capturingFilter = new ResponseFilter(response.Filter, filename);
                        response.Filter = _capturingFilter;

                        // move the event - non-signaled state
                        _capturingEvent.Reset();
                        // remember the response
                        _capturingResponse = response;
                        // started capturing - return from this method
                        break;
                    }
                }

                // capturing started from another thread - wait until done and continue (outside of the lock)
                _capturingEvent.WaitOne();
            }

            // send the cached response if available (outside of the lock)
            if (responseData != null) {
                response.OutputStream.Write(responseData, 0, responseData.Length);
                return true;
            }
            else if (responseFile != null) {
                try {
                    response.TransmitFile(responseFile);
                }
                catch {
                    // if there is a problem sending data file, invalidate the cached response
                    InvalidateCachedResponse();
                    throw;
                }

                return true;
            }
            else {
                return false;
            }
        }

        public void FinishCaptureAndSaveResponse(HttpResponse response) {
            if (_capturingResponse != response) {
                throw new InvalidOperationException("Attempt to complete response capture occured on wrong HttpResponse");
            }

            FinishOrCancelCapture(response, false /*cancel*/);
        }

        public void CancelCapture(HttpResponse response) {
            if (_capturingResponse != null && _capturingResponse != response) {
                throw new InvalidOperationException("Attempt to cancel response capture occured on wrong HttpResponse");
            }

            FinishOrCancelCapture(response, true /*cancel*/);
        }

        // try to find an existing cached file on disk
        void LookForCachedResponseOnDisk() {
            // walk trough all files on disk matching <prefix>????????.info, finding the right one

            string dir = Path.GetDirectoryName(_filenamePrefix);
            string pattern = Path.GetFileName(_filenamePrefix) + "_????????" + InfoFileExt;
            string[] files = Directory.GetFiles(dir, pattern, SearchOption.TopDirectoryOnly);

            foreach (string infoFilename in files) {
                try {
                    string path;
                    string varyById;
                    DateTime timestamp;
                    DateTime expiry;
                    long hash;

                    if (TryReadInfoFile(infoFilename, out path, out varyById, out timestamp, out expiry, out hash)) {
                        // check that this info file matches current file path
                        if (string.Compare(path, _path, StringComparison.OrdinalIgnoreCase) != 0) {
                            continue;
                        }

                        // check that vary-by key matches
                        if (string.Compare(varyById, _varyById, StringComparison.OrdinalIgnoreCase) != 0) {
                            continue;
                        }

                        string dataFilename = infoFilename.Replace(InfoFileExt, DataFileExt);

                        // check for 1 day old file
                        if (DateTime.Now.AddDays(-1) > File.GetCreationTime(infoFilename))
                        {
                            File.Delete(infoFilename);
                            File.Delete(dataFilename);
                        }

                        // check that data file exists
                        if (!File.Exists(dataFilename)) {
                            continue;
                        }

                        // calculate response expiration
                        DateTime newExpiry = timestamp + _duration;

                        if (newExpiry > expiry) {
                            // don't consider response valid longer than originally intended
                            // (this could be possible if the duration changed in config)
                            newExpiry = expiry;
                        }

                        // check that not expired (only timestamp)
                        DateTime utcNow = DateTime.UtcNow;

                        if (expiry <= utcNow) {
                            DeleteFiles(infoFilename, dataFilename);
                            continue;
                        }

                        // found the right file (and data file)
                        _infoFilename = infoFilename;
                        _dataFilename = dataFilename;
                        _cachedResponseHash = hash;
                        _cachedResponseExpiry = newExpiry;
                        _nextResponseValidationTime = utcNow;

                        // read the data if needed
                        _cachedResponseBytes = new byte[0];
                        if (_serveFromMemory) {
                            _cachedResponseBytes = File.ReadAllBytes(dataFilename);
                        }

                        _cachedResponseLoaded = true;
                        break;
                    }
                }
                catch {
                    // if one file failed to load move to the next one
                }
            }
        }

        bool ValidateLoadedCachedResponse() {
            DateTime utcNow = DateTime.UtcNow;

            // check time-based expiration
            bool expired = (_cachedResponseExpiry <= utcNow);

            // check for recompilation (not too often)
            if (!expired && _nextResponseValidationTime <= utcNow) {
                expired = (_cachedResponseHash != CalculateHandlerHash(_path));
                _nextResponseValidationTime = utcNow + Cache.FileValidationDelay;
            }

            if (!expired) {
                return true;
            }

            // if expired delete the info file and schedule deletion of the data file
            InvalidateCachedResponse();
            return false;
        }

        void InvalidateCachedResponse() {
            DeleteFiles(_infoFilename, _dataFilename);
            _cachedResponseLoaded = false;
        }

        void FinishOrCancelCapture(HttpResponse response, bool cancel) {
            if (_capturingResponse != response) {
                return;
            }

            if (_capturingFilter == null) {
                throw new InvalidOperationException("Response capturing filter is missing.");
            }

            lock (this) {
                try {
                    _capturingFilter.StopFiltering(cancel);

                    if (!cancel) {
                        // remember the captured response
                        string tempFilename = _capturingFilter.CaptureFilename;
                        _infoFilename = tempFilename.Replace(TempFileExt, InfoFileExt);
                        _dataFilename = tempFilename.Replace(TempFileExt, DataFileExt);

                        DateTime timestamp = DateTime.UtcNow;
                        _cachedResponseHash = CalculateHandlerHash(_path);
                        _cachedResponseExpiry = timestamp + _duration;
                        _nextResponseValidationTime = timestamp + Cache.FileValidationDelay;

                        // save info file
                        string info = string.Format(
                            "<{0} path=\"{1}\" vary=\"{2}\" timestamp=\"{3}\" expiry=\"{4}\" hash=\"{5}\" />",
                            InfoTagName, _path, _varyById, 
                            timestamp.ToBinary(), _cachedResponseExpiry.ToBinary(),
                            _cachedResponseHash);
                        File.WriteAllText(_infoFilename, info, Encoding.UTF8);

                        // rename temp file into data file
                        File.Move(tempFilename, _dataFilename);

                        // read the data into memory if needed
                        _cachedResponseBytes = new byte[0];
                        if (_serveFromMemory) {
                            _cachedResponseBytes = File.ReadAllBytes(_dataFilename);
                        }

                        // now everything's ready
                        _cachedResponseLoaded = true;
                    }
                }
                finally {
                    // notify any waiting threads that capturing is complete
                    _capturingFilter = null;
                    _capturingResponse = null;
                    _capturingEvent.Set();
                }
            }
        }

        // schedule all expired disk cache entries for deletion
        internal static void ScavangeFilesOnAppDomainStartup(string location) {
            ThreadPool.QueueUserWorkItem(ScavangeFiles);
        }

        static void ScavangeFiles(object state) {
            try {
                string pattern;
                string[] files;

                // walk trough all *.data files, look for missing info files
                pattern = "*" + DataFileExt;
                files = Directory.GetFiles(Cache.Location, pattern, SearchOption.TopDirectoryOnly);

                foreach (string dataFilename in files) {
                    try {
                        string infoFilename = dataFilename.Replace(DataFileExt, InfoFileExt);

                        if (!File.Exists(infoFilename)) {
                            // schedule data file for deletion
                            Cache.ScheduleFileDeletion(dataFilename);
                        }
                    }
                    catch {
                        // ignore single file failures - move to the next file
                    }

                    if (Cache.ShuttingDown) {
                        // stop scavanging on app domain shutdown
                        return;
                    }
                }

                // walk through all *.info files, look for expired ones
                pattern = "*" + InfoFileExt;
                files = Directory.GetFiles(Cache.Location, pattern, SearchOption.TopDirectoryOnly);

                foreach (string infoFilename in files) {
                    try {
                        string path;
                        string varyById;
                        DateTime timestamp;
                        DateTime expiry;
                        long hash;

                        if (TryReadInfoFile(infoFilename, out path, out varyById, out timestamp, out expiry, out hash)) {
                            if (expiry < DateTime.UtcNow) {
                                string dataFilename = infoFilename.Replace(InfoFileExt, DataFileExt);
                                DeleteFiles(infoFilename, dataFilename);
                            }
                        }
                    }
                    catch {
                        // ignore single file failures - move to the next file
                    }

                    if (Cache.ShuttingDown) {
                        // stop scavanging on app domain shutdown
                        return;
                    }
                }
            }
            finally {
                _scavangingCompleted = true;
            }
        }

        internal static bool ScavangingCompleted {
            get { return _scavangingCompleted; }
        }

        // helper to parse info file
        static bool TryReadInfoFile(string infoFilename, out string path, out string vary, 
                        out DateTime timestamp, out DateTime expiry, out long hash) {
            path = string.Empty;
            vary = string.Empty;
            timestamp = DateTime.MinValue;
            expiry = DateTime.MinValue;
            hash = 0;

            // read the XML file: 
            // <diskOutputCacheItem path="..." vary="..." timestamp="..." expiry="..." hash="..." />

            XmlDocument doc = new XmlDocument();
            doc.Load(infoFilename);

            XmlNode rootNode = null;
            for (XmlNode node = doc.FirstChild; node != null; node = node.NextSibling) {
                if (node.NodeType == XmlNodeType.Element) {
                    rootNode = node;
                    break;
                }
            }

            if (rootNode != null && rootNode.Name == InfoTagName) {
                path = rootNode.Attributes["path"].Value;
                vary = rootNode.Attributes["vary"].Value;
                timestamp = DateTime.FromBinary(long.Parse(rootNode.Attributes["timestamp"].Value));
                expiry = DateTime.FromBinary(long.Parse(rootNode.Attributes["expiry"].Value));
                hash = long.Parse(rootNode.Attributes["hash"].Value);
                return true;
            }

            return false;
        }

        // helper to delete the info file and schedule data file for deletion in the future
        static void DeleteFiles(string infoFilename, string dataFilename) {
            try {
                File.Delete(infoFilename);
            }
            catch {
                // could be deleted already by the scavanger
            }

            Cache.ScheduleFileDeletion(dataFilename);
        }

        // helper to calculate hash that would change when the page is recompiled
        static long CalculateHandlerHash(string path) {
            long hash = 0;

            // use the type name (and the containing assembly) as the hash
            // assuming the assembly will get recompiled if a dependency changes

            try {
                Type t = BuildManager.GetCompiledType(path);

                if (t != null) {
                    string typeName = t.AssemblyQualifiedName;
                    string typeFileName = t.Module.FullyQualifiedName;
                    hash = ((long)typeName.GetHashCode() & 0xffffffff) +
                          (((long)typeFileName.GetHashCode() & 0xffffffff) << 29);
                }
            }
            catch {
                // failure to calculate hash is not fatal
            }

            return hash;
        }

        // helper to create a canonicalized representation of the query string
        // give vary-by and the current request
        static string CalculateVaryByKey(string[] varyBy, HttpRequest request) {
            if (varyBy == null || varyBy.Length == 0) {
                return string.Empty;
            }
            else if (varyBy.Length == 1 && varyBy[0] == "*") {
                return request.QueryString.ToString();
            }
            else {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < varyBy.Length; i++) {
                    sb.Append(request.QueryString[varyBy[i]]);
                    sb.Append('-');
                }

                return HttpUtility.UrlEncodeUnicode(sb.ToString());
            }
        }
    }
}
