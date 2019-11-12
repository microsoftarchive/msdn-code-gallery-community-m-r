// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StockTraderRI.Modules.Watch.Services
{
    public interface IWatchListService
    {
        ObservableCollection<string> RetrieveWatchList();
        ICommand AddWatchCommand { get; set; }
    }
}