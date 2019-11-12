// <copyright file="MarkerEditBoxPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkerEditBoxPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
namespace RCE.Modules.Markers
{
    using System;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    using Services.Contracts;

    public class MarkerEditBoxPresentationModel : BaseModel, IMarkerEditBoxPresentationModel
    {
        private readonly IMarkerViewPreview preview;

        private readonly ISequenceRegistry sequenceRegistry;
        
        private Marker marker;

        private double time;

        private string text;

        public MarkerEditBoxPresentationModel(IMarkerEditBox view, IMarkerViewPreview preview, ISequenceRegistry sequenceRegistry)
        {
            this.View = view;
            this.preview = preview;
            this.sequenceRegistry = sequenceRegistry;
           
            this.CloseCommand = new DelegateCommand<object>(this.Close);
            this.SaveCommand = new DelegateCommand<object>(this.Save, this.CanSave);
            this.DeleteCommand = new DelegateCommand<object>(this.Delete);

            this.marker = new Marker();
            this.sequenceRegistry.CurrentSequence.Markers.Add(this.marker);

            this.View.Model = this;
            this.preview.Model = this;
        }

        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public IMarkerEditBox View { get; private set; }

        public object Preview
        {
            get { return this.preview; }
        }

        public object EditBox
        {
            get { return this.View; }
        }
        
        public DelegateCommand<object> CloseCommand { get; private set; }

        public DelegateCommand<object> SaveCommand { get; private set; }

        public DelegateCommand<object> DeleteCommand { get; private set; }

        public double Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
                this.OnPropertyChanged("Time");
                this.SaveCommand.RaiseCanExecuteChanged();
                ValidateTime(value);
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        public double Position
        {
            get { return this.Time; }
        }

        public object DisplayBox
        {
            get
            {
                return null;
            }
        }

        public void ShowEditBox()
        {
            this.Time = TimeSpan.FromTicks(this.marker.Time).TotalSeconds;
            this.Text = this.marker.Text;
            this.View.Show();
        }

        /// <summary>
        /// Refreshes the comments when timeline zoom In/Out happen.
        /// </summary>
        /// <param name="refreshedWidth">The refreshed width.</param>
        public void RefreshPreview(double refreshedWidth)
        {
            this.OnTimelineBarElementUpdated();
        }

        public void SetElement(object value, CommentMode mode)
        {
            var newMarker = value as Marker;

            if (newMarker != null)
            {
                this.sequenceRegistry.CurrentSequence.Markers.Remove(this.marker);
                this.marker = newMarker;

                if (!this.sequenceRegistry.CurrentSequence.Markers.Contains(this.marker))
                {
                    this.sequenceRegistry.CurrentSequence.Markers.Add(this.marker);
                }
                
                this.SetPosition(TimeSpan.FromTicks(this.marker.Time));
                this.View.Close();
            }
        }

        public void SetPosition(TimeSpan position)
        {
            this.marker.Time = position.Ticks;
            this.Time = position.TotalSeconds;

            this.OnTimelineBarElementUpdated();
        }

        public T GetElement<T>() where T : class
        {
            return this.marker as T;
        }

        private static void ValidateTime(double time)
        {
            if (double.IsNaN(time) || double.IsInfinity(time) || time < 0)
            {
                throw new InputValidationException("Position is not valid.");
            }
        }

        private bool CanSave(object arg)
        {
            try
            {
                ValidateTime(this.Time);
                return true;
            }
            catch (InputValidationException)
            {
                return false;
            }
        }

        private void Save(object obj)
        {
            this.marker.Time = TimeSpan.FromSeconds(this.Time).Ticks;
            this.marker.Text = this.Text;

            this.OnTimelineBarElementUpdated();
            this.View.Close();
        }

        private void Delete(object obj)
        {
            this.sequenceRegistry.CurrentSequence.Markers.Remove(this.marker);
            this.View.Close();
            this.OnDeleting();
        }

        private void Close(object obj)
        {
            this.View.Close();
        }

        private void OnDeleting()
        {
            EventHandler<EventArgs> handler = this.Deleting;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnTimelineBarElementUpdated()
        {
            EventHandler<EventArgs> handler = this.TimelineBarElementUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
