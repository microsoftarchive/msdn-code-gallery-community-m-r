// <copyright file="TitlePlayer.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitlePlayer.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Models
{
    using System;
    using System.Resources;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media.Animation;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.ServiceLocation;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// It can be used to play the title asset. 
    /// It loads the xaml for the tile asset and plays it.
    /// </summary>
    public class TitlePlayer : UserControl
    {
        /// <summary>
        /// Storyboard to animate the title.
        /// </summary>
        private readonly Storyboard timer;

        /// <summary>
        /// Time <see cref="DateTime"/>.
        /// </summary>
        private DateTime lastTime;

        /// <summary>
        /// The position of the title with respect to the start time.
        /// </summary>
        private double position;

        /// <summary>
        /// Value indicating if the title is playing.
        /// </summary>
        private bool outPlaying;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitlePlayer"/> class.
        /// </summary>
        /// <param name="asset">The asset.</param>
        public TitlePlayer(TitleAsset asset)
        {
            this.DataContext = asset;
            
            Canvas.SetZIndex(this, 5);

            if (asset.TitleTemplate.XamlResource == null)
            {
                IConfigurationService configurationService = ServiceLocator.Current.GetInstance(typeof(IConfigurationService)) as IConfigurationService;
                
                if (configurationService != null)
                {
                    Downloader downloader = ServiceLocator.Current.GetInstance(typeof(Downloader)) as Downloader;

                    if (downloader != null)
                    {
                        Uri xamlResource = configurationService.GetTitleTemplate(asset.TitleTemplate.Title);
                        downloader.DownloadStringCompleted += (sender, e) =>
                                                                  {
                                                                      TitleTemplate template =
                                                                          e.UserState as TitleTemplate;
                                                                      if (e.Error == null && template != null)
                                                                      {
                                                                          template.XamlResource = e.Result;
                                                                          this.LoadTitle(template.XamlResource);
                                                                      }
                                                                  };
                        downloader.DownloadStringAsync(xamlResource, asset.TitleTemplate);
                    }
                }
            }
            else
            {
                this.LoadTitle(asset.TitleTemplate.XamlResource);
            }

            this.timer = new Storyboard();
            this.timer.Completed += this.Timer_Completed;
        }

        /// <summary>
        /// Gets or sets the duration of the title.
        /// </summary>
        /// <value>The duration.</value>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public double Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                double inDuration = this.InTransition.Duration.TimeSpan.TotalMilliseconds;
                double outDuration = this.OutTransition.Duration.TimeSpan.TotalMilliseconds;

                if (this.Duration.TotalMilliseconds > inDuration + outDuration)
                {
                    if (value < inDuration)
                    {
                        this.InTransitionPosition = value;
                    }
                    else if (value > this.Duration.TotalMilliseconds - outDuration)
                    {
                        this.OutTransitionPosition = value - (this.Duration.TotalMilliseconds - outDuration);
                    }
                    else
                    {
                        this.InTransitionPosition = inDuration;
                    }
                }
                else
                {
                    if (value < this.Duration.TotalMilliseconds / 2)
                    {
                        this.InTransitionPosition = value;
                    }
                    else
                    {
                        this.OutTransitionPosition = (value - (this.Duration.TotalMilliseconds / 2)) + (outDuration - (this.Duration.TotalMilliseconds / 2));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the in transition storyboard.
        /// </summary>
        /// <value>The <see cref="Storyboard"/>.</value>
        private Storyboard InTransition { get; set; }

        /// <summary>
        /// Gets or sets the out transition storyboard.
        /// </summary>
        /// <value>The <see cref="Storyboard"/>.</value>
        private Storyboard OutTransition { get; set; }

        /// <summary>
        /// Sets the in transition position.
        /// </summary>
        /// <value>The in transition position.</value>
        private double InTransitionPosition
        {
            set
            {
                this.InTransition.Begin();
                this.InTransition.Seek(TimeSpan.FromMilliseconds(value));
                this.InTransition.Pause();
            }
        }

        /// <summary>
        /// Sets the out transition position.
        /// </summary>
        /// <value>The out transition position.</value>
        private double OutTransitionPosition
        {
            set
            {
                this.OutTransition.Begin();
                this.OutTransition.Seek(TimeSpan.FromMilliseconds(value));
                this.OutTransition.Pause();
            }
        }

        /// <summary>
        /// Plays this title.
        /// </summary>
        public void Play()
        {
            this.outPlaying = false;
            this.InTransitionPosition = this.Position;

            if (this.Position < this.InTransition.Duration.TimeSpan.TotalMilliseconds)
            {
                this.InTransition.Begin();
            }
            else if (this.Position > this.Duration.TotalMilliseconds - this.OutTransition.Duration.TimeSpan.TotalMilliseconds)
            {
                this.OutTransition.Begin();
            }

            this.timer.Begin();
        }

        /// <summary>
        /// Pauses this title.
        /// </summary>
        public void Pause()
        {
            this.timer.Pause();
            this.InTransition.Pause();
            this.OutTransition.Pause();
        }

        /// <summary>
        /// Stops playing the title.
        /// </summary>
        public void Stop()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// Loads the storyboard and controls from the title xaml.
        /// </summary>
        /// <param name="xamlResource">The xaml resource.</param>
        private void LoadTitle(string xamlResource)
        {
            Canvas canvas = XamlReader.Load(xamlResource) as Canvas;

            this.Width = canvas.Width;
            this.Height = canvas.Height;
            this.Content = canvas;

            this.InTransition = (Storyboard)canvas.Resources["InTransition"];
            this.OutTransition = (Storyboard)canvas.Resources["OutTransition"];
            this.OutTransition.Completed += this.OutTransition_Completed;
        }

        /// <summary>
        /// Handles the Completed event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Timer_Completed(object sender, EventArgs e)
        {
            this.timer.Begin();
            DateTime now = DateTime.Now;
            double elapsed = (now - this.lastTime).TotalMilliseconds;
            bool reset = false;

            if (elapsed < 100)
            {
                this.position += elapsed;
                if (this.position >= this.Duration.TotalMilliseconds - this.OutTransition.Duration.TimeSpan.TotalMilliseconds && !this.outPlaying)
                {
                    this.OutTransition.Begin();
                    this.outPlaying = true;
                }
                else if (this.Position > this.Duration.TotalMilliseconds)
                {
                    this.Stop();
                    reset = true;
                }
            }

            this.lastTime = reset ? new DateTime() : now;
        }

        /// <summary>
        /// Handles the Completed event of the OutTransition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OutTransition_Completed(object sender, EventArgs args)
        {
            // this.position = 0;
        }
    }
}