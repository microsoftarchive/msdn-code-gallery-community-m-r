// <copyright file="OOBWindowAdapter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OOBWindowAdapter.cs                     
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
    using System.Windows;
    using System.Windows.Controls;

    public class OOBWindowAdapter : IWindow
    {
        public event EventHandler Closed;

        public event EventHandler Closing;

        public object Content
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Style Style
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double Left
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double Top
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsOpen
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsModal
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool HasFocus
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Title
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Size Size
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Show(Panel root)
        {
            throw new NotImplementedException();
        }

        public void ShowDialog(Panel root)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void SetWindowPosition(double heigth, double width)
        {
            throw new NotImplementedException();
        }
    }
}
