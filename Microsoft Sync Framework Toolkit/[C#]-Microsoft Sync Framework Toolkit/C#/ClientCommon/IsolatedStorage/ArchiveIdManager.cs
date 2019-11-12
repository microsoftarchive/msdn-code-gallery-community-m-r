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


namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// This class manages object identities during archiving.  It guarantees that an object won't be
    /// written to an archive file more than once.  Because an object can have either a primary key,
    /// or an atom id, or both, it manages the relationships between these.  The contract is that
    /// the calling code must call ProcessedEntity after each entity it encounters, whether it is
    /// written to the archive file or not.
    /// </summary>
    class ArchiveIdManager
    {
        public ArchiveIdManager()
        {
            _atomIdSet = new Set<string>();
            _pkeySet = new Set<OfflineEntityKey>();
        }

        public bool ContainsEntity(IsolatedStorageOfflineEntity entity)
        {
            // If the entity is a tombstone, use the atom id
            if (entity.IsTombstone)
            {
                if (String.IsNullOrEmpty(entity.ServiceMetadata.Id))
                {
                    // if it's a tombstone and the id is null, it means it is a delete of
                    // a local insert that can be skipped, so we report it as already written
                    return true;
                }
                else
                {
                    return _atomIdSet.Contains(entity.ServiceMetadata.Id);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(entity.ServiceMetadata.Id))
                {
                    return _atomIdSet.Contains(entity.ServiceMetadata.Id);
                }

                OfflineEntityKey key = entity.GetIdentity() as OfflineEntityKey;
                key.TypeName = entity.GetType().FullName;

                if (_pkeySet.Contains(key))
                {
                    return true;
                }

                return false;
            }
        }

        public void ProcessedEntity(IsolatedStorageOfflineEntity entity)
        {
            string atomId = entity.ServiceMetadata.Id;
            OfflineEntityKey key = entity.GetIdentity() as OfflineEntityKey;
            key.TypeName = entity.GetType().FullName;
            
            if (entity.IsTombstone)
            {
                if (String.IsNullOrEmpty(atomId))
                {
                    _pkeySet.Add(key);
                }
                else
                {
                    _atomIdSet.Add(atomId);
                }
            }
            else
            {
                _pkeySet.Add(key);

                if (!String.IsNullOrEmpty(atomId))
                {
                    _atomIdSet.Add(atomId);
                }
            }
        }


        /// <summary>
        /// This class keeps track of a unique set of items of type T.
        /// </summary>
        /// <typeparam name="T">The type of item for which to store a unique set.</typeparam>
        class Set<T>
        {
            public Set()
            {
                _dictionary = new Dictionary<T, bool>();
            }

            public void Add(T t)
            {
                _dictionary[t] = false;
            }

            public bool Contains(T t)
            {
                return _dictionary.ContainsKey(t);
            }

            // bool is used here because it uses less space.
            Dictionary<T, bool> _dictionary;
        }

        Set<string> _atomIdSet;
        Set<OfflineEntityKey> _pkeySet;
    }


}
