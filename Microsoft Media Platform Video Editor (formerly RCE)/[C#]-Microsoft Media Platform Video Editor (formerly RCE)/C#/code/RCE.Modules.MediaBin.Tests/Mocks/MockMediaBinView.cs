// <copyright file="MockMediaBinView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMediaBinView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using System.Collections.Generic;
    using Services.Contracts;
    using SMPTETimecode;

    public class MockMediaBinView : IMediaBinView
    {
        public IMediaBinViewPresentationModel Model { get; set; }
        
        public bool RefreshElementCalled { get; set; }

        public bool UpdateSmpteFrameRateCalled { get; set; }

        public SmpteFrameRate CurrentSmpteRate { get; set; }

        public bool ShowProgressBarCalled { get; set; }

        public bool HideProgressBarCalled { get; set; }

        public bool GetDeleteAssetConfirmationCalled { get; set; }

        public void RefreshElement(double timePosition)
        {
            this.RefreshElementCalled = true;
        }

        public void SetCurrentSmpteFrameRate(SmpteFrameRate frameRate)
        {
            this.UpdateSmpteFrameRateCalled = true;
            this.CurrentSmpteRate = frameRate;
        }

        public void AddMetadataFields(IList<string> metadataFields)
        {
        }

        public void ShowProgressBar()
        {
            this.ShowProgressBarCalled = true;
        }

        public void HideProgressBar()
        {
            this.HideProgressBarCalled = true;
        }

        public void GetDeleteAssetConfirmation()
        {
            this.GetDeleteAssetConfirmationCalled = true;
        }
    }
}
