// <copyright file="IManifestMediaModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IManifestMediaModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Models
{
    using System;
    using System.IO;
    using Microsoft.SilverlightMediaFramework.Plugins;

    public interface IManifestMediaModel : IPlayableMediaModel
    {
        event EventHandler PersistCompleted;

        event EventHandler PlayingStateChanged;

        event Action<IMediaPlugin> MediaElementFailed;

        bool IsStopped { get; }

        void SetStreamSource(int trackId, Stream manifestStream);

        void InvokeMethodForAllMediaData(Action<MediaData> action);

        void SetSource(int trackId, string manifest);

        MediaData GetPlayingMediaData();

        void Stop();

        void PlaySlowMotion();

        MediaData GetVisualMediaData();

        void ChangeVolumeSettingsRubberBanding(bool enableRubberBanding, double volumeLevel);

        void ChangeTrackMute(int trackId, bool isMuted);

        void ResetRubberBandingManagers();

        void ResetTransitionsManagers();
    }
}
