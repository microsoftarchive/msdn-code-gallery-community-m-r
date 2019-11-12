// Copyright 2010 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
// CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
// MERCHANTABLITY OR NON-INFRINGEMENT. 

// See the Apache 2 License for the specific language governing 
// permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// Event args for the CacheRequestHandler.ProcessCacheRequestAsync call.
    /// </summary>
    class ProcessCacheRequestCompletedEventArgs : EventArgs
    {
        public Guid Id;
        public ChangeSet ChangeSet;
        public ChangeSetResponse ChangeSetResponse;
        public Exception Error;
        public object State;
        public uint BatchUploadCount;

        public ProcessCacheRequestCompletedEventArgs(Guid id, ChangeSetResponse response, int uploadCount, Exception error, object state)
        {
            this.ChangeSetResponse = response;
            this.Error = error;
            this.State = state;
            this.Id = id;
            this.BatchUploadCount = (uint)uploadCount;

            // Check that error is carried over to the response
            if (this.Error != null)
            {
                if (this.ChangeSetResponse == null)
                {
                    this.ChangeSetResponse = new ChangeSetResponse();
                }
                this.ChangeSetResponse.Error = this.Error;
            }
        }

        public ProcessCacheRequestCompletedEventArgs(Guid id, ChangeSet changeSet, Exception error, object state)
        {
            this.ChangeSet = changeSet;
            this.Error = error;
            this.State = state;
            this.Id = id;
        }
    }
}
