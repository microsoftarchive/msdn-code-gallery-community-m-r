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


namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// The various states an entity can have.  They are as follows:
    ///     Detached - The entity has been created outside of the context and has not been added.
    ///     Unmodified - The entity has been received or sent during sync and has not been modified since
    ///     Modified - The entity has been updated, created, or deleted, but not saved.
    ///     Saved - The entity has been modified and saved.
    /// </summary>
    public enum OfflineEntityState
    {
        /// <summary>
        /// State when the entity is deleted. The instance referred by the app is set to Detached
        /// </summary>
        Detached,
        /// <summary>
        /// Represents the state of the entity before its modified by the app
        /// </summary>
        Unmodified,
        /// <summary>
        /// Represents the state of the entity when its modified
        /// </summary>
        Modified,
        /// <summary>
        /// Represents the state of the entity after it is saved
        /// </summary>
        Saved
    }
}
