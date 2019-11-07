// <copyright file="MockMarkerEditBox.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMarkerEditBox.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Tests.Mocks
{
    public class MockMarkerEditBox : IMarkerEditBox
    {
        public IMarkerEditBoxPresentationModel Model { get; set; }

        public bool ShowCalled { get; set; }

        public bool CloseCalled { get; set; }

        public void Close()
        {
            this.CloseCalled = true;
        }

        public void Show()
        {
            this.ShowCalled = true;
        }
    }
}
