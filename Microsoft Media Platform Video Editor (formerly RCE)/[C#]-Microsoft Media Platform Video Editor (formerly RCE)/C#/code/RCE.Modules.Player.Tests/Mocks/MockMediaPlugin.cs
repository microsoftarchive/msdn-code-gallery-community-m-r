// <copyright file="MockMediaPlugin.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMediaPlugin.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;

    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

    public class MockMediaPlugin : IMediaPlugin
    {
        public event Action<IPlugin, LogEntry> LogReady;

        public event Action<IPlugin> PluginLoaded;

        public event Action<IPlugin> PluginUnloaded;

        public event Action<IPlugin, Exception> PluginLoadFailed;

        public event Action<IPlugin, Exception> PluginUnloadFailed;

        public event Action<IMediaPlugin, double> BufferingProgressChanged;

        public event Action<IMediaPlugin, double, double> DownloadProgressChanged;

        public event Action<IMediaPlugin, MediaMarker> MarkerReached;

        public event Action<IMediaPlugin> MediaEnded;

        public event Action<IMediaPlugin, Exception> MediaFailed;

        public event Action<IMediaPlugin> MediaOpened;

        public event Action<IMediaPlugin> PlaybackRateChanged;

        public event Action<IMediaPlugin, MediaPluginState> CurrentStateChanged;

        public event Action<IMediaPlugin, bool> SeekCompleted;

        public event Action<IAdaptiveMediaPlugin, IAdContext> AdClickThrough;

        public event Action<IAdaptiveMediaPlugin, IAdContext> AdError;

        public event Action<IAdaptiveMediaPlugin, IAdContext, AdProgress> AdProgressUpdated;

        public bool IsLoaded { get; private set; }

        public bool AutoPlay { get; set; }

        public CacheMode CacheMode { get; set; }

        public double Balance { get; set; }

        public double BufferingProgress { get; private set; }

        public TimeSpan BufferingTime { get; set; }

        public bool CanPause { get; private set; }

        public bool CanSeek { get; private set; }

        public MediaPluginState CurrentState { get; private set; }

        public double DownloadProgress { get; private set; }

        public TimeSpan Duration { get; private set; }

        public TimeSpan EndPosition { get; private set; }

        public bool IsMuted { get; set; }

        public LicenseAcquirer LicenseAcquirer { get; set; }

        public double PlaybackRate { get; set; }

        public TimeSpan Position { get; set; }

        public TimeSpan StartPosition { get; private set; }

        public DeliveryMethods SupportedDeliveryMethods { get; private set; }

        public IEnumerable<double> SupportedPlaybackRates { get; private set; }

        public double Volume { get; set; }

        public Uri Source { get; set; }

        public Stream StreamSource { get; set; }

        public Size NaturalVideoSize { get; private set; }

        public Stretch Stretch { get; set; }

        public bool EnableGPUAcceleration { get; set; }

        public FrameworkElement VisualElement { get; private set; }

        public double DroppedFramesPerSecond { get; private set; }

        public double RenderedFramesPerSecond { get; private set; }

        public bool SupportsAdScheduling { get; private set; }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void RequestLog()
        {
            throw new NotImplementedException();
        }

        public IAdContext ScheduleAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? duration, TimeSpan? startTime, TimeSpan? startOffset, Uri clickThrough, bool pauseTimeline, IAdContext appendToAd, object data)
        {
            throw new NotImplementedException();
        }


        public event Action<IAdaptiveMediaPlugin, IAdContext> AdStateChanged;

        public TimeSpan ClipPosition
        {
            get { throw new NotImplementedException(); }
        }

        public double DownloadProgressOffset
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDecodingOnGPU
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<MediaDrmSetupDecryptorCompletedEventArgs> MediaDrmSetupDecryptorCompleted;

        public IAdContext ScheduleAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? duration = null, TimeSpan? startTime = null, TimeSpan? startOffset = null, Uri clickThrough = null, bool pauseTimeline = true, IAdContext appendToAd = null, object data = null, bool isLinearClip = false)
        {
            throw new NotImplementedException();
        }
    }
}
