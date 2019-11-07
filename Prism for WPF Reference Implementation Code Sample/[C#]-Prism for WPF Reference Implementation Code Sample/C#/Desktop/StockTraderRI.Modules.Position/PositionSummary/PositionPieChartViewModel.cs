// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    [Export(typeof(IPositionPieChartViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PositionPieChartViewModel : IPositionPieChartViewModel
    {
        public IObservablePosition Position { get; private set; }

        [ImportingConstructor]
        public PositionPieChartViewModel(IObservablePosition observablePosition)
        {
            this.Position = observablePosition;
        }
    }
}
