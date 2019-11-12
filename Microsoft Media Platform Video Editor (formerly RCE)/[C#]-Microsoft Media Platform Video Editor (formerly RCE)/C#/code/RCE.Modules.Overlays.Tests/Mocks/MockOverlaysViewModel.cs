// <copyright file="MockOverlaysViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockOverlaysViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Overlays.Tests.Mocks
{
    using RCE.Modules.Overlays.Views;

    public class MockOverlaysViewModel : IOverlaysViewModel
    {
        private IOverlaysView view = new MockOverlaysView();

        public object View
        {
            get
            {
                return this.view;
            }
        }
    }
}
