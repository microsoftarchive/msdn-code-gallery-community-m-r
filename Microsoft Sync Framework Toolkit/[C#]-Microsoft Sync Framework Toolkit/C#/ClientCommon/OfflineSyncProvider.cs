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
    /// OfflineSyncProvider
    /// </summary>
    public abstract class OfflineSyncProvider
    {
        /// <summary>
        /// Begin Session
        /// </summary>
        public abstract void BeginSession();

        /// <summary>
        /// GetChangeSet, called on the Source to get the changes
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public abstract ChangeSet GetChangeSet(Guid state);

        /// <summary>
        /// OnChangeSetUploaded, fired when changeset is uploaded
        /// </summary>
        /// <param name="state"></param>
        /// <param name="response"></param>
        public abstract void OnChangeSetUploaded(Guid state, ChangeSetResponse response);

        /// <summary>
        /// Gets the server blob
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetServerBlob();

        /// <summary>
        /// SaveChangeSet, called on the destination to save the changes on the local storage
        /// </summary>
        /// <param name="changeSet"></param>
        public abstract void SaveChangeSet(ChangeSet changeSet);

        /// <summary>
        /// End Session
        /// </summary>
        public abstract void EndSession();
    }
}
