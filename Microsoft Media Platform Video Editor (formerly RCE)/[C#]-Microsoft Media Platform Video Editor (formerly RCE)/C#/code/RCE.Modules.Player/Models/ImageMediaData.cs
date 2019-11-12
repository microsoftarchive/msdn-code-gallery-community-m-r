// <copyright file="ImageMediaData.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImageMediaData.cs                     
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
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Infrastructure.Models;

    using RCE.Infrastructure;

    /// <summary>
    /// Used to play a image in the player for the given time.
    /// </summary>
    public class ImageMediaData : MediaData
    {
        /// <summary>
        /// Image control to show the image.
        /// </summary>
        private readonly Image image;

        /// <summary>
        /// The <see cref="DispatcherTimer"/> to have the position while playing <see cref="ImageMediaData"/>.
        /// </summary>
        private readonly DispatcherTimer timer;

        /// <summary>
        /// The <see cref="TimelineElement"/> for the <see cref="ImageMediaData"/>
        /// to have the position of the mediadata in the timeline.
        /// </summary>
        private readonly TimelineElement timelineElement;

        /// <summary>
        /// To have the time when the asset starts playing.
        /// </summary>
        private DateTime processTime;

        /// <summary>
        /// The <see cref="TimeSpan"/> position of the <see cref="ImageMediaData"/> while playing.
        /// </summary>
        private TimeSpan inPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMediaData"/> class.
        /// </summary>
        /// <param name="timelineElement">The timeline element.</param>
        public ImageMediaData(TimelineElement timelineElement)
        {
            this.timelineElement = timelineElement;
            this.In = TimeSpan.FromSeconds(timelineElement.InPosition.TotalSeconds);
            this.Out = TimeSpan.FromSeconds(timelineElement.OutPosition.TotalSeconds);

            timelineElement.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "InPosition")
                {
                    this.In = TimeSpan.FromSeconds(timelineElement.InPosition.TotalSeconds);
                }

                if (e.PropertyName == "OutPosition")
                {
                    this.Out = TimeSpan.FromSeconds(timelineElement.OutPosition.TotalSeconds);
                }
            };

            this.image = new Image
                             {
                                 Source = new BitmapImage(timelineElement.Asset.Source),
                                 Opacity = 0
                             };

            this.timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(UtilityHelper.PositionUpdateIntervalMillis) };
            this.timer.Tick += (sender, e) =>
                                   {
                                       this.timer.Stop();

                                       if (this.Playing)
                                       {
                                           DateTime now = DateTime.Now;
                                           this.Position += TimeSpan.FromMilliseconds((now - this.processTime).TotalMilliseconds);
                                           this.processTime = now;
                                           this.timer.Start();
                                       }
                                   };
        }

        /// <summary>
        /// Gets the control corresponding to the <see cref="ImageMediaData"/> which
        /// is used to play the <see cref="ImageMediaData"/>.
        /// </summary>
        /// <value>The user control.</value>
        public override object Media
        {
            get { return this.image; }
        }

        /// <summary>
        /// Gets the timeline element for the <see cref="ImageMediaData"/>.
        /// </summary>
        /// <value>The timeline element.</value>
        public override TimelineElement TimelineElement
        {
            get { return this.timelineElement; }
        }

        /// <summary>
        /// Gets or sets start position of the asset of the <see cref="ImageMediaData"/>.
        /// </summary>
        /// <value>
        /// Start position from where <see cref="ImageMediaData"/> will start playing.
        /// </value>
        public override TimeSpan In
        {
            get
            {
                return this.inPosition;
            }

            set
            {
                this.inPosition = value;
                this.Position = value;
            }
        }

        /// <summary>
        /// Plays this <see cref="ImageMediaData"/>.
        /// </summary>
        public override void Play()
        {
            this.Playing = true;
            this.processTime = DateTime.Now;
            this.timer.Start();
        }

        /// <summary>
        /// Pauses this <see cref="ImageMediaData"/>.
        /// </summary>
        public override void Pause()
        {
            this.Playing = false;
        }

        /// <summary>
        /// Stops this <see cref="ImageMediaData"/>.
        /// </summary>
        public override void Stop()
        {
            this.Position = new TimeSpan(0);
            this.Playing = false;
        }

        /// <summary>
        /// Hides this <see cref="ImageMediaData"/>.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
            this.image.Opacity = 0;
        }

        /// <summary>
        /// Shows this <see cref="ImageMediaData"/>.
        /// </summary>
        public override void Show()
        {
            base.Show();
            this.image.Opacity = 1;
        }
    }
}
