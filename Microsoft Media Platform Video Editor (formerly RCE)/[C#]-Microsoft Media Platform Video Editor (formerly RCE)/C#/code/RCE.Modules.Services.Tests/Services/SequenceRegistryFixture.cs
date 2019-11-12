// <copyright file="SequenceRegistryFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequenceRegistryFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Services
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Services.Services;
    using RCE.Modules.Services.Tests.Mocks;

    using SMPTETimecode;

    [TestClass]
    public class SequenceRegistryFixture
    {
        private MockUnityContainer container;

        private SequenceModel sequenceModel;

        private Sequence sequence;

        private MockConfigurationService configurationService;

        private MockEventAggregator eventAggregator;

        private MockThumbnailEvent thumbnailEvent;

        [TestInitialize]
        public void TestInitialize()
        {
            this.container = new MockUnityContainer();
            this.configurationService = new MockConfigurationService();
            this.eventAggregator = new MockEventAggregator();
            this.thumbnailEvent = new MockThumbnailEvent();
            this.eventAggregator.AddMapping<ThumbnailEvent>(this.thumbnailEvent);
            this.sequenceModel = new SequenceModel(this.eventAggregator);
            
            this.sequence = new Sequence();

            this.container.Bag[typeof(ISequenceModel)] = this.sequenceModel;
        }

        [TestMethod]
        public void ShouldHaveZeroSequencesWhenCreated()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            Assert.AreEqual(0, sequenceRegistry.Sequences.Count());
        }

        [TestMethod]
        public void ShouldUseContainerToCreateSequences()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            Assert.AreEqual(0, this.container.ResolveCalls);

            var createdSequence = sequenceRegistry.CreateSequence(this.sequence);

            Assert.AreEqual(1, this.container.ResolveCalls);
            Assert.AreSame(this.sequenceModel, createdSequence);
        }

        [TestMethod]
        public void ShouldAddCreatedSequenceToSequences()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            Assert.AreEqual(0, sequenceRegistry.Sequences.Count());

            var createdSequence = sequenceRegistry.CreateSequence(this.sequence);

            Assert.AreEqual(1, sequenceRegistry.Sequences.Count());
            Assert.AreSame(this.sequenceModel, sequenceRegistry.Sequences.First());
            Assert.AreSame(createdSequence, sequenceRegistry.Sequences.First());
        }

        [TestMethod]
        public void ShouldMapTimelineModelToTimeline()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();
            
            var createdSequence = sequenceRegistry.CreateSequence(this.sequence);

            Assert.AreSame(createdSequence, sequenceRegistry.GetSequenceForTimeline(this.sequence));

            var anotherTimeline = new Sequence();

            var anotherSequence = sequenceRegistry.CreateSequence(anotherTimeline);

            Assert.AreSame(anotherSequence, sequenceRegistry.GetSequenceForTimeline(anotherTimeline));
        }

        [TestMethod]
        public void ShouldChangeCurrentSequenceWhenCreatingSequence()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            bool wasRaised = false;

            sequenceRegistry.CurrentSequenceChanged += (s, a) => { wasRaised = true; };
            Assert.IsFalse(wasRaised);

            sequenceRegistry.CreateSequence(this.sequence);
            Assert.IsTrue(wasRaised);
        }

        [TestMethod]
        public void ShouldRaiseCurrentSequenceChangedEventWhenCurrentSequenceChanges()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            bool wasRaised = false;
            sequenceRegistry.CurrentSequenceChanged += (s, a) => { wasRaised = true; };

            Assert.IsFalse(wasRaised);

            sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            Assert.IsTrue(wasRaised);
        }

        [TestMethod]
        public void ShouldPassPreviousSequenceAsSequenceChangedArgument()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            bool wasRaised = false;

            ISequenceModel eventSequence = null;

            ISequenceModel anotherSequence = new SequenceModel(new MockEventAggregator());

            sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            sequenceRegistry.CurrentSequenceChanged += (s, a) => { eventSequence = a.Data; };

            sequenceRegistry.CurrentSequenceModel = anotherSequence;

            Assert.AreSame(this.sequenceModel, eventSequence);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfCurrentSequenceIsSetToNull()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            sequenceRegistry.CurrentSequenceModel = null;
        }

        [TestMethod]
        public void ShouldAddTracksFromTimelineIntoSequenceWhenCreatingSequence()
        {
            var sequenceRegistry = this.CreateSequenceRegistry();

            var track1 = new Track();
            var track2 = new Track();
            var track3 = new Track();

            this.sequence.Tracks.Add(track1);
            this.sequence.Tracks.Add(track2);
            this.sequence.Tracks.Add(track3);

            sequenceRegistry.CreateSequence(this.sequence);

            Assert.AreEqual(3, sequenceRegistry.CurrentSequenceModel.Tracks.Count);
            CollectionAssert.Contains(sequenceRegistry.CurrentSequenceModel.Tracks, track1);
            CollectionAssert.Contains(sequenceRegistry.CurrentSequenceModel.Tracks, track2);
            CollectionAssert.Contains(sequenceRegistry.CurrentSequenceModel.Tracks, track3);
        }

        [TestMethod]
        public void ShouldSetDurationBasedOnValueObtainedFromConfigurationService()
        {
            this.configurationService.GetParameterValueReturnFunction = s => { return "60"; };
            
            var sequenceRegistry = this.CreateSequenceRegistry();

            var sequence = sequenceRegistry.CreateSequence(new Sequence());

            Assert.AreEqual(TimeCode.FromSeconds(60.0, SmpteFrameRate.Smpte2997Drop), sequence.Duration);
        }

        private SequenceRegistry CreateSequenceRegistry()
        {
            return new SequenceRegistry(this.container, this.configurationService, this.eventAggregator, new MockProjectService());
        }
    }
}