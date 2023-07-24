/*=======================================================================
  Copyright (C) Microsoft Corporation.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

using System;
using System.IO;

namespace DiskOutputCache {
    // helper class to capture the response into a file
    class ResponseFilter : Stream {
        string _captureFilename;
        FileStream _captureFileStream;
        Stream _chainedStream;

        internal ResponseFilter(Stream filterChain, string captureFilename) {
            _chainedStream = filterChain;
            _captureFilename = captureFilename;
            _captureFileStream = new FileStream(_captureFilename, FileMode.CreateNew, FileAccess.Write);
        }

        internal void StopFiltering(bool deleteDataFile) {
            if (_captureFileStream != null) {
                _captureFileStream.Close();
                _captureFileStream = null;
            }

            if (deleteDataFile) {
                File.Delete(_captureFilename);
            }
        }

        internal string CaptureFilename {
            get { return _captureFilename; } 
        }

        // Stream implementation

        public override bool CanRead {
            get { return false; } 
        }

        public override bool CanSeek {
            get { return false; } 
        }

        public override bool CanWrite {
            get { return true; } 
        }

        public override void Flush() {
            _chainedStream.Flush();

            if (_captureFileStream != null) {
                _captureFileStream.Flush();
            }
        }

        public override void Write(byte[] buffer, int offset, int count) {
            _chainedStream.Write(buffer, offset, count);

            if (_captureFileStream != null) {
                _captureFileStream.Write(buffer, offset, count);
            }
        }

        public override long Length {
            get { throw new NotSupportedException(); } 
        }

        public override long Position {
            get { throw new NotSupportedException(); } 
            set { throw new NotSupportedException(); } 
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotSupportedException(); 
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotSupportedException(); 
        }

        public override void SetLength(long value) { 
            throw new NotSupportedException(); 
        }
    }

}
