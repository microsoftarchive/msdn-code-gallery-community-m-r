// <copyright file="AdEditBoxPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdEditBoxPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    using Services.Contracts;

    public class AdEditBoxPresentationModel : BaseModel, IAdEditBoxPresentationModel
    {
        private readonly IAdViewPreview preview;

        private readonly IConfigurationService configurationService;

        private readonly ISequenceRegistry sequenceRegistry;

        private AdOpportunity adOpportunity;

        private double time;

        private string selectedTemplateType;
        
        public AdEditBoxPresentationModel(IAdEditBox view, IAdViewPreview preview, IConfigurationService configurationService, ISequenceRegistry sequenceRegistry)
        {
            this.View = view;
            this.preview = preview;
            this.configurationService = configurationService;
            this.sequenceRegistry = sequenceRegistry;

            this.CloseCommand = new DelegateCommand<object>(this.Close);
            this.SaveCommand = new DelegateCommand<object>(this.Save, this.CanSave);
            this.DeleteCommand = new DelegateCommand<object>(this.Delete);

            this.TemplateTypes = this.ParseTemplateTypes();

            if (this.TemplateTypes.Count > 0)
            {
                this.SelectedTemplateType = this.TemplateTypes[0];
            }

            this.adOpportunity = new AdOpportunity { TemplateType = this.SelectedTemplateType };
            this.sequenceRegistry.CurrentSequence.AdOpportunities.Add(this.adOpportunity);

            this.View.Model = this;
            this.preview.Model = this;
        }

        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public IAdEditBox View { get; private set; }

        public object Preview
        {
            get { return this.preview; }
        }

        public object EditBox
        {
            get { return this.View; }
        }
        
        public IList<string> TemplateTypes { get; private set; }

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

        public string SelectedTemplateType
        {
            get
            {
                return this.selectedTemplateType;
            }

            set
            {
                this.selectedTemplateType = value;
                this.OnPropertyChanged("SelectedTemplateType");
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
            this.Time = TimeSpan.FromTicks(this.adOpportunity.Time).TotalSeconds;
            this.SelectedTemplateType = this.adOpportunity.TemplateType;
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
            var newAdOpportunity = value as AdOpportunity;

            if (newAdOpportunity != null)
            {
                this.sequenceRegistry.CurrentSequence.AdOpportunities.Remove(this.adOpportunity);
                this.adOpportunity = newAdOpportunity;

                if (!this.sequenceRegistry.CurrentSequence.AdOpportunities.Contains(this.adOpportunity))
                {
                    this.sequenceRegistry.CurrentSequence.AdOpportunities.Add(this.adOpportunity);
                }

                this.SetPosition(TimeSpan.FromTicks(this.adOpportunity.Time));
                this.View.Close();
            }
        }

        public void SetPosition(TimeSpan position)
        {
            this.adOpportunity.Time = position.Ticks;
            this.Time = position.TotalSeconds;

            this.OnTimelineBarElementUpdated();
        }

        public T GetElement<T>() where T : class
        {
            return this.adOpportunity as T;
        }

        private static void ValidateTime(double time)
        {
            if (double.IsNaN(time) || double.IsInfinity(time) || time < 0)
            {
                throw new InputValidationException("Position is not valid.");
            }
        }

        private IList<string> ParseTemplateTypes()
        {
            IList<string> templateTypes = new List<string>();

            string templateTypesValue = this.configurationService.GetParameterValue("TemplateTypes");

            if (!string.IsNullOrEmpty(templateTypesValue))
            {
                string[] templates = templateTypesValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string template in templates)
                {
                    templateTypes.Add(template);
                }
            }

            return templateTypes;
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
            this.adOpportunity.Time = TimeSpan.FromSeconds(this.Time).Ticks;
            this.adOpportunity.TemplateType = this.SelectedTemplateType;

            this.OnTimelineBarElementUpdated();
            this.View.Close();
        }

        private void Delete(object obj)
        {
            this.sequenceRegistry.CurrentSequence.AdOpportunities.Remove(this.adOpportunity);
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
