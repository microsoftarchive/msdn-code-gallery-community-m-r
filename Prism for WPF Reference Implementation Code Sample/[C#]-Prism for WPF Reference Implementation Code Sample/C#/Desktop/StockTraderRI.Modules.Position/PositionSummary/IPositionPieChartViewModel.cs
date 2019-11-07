// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace StockTraderRI.Modules.Position.PositionSummary
{
    public interface IPositionPieChartViewModel
    {
        IObservablePosition Position { get; }
    }
}
