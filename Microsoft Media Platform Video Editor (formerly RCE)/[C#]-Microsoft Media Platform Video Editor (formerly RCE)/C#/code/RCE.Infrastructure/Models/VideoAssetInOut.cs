// <copyright file="VideoAssetInOut.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VideoAssetInOut.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RCE.Services.Contracts;

    using SMPTETimecode;

    /// <summary>
    /// Specifies the In and Out scrub position of the <see cref="VideoAsset"/>.
    /// </summary>
    public class VideoAssetInOut : VideoAsset, ISubClipAsset
    {
        /// <summary>
        /// The in position of the scrubbed <see cref="VideoAsset"/>.
        /// </summary>
        private double inPosition = -1;

        /// <summary>
        /// The out position of the scrubbed <see cref="VideoAsset"/>.
        /// </summary>
        private double outPosition = -1;

        private AudioStream previewAudioStream;

        private string previewVideoCamera;

        private double timelineStartPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoAssetInOut"/> class.
        /// </summary>
        /// <param name="videoAsset">The video asset.</param>
        public VideoAssetInOut(VideoAsset videoAsset)
        {
            this.VideoAsset = videoAsset;
            this.Id = Guid.NewGuid();
            this.CMSId = videoAsset.CMSId;
            this.AzureId = videoAsset.AzureId;
            this.ArchiveURL = videoAsset.ArchiveURL;
            this.ProviderUri = videoAsset.ProviderUri;
            this.Title = videoAsset.Title;
            this.Source = videoAsset.Source;
            this.Height = videoAsset.Height;
            this.Width = videoAsset.Width;
            this.ResourceType = videoAsset.ResourceType;
            this.ThumbnailSource = videoAsset.ThumbnailSource;
            this.Metadata = videoAsset.Metadata;
            this.AddMarkersToSequence = true;
            this.SequenceAudioStreams = new List<AudioStream>();
            this.PlayByPlayMarkers = new List<PlayByPlay>();
        }

        public bool AddMarkersToSequence { get; set; }

        public string SequenceVideoCamera { get; set; }

        public IList<AudioStream> SequenceAudioStreams { get; set; }

        public AudioStream PreviewAudioStream
        {
            get
            {
                return this.previewAudioStream;
            }

            set
            {
                this.previewAudioStream = value;
                this.OnPropertyChanged("PreviewAudioStream");
            }
        }

        public string PreviewVideoCamera
        {
            get
            {
                return this.previewVideoCamera;
            }

            set
            {
                this.previewVideoCamera = value;
                this.OnPropertyChanged("PreviewVideoCamera");
            }
        }

        /// <summary>
        /// Gets the video asset associated with the Video In Out asset.
        /// </summary>
        /// <value>The video asset associated with the Video In Out asset.</value>
        public VideoAsset VideoAsset { get; private set; }

        /// <summary>
        /// Gets or sets the value of InPosition in seconds from the begining of the video.
        /// </summary>
        /// <value>InPosition value in second.</value>
        public double InPosition
        {
            get
            {
                return this.inPosition;
            }

            set
            {
                this.inPosition = value;
                this.OnPropertyChanged("SubClipDuration");
            }
        }

        /// <summary>
        /// Gets or sets the value of OutPosition in seconds from the begining of the video.
        /// </summary>
        /// <value>OutPosition value in second.</value>
        public double OutPosition
        {
            get
            {
                return this.outPosition;
            }

            set
            {
                this.outPosition = value;
                this.OnPropertyChanged("SubClipDuration");
            }
        }

        /// <summary>
        /// Gets or sets the duration of the Video.
        /// </summary>
        /// <value>The duration of the video.</value>
        public override TimeCode Duration
        {
            get
            {
                return this.VideoAsset.Duration;
            }

            set
            {
                this.VideoAsset.Duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SmpteFrameRate"/> of the Video.
        /// </summary>
        /// <value>The frame rate of the video.</value>
        public override SmpteFrameRate FrameRate
        {
            get
            {
                return this.VideoAsset.FrameRate;
            }

            set
            {
                this.VideoAsset.FrameRate = value;
            }
        }

        public double SubClipDuration
        {
            get
            {
                return this.OutPosition - this.InPosition;
            }
        }

        public List<PlayByPlay> PlayByPlayMarkers { get; set; }

        public IEnumerable<PlayByPlay> PlayByPlayFilteredMarkers { get; private set; }

        private List<PlayByPlay> SourceAssetPlayByPlayMarkers
        {
            get { return ((SmoothStreamingVideoAsset)this.VideoAsset).PlayByPlayMarkers; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return "VideoInOut";
        }

        public void AddPlayByPlayMarker(PlayByPlay playByPlay)
        {
            if (!this.IsPlayByPlayAlreadyAdded(playByPlay))
            {
                this.AddPlayByPlayToSmoothStreamAsset(playByPlay);
            }
        }

        public void GenerateTimelinePlayByPlayList(double startPosition)
        {
            foreach (var pbp in this.PlayByPlayMarkers)
            {
                // update timeline for sequence
                pbp.Time = TimeSpan.FromSeconds(startPosition).Ticks + pbp.Time - TimeSpan.FromSeconds(this.inPosition).Ticks;
            }

            this.PlayByPlayFilteredMarkers = this.PlayByPlayMarkers.Where(pbp => this.ShouldDisplayPlayByPlayMarker(pbp, startPosition));
        }

        public void UpdateFilter(double startPosition)
        {
            this.PlayByPlayFilteredMarkers = this.PlayByPlayMarkers.Where(pbp => this.ShouldDisplayPlayByPlayMarker(pbp, startPosition));
        }

        public void MovePlayByPlayMarkers(long ticks)
        {
            this.PlayByPlayMarkers.ForEach(pbp => pbp.Time += ticks);
        }

        public VideoAssetInOut Clone() 
        {
            var clone = new VideoAssetInOut(this.VideoAsset);
            clone.Duration = this.Duration;
            clone.FrameRate = this.FrameRate;
            clone.InPosition = this.InPosition;
            clone.OutPosition = this.OutPosition;
            clone.SequenceAudioStreams = this.SequenceAudioStreams;
            clone.SequenceVideoCamera = this.SequenceVideoCamera;
            clone.PreviewAudioStream = this.PreviewAudioStream;
            clone.PreviewVideoCamera = this.PreviewVideoCamera;
            clone.AddMarkersToSequence = this.AddMarkersToSequence;
            clone.PlayByPlayMarkers = this.GetPlayByPlayClones();
            return clone;
        }

        private List<PlayByPlay> GetPlayByPlayClones()
        {
            return
                this.SourceAssetPlayByPlayMarkers.Select(
                    e =>
                    new PlayByPlay((TimeSpan.FromTicks(e.Time) - TimeSpan.FromSeconds(this.InPosition)).Ticks)
                    {
                        Text = e.Text,
                        Time = e.Time,
                        TimelineId = Guid.NewGuid(),
                        ID = Guid.NewGuid()
                    }).ToList();
        }

        private void AddPlayByPlayToSmoothStreamAsset(PlayByPlay playByPlay)
        {
            SmoothStreamingVideoAsset smoothAsset = this.VideoAsset as SmoothStreamingVideoAsset;

            if (smoothAsset == null)
            {
                return;
            }
            
            smoothAsset.PlayByPlayMarkers.Add(playByPlay);
        }

        private bool IsPlayByPlayAlreadyAdded(PlayByPlay playByPlay)
        {
            SmoothStreamingVideoAsset smoothAsset = this.VideoAsset as SmoothStreamingVideoAsset;

            if (smoothAsset == null)
            {
                return false;
            }

            return smoothAsset.PlayByPlayMarkers.Any(pbp => pbp.TimeWithOffset == playByPlay.TimeWithOffset && pbp.Text.Equals(playByPlay.Text));
        }

        private bool ShouldDisplayPlayByPlayMarker(PlayByPlay playByPlay, double startPosition)
        {
            double endPosition = (this.OutPosition - this.InPosition) + startPosition;

            return playByPlay.Time >= TimeSpan.FromSeconds(startPosition).Ticks && playByPlay.Time <= TimeSpan.FromSeconds(endPosition).Ticks;
        }
    }
}
