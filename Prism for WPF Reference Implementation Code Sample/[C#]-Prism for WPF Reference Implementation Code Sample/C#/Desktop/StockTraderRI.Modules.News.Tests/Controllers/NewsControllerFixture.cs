// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.News.Article;
using StockTraderRI.Modules.News.Controllers;
using StockTraderRI.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.PubSubEvents;

namespace StockTraderRI.Modules.News.Tests.Controllers
{
    [TestClass]
    public class NewsControllerFixture
    {
        [TestMethod]
        public void WhenArticleViewModelSelectedArticleChanged_NewsReaderViewModelNewsArticleUpdated()
        {
            // Prepare
            INewsFeedService newsFeedService = new Mock<INewsFeedService>().Object;
            IRegionManager regionManager = new Mock<IRegionManager>().Object;

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            var mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);
            IEventAggregator eventAggregator = mockEventAggregator.Object;


            ArticleViewModel articleViewModel = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);
            NewsReaderViewModel newsReaderViewModel = new NewsReaderViewModel();

            var controller = new NewsController(articleViewModel, newsReaderViewModel);

            NewsArticle newsArticle = new NewsArticle() { Title = "SomeTitle", Body = "Newsbody" };

            // Act
            articleViewModel.SelectedArticle = newsArticle;

            // Verify
            Assert.AreSame(newsArticle, newsReaderViewModel.NewsArticle);
        }

        internal class MockTickerSymbolSelectedEvent : TickerSymbolSelectedEvent
        {
            public Action<string> SubscribeArgumentAction;
            public Predicate<string> SubscribeArgumentFilter;
            public override SubscriptionToken Subscribe(Action<string> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<string> filter)
            {
                SubscribeArgumentAction = action;
                SubscribeArgumentFilter = filter;
                return null;
            }
        }

    }
}
