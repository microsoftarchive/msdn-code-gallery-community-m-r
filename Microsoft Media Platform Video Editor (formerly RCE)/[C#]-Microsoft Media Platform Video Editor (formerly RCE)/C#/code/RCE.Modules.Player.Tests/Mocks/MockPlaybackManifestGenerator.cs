// <copyright file="MockPlaybackManifestGenerator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPlaybackManifestGenerator.cs                     
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

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Player.Services;

    public class MockPlaybackManifestGenerator : IPlaybackManifestGenerator
    {
        public event EventHandler<DataEventArgs<IDictionary<Track, string>>> ManifestGenerationCompleted;

        public void BeginManifestGeneration(Action<IDictionary<Track, string>> callback)
        {
            throw new NotImplementedException();
        }
    }
}
