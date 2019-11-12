// <copyright file="IPlaybackManifestGenerator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IPlaybackManifestGenerator.cs                     
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

    using RCE.Infrastructure.Models;

    public interface IPlaybackManifestGenerator
    {
        void BeginManifestGeneration(Action<IDictionary<Track, string>> callback);
    }
}
