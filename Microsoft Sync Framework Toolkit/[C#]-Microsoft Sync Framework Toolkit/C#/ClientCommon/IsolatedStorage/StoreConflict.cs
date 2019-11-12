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
    /// Conflict that happens when SaveChanges is attempted but a modified item has been
    /// changed during sync.  CurrentEntity is the entity which is represented by the store.
    /// Modified entity is the currently modified one.
    /// </summary>
    public class StoreConflict : Conflict
    {
        /// <summary>
        /// Constructor which intializes the StoreConflict with the specified context.
        /// </summary>
        /// <param name="context"></param>
        internal StoreConflict(IsolatedStorageOfflineContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Returns the entity that the user has modified and which cannot currently be saved.
        /// </summary>
        public IsolatedStorageOfflineEntity ModifiedEntity
        {
            get;
            internal set;
        }

        /// <summary>
        /// Calls the context to resolve it.
        /// </summary>
        /// <param name="resolutionAction"></param>
        public void Resolve(StoreConflictResolutionAction resolutionAction)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Conflict has already been resolved");
            }

            _context.ResolveStoreConflict(this, resolutionAction);
        }

        /// <summary>
        /// Called internally to avoid a deadlock when CancelChanges is called on the
        /// context
        /// </summary>
        /// <param name="resolutionAction"></param>
        internal void ResolveInternal(StoreConflictResolutionAction resolutionAction)
        {
            _context.ResolveStoreConflictNoLock(this, resolutionAction);
        }

        /// <summary>
        /// Clears out the context so that conflicts cannot be resolved multiple times.
        /// </summary>
        internal void ClearContext()
        {
            _context = null;
        }

        private IsolatedStorageOfflineContext _context;
    }
}
