// <copyright file="DoubleClickTrigger.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DoubleClickTrigger.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Triggers
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Threading;

    public class DoubleClickTrigger : TriggerBase<UIElement>
    {
        private readonly DispatcherTimer timer;

        public DoubleClickTrigger()
        {
            this.timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 500)
            };

            this.timer.Tick += this.OnTimerTick;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += this.OnMouseButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= this.OnMouseButtonDown;
            if (this.timer.IsEnabled)
            {
                this.timer.Stop();
            }
        }

        private void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.timer.IsEnabled)
            {
                this.timer.Start();
                return;
            }

            this.timer.Stop();
            InvokeActions(null);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.timer.Stop();
        }
    }
}
