// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Behaviors;

namespace StockTraderRI.Infrastructure.Tests.Behaviors
{
    [TestClass]
    public class WindowWrapperFixture
    {
        private bool _wasClosed;

        [TestMethod]
        public void ShouldCreateWindowWrapperAndSetProperties()
        {
            var a = new WindowDialogActivationBehavior();

            var windowWrapper = new WindowWrapper();
            var style = new Style();
            windowWrapper.Style = style;
            Assert.AreEqual(style, windowWrapper.Style);

            var content = new Grid();
            windowWrapper.Content = content;
            Assert.AreEqual(content, windowWrapper.Content);
            
            var owner = new Window();
            owner.Show();
            windowWrapper.Owner = owner;
            Assert.AreEqual(owner, windowWrapper.Owner);

            windowWrapper.Show();
            windowWrapper.Closed += WindowWrapperOnClosed;
            windowWrapper.Close();
            windowWrapper.Closed -= WindowWrapperOnClosed;
            Assert.IsTrue(this._wasClosed);

        }

        private void WindowWrapperOnClosed(object sender, EventArgs eventArgs)
        {
             this._wasClosed = true;
        }
    }
}
