// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using StockTraderRI.Infrastructure;
using Microsoft.Practices.Prism.PubSubEvents;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    /// <summary>
    /// Interaction logic for PositionPieChartView.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.ResearchRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PositionPieChartView : UserControl
    {
        public event EventHandler<DataEventArgs<string>> PositionSelected = delegate { };

        public PositionPieChartView()
        {
            InitializeComponent();
        }

        [Import]
        public IPositionPieChartViewModel Model
        {
            get
            {
                return this.DataContext as IPositionPieChartViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
