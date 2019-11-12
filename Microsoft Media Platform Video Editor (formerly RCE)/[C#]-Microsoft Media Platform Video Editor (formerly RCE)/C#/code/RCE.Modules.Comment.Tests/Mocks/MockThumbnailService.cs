// <copyright file="MockThumbnailService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockThumbnailService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Mocks
{
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    using SMPTETimecode;

    public class MockThumbnailService : IThumbnailService
    {
        public MockThumbnailService()
        {
            this.GetThumbnailSourceResult = string.Empty;
        }

        public string GetThumbnailSourceResult { get; set; }

        public string GetThumbnailSource(Asset asset)
        {
            return this.GetThumbnailSourceResult;
        }

        public string GetThumbnailSource(Asset asset, TimeCode timeCode)
        {
            return this.GetThumbnailSourceResult;
        }

        public string GetThumbnailSource(Asset asset, int seconds, double width, double height)
        {
            return this.GetThumbnailSourceResult;
        }
    }
}
