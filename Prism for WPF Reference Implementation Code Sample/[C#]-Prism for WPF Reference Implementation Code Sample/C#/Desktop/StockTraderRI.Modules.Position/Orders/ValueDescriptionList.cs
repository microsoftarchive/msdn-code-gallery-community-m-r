// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace StockTraderRI.Modules.Position.Orders
{
    public class ValueDescriptionList<T> : List<ValueDescription<T>>, IValueDescriptionList<T> where T : struct
    {

    }
}
