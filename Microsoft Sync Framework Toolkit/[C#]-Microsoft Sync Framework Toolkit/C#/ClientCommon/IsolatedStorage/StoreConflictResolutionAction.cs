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

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// Enumerator which represents the resolution actions which are available for store conflicts.
    /// There are only two choices, AcceptStoreEntity or AcceptModifiedEntity.  If a merge resolution
    /// is desired, the ModifiedEntity of the StoreConflict should be changed, and the AcceptModifiedEntity
    /// should be called.
    /// </summary>
    public enum StoreConflictResolutionAction
    {
        /// <summary>
        /// Accept the store version of the entity, rolling back changes to the modified entity.
        /// </summary>
        AcceptStoreEntity,

        /// <summary>
        /// Accept the modified entity, allowing it to be saved.
        /// </summary>
        AcceptModifiedEntity
    }
}
