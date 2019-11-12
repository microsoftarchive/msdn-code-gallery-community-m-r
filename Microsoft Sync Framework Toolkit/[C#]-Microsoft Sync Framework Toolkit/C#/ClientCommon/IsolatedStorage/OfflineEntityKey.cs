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
    /// Internal class used to represent the keys for entities.  This is used because it allows support for
    /// multiple key properties.
    /// </summary>
    internal class OfflineEntityKey
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public OfflineEntityKey()
        {
            _keys = new List<KeyValuePair<string, object>>();
            _type = null;
        }

        public OfflineEntityKey(string type)
            : this()
        {
            _type = type;
        }

        public string TypeName
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// Adds a key field with the specified key and value.  This will be inserted in sorted order in the key collection.
        /// </summary>
        /// <param name="key">Property name for the field</param>
        /// <param name="value">Value</param>
        public void AddKey(string key, object value)
        {
            int index = 0;

            int compare = -1;

            // Loop to figure out where the new value goes by comparing keys.
            while (index < _keys.Count &&
                (compare = string.Compare(key, _keys[index].Key)) > 0)
            {
                index++;
            }

            if (compare == 0)
            {
                throw new ArgumentException("An item with the same key has already been added");
            }

            // Insert at the specified index
            _keys.Insert(index, new KeyValuePair<string, object>(key, value));
        }

        /// <summary>
        /// Uses the keys and values to return a hash code.  This is so that different instances that have the same values
        /// hash correctly.
        /// </summary>
        /// <returns>Hash code for the key.</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;

            if (_type != null)
            {
                hashCode = _type.GetHashCode();
            }

            foreach (KeyValuePair<string, object> kvp in _keys)
            {
                hashCode ^= kvp.Key.GetHashCode();
                hashCode ^= kvp.Value.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Compares two OfflineEntityKey objects to determine if they are equal.  This must be implemented for the Dictionary.
        /// </summary>
        /// <param name="obj">Object to which to compare this object.</param>
        /// <returns>Whether or not this object equals the object passed in.</returns>
        public override bool Equals(object obj)
        {
            OfflineEntityKey other = obj as OfflineEntityKey;

            if (other != null)
            {
                if (_type != other._type)
                {
                    return false;
                }

                if (_keys.Count != other._keys.Count)
                {
                    return false;
                }

                // Loop over each key and value.  This is where the sorting comes in.
                for (int i = 0; i < _keys.Count; ++i)
                {
                    if (!_keys[i].Key.Equals(other._keys[i].Key))
                    {
                        return false;
                    }

                    if (!_keys[i].Value.Equals(other._keys[i].Value))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }


        private List<KeyValuePair<string, object>> _keys;

        // This is optional and is used to store the type of the entity being stored.  This is helpful when entities from
        // multiple types are being stored in the same collection 
        private string _type;
    }
}
