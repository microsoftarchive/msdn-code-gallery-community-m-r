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
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Threading;
using System.IO.IsolatedStorage;
using System.IO;
using System.Security.Cryptography;

[assembly: InternalsVisibleTo("System.ServiceModel.Web, PublicKey=00240000048000009400000006020000002400005253413100040000010001008d56c76f9e8649383049f383c44be0ec204181822a6c31cf5eb7ef486944d032188ea1d3920763712ccb12d75fb77e9811149e6148e5d32fbaab37611c1878ddc19e20ef135d0cb2cff2bfec3d115810c3d9069638fe4be215dbf795861920e5ab6f7db2e2ceef136ac23d5dd2bf031700aec232f6c6b1c785b4305c123b37ab")]
namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// IsolatedStorageOfflineContext
    /// </summary>
    public class IsolatedStorageOfflineContext : OfflineSyncProvider, IDisposable
    {
        /// <summary>
        /// Constructor for the offline context.
        /// </summary>
        /// <param name="schema">The schema that specifies the set of the collections for the context.</param>
        /// <param name="scopeName">The scope name used to identify the scope on the service.</param>
        /// <param name="cachePath">Path in isolated storage where the data will be stored.</param>
        /// <param name="uri">Uri of the scopeName.  Used to intialize the CacheController.</param>
        /// <remarks>
        /// If the Uri specified is different from the one that is stored in the cache path, the
        /// Load method will throw an InvalidOperationException.
        /// </remarks>
        public IsolatedStorageOfflineContext(IsolatedStorageSchema schema, string scopeName, string cachePath,
            Uri uri) : this(schema, scopeName, cachePath, uri, null)
        {
        }

        
        /// <summary>
        /// Constructor for the offline context which allows a symmetric encryption algorithm to be specified.
        /// </summary>
        /// <param name="schema">The schema that specifies the set of the collections for the context.</param>
        /// <param name="scopeName">The scope name used to identify the scope on the service.</param>
        /// <param name="cachePath">Path in isolated storage where the data will be stored.</param>
        /// <param name="uri">Uri of the scopeName.  Used to intialize the CacheController.</param>
        /// <param name="encryptionAlgorithm">The symmetric encryption algorithm to use for files on disk</param>
        /// <remarks>
        /// If the Uri specified is different from the one that is stored in the cache path, the
        /// Load method will throw an InvalidOperationException.
        /// </remarks>
        public IsolatedStorageOfflineContext(IsolatedStorageSchema schema, string scopeName, string cachePath,
            Uri uri, SymmetricAlgorithm encryptionAlgorithm)
        {
            if (schema == null)
            {
                throw new ArgumentNullException("schema");
            }

            if (string.IsNullOrEmpty(scopeName))
            {
                throw new ArgumentNullException("scopeName");
            }

            if (string.IsNullOrEmpty(cachePath))
            {
                throw new ArgumentNullException("cachePath");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            _isDisposed = false;

            _schema = schema;
            _scopeUri = uri;
            _scopeName = scopeName;
            _cachePath = cachePath;
            _storageHandler = new StorageHandler(schema, cachePath, encryptionAlgorithm);
            _saveSyncLock = new AutoResetLock();
            
            CreateCacheController();
        }

        /// <summary>
        /// Explicitly loads the data from the cache into memory synchronously.
        /// </summary>
        /// <remarks>
        /// Performing any method on the cache will implicitly load it if this method is not called. This
        /// method allows better control over when data is loaded.
        /// If the cache is already loaded, this method will do nothing.
        /// </remarks>
        public void Load()
        {
            ThrowIfDisposed();

            // Double-checked lock to ensure noone else is loading.
            if (!_loaded)
            {
                // Block in case anyone else is loading
                lock (_loadLock)
                {
                    // Only continue if we still aren't loaded
                    if (!_loaded)
                    {
                        // Do the actual loading.
                        LoadInternal();
                    }
                }
            }
        }

        /// <summary>
        /// Loads the data from the cache into memory asynchronously.
        /// </summary>
        /// <remarks>
        /// Performing any method on the cache will implicitly load it if this method is not called. This
        /// method allows better control over when data is loaded.
        /// If the cache is already loaded, this method will do nothing.
        /// </remarks>
        /// TODO: Fix to use AsyncOperationManager
        public void LoadAsync()
        {
            ThrowIfDisposed();

            // Use the ThreadPool to queue our load.  This will happen regardless of whether the cache is already loaded
            ThreadPool.QueueUserWorkItem(LoadThread);
        }
        
        /// <summary>
        /// Event called when the LoadAsync is completed.  This will be called even if the cache is already loaded.
        /// </summary>
        public event EventHandler<LoadCompletedEventArgs> LoadCompleted;

        /// <summary>
        /// Get the collection of entities corresponding to the desired type.  This method will load if the cache is not already
        /// loaded.
        /// </summary>
        /// <typeparam name="T">Type of entity to return</typeparam>
        /// <returns>An IEnumerable of the entities requested</returns>
        public IEnumerable<T> GetCollection<T>() where T: IsolatedStorageOfflineEntity
        {
            ThrowIfDisposed();
            Load();

            return (IsolatedStorageCollection<T>)_cacheData.Collections[typeof(T)];
        }


        /// <summary>
        /// Saves any outstanding changes made by the application.  This method will throw if a sync is currently 
        /// active.
        /// </summary>
        /// <exception cref="Microsoft.Synchronization.ClientServices.IsolatedStorage.SyncActiveException">
        /// Thrown if sync is active when the Save is attempted</exception>
        /// <exception cref="Microsoft.Synchronization.ClientServices.IsolatedStorage.SaveFailedException">
        /// Thrown if there is a modified item changed during sync and a save for that item is attempted.</exception>
        public void SaveChanges()
        {
            ThrowIfDisposed();

            // If the cache is not loaded, this is a no-op
            if (_loaded)
            {             
                if (!_syncActive)
                {
                    using (_saveSyncLock.LockObject())
                    {
                      
                        // Don't allow SaveChanges to execute if there are unhandled conflicts from a previous
                        // save attempt
                        if (_storeConflicts != null && _storeConflicts.Count != 0)
                        {
                            throw new SaveFailedException(_storeConflicts,
                                "Existing store conflicts must be resolved or have items rejected before " +
                                "SaveChanges can be called");
                        }

                        // Determine if there are any items that can't be saved
                        var failures = _cacheData.GetSaveFailures();

                        if (failures.Count != 0)
                        {
                            // Generate store conflicts for the items.
                            _storeConflicts = (from f in failures
                                                select new StoreConflict(this) { 
                                                    ModifiedEntity = f,
                                                    LiveEntity = f.GetOriginal() 
                                                }).ToList();

                            // Make sure the modified entities point to their store conflicts
                            foreach (StoreConflict sc in _storeConflicts)
                            {
                                sc.ModifiedEntity.StoreConflict = sc;
                            }

                            // Throw an exception and let the user know which items are in conflict
                            // They will also be able to be retrieved from the context later.
                            throw new SaveFailedException(_storeConflicts, 
                                "One or more modified items has had an update received from the service. The conflicts must be resolved before SaveChanges can complete successfully.");
                        }
                        else
                        {
                            // Everything is ok, so actually save changes.
                            IEnumerable<IsolatedStorageOfflineEntity> changes = _cacheData.CommitChanges();
                            _storageHandler.SaveChanges(changes);
                        }
                        
                    }

                }
                else // Throw if sync was active when we tried to SaveChanges
                {
                    throw new SyncActiveException("SaveChanges is not permitted while sync is active");
                }
            }
        }

        /// <summary>
        /// Returns any conflicts that ocurred when trying to call SaveChanges.  This collection must
        /// be empty before SaveChanges can be called again.  
        /// 
        /// Applications have 3 ways to retrieve store conflicts:
        ///  1. In the exception that is thrown when they are first detected.
        ///  2. From this collection.
        ///  3. From the entities that have conflicts.
        /// </summary>
        public ReadOnlyCollection<StoreConflict> StoreConflicts 
        { 
            get
            {
                ThrowIfDisposed();
                return new ReadOnlyCollection<StoreConflict>(_storeConflicts);
            }
        }

        /// <summary>
        /// Cancels and rolls back any unsaved changes.  This will block while any other operation is
        /// happening on the context.
        /// </summary>
        public void CancelChanges()
        {
            ThrowIfDisposed();            
          
            if (_loaded)
            {                
                // Lock so nothing changes while adding the entity
                if (!_syncActive)
                {
                    using (_saveSyncLock.LockObject())
                    {                                                
                        _cacheData.Rollback();                       
                    }
                }
                else // Throw if sync was active when we tried to Cancel Changes
                {
                    throw new SyncActiveException("Cancel changes is not permitted while sync is active");
                }
            }
        }

        
        /// <summary>
        /// Removes the requested item.  This entity will not be included for synchronization until SaveChanges is
        /// called.  If the context has not been loaded, this method will load the context.
        /// </summary>
        /// <typeparam name="T">Type of the entity to remove</typeparam>
        /// <param name="entity">The entity to remove</param>
        /// <exception cref="System.InvalidOperationException">Thrown if the context has never been synchronized with
        /// the service</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if the entity is null</exception>
        public void DeleteItem<T>(T entity) where T : IsolatedStorageOfflineEntity
        {
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Call load before doing any work.  This is a no-op if the data is already loaded.
            Load();
        

            // Lock so nothing changes while adding the entity
            if (!_syncActive)
            {
                using (_saveSyncLock.LockObject())
                {
                    // Make sure the context has been synchronized once
                    if (_cacheData.AnchorBlob == null)
                    {
                        throw new InvalidOperationException(
                            "Anchor is null.  Items cannot be deleted before an initial sync has occurred");
                    }

                    // Find the corresponding collection and throw
                    ((IsolatedStorageCollection<T>)_cacheData.Collections[typeof(T)]).DeleteItem(entity);
                 
                }
            }
            else// Throw if sync was active when we tried to Delete Changes
            {
                throw new SyncActiveException("Deleting changes is not permitted while sync is active");
            }

        }

        /// <summary>
        /// Adds the requested item to the collection corresponding to the type passed specified by T.  This entity
        /// will not be included in synchronization until SaveChanges is called.  If the context has not been loaded,
        /// calling this method will load the context.
        /// </summary>
        /// <typeparam name="T">Type of the entity being passed in.  Must be a type specified in the schema.</typeparam>
        /// <param name="entity">Entity to add</param>
        public void AddItem<T>(T entity) where T : IsolatedStorageOfflineEntity
        {
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            // Make sure the context is loaded.
            Load();

            // Lock so nothing changes while adding the entity
            if (!_syncActive)
            {
                using (_saveSyncLock.LockObject())
                {
                    
                    // Make sure sync has happened.
                    if (_cacheData.AnchorBlob == null)
                    {
                        throw new InvalidOperationException(
                            "Anchor is null.  Items cannot be added before an initial sync has occurred");
                    }

                    // Add the item
                    ((IsolatedStorageCollection<T>)_cacheData.Collections[typeof(T)]).AddItem(entity);
                  
                }
            }
            else  // Throw if sync was active when we tried to AddChanges
            {
                throw new SyncActiveException("Adding changes is not permitted while sync is active");
            }
        }

        /// <summary>
        /// Sync conflicts that occurred during sync.  The application is not required to handle these, other than
        /// to clear them to save space.  Calling this method will cause the context to be loaded from disk if it
        /// is not already.
        /// </summary>
        public ReadOnlyCollection<SyncConflict> SyncConflicts 
        {
            get
            {
                ThrowIfDisposed();

                // Load if necessary
                Load();

                return new ReadOnlyCollection<SyncConflict>(_cacheData.SyncConflicts);
            }
        }

        /// <summary>
        /// Sync errors that occurred during sync.  The application is not required to handle these, other than
        /// to clear them to save space.  Calling this method will cause the context to be loaded from disk if it
        /// is not already.
        /// </summary>
        public ReadOnlyCollection<SyncError> SyncErrors 
        { 
            get
            {
                ThrowIfDisposed();

                // Load if necessary
                Load();

                return new ReadOnlyCollection<SyncError>(_cacheData.SyncErrors);
            }
        }

        /// <summary>
        /// Clears sync conflicts from memory and from disk.
        /// </summary>
        public void ClearSyncConflicts()
        {
            ThrowIfDisposed();

            Load();

            using (_saveSyncLock.LockObject())
            {
                _cacheData.ClearSyncConflicts();
                _storageHandler.ClearConflicts();
            }
        }

        /// <summary>
        /// Clears sync errors from memory and from disk.
        /// </summary>
        public void ClearSyncErrors()
        {
            ThrowIfDisposed();

            Load();

            using (_saveSyncLock.LockObject())
            {
                _cacheData.ClearSyncErrors();
                _storageHandler.ClearErrors();
            }
        }

        /// <summary>
        /// Event called during sync when item which has been modified but not saved has
        /// had a new change synchronized for it.
        /// </summary>
        public event Action<IsolatedStorageOfflineEntity> ModifiedItemChanged;

        /// <summary>
        /// Returns the cache path of the context.
        /// </summary>
        public string CachePath 
        { 
            get
            {
                ThrowIfDisposed();
                return _cachePath;
            }
        }

        /// <summary>
        /// Returns the schema used by the context.  Any changes to the schema after instantiation of the context
        /// will be ignored.
        /// </summary>
        public IsolatedStorageSchema Schema 
        { 
            get
            {
                ThrowIfDisposed();
                return _schema;
            }
        }

        /// <summary>
        /// The Uri of the scopeName used for sync.
        /// </summary>
        public Uri ScopeUri 
        { 
            get
            {
                ThrowIfDisposed();
                return _scopeUri;
            }
        }

        /// <summary>
        /// Clears all data from the disk and memory.
        /// </summary>
        public void ClearCache()
        {
            ThrowIfDisposed();

            // Lock this one to ensure that it blocks if the cache is being loaded in another thread.  Also
            // prevents the cache from being loaded while the clear is being performed.
            lock (_loadLock)
            {
                // Lock here to ensure that this method doesn't overload with other method calls, including
                // SaveChanges and synchronization.
                using (_saveSyncLock.LockObject())
                {
                    // If loaded, clear the in-memory data.
                    if (_loaded)
                    {
                        _cacheData.Clear();
                    }

                    // Delete storage internal changes cache and the files.
                    _storageHandler.ClearCache();
                }
            }
        }

        /// <summary>
        /// A preinitialized CacheController which can be used to synchronize the context with the uri specified
        /// in the constructor.
        /// </summary>
        public CacheController CacheController 
        {
            get
            {
                ThrowIfDisposed();
                return _cacheController;
            }
        }

        /// <summary>
        /// Returns the symmetric encryption algorithm specified in the constructor in order to encrypt files on disk.
        /// If this property is null, the data will not be encrypted.
        /// </summary>
        public bool IsEncrypted
        {
            get
            {
                ThrowIfDisposed();
                return _storageHandler.EncryptionAlgorithm != null;
            }
        }

        #region OfflineSyncProvider

        /// <summary>
        /// OfflineSyncProvider method called when the controller is about to start a sync session.
        /// </summary>
        public override void BeginSession()
        {
            ThrowIfDisposed();

            // Don't start a second session if sync is already active.
            if (_syncActive)
            {
                throw new InvalidOperationException("Sync session already active for context");
            }

            //Reset IsFirst Sync. This will be set only when the server blob is null;
            _isFirstSync = false;

            // Load the cache if it is not already loaded.
            Load();

            // Lock everything else out while sync is happening.
            _saveSyncLock.Lock();
            _syncActive = true;
        }

        /// <summary>
        /// OfflineSyncProvider method implementation to return a set of sync changes.
        /// </summary>
        /// <param name="state">A unique identifier for the changes that are uploaded</param>
        /// <returns>The set of incremental changes to send to the service</returns>
        public override ChangeSet GetChangeSet(Guid state)
        {
            ThrowIfDisposed();

            if (!_syncActive)
            {
                throw new InvalidOperationException("GetChangeSet cannot be called without calling BeginSession");
            }

            ChangeSet changeSet = new ChangeSet();

            // Get the changes from the storage layer (not the in-memory data that can change)
            IEnumerable<IsolatedStorageOfflineEntity> changes = _storageHandler.GetChanges(state);

            // Fill the change list.
            changeSet.Data = (from change in changes select (IOfflineEntity)change).ToList();
            changeSet.IsLastBatch = true;
            changeSet.ServerBlob = _cacheData.AnchorBlob;

            return changeSet;
        }

        /// <summary>
        /// OfflineSyncProvider method implementation called when a change set returned from GetChangeSet has been
        /// successfully uploaded.
        /// </summary>
        /// <param name="state">The unique identifier passed in to the GetChangeSet call.</param>
        /// <param name="response">ChangeSetResponse that contains an updated server blob and any conflicts or errors that
        /// happened on the service.</param>
        public override void OnChangeSetUploaded(Guid state, ChangeSetResponse response)
        {
            ThrowIfDisposed();

            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            if (!_syncActive)
            {
                throw new InvalidOperationException("OnChangeSetUploaded cannot be called without calling BeginSession");
            }

            if (response.Error == null)
            {
                IEnumerable<IsolatedStorageOfflineEntity> updatedItems = response.UpdatedItems.Cast<IsolatedStorageOfflineEntity>();
                // Notify the disk management that changes uploaded successfully.
                IEnumerable<Conflict> conflicts = _storageHandler.UploadSucceeded(state, response.ServerBlob, response.Conflicts, updatedItems);

                // Update the in-memory representation.
                _cacheData.AddUploadChanges(response.ServerBlob, conflicts, updatedItems, this);
            }
            else
            {
                _storageHandler.UploadFailed(state);
            }
        }

        /// <summary>
        /// Returns the last server blob that the context received during sync
        /// </summary>
        /// <returns>The server blob.  This will be null if the context has not synchronized with the service</returns>
        public override byte[] GetServerBlob()
        {
            ThrowIfDisposed();

            if (!_syncActive)
            {
                throw new InvalidOperationException("GetServerBlob cannot be called without calling BeginSession");
            }

            byte[] serverBlob = _cacheData.AnchorBlob;
            
            if (serverBlob == null)
                _isFirstSync = true;

            return serverBlob;
        }

        /// <summary>
        /// OfflineSyncProvider method called to save changes retrieved from the sync service.
        /// </summary>
        /// <param name="changeSet">The set of changes from the service to save. Also contains an updated server
        /// blob.</param>
        public override void SaveChangeSet(ChangeSet changeSet)
        {
            ThrowIfDisposed();

            if (changeSet == null)
            {
                throw new ArgumentNullException("changeSet");
            }

            if (!_syncActive)
            {
                throw new InvalidOperationException("SaveChangeSet cannot be called without calling BeginSession");
            }

            if (changeSet.Data.Count == 0 && !_isFirstSync)
            {
                return;
            }
            
            // Cast to the isolated storage-specific entity.
            var entities = changeSet.Data.Cast<IsolatedStorageOfflineEntity>();
            
            // Store the downloaded changes to disk.
            _storageHandler.SaveDownloadedChanges(changeSet.ServerBlob, entities);

            // Update in-memory representation.
            _cacheData.DownloadedChanges(changeSet.ServerBlob, entities);
        }

        /// <summary>
        /// OfflineSyncProvider method called when sync is completed.  This method will unlock so that SaveChanges
        /// and other operations can be called.
        /// </summary>
        public override void EndSession()
        {
            ThrowIfDisposed();

            // If sync is not active, throw.  The context doesn't need to worry about exiting the lock if this throws
            // because it can only be set to false outside of the lock.
            if (!_syncActive)
            {
                throw new InvalidOperationException("Sync session not active for context");
            }

            //If this is first sync then call notfication as a reset instead of Add for every item.
            if (_isFirstSync)
            {
                _cacheData.NotifyAllCollections();
            }

            // Sync is no longer active
            _syncActive = false;

            // Unlock.
            _saveSyncLock.Unlock();
        }

        #endregion

#if SLUNITTEST
        #region Testability

        /// <summary>
        /// Testing-only method to enable control over archive.  Otherwise, unit tests must wait for the timer
        /// interval for archive to occur.
        /// </summary>
        /// <param name="interval">The desired interval for the archive timer</param>
        public void SetArchiveTimerInterval(long interval)
        {
            _storageHandler.SetArchiveInterval(interval);
        }

        /// <summary>
        /// Testing-only method to enable control over archive.  Otherwise, unit tests must wait for the timer
        /// interval for archive to occur.
        /// </summary>
        /// <param name="interval">The desired interval for the archive timer</param>
        public void SetArchiveTimerInterval(TimeSpan interval)
        {
            _storageHandler.SetArchiveInterval(interval);
        }
        
        #endregion
#endif

        #region IDisposable

        /// <summary>
        /// Closes the context and releases the lock on the cache path
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Dispose the context and releases the lock on the cache path
        /// </summary>
        public void Dispose()
        {
            // This is the standard dispose pattern
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose internal references
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_storageHandler != null)
                    {
                        _storageHandler.Dispose();
                        _storageHandler = null;
                    }

                    if (_saveSyncLock != null)
                    {
                        _saveSyncLock.Dispose();
                        _saveSyncLock = null;
                    }
                }

                _isDisposed = true;
            }
        }


        /// <summary>
        /// Method which checks whether or not the object is disposed and throws if it is
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("Cannot access a disposed IsolatedStorageOfflineContext");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the cache controller to sync with the context.
        /// </summary>
        private void CreateCacheController()
        {
            _cacheController = new CacheController(_scopeUri, _scopeName, this);

            CacheControllerBehavior behavior = _cacheController.ControllerBehavior;

            // Because the AddType is AddType<T>, need to create a generic method
            // for each type in the schema.  This will only be done once, so it's
            // not too expensive.
            Type behaviorType = behavior.GetType();
            MethodInfo addType = behaviorType.GetMethod("AddType");

            // Loop over each collection in the schema.
            foreach (Type t in _schema.Collections)
            {
                // Create the generic method for the type.
                MethodInfo addType_T = addType.MakeGenericMethod(t);

                // Invoke the created method.
                addType_T.Invoke(behavior, new object[] { });
            }
        }

        /// <summary>
        /// Thread method used when LoadAsync is called.
        /// </summary>
        /// <param name="state">Required by ThreadPool, but not used</param>
        private void LoadThread(object state)
        {
            Exception exception = null;

            try
            {
                // Double-checking lock to ensure that loading is actually necesary.
                if (!_loaded)
                {
                    lock (_loadLock)
                    {
                        if (!_loaded)
                        {
                            LoadInternal();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }

                // Catch the exception and store it.
                exception = e;
            }

            // Pass the event args (including the exception to the callback).
            EventHandler<LoadCompletedEventArgs> loadCompleted = LoadCompleted;
            if (loadCompleted != null)
            {
                loadCompleted(this, new LoadCompletedEventArgs(exception));
            }
        }

        /// <summary>
        /// Loads the data from the cache without doing any locking.
        /// </summary>
        private void LoadInternal()
        {
            // Verify the schema and uri match was was previously used for the cache path.
            CheckSchemaAndUri(_cachePath, _schema, _scopeUri, _scopeName, StorageHandler.EncryptionAlgorithm);

            // Load the data.
            _cacheData = _storageHandler.Load(this);

            foreach (IsolatedStorageCollection collection in _cacheData.Collections.Values)
            {
                collection.ModifiedItemChanged += new Action<IsolatedStorageOfflineEntity>(CollectionModifiedEntityChanged);
            }

            _loaded = true;
        }

        /// <summary>
        /// Method that verifies a previously cached schema and uri (if they exist) with the current schema and uri.
        /// </summary>
        /// <param name="cachePath">Cache path for the context</param>
        /// <param name="schema">Schema to verify</param>
        /// <param name="uri">Uri to verify</param>
        /// <param name="scopeName">The scope name that the client will be accessing on the service</param>
        /// <param name="encryptionAlgorithm">The encryption algorithm which will be used to verify the schema</param>
        private void CheckSchemaAndUri(string cachePath, IsolatedStorageSchema schema, Uri uri, string scopeName, SymmetricAlgorithm encryptionAlgorithm)
        {
            // Get the isolated storage file for the application.
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Generate the path to the scopeName info file.
                string infoPath = Path.Combine(cachePath, Constants.SCOPE_INFO);

                // If the file exists, read it, otherwise, everything is fine.
                if (isoFile.FileExists(infoPath))
                {
                    // Open the scopeName file.
                    using (IsolatedStorageFileStream stream = isoFile.OpenFile(infoPath, FileMode.Open))
                    {
                        Stream readStream = stream;
                        ICryptoTransform decryptor = null;

                        try
                        {
                            if (encryptionAlgorithm != null)
                            {
                                decryptor = encryptionAlgorithm.CreateDecryptor();
                                readStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
                            }

                            List<string> fileTypes;
                            string fileUri;
                            string fileScopeName;

                            // Read the file types and uri from the file.
                            ReadSchemaAndUri(readStream, out fileUri, out fileScopeName, out fileTypes);

                            // Verify the scopeName uri.
                            if (fileUri != uri.AbsoluteUri)
                            {
                                throw new ArgumentException("Specified uri does not match uri previously used for the specified cache path");
                            }

                            if (fileScopeName != scopeName)
                            {
                                throw new ArgumentException("Specified scope name does not match scope name previously used for the specified cache path");
                            }

                            // Verify the types.
                            List<Type> userTypes = schema.Collections.ToList();

                            // Sort by name (the class Type isn't sortable)
                            userTypes.Sort((x, y) =>
                            {
                                return x.FullName.CompareTo(y.FullName);
                            });

                            if (userTypes.Count != fileTypes.Count)
                            {
                                throw new ArgumentException("Specified schema does not match schema previously used for cache path");
                            }

                            for (int i = 0; i < userTypes.Count; ++i)
                            {
                                if (userTypes[i].FullName != fileTypes[i])
                                {
                                    throw new ArgumentException("Specified schema does not match schema previously used for cache path");
                                }
                            }
                        }
                        finally
                        {
                            readStream.Dispose();

                            if (decryptor != null)
                            {
                                decryptor.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    // If the file doesn't exist, write the new info.
                    using (IsolatedStorageFileStream stream = isoFile.CreateFile(infoPath))
                    {
                        Stream writeStream = stream;
                        ICryptoTransform encryptor = null;

                        try
                        {
                            if (encryptionAlgorithm != null)
                            {
                                encryptor = encryptionAlgorithm.CreateEncryptor();
                                writeStream = new CryptoStream(writeStream, encryptor, CryptoStreamMode.Write);
                            }

                            WriteSchemaFile(writeStream, uri, scopeName, schema);

                        }
                        finally
                        {
                            writeStream.Dispose();

                            if (encryptor != null)
                            {
                                encryptor.Dispose();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reads the cached schema and uri information from the stream. Everything is stored in text, so just use a stream reader.
        /// </summary>
        /// <param name="stream">Stream to read from</param>
        /// <param name="uri">Uri for the scopeName</param>
        /// <param name="scopeName">Scope Name</param>
        /// <param name="types">List of types returned from the file</param>
        private void ReadSchemaAndUri(Stream stream, 
            out string uri, 
            out string scopeName,
            out List<string> types)
        {
            string schemaUri;
            string schemaScopeName;
            List<string> schemaTypes = new List<string>();

            // Create the stream reader.
            using (StreamReader reader = new StreamReader(stream))
            {
                // First line of the file is the uri
                schemaUri = reader.ReadLine();
                schemaScopeName = reader.ReadLine();

                // Rest of the file are the types (Full type names including namespaces).
                while (!reader.EndOfStream)
                {
                    schemaTypes.Add(reader.ReadLine());
                }
            }

            // Fill the output parameters.
            types = schemaTypes;
            uri = schemaUri;
            scopeName = schemaScopeName;
        }

        /// <summary>
        /// Writes the file with schema information.
        /// </summary>
        /// <param name="stream">Stream to which to write</param>
        /// <param name="uri">Scope uri to be written</param>
        /// <param name="scopeName">Scope Name</param>
        /// <param name="schema">Schema to be written</param>
        private void WriteSchemaFile(Stream stream, 
            Uri uri, 
            string scopeName, 
            IsolatedStorageSchema schema)
        {
            // Write data as text, so create the stream reader.
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Write the text version of the Uri.
                writer.WriteLine(uri.AbsoluteUri);
                writer.WriteLine(scopeName);

                // Get the list of types as strings and sort to make comparison
                // faster when reading.
                List<string> types = (from type in schema.Collections
                                     select type.FullName).ToList();
                types.Sort();

                // Write the types.
                foreach (string type in types)
                {
                    writer.WriteLine(type);
                }
            }
        }

        /// <summary>
        /// Internal method called by the StoreConflict class in order to resolve a store conflict.  This must be done
        /// because there must be some maintenance of the in-memory collections depending on the resolution of the conflict
        /// </summary>
        /// <param name="conflict">Conflict to resolve</param>
        /// <param name="resolutionAction">Resolution action.</param>
        internal void ResolveStoreConflict(StoreConflict conflict, StoreConflictResolutionAction resolutionAction)
        {
            using (_saveSyncLock.LockObject())
            {
                ResolveStoreConflictNoLock(conflict, resolutionAction);
            }
        }

        /// <summary>
        /// Internal method called by the StoreConflict class during RejectChangesInternal to resolve a store conflict
        /// without locking the the save sync lock.  This ultimately filters down through CancelChanges, which locks,
        /// so this method does not need to.  When it does lock, it causes a deadlock.
        /// </summary>
        /// <param name="conflict"></param>
        /// <param name="resolutionAction"></param>
        internal void ResolveStoreConflictNoLock(StoreConflict conflict, StoreConflictResolutionAction resolutionAction)
        {
            // Cache the modified entity, which may disappear depending on the resolution
            IsolatedStorageOfflineEntity visibleEntity = conflict.ModifiedEntity;

            // Respond to the resolution
            if (resolutionAction == StoreConflictResolutionAction.AcceptModifiedEntity)
            {
                ((IsolatedStorageOfflineEntity)conflict.ModifiedEntity).UpdateModifiedTickCount();
            }
            else if (resolutionAction == StoreConflictResolutionAction.AcceptStoreEntity)
            {
                _cacheData.ResolveStoreConflictByRollback(conflict.ModifiedEntity);
            }
            else
            {
                throw new ArgumentException("Invalid resolution action specified");
            }

            // Cleanup pointers to conflicts everywhere.
            visibleEntity.StoreConflict = null;
            this._storeConflicts.Remove(conflict);

            // Clearing the context will prevent the resolution from being triggered again.
            conflict.ClearContext();
        }

        /// <summary>
        /// Called by SyncErrorInfo from the ClearSyncConflict method.  Used to remove the conflict
        /// from the collection and from the disk
        /// </summary>
        /// <param name="conflict">conflict to clear</param>
        internal void ClearSyncConflict(SyncConflict conflict)
        {
            using (_saveSyncLock.LockObject())
            {
                _cacheData.RemoveSyncConflict(conflict);
                _storageHandler.ClearSyncConflict((IsolatedStorageSyncConflict)conflict);
            }
        }

        /// <summary>
        /// Called by SyncErrorInfo from the ClearSyncError method.  Used to remove the error
        /// from the collection and from the disk
        /// </summary>
        ///<param name="error">SyncError</param>
        internal void ClearSyncError(SyncError error)
        {
            using (_saveSyncLock.LockObject())
            {
                _cacheData.RemoveSyncError(error);
                _storageHandler.ClearSyncError((IsolatedStorageSyncError)error);
            }
        }

        private void CollectionModifiedEntityChanged(IsolatedStorageOfflineEntity entity)
        {
            Action<IsolatedStorageOfflineEntity> action = ModifiedItemChanged;

            if (action != null)
            {
                action(entity);
            }
        }

        internal StorageHandler StorageHandler
        {
            get
            {
                return _storageHandler;
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// This member stores the in-memory data for the context.  It is returned by the
        /// _storageHandler.Load method when Load is executed
        /// </summary>
        private CacheData _cacheData = null;

        /// <summary>
        /// This member manages the disk access for the context. It is instantiated in the
        /// constructor.
        /// </summary>
        private StorageHandler _storageHandler = null;

        /// <summary>
        /// Specifies whether or not the context is loaded.  It is set when the context has been
        /// successfully loaded. It is guared by the _loadLock
        /// </summary>
        private volatile bool _loaded = false;

        // Note: When these locks are nested, _loadLock should always be used before _saveSyncLock
        // to avoid deadlocks.

        /// <summary>
        /// This lock guards the _loaded variable.  It is used to cause multiple threads attempting
        /// to load to block.
        /// </summary>
        private object _loadLock = new object();

        /// <summary>
        /// Essentially guards the _cacheData object.  Prevents multiple accesses that result in
        /// modification of the _cacheData object.  Also used to prevent save during sync.
        /// </summary>
        private AutoResetLock _saveSyncLock;

        /// <summary>
        /// The scope uri for the context.  Passed in to the constructor.
        /// </summary>
        private Uri _scopeUri;

        /// <summary>
        /// The scope name for the context. Passed in to the constructor.
        /// </summary>
        private string _scopeName;

        /// <summary>
        /// The cache path for the context. Passed in to the constructor.
        /// </summary>
        private string _cachePath;

        /// <summary>
        /// Cache controller generated as a convenience for the user.  Created
        /// in the constructor.
        /// </summary>
        private CacheController _cacheController;

        /// <summary>
        /// Schema passed in to the constructor.  Passed to the storage handler
        /// so that the appropriate collections can be created.
        /// </summary>
        private IsolatedStorageSchema _schema;

        /// <summary>
        /// List of store conflicts. Created during the SaveChanges call, and
        /// cleaned up as the conflicts are resolved or the changes are cancelled.
        /// </summary>
        private IList<StoreConflict> _storeConflicts = new List<StoreConflict>();

        /// <summary>
        /// Specifies that sync is active.  It is set to true in BeginSession and 
        /// set to false in EndSession.
        /// </summary>
        private volatile bool _syncActive = false;

        /// <summary>
        /// Specifies that the context has been disposed.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// Used to detect if this is the first sync to the server.
        /// This is used to send notification to the databinder only once
        /// instead of each item.
        /// </summary>
        bool _isFirstSync = false;
        
        #endregion
    }

}
