// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace StockTraderRI.Modules.Position.Orders
{
    public class ValueDescription<T> where T: struct
    {
        public ValueDescription()
        {
        }

        public ValueDescription(T value, string description)
        {
            Value = value;
            Description = description;
        }
        public T Value { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return Description;
        }
    }
}