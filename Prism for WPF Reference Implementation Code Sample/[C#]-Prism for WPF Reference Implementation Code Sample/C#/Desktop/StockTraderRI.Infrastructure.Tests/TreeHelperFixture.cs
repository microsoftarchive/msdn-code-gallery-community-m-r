// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockTraderRI.Infrastructure.Tests
{
    [TestClass]
    public class TreeHelperFixture
    {
        [TestMethod]
        public void ShouldFindDirectAncestor()
        {
            ContentControl parent = new ContentControl();
            TextBlock child = new TextBlock();
            parent.Content = child;

            var foundParent = TreeHelper.FindAncestor(child, d => d == parent);

            Assert.IsNotNull(foundParent);
            Assert.AreSame(parent, foundParent);
        }

        [TestMethod]
        public void ShouldNotFindDirectAncestor()
        {
            ContentControl parent = new ContentControl();
            TextBlock child = new TextBlock();

            var foundParent = TreeHelper.FindAncestor(child, d => d == parent);

            Assert.IsNull(foundParent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullException()
        {
            ContentControl parent = new ContentControl();
            TextBlock child = new TextBlock();
            parent.Content = child;
            TreeHelper.FindAncestor(child, null);
        }

        [TestMethod]
        public void ShouldFindIndirectAncestor()
        {
            ContentControl grandParent = new ContentControl();
            ContentControl parent = new ContentControl();
            TextBlock child = new TextBlock();
            grandParent.Content = parent;
            parent.Content = child;

            var foundGrandParent = TreeHelper.FindAncestor(child, d => d == grandParent);

            Assert.IsNotNull(foundGrandParent);
            Assert.AreSame(grandParent, foundGrandParent);
        }
    }
}
