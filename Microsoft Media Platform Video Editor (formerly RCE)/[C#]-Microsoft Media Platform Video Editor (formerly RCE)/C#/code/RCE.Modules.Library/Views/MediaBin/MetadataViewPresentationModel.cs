// <copyright file="MetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;

    using Infrastructure;
    using Infrastructure.Models;

    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.ServiceLocation;

    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    /// <summary>
    /// Presentation Model for the Metadata view.
    /// </summary>
    public class MetadataViewPresentationModel : BaseModel, IKeyboardAware
    {
        /// <summary>
        /// The <see cref="IConfigurationService"/> to get configuration parameters.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The <see cref="IEventDataParser{T}"/> used to parse events.
        /// </summary>
        private readonly IEventDataParser<EventData> eventDataParser;

        /// <summary>
        /// The <see cref="IEventDataParser{T}"/> used to parse offset events.
        /// </summary>
        private readonly IEventDataParser<EventOffset> eventOffsetParser;

        /// <summary>
        /// Contains the available metadata.
        /// </summary>
        private readonly IList<EventData> availableMetadata;

        /// <summary>
        /// The results message pattern used for showing results message.
        /// </summary>
        private const string ResultsMessagePattern = "{0} of {1} results {2}";

        /// <summary>
        /// Contains the results message instance.
        /// </summary>
        private string resultsText;

        /// <summary>
        /// The collection of events.
        /// </summary>
        private ILogEntryCollection logEntryCollection;

        /// <summary>
        /// The current event offset.
        /// </summary>
        private TimeSpan currentEventOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataViewPresentationModel"/> class.
        /// </summary>
        public MetadataViewPresentationModel()
            : this(
            ServiceLocator.Current.GetInstance<IConfigurationService>(), 
            ServiceLocator.Current.GetInstance<IEventDataParser<EventData>>(), 
            ServiceLocator.Current.GetInstance<IEventDataParser<EventOffset>>())
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataViewPresentationModel"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service instance.</param>
        /// <param name="eventDataParser">The event data parser instance.</param>
        /// <param name="eventOffsetParser">The event offset parser instance.</param>
        public MetadataViewPresentationModel(IConfigurationService configurationService, IEventDataParser<EventData> eventDataParser, IEventDataParser<EventOffset> eventOffsetParser)
        {
            this.configurationService = configurationService;
            this.eventDataParser = eventDataParser;
            this.eventOffsetParser = eventOffsetParser;
            this.SearchCommand = new DelegateCommand<string>(this.Search);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);
            this.Metadata = new ObservableCollection<EventData>();
            this.availableMetadata = new List<EventData>();
            this.MetadataFilters = new List<string> { "Both", "Play By Play", "Commentary" };
            this.SelectedMetadataFilter = this.MetadataFilters[0];
        }

        /// <summary>
        /// Gets or sets the event data collection.
        /// </summary>
        /// <value>The collection of events.</value>
        public ObservableCollection<EventData> Metadata { get; set; }

        /// <summary>
        /// Gets the search command being executed when search ocurrs.
        /// </summary>
        /// <value>The search command instance.</value>
        public DelegateCommand<string> SearchCommand { get; private set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Metadata;
            }
        }

        /// <summary>
        /// Gets or sets teh start offset being applied to the events.
        /// </summary>
        /// <value>The start offset.</value>
        public TimeCode StartOffset { get; set; }

        /// <summary>
        /// Gets or sets the results text message being shown to the user.
        /// </summary>
        /// <value>The results text message.</value>
        public string ResultsText
        {
            get 
            {
                return this.resultsText; 
            }

            set
            {
                this.resultsText = value;

                this.OnPropertyChanged("ResultsText");
            }
        }

        /// <summary>
        /// Gets the list of available filters.
        /// </summary>
        /// <value>The list of available filters.</value>
        public IList<string> MetadataFilters { get; private set; }

        /// <summary>
        /// Gets or sets the selected filter.
        /// </summary>
        /// <value>The selected metadata filter.</value>
        public string SelectedMetadataFilter { get; set; }

        /// <summary>
        /// Sets the current stream data.
        /// </summary>
        /// <param name="entryCollection">The current stream data.</param>
        public void SetInStreamData(ILogEntryCollection entryCollection)
        {
            this.logEntryCollection = entryCollection;
            this.logEntryCollection.EventDataAdded += this.EventDataAdded;
            this.logEntryCollection.EventDataRemoved += this.EventDataRemoved;

            this.logEntryCollection.Items.ForEach(this.AddEvent);
        }

        private void EventDataRemoved(object sender, Infrastructure.DataEventArgs<EventData> e)
        {
            this.RemoveEvent(e.Data);
        }

        private void EventDataAdded(object sender, Infrastructure.DataEventArgs<EventData> e)
        {
            this.AddEvent(e.Data);
        }

        /// <summary>
        /// Searches over teh available metadata.
        /// </summary>
        /// <param name="parameter">The search parameter.</param>
        private void Search(string parameter)
        {
            List<EventData> results = new List<EventData>();
            int? currentLimit = this.configurationService.GetParameterValueAsInt("SearchWithinAssetsLimit");

            this.Metadata.Clear();
            this.ResultsText = string.Empty;

            this.availableMetadata.Where(x => x.Text.ToUpper(CultureInfo.InvariantCulture).Contains(parameter.ToUpper()))
                .ToList()
                .ForEach(m => results.Add(m));

            results.Sort((t1, t2) => t1.Time.CompareTo(t2.Time));

            bool limitExceeded = currentLimit.HasValue && results.Count > currentLimit;

            if (limitExceeded)
            {
                this.SetLimitExceededMessage(currentLimit.Value, results.Count);
                results = results.Take(currentLimit.Value).ToList();
            }
            else
            {
                this.SetResultsMessage(results.Count);
            }

            results.ForEach(x => this.Metadata.Add(x));
        }

        /// <summary>
        /// Shows an event.
        /// </summary>
        /// <param name="eventData">The event being shown.</param>
        private void ShowEvent(EventData eventData)
        {
            int? currentLimit = this.configurationService.GetParameterValueAsInt("SearchWithinAssetsLimit");

            bool limitExceeded = currentLimit.HasValue && this.Metadata.Count > currentLimit;

            if (limitExceeded)
            {
                this.SetLimitExceededMessage(currentLimit.Value, this.availableMetadata.Count);
            }
            else
            {
                this.Metadata.Add(eventData);
                this.SetResultsMessage(this.Metadata.Count);
            }
        }

        /// <summary>
        /// Hides an event.
        /// </summary>
        private void HideEvent()
        {
            int? currentLimit = this.configurationService.GetParameterValueAsInt("SearchWithinAssetsLimit");

            bool limitExceeded = currentLimit.HasValue && this.Metadata.Count > currentLimit;

            if (limitExceeded)
            {
                this.SetLimitExceededMessage(currentLimit.Value, this.availableMetadata.Count);
            }
            else
            {
                this.SetResultsMessage(this.Metadata.Count);
            }
        }

        /// <summary>
        /// Sets the limit exceeded message to the results text.
        /// </summary>
        /// <param name="currentLimit">The current limit.</param>
        /// <param name="availableItems">The number of available items.</param>
        private void SetLimitExceededMessage(int currentLimit, int availableItems)
        {
            this.ResultsText = string.Format(ResultsMessagePattern, currentLimit, availableItems, "(limit exceeded)");
        }

        /// <summary>
        /// Sets the results message to the results text.
        /// </summary>
        /// <param name="currentItems">The number of current items.</param>
        private void SetResultsMessage(int currentItems)
        {
            this.ResultsText = string.Format(ResultsMessagePattern, currentItems, currentItems, string.Empty);
        }

        /// <summary>
        /// Adds a list of events to the current items collection.
        /// </summary>
        /// <param name="eventData">The event data being added.</param>
        private void AddEvent(EventData eventData)
        {
            double? offset = this.configurationService.GetParameterValueAsDouble("GSISOffsetSeconds");
            EventData currentEventData =
                this.availableMetadata.Where(x => x.Id == eventData.Id).FirstOrDefault();

            if (currentEventData == null)
            {
                eventData.Time =
                    eventData.Time.Add(TimeSpan.FromSeconds(offset.GetValueOrDefault()));
                this.availableMetadata.Add(eventData);
            }

            this.ShowEvent(eventData);
        }

        /// <summary>
        /// Removes the list of items from the current event collection.
        /// </summary>
        /// <param name="eventData">The eventData being removed.</param>
        private void RemoveEvent(EventData eventData)
        {
            EventData currentEventData =
                this.availableMetadata.Where(x => x.Id == eventData.Id).FirstOrDefault();

            if (currentEventData != null)
            {
                this.availableMetadata.Remove(currentEventData);
                this.Metadata.Remove(currentEventData);

                this.HideEvent();
            }
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.Search:
                    this.Search(parameter.Item2.ToString());
                    break;
            }
        }
    }
}
