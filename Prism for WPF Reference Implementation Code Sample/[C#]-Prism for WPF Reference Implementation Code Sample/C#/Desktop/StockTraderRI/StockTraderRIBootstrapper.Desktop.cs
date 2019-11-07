// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Practices.Prism.Logging;

namespace StockTraderRI
{
    public partial class StockTraderRIBootstrapper
    {
        private readonly EnterpriseLibraryLoggerAdapter _logger = new EnterpriseLibraryLoggerAdapter();

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }
    }
}
