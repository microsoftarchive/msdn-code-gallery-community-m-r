// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTraderRI.Modules.News.Article;
using StockTraderRI.Infrastructure.Models;
using System.ComponentModel;

namespace StockTraderRI.Modules.News.Tests.Article
{
    [TestClass]
    public class NewsReaderViewModelFixture
    {
        [TestMethod]
        public void SetNewsArticleUpdatesProperty()
        {
            var target = new NewsReaderViewModel();

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "NewsArticle")
                {
                    propertyChangedRaised = true;
                }
            };

            NewsArticle article = new NewsArticle() { Title = "My Title", Body = "My Body" };
            target.NewsArticle = article;

            Assert.AreSame(article, target.NewsArticle);
            Assert.IsTrue(propertyChangedRaised);
        }
    }
}
