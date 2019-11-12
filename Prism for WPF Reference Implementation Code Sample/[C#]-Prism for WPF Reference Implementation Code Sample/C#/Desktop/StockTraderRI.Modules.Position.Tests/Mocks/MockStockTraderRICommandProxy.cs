// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Practices.Prism.Commands;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.Position.Tests.Mocks
{
    public class MockStockTraderRICommandProxy : StockTraderRICommandProxy
    {
        CompositeCommand _submitAllOrdersCommand = new CompositeCommand();
        CompositeCommand _cancelAllOrdersCommand = new CompositeCommand();
        CompositeCommand _submitOrderCommand = new CompositeCommand(true);
        CompositeCommand _cancelOrderCommand = new CompositeCommand(true);

        public override CompositeCommand SubmitOrderCommand
        {
            get
            {
                return this._submitOrderCommand;
            }
        }

        public override CompositeCommand SubmitAllOrdersCommand
        {
            get
            {
                return this._submitAllOrdersCommand;
            }
        }
        public override CompositeCommand CancelOrderCommand
        {
            get
            {
                return this._cancelOrderCommand;
            }
        }

        public override CompositeCommand CancelAllOrdersCommand
        {
            get
            {
                return this._cancelAllOrdersCommand;
            }
        }
    }
}
