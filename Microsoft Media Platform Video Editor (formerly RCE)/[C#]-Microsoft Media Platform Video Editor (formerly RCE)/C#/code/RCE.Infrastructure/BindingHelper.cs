// <copyright file="BindingHelper.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BindingHelper.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Helper class to achieve a kind of element name binding in Silverlight 2.
    /// </summary>
    public class BindingHelper : FrameworkElement, INotifyPropertyChanged
    {
        /// <summary>
        /// Holds the binding value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(object), typeof(BindingHelper), new PropertyMetadata(ValueChangedCallback));

        /// <summary>
        /// Occurs when a property change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the value of the binding.
        /// </summary>
        /// <value>The value associated with the value.</value>
        public object Value
        {
            get { return this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Ocurrs when the Value property changes the value.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject asociated.</param>
        /// <param name="propertyChangedEventArgs">The event args.</param>
        private static void ValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs propertyChangedEventArgs)
        {
            BindingHelper thisInstance = (BindingHelper)dependencyObject;
            PropertyChangedEventHandler eventHandler = thisInstance.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(thisInstance, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
