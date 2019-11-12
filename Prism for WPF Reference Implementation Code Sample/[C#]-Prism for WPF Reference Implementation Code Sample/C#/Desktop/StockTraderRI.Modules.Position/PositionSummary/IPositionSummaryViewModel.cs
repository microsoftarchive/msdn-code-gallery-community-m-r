// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Input;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Position.Interfaces;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    public interface IPositionSummaryViewModel : IHeaderInfoProvider<string>
    {
        IObservablePosition Position { get; }

        ICommand BuyCommand { get; }

        ICommand SellCommand { get; }
    }
}