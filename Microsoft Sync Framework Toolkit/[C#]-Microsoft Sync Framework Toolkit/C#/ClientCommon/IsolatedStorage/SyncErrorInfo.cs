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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// Class which contains the last conflict and error that occurred during sync
    /// for a given entity.
    /// </summary>
    public class SyncErrorInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Whether or not the error info has a sync conflict.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool HasSyncConflict
        {
            get
            {
                return _syncConflict != null;
            }
        }

        /// <summary>
        /// Whether or not the error info has a sync error.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool HasSyncError
        {
            get
            {
                return _syncError != null;
            }
        }

        /// <summary>
        /// The sync error.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SyncError SyncError
        {
            get
            {
                return _syncError;
            }
        }

        /// <summary>
        /// The sync conflict.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SyncConflict SyncConflict
        {
            get
            {
                return _syncConflict;
            }
        }

        /// <summary>
        /// Clears the sync conflict and removes it from the conflict list on the context.
        /// </summary>
        public void ClearSyncConflict()
        {
            if (_syncConflict != null)
            {
                _context.ClearSyncConflict(_syncConflict);
                _syncConflict = null;

                OnPropertyChanged("SyncConflict");
                OnPropertyChanged("HasSyncConflict");
            }
        }

        /// <summary>
        /// Sets the sync conflict to null. This method is not thread safe.    
        /// </summary>
        internal void UnsafeClearSyncConflict()
        {
            _syncConflict = null;

            OnPropertyChanged("SyncConflict");
            OnPropertyChanged("HasSyncConflict");
        }

        /// <summary>
        ///  Sets the sync error to null. This method is not thread safe.
        /// </summary>
        internal void UnsafeClearSyncError()
        {
            _syncConflict = null;

            OnPropertyChanged("SyncConflict");
            OnPropertyChanged("HasSyncConflict");
        }

        /// <summary>
        /// Clears the sync error and removes it from the error list on the context.
        /// </summary>
        public void ClearSyncError()
        {
            if (_syncError != null)
            {
                _context.ClearSyncError(_syncError);
                _syncError = null;

                OnPropertyChanged("SyncError");
                OnPropertyChanged("HasSyncError");
            }
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Private method which handles raising the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property for which the event is being raised</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        /// <summary>
        /// Sets the sync conflict, providing the cache data so that the conflict can be removed if ClearSyncConflict
        /// is called.
        /// </summary>
        ///<param name="context">IsolatedStorageOfflineContext</param>
        /// <param name="syncConflict">conflict to set.</param>
        internal void SetSyncConflict(
            IsolatedStorageOfflineContext context, 
            SyncConflict syncConflict)
        {
            SyncConflict oldConflict = this._syncConflict;

            this._context = context;
            this._syncConflict = syncConflict;

            OnPropertyChanged("SyncConflict");

            if (oldConflict == null)
            {
                OnPropertyChanged("HasSyncConflict");
            }
        }

        /// <summary>
        /// Sets the sync error, providing the cache data so that the error can be removed if ClearSyncerror
        /// is called.
        /// </summary>
        ///<param name="context">IsolatedStorageOfflineContext</param>
        /// <param name="syncError">error to set.</param>
        internal void SetSyncError(IsolatedStorageOfflineContext context, SyncError syncError)
        {
            SyncError oldError = this._syncError;

            this._context = context;
            this._syncError = syncError;

            OnPropertyChanged("SyncError");

            if (oldError == null)
            {
                OnPropertyChanged("HasSyncError");
            }
        }

        private IsolatedStorageOfflineContext _context = null;
        private SyncConflict _syncConflict = null;
        private SyncError _syncError = null;
    }
}
