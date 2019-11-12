// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Position.Orders
{
    [Export(typeof(IOrdersViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class OrdersViewModel : IOrdersViewModel
    {
        public string HeaderInfo
        {
            get { return "BUY & SELL"; }
        }
    }
}
