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
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// This class is the base entity from which all entities used by the isolated
    /// storage offline context must inherit
    /// </summary>
    [DataContract()]
    public abstract class IsolatedStorageOfflineEntity : 
        IOfflineEntity,
        INotifyPropertyChanged
    {
        /// <summary>
        /// Protected constructor because class is private.  Initial state of created
        /// entities will be Detached.
        /// </summary>
        protected IsolatedStorageOfflineEntity()
        {
            this._state = OfflineEntityState.Detached;
            this._syncInfo = new SyncErrorInfo();
            this._entityMetadata = new OfflineEntityMetadata();
        }

        /// <summary>
        /// Whether the entity is a tombstone.
        /// </summary>
        /// <remarks>
        /// The setter can only be called if the state is detached.  Otherwise,
        /// the tombstone state should be managed by the context.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Display(AutoGenerateField = false)]
        [DataMember]
        public OfflineEntityMetadata ServiceMetadata
        {
            get
            {
                return _entityMetadata;
            }

            set
            {
                if (EntityState == OfflineEntityState.Detached)
                {
                    SetServiceMetadata(value);
                }
                else
                {
                    throw new InvalidOperationException("EntityMetadata can only be set when the entity state is Detached.");
                }
            }
        }

        /// <summary>
        /// Event raised whenever a Microsoft.Synchronization.ClientServices.IsolatedStorage.IsolatedStorageOfflineEntity
        /// has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called from a property setter to notify the framework that a property
        /// is about to be changed.  This method will perform change-tracking related
        /// operations.
        /// </summary>
        /// <param name="propertyName">The name of the property that is changing</param>
        protected void OnPropertyChanging(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName");
            }

            CreateSnapshot();
        }

        /// <summary>
        /// Called from a property setter to notify the framework that a property
        /// has changed.  This method will raise the PropertyChanged event and change
        /// the state to Modified if its current state is Unmodified or Submitted.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            RaisePropertyChanged(propertyName);

            if ((EntityState == OfflineEntityState.Unmodified ||
                EntityState == OfflineEntityState.Saved) && _trackChanges)
            {
                EntityState = OfflineEntityState.Modified;
            }
        }

        /// <summary>
        /// Gets the original state of the entity at the time it was last Submitted.
        /// </summary>
        /// <returns>The original entity, if the entity has been modified, null otherwise.</returns>
        public IsolatedStorageOfflineEntity GetOriginal()
        {
            IsolatedStorageOfflineEntity original = null;

            lock (_syncRoot)
            {
                if (this.EntityState == OfflineEntityState.Modified &&
                    this._original != null)
                {
                    original = (IsolatedStorageOfflineEntity)Activator.CreateInstance(this.GetType());

                    FillEntityFromSnapshot(original);
                    original.IsReadOnly = true;
                }
            }

            return original;
        }

        /// <summary>
        /// Reverts all changes made to the entity since the last time it was Submitted and restores
        /// it to its original state.  If the entity has a store conflict, it will be treated as though
        /// the conflict is resolved with the AcceptStoreEntity resolution.
        /// </summary>
        public void RejectChanges()
        {
            lock (_syncRoot)
            {
                if (this._storeConflict == null)
                {
                    if (_state != OfflineEntityState.Modified)
                    {
                        throw new InvalidOperationException("Cannot reject changes to unmodified entity");
                    }

                    if (_entityMetadata.IsTombstone)
                    {
                        throw new InvalidOperationException("Tombstone changes can only be rejected by calling CancelChanges on the context");
                    }

                    if (_original == null)
                    {
                        throw new InvalidOperationException("Added items can only be rejected by calling CancelChanges on the context");
                    }

                    if (_original.IsTombstone)
                    {
                        throw new InvalidOperationException("The item snapshot is a tombstone, so the change can only be rejected by calling CancelChanges on the context");
                    }


                    FillEntityFromSnapshot(this);
                    _original = null;
                }
                else
                {
                    this._storeConflict.Resolve(StoreConflictResolutionAction.AcceptStoreEntity);
                }
            }
        }

        /// <summary>
        /// Method called when CancelChanges is called on the context.  This method is used so that the
        /// ResolveInternal method can be called on the conflict, which helps avoid a dead lock on the
        /// SaveSyncLock on the context.
        /// </summary>
        internal void RejectChangesInternal()
        {
            lock (_syncRoot)
            {
                if (this._storeConflict == null)
                {
                    FillEntityFromSnapshot(this);
                    _original = null;
                }
                else
                {
                    this._storeConflict.ResolveInternal(StoreConflictResolutionAction.AcceptStoreEntity);
                }
            }

        }

        /// <summary>
        /// Returns whether or not the changes made to the item can be saved.  It will return false if
        /// the snapshot changed as a result of sync.
        /// </summary>
        internal bool CanSaveChanges
        {
            get
            {
                if (_state != OfflineEntityState.Modified)
                {
                    // Since this is only called by the context, this should never happen
                    throw new InvalidOperationException("Entity is not modified");
                }

                lock (_syncRoot)
                {
                    if (_original != null)
                    {
                        if (TickCount != (ulong)_original.TickCount)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        internal bool IsTombstone
        {
            get
            {
                return _entityMetadata.IsTombstone;
            }

            set
            {
                if (value != _entityMetadata.IsTombstone)
                {
                    _entityMetadata.IsTombstone = value;
                }
            }
        }

        /// <summary>
        /// Called when the context is submitting items.  It will throw if the item cannot be accepted.
        /// </summary>
        internal void AcceptChanges()
        {
            if (_state != OfflineEntityState.Modified)
            {
                // Since this is only called by the context, this should never happen
                throw new InvalidOperationException("Entity is not modified");
            }

            lock (_syncRoot)
            {

                if (_original != null)
                {
                    if (TickCount != (ulong)_original.TickCount)
                    {
                        throw new InvalidOperationException("Snapshot has changed since the item was last submitted");
                    }
                }

                _state = OfflineEntityState.Saved;
                _original = null;
            }
        }

        /// <summary>
        /// Returns whether or not the entity is read-only.
        /// </summary>
        [Display(AutoGenerateField=false)]
        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }

            internal set
            {
                _isReadOnly = value;
            }
        }

        /// <summary>
        /// Returns the current state of the entity.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public OfflineEntityState EntityState
        {
            get
            {
                return _state;
            }

            internal set
            {
                if (_state != value)
                {
                    _state = value;
                    RaisePropertyChanged("EntityState");
                }
            }
        }

        /// <summary>
        /// Returns whether or not the entity is modified
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool HasChanges
        {
            get
            {
                return EntityState == OfflineEntityState.Modified;
            }
        }

        /// <summary>
        /// Returns the class that contains information about any sync conflicts
        /// that may have occurred
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SyncErrorInfo SyncErrorInfo
        {
            get
            {
                return _syncInfo;
            }
        }

        /// <summary>
        /// The store conflict that occurred the last time SaveChanges was attempted.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public StoreConflict StoreConflict
        {
            get
            {
                return _storeConflict;
            }

            internal set
            {
                if (value != _storeConflict)
                {
                    _storeConflict = value;
                    RaisePropertyChanged("StoreConflict");
                }
            }
        }

        /// <summary>
        /// Returns whether or not the entity has a store conflict 
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool HasStoreConflict
        {
            get
            {
                return _storeConflict != null;
            }
        }

        /// <summary>
        /// Tick count used to determine if there are conflicts.
        /// </summary>
        internal ulong TickCount { get; set; }

        /// <summary>
        /// Updates an entity from sync.
        /// </summary>
        /// <param name="entity">Entity with changes to update</param>
        /// <returns>Whether or not the entity is already modified</returns>
        internal void UpdateFromSync(IsolatedStorageOfflineEntity entity)
        {
            lock (_syncRoot)
            {
                if (this.EntityState == OfflineEntityState.Modified)
                {
                    OfflineEntitySnapshot snapshot = GetSnapshotFromEntity(entity);
                    snapshot.IsTombstone = entity.IsTombstone;
                    snapshot.TickCount = this.TickCount + 1;

                    this._original = snapshot;
                }
                else
                {
                    _trackChanges = false;
                    // Update properties
                    CopyEntityToThis(entity);
                    _trackChanges = true;
                }
            }
        }

        /// <summary>
        /// Does reflection to copy the properties from another entity to this entity
        /// </summary>
        /// <param name="entity">Entity from which to copy properties</param>
        private void CopyEntityToThis(IsolatedStorageOfflineEntity entity)
        {
            PropertyInfo[] propInfos = GetEntityProperties();

            object [] parameters = new object[]{};
            foreach (PropertyInfo propInfo in propInfos)
            {
                propInfo.GetSetMethod().Invoke(this, new object[] { propInfo.GetGetMethod().Invoke(entity, parameters) });
            }

            CopyODataPropertiesToThis(entity);
            this._entityMetadata.IsTombstone = entity._entityMetadata.IsTombstone;
        }

        /// <summary>
        /// Copies OData properties that don't need to be snapshoted
        /// </summary>
        /// <param name="entity">Entity from which to copy properties</param>
        private void CopyODataPropertiesToThis(IsolatedStorageOfflineEntity entity)
        {
            this._entityMetadata.Id = entity._entityMetadata.Id;
            this._entityMetadata.EditUri = entity._entityMetadata.EditUri;
            this._entityMetadata.ETag = entity._entityMetadata.ETag;
        }
        
        /// <summary>
        /// Creates a snapshot of an entity.
        /// </summary>
        private void CreateSnapshot()
        {
            if (EntityState == OfflineEntityState.Unmodified ||
                EntityState == OfflineEntityState.Saved)
            {
                if (_original == null && _trackChanges)
                {
                    lock (_syncRoot)
                    {
                        if (_original == null && _trackChanges)
                        {
                            OfflineEntitySnapshot snapshot = GetSnapshotFromEntity(this);
                            _original = snapshot;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Uses the snapshot to fill the entity's properties
        /// </summary>
        /// <param name="entity">Entity to fill</param>
        internal void FillEntityFromSnapshot(IsolatedStorageOfflineEntity entity)
        {
            Type type = entity.GetType();
            foreach (var property in _original.Properties)
            {
                PropertyInfo propInfo = type.GetProperty(property.Key);

                // If propInfo is null, it's an internal property
                // that will be handled later
                if (propInfo != null)
                {
                    propInfo.GetSetMethod().Invoke(entity, new object[] { property.Value });
                }
            }

            // Get the internal properties
            entity._entityMetadata = _original.Metadata;
            entity.EntityState = _original.EntityState;
            entity.TickCount = _original.TickCount;
        }

        /// <summary>
        /// Sets the metadata for the entity and does any notification.
        /// The property setter asserts on whether or not the entity is attached, but this
        /// method does not
        /// </summary>
        /// <param name="metadata">Metadata to set</param>
        internal void SetServiceMetadata(OfflineEntityMetadata metadata)
        {
            if (metadata != _entityMetadata)
            {
                _entityMetadata = metadata;
                RaisePropertyChanged("EntityMetadata");
            }
        }

        /// <summary>
        /// Copies the properties from the entity to the snapshot
        /// </summary>
        /// <param name="entity">Entity from which to copy properties</param>        
        private OfflineEntitySnapshot GetSnapshotFromEntity(IsolatedStorageOfflineEntity entity)
        {
            OfflineEntitySnapshot snapshot = new OfflineEntitySnapshot();
            PropertyInfo[] properties = GetEntityProperties();

            // Copy data properties
            foreach (PropertyInfo property in properties)
            {
                object val = property.GetGetMethod().Invoke(entity, null);
                snapshot.Properties[property.Name] = val;
            }

            snapshot.TickCount = entity.TickCount;
            snapshot.EntityState = entity.EntityState;
            snapshot.Metadata = entity.ServiceMetadata.Clone();

            return snapshot;
        }

        /// <summary>
        /// Converts an entity to a tombstone and tracks the change.  It also converts all properties which aren't
        /// part of the key to null/default.
        /// </summary>
        internal void DeleteItem()
        {
            lock (_syncRoot)
            {
                CreateSnapshot();

                _entityMetadata.IsTombstone = true;
                _state = OfflineEntityState.Modified;

                // Clear out all properties
                PropertyInfo[] properties = GetEntityNonKeyProperties();
                foreach (PropertyInfo property in properties)
                {
                    property.GetSetMethod().Invoke(this, new object[] { null });
                }
            }

            // Notify in case there was any binding
            RaisePropertyChanged("ServiceMetadata");
        }

        internal void ApplyDelete()
        {
            lock (_syncRoot)
            {
                EntityState = OfflineEntityState.Detached;
                IsTombstone = true;
                
                // Clear out all properties
                PropertyInfo[] properties = GetEntityNonKeyProperties();
                foreach (PropertyInfo property in properties)
                {
                    property.GetSetMethod().Invoke(this, new object[] { null });
                }
            }
        }

        /// <summary>
        /// Returns the entity state for the snapshot.
        /// </summary>
        internal OfflineEntityState SnapshotState
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_original != null)
                    {
                        return _original.EntityState;
                    }
                }

                return OfflineEntityState.Detached;
            }

            set
            {
                _original.EntityState = value;
            }
        }

        /// <summary>
        /// Creates a new entity by copying the properties.
        /// </summary>
        /// <returns>The cloned entity</returns>
        internal IsolatedStorageOfflineEntity Clone()
        {
            return (IsolatedStorageOfflineEntity)this.MemberwiseClone();
        }

        /// <summary>
        /// Returns all properties which are not keys
        /// </summary>
        /// <returns>Array of properties which are not keys</returns>
        private PropertyInfo[] GetEntityNonKeyProperties()
        {
            return (from p in GetEntityProperties()
                    where p.GetCustomAttributes(typeof(KeyAttribute), false).Length == 0
                    select p).ToArray();
        }

        /// <summary>
        /// Returns all properties which are keys for the entity
        /// </summary>
        /// <returns>Array of properties which are keys</returns>
        private PropertyInfo[] GetEntityKeyProperties()
        {
            return GetEntityKeyProperties(base.GetType());
        }

        /// <summary>
        /// Returns all properties of the entity which are passed for sync (all properties which have 
        /// getters and setters).
        /// </summary>
        /// <returns>Array of entity properties</returns>
        private PropertyInfo[] GetEntityProperties()
        {
            return GetEntityProperties(base.GetType());
        }

        /// <summary>
        /// Returns all properties of the specified type which are passed for sync (all properties which have 
        /// getters and setters).
        /// </summary>
        /// <param name="t">Type from which to retrieve properties</param>
        /// <returns>Properties for the type</returns>
        internal static PropertyInfo[] GetEntityProperties(Type t)
        {
            return (from p in t.GetProperties(SnapshotBindingFlags)
                    where p.GetGetMethod() != null && p.GetSetMethod() != null && p.DeclaringType == t
                    select p).ToArray();
        }

        /// <summary>
        /// Returns all properties of the specified type which are keys for the type
        /// </summary>
        /// <param name="t">Type from which to retrieve properties</param>
        /// <returns>Key propeties for the type.</returns>
        internal static PropertyInfo[] GetEntityKeyProperties(Type t)
        {
            return (from p in GetEntityProperties(t)
                    where p.GetCustomAttributes(typeof(KeyAttribute), false).Length != 0
                    select p).ToArray();
        }

        /// <summary>
        /// Notifies the PropertyChanged event if it is registered.
        /// </summary>
        /// <param name="propertyName">Name of the property for which the event is being raised.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Reflects on the keys for the entity and returns a representation of its key.
        /// </summary>
        /// <returns>The object which is the key for the entity</returns>
        internal object GetIdentity()
        {
            OfflineEntityKey key = new OfflineEntityKey();

            PropertyInfo[] propInfos = GetEntityKeyProperties();

            foreach (PropertyInfo propInfo in propInfos)
            {
                key.AddKey(propInfo.Name, propInfo.GetGetMethod().Invoke(this, new object[] { }));
            }

            return key;
        }

        /// <summary>
        /// When resolving store conflicts in favor of the modified entity, this is called to update
        /// tick counts
        /// </summary>
        internal void UpdateModifiedTickCount()
        {
            lock (_syncRoot)
            {
                if (_original != null)
                {
                    this.TickCount = _original.TickCount;
                }
            }
        }

        /// <summary>
        /// Returns whether or not the entity has a snapshot.
        /// </summary>
        internal bool HasSnapshot
        {
            get
            {
                lock (_syncRoot)
                {
                    return _original != null;
                }
            }
        }

        /// <summary>
        /// Returns the object used to lock the entity
        /// </summary>
        internal object SyncRoot
        {
            get
            {
                return _syncRoot;
            }
        }

        /// <summary>
        /// Stores the snapshot of the item.  Original is probably a bad name as it is really
        /// the last known committed version of the entity, whether it was saved or received
        /// from the service.
        /// 
        /// This member is set when a property is modified (OnPropertyChanged).
        /// It is cleared when the changes are either cancelled or accepted.
        /// It is updated when it exists (the entity is modified) and a version for the entity
        /// is received from the service.
        /// </summary>
        OfflineEntitySnapshot _original;

        /// <summary>
        /// Specifies the current state of the entity.  It is updated based on various actions
        /// performed on the entity.  See the OfflineEntityState enum definition for descriptions
        /// of the states.
        /// </summary>
        OfflineEntityState _state;

        /// <summary>
        /// Specifies whether or not the entity is read-only.  It is not enforced, but is mainly
        /// used as information for clients.  When GetOriginal is called and the object is constructed
        /// and returned, _isReadOnly is set to true.
        /// </summary>
        bool _isReadOnly = false;

        /// <summary>
        /// Stores the information that must be persisted for OData.  The most important attributes
        /// are tombstone and id.
        /// </summary>
        OfflineEntityMetadata _entityMetadata;

        /// <summary>
        /// Stores the last sync error and sync conflict that was seen for the entity.  It is updated when
        /// conflicts and errors are received from the store, and when sync conflicts and errors are cleared
        /// by the app.
        /// </summary>
        SyncErrorInfo _syncInfo;

        /// <summary>
        /// Specifies whether or not changes should be track and a snapshot is created when properties are modified
        /// Setting this to false ensures that a snapshot will not be created and the state will not be set
        /// to modified if a property is updated.  This is used when applying updates from the service, as they
        /// should not result in state changes or a snapshot being created.
        /// </summary>
        bool _trackChanges = true;

        /// <summary>
        /// Used to synchronize the lifetime of the _original object.  This is how concurrent access to the entity
        /// is managed, since it is more performant that managing access through every property set, particularly
        /// when the entity is being deserialized.
        /// </summary>
        object _syncRoot = new object();

        /// <summary>
        /// The current store conflict for the item. It is set when the application attempts to save changes
        /// for which there are store conflicts.
        /// </summary>
        StoreConflict _storeConflict;

        /// <summary>
        /// Binding flags used for the reflection calls to look up properties for copying from sync.
        /// </summary>
        const BindingFlags SnapshotBindingFlags = BindingFlags.Public | BindingFlags.Instance;
    }
}
