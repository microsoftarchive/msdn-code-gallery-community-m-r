// <copyright file="IRCESmoothStreamingMediaPlugin.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IRCESmoothStreamingMediaPlugin.cs                     
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
    using System.IO;

    using Microsoft.SilverlightMediaFramework.Core;
    using Microsoft.SilverlightMediaFramework.Core.Media;
    using Microsoft.SilverlightMediaFramework.Plugins;

    public interface IRCESmoothStreamingMediaPlugin : IAdaptiveMediaPlugin, ILiveDvrMediaPlugin, IDisposable
    {
        event Action<IRCESmoothStreamingMediaPlugin> ManifestMerge;

        TimeSpan PositionLiveBuffer { get; set; }

        string AudioStreamName { get; set; }

        string VideoStreamName { get; set; }

        PlaySpeedManager PlaySpeedManager { get; }

        IEnumerable<StreamMetadata> AvailableAudioStreams { get; }

        bool IsStereo { get; }

        StreamMetadata SelectedAudioStream { get; set; }

        void ParseExternalManifest(Uri externalManifestUri, int millisecondsTimeout, out object externalManifest);

        void MergeExternalManifest(object externalManifest);

        void StartSeekToLive();

        void FastRewind();

        void FastForward();

        void SlowMotion();

        void SelectTracks(string key, string value, long minBitrate, long maxBitrate);

        void SelectMaxAvailableBitrateTracks(string key, string value);

        void SetManifestStreamSource(Stream manifestStream);
    }
}