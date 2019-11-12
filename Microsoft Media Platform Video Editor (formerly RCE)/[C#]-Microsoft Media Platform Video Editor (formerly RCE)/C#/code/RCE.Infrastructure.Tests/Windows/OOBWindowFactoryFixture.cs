// <copyright file="OOBWindowFactoryFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OOBWindowFactoryFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Windows
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Windows;

    [TestClass]
    public class OOBWindowFactoryFixture
    {
        [TestMethod]
        public void ShouldCreateInstancesOfFloatableWindowAdapter()
        {
            IWindowManager windowManager = this.CreateFactory();

            IWindow window = windowManager.CreateWindow();

            Assert.IsInstanceOfType(window, typeof(OOBWindowAdapter));
        }

        private IWindowManager CreateFactory()
        {
            return new OOBWindowManager();
        }
    }
}