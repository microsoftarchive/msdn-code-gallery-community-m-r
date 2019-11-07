using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MyEvents.Data;

namespace MyEvents.Api.Tests.WebTests
{
    [TestClass]
    public class WebReportTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetTopSpeakers_Integration_CallWebAPI_GetResults_NotFail_Test()
        {
            int expected = 2;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);
            int organizerId = Int32.Parse(ConfigurationManager.AppSettings["fakeUserId"]);
        
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.ReportService.GetTopSpeakersAsync(organizerId, (IList<Client.Speaker> speakers) =>
            {
                TestHelper.ValidateResult(expected,speakers.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetTopTags_Integration_CallWebAPI_GetResults_NotFail_Test()
        {
            int expected = 5;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);
            int organizerId = Int32.Parse(ConfigurationManager.AppSettings["fakeUserId"]);

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.ReportService.GetTopTagsAsync(organizerId, (IList<Client.Tag> tags) =>
            {
                TestHelper.ValidateResult(expected, tags.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }
    }
}
