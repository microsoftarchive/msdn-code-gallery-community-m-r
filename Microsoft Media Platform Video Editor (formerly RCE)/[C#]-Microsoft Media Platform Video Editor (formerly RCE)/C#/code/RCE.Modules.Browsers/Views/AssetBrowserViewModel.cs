// <copyright file="AssetBrowserViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetBrowserViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Views
{
    using System;
    using System.Windows;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;

    public class AssetBrowserViewModel : IAssetBrowserViewModel, IWindowMetadataProvider, IKeyboardAware
    {
        private readonly IAssetBrowserView view;

        private readonly DelegateCommand<Tuple<KeyboardAction, object>> handleKeyboardActionCommand;

        public AssetBrowserViewModel(IAssetBrowserView assetBrowserView, IEventAggregator eventAggregator)
        {
            this.handleKeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.HandleKeyboardAction);
            this.view = assetBrowserView;
            this.view.SetViewModel(this);
            eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public object View
        {
            get
            {
                return this.view;
            }
        }

        public VerticalWindowPosition VerticalPosition 
        { 
            get
            {
                return VerticalWindowPosition.Top;
            }
        }

        public HorizontalWindowPosition HorizontalPosition 
        {
            get
            {
                return HorizontalWindowPosition.Left;
            } 
        }

        public object Title 
        {   
            get
            {
                return "Asset Browser";
            } 
        }

        public ResizeDirection ResizeDirection 
        { 
            get
            {
                return Infrastructure.Windows.ResizeDirection.Vertical;
            } 
        }

        public Size Size
        {
            get
            {
                return new Size(450, 300);
            }
        }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand
        {
            get
            {
                return this.handleKeyboardActionCommand;
            }
        }

        public KeyboardActionContext ActionContext
        {
            get
            {
                var viewModel = this.SelectedView.DataContext as IKeyboardAware;
                if (viewModel != null)
                {
                    return viewModel.ActionContext;
                }

                return KeyboardActionContext.AssetBrowser;
            }
        }

        public FrameworkElement SelectedView { get; set; }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        private void HandleKeyboardAction(Tuple<KeyboardAction, object> obj)
        {
        }
    }
}
