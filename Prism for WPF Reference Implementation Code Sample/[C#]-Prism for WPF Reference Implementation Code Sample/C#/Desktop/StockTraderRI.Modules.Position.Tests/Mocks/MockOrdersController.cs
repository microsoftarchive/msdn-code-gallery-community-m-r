// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using StockTraderRI.Modules.Position.Controllers;
using Microsoft.Practices.Prism.Commands;

namespace StockTraderRI.Modules.Position.Tests.Mocks
{
    public class MockOrdersController : IOrdersController
    {
        #region IOrdersController Members
        DelegateCommand<string> _buyCommand = new DelegateCommand<string>(delegate { });
        public DelegateCommand<string> BuyCommand
        {
            get { return _buyCommand; }
        }

        DelegateCommand<string> _sellCommand = new DelegateCommand<string>(delegate { });
        public DelegateCommand<string> SellCommand
        {
            get { return _sellCommand; }
        }
        #endregion
    }
}
