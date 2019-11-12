// <copyright file="VolumeControl.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VolumeControl.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Control for controling the volume. It has bars which shows the bars corresponding 
    /// to the volume level.
    /// </summary>
    public partial class VolumeControl : UserControl
    {
        /// <summary>
        /// The <see cref="DependencyProperty"/> for volume.
        /// </summary>
        public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register("Volume", typeof(double), typeof(VolumeControl), new PropertyMetadata(OnVolumeChanged));

        /// <summary>
        /// Using a DependencyProperty as the backing store for BarOffBrush.
        /// </summary>
        public static readonly DependencyProperty BarOffBrushProperty = DependencyProperty.Register("BarOffBrush", typeof(SolidColorBrush), typeof(VolumeControl), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for BarOnBrush. 
        /// </summary>
        public static readonly DependencyProperty BarOnBrushProperty = DependencyProperty.Register("BarOnBrush", typeof(SolidColorBrush), typeof(VolumeControl), null);

        /// <summary>
        /// True if the timeline is in mute state.
        /// </summary>
        private bool isMuted;

        /// <summary>
        /// List of bars which represents the volume level.
        /// </summary>
        private IList<Path> bars;

        /// <summary>
        /// The value of volume before the volume is muted.
        /// </summary>
        private double volumeBeforeMute;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeControl"/> class.
        /// </summary>
        public VolumeControl()
        {
            InitializeComponent();
            this.Initialize();

            if (this.BarOffBrush == null)
            {
                this.BarOffBrush = (SolidColorBrush)this.Resources["VolumeDefaultBarOffBrush"];
            }

            if (this.BarOnBrush == null)
            {
                this.BarOnBrush = (SolidColorBrush)this.Resources["VolumeDefaultBarOnBrush"];
            }
        }

        /// <summary>
        /// Occurs when [volume changed].
        /// </summary>
        public event EventHandler VolumeChanged;

        /// <summary>
        /// Bar color if the bar is in the volume level.  Allows customization of the 
        /// default brush color.
        /// </summary>
        public SolidColorBrush BarOnBrush
        {
            get { return (SolidColorBrush)GetValue(BarOnBrushProperty); }
            set { SetValue(BarOnBrushProperty, value); }
        }

       /// <summary>
        /// Bar color if the bar out of the volume level.Allows customization of the 
        /// default brush color.
        /// </summary>
        public SolidColorBrush BarOffBrush
        {
            get { return (SolidColorBrush)GetValue(BarOffBrushProperty); }
            set { SetValue(BarOffBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public double Volume
        {
            get
            {
                return (double)this.GetValue(VolumeProperty);
            }

            set
            {
                this.SetValue(VolumeProperty, value);

                this.OnVolumeChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is muted.
        /// </summary>
        /// <value>A <c>true</c> if this instance is muted; otherwise, <c>false</c>.</value>
        public bool IsMuted
        {
            get
            {
                return this.isMuted;
            }

            set
            {
                this.isMuted = value;
                this.ToogleMute();
                this.OnVolumeChanged();
            }
        }

        /// <summary>
        /// Called when [volume changed].
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnVolumeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VolumeControl volumeControl = d as VolumeControl;

            if (volumeControl != null)
            {
                if (volumeControl.Volume == 0)
                {
                    if (!volumeControl.IsMuted)
                    {
                        volumeControl.isMuted = true;
                    }
                }
                else
                {
                    if (volumeControl.isMuted)
                    {
                        volumeControl.isMuted = false;
                    }
                }

                volumeControl.PaintColorBars();
            }
        }

        /// <summary>
        /// Toogles the mute.
        /// </summary>
        private void ToogleMute()
        {
            if (this.IsMuted)
            {
                this.volumeBeforeMute = this.Volume;
                this.Volume = 0;
            }
            else
            {
                this.Volume = this.volumeBeforeMute;
            }
        }

        /// <summary>
        /// Paints the color bars corresponding to the volume level.
        /// </summary>
        private void PaintColorBars()
        {
            if (this.bars.Count == 0)
            {
                return;
            }

            this.speaker.Fill = this.Volume == 0 ? this.BarOffBrush : this.BarOnBrush;

            double barPercent = (double)1 / this.bars.Count;

            for (int i = 0; i < this.bars.Count; i++)
            {
                double currentBarPercent = barPercent * i;

                if (this.Volume >= currentBarPercent && !this.IsMuted)
                {
                    this.bars[i].Fill = this.BarOnBrush;
                }
                else
                {
                    this.bars[i].Fill = this.BarOffBrush;
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.volumeHitArea.MouseLeftButtonDown += this.Volume_MouseLeftButtonDown;
            this.muteHitArea.MouseLeftButtonDown += this.Mute_MouseLeftButtonDown;

            this.bars = new List<Path> { this.bar0, this.bar1, this.bar2, this.bar3, this.bar4 };
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Volume control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void Volume_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMuted)
            {
                double percent = e.GetPosition(this.volumeHitArea).Y / this.volumeHitArea.Height;
                this.Volume = 1 - percent;
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Mute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void Mute_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            this.IsMuted = !this.IsMuted;
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.PaintColorBars();
        }

        /// <summary>
        /// Called when [volume changed].
        /// </summary>
        private void OnVolumeChanged()
        {
            EventHandler volumeChangedHandler = this.VolumeChanged;
            if (volumeChangedHandler != null)
            {
                volumeChangedHandler(this, EventArgs.Empty);
            }
        }
    }
}
