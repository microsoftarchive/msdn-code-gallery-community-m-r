// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    public interface IObservablePosition
    {
        ObservableCollection<PositionSummaryItem> Items { get; }
    }
}