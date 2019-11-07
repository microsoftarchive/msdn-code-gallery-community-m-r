// <copyright file="MockSettingsViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSettingsViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Media;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;

    public class MockSettingsViewPresentationModel : ISettingsViewPresentationModel
    {
        public MockSettingsViewPresentationModel()
        {
            this.View = new MockSettingsView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ISettingsView View { get; set; }

        public IList<string> SmpteTimeCodeValues { get; private set; }

        public IList<string> PixelAspectRatioValues { get; private set; }

        public IList<string> EditModeValues { get; private set; }

        public string ProjectName { get; set; }

        public bool IsAspectRatio43Selected { get; set; }

        public bool IsAspectRatio169Selected { get; set; }

        public string SelectedPixelAspectRatio { get; set; }

        public string SelectedSmpteTimeCode { get; set; }

        public string SelectedFrameRate { get; set; }

        public string SelectedStartTimeCode { get; set; }

        public string SelectedOutputResolution { get; set; }

        public string SelectedEditMode { get; set; }

        public ImageSource ThumbImage { get; set; }

        public DelegateCommand<object> DeleteThumbnailCommand { get; private set; }

        public DelegateCommand<object> PickThumbnailCommand { get; private set; }

        public int SelectedAutoSaveTimeInterval { get; set; }

        public string HeaderInfo { get; set; }

        public string HeaderIconOn { get; set; }

        public string HeaderIconOff { get; set; }

        public DelegateCommand<object> IncreaseStorageQuotaCommand { get; private set; }

        public DelegateCommand<object> ClearStorageCommand { get; private set; }

        public DelegateCommand<object> ApplyKeyboardMappingCommand { get; private set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void SaveProject()
        {
            throw new System.NotImplementedException();
        }
    }
}