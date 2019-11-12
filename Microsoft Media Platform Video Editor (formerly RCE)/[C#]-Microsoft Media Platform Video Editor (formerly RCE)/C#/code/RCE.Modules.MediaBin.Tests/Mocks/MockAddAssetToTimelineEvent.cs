// <copyright file="MockAddAssetToTimelineEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IAggregateMediaModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Mock class of AddAssetToTimelineEvent class.
    /// </summary>
    public class MockAddAssetToTimelineEvent : AddAssetToTimelineEvent
    {
        public bool PublishCalled { get; set; }

        public Asset Asset { get; set; }

        /// <summary>
        /// Override the publish method of the composite event and 
        /// set the PublishCalled flag and Asset.
        /// </summary>
        /// <param name="asset">Asset to be published.</param>
        public override void Publish(Asset asset)
        {
            this.PublishCalled = true;
            this.Asset = asset;
        }
    }
}
