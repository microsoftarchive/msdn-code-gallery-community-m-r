// <copyright file="RCESmoothStreamingMediaPlugin.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RCESmoothStreamingMediaPlugin.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.SilverlightMediaFramework.Core;
    using Microsoft.SilverlightMediaFramework.Core.Media;
    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
    using Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming;
    using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
    using Microsoft.Web.Media.SmoothStreaming;

    using RCE.Infrastructure.Services;

    public class RCESmoothStreamingMediaPlugin : SmoothStreamingMediaPlugin, IRCESmoothStreamingMediaPlugin
    {
        private const long DefaultPositionLiveBufferMillis = 3000;

        private const string ChannelsAttribute = "channels";
        
        private static CookieContainer cookies;
        
        private StreamMetadata selectedAudioStream;

        /// <summary>
        /// Stores the token cookies.
        /// </summary>
        private object smoothStreamingCache;

        private long? minBitrate;

        private long? maxBitrate;

        private string cameraAttribute;

        private string cameraAttributeValue;

        private bool singleBitrate;

        private IUserSettingsService userSettingsService;

        private IConfigurationService configurationService;

        private bool manifestReady;

        private string videoStreamName;

        public RCESmoothStreamingMediaPlugin()
        {
            this.Load();

            this.MediaElement.LivePlaybackOffset = TimeSpan.FromSeconds(10);
            CookiesChanged += this.RCESmoothStreamingMediaPlugin_CookiesChanged;
            this.PluginUnloaded += this.OnPluginUnloaded;

            this.Configure();

            this.PlaySpeedManager = new PlaySpeedManager(this);
            this.MediaElement.ManifestMerge += this.MediaElement_ManifestMerge;
            this.ManifestReady += this.MediaElement_ManifestReady;
            this.PositionLiveBuffer = TimeSpan.FromMilliseconds(DefaultPositionLiveBufferMillis);
            this.MediaElement.CookieContainer = Cookies;
            this.SetSmoothStreamingCache();
        }

        /// <summary>
        /// Occurs when the cookies change.
        /// </summary>
        public static event EventHandler CookiesChanged;

        public event Action<IRCESmoothStreamingMediaPlugin> ManifestMerge;

        /// <summary>
        /// Gets or sets the <seealso cref="CookieContainer"/>.
        /// </summary>
        /// <value>The cookie container that stores the toke cookies.</value>
        public static CookieContainer Cookies
        {
            get
            {
                return cookies ?? (cookies = new CookieContainer());
            }

            set
            {
                cookies = value;
                OnCookiesChanged();
            }
        }

        public TimeSpan PositionLiveBuffer { get; set; }

        public PlaySpeedManager PlaySpeedManager { get; private set; }

        public string AudioStreamName { get; set; }

        public string VideoStreamName
        {
            get
            {
                return this.videoStreamName;
            }

            set
            {
                this.videoStreamName = value;
                if (this.manifestReady)
                {
                    this.SelectPlaylistVideoStream();
                }
            }
        }

        public IEnumerable<StreamMetadata> AvailableAudioStreams { get; private set; }

        public StreamMetadata SelectedAudioStream
        {
            get
            {
                return this.selectedAudioStream;
            }

            set
            {
                this.selectedAudioStream = value;
                this.SelectAudioStream();
            }
        }

        public bool IsStereo
        {
            get
            {
                IMediaStream stream = this.CurrentSegment.SelectedStreams.Where(x => x.Type == StreamType.Audio).FirstOrDefault();

                if (stream != null)
                {
                    IMediaTrack track = stream.SelectedTracks.FirstOrDefault();

                    if (track != null)
                    {
                        int channels = GetChannels(track);
                        return channels > 1;
                    }
                }

                return false;
            }
        }

        protected string AudioStreamLanguage { get; set; }

        private bool IsMediaPluginPlayReady
        {
            get
            {
                return this.CurrentState == MediaPluginState.Buffering
                           || this.CurrentState == MediaPluginState.Paused
                           || this.CurrentState == MediaPluginState.Playing
                           || this.CurrentState == MediaPluginState.ClipPlaying;
            }
        }

        public void Dispose()
        {
        }

        public void ParseExternalManifest(Uri externalManifestUri, int millisecondsTimeout, out object externalManifest)
        {
            this.MediaElement.ParseExternalManifest(externalManifestUri, millisecondsTimeout, out externalManifest);
        }

        public void MergeExternalManifest(object externalManifest)
        {
            this.MediaElement.MergeExternalManifest(externalManifest);
        }

        public void StartSeekToLive()
        {
            if (this.IsMediaPluginPlayReady && this.IsSourceLive && !this.IsLivePosition)
            {
                this.SeekToLive();
            }
        }

        public void FastRewind()
        {
            if (this.MediaElement != null && this.PlaySpeedManager != null && this.IsMediaPluginPlayReady)
            {
                if (this.PlaySpeedManager.IsRewinding)
                {
                    if (this.PlaySpeedManager.CanIncrementRewind)
                    {
                        this.PlaySpeedManager.IncrementRewind();
                    }
                    else
                    {
                        this.PlaySpeedManager.RestoreNaturalPlaySpeed();
                    }
                }
                else
                {
                    this.PlaySpeedManager.Rewind();
                }
            }
        }

        public void FastForward()
        {
            if (this.MediaElement != null && this.PlaySpeedManager != null && this.IsMediaPluginPlayReady)
            {
                if (this.PlaySpeedManager.IsFastForwarding)
                {
                    if (this.PlaySpeedManager.CanIncrementFastForward)
                    {
                        this.PlaySpeedManager.IncrementFastForward();
                    }
                    else
                    {
                        this.PlaySpeedManager.RestoreNaturalPlaySpeed();
                    }
                }
                else
                {
                    this.PlaySpeedManager.FastForward();
                }
            }
        }

        public void SlowMotion()
        {
            if (this.MediaElement != null && this.PlaySpeedManager != null && this.IsMediaPluginPlayReady)
            {
                if (this.PlaySpeedManager.IsSlowMotion)
                {
                    this.PlaySpeedManager.RestoreNaturalPlaySpeed();
                }
                else
                {
                    this.PlaySpeedManager.SlowMotion();
                }
            }
        }

        public void SelectMaxAvailableBitrateTracks(string key, string value)
        {
            IMediaStream videoStream = this.CurrentSegment.AvailableStreams.FirstOrDefault(x => x.Type == StreamType.Video);

            if (videoStream != null)
            {
                long bitrate = videoStream.AvailableTracks.Max(x => x.Bitrate);

                this.SelectTracks(key, value, bitrate, bitrate);
            }
        }

        public void SetManifestStreamSource(Stream manifestStream)
        {
            this.StreamSource = manifestStream;
        }

        public void SelectTracks(string key, string value, long minBitrate, long maxBitrate)
        {
            if (this.CurrentSegment != null)
            {
                IMediaStream videoStream = this.CurrentSegment.AvailableStreams.FirstOrDefault(x => x.Type == StreamType.Video);

                if (videoStream != null)
                {
                    bool attributeAvailable = false;
                    IList<IMediaTrack> tracks = new List<IMediaTrack>();

                    if (key != null && value != null)
                    {
                        foreach (IMediaTrack trackInfo in videoStream.AvailableTracks)
                        {
                            string keyValue;

                            trackInfo.CustomAttributes.TryGetValue(key, out keyValue);

                            if (!string.IsNullOrEmpty(keyValue) && keyValue.ToUpper(CultureInfo.InvariantCulture) == value.ToUpper(CultureInfo.InvariantCulture))
                            {
                                attributeAvailable = true;
                                if (trackInfo.Bitrate >= minBitrate && trackInfo.Bitrate <= maxBitrate)
                                {
                                    tracks.Add(trackInfo);
                                }
                            }
                        }
                    }

                    if (!attributeAvailable)
                    {
                        tracks = videoStream.AvailableTracks.Where(x => x.Bitrate >= minBitrate && x.Bitrate <= maxBitrate).ToList();
                    }

                    if (tracks.Count > 0)
                    {
                        if (this.singleBitrate && tracks.Count > 1)
                        {
                            long bitrate = tracks.Max(x => x.Bitrate);

                            IMediaTrack track = tracks.FirstOrDefault(x => x.Bitrate == bitrate);

                            tracks.Clear();
                            tracks.Add(track);
                        }

                        videoStream.SetSelectedTracks(tracks);
                    }
                }
            }
        }

        /// <summary>
        /// Raises the CookieChanged event.
        /// </summary>
        private static void OnCookiesChanged()
        {
            EventHandler handler = CookiesChanged;
            if (handler != null)
            {
                handler(null, EventArgs.Empty);
            }
        }

        private static bool IsCameraAttribute(KeyValuePair<string, string> kvp, string camera)
        {
            return string.Equals(kvp.Key, "cameraAngle", StringComparison.OrdinalIgnoreCase) && string.Equals(kvp.Value, camera, StringComparison.OrdinalIgnoreCase);
        }

        private static int GetChannels(IMediaTrack track)
        {
            int result;
            int.TryParse(track.Attributes.GetEntryIgnoreCase(ChannelsAttribute), out result);
            return result;
        }

        private void Configure()
        {
            try
            {
                this.userSettingsService = ServiceLocator.Current.GetInstance<IUserSettingsService>();
                this.configurationService = ServiceLocator.Current.GetInstance<IConfigurationService>();
                this.userSettingsService.SettingsChanged += this.OnSettingsChanged;

                this.RetrieveSettings();
            }
            catch
            {
            }
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            this.RetrieveSettings();
            this.SelectTracks();
        }

        private void RetrieveSettings()
        {
            this.minBitrate = this.userSettingsService.GetSettings().MinBitrate;
            this.maxBitrate = this.userSettingsService.GetSettings().MaxBitrate;
            this.singleBitrate = this.userSettingsService.GetSettings().IsSingleBitrate;
            this.cameraAttribute = this.configurationService.GetParameterValue("CameraAttribute");
            this.cameraAttributeValue = this.configurationService.GetParameterValue("CameraAttributeValue");
        }

        private void SelectTracks()
        {
            if (this.minBitrate.HasValue && this.maxBitrate.HasValue)
            {
                this.SelectTracks(this.cameraAttribute, this.cameraAttributeValue, this.minBitrate.Value, this.maxBitrate.Value);
            }
        }

        private void MediaElement_ManifestReady(IAdaptiveMediaPlugin adaptiveMediaPlugin)
        {
            this.manifestReady = true;
            this.SelectTracks();
            this.UpdateAvailableAudioTracks();
            this.SelectPlaylistAudioStream();
            this.SelectPlaylistVideoStream();
        }

        private void MediaElement_ManifestMerge(SmoothStreamingMediaElement ssme)
        {
            this.SetSmoothStreamingCache();
            this.ManifestMerge.IfNotNull(i => i(this));
        }

        private void UpdateAvailableAudioTracks()
        {
            try
            {
                this.AvailableAudioStreams = this.CurrentSegment != null &&
                                             this.CurrentSegment.AvailableStreams != null
                                                 ? this.CurrentSegment.AvailableStreams.Where(
                                                     i => i.Type == StreamType.Audio).Select(
                                                         i =>
                                                         new StreamMetadata
                                                             {
                                                                 Id = i.Id,
                                                                 Attributes = i.Attributes
                                                             }).OrderBy(i => i.Name).ToList() : Enumerable.Empty<StreamMetadata>();
            }
            catch (Exception err)
            {
            }
        }

        private void SelectPlaylistVideoStream()
        {
            if (string.IsNullOrEmpty(this.VideoStreamName))
            {
                return;
            }

            ISegment segment = this.CurrentSegment;

            if (segment != null && segment.AvailableStreams != null)
            {
                IMediaStream videoStream = segment.AvailableStreams.Where(s => s.Type == StreamType.Video).FirstOrDefault();

                if (videoStream == null)
                {
                    return;
                }

                // get all media tracks that which match the camera name (might be one per bitrate)
                IEnumerable<IMediaTrack> tracks =
                    videoStream.AvailableTracks.Where(
                        t =>
                        t.CustomAttributes.Any(kvp => IsCameraAttribute(kvp, this.VideoStreamName)));

                if (tracks.Count() > 0)
                {
                    videoStream.SetSelectedTracks(tracks);
                }
            }
        }

        private void SelectPlaylistAudioStream()
        {
            if (!string.IsNullOrWhiteSpace(this.AudioStreamName) ||
                       !string.IsNullOrWhiteSpace(this.AudioStreamLanguage))
            {
                this.SelectedAudioStream =
                    this.AvailableAudioStreams.Where(
                        i =>
                        string.IsNullOrWhiteSpace(this.AudioStreamName) ||
                        string.Equals(i.Name, this.AudioStreamName, StringComparison.CurrentCultureIgnoreCase)).Where(
                            i =>
                            string.IsNullOrWhiteSpace(this.AudioStreamLanguage) ||
                            string.Equals(
                                i.Language, this.AudioStreamLanguage, StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();
            }
            else
            {
                this.UpdateSelectedAudioStream();
            }
        }

        private void UpdateSelectedAudioStream()
        {
            try
            {
                this.SelectedAudioStream = this.CurrentSegment != null &&
                                           this.CurrentSegment.SelectedStreams != null
                                               ? this.CurrentSegment.SelectedStreams.Where(
                                                   i => i.Type == StreamType.Audio).Select(
                                                       i => new StreamMetadata { Id = i.Id, Attributes = i.Attributes })
                                                     .FirstOrDefault()
                                               : null;
            }
            catch (Exception err)
            {
            }
        }

        private void SelectAudioStream()
        {
            if (this.SelectedAudioStream != null
                && this.CurrentSegment != null
                && this.CurrentSegment.AvailableStreams != null
                && this.CurrentSegment.SelectedStreams != null
                && !this.CurrentSegment.SelectedStreams.Any(i => i.Id == this.SelectedAudioStream.Id))
            {
                try
                {
                    // Get currently selected streams
                    List<IMediaStream> audioStreams =
                        this.CurrentSegment.AvailableStreams.Where(i => i.Type == StreamType.Audio).ToList();

                    IMediaStream audioStream = audioStreams
                                                         .Where(i => i.Id == this.SelectedAudioStream.Id)
                                                         .FirstOrDefault();
                    
                    // Remove all audio streams from the list
                    List<IMediaStream> streamsToRemove =
                        audioStreams.Where(i => i.Id != audioStream.Id)
                        .ToList();

                    if (audioStream != null && audioStream.AvailableTracks.Count() > 0)
                    {
                        // If one exists w/ the currently specified language add it to the list and set the streams
                        List<IMediaStream> streamsToAdd = new List<IMediaStream>();
                        streamsToAdd.Add(audioStream);
                        this.ModifySegmentSelectedStreams(this.CurrentSegment, streamsToAdd, streamsToRemove);
                    }
                    else
                    {
                        this.UpdateSelectedAudioStream();
                    }
                }
                catch (Exception err)
                {
                }
            }
        }

        private void RCESmoothStreamingMediaPlugin_CookiesChanged(object sender, EventArgs e)
        {
            this.MediaElement.CookieContainer = Cookies;
        }

        private void SetSmoothStreamingCache()
        {
            if (this.smoothStreamingCache == null && this.MediaElement != null)
            {
                try
                {
                    ICache smoothStreamingCache = ServiceLocator.Current.GetInstance<ICache>();

                    if (smoothStreamingCache != null)
                    {
                        this.smoothStreamingCache = smoothStreamingCache;
                        this.SetCacheProvider(smoothStreamingCache);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void OnPluginUnloaded(IPlugin obj)
        {
            this.PluginUnloaded -= this.OnPluginUnloaded;
            this.ManifestReady -= this.MediaElement_ManifestReady;
            CookiesChanged -= this.RCESmoothStreamingMediaPlugin_CookiesChanged;
            if (this.userSettingsService != null)
            {
                this.userSettingsService.SettingsChanged -= this.OnSettingsChanged;
            }
        }
    }
}