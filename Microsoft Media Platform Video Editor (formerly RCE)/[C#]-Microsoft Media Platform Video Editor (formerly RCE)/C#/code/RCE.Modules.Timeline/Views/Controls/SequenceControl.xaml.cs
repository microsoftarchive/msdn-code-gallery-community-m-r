// <copyright file="SequenceControl.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequenceControl.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Views.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using RCE.Infrastructure.Models;

    public partial class SequenceControl : UserControl
    {
        private static readonly DependencyProperty SequenceProperty =
            DependencyProperty.RegisterAttached("Sequence", typeof(Sequence), typeof(SequenceControl), null);

        public SequenceControl()
        {
            InitializeComponent();
        }

        public Sequence Sequence 
        { 
            get
            {
                return (Sequence)this.GetValue(SequenceProperty);
            }

            set
            {
                this.SetValue(SequenceProperty, value);
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            this.NameTextBox.Text = string.Empty;
            this.NameTextBox.IsReadOnly = false;
            this.NameTextBox.Background = new SolidColorBrush(Colors.White);
            Dispatcher.BeginInvoke(() => this.NameTextBox.Focus());
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.NameTextBox.IsReadOnly = true;
            this.NameTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 176, 176, 176));
        }
    }
}
