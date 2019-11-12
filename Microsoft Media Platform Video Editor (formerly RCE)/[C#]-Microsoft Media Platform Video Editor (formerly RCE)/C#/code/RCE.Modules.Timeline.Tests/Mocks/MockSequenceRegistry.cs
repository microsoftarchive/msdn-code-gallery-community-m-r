// <copyright file="MockSequenceRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSequenceRegistry.cs                     
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
    using System.Collections.Generic;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class MockSequenceRegistry : ISequenceRegistry
    {
        public event EventHandler<DataEventArgs<ISequenceModel>> CurrentSequenceChanged;

        public ISequenceModel CurrentSequenceModel { get; set; }

        public Sequence CurrentSequence
        {
            get;
            set;
        }

        public ISequenceModel SequenceForSequenceModel { get; set; }

        public IEnumerable<ISequenceModel> Sequences { get; private set; }

        public Sequence CreateSequenceParameter { get; set; }

        public ISequenceModel CreateSequence(Sequence sequence)
        {
            this.InvokeCurrentSequenceChanged();
            foreach (var track in sequence.Tracks)
            {
                this.CurrentSequenceModel.Tracks.Add(track);
            }

            this.CreateSequenceParameter = sequence;
            return this.CurrentSequenceModel;
        }

        public ISequenceModel GetSequenceForTimeline(Sequence sequence)
        {
            return this.SequenceForSequenceModel;
        }

        public void InvokeCurrentSequenceChanged()
        {
            this.InvokeCurrentSequenceChanged(null);
        }

        public void InvokeCurrentSequenceChanged(ISequenceModel sequenceModel)
        {
            EventHandler<DataEventArgs<ISequenceModel>> handler = this.CurrentSequenceChanged;
            if (handler != null)
            {
                handler(this, new DataEventArgs<ISequenceModel>(sequenceModel));
            }
        }
    }
}
