using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Samples.Synchronization.ClientServices;

namespace Microsoft.Samples.Synchronization.ClientServices.Formatters
{
    /// <summary>
    /// Abstract class for SyncReader that individual format readers needs to extend
    /// </summary>    
    abstract class SyncReader : IDisposable
    {
        protected XmlReader _reader;
        protected Stream _inputStream;
        protected Type[] _knownTypes;
        protected EntryInfoWrapper _currentEntryWrapper;
        protected ReaderItemType _currentType;
        protected bool _currentNodeRead = false;
        protected IOfflineEntity _liveEntity;

        protected SyncReader(Stream stream, Type[] knownTypes)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this._inputStream = stream;
            this._knownTypes = knownTypes;
        }

        public abstract void Start();
        public abstract ReaderItemType ItemType { get; }
        public abstract IOfflineEntity GetItem();
        public abstract byte[] GetServerBlob();
        public abstract bool GetHasMoreChangesValue();
        public abstract bool Next();

        /// <summary>
        /// Check to see if the current object that was just parsed had a conflict element on it or not.
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool HasConflict()
        {
            if (_currentEntryWrapper != null)
            {
                return this._currentEntryWrapper.ConflictWrapper != null;
            }
            return false;
        }

        /// <summary>
        /// Check to see if the current conflict object that was just parsed has a tempId element on it or not.
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool HasConflictTempId()
        {
            if (_currentEntryWrapper != null && _currentEntryWrapper.ConflictWrapper != null)
            {
                return this._currentEntryWrapper.ConflictWrapper.TempId != null;
            }
            return false;
        }

        /// <summary>
        /// Check to see if the current object that was just parsed has a tempId element on it or not.
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool HasTempId()
        {
            if (_currentEntryWrapper != null)
            {
                return this._currentEntryWrapper.TempId != null;
            }
            return false;
        }

        /// <summary>
        /// Returns the TempId parsed from the current object if present
        /// </summary>
        /// <returns>string</returns>
        public virtual string GetTempId()
        {
            if (!HasTempId())
            {
                return null;
            }

            return this._currentEntryWrapper.TempId;
        }

        /// <summary>
        /// Returns the TempId parsed from the current conflict object if present
        /// </summary>
        /// <returns>string</returns>
        public virtual string GetConflictTempId()
        {
            if (!HasConflictTempId())
            {
                return null;
            }

            return this._currentEntryWrapper.ConflictWrapper.TempId;
        }

        /// <summary>
        /// Get the conflict item
        /// </summary>
        /// <returns>Conflict item</returns>
        public virtual Conflict GetConflict()
        {
            if (!HasConflict())
            {
                return null;
            }

            Conflict conflict = null;

            if (_currentEntryWrapper.IsConflict)
            {
                conflict = new SyncConflict()
                {
                    LiveEntity = _liveEntity,
                    LosingEntity = ReflectionUtility.GetObjectForType(_currentEntryWrapper.ConflictWrapper, this._knownTypes),
                    Resolution = (SyncConflictResolution)Enum.Parse(FormatterConstants.SyncConflictResolutionType, _currentEntryWrapper.ConflictDesc, true)
                };
            }
            else
            {
                conflict = new SyncError()
                {
                    LiveEntity = _liveEntity,
                    ErrorEntity = ReflectionUtility.GetObjectForType(_currentEntryWrapper.ConflictWrapper, this._knownTypes),
                    Description = _currentEntryWrapper.ConflictDesc
                };
            }

            return conflict;
        }

        protected void CheckItemType(ReaderItemType type)
        {
            if (_currentType != type)
            {
                throw new InvalidOperationException(string.Format("{0} is not a valid {1} element.", _reader.Name, type));
            }

            _currentNodeRead = true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this._inputStream != null)
            {
                using (this._inputStream)
                {
                    this._inputStream.Close();
                }
            }
            this._inputStream = null;
            this._knownTypes = null;
        }

        #endregion
    }
}
