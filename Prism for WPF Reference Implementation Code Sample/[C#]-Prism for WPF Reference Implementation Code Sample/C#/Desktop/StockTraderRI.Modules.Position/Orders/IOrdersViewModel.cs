// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using StockTraderRI.Infrastructure.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace StockTraderRI.Modules.Position.Orders
{
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This interface is used as a contract type in the MEF container.")]
    public interface IOrdersViewModel : IHeaderInfoProvider<string>
    {
    }
}