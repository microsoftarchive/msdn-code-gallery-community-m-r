// <copyright file="ThumbPlayer.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ThumbPlayer.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Views.MediaBin.Controls
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using Infrastructure;

    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

    public partial class ThumbPlayer : UserControl, IDisposable
    {
        private RCESmoothStreamingMediaPlugin plugin;

        private TimeSpan position;
        private Uri source;
        private bool isAdaptive;
        private DispatcherTimer seekTimer;

        private bool disposed;

        public ThumbPlayer()
        {
            this.seekTimer = new DispatcherTimer();
            InitializeComponent();
        }

        public void ShowThumb(Uri source, bool isAdapative, TimeSpan position)
        {
            this.source = source;
            this.isAdaptive = isAdapative;
            this.position = position;

            this.Configure();
        }

        public void UnLoad()
        {
            if (this.plugin != null)
            {
                this.plugin.Unload();
                this.plugin.Dispose();
                this.plugin = null;
            }
        }

        public void HideThumb()
        {
            this.LoadingContainer.Visibility = Visibility.Collapsed;
            if (this.plugin != null)
            {
                this.plugin.VisualElement.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if Dispose is being called from the Dispose method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.plugin != null)
                {
                    this.plugin.MediaOpened -= this.OnMediaOpened;
                    this.plugin.SeekCompleted -= this.OnSeekCompleted;
                    this.plugin.Unload();
                    this.plugin.Dispose();
                    this.plugin = null;
                }

                this.disposed = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitPlugin();
        }

        private void InitPlugin()
        {
            if (this.plugin == null)
            {
                this.plugin = new RCESmoothStreamingMediaPlugin();
                this.plugin.IsMuted = true;
                this.plugin.AutoPlay = false;
                this.plugin.MediaOpened += this.OnMediaOpened;
                this.plugin.SeekCompleted += this.OnSeekCompleted;
                this.plugin.VisualElement.Width = 112;
                this.plugin.VisualElement.Height = 63;
                this.plugin.VisualElement.Visibility = Visibility.Collapsed;
                this.LayoutRoot.Children.Add(this.plugin.VisualElement);
            }

            this.LoadingContainer.Visibility = Visibility.Collapsed;
        }

        private void Configure()
        {
            if (this.source != null && this.plugin != null)
            {
                this.LoadingContainer.Visibility = Visibility.Visible;
                this.plugin.VisualElement.Visibility = Visibility.Visible;

                if (this.isAdaptive)
                {
                    if (this.plugin.AdaptiveSource != this.source)
                    {
                        this.plugin.AdaptiveSource = this.source;
                    }
                }
                else
                {
                    if (this.plugin.Source != this.source)
                    {
                        this.plugin.Source = this.source;
                    }
                }

                if (this.plugin.CanSeek && this.position != this.plugin.Position)
                {
                    this.plugin.Position = this.position;
                }
            }
        }

        private void OnSeekCompleted(Microsoft.SilverlightMediaFramework.Plugins.IMediaPlugin arg1, bool arg2)
        {
            if (Math.Abs(arg1.Position.TotalMilliseconds - this.position.TotalMilliseconds) < 20)
            {
                this.plugin.VisualElement.Visibility = Visibility.Collapsed;

                if (arg1.CurrentState == MediaPluginState.Paused)
                {
                    this.ResetSeekTimer();
                }
                else
                {
                    this.plugin.CurrentStateChanged -= this.PluginCurrentStateChanged;
                    this.plugin.CurrentStateChanged += this.PluginCurrentStateChanged;
                }
            }
        }

        private void ResetSeekTimer()
        {
            this.seekTimer.Stop();
            this.seekTimer.Interval = TimeSpan.FromMilliseconds(500);
            this.seekTimer.Tick -= this.HandleSeekTimerTick;
            this.seekTimer.Tick += this.HandleSeekTimerTick;
            this.seekTimer.Start();
        }

        private void HandleSeekTimerTick(object sender, EventArgs e)
        {
            if (this.plugin.CurrentState == MediaPluginState.Buffering)
            {
                this.plugin.CurrentStateChanged -= this.PluginCurrentStateChanged;
                this.plugin.CurrentStateChanged += this.PluginCurrentStateChanged;
            }
            else
            {
                this.seekTimer.Stop();
                this.seekTimer.Tick -= this.HandleSeekTimerTick;
                this.LoadingContainer.Visibility = Visibility.Collapsed;
                this.plugin.VisualElement.Visibility = Visibility.Visible;
            }
        }

        private void PluginCurrentStateChanged(Microsoft.SilverlightMediaFramework.Plugins.IMediaPlugin arg1, MediaPluginState arg2)
        {
            if (arg2 == MediaPluginState.Paused)
            {
                this.ResetSeekTimer();
            }
        }

        private void OnMediaOpened(Microsoft.SilverlightMediaFramework.Plugins.IMediaPlugin obj)
        {
            if (this.position != this.plugin.Position)
            {
                this.plugin.Position = this.position;
            }
        }
    }
}
