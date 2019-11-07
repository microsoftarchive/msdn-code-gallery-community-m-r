// <copyright file="CachedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CachedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Events
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    /// <summary>
    /// Defines a class that manages publication and subscription to <see cref="CachedEvent{TPayload}"/>.
    /// Base class for <see cref="RefreshElementsEvent"/>.
    /// </summary>
    /// <typeparam name="TPayload">The type of message that will be passed to the subscribers.</typeparam>
    public class CachedEvent<TPayload> : CompositePresentationEvent<TPayload>
    {
        /// <summary>
        /// List of payload objects.
        /// </summary>
        private readonly List<TPayload> payloadCache = new List<TPayload>();

        /// <summary>
        /// Gets a value indicating whether this instance has subscribers.
        /// </summary>
        /// <value>
        /// True if this instance has subscribers; otherwise, <c>false</c>.
        /// </value>
        private bool HasSubscribers
        {
            get { return this.Subscriptions.Count > 0; }
        }

        /// <summary>
        /// Publishes the <see cref="T:Microsoft.Practices.Composite.Presentation.Events.CompositePresentationEvent`1"/>.
        /// </summary>
        /// <param name="payload">Message to pass to the subscribers.</param>
        public override void Publish(TPayload payload)
        {
            this.payloadCache.Add(payload);
            this.FlushPendingPayloads();
        }

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
        public override SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TPayload> filter)
        {
            SubscriptionToken token = base.Subscribe(action, threadOption, keepSubscriberReferenceAlive, filter);
            this.FlushPendingPayloads();
            return token;
        }

        /// <summary>
        /// Flushes the pending payloads.
        /// </summary>
        public void FlushPendingPayloads()
        {
            if (!this.HasSubscribers)
            {
                return;
            }

            foreach (TPayload payload in this.payloadCache)
            {
                base.Publish(payload);
            }

            this.payloadCache.Clear();
        }
    }
}