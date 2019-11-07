// <copyright file="TimelineControlFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineControlFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Controls
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Contracts;
    using SMPTETimecode;

    [TestClass]
    public class TimelineControlFixture
    {
        /// <summary>
        /// Tests if the StartOffset is being set when the SetStartOffset method is called.
        /// </summary>
        [TestMethod]
        public void ShouldSetStartOffsetWhenCallingToSetStartTimeCode()
        {
            var timeCode = TimeCode.FromSeconds(450d, SmpteFrameRate.Smpte2997NonDrop);
            
            var timelineControl = new TimelineControl();
            
            timelineControl.SetStartTimeCode(timeCode);

            Assert.AreEqual(timeCode, timelineControl.StartOffset);
        }

        /// <summary>
        /// Tests if the initial position is being retrieved when the mark in is not set.
        /// </summary>
        [TestMethod]
        public void ShouldReturnStartOffsetIfMarkInWasNotSetWhenGettingTheMarkIn()
        {
            var timelineControl = new TimelineControl();
            timelineControl.SetStartTimeCode(TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997NonDrop));

            Assert.AreEqual(timelineControl.StartOffset, timelineControl.InPosition);

            timelineControl.SetStartTimeCode(TimeCode.FromSeconds(100d, SmpteFrameRate.Smpte2997NonDrop));

            Assert.AreEqual(timelineControl.StartOffset, timelineControl.InPosition);
        }

        /// <summary>
        /// Tests if the end position is being retrieved when the mark out is not set.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTheDurationIfMarkOutWasNotSetWhenGettingTheMarkOut()
        {
            var duration = TimeCode.FromSeconds(800d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            timelineControl.SetDuration(duration);

            Assert.AreEqual(duration, timelineControl.OutPosition);
        }

        /// <summary>
        /// Tests if the MarkIn position is being set.
        /// </summary>
        [TestMethod]
        public void ShouldSetMarkIn()
        {
            var markIn = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();
            
            timelineControl.SetMarkIn(markIn);

            Assert.AreEqual(markIn, timelineControl.InPosition);
        }

        /// <summary>
        /// Tests if the HasMarkIn property returns false when the mark in is not set.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseIfMarkInWasNotSet()
        {
            var timelineControl = new TimelineControl();
   
            Assert.IsFalse(timelineControl.HasMarkIn);
        }

        /// <summary>
        /// Tests if the HasMarkIn property returns false when the mark in is set.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfMarkInWasSet()
        {
            var markIn = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            timelineControl.SetMarkIn(markIn);

            Assert.IsTrue(timelineControl.HasMarkIn);
        }

        /// <summary>
        /// Tests if the MarkOut position is being set.
        /// </summary>
        [TestMethod]
        public void ShouldSetMarkOut()
        {
            var markOut = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            timelineControl.SetMarkOut(markOut);

            Assert.AreEqual(markOut, timelineControl.OutPosition);
        }

        /// <summary>
        /// Tests if the HasMarkIn property returns false when the mark out is not set.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseIfMarkOutWasNotSet()
        {
            var timelineControl = new TimelineControl();

            Assert.IsFalse(timelineControl.HasMarkOut);
        }

        /// <summary>
        /// Tests if the HasMarkIn property returns false when the mark Out is set.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfMarkOutWasSet()
        {
            var markOut = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            timelineControl.SetMarkOut(markOut);

            Assert.IsTrue(timelineControl.HasMarkOut);
        }

        /// <summary>
        /// Tests if the MarkIn position is not being set after an existing mark out.
        /// </summary>
        [TestMethod]
        public void ShouldNotSetMarkInAfterMarkOut()
        {
            var markOut = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);
            var markIn = TimeCode.FromSeconds(800d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            timelineControl.SetMarkOut(markOut);
            timelineControl.SetMarkIn(markIn);

            Assert.AreEqual(0, timelineControl.InPosition.TotalSeconds);
            Assert.AreEqual(markOut, timelineControl.OutPosition);
        }

        /// <summary>
        /// Tests if false is returned when the MarkIn is invalid.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseIfTheMarkInIsInvalid()
        {
            var markOut = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);
            var markIn = TimeCode.FromSeconds(800d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            var resultMarkOut = timelineControl.SetMarkOut(markOut);
            var resultMarkIn = timelineControl.SetMarkIn(markIn);

            Assert.IsTrue(resultMarkOut);
            Assert.IsFalse(resultMarkIn);
        }

        /// <summary>
        /// Tests if false is returned when the MarkOut is invalid.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseIfTheMarkOutIsInvalid()
        {
            var markIn = TimeCode.FromSeconds(800d, SmpteFrameRate.Smpte2997NonDrop);
            var markOut = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();

            var resultMarkIn = timelineControl.SetMarkIn(markIn);
            var resultMarkOut = timelineControl.SetMarkOut(markOut);

            Assert.IsTrue(resultMarkIn);
            Assert.IsFalse(resultMarkOut);
        }

        /// <summary>
        /// Tests if the MarkOut position is not being set before an existing mark in.
        /// </summary>
        [TestMethod]
        public void ShouldNotSetMarkOutBeforeMarkIn()
        {
            var markIn = TimeCode.FromSeconds(800d, SmpteFrameRate.Smpte2997NonDrop);
            var markOut = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997NonDrop);
            var duration = TimeCode.FromSeconds(900d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();
            timelineControl.SetDuration(duration);

            timelineControl.SetMarkIn(markIn);

            timelineControl.SetMarkOut(markOut);

            Assert.AreEqual(duration, timelineControl.OutPosition);
            Assert.AreEqual(markIn, timelineControl.InPosition);
        }

        /// <summary>
        /// Tests if the position is being returned without offset.
        /// </summary>
        [TestMethod]
        public void ShouldGetPositionWithoutOffset()
        {
            var position = TimeCode.FromSeconds(800d, SmpteFrameRate.Smpte2997NonDrop);
            var startOffset = TimeCode.FromSeconds(450d, SmpteFrameRate.Smpte2997NonDrop);

            var timelineControl = new TimelineControl();
            timelineControl.SetStartTimeCode(startOffset);

            var result = timelineControl.GetPositionWithoutStartOffset(position);

            Assert.AreEqual(350, result.TotalSeconds);
        }
    }
}
