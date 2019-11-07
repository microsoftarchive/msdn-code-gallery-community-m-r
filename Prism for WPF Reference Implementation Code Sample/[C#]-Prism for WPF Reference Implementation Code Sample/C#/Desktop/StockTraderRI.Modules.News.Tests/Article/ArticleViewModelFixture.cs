// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Modules.News.Article;
using StockTraderRI.Infrastructure.Interfaces;
using System.ComponentModel;
using Moq;
using StockTraderRI.Infrastructure.Models;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.PubSubEvents;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.News.Tests.Article
{
    [TestClass]
    public class ArticleViewModelFixture
    {
        [TestMethod]        
        public void WhenConstructed_InitializesValues()
        {
            // Prepare
            INewsFeedService newsFeedService = new Mock<INewsFeedService>().Object;
            IRegionManager regionManager = new Mock<IRegionManager>().Object;

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            var mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            // Act
            ArticleViewModel actual = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            // Verify
            Assert.IsNull(actual.Articles);
            Assert.IsNull(actual.SelectedArticle); 
            Assert.IsNotNull(actual.ShowArticleListCommand);
            Assert.IsNotNull(actual.ShowNewsReaderCommand);
        }

        [TestMethod]
        public void WhenConstructedWithDefaultConstructor()
        {
            // Prepare
            INewsFeedService newsFeedService = new Mock<INewsFeedService>().Object;
            IRegionManager regionManager = new Mock<IRegionManager>().Object;

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            var mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            // Act
            ArticleViewModel actual = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            // Verify
            Assert.IsNull(actual.Articles);
            Assert.IsNull(actual.SelectedArticle);
            Assert.IsNotNull(actual.ShowArticleListCommand);
            Assert.IsNotNull(actual.ShowNewsReaderCommand);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullNewsFeedService_Throws()
        {
            // Prepare
            INewsFeedService newsFeedService = null;
            IRegionManager regionManager = new Mock<IRegionManager>().Object;
            IEventAggregator eventAggregator = new Mock<IEventAggregator>().Object;

            // Act
            ArticleViewModel actual = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullRegionManager_Throws()
        {
            // Prepare
            INewsFeedService newsFeedService = new Mock<INewsFeedService>().Object;
            IRegionManager regionManager = null;
            IEventAggregator eventAggregator = new Mock<IEventAggregator>().Object;

            // Act
            ArticleViewModel actual = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullEventAggregator_Throws()
        {
            // Prepare
            INewsFeedService newsFeedService = new Mock<INewsFeedService>().Object;
            IRegionManager regionManager = new Mock<IRegionManager>().Object;
            IEventAggregator eventAggregator = null;

            // Act
            ArticleViewModel actual = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            // Verify
        }        

        [TestMethod]
        public void WhenCompanySymbolSet_PropertyIsUpdated()
        {
            // Prepare
            string companySymbol = "CompanySymbol";

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            Mock<INewsFeedService> mockNewsFeedService = new Mock<INewsFeedService>();
            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);

            INewsFeedService newsFeedService = mockNewsFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            ArticleViewModel target = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CompanySymbol")
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            target.CompanySymbol = companySymbol;

            // Verify
            Assert.AreEqual(companySymbol, target.CompanySymbol);
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void WhenCompanySymbolSet_NewsArticlesAreRetrieved()
        {
            // Prepare
            string companySymbol = "CompanySymbol";

            List<NewsArticle> articles = new List<NewsArticle>();

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            Mock<INewsFeedService> mockNewsFeedService = new Mock<INewsFeedService>();
            mockNewsFeedService.Setup(x => x.GetNews(companySymbol)).Returns(articles).Verifiable();
            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);

            INewsFeedService newsFeedService = mockNewsFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            ArticleViewModel target = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Articles")
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            target.CompanySymbol = companySymbol;

            // Verify
            mockNewsFeedService.VerifyAll();
            Assert.AreSame(target.Articles, articles);
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void WhenSelectedArticleSet_PropertyIsUpdated()
        {
            // Prepare
            NewsArticle newsArticle = new NewsArticle();

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            Mock<INewsFeedService> mockNewsFeedService = new Mock<INewsFeedService>();
            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);

            INewsFeedService newsFeedService = mockNewsFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            ArticleViewModel target = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "SelectedArticle")
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            target.SelectedArticle = newsArticle;

            // Verify
            Assert.AreEqual(newsArticle, target.SelectedArticle);
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void WhenShowNewsReaderCommandInvokes_RegionIsNavigated()
        {
            // Prepare
            NewsArticle newsArticle = new NewsArticle();

            var tickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>().Object;

            Mock<INewsFeedService> mockNewsFeedService = new Mock<INewsFeedService>();
            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);

            INewsFeedService newsFeedService = mockNewsFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            ArticleViewModel target = new ArticleViewModel(newsFeedService, regionManager, eventAggregator);

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "SelectedArticle")
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            target.SelectedArticle = newsArticle;

            // Verify
            Assert.AreEqual(newsArticle, target.SelectedArticle);
            Assert.IsTrue(propertyChangedRaised);
        }

    }
}
