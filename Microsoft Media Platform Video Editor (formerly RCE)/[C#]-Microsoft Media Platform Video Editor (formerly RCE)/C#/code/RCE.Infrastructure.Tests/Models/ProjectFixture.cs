// <copyright file="ProjectFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectFixture.cs                     
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
    using Project = RCE.Infrastructure.Models.Project;
    using Track = RCE.Infrastructure.Models.Track;

    /// <summary>
    /// Test class for <see cref="Infrastructure.Models.Project"/> class.
    /// </summary>
    [TestClass]
    public class ProjectFixture
    {
        /// <summary>
        /// Tests if the duration of the project is zero when there is not 
        /// track in the project.
        /// </summary>
        [TestMethod]
        public void ShouldSetDurationToZeroIfThereIsNotTrackInProject()
        {
            Project project = new Project();
            project.AddTimeline(new Sequence());
            project.SetProjectDuration();

            Assert.IsTrue(project.Duration == 0);
        }

        /// <summary>
        /// Tests if the project duration is zero when there are tracks in the project
        /// but there is no timeline elements in the tracks.
        /// </summary>
        [TestMethod]
        public void ShouldSetDurationToZeroIfThereIsNoElementsInTracks()
        {
            Project project = new Project();
            project.AddTimeline(new Sequence());
            Track track1 = new Track();
            Track track2 = new Track();
            project.Timelines[0].Tracks.Add(track1);
            project.Timelines[0].Tracks.Add(track2);

            project.SetProjectDuration();

            Assert.IsTrue(project.Duration == 0);
        }

        /// <summary>
        /// Tests if the duration is set correctly when the project has only one track and 
        /// only one timeline element in this track.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheProjectDurationWhenThereIsOneTrackAndOneShot()
        {
            Project project = new Project();
            project.AddTimeline(new Sequence());
            Track track = new Track();
            project.Timelines[0].Tracks.Add(track);
            TimelineElement timelineElement = new TimelineElement()
                                                  {
                                                      InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                                                      OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                                                      Position = TimeCode.FromSeconds(10000.0, SmpteFrameRate.Smpte25)
                                                  };
            track.Shots.Add(timelineElement);
            project.SetProjectDuration();
            var duration = timelineElement.OutPosition.TotalSeconds - timelineElement.InPosition.TotalSeconds +
                           timelineElement.Position.TotalSeconds;
            Assert.AreEqual(project.Duration, duration);
        }

        /// <summary>
        /// Tests if the duration is set correctly when the project has only one track and 
        /// more than one timeline elements in this track.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheProjectDurationWhenThereIsOneTrackAndMoreThanOneShot()
        {
            Project project = new Project();
            project.AddTimeline(new Sequence());
            Track track = new Track();
            project.Timelines[0].Tracks.Add(track);
            TimelineElement timelineElement1 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(10000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement2 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(12000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(14000.0, SmpteFrameRate.Smpte25)
            };

            track.Shots.Add(timelineElement);
            track.Shots.Add(timelineElement1);
            track.Shots.Add(timelineElement2);
            project.SetProjectDuration();

            var duration = timelineElement.OutPosition.TotalSeconds - timelineElement.InPosition.TotalSeconds +
                           timelineElement.Position.TotalSeconds;
            Assert.AreEqual(project.Duration, duration);
        }

        /// <summary>
        /// Tests if the duration is set correctly when the project has more than one tracks and 
        /// more then one timeline elements in all the tracks.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheProjectDurationWhenThereIsMoreThanOneTrackAndMoreThanOneShot()
        {
            Project project = new Project();
            project.AddTimeline(new Sequence());
            Track track1 = new Track();
            Track track2 = new Track();
            project.Timelines[0].Tracks.Add(track1);
            project.Timelines[0].Tracks.Add(track2);

            TimelineElement timelineElement1 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(10000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement2 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(12000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement3 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(14000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement4 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(3000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(10000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement5 = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(4000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(6000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(12000.0, SmpteFrameRate.Smpte25)
            };

            TimelineElement timelineElement = new TimelineElement()
            {
                InPosition = TimeCode.FromSeconds(2000.0, SmpteFrameRate.Smpte25),
                OutPosition = TimeCode.FromSeconds(4000.0, SmpteFrameRate.Smpte25),
                Position = TimeCode.FromSeconds(20000.0, SmpteFrameRate.Smpte25)
            };

            track1.Shots.Add(timelineElement1);
            track1.Shots.Add(timelineElement2);
            track1.Shots.Add(timelineElement3);

            track2.Shots.Add(timelineElement4);
            track2.Shots.Add(timelineElement5);
            track2.Shots.Add(timelineElement);

            project.SetProjectDuration();

            var duration = timelineElement.OutPosition.TotalSeconds - timelineElement.InPosition.TotalSeconds +
                           timelineElement.Position.TotalSeconds;
            Assert.AreEqual(project.Duration, duration);
        }
    }
}
