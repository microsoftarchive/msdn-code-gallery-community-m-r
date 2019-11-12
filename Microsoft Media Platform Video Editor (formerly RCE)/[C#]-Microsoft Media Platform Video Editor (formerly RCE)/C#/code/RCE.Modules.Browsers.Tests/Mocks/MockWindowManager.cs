// <copyright file="MockWindowManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockWindowManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Tests.Mocks
{
    using System;

    using RCE.Infrastructure.Windows;

    public class MockWindowManager : IWindowManager
    {
        public IWindow CreateWindow()
        {
            throw new NotImplementedException();
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
            return true;
        }
    }
}
