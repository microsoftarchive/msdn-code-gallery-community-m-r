// <copyright file="MockSubClipViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSubClipViewModel.cs                     
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

    public class MockSubClipViewModel : ISubClipViewModel
    {
        private ISubClipView view;
        
        public ISubClipView View
        {
            get
            {
                if (this.view == null)
                {
                    this.view = new MockSubClipView();
                }

                return this.view;
            }
        }
    }
}
