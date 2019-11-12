// <copyright file="MockSubClipView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSubClipView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Tests.Mocks
{
    using RCE.Modules.SubClip.Views;

    public class MockSubClipView : ISubClipView
    {
        public object SetDataContextParameter { get; set; }

        public void SetDataContext(object viewModel)
        {
            this.SetDataContextParameter = viewModel;
        }
    }
}
