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

namespace RCE.Modules.Metadata.Tests.Mocks
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

        public IEnumerable<ISequenceModel> Sequences { get; private set; }

        public Sequence CreateSequenceParameter { get; set; }

        public ISequenceModel CreateSequence(Sequence sequence)
        {
            this.InvokeCurrentSequenceChanged();
            this.CreateSequenceParameter = sequence;
            return this.CurrentSequenceModel;
        }

        public ISequenceModel GetSequenceForTimeline(Sequence sequence)
        {
            throw new NotImplementedException();
        }

        public void InvokeCurrentSequenceChanged()
        {
            EventHandler<DataEventArgs<ISequenceModel>> handler = this.CurrentSequenceChanged;
            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}
