// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Practices.Prism.Logging;

namespace StockTraderRI.Modules.Position.Tests.Mocks
{
    internal class MockLogger : ILoggerFacade
    {
        public string LastMessage;

        public void Log(string message, Category category, Priority priority)
        {
            LastMessage = message;
        }
    }
}
