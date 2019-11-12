// <copyright file="OverlaysViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlaysViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Overlays.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Microsoft.Practices.Composite.Events;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    public class OverlaysViewModel : BaseModel, IOverlaysViewModel, IHeaderInfoProvider<string>
    {
        private readonly IOverlaysView view;

        private readonly IEventAggregator eventAggregator;

        private ObservableCollection<OverlayAsset> overlays;

        public OverlaysViewModel(IOverlaysView view, IEventAggregator eventAggregator)
        {
            this.Overlays = new ObservableCollection<OverlayAsset>();
            this.view = view;
            this.view.SetViewModel(this);
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<AssetsAvailableEvent>().Subscribe(this.OnAssetsAvailable, true);
        }

        public object View
        {
            get
            {
                return this.view;
            }
        }

        public ObservableCollection<OverlayAsset> Overlays
        {
            get
            {
                return this.overlays;
            }

            private set
            {
                this.overlays = value;
                this.OnPropertyChanged("Overlays");
            }
        }

        public string HeaderInfo
        {
            get
            {
                return "Overlays";
            }
        }

        public void OnAssetsAvailable(Infrastructure.DataEventArgs<List<Asset>> args)
        {
            // process overlays
            if (args.Error == null)
            {
                this.Overlays.Clear();
                args.Data.Where(a => a.GetType() == typeof(OverlayAsset)).Cast<OverlayAsset>().ForEach(oa => this.Overlays.Add(oa));
            }
        }
    }
}
