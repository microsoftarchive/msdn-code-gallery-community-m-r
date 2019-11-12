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
using System.Text.RegularExpressions;

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// Contains contants used by the context
    /// </summary>
    internal static class Constants
    {
        // File name used for storing scopeName info
        public static readonly string SCOPE_INFO = "scopeinfo";

        // File name used for lock for the cache path
        public static readonly string LOCKFILE = "lock";

        private static readonly Regex regex = new Regex("^[a-fA-F0-9]{8}[.]((-?[0-9]+[.][CE])|([ADSU]))$");

        public static readonly long TIMER_INTERVAL = 5 * 60 * 1000;


        // Returns whether or not the requested file is one of the special files.
        public static bool SpecialFile(string fileName)
        {
            if (fileName == SCOPE_INFO)
            {
                return true;
            }
            else if (fileName == LOCKFILE)
            {
                return true;
            }

            return false;
        }

        // Returns whether or not the file name is one of ours.
        public static bool IsCacheFile(string fileName)
        {
            return regex.IsMatch(fileName);
        }
    }
}                                                                
