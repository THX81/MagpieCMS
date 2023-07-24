/*=======================================================================
  Copyright (C) Microsoft Corporation.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

using System;
using System.Web;

namespace DiskOutputCache {

    /*
     * Module is an IHttpModules that calls into Cache object to perform
     * output cache operations.
     * 
     * On ResolveRequestCache event it performs the lookup and either
     * (a) ignores the current request (from cache perspective) or
     * (b) serves the cached response and completes the request or
     * (c) captures the output using response filter
     * 
     * On UpdateRequestCache event the module inserts the new content
     * into cache
     * 
     * On EndResponse it performs cleanup
     * 
     * The cache functionality is implemented by Cache object.
     * Individual cached Urls are tracked via Tracker objects.
     * 
     */

    public sealed class Module : IHttpModule {
        HttpApplication _app;
        Tracker _trackerCapturingResponse;

        void IHttpModule.Init(HttpApplication app) {
            _app = app;
            // module's Init method could be called many times,
            // while Cache needs to be initialized only once.
            // EnsureInitilized takes care of that
            Cache.EnsureInitialized();

            app.ResolveRequestCache += new EventHandler(OnResolveRequestCache);
            app.UpdateRequestCache += new EventHandler(OnUpdateRequestCache);
            app.EndRequest += new EventHandler(OnEndRequest);
        }

        void IHttpModule.Dispose() {
        }

        void OnResolveRequestCache(object sender, EventArgs e) {
            // start clean
            _trackerCapturingResponse = null;

            Tracker tracker = Cache.Lookup(_app.Context);

            if (tracker == null) {
                // this request is not subject to cache
                return;
            }

            // try to send response or start capture
            // (use 'finally' because starting capture would lock)
            try {} finally {
                if (tracker.TrySendResponseOrStartResponseCapture(_app.Response)) {
                    // successfully sent current response
                    _app.CompleteRequest();
                }
                else {
                    // started capturing
                    _trackerCapturingResponse = tracker;
                }
            }
        }

        void OnUpdateRequestCache(object sender, EventArgs e) {
            if (_trackerCapturingResponse != null) {
                // if capturing, finish the capture and save the file
                _trackerCapturingResponse.FinishCaptureAndSaveResponse(_app.Response);
                _trackerCapturingResponse = null;
            }
        }

        void OnEndRequest(object sender, EventArgs e) {
            if (_trackerCapturingResponse != null) {
                // if still capturing, abandon the process
                try {
                    _trackerCapturingResponse.CancelCapture(_app.Response);
                }
                finally {
                    _trackerCapturingResponse = null;
                }
            }
        }
    }
}
