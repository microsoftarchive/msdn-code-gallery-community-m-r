// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Practices.Prism.Commands;

namespace StockTraderRI.Modules.Position.Controllers
{

    public interface IOrdersController
    {
        DelegateCommand<string> BuyCommand { get; }
        DelegateCommand<string> SellCommand { get; }
    }
}
