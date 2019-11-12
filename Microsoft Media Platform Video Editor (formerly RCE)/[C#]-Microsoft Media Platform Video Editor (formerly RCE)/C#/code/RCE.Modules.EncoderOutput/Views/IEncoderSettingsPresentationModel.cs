// <copyright file="IEncoderSettingsPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IEncoderSettingsPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput.Views
{
    using System.Collections.Generic;
    using Infrastructure;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using RCE.Services.Contracts.Output;

    public interface IEncoderSettingsPresentationModel : IHeaderInfoProvider<string>
    {
        IEncoderSettingsView View { get; }

        OutputMetadata Metadata { get; }

        List<string> ResizeModeOptions { get; }

        List<string> AspectRatioOptions { get; }

        List<double> FrameRateOptions { get; }

        bool CompressManifest { get; set; }

        DelegateCommand<object> GenerateOutputCommand { get; }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>The header info.</value>
        string HeaderIconOn { get; }

        /// <summary>
        /// Gets the Header Icon.
        /// </summary>
        /// <value>The header icon name.</value>
        string HeaderIconOff { get; }

        string ExportMessage { get; }

        bool IsCsmOutput { get; set; }

        string PbpDataStreamName { get; set; }

        string AdsDataStreamName { get; set; }
    }
}