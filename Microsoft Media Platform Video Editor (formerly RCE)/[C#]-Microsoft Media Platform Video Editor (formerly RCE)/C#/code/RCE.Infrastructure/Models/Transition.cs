// <copyright file="Transition.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Transition.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System.ComponentModel;

    public class Transition : INotifyPropertyChanged
    {
        private TransitionType transitionType;
        private double duration;

        public Transition(TransitionType type)
        {
            this.Type = type;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TransitionType Type
        {
            get
            {
                return this.transitionType;
            }

            set
            {
                if (this.transitionType != value)
                {
                    this.transitionType = value;
                    this.OnPropertyChanged("Type");
                }
            }
        }

        public double Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                if (this.duration != value)
                {
                    this.duration = value;
                    this.OnPropertyChanged("Duration");
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
