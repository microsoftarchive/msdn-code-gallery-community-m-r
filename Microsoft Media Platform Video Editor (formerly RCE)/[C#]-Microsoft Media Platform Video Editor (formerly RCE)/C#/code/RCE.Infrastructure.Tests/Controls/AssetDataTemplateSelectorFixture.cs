// <copyright file="AssetDataTemplateSelectorFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetDataTemplateSelectorFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Controls
{
    using System.Windows;
    using Infrastructure.Controls;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="AssetDataTemplateSelector"/>.
    /// </summary>
    [TestClass]
    public class AssetDataTemplateSelectorFixture
    {
        /// <summary>
        /// Should return null for an unknown item.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullForAnUnknownItem()
        {
            var assetDataTemplateSelector = new AssetDataTemplateSelector();

            var result = assetDataTemplateSelector.SelectTemplate(new object(), null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Should return VideoAsset data template for <see cref="VideoAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldReturnVideoAssetDataTemplateForAVideoAsset()
        {
            var assetDataTemplateSelector = new AssetDataTemplateSelector();

            var dataTemplate = new DataTemplate();

            assetDataTemplateSelector.VideoAssetDataTemplate = dataTemplate;

            var result = assetDataTemplateSelector.SelectTemplate(new VideoAsset(), null);

            Assert.AreEqual(dataTemplate, result);
        }

        /// <summary>
        /// Should return FolderAsset data template for <see cref="FolderAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFolderAssetDataTemplateForAFolderAsset()
        {
            var assetDataTemplateSelector = new AssetDataTemplateSelector();

            var dataTemplate = new DataTemplate();

            assetDataTemplateSelector.FolderAssetDataTemplate = dataTemplate;

            var result = assetDataTemplateSelector.SelectTemplate(new FolderAsset(), null);

            Assert.AreEqual(dataTemplate, result);
        }

        /// <summary>
        /// Should return AudioAsset data template for <see cref="AudioAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldReturnAudioAssetDataTemplateForAnAudioAsset()
        {
            var assetDataTemplateSelector = new AssetDataTemplateSelector();

            var dataTemplate = new DataTemplate();

            assetDataTemplateSelector.AudioAssetDataTemplate = dataTemplate;

            var result = assetDataTemplateSelector.SelectTemplate(new AudioAsset(), null);

            Assert.AreEqual(dataTemplate, result);
        }

        /// <summary>
        /// Should return ImageAsset data template for <see cref="ImageAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldReturnImageAssetDataTemplateForAnImageAsset()
        {
            var assetDataTemplateSelector = new AssetDataTemplateSelector();

            var dataTemplate = new DataTemplate();

            assetDataTemplateSelector.ImageAssetDataTemplate = dataTemplate;

            var result = assetDataTemplateSelector.SelectTemplate(new ImageAsset(), null);

            Assert.AreEqual(dataTemplate, result);
        }
    }
}
