// <copyright file="MockManifestMediaModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockManifestMediaModel.cs                     
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
    using System.IO;

    using Microsoft.SilverlightMediaFramework.Plugins;

    using RCE.Infrastructure.Events;
    using RCE.Modules.Player.Models;

    public class MockManifestMediaModel : IManifestMediaModel
    {
        private MockMediaData mediaData = new MockMediaData();

        public event EventHandler FinishedPlaying;

        public event Action<IMediaPlugin> MediaElementFailed;

        public event EventHandler<PositionPayloadEventArgs> PositionUpdated;

        public event EventHandler PersistCompleted;

        public event EventHandler PlayingStateChanged;

        public TimeSpan Position { get; set; }

        public bool IsPlaying { get; private set; }

        public TimeSpan Duration { get; private set; }

        public bool Mute { private get; set; }

        public bool IsStopped { get; private set; }

        public void FastForward()
        {
        }

        public void FastRewind()
        {
        }

        public void Play()
        {
        }

        public void Pause()
        {
        }

        public void SetStreamSource(int trackId, Stream manifestStream)
        {
        }

        public void InvokeMethodForAllMediaData(Action<MediaData> action)
        {
        }

        public void SetSource(int trackId, string manifest)
        {
        }

        public MediaData GetPlayingMediaData()
        {
            return this.mediaData;
        }

        public void Stop()
        {
        }

        public void PlaySlowMotion()
        {
        }

        public MediaData GetVisualMediaData()
        {
            return this.mediaData;
        }

        public void ChangeVolumeSettingsRubberBanding(bool enableRubberBanding, double volumeLevel)
        {
        }

        public void ResetRubberBandingManagers()
        {
        }

        public void ResetTransitionsManagers()
        {
        }

        public void ChangeTrackMute(int trackId, bool isMuted)
        {
            throw new NotImplementedException();
        }
    }
}
