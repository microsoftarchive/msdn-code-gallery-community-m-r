// <copyright file="PlaybackManifestGenerator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlaybackManifestGenerator.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Threading;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    using SmoothStreamingManifestGenerator;
    using SmoothStreamingManifestGenerator.Models;

    using SMPTETimecode;

    using Transition = RCE.Infrastructure.Models.Transition;

    public class PlaybackManifestGenerator : IPlaybackManifestGenerator
    {
        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IUserSettingsService userSettingsService;

        private readonly RCE.Infrastructure.Services.DownloaderManager downloaderManager;

        private readonly IDictionary<Uri, Stream> streamsByUri;

        private readonly IList<Uri> pendingDownloads;

        private readonly Dispatcher dispatcher;

        private Uri gapUri;

        private bool startedAllDownloads;

        private Action<IDictionary<Track, string>> actionCallback;

        private string gapCmsId;

        private string gapAzureId;

        private bool treatGapsAsError;

        public PlaybackManifestGenerator(
            ISequenceRegistry sequenceRegistry,
            IConfigurationService configurationSettings,
            IUserSettingsService userSettingsService)
        {
            this.dispatcher = Application.Current.RootVisual.Dispatcher;
            this.sequenceRegistry = sequenceRegistry;
            this.userSettingsService = userSettingsService;
            this.streamsByUri = new Dictionary<Uri, Stream>();
            this.pendingDownloads = new List<Uri>();
            this.downloaderManager = new RCE.Infrastructure.Services.DownloaderManager();
            this.downloaderManager.DownloadCompleted += this.OnManifestDownloadCompleted;
            this.treatGapsAsError = configurationSettings.GetTreatGapAsError();

            if (!this.treatGapsAsError)
            {
                this.GetGapAttributesFromUserSettings(configurationSettings);
            }
        }

        public void BeginManifestGeneration(Action<IDictionary<Track, string>> callback)
        {
            bool downloadIsInProgress = false;

            this.actionCallback = callback;

            this.startedAllDownloads = false;

            if (this.sequenceRegistry.CurrentSequenceModel != null)
            {
                if (!this.treatGapsAsError && !this.streamsByUri.ContainsKey(this.gapUri))
                {
                    this.pendingDownloads.Add(this.gapUri);
                    this.downloaderManager.DownloadManifestAsync(this.gapUri, true, null);
                    downloadIsInProgress = true;
                }

                var tracks =
                    this.sequenceRegistry.CurrentSequenceModel.Tracks.Where(
                        t => (t.TrackType == TrackType.Visual || t.TrackType == TrackType.Audio) && t.Shots.Count > 0);

                // for each track get all elements and start downloading.
                foreach (var track in tracks)
                {
                    foreach (TimelineElement shot in track.Shots)
                    {
                        VideoAssetInOut assetInOut = shot.Asset as VideoAssetInOut;

                        bool canProcess = (assetInOut != null) ? (assetInOut.VideoAsset is SmoothStreamingVideoAsset) : (shot.Asset is SmoothStreamingVideoAsset || shot.Asset is SmoothStreamingAudioAsset);

                        if (!(this.streamsByUri.ContainsKey(shot.Asset.Source) || this.pendingDownloads.Contains(shot.Asset.Source)) && canProcess)
                        {
                            this.pendingDownloads.Add(shot.Asset.Source);
                            this.downloaderManager.DownloadManifestAsync(shot.Asset.Source, true, null);
                            downloadIsInProgress = true;
                        }
                    }
                }
            }

            this.startedAllDownloads = true;

            if (this.CanGenerateManifest() && !downloadIsInProgress)
            {
                this.GenerateManifest();
            }
        }

        private static IDictionary<TimelineElement, double> CalculateGapsDuration(Track track)
        {
            IDictionary<TimelineElement, double> gapDurations = new Dictionary<TimelineElement, double>();
            for (int i = 0; i < track.Shots.Count; i++)
            {
                double duration;
                var currentElement = track.Shots[i];
                if (i > 0)
                {
                    var previousElement = track.Shots[i - 1];
                    duration = currentElement.Position.TotalSeconds - (previousElement.Position.TotalSeconds + previousElement.Duration.TotalSeconds);
                }
                else
                {
                    duration = currentElement.Position.TotalSeconds;
                }

                gapDurations.Add(currentElement, duration);
            }

            return gapDurations;
        }

        private static void AddClipToCompositeManifestInfo(TimelineElement shot, Stream manifestStream, CompositeManifestInfo compositeManifestInfo)
        {
            const ulong Timescale = 10000000;

            ulong startPosition = 0;

            SmoothStreamingManifestParser parser = new SmoothStreamingManifestParser(manifestStream);
            manifestStream.Seek(0, SeekOrigin.Begin);

            var subClipAsset = shot.Asset as VideoAssetInOut;
            var adaptiveAsset = subClipAsset != null && subClipAsset.IsAdaptiveAsset ? (IAdaptiveAsset)subClipAsset.VideoAsset : shot.Asset as IAdaptiveAsset;

            // the timeline elements holds the in/out position, instead of creating an ISubClipAsset
            double markIn = shot.InPosition.TotalSeconds;
            double markOut = shot.OutPosition.TotalSeconds;

            if (adaptiveAsset != null)
            {
                startPosition = GetStartPositionFromManifest(parser);
            }

            if (!string.IsNullOrEmpty(shot.SelectedAudioStream))
            {
                var audioStreamsToRemove = parser.ManifestInfo.Streams.Where(s => s.StreamType.Equals("Audio", StringComparison.CurrentCultureIgnoreCase) &&
                    !s.Attributes["Name"].Equals(shot.SelectedAudioStream)).ToList();

                if (audioStreamsToRemove != null)
                {
                    foreach (var audioStream in audioStreamsToRemove)
                    {
                        parser.ManifestInfo.Streams.Remove(audioStream);
                    }
                }
            }

            if (subClipAsset != null)
            {
                if (!string.IsNullOrEmpty(subClipAsset.SequenceVideoCamera))
                {
                    var videoStream = parser.ManifestInfo.Streams.First(s => s.StreamType.Equals("Video", StringComparison.CurrentCultureIgnoreCase));

                    var qualityLevelsToRemove = videoStream.QualityLevels.Where(q => !q.CustomAttributes.ContainsKey("cameraAngle") ||
                        !q.CustomAttributes["cameraAngle"].Equals(subClipAsset.SequenceVideoCamera)).ToList();

                    foreach (var qualityLevel in qualityLevelsToRemove)
                    {
                        videoStream.QualityLevels.Remove(qualityLevel);
                    }
                }

                startPosition = GetStartPositionFromManifest(parser);
            }

            ulong clipBegin = (ulong)(Convert.ToUInt64(markIn * Timescale) + startPosition);
            ulong clipEnd = (ulong)(Convert.ToUInt64(markOut * Timescale) + startPosition);
            var customAttributes = new Dictionary<string, string> { { "CMSId", shot.Asset.CMSId }, { "AzureId", shot.Asset.AzureId } };

            compositeManifestInfo.AddClip(shot.Asset.Source, clipBegin, clipEnd, customAttributes, parser.ManifestInfo);
        }

        private static ulong GetStartPositionFromManifest(SmoothStreamingManifestParser parser)
        {
            ulong? maxStartTime =
                parser.ManifestInfo.Streams.Where(
                    s =>
                    s.StreamType.Equals("video", StringComparison.CurrentCultureIgnoreCase)
                    || s.StreamType.Equals("audio", StringComparison.CurrentCultureIgnoreCase)).Max(
                        s => s.Chunks.First().Time);

            if (maxStartTime.HasValue)
            {
                return maxStartTime.Value;
            }

            return 0;
        }

        private static void AddTransition(SmoothStreamingManifestGenerator.Models.TransitionType transitionType, AssetType assetType, ulong position, double duration, CompositeManifestInfo compositeManifestInfo)
        {
            compositeManifestInfo.AddTransition(transitionType, assetType, position, duration);
        }

        private void AddPreviousGap(double gapDuration, Stream gapManifestStream, CompositeManifestInfo compositeManifestInfo)
        {
            if (gapDuration == 0)
            {
                return;
            }

            const double Timescale = 10000000.0;

            SmoothStreamingManifestParser parser = new SmoothStreamingManifestParser(gapManifestStream);
            gapManifestStream.Seek(0, SeekOrigin.Begin);

            double gapVideoCount = gapDuration / (parser.ManifestInfo.ManifestDuration / Timescale);

            int completeVideos = (int)gapVideoCount;
            double partialVideoProportion = gapVideoCount - (int)gapVideoCount;

            ulong clipEnd = parser.ManifestInfo.ManifestDuration;
            var customAttributes = new Dictionary<string, string> { { "CMSId", this.gapCmsId }, { "AzureId", this.gapAzureId } };

            for (int i = 0; i < completeVideos; i++)
            {
                compositeManifestInfo.AddClip(this.gapUri, 0, clipEnd, customAttributes, true, parser.ManifestInfo);
            }

            clipEnd = (ulong)(clipEnd * partialVideoProportion);

            if (clipEnd != 0)
            {
                compositeManifestInfo.AddClip(this.gapUri, 0, clipEnd, customAttributes, true, parser.ManifestInfo);
            }
        }

        private void OnManifestDownloadCompleted(object sender, DownloaderManagerEventArgs e)
        {
            this.streamsByUri[e.ManifestUri] = e.Stream;
            this.pendingDownloads.Remove(e.ManifestUri);

            if (this.CanGenerateManifest())
            {
                this.GenerateManifest();
            }
        }

        private void GenerateManifest()
        {
            IDictionary<Track, string> manifestByTrack = new Dictionary<Track, string>();

            var tracks =
                this.sequenceRegistry.CurrentSequenceModel.Tracks.Where(
                    t => (t.TrackType == TrackType.Visual || t.TrackType == TrackType.Audio) && t.Shots.Count > 0);

            foreach (var track in tracks)
            {
                CompositeManifestInfo compositeManifestInfo = new CompositeManifestInfo(2, 0);
                compositeManifestInfo.RubberBandingDataStreamName = "RubberBanding";
                compositeManifestInfo.TransitionDataStreamName = "Transition";

                IDictionary<TimelineElement, double> gapDurations = CalculateGapsDuration(track);
                foreach (TimelineElement shot in track.Shots)
                {
                    if (this.streamsByUri.ContainsKey(shot.Asset.Source))
                    {
                        Stream manifestStream = this.streamsByUri[shot.Asset.Source];
                        if (manifestStream != null)
                        {
                            if (!this.treatGapsAsError)
                            {
                                this.AddPreviousGap(gapDurations[shot], this.streamsByUri[this.gapUri], compositeManifestInfo);
                            }

                            AddClipToCompositeManifestInfo(shot, manifestStream, compositeManifestInfo);
                            this.AddRubberBandingPoints(shot, compositeManifestInfo);
                            this.AddTransitions(shot, compositeManifestInfo);
                        }
                    }
                }

                if (track.TrackType == TrackType.Visual)
                {
                    compositeManifestInfo.OverlayDataStreamName = "Overlay";
                    var overlaysTrack = this.sequenceRegistry.CurrentSequenceModel.Tracks.First(t => t.TrackType == TrackType.Overlay);

                    foreach (TimelineElement shot in overlaysTrack.Shots)
                    {
                        this.AddOverlay(shot, compositeManifestInfo);
                    }
                }

                var userSettings = this.userSettingsService.GetSettings();

                SmoothStreamingManifestWriter writer = new SmoothStreamingManifestWriter();
                string manifest = writer.GenerateCompositeManifest(
                    compositeManifestInfo,
                    false,
                    false,
                    userSettings.MinBitrate,
                    userSettings.MaxBitrate,
                    userSettings.IsSingleBitrate);

                manifestByTrack[track] = manifest;
            }

            this.dispatcher.BeginInvoke(() => this.actionCallback(manifestByTrack));
        }

        private void AddOverlay(TimelineElement shot, CompositeManifestInfo compositeManifestInfo)
        {
            var overlayAsset = shot.Asset as OverlayAsset;

            if (overlayAsset != null)
            {
                var dict = new Dictionary<string, object>();
                overlayAsset.Metadata.ForEach(mf => dict.Add(mf.Name, mf.Value));

                const double Timescale = 10000000.0;

                compositeManifestInfo.AddOverlay(
                    overlayAsset.Title,
                    overlayAsset.PositionX,
                    overlayAsset.PositionY,
                    overlayAsset.Height,
                    overlayAsset.Width,
                    dict,
                    overlayAsset.XamlResource,
                    (ulong)(shot.Position.TotalSeconds * Timescale),
                    (ulong)((shot.Position.TotalSeconds + shot.Duration.TotalSeconds) * Timescale));
            }
        }

        private void AddTransitions(TimelineElement shot, CompositeManifestInfo compositeManifestInfo)
        {
            const double Timescale = 10000000.0;

            if (!(shot.Asset is VideoAsset) && !(shot.Asset is AudioAsset))
            {
                return;
            }

            AssetType assetType = (shot.Asset is VideoAsset) ? AssetType.Video : AssetType.Audio;

            ulong position = (ulong)(shot.Position.TotalSeconds * Timescale);
            AddTransition(SmoothStreamingManifestGenerator.Models.TransitionType.FadeIn, assetType, position, shot.InTransition.Duration, compositeManifestInfo);

            position = (ulong)((shot.Position.TotalSeconds + shot.Duration.TotalSeconds - shot.OutTransition.Duration) * Timescale);
            AddTransition(SmoothStreamingManifestGenerator.Models.TransitionType.FadeOut, assetType, position, shot.OutTransition.Duration, compositeManifestInfo);
        }

        private void AddRubberBandingPoints(TimelineElement element, CompositeManifestInfo compositeManifestInfo)
        {
            const ulong Timescale = 10000000;

            List<Point> elementVolumeCollection = element.VolumeNodeCollection;
            foreach (Point point in elementVolumeCollection)
            {
                double volume = 1 - point.Y;
                long frames = (long)point.X;
                double totalSeconds = TimeCode.FromFrames(frames, this.sequenceRegistry.CurrentSequenceModel.CurrentPosition.FrameRate).TotalSeconds + element.Position.TotalSeconds;

                long ticks = (long)(totalSeconds * Timescale);

                compositeManifestInfo.AddRubberBandingPoint(ticks, volume);
            }
        }

        private bool CanGenerateManifest()
        {
            // All downloads that are neccessary have been completed.
            return this.startedAllDownloads && this.pendingDownloads.Count == 0;
        }

        private void GetGapAttributesFromUserSettings(IConfigurationService configurationSettings)
        {
            this.gapUri = new Uri(configurationSettings.GetParameterValue("GapVideoUrl"));
            this.gapCmsId = configurationSettings.GetParameterValue("GapCMSId");
            this.gapAzureId = configurationSettings.GetParameterValue("GapAzureId");
        }
    }
}
