// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockTraderRI.Modules.Watch.Services;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.ComponentModel.Composition;


namespace StockTraderRI.Modules.Watch.AddWatch
{
    [Export(typeof(AddWatchViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddWatchViewModel : BindableBase
    {
        private string stockSymbol;
        private IWatchListService watchListService;

        [ImportingConstructor]
        public AddWatchViewModel(IWatchListService watchListService)
        {
            if (watchListService == null)
            {
                throw new ArgumentNullException("watchListService");
            }

            this.watchListService = watchListService;
        }

        public string StockSymbol
        {
            get { return stockSymbol; }
            set
            {
                SetProperty(ref stockSymbol, value);
            }
        }

        public ICommand AddWatchCommand { get { return this.watchListService.AddWatchCommand; } }
    }
}
