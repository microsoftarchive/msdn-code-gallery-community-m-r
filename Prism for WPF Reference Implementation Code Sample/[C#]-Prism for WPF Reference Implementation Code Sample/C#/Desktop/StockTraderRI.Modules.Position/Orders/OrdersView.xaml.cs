// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel.Composition;
using System.Windows.Controls;
using StockTraderRI.Modules.Position.Interfaces;

namespace StockTraderRI.Modules.Position.Orders
{
    /// <summary>
    /// Interaction logic for TransactionView.xaml
    /// </summary>
    [Export(typeof(IOrdersView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class OrdersView : UserControl, IOrdersView
    {
        public OrdersView()
        {
            InitializeComponent();
        }

        [Import]
        public IOrdersViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
