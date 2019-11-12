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

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// Custom exception for any CacheController related invalid operations.
    /// </summary>
    public class CacheControllerException : Exception
    {
        /// <summary>
        /// CacheControllerException ctor
        /// </summary>
        /// <param name="message"></param>
        public CacheControllerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// CacheControllerException ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public CacheControllerException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// CacheControllerException ctor
        /// </summary>
        public CacheControllerException()
            : base()
        { }

        internal static CacheControllerException CreateCacheBusyException()
        {
            return new CacheControllerException("Cannot complete WebRequest as another Refresh() operation is in progress.");
        }
    }
}
