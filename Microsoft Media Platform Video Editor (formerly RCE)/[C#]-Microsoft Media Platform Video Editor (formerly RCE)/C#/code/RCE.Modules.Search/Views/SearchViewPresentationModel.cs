// <copyright file="SearchViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SearchViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Search.Services;

    public class SearchViewPresentationModel : BaseModel, ISearchViewPresentationModel
    {
        /// <summary>
        /// The <see cref="IConfigurationService"/> to get configuration parameters.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The <seealso cref="IAssetsDataServiceFacade"/> instance used to used to get the list of assets.
        /// </summary>
        private readonly IAssetsDataServiceFacade assetsDataServiceFacade;

        /// <summary>
        /// The <see cref="IEventAggregator"/> to publish/subscribe for the events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The <see cref="DelegateCommand{T}"/> to handle the serach.
        /// </summary>
        private readonly DelegateCommand<string> searchCommand;

        private readonly ISearchServiceBridge searchServiceBridge;

        private bool searchIntegrationEnabled;

        private bool canSearch;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewPresentationModel"/> class.
        /// </summary>
        /// <param name="view">The view associated with this presentation model.</param>
        /// <param name="configurationService">The configuration service instance used to retrieve configuration values.</param>
        /// <param name="assetsDataServiceFacade">The service facade used to search for assets.</param>
        /// <param name="eventAggregator">The event aggregator instance used to publish/subscribe for events.</param>
        public SearchViewPresentationModel(ISearchView view, IConfigurationService configurationService, IAssetsDataServiceFacade assetsDataServiceFacade, IEventAggregator eventAggregator, ISearchServiceBridge searchServiceBridge)
        {
            this.configurationService = configurationService;

            this.assetsDataServiceFacade = assetsDataServiceFacade;
            this.assetsDataServiceFacade.LoadAssetsCompleted += this.OnLoadAssetsCompleted;

            this.searchServiceBridge = searchServiceBridge;
            this.searchServiceBridge.ResultsAvailable += this.OnLoadAssetsCompleted;

            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<AssetsLoadingEvent>().Subscribe(this.OnAssetsLoading, true);

            this.searchCommand = new DelegateCommand<string>(this.Search);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);

            this.SearchIntegrationEnabled = this.configurationService.GetParameterValueAsBoolean("SearchIntegrationEnabled").GetValueOrDefault();
            this.CanSearch = true;

            this.View = view;
            this.View.Model = this;

            if (!this.SearchIntegrationEnabled)
            {
                this.Search(null);
            }
        }

        public ISearchView View { get; private set; }
        
        public string Title { get; set; }
        
        public DelegateCommand<string> SearchCommand
        {
            get { return this.searchCommand; }
        }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public bool SearchIntegrationEnabled
        {
            get
            {
                return this.searchIntegrationEnabled;
            }

            set
            {
                this.searchIntegrationEnabled = value;
                this.OnPropertyChanged("SearchIntegrationEnabled");
            }
        }

        public bool CanSearch
        {
            get
            {
                return this.canSearch;
            }

            set
            {
                this.canSearch = value;
                this.OnPropertyChanged("CanSearch");
            }
        }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Search;
            }
        }

        private void Search(string parameter)
        {
            if (this.searchIntegrationEnabled)
            {
                this.searchServiceBridge.OpenPopup();
            }
            else
            {
                this.eventAggregator.GetEvent<AssetsLoadingEvent>().Publish(null);
                this.assetsDataServiceFacade.LoadAssetsAsync(parameter, this.configurationService.GetMaxNumberOfItems());
            }
        }

        private void OnAssetsLoading(object e)
        {
            this.CanSearch = false;
        }

        private void OnLoadAssetsCompleted(object sender, Infrastructure.DataEventArgs<List<Asset>> e)
        {
            this.CanSearch = true;
            this.eventAggregator.GetEvent<AssetsAvailableEvent>().Publish(e);
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> tuple)
        {
            switch (tuple.Item1)
            {
                case KeyboardAction.Search:
                    this.Search(tuple.Item2.ToString());
                    break;
            }
        }
    }
}