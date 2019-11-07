// <copyright file="MenuButtonViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MenuButtonViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Menu
{
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Regions;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;

    public class MenuButtonViewModel : BaseModel, IMenuButtonViewModel
    {
        private readonly IRegionManager regionManager;

        private string text;

        private bool isViewActive;

        public MenuButtonViewModel(IMenuButtonView menuButtonView, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.regionManager.Regions[RegionNames.MainRegion].Views.CollectionChanged += this.MainRegionViewsCollectionChanged;
            this.ClickCommand = new DelegateCommand<object>(this.HandleClick);
            this.View = menuButtonView;
            this.View.SetViewModel(this);
        }

        public IMenuButtonView View { get; set; }

        public object ViewToDisplay { get; set; }

        public bool IsViewActive
        {
            get
            {
                return this.isViewActive;
            }

            set
            {
                this.isViewActive = value;
                this.OnPropertyChanged("IsViewActive");
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        public ICommand ClickCommand { get; set; }

        private void HandleClick(object obj)
        {
            if (this.regionManager.Regions[RegionNames.MainRegion].Views.Contains(this.ViewToDisplay))
            {
                this.regionManager.Regions[RegionNames.MainRegion].Remove(this.ViewToDisplay);
                this.IsViewActive = false;
            }
            else
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(this.ViewToDisplay);
                this.IsViewActive = true;
            }
        }

        private void MainRegionViewsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems[0] == this.ViewToDisplay)
            {
                this.IsViewActive = false;
            }
        }
    }
}
