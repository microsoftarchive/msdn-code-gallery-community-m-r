// <copyright file="TimelineModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Models
{
    using System;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Tests.Mocks;
    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// Test class for <see cref="SequenceModel"/>.
    /// </summary>
    [TestClass]
    public class TimelineModelFixture
    {
        /// <summary>
        /// Tests if the elements are added properly while adding them at last.
        /// </summary>
        [TestMethod]
        public void AddElementInsertsLast()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();

            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            Assert.AreEqual(3, model.Tracks[0].Shots.Count);
            Assert.AreEqual(elementA.Id, model.Tracks[0].Shots[0].Id);
            Assert.AreEqual(elementB.Id, model.Tracks[0].Shots[1].Id);
            Assert.AreEqual(elementC.Id, model.Tracks[0].Shots[2].Id);
        }

        /// <summary>
        /// Tests if the elements are added properly while adding them at start.
        /// </summary>
        [TestMethod]
        public void AddElementInsertsFirst()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();

            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.AddElement(elementB, model.Tracks[0]);
            model.AddElement(elementC, model.Tracks[0]);
            model.AddElement(elementA, model.Tracks[0]);

            Assert.AreEqual(3, model.Tracks[0].Shots.Count);
            Assert.AreEqual(elementA.Id, model.Tracks[0].Shots[0].Id);
            Assert.AreEqual(elementB.Id, model.Tracks[0].Shots[1].Id);
            Assert.AreEqual(elementC.Id, model.Tracks[0].Shots[2].Id);
        }

        /// <summary>
        /// Tests if the elements are added properly while adding them at middle.
        /// </summary>
        [TestMethod]
        public void AddElementInsertsInMiddle()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();

            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.AddElement(elementA, model.Tracks[0]);
            model.AddElement(elementC, model.Tracks[0]);
            model.AddElement(elementB, model.Tracks[0]);

            Assert.AreEqual(3, model.Tracks[0].Shots.Count);
            Assert.AreEqual(elementA.Id, model.Tracks[0].Shots[0].Id);
            Assert.AreEqual(elementB.Id, model.Tracks[0].Shots[1].Id);
            Assert.AreEqual(elementC.Id, model.Tracks[0].Shots[2].Id);
        }

        /// <summary>
        /// Gets the element at current position.
        /// </summary>
        [TestMethod]
        public void GetElementAtPosition()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();

            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            Assert.AreSame(elementA, model.GetElementAtPosition(TimeCode.FromAbsoluteTime(0.1, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementA, model.GetElementAtPosition(TimeCode.FromAbsoluteTime(1, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementB, model.GetElementAtPosition(TimeCode.FromAbsoluteTime(1.1, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementB, model.GetElementAtPosition(TimeCode.FromAbsoluteTime(2, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementC, model.GetElementAtPosition(TimeCode.FromAbsoluteTime(2.1, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementC, model.GetElementAtPosition(TimeCode.FromAbsoluteTime(3, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.IsNull(model.GetElementAtPosition(TimeCode.FromAbsoluteTime(4, SmpteFrameRate.Smpte30), model.Tracks[0], null));
        }

        /// <summary>
        /// Tests if GetElementWithinRange is working properly. 
        /// </summary>
        [TestMethod]
        public void GetElementWithinRange()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();

            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            Assert.AreSame(elementA, model.GetElementWithinRange(TimeCode.FromAbsoluteTime(0.1, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(1, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementA, model.GetElementWithinRange(TimeCode.FromAbsoluteTime(0.1, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(2, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementB, model.GetElementWithinRange(TimeCode.FromAbsoluteTime(1.1, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(1.2, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.AreSame(elementC, model.GetElementWithinRange(TimeCode.FromAbsoluteTime(2, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(2.1, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.IsNull(model.GetElementWithinRange(TimeCode.FromAbsoluteTime(3, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(10, SmpteFrameRate.Smpte30), model.Tracks[0], null));
            Assert.IsNull(model.GetElementWithinRange(TimeCode.FromAbsoluteTime(0.1, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(0.2, SmpteFrameRate.Smpte30), model.Tracks[0], elementA));
            Assert.IsNull(model.GetElementWithinRange(TimeCode.FromAbsoluteTime(4, SmpteFrameRate.Smpte30), TimeCode.FromAbsoluteTime(5, SmpteFrameRate.Smpte30), model.Tracks[0], null));
        }

        /// <summary>
        /// Tests if the comments are added properly while adding them at start.
        /// </summary>
        [TestMethod]
        public void AddCommentInsertsFirst()
        {
            var model = new Sequence();
            model.AddComment(new Comment { Text = "Second", MarkIn = 1 });
            model.AddComment(new Comment { Text = "First", MarkIn = 0 });

            Assert.AreEqual("First", model.CommentElements[0].Text);
            Assert.AreEqual("Second", model.CommentElements[1].Text);
        }

        /// <summary>
        /// Tests if the comments are added properly while adding them at last.
        /// </summary>
        [TestMethod]
        public void AddCommentInsertsLast()
        {
            var model = new Sequence();
            model.AddComment(new Comment { Text = "First", MarkIn = 0 });
            model.AddComment(new Comment { Text = "Second", MarkIn = 1 });

            Assert.AreEqual("First", model.CommentElements[0].Text);
            Assert.AreEqual("Second", model.CommentElements[1].Text);
        }

        /// <summary>
        /// Tests if the comments are added properly while adding them at middle.
        /// </summary>
        [TestMethod]
        public void AddCommentInsertsInMiddle()
        {
            var model = new Sequence();
            
            model.AddComment(new Comment { Text = "Third", MarkIn = 3 });
            model.AddComment(new Comment { Text = "Second", MarkIn = 1 });
            model.AddComment(new Comment { Text = "First", MarkIn = 0 });

            Assert.AreEqual("First", model.CommentElements[0].Text);
            Assert.AreEqual("Second", model.CommentElements[1].Text);
            Assert.AreEqual("Third", model.CommentElements[2].Text);
        }

        /// <summary>
        /// Tests if the link to right element is working properly.
        /// </summary>
        [TestMethod]
        public void LinkRightElement()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);

            var result = model.LinkNextElement(elementA, elementB);
            var linkA = model.GetElementLink(elementA);
            var linkB = model.GetElementLink(elementB);

            Assert.IsTrue(result);
            Assert.IsNotNull(linkA);
            Assert.IsNotNull(linkB);
            Assert.AreEqual(elementB.Id, linkA.NextElementId);
            Assert.AreEqual(elementA.Id, linkB.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkA.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkB.NextElementId);
        }

        /// <summary>
        /// Tests if the link to left element is working properly.
        /// </summary>
        [TestMethod]
        public void LinkLeftElement()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            
            var result = model.LinkPreviousElement(elementB, elementA);
            var linkA = model.GetElementLink(elementA);
            var linkB = model.GetElementLink(elementB);

            Assert.IsTrue(result);
            Assert.IsNotNull(linkA);
            Assert.IsNotNull(linkB);
            Assert.AreEqual(elementB.Id, linkA.NextElementId);
            Assert.AreEqual(elementA.Id, linkB.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkA.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkB.NextElementId);
        }

        /// <summary>
        /// Tests if LinkNextElement returns false while linking to right with wrong position.
        /// </summary>
        [TestMethod]
        public void LinkRightElementWrongPositionsReturnsFalse()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            var result = model.LinkNextElement(elementA, elementC);
            var linkA = model.GetElementLink(elementA);
            var linkB = model.GetElementLink(elementB);

            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, linkA.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkA.NextElementId);
            Assert.AreEqual(Guid.Empty, linkB.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkB.NextElementId);
        }

        /// <summary>
        /// Tests if LinkNextElement returns false while linking to left with wrong position.
        /// </summary>
        [TestMethod]
        public void LinkLeftElementWrongPositionsReturnsFalse()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            var result = model.LinkPreviousElement(elementC, elementA);
            var linkA = model.GetElementLink(elementA);
            var linkB = model.GetElementLink(elementB);

            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, linkA.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkA.NextElementId);
            Assert.AreEqual(Guid.Empty, linkB.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkB.NextElementId);
        }

        /// <summary>
        /// Tests the Unlinking of the elements.
        /// </summary>
        [TestMethod]
        public void UnlinkElements()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            // link
            var result = model.LinkNextElement(elementA, elementB);
            Assert.IsTrue(result);
            result = model.LinkNextElement(elementB, elementC);
            Assert.IsTrue(result);

            // unlink
            model.UnlinkElements(elementA, elementB);

            var linkA = model.GetElementLink(elementA);
            Assert.AreEqual(Guid.Empty, linkA.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkA.NextElementId);

            var linkB = model.GetElementLink(elementB);
            Assert.AreEqual(Guid.Empty, linkB.PreviousElementId);
            Assert.AreEqual(elementC.Id, linkB.NextElementId);

            var linkC = model.GetElementLink(elementC);
            Assert.AreEqual(elementB.Id, linkC.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkC.NextElementId);
        }

        /// <summary>
        /// Tests if Unlink the middle element removes all links.
        /// </summary>
        [TestMethod]
        public void UnlinkMiddleElementRemovesAllLinks()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());
            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            // link
            var result = model.LinkNextElement(elementA, elementB);
            Assert.IsTrue(result);
            result = model.LinkNextElement(elementB, elementC);
            Assert.IsTrue(result);

            // unlink
            model.UnlinkElement(elementB);

            var linkA = model.GetElementLink(elementA);
            Assert.AreEqual(Guid.Empty, linkA.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkA.NextElementId);

            var linkB = model.GetElementLink(elementB);
            Assert.AreEqual(Guid.Empty, linkB.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkB.NextElementId);

            var linkC = model.GetElementLink(elementC);
            Assert.AreEqual(Guid.Empty, linkC.PreviousElementId);
            Assert.AreEqual(Guid.Empty, linkC.NextElementId);
        }

        /// <summary>
        /// Should get first element in chain of links.
        /// </summary>
        [TestMethod]
        public void ShouldGetFirstElementInChainOfLinks()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());

            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            // B -> C
            model.LinkNextElement(elementB, elementC);
            
            // A -> B
            model.LinkPreviousElement(elementB, elementA);

            var element = model.FindFirstElementLinking(elementC, model.Tracks[0]);
            
            Assert.AreEqual(elementA, element);

            element = model.FindFirstElementLinking(elementB, model.Tracks[0]);

            Assert.AreEqual(elementA, element);
        }

        /// <summary>
        /// Should get last element in chain of links.
        /// </summary>
        [TestMethod]
        public void ShouldGetLastElementInChainOfLinks()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());

            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            // B -> C
            model.LinkNextElement(elementB, elementC);

            // A -> B
            model.LinkPreviousElement(elementB, elementA);

            var element = model.FindLastElementLinking(elementA, model.Tracks[0]);

            Assert.AreEqual(elementC, element);

            element = model.FindLastElementLinking(elementB, model.Tracks[0]);

            Assert.AreEqual(elementC, element);
        }

        /// <summary>
        /// Tests if two elements are link to each other.
        /// </summary>
        [TestMethod]
        public void ShouldGetIfAnElementIsLinkedToOther()
        {
            var elementA = GetElementA();
            var elementB = GetElementB();
            var elementC = GetElementC();
            var model = new SequenceModel(new MockEventAggregator());

            model.Tracks.Add(new Track { TrackType = TrackType.Visual });

            model.Tracks[0].Shots.Add(elementA);
            model.Tracks[0].Shots.Add(elementB);
            model.Tracks[0].Shots.Add(elementC);

            // B -> C
            model.LinkNextElement(elementB, elementC);

            var result = model.IsElementLinkedTo(elementB, elementC);

            Assert.IsTrue(result);

            result = model.IsElementLinkedTo(elementA, elementC);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Gets the element A.
        /// </summary>
        /// <returns>The <see cref="TimelineElement"/>.</returns>
        private static TimelineElement GetElementA()
        {
            return new TimelineElement
                       {
                           InPosition = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30),
                           OutPosition = TimeCode.FromAbsoluteTime(1, SmpteFrameRate.Smpte30),
                           Position = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30)
                       };
        }

        /// <summary>
        /// Gets the element B.
        /// </summary>
        /// <returns>The <see cref="TimelineElement"/>.</returns>
        private static TimelineElement GetElementB()
        {
            return new TimelineElement
                       {
                           InPosition = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30),
                           OutPosition = TimeCode.FromAbsoluteTime(1, SmpteFrameRate.Smpte30),
                           Position = TimeCode.FromAbsoluteTime(1, SmpteFrameRate.Smpte30)
                       };
        }

        /// <summary>
        /// Gets the element C.
        /// </summary>
        /// <returns>The <see cref="TimelineElement"/>.</returns>
        private static TimelineElement GetElementC()
        {
            return new TimelineElement
                       {
                           InPosition = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30),
                           OutPosition = TimeCode.FromAbsoluteTime(1, SmpteFrameRate.Smpte30),
                           Position = TimeCode.FromAbsoluteTime(2, SmpteFrameRate.Smpte30)
                       };
        }
    }
}