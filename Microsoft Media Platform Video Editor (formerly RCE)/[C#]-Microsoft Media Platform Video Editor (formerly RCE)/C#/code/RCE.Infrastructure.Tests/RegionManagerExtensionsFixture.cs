// <copyright file="RegionManagerExtensionsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RegionManagerExtensionsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests
{
    using System;
    using System.Linq;
    using Microsoft.Practices.Composite.Presentation.Regions;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="Infrastructure.RegionManagerExtensions"/>.
    /// </summary>
    [TestClass]
    public class RegionManagerExtensionsFixture
    {
        /// <summary>
        /// Tests if the views are added into the right order.
        /// </summary>
        [TestMethod]
        public void ShouldAddViewsInRightOrder()
        {
            var regionManager = new RegionManager();
            var region = new SingleActiveRegion();

            var view1 = new object();
            var view2 = new object();
            var view3 = new object();

            regionManager.Regions.Add("MyRegion", region);

            regionManager.RegisterViewWithRegionInIndex("MyRegion", view1, 0);
            regionManager.RegisterViewWithRegionInIndex("MyRegion", view2, 1);
            regionManager.RegisterViewWithRegionInIndex("MyRegion", view3, 2);

            Assert.AreEqual(3, region.Views.Count());
            Assert.AreEqual(view1, region.Views.ElementAt(0));
            Assert.AreEqual(view2, region.Views.ElementAt(1));
            Assert.AreEqual(view3, region.Views.ElementAt(2));
        }

        /// <summary>
        /// Tests if the views are added into the right order even if they are added in the 
        /// reverse order.
        /// </summary>
        [TestMethod]
        public void ShouldAddViewsInRightOrderEvenIfTheyAreAddedInReverseMode()
        {
            var regionManager = new RegionManager();
            var region = new SingleActiveRegion();

            var view1 = new object();
            var view2 = new object();
            var view3 = new object();

            regionManager.Regions.Add("MyRegion", region);

            regionManager.RegisterViewWithRegionInIndex("MyRegion", view1, 2);
            regionManager.RegisterViewWithRegionInIndex("MyRegion", view2, 1);
            regionManager.RegisterViewWithRegionInIndex("MyRegion", view3, 0);

            Assert.AreEqual(3, region.Views.Count());
            Assert.AreEqual(view1, region.Views.ElementAt(1));
            Assert.AreEqual(view2, region.Views.ElementAt(2));
            Assert.AreEqual(view3, region.Views.ElementAt(0));
        }

        /// <summary>
        /// Should add views in right order even if they are added in other order.
        /// </summary>
        [TestMethod]
        public void ShouldAddViewsInRightOrderEvenIfTheyAreAddedInOtherOrder()
        {
            var regionManager = new RegionManager();
            var region = new SingleActiveRegion();

            var view1 = new object();
            var view2 = new object();
            var view3 = new object();

            regionManager.Regions.Add("MyRegion", region);

            regionManager.RegisterViewWithRegionInIndex("MyRegion", view1, 1);
            regionManager.RegisterViewWithRegionInIndex("MyRegion", view2, 2);
            regionManager.RegisterViewWithRegionInIndex("MyRegion", view3, 0);

            Assert.AreEqual(3, region.Views.Count());
            Assert.AreEqual(view1, region.Views.ElementAt(1));
            Assert.AreEqual(view2, region.Views.ElementAt(2));
            Assert.AreEqual(view3, region.Views.ElementAt(0));
        }

        /// <summary>
        /// Should throw exception if index is less than 0.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrownExceptionIfIndexIsLessThan0()
        {
            var regionManager = new RegionManager();

            var region = new SingleActiveRegion();

            var view1 = new object();

            regionManager.Regions.Add("MyRegion", region);

            regionManager.RegisterViewWithRegionInIndex("MyRegion", view1, -1);
        }
    }
}
