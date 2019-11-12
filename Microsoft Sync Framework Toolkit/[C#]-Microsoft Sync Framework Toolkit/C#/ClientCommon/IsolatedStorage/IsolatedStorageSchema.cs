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
using System.Collections.ObjectModel;
using System.Reflection;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// This class is used to specify the schema used by the IsolatedStorageOfflineContext
    /// </summary>
    public class IsolatedStorageSchema
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public IsolatedStorageSchema()
        {
            _collections = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Adds a new collection for the type T.
        /// </summary>
        /// <typeparam name="T">Type of entity for the new collection</typeparam>
        public void AddCollection<T>() where T : IsolatedStorageOfflineEntity
        {
            Type t = typeof(T);

            if (IsolatedStorageOfflineEntity.GetEntityKeyProperties(t).Length == 0)
            {
                throw new ArgumentException("Type: " + t.FullName + " does not have a key specified");
            }

            _collections.Add(t.FullName, t);
        }

        //
        // POST LABS METHOD - Maintained for reference
        //public void AddCollection<T>(string collectionName, 
        //    IEnumerable<Type> subTypes) where T : IsolatedStorageOfflineEntity;

        /// <summary>
        /// Returns the list of types used for collections.
        /// </summary>
        public ReadOnlyCollection<Type> Collections
        { 
            get
            {
                return new ReadOnlyCollection<Type>(new List<Type>(_collections.Values));
            }

        }

        //
        // POST LABS METHOD
        //public IEnumerable<Type> GetTypes(Type t);

        Dictionary<string, Type> _collections;
    }
}
