// <copyright file="MockCommentViewPreview.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCommentViewPreview.cs                     
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
    using System;
    using SMPTETimecode;

    public class MockCommentViewPreview : ICommentViewPreview
    {
        public ICommentEditBoxPresentationModel Model { get; set; }

        public bool SetTimelineDurationCalled { get; set; }

        public TimeCode SetTimelineDurationArgument { get; set; }

        public bool RefreshPreviewCalled { get; set; }

        public double? RefreshPreviewArgument { get; set; }

        public bool UpdateCommentDurationCalled { get; set; }

        public TimeCode UpdateCommentDurationArgument { get; set; }

        public void UpdateCommentDuration(TimeCode duration)
        {
            this.UpdateCommentDurationCalled = true;
            this.UpdateCommentDurationArgument = duration;
        }

        public void RefreshPreview(double width)
        {
            this.RefreshPreviewCalled = true;
            this.RefreshPreviewArgument = width;
        }

        public void SetTimelineDuration(TimeCode duration)
        {
            this.SetTimelineDurationCalled = true;
            this.SetTimelineDurationArgument = duration;
        }
    }
}
