// <copyright file="CommentType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentType.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    /// <summary>
    /// Specifies the type of the <see cref="Comment"/>.
    /// </summary>
    public enum CommentType
    {
        /// <summary>
        /// If it is Global comment.
        /// </summary>
        Global,

        /// <summary>
        /// If it is Timeline comment.
        /// </summary>
        Timeline,

        /// <summary>
        /// If it is Shot comment.
        /// </summary>
        Shot,

        /// <summary>
        /// If it is Ink comment.
        /// </summary>
        Ink
    }
}
