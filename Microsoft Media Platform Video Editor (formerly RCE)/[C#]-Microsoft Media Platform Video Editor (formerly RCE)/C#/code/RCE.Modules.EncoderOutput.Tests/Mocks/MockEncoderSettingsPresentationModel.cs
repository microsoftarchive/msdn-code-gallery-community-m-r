// <copyright file="MockEncoderSettingsPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTitlesViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput.Tests.Mocks
{
    using System.Collections.Generic;
    using EncoderOutput.Views;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using RCE.Services.Contracts.Output;

    public class MockEncoderSettingsPresentationModel : IEncoderSettingsPresentationModel
    {
        public MockEncoderSettingsPresentationModel()
        {
            this.View = new MockEncoderSettingsView();
        }

        public string HeaderInfo { get; private set; }

        public IEncoderSettingsView View { get; private set; }
        
        public OutputMetadata Metadata { get; private set; }
        
        public List<string> ResizeModeOptions { get; private set; }
        
        public List<string> AspectRatioOptions { get; private set; }
        
        public List<double> FrameRateOptions { get; private set; }

        public bool CompressManifest { get; set; }
        
        public DelegateCommand<object> GenerateOutputCommand { get; private set; }

        public string HeaderIconOn { get; set; }

        public string HeaderIconOff { get; set; }

        public string ExportMessage { get; private set; }

        public bool IsCsmOutput { get; set; }

        public string PbpDataStreamName { get; set; }

        public string AdsDataStreamName { get; set; }
    }
}