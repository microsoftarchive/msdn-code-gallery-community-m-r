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

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using System;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    /// <summary>
    /// Mock to test the DownloadProgressChangedEvent event.
    /// </summary>
    public class MockDownloadProgressChangedEvent : DownloadProgressChangedEvent
    {
        /// <summary>
        /// Gets or sets the subscribe argument action.
        /// </summary>
        /// <value>The subscribe argument action.</value>
        public Action<AssetDownloadProgressEventArgs> SubscribeArgumentAction { get; set; }

        /// <summary>
        /// Gets or sets the subscribe argument filter.
        /// </summary>
        /// <value>The subscribe argument filter.</value>
        public Predicate<AssetDownloadProgressEventArgs> SubscribeArgumentFilter { get; set; }

        /// <summary>
        /// Gets or sets the subscribe argument thread option.
        /// </summary>
        /// <value>The subscribe argument thread option.</value>
        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        /// <summary>
        /// Subscribes a delegate to an event.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="threadOption">Specifies on which thread to receive the delegate callback.</param>
        /// <param name="keepSubscriberReferenceAlive">When <see langword="true"/>, the <seealso cref="T:Microsoft.Practices.Composite.Presentation.Events.CompositePresentationEvent`1"/> keeps a reference to the subscriber so it does not get garbage collected.</param>
        /// <param name="filter">Filter to evaluate if the subscriber should receive the event.</param>
        /// <returns>
        /// A <see cref="T:Microsoft.Practices.Composite.Events.SubscriptionToken"/> that uniquely identifies the added subscription.
        /// </returns>
        /// <remarks>
        /// If <paramref name="keepSubscriberReferenceAlive"/> is set to <see langword="false"/>, <see cref="T:Microsoft.Practices.Composite.Presentation.Events.CompositePresentationEvent`1"/> will maintain a <seealso cref="T:System.WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate.
        /// If not using a WeakReference (<paramref name="keepSubscriberReferenceAlive"/> is <see langword="true"/>), the user must explicitly call Unsubscribe for the event when disposing the subscriber in order to avoid memory leaks or unexepcted behavior.
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public override SubscriptionToken Subscribe(Action<AssetDownloadProgressEventArgs> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<AssetDownloadProgressEventArgs> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}