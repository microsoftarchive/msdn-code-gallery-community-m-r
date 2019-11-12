// <copyright file="MockLibraryViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLibraryViewPresentationModel.cs                     
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;

    public class MockLibraryViewPresentationModel : ILibraryViewPresentationModel
    {
        public MockLibraryViewPresentationModel()
        {
            this.View = new MockLibraryView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ILibraryView View { get; set; }

        public List<Asset> Assets { get; set; }

        public bool ShowVideos { get; set; }

        public bool ShowAudio { get; set; }

        public bool ShowImages { get; set; }

        public double Scale { get; set; }

        public DelegateCommand<string> SearchCommand { get; set; }

        public DelegateCommand<object> PlaySelectedAssetCommand { get; set; }

        public DelegateCommand<object> AddItemCommand { get; set; }

        public DelegateCommand<string> ShiftSliderScaleCommand { get; set; }

        public DelegateCommand<string> HelpButtonCommand { get; set; }

        public DelegateCommand<string> UpArrowCommand { get; set; }

        public string HeaderInfo { get; set; }

        public string HeaderIconOn { get; set; }

        public bool IsActive { get; set; }

        public string HeaderIconOff { get; set; }

        public bool IsHelpWindowOpen { get; set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void OnDropItem(Asset asset, MouseButtonEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void OnAddAsset(Asset asset)
        {
            throw new System.NotImplementedException();
        }

        public void OnAssetSelected(Asset asset)
        {
            throw new System.NotImplementedException();
        }

        public void ShowMetadata(TimelineElement timelineElement)
        {
            throw new System.NotImplementedException();
        }

        public void Activate()
        {
        }
    }
}