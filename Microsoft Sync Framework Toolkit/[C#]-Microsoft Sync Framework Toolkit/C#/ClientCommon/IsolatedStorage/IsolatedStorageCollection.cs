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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// Abstract class that serves as the base class for all collections synchronized from the server.
    /// </summary>
    internal abstract class IsolatedStorageCollection
    {
        public abstract void AddSerializedEntity(IsolatedStorageOfflineEntity entity);

        public abstract IsolatedStorageOfflineEntity AddOrUpdateSyncEntity(IsolatedStorageOfflineEntity entity);
        public abstract IsolatedStorageOfflineEntity AddOrUpdateSyncEntity(IsolatedStorageOfflineEntity entity, bool delayNotification);
        public abstract void ResetSavedEntitiesToUnmodified();
        public abstract SyncConflict MapSyncConflict(IsolatedStorageOfflineEntity entity, SyncConflict conflict, IsolatedStorageOfflineContext context);
        public abstract SyncError MapSyncError(IsolatedStorageOfflineEntity entity, SyncError error, IsolatedStorageOfflineContext context);
        public abstract void Clear();
        public abstract IEnumerable<IsolatedStorageOfflineEntity> GetSaveFailures();
        public abstract IEnumerable<IsolatedStorageOfflineEntity> CommitChanges();
        public abstract void Rollback();
        public abstract void ResolveConflictByRollback(IsolatedStorageOfflineEntity entity);
        public abstract void Notify();
        public abstract void ClearSyncConflict(IsolatedStorageOfflineEntity entity);
        public abstract void ClearSyncError(IsolatedStorageOfflineEntity entity);

        public event Action<IsolatedStorageOfflineEntity> ModifiedItemChanged;

        protected void OnModifiedItemChanged(IsolatedStorageOfflineEntity changedEntity)
        {
            Action<IsolatedStorageOfflineEntity> action = ModifiedItemChanged;

            if (action != null)
            {
                action(changedEntity);
            }
        }
    }

    internal class IsolatedStorageCollection<T> : IsolatedStorageCollection, IEnumerable, IEnumerable<T>, INotifyCollectionChanged where T : IsolatedStorageOfflineEntity
    {
        public IsolatedStorageCollection()
        {
            this._entityMap = new Dictionary<object, T>();
            this._entityList = new List<T>();
            this._tombstones = new List<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            List<T> enumList = null;
            lock (_syncRoot)
            {
                enumList = new List<T>(_entityList);
            }

            if (enumList != null)
            {
                foreach (T t in enumList)
                {
                    yield return t;
                }
            }
        }

        /// <summary>
        /// Adds an item that was serialized from a download response.  This item will be
        /// blindly added, rather than updating the entity that is already there.
        /// </summary>
        /// <param name="entity">The entity to add to the collection.  If the entity with the same keys already exists,
        /// that entity will be updated.  Otherwise, the passed-in entity will be returned</param>
        public override void AddSerializedEntity(IsolatedStorageOfflineEntity entity)
        {
            // Retrieve the key for the item
            object key = entity.GetIdentity();

            // If the incoming entity is a tombstone
            if (entity.IsTombstone)
            {
                // If the item exists, remove it
                T t;
                if (_entityMap.TryGetValue(key, out t))
                {
                    _entityMap.Remove(key);
                    _entityList.Remove(t);
                }

                // Don't need to worry about adding a Saved entity to tombstones because
                // the disk layer manages saved tombstones.
            }
            else
            {
                T current;

                // If the entity exists, update its properties
                if (_entityMap.TryGetValue(key, out current))
                {
                    current.UpdateFromSync(entity);
                }

                // Otherwise add the entity to the collection (no notification because its a serialized item
                // which means that the app doesn't have a reference
                else
                {
                    _entityList.Add((T)entity);
                    _entityMap.Add(key, (T)entity);
                }
            }
        }

        /// <summary>
        /// Handles downloaded changes from sync.
        /// </summary>
        /// <param name="entity">Entity whose values should get added.</param>
        /// <returns>The entity that was updated (the one in the collection if it is not a tombstone)</returns>
        public override IsolatedStorageOfflineEntity AddOrUpdateSyncEntity(IsolatedStorageOfflineEntity entity)
        {
            return AddOrUpdateSyncEntity(entity, false);
        }

        /// <summary>
        /// Handles downloaded changes from sync. During first sync we delay notifying the client or databinder until
        /// EndSession. In EndSession the NotifyAllCollections is called notifying the entire collection to the databinder instead of per item
        /// </summary>
        /// <param name="entity">Entity whose values should get added.</param>
        /// <param name="delayNotification">notify the databinder only at the end of the session instead of per entity</param>
        /// <returns>The entity that was updated (the one in the collection if it is not a tombstone)</returns>
        public override IsolatedStorageOfflineEntity AddOrUpdateSyncEntity(IsolatedStorageOfflineEntity entity, bool delayNotification)
        {
            T localItem = null;
            NotifyCollectionChangedEventArgs eventArgs = null;
            int index = -1;
            bool foundInTombstoneList = false;
            
            string atomId = entity.ServiceMetadata.Id;

            // Find the local copy of the item
            if (entity.IsTombstone && !string.IsNullOrEmpty(atomId))
            {
                // If the incoming entity is a tombstone and has an atom id, we need to search by atom id
                for (int i = 0; i < _entityList.Count; ++i)
                {
                    if (atomId.Equals(_entityList[i].ServiceMetadata.Id, StringComparison.OrdinalIgnoreCase))
                    {
                        index = i;
                        localItem = _entityList[i];
                        foundInTombstoneList = false;
                        break;
                    }
                }

                // If it's not live, see if it's an unsaved tombstone
                if (localItem == null)
                {
                    for (int i = 0; i < _tombstones.Count; ++i)
                    {
                        if (_tombstones[i].ServiceMetadata.Id == atomId)
                        {
                            index = i;
                            localItem = _tombstones[i];
                            foundInTombstoneList = true;
                            break; ;
                        }
                    }
                }
            }

            // if the local item is still null at this point, the incoming entity is either a live item
            // or it could be a tombstone for which there was a local insert and there was an insert-delete
            // conflict, so search by key
            if (localItem == null)
            {
                object key = entity.GetIdentity();                
                // If there's no atom id, this may be an insert that failed, so get entity based on key
                if (_entityMap.TryGetValue(key, out localItem))
                {
                    for (int i = 0; i < _entityList.Count; ++i)
                    {
                        if (_entityList[i].GetIdentity().Equals(key))
                        {
                            index = i;
                            foundInTombstoneList = false;
                            break;
                        }
                    }

                    Debug.Assert(index >= 0);                    
                }
                else {
                    // If it's not in the live list, see if it's an unsaved tomstone
                    for (int i = 0; i < _tombstones.Count; ++i)
                    {
                        if (_tombstones[i].GetIdentity().Equals(key))
                        {
                            index = i;
                            localItem = _tombstones[i];
                            foundInTombstoneList = true;
                            break;
                        }
                    }
                }
            }

            if (entity.IsTombstone)
            {
                // If the local item was found somewhere, update it
                if (localItem != null)
                {
                    lock (localItem.SyncRoot)
                    {
                        if (!localItem.IsTombstone)
                        {
                            // If the local item is live and unmodified, remove it
                            if (localItem.EntityState == OfflineEntityState.Unmodified)
                            {
                                object key = localItem.GetIdentity();

                                _entityMap.Remove(key);
                                Debug.Assert(!foundInTombstoneList && index >= 0);
                                _entityList.RemoveAt(index);

                                localItem.ApplyDelete();

                                if (!delayNotification)
                                {
                                    eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, localItem, index);
                                }
                            }
                            else
                            {
                                // If it is modified, call UpdateFromSync to update the snapshot (will cause
                                // a store conflict when SaveChanges is called)
                                localItem.UpdateFromSync(entity);
                            }
                        }
                        else
                        {
                            // Remove it from the tombstones.  Technically this is a store conflict, but either
                            // resolution will result in the same thing, and doing a CancelChanges will be a no-op
                            // so just remove it now
                            Debug.Assert(foundInTombstoneList && index >= 0);
                            _tombstones.RemoveAt(index);
                        }
                    }
                }
                else
                {
                    // We got a tombstone for an item we don't know anything about,
                    // so just ignore the tombstone
                }

            }
            else
            {
                // If the local item is not null, update it from sync
                if (localItem != null)
                {
                    lock (localItem.SyncRoot)
                    {
                        localItem.UpdateFromSync(entity);
                    }
                }
                else
                {
                    // Handle an incoming live item
                    object key = entity.GetIdentity();
                    // There is no local item so insert it
                    T entityT = (T)entity;
                    // Insert item
                    entity.EntityState = OfflineEntityState.Unmodified;
                    _entityMap.Add(key, entityT);
                    _entityList.Add(entityT);
                    localItem = entityT;
                    if (!delayNotification)
                    {
                        eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entity, _entityList.Count - 1);
                    }
                }
            }

            // If the local entity is modified, notify that it was changed
            if (localItem != null && localItem.EntityState == OfflineEntityState.Modified)
            {
                OnModifiedItemChanged(localItem);
            }

            // If an entity was added or removed, trigger the notification
            if (eventArgs != null)
            {
                OnCollectionChanged(eventArgs);
            }

            return localItem;
        }

        /// <summary>
        /// Called from CacheData.NotifyAllCollections to notify the databinder only once
        /// during initial sync rather than for every item
        /// </summary>
        public override void Notify()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// This method should only be used when reading at the beginning.  It retrieves an existing entity or adds it if it is
        /// not found
        /// </summary>
        /// <param name="entity">entity to look up</param>
        /// <returns>Entity which is in the collection</returns>
        private T GetEntity(T entity)
        {
            T t;

            object key = entity.GetIdentity();

            // Try to get the entity
            if (!_entityMap.TryGetValue(key, out t))
            {
                // If it doesn't exist, check the tombstones
                t = (from tombstone in _tombstones
                     where tombstone.GetIdentity().Equals(key)
                     select tombstone).FirstOrDefault();
            }

            return t;
        }

        /// <summary>
        /// Sets the entity state from Saved to Modified.  This should only ever be
        /// called during deserialization.
        /// </summary>       
        public override void ResetSavedEntitiesToUnmodified()
        {
            var query = from s in _entityList
                        where (s.EntityState == OfflineEntityState.Saved) ||
                            (s.EntityState == OfflineEntityState.Modified && s.SnapshotState == OfflineEntityState.Saved)
                        select s;

            foreach (T entity in query)
            {
                if (entity.EntityState != OfflineEntityState.Saved &&
                    (entity.EntityState != OfflineEntityState.Modified || entity.SnapshotState != OfflineEntityState.Saved))
                {
                    throw new InvalidOperationException("Entity state should be Saved");
                }

                if (entity.EntityState == OfflineEntityState.Saved)
                {
                    entity.EntityState = OfflineEntityState.Unmodified;
                }
                else
                {
                    entity.SnapshotState = OfflineEntityState.Unmodified;
                }
            }
        }

        /// <summary>
        /// Ensures that the references between the conflict and the live entity are correct.
        /// </summary>
        /// <param name="mapEntity">IsolatedStorageOfflineEntity</param>
        /// <param name="conflict">The new conflict to set on the entity</param>
        /// <param name="context">The overall context so the conflict can be cleared</param>
        /// <returns>The old conflict that was on the item</returns>
        public override SyncConflict MapSyncConflict(IsolatedStorageOfflineEntity mapEntity, SyncConflict conflict, IsolatedStorageOfflineContext context)
        {
            SyncConflict oldConflict = null;

            // If the map entity is null, it is likely the case that we have a serialized
            // conflict, so find it in the collection
            if (mapEntity == null)
            {
                mapEntity = GetEntity((T)conflict.LosingEntity);

                // If it's not in the collection, it was deleted by an upload response, so clone
                // the losing entity and turn it into a tombstone, since we need a representation
                // of the live entity
                if (mapEntity == null)
                {
                    mapEntity = ((IsolatedStorageOfflineEntity)conflict.LosingEntity).Clone();
                    mapEntity.ApplyDelete();
                }
            }

            oldConflict = mapEntity.SyncErrorInfo.SyncConflict;

            mapEntity.SyncErrorInfo.SetSyncConflict(context, conflict);
            conflict.LiveEntity = mapEntity;

            return oldConflict;
        }

        /// <summary>
        /// Ensures that the references between the error and the live entity are correct.
        /// </summary>
        /// <param name="mapEntity">IsolatedStorageOfflineEntity</param>
        /// <param name="error">The new error to set on the entity</param>
        /// <param name="context">The overall context so the error can be cleared</param>
        /// <returns>The old error that was on the item</returns>
        public override SyncError MapSyncError(IsolatedStorageOfflineEntity mapEntity, SyncError error, IsolatedStorageOfflineContext context)
        {
            SyncError oldError = null;

            // If the map entity is null, it is likely the case that we have a serialized
            // error, so find it in the collection
            if (mapEntity == null)
            {
                mapEntity = GetEntity((T)error.ErrorEntity);

                // If it's not in the collection, it was deleted by an upload response, so clone
                // the losing entity and turn it into a tombstone, since we need a representation
                // of the live entity
                if (mapEntity == null)
                {
                    mapEntity = ((IsolatedStorageOfflineEntity)error.ErrorEntity).Clone();
                    mapEntity.ApplyDelete();
                }
            }

            oldError = mapEntity.SyncErrorInfo.SyncError;

            mapEntity.SyncErrorInfo.SetSyncError(context, error);
            error.LiveEntity = mapEntity;

            return oldError;
        }

        /// <summary>
        /// Clears the in-memory data.
        /// </summary>
        public override void Clear()
        {
            lock (_syncRoot)
            {
                _entityMap.Clear();
                _entityList.Clear();
                _tombstones.Clear();

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Sets the SyncConflict from the Entity to null.
        /// </summary>
        /// <param name="entity"></param>
        public override void ClearSyncConflict(IsolatedStorageOfflineEntity entity)
        {
            lock (_syncRoot)
            {
                 object key = entity.GetIdentity();
                 T localItem = null;

                 if (_entityMap.TryGetValue(key, out localItem))
                 {
                     //Note: We can call ClearSyncConflict on the SyncErrorInfo,but, that would
                     //Cause a deadlock. We would need to get the _cacheData.ClearSyncConflicts() ouside the _saveSyncLock in
                     //IsoContext.ClearConflicts(). This "might" expose to unsafe access.
                     localItem.SyncErrorInfo.UnsafeClearSyncConflict();
                 }

            }
        }

        /// <summary>
        /// Sets the SyncError from the Entity to null.
        /// </summary>
        /// <param name="entity"></param>
        public override void ClearSyncError(IsolatedStorageOfflineEntity entity)
        {
            lock (_syncRoot)
            {
                object key = entity.GetIdentity();
                T localItem = null;

                if (_entityMap.TryGetValue(key, out localItem))
                {
                    //Note: We can call ClearSyncError on the SyncErrorInfo,but, that would
                    //Cause a deadlock. We would need to get the _cacheData.ClearSyncErrors() ouside the _saveSyncLock in
                    //IsoContext.ClearSyncErrors(). This "might" expose to unsafe access.
                    localItem.SyncErrorInfo.UnsafeClearSyncError();
                }

            }
        }

        /// <summary>
        /// Returns any failures that would happen if attempting to save changes.
        /// </summary>
        /// <returns>List of failures.</returns>
        public override IEnumerable<IsolatedStorageOfflineEntity> GetSaveFailures()
        {
            var liveFailures = from e in _entityMap.Values
                   where (e.EntityState == OfflineEntityState.Modified && !e.CanSaveChanges)
                   select (IsolatedStorageOfflineEntity)e;

            var tombstoneFailures = from e in _tombstones
                   where (e.EntityState == OfflineEntityState.Modified && !e.CanSaveChanges)
                   select (IsolatedStorageOfflineEntity)e;

            // concat them to return
            return liveFailures.Concat(tombstoneFailures);
        }

        /// <summary>
        /// Commits any modified changes in the collection, 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IsolatedStorageOfflineEntity> CommitChanges()
        {
            List<IsolatedStorageOfflineEntity> changes = new List<IsolatedStorageOfflineEntity>();

            var changeQuery = (from e in _entityMap.Values
                               where e.EntityState == OfflineEntityState.Modified
                               select e).Concat(_tombstones);

            foreach (IsolatedStorageOfflineEntity entity in changeQuery)
            {
                entity.AcceptChanges();
                changes.Add(entity.Clone());
            }

            _tombstones.Clear();

            return changes;
        }

        /// <summary>
        /// Reverts any pending changes.
        /// </summary>
        public override void Rollback()
        {
            // Find the items that have changed
            var changeQuery = (from e in _entityMap.Values
                               where e.EntityState == OfflineEntityState.Modified
                               select e).ToList();

            List<IsolatedStorageOfflineEntity> changesToRemove = new List<IsolatedStorageOfflineEntity>();

            foreach (IsolatedStorageOfflineEntity entity in changeQuery)
            {
                // If the entity has a snapshot, it's a change to an entity, so
                // just rollback.
                if (entity.HasSnapshot)
                {
                    entity.RejectChangesInternal();

                    // Check for tombstone at this point.  If this happens, a tombstone was
                    // brought in during sync for a modified entity.
                    if (entity.IsTombstone)
                    {
                        changesToRemove.Add(entity);
                    }
                }
                else
                {
                    // If there's no snapshot, the entity was a create, so remove it
                    changesToRemove.Add(entity);
                }

            }

            // remove the changes that need to be removed
            foreach (IsolatedStorageOfflineEntity entity in changesToRemove)
            {
                _entityMap.Remove(entity.GetIdentity());
                _entityList.Remove((T)entity);

                entity.EntityState = OfflineEntityState.Detached;
            }

            // now rollback tombstones
            foreach (T entity in _tombstones.ToList())
            {
                if (entity.HasStoreConflict)
                {
                    entity.RejectChangesInternal();
                }
                else
                {
                    // Get the original
                    T original = (T)entity.GetOriginal();

                    // If the original is not a tombstone add it back.
                    if (original != null && !original.IsTombstone)
                    {
                        original.IsReadOnly = false;
                        _entityMap.Add(original.GetIdentity(), original);
                        _entityList.Add(original);
                    }
                }
            }

            // Since only non-saved tombstones exist here, revert them.
            _tombstones.Clear();
        }

        /// <summary>
        /// Resolves a conflict by rolling back a specific change.
        /// </summary>
        /// <param name="entity"></param>
        public override void ResolveConflictByRollback(IsolatedStorageOfflineEntity entity)
        {
            lock (entity.SyncRoot)
            {
                bool modifyIsTombstone = entity.IsTombstone;
                bool snapshotIsTombstone = false;
                T entityT = (T)entity;

                // Entity must have a snapshot for there to be a conflict
                Debug.Assert(entityT.HasSnapshot);

                // Fill the entity with its snapshot
                entityT.FillEntityFromSnapshot(entityT);

                snapshotIsTombstone = entityT.IsTombstone;

                // We should prevent delete-delete conflicts sooner, so check here to make
                // sure they're not happening
                Debug.Assert(!modifyIsTombstone || !snapshotIsTombstone);

                // If the modified entity was a tombstone, remove it from the tombstone and add it to the
                // entities
                if (modifyIsTombstone)
                {
                    // Remove it from the tombstone list
                    _tombstones.Remove(entityT);

                    // Add the live snapshot back to the live items list
                    _entityMap.Add(entity.GetIdentity(), entityT);
                    _entityList.Add(entityT);

                    // Notify any databinding
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, entity, _entityList.Count - 1));
                }
                // If the snapshot is a tombstone, remove it from the live entities
                else if (snapshotIsTombstone)
                {
                    // Remove the item from the list of live items
                    _entityMap.Remove(entity.GetIdentity());

                    int index = _entityList.IndexOf(entityT);
                    _entityList.RemoveAt(index);

                    // Note: We don't add to tombstones list here because that only contains items that are
                    // deleted locally, not deletes brought in by sync (which is how the tombstone would be a
                    // snapshot).

                    // Notify any databinding
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove, entity, index));
                }
            }
        }

        /// <summary>
        /// Removes the specified time and marks it as a tombstone
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void DeleteItem(T item)
        {
            object key = item.GetIdentity();

            T t;

            // Verify that this is an item that can be removed.
            if (!_entityMap.TryGetValue(key, out t))
            {
                throw new ArgumentException(String.Format("The requested item does not exist in the collection for type {0}", typeof(T).FullName));
            }

            _entityMap.Remove(key);

            // Have to do a search in the entity list to get the item anyway,
            // so get the index to avoid searching twice and allow better collection changed notification.
            int index = _entityList.IndexOf(t);

            Debug.Assert(index >= 0 && index < _entityList.Count);

            _entityList.RemoveAt(index);

            t.DeleteItem();
            _tombstones.Add(t);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, t, index));
        }

        /// <summary>
        /// Adds the requested item.
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddItem(T item)
        {
            object key = item.GetIdentity();

            if (_entityMap.ContainsKey(key))
            {
                throw new ArgumentException(String.Format("An item with the same primary keys already exists in the collection for type {0}", typeof(T).FullName));
            }

            item.EntityState = OfflineEntityState.Modified;
            _entityMap.Add(key, item);
            _entityList.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, _entityList.Count - 1));
        }

        #region INotifyCollectionChanged

        /// <summary>
        /// Event from INotifyCollectionChanged that is notified when the collection has changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        /// <summary>
        /// Method that calls the CollectionChanged event.
        /// </summary>
        /// <param name="args"></param>
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        /// <summary>
        /// Collection that allows lookup of entities that are part of the collection by primary key.  It is updated whenever
        /// an entity is added or removed from the collection, either by sync or by the application.
        /// </summary>
        private Dictionary<object, T> _entityMap;

        /// <summary>
        /// Collection that allows linear and random access to entities.  It contains the same entities as the _entityMap.  It is
        /// mainly beneficial for providing accurate collection notification change events.  But it is also helpful for providing
        /// enumeration and will be beneficial should random access to the collection be exposed in the future.  It is always
        /// updated when the _entityMap is updated.
        /// </summary>
        private List<T> _entityList;

        /// <summary>
        /// The collection of unsaved tombstones.  They are added whenever the application requests the deletion of an item.
        /// The collectoin is cleared when changes are saved or when the changes are cancelled.
        /// </summary>
        private List<T> _tombstones;

        /// <summary>
        /// Synchronizes access for the collection generated for enumeration.
        /// </summary>
        private object _syncRoot = new object();
    }
}
