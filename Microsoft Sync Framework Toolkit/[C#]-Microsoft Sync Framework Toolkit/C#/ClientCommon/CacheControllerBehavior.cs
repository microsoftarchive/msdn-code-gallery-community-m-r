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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// CacheControllerBehavior Class
    /// </summary>
    public class CacheControllerBehavior
    {
        bool _locked;
        object _lockObject = new object();
        List<Type> _knownTypes;
        ICredentials _credentials;
        SerializationFormat _serFormat;
        string _scopeName;
        Action<HttpWebRequest, Action<HttpWebRequest>> _beforeSendingRequestHandler;
        Action<HttpWebResponse> _afterSendingResponse;
        Dictionary<string, string> _scopeParameters;

        /// <summary>
        /// Represents the list of types that will be referenced when deserializing entities from the sync service.
        /// Default behavior is all public types deriving from IOfflineEntity is searched in all loaded assemblies.
        /// When this list is non empty the search is narrowed down to this list.
        /// </summary>
        public ReadOnlyCollection<Type> KnownTypes
        {
            get { return new ReadOnlyCollection<Type>(this._knownTypes); }
        }

        /// <summary>
        /// A enumerator that lets uses browse the list of specified scope level filter parameters.
        /// </summary>
        public Dictionary<string,string>.Enumerator ScopeParameters
        {
            get
            {
                return this._scopeParameters.GetEnumerator();
            }
        }

        /// <summary>
        /// Credentials that will be passed along with each outgoing WebRequest.
        /// </summary>
        public ICredentials Credentials
        {
            get
            {
                return this._credentials;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                lock (this._lockObject)
                {
                    CheckLockState();
                    this._credentials = value;
                }
            }
        }

        /// <summary>
        /// Represents the SerializatonFormat to be used for the network payload.
        /// </summary>
        public SerializationFormat SerializationFormat
        {
            get
            {
                return this._serFormat;
            }

            set
            {
                lock (this._lockObject)
                {
                    CheckLockState();
                    this._serFormat = value;
                }
            }
        }

        /// <summary>
        /// Returns the Sync scope name
        /// </summary>
        public string ScopeName
        {
            get
            {
                return this._scopeName;
            }

            internal set
            {
                this._scopeName = value;
            }
        }

        /// <summary>
        /// Delegate to be notified before each WebRequest is sent to the sync service.
        /// User need to call the resumption delegate param when done with custom processing.
        /// </summary>        
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public Action<HttpWebRequest, Action<HttpWebRequest>> BeforeSendingRequest
        {
            get
            {
                return this._beforeSendingRequestHandler;
            }

            set
            {
                lock (this._lockObject)
                {
                    CheckLockState();
                    this._beforeSendingRequestHandler = value;
                }
            }
        }

        /// <summary>
        /// Delegate that will be invoked after receiving an response from the sync service.
        /// </summary>
        public Action<HttpWebResponse> AfterReceivingResponse
        {
            get
            {
                return this._afterSendingResponse;
            }

            set
            {
                lock (this._lockObject)
                {
                    CheckLockState();
                    this._afterSendingResponse = value;
                }
            }
        }

        /// <summary>
        /// Adds an Type to the collection of KnownTypes.
        /// </summary>
        /// <typeparam name="T">Type to include in search</typeparam>
        public void AddType<T>() where T : IOfflineEntity
        {
            lock (this._lockObject)
            {
                CheckLockState();
                this._knownTypes.Add(typeof(T));
            }
        }

        /// <summary>
        /// Function that users will use to add any custom scope level filter parameters and their values.
        /// </summary>
        /// <param name="key">parameter name as string</param>
        /// <param name="value">parameter value as string</param>
        public void AddScopeParameters(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key cannot be empty", "key");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            lock (this._lockObject)
            {
                CheckLockState();
                this._scopeParameters.Add(key, value);
            }
        }

        private void CheckLockState()
        {
            if (this._locked)
            {
                throw new CacheControllerException("Cannot modify CacheControllerBehavior when sync is in progress.");
            }
        }

        internal Dictionary<string, string> ScopeParametersInternal
        {
            get
            {
                return this._scopeParameters;
            }
        }

        internal bool Locked
        {
            set
            {
                lock (this._lockObject)
                {
                    this._locked = value;
                }
            }
        }

        internal CacheControllerBehavior()
        {
            this._knownTypes = new List<Type>();
            this._serFormat = SerializationFormat.ODataAtom;
            this._scopeParameters = new Dictionary<string, string>();
        }
    }
}
