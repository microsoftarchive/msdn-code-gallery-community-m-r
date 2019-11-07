// <copyright file="MetadataSearchKeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataSearchKeyboardBehavior.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Behaviors
{
    using RCE.Infrastructure;
    using RCE.Infrastructure.Behaviors;

    public class MetadataSearchKeyboardBehavior : TextBoxKeyboardBehavior
    {
        protected override KeyboardActionContext GetKeyboardActionContext()
        {
            return KeyboardActionContext.Metadata;
        } 
    }
}
