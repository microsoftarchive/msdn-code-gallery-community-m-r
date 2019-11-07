// <copyright file="OOBWindowManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OOBWindowManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows
{
    using System;

    public class OOBWindowManager : IWindowManager
    {
        public IWindow CreateWindow()
        {
            return new OOBWindowAdapter();
        }

        public IWindow GetWindowWithFocus()
        {
            throw new NotImplementedException();
        }

        public void PersistProperty(string windowName, string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public object RecoverProperty(string windowName, string propertyName)
        {
            throw new NotImplementedException();
        }

        public void RemoveProperty(string windowName, string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool ShouldDisplayWindow(string toString, bool defaultValue)
        {
            throw new NotImplementedException();
        }
    }
}
