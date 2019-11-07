// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Infrastructure.Interfaces
{
    public interface INewsFeedService
    {
        IList<NewsArticle> GetNews(string tickerSymbol);
        bool HasNews(string tickerSymbol);
        event EventHandler<NewsFeedEventArgs> Updated;

        
    }
}
