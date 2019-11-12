// <copyright file="TitleMediaData.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitleMediaData.cs                     
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
    using Infrastructure.Models;

    /// <summary>
    /// It is used to play the title in the player.
    /// </summary>
    public class TitleMediaData : MediaData
    {
        /// <summary>
        /// Title player for the title.
        /// </summary>
        private readonly TitlePlayer titlePlayer;

        /// <summary>
        /// The <see cref="TimelineElement"/> for the title of the <see cref="TitleMediaData"/>.
        /// </summary>
        private readonly TimelineElement timelineElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleMediaData"/> class.
        /// </summary>
        /// <param name="timelineElement">The timeline element.</param>
        public TitleMediaData(TimelineElement timelineElement)
        {
            this.timelineElement = timelineElement;
            this.titlePlayer = new TitlePlayer(timelineElement.Asset as TitleAsset) { Opacity = 0 };

            this.In = TimeSpan.FromSeconds(0);
            this.Out = TimeSpan.FromSeconds(timelineElement.Duration.TotalSeconds);

            timelineElement.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "InPosition")
                {
                    this.In = TimeSpan.FromSeconds(0);
                }

                if (e.PropertyName == "OutPosition")
                {
                    this.Out = TimeSpan.FromSeconds(timelineElement.Duration.TotalSeconds);
                }
            };
        }

        /// <summary>
        /// Gets the timeline element for the <see cref="TitleMediaData"/>.
        /// </summary>
        /// <value>The timeline element.</value>
        public override TimelineElement TimelineElement
        {
            get
            {
                return this.timelineElement;
            }
        }

        /// <summary>
        /// Gets the control corresponding to the <see cref="TitleMediaData"/> which
        /// is used to play the <see cref="TitleMediaData"/>.
        /// </summary>
        /// <value>The user control.</value>
        public override object Media
        {
            get
            {
                return this.titlePlayer;
            }
        }

        /// <summary>
        /// Gets or sets start position of the asset of the <see cref="TitleMediaData"/>.
        /// </summary>
        /// <value>
        /// Start position from where <see cref="TitleMediaData"/> will start playing.
        /// </value>
        public override TimeSpan In
        {
            get
            {
                return base.In;
            }

            set
            {
                this.titlePlayer.Duration = TimeSpan.FromSeconds(this.timelineElement.Duration.TotalSeconds);
                base.In = value;
            }
        }

        /// <summary>
        /// Gets or sets the stop position of the asset of the <see cref="MediaData"/>.
        /// </summary>
        /// <value>
        /// End position from where <see cref="TitleMediaData"/> will stop playing.
        /// </value>
        public override TimeSpan Out
        {
            get
            {
                return base.Out;
            }

            set
            {
                this.titlePlayer.Duration = TimeSpan.FromSeconds(this.timelineElement.Duration.TotalSeconds);
                base.Out = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the <see cref="TitleMediaData"/>.
        /// </summary>
        /// <value>The position.</value>
        public override TimeSpan Position
        {
            get
            {
                return TimeSpan.FromMilliseconds(this.titlePlayer.Position);
            }

            set
            {
                this.titlePlayer.Position = value.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Plays this <see cref="TitleMediaData"/>.
        /// </summary>
        public override void Play()
        {
            this.titlePlayer.Play();
            this.Playing = true;
        }

        /// <summary>
        /// Pauses this <see cref="TitleMediaData"/>.
        /// </summary>
        public override void Pause()
        {
            this.titlePlayer.Pause();
            this.Playing = false;
        }

        /// <summary>
        /// Stops this <see cref="TitleMediaData"/>.
        /// </summary>
        public override void Stop()
        {
            this.titlePlayer.Stop();
            this.Playing = false;
        }

        /// <summary>
        /// Hides this <see cref="TitleMediaData"/>.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
            this.titlePlayer.Opacity = 0;
        }

        /// <summary>
        /// Shows this <see cref="TitleMediaData"/>.
        /// </summary>
        public override void Show()
        {
            base.Show();
            this.titlePlayer.Opacity = 1;
        }
    }
}