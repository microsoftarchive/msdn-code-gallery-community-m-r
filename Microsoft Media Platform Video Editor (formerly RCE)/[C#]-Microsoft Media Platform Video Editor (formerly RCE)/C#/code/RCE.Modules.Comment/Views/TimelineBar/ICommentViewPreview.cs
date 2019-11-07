// <copyright file="ICommentViewPreview.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICommentViewPreview.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment
{
    using SMPTETimecode;

    public interface ICommentViewPreview
    {
        ICommentEditBoxPresentationModel Model { get; set; }

        void UpdateCommentDuration(TimeCode commentDuration);

        void RefreshPreview(double width);

        void SetTimelineDuration(TimeCode currentDuration);
    }
}