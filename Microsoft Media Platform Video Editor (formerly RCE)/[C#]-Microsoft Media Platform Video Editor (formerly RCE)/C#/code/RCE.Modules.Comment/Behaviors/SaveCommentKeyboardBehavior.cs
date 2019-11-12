// <copyright file="SaveCommentKeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SaveCommentKeyboardBehavior.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Behaviors
{
    using RCE.Infrastructure;
    using RCE.Infrastructure.Behaviors;

    public class SaveCommentKeyboardBehavior : TextBoxKeyboardBehavior
    {
        protected override KeyboardActionContext GetKeyboardActionContext()
        {
            return KeyboardActionContext.CommentEdit;
        }
    }
}
