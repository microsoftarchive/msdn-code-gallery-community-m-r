// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Represents the base interface that all offline cacheable object should derive from.
    /// </summary>
    public interface IOfflineEntity
    {
        /// <summary>
        /// Represents the sync and OData metadata used for the entity
        /// </summary>
        OfflineEntityMetadata ServiceMetadata { get; set; }
    }

    /// <summary>
    /// Class that represents the metadata required for the sync protocol to work correctly.
    /// Applications should not change these properties except when required by the protocol
    /// (the Id will change for an item that is inserted for the first time).
    /// The exception to this is the IsTombstone property, which should be set when the application
    /// is using a custom store and an item is being deleted.  Applications using the
    /// IsolatedStorageOfflineContext should never set any properties.
    /// </summary>
    public class OfflineEntityMetadata 
    {
        /// <summary>
        /// Public constructor for the metadata
        /// </summary>
        public OfflineEntityMetadata()
        {
            _isTombstone = false;
            _id = null;
            _etag = null;
            _editUri = null;
        }

        /// <summary>
        /// Public constructor for the metadata which takes parameters for the metadata.
        /// </summary>
        /// <param name="isTombstone">Whether or not the entity is a tombstone</param>
        /// <param name="id">Sync id for the item</param>
        /// <param name="etag">Item's OData ETag</param>
        /// <param name="editUri">Item's OData Edit Uri</param>
        public OfflineEntityMetadata(bool isTombstone, string id, string etag, Uri editUri)
        {
            this._isTombstone = isTombstone;
            this._id = id;
            this._etag = etag;
            this._editUri = editUri;
        }

        /// <summary>
        /// Whether or not the entity to which this item is attached is a tombstone
        /// </summary>
        public bool IsTombstone
        {
            get
            {
                return _isTombstone;
            }

            set
            {
                if (value != _isTombstone)
                {
                    _isTombstone = value;
                }
            }
        }

        /// <summary>
        /// The Id for the entity used for synchronization.  This should not be set by applications
        /// and should be empty when an item is first uploaded.  It will be subsequently filled in 
        /// during the upload response.
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (value != _id)
                {
                    _id = value;
                }
            }
        }

        /// <summary>
        /// The OData ETag for the item.  This should not be set by applications and should
        /// be empty during the first upload for an item.  It will subsequently be filled in
        /// after upload.
        /// </summary>
        public string ETag
        {
            get
            {
                return _etag;
            }

            set
            {
                if (value != _etag)
                {
                    _etag = value;
                }
            }

        }

        /// <summary>
        /// The OData edit Uri for the item.  This should not be set by applications and should
        /// be empty during the first upload for an item.  It will subsequently be filled in
        /// after upload.
        /// </summary>
        public Uri EditUri
        {
            get
            {
                return _editUri;
            }

            set
            {
                if (value != _editUri)
                {
                    _editUri = value;
                }
            }
        }

        /// <summary>
        /// Used while creating Snapshot to do a depp copy of the original copy's metadata
        /// </summary>
        /// <returns></returns>
        internal OfflineEntityMetadata Clone()
        {
            OfflineEntityMetadata metaData = new OfflineEntityMetadata(
                _isTombstone,
                _id,
                _etag,
                _editUri);

            return metaData;
        }

        private bool _isTombstone;
        private string _id;
        private string _etag;
        private Uri _editUri;
    }
}
