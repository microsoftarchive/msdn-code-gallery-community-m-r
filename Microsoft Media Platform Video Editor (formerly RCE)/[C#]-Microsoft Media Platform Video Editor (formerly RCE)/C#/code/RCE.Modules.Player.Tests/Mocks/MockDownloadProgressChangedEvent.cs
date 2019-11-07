// <copyright file="MockDownloadProgressChangedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDownloadProgressChangedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using Infrastructure.Events;

    /// <summary>
    /// Mock for the unit testing of DownloadProgressChangedEvent event.
    /// </summary>
    public class MockDownloadProgressChangedEvent : DownloadProgressChangedEvent
    {
        /// <summary>
        /// Gets or sets a value indicating whether [publish called].
        /// </summary>
        /// <value><c>true</c> if [publish called]; otherwise, <c>false</c>.</value>
        public bool PublishCalled { get; set; }

        /// <summary>
        /// Gets or sets the publish argument payload.
        /// </summary>
        /// <value>The publish argument payload.</value>
        public AssetDownloadProgressEventArgs PublishArgumentPayload { get; set; }

        /// <summary>
        /// Gets or sets the publish called event.
        /// </summary>
        /// <value>The publish called event.</value>
        public EventHandler PublishCalledEvent { get; set; }

        /// <summary>
        /// Publishes the <see cref="T:Microsoft.Practices.Composite.Presentation.Events.CompositePresentationEvent`1"/>.
        /// </summary>
        /// <param name="payload">Message to pass to the subscribers.</param>
        public override void Publish(AssetDownloadProgressEventArgs payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload;
        }
    }
}