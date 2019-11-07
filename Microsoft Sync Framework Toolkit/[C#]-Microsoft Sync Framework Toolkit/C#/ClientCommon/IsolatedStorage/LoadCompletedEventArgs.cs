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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// Event args passed to the LoadCompletedEventHandler when LoadAsync completes
    /// </summary>
    public class LoadCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor which takes the exception that occurred when loading.  The exception
        /// can be null if no exception occurred
        /// </summary>
        /// <param name="e">Exception which occurred during loading (can be null).</param>
        public LoadCompletedEventArgs(Exception e)
        {
            this._exception = e;
        }

        /// <summary>
        /// The exception that occurred during execution of LoadAsync.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }

        Exception _exception;
    }
}
