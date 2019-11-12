// <copyright file="OOBWindowAdapterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OOBWindowAdapterFixture.cs                     
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
    using System.Windows.Controls;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Windows;

    public class OOBWindowAdapterFixture
    {
        private Panel rootPanel;

        [TestInitialize]
        public void TestInitialize()
        {
            this.rootPanel = new Canvas();
        }

        [TestMethod]
        public void ShouldShowNonModalWindowWhenShowIsCalled()
        {
            IWindow window = this.CreateAdapter();

            Assert.IsFalse(window.IsOpen);

            window.Show(this.rootPanel);

            Assert.IsTrue(window.IsOpen);
            Assert.IsFalse(window.IsModal);
        }

        private IWindow CreateAdapter()
        {
            return new OOBWindowAdapter();
        }
    }
}
