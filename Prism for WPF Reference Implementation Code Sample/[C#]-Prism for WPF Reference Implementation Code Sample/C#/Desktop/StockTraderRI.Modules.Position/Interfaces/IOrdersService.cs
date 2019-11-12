// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using StockTraderRI.Modules.Position.Models;

namespace StockTraderRI.Modules.Position.Interfaces
{
    public interface IOrdersService
    {
        void Submit(Order order);
    }
}