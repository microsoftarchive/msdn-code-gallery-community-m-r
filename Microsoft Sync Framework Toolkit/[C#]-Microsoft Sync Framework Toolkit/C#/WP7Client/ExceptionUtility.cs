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
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;


namespace Microsoft.Synchronization.ClientServices
{
    static class ExceptionUtility
    {
        internal static bool IsFatal(Exception exception)
        {
            while (exception != null)
            {
                if (exception is OutOfMemoryException || exception is ThreadAbortException || exception is AccessViolationException 
#if !WPCLIENT
                    || exception is SEHException
#endif
                )
                {
                    return true;
                }
                exception = exception.InnerException;
            }
            return false;
        }
    }
}
