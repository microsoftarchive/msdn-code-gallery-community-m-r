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
    /// Exception thrown when the SaveChanges call fails
    /// </summary>
    public class SaveFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the SaveFailedException class.
        /// </summary>
        /// <param name="conflicts">Store conflicts which are preventing SaveChanges from succeeding</param>
        internal SaveFailedException(IEnumerable<StoreConflict> conflicts)
            : base()
        {
            this._conflicts = conflicts;
        }

        /// <summary>
        /// Initializes a new instance of the SaveFailedException class.
        /// </summary>
        /// <param name="conflicts">Store conflicts which are preventing SaveChanges from succeeding</param>
        /// <param name="message">Message that describes the error.</param>
        public SaveFailedException(IEnumerable<StoreConflict> conflicts, string message)
            : base(message)
        {
            this._conflicts = conflicts;
        }

        /// <summary>
        /// Initializes a new instance of the SaveFailedException class.
        /// </summary>
        /// <param name="conflicts">Store conflicts which are preventing SaveChanges from succeeding</param>
        /// <param name="message">Message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        ///  (Nothing in Visual Basic) if no inner exception is specified. </param>
        public SaveFailedException(
            IEnumerable<StoreConflict> conflicts, 
            string message, 
            Exception innerException)
            : base(message, innerException)
        {
            this._conflicts = conflicts;
        }

        /// <summary>
        /// The list of StoreConflicts that occurred when SaveChanges was attempted.
        /// </summary>
        public IEnumerable<StoreConflict> Conflicts
        {
            get
            {
                return _conflicts;
            }
        }

        IEnumerable<StoreConflict> _conflicts;
    }
}
