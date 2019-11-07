// <copyright file="MockAssetBrowserViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockAssetBrowserViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Tests.Mocks
{
    using RCE.Modules.Browsers.Views;

    public class MockAssetBrowserViewModel : IAssetBrowserViewModel
    {
        private MockAssetBrowserView view = new MockAssetBrowserView();

        public object View
        {
            get
            {
                return this.view;
            }
        }
    }
}
