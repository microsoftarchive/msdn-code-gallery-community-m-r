// <copyright file="MockLibraryView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLibraryView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Tests.Mocks
{
    using System.Collections.Generic;

    public class MockLibraryView : ILibraryView
    {
        public ILibraryViewPresentationModel Model { get; set; }

        public bool ShowProgressBarCalled { get; set; }

        public bool HideProgressBarCalled { get; set; }

        public bool AddMetadataFieldsCalled { get; set; }

        public void AddMetadataFields(IList<string> metadataFields)
        {
            this.AddMetadataFieldsCalled = true;
        }

        public void ShowProgressBar()
        {
            this.ShowProgressBarCalled = true;
        }

        public void HideProgressBar()
        {
            this.HideProgressBarCalled = false;
        }
    }
}