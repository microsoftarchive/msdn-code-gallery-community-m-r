// <copyright file="AssetDataTemplateSelector.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetDataTemplateSelector.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Controls
{
    using System.Windows;
    using Models;

    /// <summary>
    /// <see cref="DataTemplate"/> selector for the given class.
    /// </summary>
    public class AssetDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> for <see cref="VideoAsset"/>.
        /// </summary>
        /// <value>The <see cref="VideoAsset"/> data template.</value>
        public DataTemplate VideoAssetDataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> for <see cref="ImageAsset"/>.
        /// </summary>
        /// <value>The <see cref="ImageAsset"/> data template.</value>
        public DataTemplate ImageAssetDataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> for <see cref="AudioAsset"/>.
        /// </summary>
        /// <value>The <see cref="AudioAsset"/> data template.</value>
        public DataTemplate AudioAssetDataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> for <see cref="FolderAsset"/>.
        /// </summary>
        /// <value>The <see cref="FolderAsset"/> data template.</value>
        public DataTemplate FolderAssetDataTemplate { get; set; }

        /// <summary>
        /// Selects the appropiate template for the given item.
        /// </summary>
        /// <param name="item">The item for which <see cref="DataTemplate"/> is to be selected.</param>
        /// <param name="container">The container.</param>
        /// <returns>
        /// The <see cref="DataTemplate"/> for the given item.
        /// </returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                if (item is VideoAsset)
                {
                    return this.VideoAssetDataTemplate;
                }

                if (item is ImageAsset)
                {
                    return this.ImageAssetDataTemplate;
                }

                if (item is AudioAsset)
                {
                    return this.AudioAssetDataTemplate;
                }

                if (item is FolderAsset)
                {
                    return this.FolderAssetDataTemplate;
                }
            }

            return null;
        }
    }
}