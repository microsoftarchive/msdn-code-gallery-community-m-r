// <copyright file="DialogActivationBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DialogActivationBehavior.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows.Behaviors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Controls;

    using Microsoft.Practices.Composite.Presentation.Regions;
    using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
    using Microsoft.Practices.Composite.Regions;

    using RCE.Infrastructure.Windows;

    /// <summary>
    /// Defines a behavior that creates a Dialog to display the active view of the target <see cref="IRegion"/>.
    /// </summary>
    public class DialogActivationBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        /// <summary>
        /// The key of this behavior
        /// </summary>
        public const string BehaviorKey = "DialogActivation";

        private readonly IWindowManager windowManager;

        private IDictionary<IWindowMetadataProvider, Point> windowsAttributes;

        private Dictionary<object, IWindow> viewWindowMappings;

        public DialogActivationBehavior(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
            this.windowsAttributes = new Dictionary<IWindowMetadataProvider, Point>();

            if (Application.Current != null)
            {
                Application.Current.Exit += new EventHandler(this.TrackOpenedWindows);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="FrameworkElement"/> that is part of the tree.</value>
        public DependencyObject HostControl { get; set; }

        protected Dictionary<object, IWindow> ViewWindowMappings
        {
            get
            {
                return this.viewWindowMappings;
            }
        }

        /// <summary>
        /// Performs the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            this.Region.Views.CollectionChanged += this.ViewsCollectionChanged;
            this.viewWindowMappings = new Dictionary<object, IWindow>();
        }

        /// <summary>
        /// Override this method to create an instance of the <see cref="IWindow"/> that 
        /// will be shown when a view is activated.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IWindow"/> that will be shown when a 
        /// view is activated on the target <see cref="IRegion"/>.
        /// </returns>
        protected virtual IWindow CreateWindow()
        {
            return this.windowManager.CreateWindow();
        }

        private void SetWindowInformationFromView(IWindow window, object view)
        {
            var viewWithDataContext = view as FrameworkElement;
            if (viewWithDataContext != null)
            {
                var windowInfo = viewWithDataContext.DataContext as IWindowMetadataProvider;
                if (windowInfo != null)
                {
                    this.PersistWindowDefaultPosition(windowInfo, viewWithDataContext);
                    window.Left = this.GetWindowLeft(view, windowInfo);
                    window.Top = this.GetWindowTop(view, windowInfo, viewWithDataContext);
                    window.Title = windowInfo.Title;
                    window.ResizeDirection = windowInfo.ResizeDirection;
                    window.Size = this.GetWindowSize(view, windowInfo);
                    windowInfo.TitleUpdated += this.WindowMetadataTitleUpdated;
                    windowInfo.ResetPositionRaised += this.OnResetWindowRaise;
                }
            }
        }

        private void PersistWindowDefaultPosition(IWindowMetadataProvider windowInfo, FrameworkElement viewWithDataContext)
        {
            if (!this.windowsAttributes.ContainsKey(windowInfo))
            {
                var top = WindowPosition.GetWindowTop(windowInfo.VerticalPosition, ((FrameworkElement)this.HostControl).ActualHeight, viewWithDataContext.Height);
                var left = WindowPosition.GetWindowLeft(windowInfo.HorizontalPosition, ((FrameworkElement)this.HostControl).ActualWidth);

                this.windowsAttributes[windowInfo] = new Point(left, top);
            }
        }

        private Size GetWindowSize(object view, IWindowMetadataProvider windowInfo)
        {
            var height = (double?)this.windowManager.RecoverProperty(view.GetType().ToString(), "Height");
            var width = (double?)this.windowManager.RecoverProperty(view.GetType().ToString(), "Width");

            if (height.HasValue && width.HasValue)
            {
                Size windowSize = new Size { Height = height.Value, Width = width.Value };

                return windowSize;
            }

            return windowInfo.Size;
        }

        private void OnResetWindowRaise(object sender, DataEventArgs<object> args)
        {
            var viewModel = (IWindowMetadataProvider)sender;
            var view = args.Data;

            if (this.windowsAttributes.ContainsKey(viewModel) && this.viewWindowMappings.ContainsKey(view))
            {
                var left = this.windowsAttributes[viewModel].X;
                var top = this.windowsAttributes[viewModel].Y;

                var container = this.viewWindowMappings[view];
                container.SetWindowPosition(top, left);
            }

            this.windowManager.RemoveProperty(view.GetType().ToString(), "PositionX"); 
            this.windowManager.RemoveProperty(view.GetType().ToString(), "PositionY");
        }

        private double GetWindowTop(object view, IWindowMetadataProvider windowInfo, FrameworkElement viewWithDataContext)
        {
            var persistedValue = (double?)this.windowManager.RecoverProperty(view.GetType().ToString(), "PositionY");

            if (persistedValue.HasValue)
            {
                return persistedValue.Value;
            }

            return WindowPosition.GetWindowTop(windowInfo.VerticalPosition, ((FrameworkElement)this.HostControl).ActualHeight, viewWithDataContext.Height);
        }

        private double GetWindowLeft(object view, IWindowMetadataProvider windowInfo)
        {
            var persistedValue = (double?)this.windowManager.RecoverProperty(view.GetType().ToString(), "PositionX");

            if (persistedValue.HasValue)
            {
                return persistedValue.Value;
            }

            return WindowPosition.GetWindowLeft(windowInfo.HorizontalPosition, ((FrameworkElement)this.HostControl).ActualWidth);
        }

        private void WindowMetadataTitleUpdated(object sender, DataEventArgs<object> e)
        {
            object view = e.Data;

            var viewWithDataContext = view as FrameworkElement;
            if (viewWithDataContext != null)
            {
                var windowInfo = viewWithDataContext.DataContext as IWindowMetadataProvider;
                if (windowInfo != null)
                {
                    IWindow relatedWindow = this.viewWindowMappings[view];
                    relatedWindow.Title = windowInfo.Title;
                }
            }
        }

        private void TrackOpenedWindows(object sender, EventArgs e)
        {
            foreach (var keyValuePair in this.ViewWindowMappings)
            {
                this.PersistWindowProperty(keyValuePair.Key.GetType().ToString(), "Visibility", keyValuePair.Value.IsOpen);
            }
        }

        private void ViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.PrepareContentDialog(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (this.viewWindowMappings.ContainsKey(e.OldItems[0]))
                {
                    this.CloseContentDialog(this.viewWindowMappings[e.OldItems[0]]);
                }
            }
        }

        private Style GetStyleForView()
        {
            return this.HostControl.GetValue(RegionWindowBehaviors.ContainerWindowStyleProperty) as Style;
        }

        private void PrepareContentDialog(object view)
        {
            IWindow window = this.CreateWindow();
            window.Content = view;
            window.Closed += this.ContentDialogClosed;
            window.Closing += this.PersistWindowPropertiesOnClosed;
            window.Style = this.GetStyleForView();
           
            this.SetWindowInformationFromView(window, view);

            window.Show((Panel)this.HostControl);
            this.UpdateIsDisplayedProperty(view, true);
            this.viewWindowMappings[view] = window;
        }

        private void PersistWindowPropertiesOnClosed(object sender, EventArgs e)
        {
            var window = (FloatableWindowAdapter)sender;
            
            string windowName = window.Content.ToString();

            this.PersistWindowProperty(windowName, "Height", window.Size.Height);
            this.PersistWindowProperty(windowName, "Width", window.Size.Width);
            this.PersistWindowProperty(windowName, "PositionX", window.Left);
            this.PersistWindowProperty(windowName, "PositionY", window.Top);
        }

        private void PersistWindowProperty(string windowName, string propertyName, object value)
        {
            this.windowManager.PersistProperty(windowName, propertyName, value);
        }

        private void CloseContentDialog(IWindow window)
        {
            if (window != null)
            {
                this.UpdateIsDisplayedProperty(window.Content, false);
                if (window.IsOpen)
                {
                    window.Close();
                    window.Closed -= this.ContentDialogClosed;
                }
                
                window.Content = null;
            }
        }

        private void ContentDialogClosed(object sender, System.EventArgs e)
        {
            IWindow window = sender as IWindow;
            bool closeDialog = true;
            if (window != null)
            {
               this.UpdateIsDisplayedProperty(window.Content, false);

                if (this.Region.Views.Contains(window.Content))
                {
                    this.Region.Remove(window.Content);
                    closeDialog = false;
                }
            }

            if (closeDialog)
            {
                this.CloseContentDialog(window);    
            }
        }

        private void UpdateIsDisplayedProperty(object view, bool value)
        {
            var viewWithDataContext = view as FrameworkElement;
            if (viewWithDataContext != null)
            {
                var windowAwareViewModel = viewWithDataContext.DataContext as IWindowAware;
                if (windowAwareViewModel != null)
                {
                    windowAwareViewModel.IsDisplayed = value;
                }
            }
        }
    }
}
