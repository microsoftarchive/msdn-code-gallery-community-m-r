// <copyright file="MediaBinSearchKeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaBinSearchKeyboardBehavior.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Views.MediaBin.Behaviors
{
    using RCE.Infrastructure;
    using RCE.Infrastructure.Behaviors;

    public class MediaBinSearchKeyboardBehavior : TextBoxKeyboardBehavior
    {
        protected override KeyboardActionContext GetKeyboardActionContext()
        {
            return KeyboardActionContext.MediaBin;
        } 
    }
}