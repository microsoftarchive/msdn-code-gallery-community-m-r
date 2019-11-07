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
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Threading;
using System.Text;
using System.Security.Cryptography;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// This is an internal class which manages all reads and writes from disk.  When sync attempts to send changes
    /// this method will supply the changes to send, rather than using the in-memory copies.
    /// </summary>
    internal class StorageHandler : IDisposable
    {
        /// <summary>
        /// Constructor which initializes the handler given the schema and the cache path
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="cachePath"></param>
        /// <param name="encryptionAlgorithm"></param>
        public StorageHandler(IsolatedStorageSchema schema, string cachePath, SymmetricAlgorithm encryptionAlgorithm)
        {
            OpenLockFile(cachePath);

            _isDisposed = false;

            this._schema = schema;
            this._cachePath = cachePath;
            this._anchor = null;
            this._changes = new Dictionary<OfflineEntityKey, IsolatedStorageOfflineEntity>();
            this._encryptionAlgorithm = encryptionAlgorithm;

            _knownTypes = new List<Type>()
            {
                typeof (SyncConflict),
                typeof (SyncError)
            };

            AddKnownTypes();
        }

        /// <summary>
        /// Loads the files from disk and returns all of the data
        /// </summary>
        /// <returns>Data read from disk.</returns>
        public CacheData Load(IsolatedStorageOfflineContext context)
        {
            CacheData cacheData = null;
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                // Get the encryptor and decryptor
                if (_encryptionAlgorithm != null)
                {
                    _encryptor = _encryptionAlgorithm.CreateEncryptor();
                    _decryptor = _encryptionAlgorithm.CreateDecryptor();
                }

                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!isoFile.DirectoryExists(_cachePath))
                    {
                        isoFile.CreateDirectory(_cachePath);
                    }

                    cacheData = new CacheData(_schema);
                    
                    ReadFiles(cacheData, context);
                }

                _anchor = cacheData.AnchorBlob;

                // Start the cleanup timer
                _cleanupTimer = new Timer(CleanupTimerCallback, null, _timerInterval, _timerInterval);
            }

            return cacheData;
        }

        /// <summary>
        /// Receives the changes passed in and stores a save file for them.  It will also keep the changes
        /// in memory for fast access during sync.
        /// </summary>
        /// <param name="changes"></param>
        public void SaveChanges(IEnumerable<IsolatedStorageOfflineEntity> changes)
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                // Add changes to list
               AddChanges(changes, false);

                string fileName = GetFileName(CacheFileType.SaveChanges);

                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                using (Stream fileStream = OpenWriteFile(isoFile, fileName))
                using (Stream writeStream = OpenWriteCryptoStream(fileStream))
                {
                    var serializer = GetSerializer(typeof(IEnumerable<IsolatedStorageOfflineEntity>));
                    serializer.WriteObject(writeStream, changes);

                    CheckAndRefreshEncryptor();
                }

                _filesSinceArchive++;
            }
        }

        /// <summary>
        /// Returns the set of changes cached in memory and sets them aside with the specified state.
        /// </summary>
        /// <param name="state">Unique identifier for the set of changes</param>
        /// <returns>The set of changes.</returns>
        public IEnumerable<IsolatedStorageOfflineEntity> GetChanges(Guid state)
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                IEnumerable<IsolatedStorageOfflineEntity> changes = _changes.Values;
                if (_sentChangesAwaitingResponse == null)
                {
                    _sentChangesAwaitingResponse = new Dictionary<Guid, IEnumerable<IsolatedStorageOfflineEntity>>();
                }

                _sentChangesAwaitingResponse[state] = changes;

                _changes = new Dictionary<OfflineEntityKey,IsolatedStorageOfflineEntity>();
                return changes;
            }
        }

        /// <summary>
        /// If the upload fails, this method is called to return the changes corresponding to the state to the overall
        /// list of changes.
        /// </summary>
        /// <param name="state">State for which the send failed.</param>
        public void UploadFailed(Guid state)
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                IEnumerable<IsolatedStorageOfflineEntity> changes = _sentChangesAwaitingResponse[state];
                _sentChangesAwaitingResponse.Remove(state);

                AddChanges(changes, true);
            }
        }

        /// <summary>
        /// Called when the changes returned above were successfully uploaded.  This method will write an updated file
        /// for the changes and then drop the in-memory copies.
        /// </summary>
        /// <param name="state">State for which the upload succeeded</param>
        /// <param name="anchor">New anchor to store</param>
        /// <param name="conflicts">Conflicts which were resolved on upload.</param>
        /// <param name="entities">Entities for which an OData property was updated</param>
        public IEnumerable<Conflict> UploadSucceeded(Guid state, byte[] anchor, IEnumerable<Conflict> conflicts, IEnumerable<IsolatedStorageOfflineEntity> entities)
        {
            List<Conflict> returnConflicts = new List<Conflict>();

            lock (_syncRoot)
            {
                // Don't need the sent changes anymore
                _sentChangesAwaitingResponse.Remove(state);

                string fileName = GetFileName(CacheFileType.UploadResponse);

                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    ResponseData responseData = new ResponseData();
                    responseData.Anchor = anchor;

                    // This approach assumes that there are not duplicates between the conflicts and the updated entities (there shouldn't be)
                    responseData.Entities = (from c in conflicts
                                            select (IsolatedStorageOfflineEntity)c.LiveEntity).Concat(entities);

                    using (Stream fileStream = OpenWriteFile(isoFile, fileName))
                    using (Stream writeStream = OpenWriteCryptoStream(fileStream))
                    {
                        var serializer = GetSerializer(typeof(ResponseData));

                        serializer.WriteObject(writeStream, responseData);

                        CheckAndRefreshEncryptor();
                    }

                    foreach (Conflict conflict in conflicts)
                    {
                        returnConflicts.Add(WriteConflictFile(isoFile, conflict));
                    }
                }

                _filesSinceArchive++;
            }

            return returnConflicts;
        }


        /// <summary>
        /// Saves the changes received when downloading to disk.
        /// </summary>
        /// <param name="anchor">New anchor for the store</param>
        /// <param name="entities">The received entities.</param>
        public void SaveDownloadedChanges(byte [] anchor, IEnumerable<IsolatedStorageOfflineEntity> entities)
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                ResponseData downloadData = new ResponseData();
                downloadData.Anchor = anchor;
                downloadData.Entities = entities;
                string fileName = GetFileName(CacheFileType.DownloadResponse);

                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                using (Stream fileStream = OpenWriteFile(isoFile, fileName))
                using (Stream writeStream = OpenWriteCryptoStream(fileStream))
                {
                    var serializer = GetSerializer(typeof(ResponseData));

                    serializer.WriteObject(writeStream, downloadData);

                    CheckAndRefreshEncryptor();
                }

                _filesSinceArchive++;
            }
        }

        public void ClearSyncConflict(IsolatedStorageSyncConflict conflict)
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    DeleteFile(isoFile, conflict.FileName);
                }
            }
        }

        public void ClearSyncError(IsolatedStorageSyncError error)
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();

                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    DeleteFile(isoFile, error.FileName);
                }
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (_archiveLock)
            {
                lock (_syncRoot)
                {
                    if (!_isDisposed)
                    {
                        if (disposing)
                        {
                            _lockFile.Dispose();
                            _lockFile = null;

                            if (_cleanupTimer != null)
                            {
                                _cleanupTimer.Dispose();
                                _cleanupTimer = null;
                            }

                            if (_encryptionAlgorithm != null &&
                                _encryptionAlgorithm is IDisposable)
                            {
                                ((IDisposable)_encryptionAlgorithm).Dispose();
                                _encryptionAlgorithm = null;
                            }

                            if (_encryptor != null &&
                                _encryptor is IDisposable)
                            {
                                ((IDisposable)_encryptor).Dispose();
                                _encryptor = null;
                            }

                            if (_decryptor != null &&
                                _decryptor is IDisposable)
                            {
                                ((IDisposable)_decryptor).Dispose();
                                _decryptor = null;
                            }
                        }

                        _isDisposed = true;
                    }
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("Cannot access disposed StorageHandler");
            }
        }

        #endregion

        /// <summary>
        /// Adds the types from the collection to the list of known types for serialization
        /// </summary>
        private void AddKnownTypes()
        {
            foreach (Type t in _schema.Collections)
            {
                _knownTypes.Add(t);
            }
        }

        /// <summary>
        /// Reads through the files on the cache path and files the cache data.
        /// </summary>
        /// <param name="cacheData">Object which will be filled with data from disk.</param>
        /// <param name="context">IsolatedStorageOfflineContext</param>
        private void ReadFiles(CacheData cacheData, IsolatedStorageOfflineContext context)
        {
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var files = isoFile.GetFileNames(_cachePath + "/*");
                Array.Sort(files);

                bool exceptionCaught = false;

                List<FileInfo> conflictFiles = new List<FileInfo>();

                foreach (string file in files)
                {
                    try
                    {
                        if (!Constants.SpecialFile(file) && Constants.IsCacheFile(file))
                        {
                            if (exceptionCaught)
                            {
                                DeleteFile(isoFile, file);
                            }
                            else
                            {
                                CacheFileType fileType = GetFileType(file);
                                _fileCount = GetFileCount(file) + 1;

                                switch (fileType)
                                {
                                    case CacheFileType.DownloadResponse:
                                        ReadDownloadResponseFile(file, cacheData);
                                        _filesSinceArchive++;
                                        break;

                                    case CacheFileType.SaveChanges:
                                        ReadSaveChangesFile(file, cacheData);
                                        _filesSinceArchive++;
                                        break;

                                    case CacheFileType.UploadResponse:
                                        ReadUploadResponseFile(file, cacheData);
                                        _filesSinceArchive++;
                                        break;

                                    case CacheFileType.Conflicts:
                                    case CacheFileType.Errors:

                                        conflictFiles.Add(new FileInfo()
                                        {
                                            FileName = file,
                                            FileType = fileType
                                        });

                                        break;

                                    case CacheFileType.Archive:
                                        ReadArchiveFile(file, cacheData, context);
                                        _filesSinceArchive = 0;
                                        break;

                                    default:
                                        // skip the file
                                        break;
                                }
                            }
                        }
                    }
                    catch (SerializationException)
                    {
                        // if there's a serialization exception set a flag to remove the subsequent files
                        exceptionCaught = true;

                        DeleteFile(isoFile, file);

                    }
                    catch (IsolatedStorageException)
                    {
                        // this can happen for a variety of reasons.  The
                        exceptionCaught = true;
                    }
                }

                foreach (FileInfo fi in conflictFiles)
                {
                    try
                    {
                        int fileCount = GetFileCount(fi.FileName);

                        if (exceptionCaught && fileCount > _fileCount)
                        {
                            DeleteFile(isoFile, fi.FileName);
                        }
                        else
                        {
                            if (fi.FileType == CacheFileType.Conflicts)
                            {
                                ReadConflictFile(fi.FileName, cacheData, context);
                            }
                            else if (fi.FileType == CacheFileType.Errors)
                            {
                                ReadErrorFile(fi.FileName, cacheData, context);
                            }
                        }
                    }
                    catch (SerializationException)
                    {
                        // Drop this exception...if reading a conflict fails, it's not the worst thing.
                    }
                    catch (IsolatedStorageException)
                    {
                        // Drop this exception...this will likely happen if a file can't be deleted.
                    }
                }
            }
        }

        /// <summary>
        /// Returns the type of the file based on the file name.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns>Type of data stored in the file</returns>
        private CacheFileType GetFileType(string fileName)
        {
            int index = fileName.LastIndexOf('.');

            CacheFileType fileType = CacheFileType.Unknown;

            if (index == fileName.Length - 2)
            {
                switch (fileName[index + 1])
                {
                    case 'D':
                        fileType = CacheFileType.DownloadResponse;
                        break;

                    case 'S':
                        fileType = CacheFileType.SaveChanges;
                        break;

                    case 'U':
                        fileType = CacheFileType.UploadResponse;
                        break;

                    case 'A':
                        fileType = CacheFileType.Archive;
                        break;

                    case 'C':
                        fileType = CacheFileType.Conflicts;
                        break;

                    case 'E':
                        fileType = CacheFileType.Errors;
                        break;

                    default:
                        fileType = CacheFileType.Unknown;
                        break;
                }
            }

            return fileType;
        }

        /// <summary>
        /// Returns the count of the file.  This is used for storing files in order.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>File count</returns>
        /// <remarks>
        /// Throughout the cache, we assume that file names are of the form>
        /// (File Count In Hex)[.Intermediate].(File Type), where the part within [] is
        /// optional.
        /// </remarks>
        private int GetFileCount(string fileName)
        {
            int index = fileName.IndexOf('.');
            string tickCount = fileName.Substring(0, index);

            return Int32.Parse(tickCount, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Creates a new file name based on the current file count and the specified type.
        /// </summary>
        /// <param name="fileType">type for which to generate a file name</param>
        /// <returns>File name.</returns>
        private string GetFileName(CacheFileType fileType)
        {
            return GetFileName(fileType, null);
        }

        /// <summary>
        /// Creates a new file name based on the current file count and the specified type.
        /// </summary>
        /// <param name="fileType">type for which to generate a file name</param>
        /// <param name="fileName">File name to inject to the file</param>
        /// <returns>File name.</returns>
        private string GetFileName(CacheFileType fileType, string fileName)
        {
            return GetFileName(fileType, fileName, GetNextFileCount());
        }

        private string GetFileName(CacheFileType fileType, string fileName, int tickCount)
        {
            string suffix = "";
            switch (fileType)
            {
                case CacheFileType.DownloadResponse:
                    suffix = "D";
                    break;

                case CacheFileType.SaveChanges:
                    suffix = "S";
                    break;

                case CacheFileType.UploadResponse:
                    suffix = "U";
                    break;

                case CacheFileType.Conflicts:
                    suffix = "C";
                    break;

                case CacheFileType.Errors:
                    suffix = "E";
                    break;

                case CacheFileType.Archive:
                    suffix = "A";
                    break;

                default:
                    // users should never see this exception
                    throw new InvalidOperationException("Unexpected value for file type");
            }

            string format = "{0:X8}.{1}";

            if (fileName != null)
            {
                format = "{0:X8}.{2}.{1}";
            }

            return Path.Combine(_cachePath, string.Format(format, tickCount, suffix, fileName));
        }

        /// <summary>
        /// Instantiates the serializer.  This method is used so that serializers can be changed easily.
        /// </summary>
        /// <param name="type">Base type for the serializer</param>
        /// <returns>The serializer</returns>
        private DataContractJsonSerializer GetSerializer(Type type)
        {
            return new DataContractJsonSerializer(type, _knownTypes);
        }

        private void ReadDownloadResponseFile(string fileName, CacheData cacheData)
        {
            ResponseData downloadResponse = ReadFile<ResponseData>(fileName);

            _anchor = downloadResponse.Anchor;
            cacheData.AddSerializedDownloadResponse(downloadResponse.Anchor, downloadResponse.Entities);
        }

        private void ReadSaveChangesFile(string fileName, CacheData cacheData)
        {
            IsolatedStorageOfflineEntity[] entities = ReadFile<IsolatedStorageOfflineEntity[]>(fileName);
            if (entities != null)
            {
                cacheData.AddSerializedLocalChanges(entities);
               AddChanges(entities, false);
            }
        }

        private void ReadUploadResponseFile(string fileName, CacheData cacheData)
        {
            ResponseData uploadResponse = ReadFile<ResponseData>(fileName);

            _anchor = uploadResponse.Anchor;
            cacheData.AddSerializedUploadResponse(uploadResponse.Anchor, uploadResponse.Entities);
            _changes.Clear();
        }

        private void ReadConflictFile(string fileName, CacheData cacheData, IsolatedStorageOfflineContext context)
        {
            SyncConflict conflict = ReadFile<SyncConflict>(fileName);
            IsolatedStorageSyncConflict syncConflict = new IsolatedStorageSyncConflict(conflict)
            {
                FileName = fileName
            };

            cacheData.AddSerializedConflict(syncConflict, context);
        }

        private void ReadErrorFile(string fileName, CacheData cacheData, IsolatedStorageOfflineContext context)
        {
            SyncError error = ReadFile<SyncError>(fileName);
            IsolatedStorageSyncError syncError = new IsolatedStorageSyncError(error)
            {
                FileName = fileName
            };

            cacheData.AddSerializedError(syncError, context);
        }

        private T ReadFile<T>(string fileName)
        {
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                return ReadFile<T>(fileName, isoFile);
            }
        }

        private T ReadFile<T>(string fileName, IsolatedStorageFile isoFile)
        {
            T t = default(T);

            using (Stream fileStream = OpenReadFile(isoFile, Path.Combine(_cachePath, fileName)))
            using (Stream readStream = OpenReadCryptoStream(fileStream))
            {
                t = ReadObject<T>(readStream);

                CheckAndRefreshDecryptor();
            }

            return t;
        }

        private T ReadObject<T>(Stream stream)
        {
            var serializer = GetSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        private Conflict WriteConflictFile(IsolatedStorageFile isoFile, Conflict conflict)
        {
            Conflict returnConflict;

            OfflineEntityKey key = (OfflineEntityKey)((IsolatedStorageOfflineEntity)conflict.LiveEntity).GetIdentity();
            // Use the type name so it's included in the hash code.
            key.TypeName = conflict.LiveEntity.GetType().FullName;
            

            string fileName;
            if (conflict is SyncConflict)
            {
                fileName = GetFileName(CacheFileType.Conflicts, string.Format("{0}", key.GetHashCode()));

                returnConflict = new IsolatedStorageSyncConflict((SyncConflict)conflict)
                {
                    FileName = fileName
                };
            }
            else if (conflict is SyncError)
            {
                fileName = GetFileName(CacheFileType.Errors, string.Format("{0}", key.GetHashCode()));
                returnConflict = new IsolatedStorageSyncError((SyncError)conflict)
                {
                    FileName = fileName
                };

            }
            else
            {
                // This should never happen, but we need to keep the compiler happy.
                throw new ArgumentException("Unknown conflict type: " + conflict.GetType().FullName);
            }

            using (Stream fileStream = OpenWriteFile(isoFile, fileName))
            using (Stream writeStream = OpenWriteCryptoStream(fileStream))
            {
                WriteObject(writeStream, conflict);

                CheckAndRefreshEncryptor();
            }

            return returnConflict;
        }

        private void WriteObject(Stream stream, object t)
        {
            var serializer = GetSerializer(t.GetType());

            serializer.WriteObject(stream, t);
        }

        /// <summary>
        /// Deletes all conflict files
        /// </summary>
        public void ClearConflicts()
        {
            DeleteFiles("*.C");
        }

        /// <summary>
        /// Deletes all error files
        /// </summary>
        public void ClearErrors()
        {
            DeleteFiles("*.E");
        }

        /// <summary>
        /// Deletes all files
        /// </summary>
        public void ClearCacheFiles()
        {
            _anchor = null;

            DeleteFiles("*");
        }

        /// <summary>
        /// Clears internal changes cache and also deletes files
        /// </summary>
        public void ClearCache()
        {
            _changes.Clear();
            ClearCacheFiles();
        }

        public SymmetricAlgorithm EncryptionAlgorithm
        {
            get
            {
                return _encryptionAlgorithm;
            }
        }

        /// <summary>
        /// Deletes the files that match the specified search pattern.
        /// </summary>
        /// <param name="searchPattern">Search pattern for which to delete files.</param>
        private void DeleteFiles(string searchPattern)
        {
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string[] files = isoFile.GetFileNames(Path.Combine(_cachePath, searchPattern));

                foreach (string file in files)
                {
                    if (file != Constants.LOCKFILE)
                    {
                        DeleteFile(isoFile, file);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the specified file
        /// </summary>
        private void DeleteFile(IsolatedStorageFile isoFile, string fileName)
        {
            int failCount = 0;
            bool retry = false;

            do
            {
                retry = false;
                try
                {
                    isoFile.DeleteFile(Path.Combine(_cachePath, fileName));
                }
                catch (IsolatedStorageException)
                {
                    // Catch the isolated storage exception and retry once (according to MSDN,
                    // applications should catch the exception and retry.  See the Remarks section
                    // at: http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragefile.deletefile(VS.95).aspx
                    failCount++;

                    if (failCount <= 1)
                    {
                        retry = true;
                    }
                }
            } while (retry);
        }

        private void OpenLockFile(string cachePath)
        {
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isoFile.DirectoryExists(cachePath))
                {
                    isoFile.CreateDirectory(cachePath);
                }

                string lockFileName = Path.Combine(cachePath, Constants.LOCKFILE);

                try
                {
                    _lockFile = isoFile.OpenFile(lockFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IsolatedStorageException)
                {
                    throw new InvalidOperationException("Another context is open with the same cache path");
                }
            }
        }

        private int GetNextFileCount()
        {
            return Interlocked.Increment(ref _fileCount);
        }

        /// <summary>
        /// Add the offline entity to the changes dictionary
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="ignoreDuplicates">duplicates are ignored only when the uploaded item has failed, because the item in the _changes 
        /// collection might be newer.</param>
        private void AddChanges(IEnumerable<IsolatedStorageOfflineEntity> changes, bool ignoreDuplicates)
        {
            foreach (IsolatedStorageOfflineEntity entity in changes)
            {
                OfflineEntityKey key = (OfflineEntityKey)entity.GetIdentity();
                key.TypeName = entity.GetType().FullName;

                if (!_changes.ContainsKey(key))
                {
                    _changes.Add(key, entity);
                }
                else if (!ignoreDuplicates)
                {
                    _changes[key] = entity;
                }
            }
        }
        

        #region Cache Coalesce code

        private void CleanupTimerCallback(object state)
        {
            lock (_archiveLock)
            {
                int tickCount;

                int originalFilesSyncArchive = 0;

                // The point of lock here is to let any other write operation clear out
                // once that is done, we'll have the tick count and will only be dealing
                // with previously written files, so we don't need the lock anymore, and
                // we want to allow other operations to continue.
                // Releasing the lock as soon as we get the filesSinceArchive is Ok, 
                // since the other operations are not dependant on the Archive file to get written.
                // Anyways if another archive thread kicks in it will be blocked by the _archiveLock.
                lock (_syncRoot)
                {
                    // Make sure enough files were written so we don't just keep copying
                    // archive files
                    if (_filesSinceArchive < ARCHIVE_FILE_THRESHOLD)
                    {
                        return;
                    }

                    originalFilesSyncArchive = _filesSinceArchive;
                    _filesSinceArchive = 0;
                    tickCount = GetNextFileCount();
                }


                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    bool caughtException = false;

                    // The actual files we can do something about
                    List<FileInfo> actualFiles = new List<FileInfo>();
                    string fileName = null;

                    try
                    {
                        byte[] archiveAnchor = null;
                        // Get all the files under the cache path
                        List<string> fileList = new List<string>(isoFile.GetFileNames(Path.Combine(_cachePath, "*")));
                        fileList.Sort();

                        // reverse so that we can avoid duplicates better
                        fileList.Reverse();

                        // Id manager for items that have been saved
                        ArchiveIdManager serializedItems = new ArchiveIdManager();

                        bool encounteredUpload = false;

                        // Preprocess the list files to pick the ones we want.
                        foreach (string file in fileList)
                        {
                            if (Constants.IsCacheFile(file))
                            {
                                int fileCount = GetFileCount(file);
                                if (fileCount > tickCount)
                                {
                                    continue;
                                }

                                CacheFileType fileType = GetFileType(file);

                                if (fileType != CacheFileType.Conflicts && fileType != CacheFileType.Errors)
                                {
                                    FileInfo fileInfo = new FileInfo()
                                    {
                                        FileType = fileType,
                                        FileName = file,
                                        HasUploadFile = false
                                    };

                                    actualFiles.Add(fileInfo);

                                    // If the file is a SaveChanges file, we want to see if we should put
                                    // a dirty flag in the archive file or not
                                    if (fileType == CacheFileType.SaveChanges && encounteredUpload)
                                    {
                                        fileInfo.HasUploadFile = true;

                                    }
                                    // if there's an upload file, make sure we note it so that we can mark future
                                    // save changes files correctly.
                                    else if (fileType == CacheFileType.UploadResponse)
                                    {
                                        encounteredUpload = true;
                                    }
                                }
                            }
                        }

                        fileName = GetFileName(CacheFileType.Archive, null, tickCount);

                        // Go through the files we parsed and handle correctly.
                        using (Stream fileStream = OpenWriteFile(isoFile, fileName))
                        using (Stream writeStream = OpenWriteCryptoStream(fileStream))
                        {
                            bool encounteredArchive = false;
                            foreach (FileInfo fi in actualFiles)
                            {
                                byte[] currentAnchor = null;
                                switch (fi.FileType)
                                {
                                    case CacheFileType.DownloadResponse:
                                        ResponseData drd = ReadFile<ResponseData>(fi.FileName, isoFile);
                                        currentAnchor = drd.Anchor;
                                        WriteArchiveEntities(drd.Entities, false, writeStream, serializedItems);
                                        break;

                                    case CacheFileType.UploadResponse:
                                        ResponseData responseData = ReadFile<ResponseData>(fi.FileName, isoFile);
                                        currentAnchor = responseData.Anchor;
                                        WriteArchiveEntities(responseData.Entities, false, writeStream, serializedItems);
                                        break;

                                    case CacheFileType.SaveChanges:
                                        IsolatedStorageOfflineEntity[] entities = ReadFile<IsolatedStorageOfflineEntity[]>(fi.FileName, isoFile);
                                        WriteArchiveEntities(entities, !fi.HasUploadFile, writeStream, serializedItems);
                                        break;

                                    case CacheFileType.Archive:
                                        currentAnchor = TransferArchiveFile(isoFile, fi.FileName, writeStream, serializedItems);
                                        encounteredArchive = true;
                                        break;

                                }

                                // Since reading is happening from the end, only need to set the anchor if the
                                // last oe was null
                                if (archiveAnchor == null)
                                {
                                    archiveAnchor = currentAnchor;
                                }

                                // Since reading is happening from the end, once an archive file is read, we
                                // can skip everything else.
                                if (encounteredArchive)
                                {
                                    break;
                                }
                            }

                            // At the end write the anchor
                            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(byte[]));
                            serializer.WriteObject(writeStream, archiveAnchor);

                            // replace the encryptor if necessary
                            lock (_syncRoot)
                            {
                                CheckAndRefreshEncryptor();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is SerializationException || e is IsolatedStorageException)
                        {
                            caughtException = true;

                            // delete the archive file
                            if (fileName != null)
                            {
                                isoFile.DeleteFile(fileName);
                            }

                            // if something failed, restore the files synce archive count
                            lock (_syncRoot)
                            {
                                // Do an add here because it could have been incremented.
                                _filesSinceArchive += originalFilesSyncArchive;
                            }
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!caughtException)
                    {
                        // If all of this completed successfully, delete the files. This is outside of the try-catch above
                        // because we want to avoid doing a rearchive if the only failure was deleting a file.
                        foreach (FileInfo fi in actualFiles)
                        {
                            DeleteFile(isoFile, fi.FileName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes the specified entities to the stream.  This approach is different than the others because it serializes one entity at a time.
        /// </summary>
        /// <param name="entities">Entities to serialize</param>
        /// <param name="dirty">whether or not they should be marked as dirty</param>
        /// <param name="stream">Stream to which to write</param>
        /// <param name="serializedEntities">List of entities already serialized, used to prevent duplicates</param>
        private void WriteArchiveEntities(IEnumerable<IsolatedStorageOfflineEntity> entities, bool dirty, Stream stream, ArchiveIdManager serializedEntities)
        {
            // Used to delimit lines
            byte [] buffer = Encoding.UTF8.GetBytes("\r\n");

            // Create the serializer
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ArchiveEntity), _knownTypes);

            // Loop through entities
            foreach (IsolatedStorageOfflineEntity entity in entities)
            {
                if (!serializedEntities.ContainsEntity(entity))
                {
                    // Write the object
                    serializer.WriteObject(stream, new ArchiveEntity()
                    {
                        Entity = entity,
                        IsDirty = dirty
                    });

                    // Write the delimiter
                    stream.Write(buffer, 0, buffer.Length);
                }

                // Always record that we've processed the entities.  This can help
                // map atom id to property key in the event we have a tombstone come first
                // and only have an atom id the first time we encounter an entity.
                serializedEntities.ProcessedEntity(entity);
            }
        }

        /// <summary>
        /// Reads the archive file and stores it in the cache data
        /// </summary>
        /// <param name="file">file name to read</param>
        /// <param name="cacheData">in-memory data</param>
        /// <param name="context"></param>
        private void ReadArchiveFile(string file, CacheData cacheData, IsolatedStorageOfflineContext context)
        {
            byte[] anchor = null;
            List<ArchiveEntity> entities = new List<ArchiveEntity>();
            bool validFile = false;

            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            using (Stream fileStream = OpenReadFile(isoFile, Path.Combine(_cachePath, file)))
            using (Stream readStream = OpenReadCryptoStream(fileStream))
            using (StreamReader reader = new StreamReader(readStream))
            {
                // Create the serializer
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ArchiveEntity), _knownTypes);

                // The approach here is to use a stream reader to read out each line, then convert that line to bytes
                // and use the json serializer.

                // If it's a curly brace, we're reading an entity
                while (!reader.EndOfStream && reader.Peek() == '{')
                {
                    string line = reader.ReadLine();
                    using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                    {
                        ArchiveEntity entity = (ArchiveEntity)serializer.ReadObject(memStream);
                        entities.Add(entity);
                    }
                }

                // If it's a square bracket, we're reading the anchor
                if (!reader.EndOfStream && reader.Peek() == '[')
                {
                    string line = reader.ReadLine();
                    using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                    {
                        anchor = (byte[])(new DataContractJsonSerializer(typeof(byte[]))).ReadObject(memStream);

                        // The file is only valid if the anchor is read successfully.
                        validFile = true;
                    }
                }

                CheckAndRefreshDecryptor();
            }

            // If the file was valid, we can use the data
            if (validFile)
            {
                // Clear existing data out
                cacheData.ClearCollections();
                _changes.Clear();

                foreach (ArchiveEntity archiveEntity in entities)
                {
                    IsolatedStorageOfflineEntity isoEntity = archiveEntity.Entity;

                    // Determine whether the change was local or downloaded
                    if (archiveEntity.IsDirty)
                    {
                        cacheData.AddSerializedLocalChange(isoEntity);
                        _changes.Add((OfflineEntityKey)isoEntity.GetIdentity(), isoEntity);
                    }
                    else
                    {
                        cacheData.AddSerializedDownloadItem(isoEntity);
                    }
                }

                // Set the anchor with the one read from the file.
                _anchor = anchor;
                cacheData.AnchorBlob = anchor;
            }
        }

        private Stream OpenReadFile(IsolatedStorageFile isoFile, string path)
        {
            Stream stream = isoFile.OpenFile(path, FileMode.Open, FileAccess.Read);

            return stream;
        }

        private Stream OpenReadCryptoStream(Stream stream)
        {
            if (_encryptionAlgorithm != null)
            {
                stream = new CryptoStream(stream, _decryptor, CryptoStreamMode.Read);
            }

            return stream;
        }

        private Stream OpenWriteCryptoStream(Stream stream)
        {
            if (_encryptionAlgorithm != null)
            {
                stream = new CryptoStream(stream, _encryptor, CryptoStreamMode.Write);
            }

            return stream;
        }

        private Stream OpenWriteFile(IsolatedStorageFile isoFile, string path)
        {
            Stream stream = isoFile.OpenFile(path, FileMode.CreateNew, FileAccess.Write);

            return stream;
        }

        private byte[] TransferArchiveFile(IsolatedStorageFile isoFile, string fileName, Stream stream, ArchiveIdManager serializedItems)
        {
            byte[] anchor = null;

            // Flush the stream
            stream.Flush();

            // Record the position.  If reading the source archive file fails, we want to reset the length to the current position.
            long position = stream.Position;

            byte[] eolBuffer = Encoding.UTF8.GetBytes("\r\n");

            using (Stream inputStream = OpenReadFile(isoFile, Path.Combine(_cachePath, fileName)))
            using (Stream readStream = OpenReadCryptoStream(inputStream))
            {
                bool validFile = false;

                using (StreamReader reader = new StreamReader(readStream))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ArchiveEntity), _knownTypes);
                    while (!reader.EndOfStream && reader.Peek() == '{')
                    {
                        string line = reader.ReadLine();

                        byte [] lineBuffer = Encoding.UTF8.GetBytes(line);
                        using (MemoryStream memStream = new MemoryStream(lineBuffer))
                        {
                            ArchiveEntity entity = (ArchiveEntity)serializer.ReadObject(memStream);

                            if (!serializedItems.ContainsEntity(entity.Entity))
                            {
                                stream.Write(lineBuffer, 0, lineBuffer.Length);
                                stream.Write(eolBuffer, 0, eolBuffer.Length);
                            }

                            serializedItems.ProcessedEntity(entity.Entity);
                        }
                    }

                    if (!reader.EndOfStream && reader.Peek() == '[')
                    {
                        string line = reader.ReadLine();

                        using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                        {

                            anchor = (byte[])(new DataContractJsonSerializer(typeof(byte[]))).ReadObject(memStream);
                            validFile = true;
                        }

                    }
                }

                // if the file wasn't valid, undo everything we just wrote by setting the length back to our original position
                if (!validFile)
                {
                    stream.SetLength(position);

                    // Throw an exception so the anchor doesn't get used and the archive file is scrapped (previously read files
                    // will be worthless anyway
                    throw new SerializationException("Transferring archive file failed");
                }
            }
            

            return anchor;
        }

        private void CheckAndRefreshDecryptor()
        {
            if (_encryptionAlgorithm != null)
            {
                if (!_decryptor.CanReuseTransform)
                {
                    if (_decryptor is IDisposable)
                    {
                        ((IDisposable)_decryptor).Dispose();
                    }

                    _decryptor = _encryptionAlgorithm.CreateDecryptor();
                }
            }
        }

        private void CheckAndRefreshEncryptor()
        {
            if (_encryptionAlgorithm != null)
            {
                if (!_encryptor.CanReuseTransform)
                {
                    if (_encryptor is IDisposable)
                    {
                        ((IDisposable)_encryptor).Dispose();
                    }

                    _encryptor = _encryptionAlgorithm.CreateEncryptor();
                }
            }
        }

        class FileInfo
        {
            public CacheFileType FileType;
            public string FileName;
            public bool HasUploadFile;
        }

        [DataContract]
        internal class ArchiveEntity
        {
            [DataMember]
            public IsolatedStorageOfflineEntity Entity
            {
                get;
                set;
            }

            [DataMember]
            public bool IsDirty
            {
                get;
                set;
            }
        }

#if SLUNITTEST
        /// <summary>
        /// This method is solely to enable unit testing
        /// </summary>
        /// <param name="interval">Timer interval in milliseconds</param>
        public void SetArchiveInterval(long interval)
        {
            _timerInterval = interval;            
        }

        public void SetArchiveInterval(TimeSpan interval)
        {
            _timerInterval = (long)interval.TotalMilliseconds;
        }
#endif

        #endregion

        /// <summary>
        /// The schema for the collection.  Passed in during construction.
        /// </summary>
        private IsolatedStorageSchema _schema;

        /// <summary>
        /// The cache path used to store files.  Passed in during constrution.
        /// </summary>
        private string _cachePath;

        /// <summary>
        /// The last anchor received from the service during download or upload
        /// response.
        /// </summary>
        private byte[] _anchor;

        /// <summary>
        /// List of known types generated during construction and passed to serializers
        /// as they are created in order to speed up serialization and ensure that all
        /// types passed in can be serialized.
        /// </summary>
        private List<Type> _knownTypes;

        /// <summary>
        /// Monotonically increasing tick count for files. Helps ensure that files
        /// can be sorted in the order that they are written.  Updated each time
        /// a new file is written.
        /// </summary>
        private int _fileCount = 0;

        /// <summary>
        /// Set of changes that have been saved but not sent the server.  A dictionary is
        /// used to ensure that there are no duplicates.
        /// </summary>
        private Dictionary<OfflineEntityKey, IsolatedStorageOfflineEntity> _changes;

        /// <summary>
        /// The set of changes that have been sent to the server but for which there has been
        /// no response received yet.  The guid is the unique identifier passed in to the GetChanges
        /// method and is used in the event that there are multiple batches uploaded before a
        /// success response is received (in case queued upload is ever implemented).
        /// </summary>
        private Dictionary<Guid, IEnumerable<IsolatedStorageOfflineEntity>> _sentChangesAwaitingResponse;

        /// <summary>
        /// File opened when the context is first created.  It is used to guard access to the cache path
        /// and ensure that there is only one instance of a context working on a cache path at any one time.
        /// </summary>
        private Stream _lockFile;

        /// <summary>
        /// Whether or not the storage handler is disposed. Set to true in the dispose method.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// Timer started when load is complete to enable archiving of files on disk in order to remove
        /// redundant data.
        /// </summary>
        private Timer _cleanupTimer;

        // Timer interval is 5 minutes
        private long _timerInterval = Constants.TIMER_INTERVAL;

        /// <summary>
        /// Keeps track of the number of files since archive.  It is incremented every time a file is written.
        /// It is also incremented during loading for ever file that occurs from the beginning or after reading
        /// and archive file.
        /// </summary>
        private int _filesSinceArchive = 0;

        /// <summary>
        /// The number of files to be written before archive will attempt to run.
        /// </summary>
        private const int ARCHIVE_FILE_THRESHOLD = 1;

        /// <summary>
        /// Encryption algorithm optionally passed during construction.  Used to encrypt on-disk storage.
        /// </summary>
        SymmetricAlgorithm _encryptionAlgorithm;

        /// <summary>
        /// Encryptor used to encrypt data when writing to disk
        /// </summary>
        ICryptoTransform _encryptor;

        /// <summary>
        /// Used to decrpyt data when reading from disk.
        /// </summary>
        ICryptoTransform _decryptor;

        /// <summary>
        /// Used to sync the _filesSinceArchive during Save, Download and Archiving files
        /// </summary>
        private object _syncRoot = new object();

        /// <summary>
        /// Used to sync multiple archive threads (timer call backs)
        /// </summary>
        private object _archiveLock = new object();
    }
}
