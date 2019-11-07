// <copyright file="CommentAction.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentAction.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    /// <summary>
    /// Enum used for comment action mapping.
    /// </summary>
    public enum CommentAction
    {
        /// <summary>
        /// Indicates that the changes to the comment are cancelled.
        /// </summary>
        Cancel = 0,

        /// <summary>
        /// Indicates to create a new comment.
        /// </summary> 
        Create = 1,

        /// <summary>
        /// Indicates to update the new comment.
        /// </summary>
        Update = 2,

        /// <summary>
        /// Indicates to delete a comment.
        /// </summary>
         Delete = 3,

        /// <summary>
        /// Indicates to play a comment.
        /// </summary>
        Play = 4
    }
}