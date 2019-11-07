// <copyright file="TimelineElementFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineElementFixture.cs                     
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Models;
    using SMPTETimecode;

    /// <summary>
    /// Test class for <see cref="TimelineElement"/>.
    /// </summary>
    [TestClass]
    public class TimelineElementFixture
    {
        /// <summary>
        /// Should get correct duration based on in position and out position values.
        /// </summary>
        [TestMethod]
        public void ShouldGetCorrectDurationBasedOnInPositionAndOutPositionValues()
        {
            var timelineElement = new TimelineElement
              {
                  Position = TimeCode.FromSeconds(100d, SmpteFrameRate.Smpte30),
                  InPosition = TimeCode.FromSeconds(100d, SmpteFrameRate.Smpte30),
                  OutPosition = TimeCode.FromSeconds(300d, SmpteFrameRate.Smpte30)
              };

            Assert.AreEqual(TimeCode.FromSeconds(200d, SmpteFrameRate.Smpte30), timelineElement.Duration);
            Assert.AreEqual(200, timelineElement.Duration.TotalSeconds);
        }

        /// <summary>
        /// Should raise on OnPropertyChanged when InPosition is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenInPositionIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var timelineElement = new TimelineElement { InPosition = TimeCode.FromSeconds(100d, SmpteFrameRate.Smpte30) };
            timelineElement.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            timelineElement.InPosition = TimeCode.FromSeconds(200d, SmpteFrameRate.Smpte30);

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("InPosition", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Shoulds raise OnPropertyChangedevent when OutPositionis updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenOutPositionIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var timelineElement = new TimelineElement { OutPosition = TimeCode.FromSeconds(100d, SmpteFrameRate.Smpte30) };
            timelineElement.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            timelineElement.OutPosition = TimeCode.FromSeconds(200d, SmpteFrameRate.Smpte30);

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("OutPosition", propertyChangedEventArgsArgument);
        }
    }
}
