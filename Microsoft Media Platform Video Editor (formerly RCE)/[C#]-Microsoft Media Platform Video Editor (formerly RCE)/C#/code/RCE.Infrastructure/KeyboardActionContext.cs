// <copyright file="KeyboardActionContext.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: KeyboardActionContext.cs                     
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
    /// <summary>
    /// Context in wich the key was pressed.
    /// </summary>
    public enum KeyboardActionContext
    {
        /// <summary>
        /// Asset Browser Context.
        /// </summary>
        AssetBrowser,
        
        /// <summary>
        /// Comment Context.
        /// </summary>
        Comment,

        /// <summary>
        /// CommentEdit Context.
        /// </summary>
        CommentEdit,

        /// <summary>
        /// Library Context.
        /// </summary>
        Library,

        /// <summary>
        /// Mediabin Context.
        /// </summary>
        MediaBin,

        /// <summary>
        /// Metadata Context.
        /// </summary>
        Metadata,

        /// <summary>
        /// Player Context.
        /// </summary>
        Player,

        /// <summary>
        /// Projects Context.
        /// </summary>
        Projects,

        /// <summary>
        /// Search Context.
        /// </summary>
        Search,

        /// <summary>
        /// Settings Context.
        /// </summary>
        Settings,

        /// <summary>
        /// Shell Context.
        /// </summary>
        Shell,

        /// <summary>
        /// Timeline Context.
        /// </summary>
        Timeline,

        /// <summary>
        /// TimelineControl Context.
        /// </summary>
        TimelineControl,

        /// <summary>
        /// Titles Context.
        /// </summary>
        Titles,

        /// <summary>
        /// VideoPreviewTimeline Context.
        /// </summary>
        VideoPreviewTimeline,

        /// <summary>
        /// SubClip Context.
        /// </summary>
        SubClip
    }
}
