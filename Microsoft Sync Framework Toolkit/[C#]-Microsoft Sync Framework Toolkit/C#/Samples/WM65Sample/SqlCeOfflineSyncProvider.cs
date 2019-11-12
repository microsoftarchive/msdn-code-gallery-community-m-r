// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Samples.Synchronization.ClientServices;

namespace SmartDeviceProject1
{
    //Note: The sample does not handle concurrent sync requests.
    public class SqlCeOfflineSyncProvider : OfflineSyncProvider
    {
        private readonly SqlCeStorageHandler _storageHandler = new SqlCeStorageHandler();

        /// <summary>
        /// OfflineSyncProvider method called when the controller is about to start a sync session.
        /// </summary>
        public override void BeginSession()
        {
        }

        /// <summary>
        /// OfflineSyncProvider method implementation to return a set of sync changes.
        /// </summary>
        /// <param name="state">A unique identifier for the changes that are uploaded</param>
        /// <returns>The set of incremental changes to send to the service</returns>
        public override ChangeSet GetChangeSet(Guid state)
        {
            var changeSet = new ChangeSet();
            IEnumerable<SqlCeOfflineEntity> changes = _storageHandler.GetChanges(state);

            changeSet.Data = changes.Select(c => (IOfflineEntity) c).ToList();
            changeSet.IsLastBatch = true;
            changeSet.ServerBlob = _storageHandler.GetAnchor();

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
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (null != response.Error)
            {
                throw new Exception("Exception during sync!");
            }

            var storageHandler = new SqlCeStorageHandler();
            if (null != response.UpdatedItems && 0 != response.UpdatedItems.Count)
            {
                foreach (var item in response.UpdatedItems)
                {
                    var offlineEntity = (SqlCeOfflineEntity) item;
                    storageHandler.ApplyItem(offlineEntity);
                }
            }

            if (null != response.Conflicts && 0 != response.Conflicts.Count)
            {
                foreach (var conflict in response.Conflicts)
                {
                    // We have an conflict so apply the LiveEntity
                    var liveEntity = (SqlCeOfflineEntity)conflict.LiveEntity;

                    // For a SyncError, which resulted from a client insert, the winning item may be a tombstone version
                    // of the client entity. In this case, the ServiceMetadata.Id property of the LiveEntity will be null.
                    // We need to lookup the item using primary keys in order to update it.
                    if (conflict.GetType() == typeof(SyncError))
                    {
                        var errorEntity = ((SyncError) conflict).ErrorEntity;

                        if (!liveEntity.ServiceMetadata.IsTombstone)
                        {
                            // If the live entity is not a tombstone, then we just need to update the entity.
                            storageHandler.ApplyItem(liveEntity);
                        }
                        else
                        {
                            // At this point, the LiveEntity is a tombstone and does not have primary key info.

                            // If the live entity is a tombstone, then delete the item by looking up the primary key
                            // from the error entity.
                            // The error entity in this case will have both Id and the primary keys.
                            errorEntity.ServiceMetadata.IsTombstone = true;
                            errorEntity.ServiceMetadata.Id = null;
                            storageHandler.ApplyItem((SqlCeOfflineEntity) errorEntity);
                        }
                    }
                    else
                    {
                        storageHandler.ApplyItem(liveEntity);    
                    }
                }
            }

            // Clear all the isdirty flags and delete all rows with IsTombstone = true
            storageHandler.ResetDirtyAndDeleteTombstones();

            storageHandler.SaveAnchor(response.ServerBlob);
        }

        /// <summary>
        /// Returns the last server blob that was received during sync
        /// </summary>
        /// <returns>The server blob.</returns>
        public override byte[] GetServerBlob()
        {
            return _storageHandler.GetAnchor() ?? new byte[0];
        }

        /// <summary>
        /// OfflineSyncProvider method called to save changes retrieved from the sync service.
        /// </summary>
        /// <param name="changeSet">The set of changes from the service to save. Also contains an updated server
        /// blob.</param>
        public override void SaveChangeSet(ChangeSet changeSet)
        {
            if (null == changeSet)
            {
                throw new ArgumentException("changeSet is null", "changeSet");
            }

            var entities = changeSet.Data.Cast<SqlCeOfflineEntity>();

            _storageHandler.SaveDownloadedChanges(changeSet.ServerBlob, entities);
        }

        /// <summary>
        /// OfflineSyncProvider method called when sync is completed. 
        /// </summary>
        public override void EndSession()
        {
        }
    }
}
