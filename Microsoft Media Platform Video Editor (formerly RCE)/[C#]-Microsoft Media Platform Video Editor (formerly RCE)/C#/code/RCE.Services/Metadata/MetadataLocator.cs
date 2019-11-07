// <copyright file="MetadataLocator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataLocator.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Metadata
{
    using Contracts;

    /// <summary>
    /// Metadata locator that uses a set of strategies to try to get the metadata of an object.
    /// </summary>
    public class MetadataLocator : IMetadataLocator
    {
        /// <summary>
        /// Contains the strategies available to extract metadata.
        /// </summary>
        private readonly IMetadataStrategy[] metadataStrategies;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLocator"/> class.
        /// </summary>
        /// <param name="metadataStrategies">The available strategies.</param>
        public MetadataLocator(IMetadataStrategy[] metadataStrategies)
        {
            this.metadataStrategies = metadataStrategies;
        }

        /// <summary>
        /// Tries to get the metadata of the provided target using one of the available strategies.
        /// </summary>
        /// <param name="target">The object to get the metadata from.</param>
        /// <returns>The metadata of the provided target.</returns>
        public Metadata GetMetadata(object target)
        {
            Metadata metadata = null;

            if (this.metadataStrategies != null)
            {
                foreach (IMetadataStrategy metadataStrategy in this.metadataStrategies)
                {
                    if (metadataStrategy.CanRetrieveMetadata(target))
                    {
                        metadata = metadataStrategy.GetMetadata(target);
                        break;
                    }
                }
            }

            return metadata;
        }
    }
}