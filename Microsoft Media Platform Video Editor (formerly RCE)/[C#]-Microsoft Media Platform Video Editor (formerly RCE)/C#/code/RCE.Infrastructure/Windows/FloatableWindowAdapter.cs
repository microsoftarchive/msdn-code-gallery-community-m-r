// <copyright file="FloatableWindowAdapter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FloatableWindowAdapter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class FloatableWindowAdapter : IWindow
    {
        private readonly FloatableWindow window;

        private ResizeDirection resizeDirection;

        private Style style;

        private double top;

        private double left;

        public FloatableWindowAdapter()
        {
            this.window = new FloatableWindow();
            this.window.Closed += this.WindowClosed;
            this.window.Closing += this.WindowClosing;
        }

        public event EventHandler Closed;

        public event EventHandler Closing;

        public object Content
        {
            get
            {
                return this.window.Content;
            }

            set
            {
                this.window.Content = value;
            }
        }

        public Style Style
        {
            get
            {
                return this.style;
            }

            set
            {
                this.style = value;
                this.window.Style = value;
            }
        }

        public double Left
        {
            get
            {
                if (this.window.ContentRoot != null)
                {
                    return this.window.ContentRoot.TransformToVisual(this.window.ParentLayoutRoot as UIElement).Transform(new Point(0, 0)).X;
                }

                return 0;
            }

            set
            {
                this.left = value;
            }
        }

        public double Top
        {
            get
            {
                if (this.window.ContentRoot != null)
                {
                    return this.window.ContentRoot.TransformToVisual(this.window.ParentLayoutRoot as UIElement).Transform(new Point(0, 0)).Y;    
                }

                return 0;
            }

            set
            {
                this.top = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.window.IsOpen;
            }
        }

        public bool IsModal
        {
            get
            {
                return this.window.IsModal;
            }
        }

        public bool HasFocus
        {
            get
            {
                return this.window.HasFocus;
            }
        }

        public object Title
        {
            get
            {
                return this.window.Title;
            }

            set
            {
                this.window.Title = value;
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return this.resizeDirection;
            }

            set
            {
                this.resizeDirection = value;
                switch (this.resizeDirection)
                {
                    case ResizeDirection.None:
                        this.window.ResizeMode = ResizeMode.NoResize;
                        break;
                    case ResizeDirection.Vertical:
                        this.window.ResizeMode = ResizeMode.CanResizeVertically;
                        break;
                    case ResizeDirection.Horizontal:
                        this.window.ResizeMode = ResizeMode.CanResizeHorizontally;
                        break;
                    case ResizeDirection.Both:
                        this.window.ResizeMode = ResizeMode.CanResize;
                        break;
                    default:
                        break;
                }
            }
        }

        public Size Size
        {
            get
            {
                return new Size(this.window.Width, this.window.Height);
            }

            set
            {
                if (!value.IsEmpty)
                {
                    this.window.Height = value.Height;
                    this.window.Width = value.Width;
                }
            }
        }

        // protected for testing purposes
        protected FloatableWindow Window
        {
            get
            {
                return this.window;
            }
        }
        
        public void Show(Panel root)
        {
            this.window.ParentLayoutRoot = root;
            this.window.Show(this.left, this.top);
        }

        public void ShowDialog(Panel root)
        {
            this.window.ParentLayoutRoot = root;
            this.window.ShowDialog();
        }

        public void SetWindowPosition(double y, double x)
        {
            if (this.IsOpen)
            {
                var transformGroup = this.Window.ContentRoot.RenderTransform as TransformGroup;

                if (transformGroup == null)
                {
                    transformGroup = new TransformGroup();
                    transformGroup.Children.Add(this.Window.ContentRoot.RenderTransform);
                }

                var t = new TranslateTransform { X = x - this.Left, Y = y - this.Top };

                transformGroup.Children.Add(t);
                this.Window.ContentRoot.RenderTransform = transformGroup;
            }
        }

        public void Close()
        {
            this.window.Close();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            EventHandler handler = this.Closing;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            EventHandler handler = this.Closed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
