// <copyright file="EncoderSettingsPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EncoderSettingsPresentationModel.cs                     
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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;

    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.EncoderOutput.Models;
    using RCE.Services.Contracts.Output;
    using Services;

    public class EncoderSettingsPresentationModel : BaseModel, IEncoderSettingsPresentationModel, IWindowMetadataProvider
    {
        private readonly IProjectService projectService;

        private readonly IOutputServiceFacade outputServiceFacade;

        private readonly IConfigurationService configurationService;

        private string exportMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncoderSettingsPresentationModel"/> class.
        /// </summary>
        public EncoderSettingsPresentationModel(IEncoderSettingsView view, IProjectService projectService, IOutputServiceFacade outputServiceFacade, IConfigurationService configurationService)
        {
            this.View = view;
            this.projectService = projectService;
            this.outputServiceFacade = outputServiceFacade;
            this.configurationService = configurationService;
            this.outputServiceFacade.GenerateOuputCompleted += this.OnGenerateOutputCompleted;
            this.outputServiceFacade.GenerateCompositestreamManifestCompleted += this.OnGenerateCompositestreamManifestCompleted;

            this.Metadata = new OutputMetadata
                                {
                                    Settings = new OutputSettings(),
                                    WindowsMediaHeaderProperties = new WindowsMediaHeaderProperties()
                                };

            this.GenerateOutputCommand = new DelegateCommand<object>(this.GenerateOutput);

            this.ResizeModeOptions = new List<string> { "Stretch", "Letterbox" };

            this.AspectRatioOptions = new List<string> { "Custom", "16:9", "4:3" };

            this.FrameRateOptions = new List<double> { 23.976, 24, 25, 29.97, 30 };

            this.PbpDataStreamName = "PBP";
            this.AdsDataStreamName = "AD";

            this.View.Model = this;
        }

        public event EventHandler<DataEventArgs<object>> TitleUpdated;

        public event EventHandler<DataEventArgs<object>> ResetPositionRaised;

        public IEncoderSettingsView View { get; private set; }

        public OutputMetadata Metadata { get; private set; }

        public List<string> ResizeModeOptions { get; private set; }

        public List<string> AspectRatioOptions { get; private set; }

        public List<double> FrameRateOptions { get; private set; }

        public bool CompressManifest { get; set; }

        public DelegateCommand<object> GenerateOutputCommand { get; private set; }

        public string HeaderInfo
        {
            get { return Resources.Resources.HeaderInfo; }
        }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon off resource.</value>
        public string HeaderIconOff
        {
            get { return Resources.Resources.HeaderIconOff; }
        }

        /// <summary>
        /// Gets the Header Icon.
        /// </summary>
        /// <value>The header icon name.</value>
        public string HeaderIconOn
        {
            get { return Resources.Resources.HeaderIconOn; }
        }

        public bool IsCsmOutput { get; set; }

        public string PbpDataStreamName { get; set; }

        public string AdsDataStreamName { get; set; }

        public string ExportMessage
        {
            get
            {
                return this.exportMessage;
            }

            set
            {
                this.exportMessage = value;
                this.OnPropertyChanged("ExportMessage");
            }
        }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Center;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Center;
            }
        }

        public object Title
        {
            get
            {
                return "Output Generator";
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return Infrastructure.Windows.ResizeDirection.None;
            }
        }

        public Size Size
        {
            get
            {
                return System.Windows.Size.Empty;
            }
        }

        private void GenerateOutput(object obj)
        {
            this.ExportMessage = "Exporting...";
            Project project = this.projectService.GetCurrentProject();

            project.Metadata = this.Metadata;

            this.View.ShowProgressBar();

            if (this.IsCsmOutput)
            {
                this.outputServiceFacade.GenerateCompositeStreamManifestAsync(
                    project, 
                    string.IsNullOrEmpty(this.PbpDataStreamName) ? "PBP" : this.PbpDataStreamName, 
                    string.IsNullOrEmpty(this.AdsDataStreamName) ? "AD" : this.AdsDataStreamName,
                    this.CompressManifest, 
                    this.configurationService.GetParameterValue("GapVideoUrl"),
                    this.configurationService.GetParameterValue("GapCMSId"),
                    this.configurationService.GetParameterValue("GapAzureId"));
            }
            else
            {
                this.outputServiceFacade.GenerateOutputAsync(project);
            }
        }

        private void OnGenerateOutputCompleted(object sender, OutputEventArgs e)
        {
            this.ExportMessage = e.Generated ? "Export Completed" : "Export Failed";
            this.View.HideProgressBar();
        }

        private void OnGenerateCompositestreamManifestCompleted(object sender, OutputEventArgs e)
        {
            this.ExportMessage = e.Generated ? string.Format(CultureInfo.InvariantCulture, "CSM: {0}", e.Result) : "Export Failed";
            this.View.HideProgressBar();
        }
    }
}