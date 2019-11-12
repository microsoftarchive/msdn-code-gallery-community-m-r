// <copyright file="TitleAssetFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitleAssetFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Models
{
    using System;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="TitleAsset"/>.
    /// </summary>
    [TestClass]
    public class TitleAssetFixture
    {
        /// <summary>
        /// Should raise OnPropertyChanged event when main text is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenMainTextIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var titleAsset = new TitleAsset { MainText = string.Empty };
            titleAsset.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            titleAsset.MainText = "NewText";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("MainText", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when sub text is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSubTextIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var titleAsset = new TitleAsset { SubText = string.Empty };
            titleAsset.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            titleAsset.SubText = "SubText";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SubText", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests the cloning of the title.
        /// </summary>
        [TestMethod]
        public void ShouldGetANewTitleAssetWhenSameValuesWhenCallingToClone()
        {
            var titleAsset = new TitleAsset
             {
                 MainText = "MainText",
                 SubText = "SubText",
                 Source = new Uri("http://contoso.com"),
             };

            var newTitleAsset = titleAsset.Clone();

            Assert.AreEqual(titleAsset.Id, newTitleAsset.Id);
            Assert.AreEqual(titleAsset.MainText, newTitleAsset.MainText);
            Assert.AreEqual(titleAsset.SubText, newTitleAsset.SubText);
            Assert.AreEqual(titleAsset.Source, newTitleAsset.Source);
            Assert.AreNotSame(titleAsset, newTitleAsset);
        }
    }
}
