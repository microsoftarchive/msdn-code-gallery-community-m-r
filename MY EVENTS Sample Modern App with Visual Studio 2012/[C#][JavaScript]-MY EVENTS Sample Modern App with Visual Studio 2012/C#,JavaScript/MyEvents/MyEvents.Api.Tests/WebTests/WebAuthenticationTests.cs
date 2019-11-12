using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MyEvents.Api.Authentication;

namespace MyEvents.Api.Tests.WebTests
{
    [TestClass]
    public class WebAuthenticationTests
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
        public void LogOn_Integration_CallWebAPI_NotFail_Test()
        {
            string facebook_token = "invalidtoken";
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);


            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);


            IAsyncResult ar = service.AuthenticationService.LogOnAsync(facebook_token, (Client.AuthenticationResponse response) =>
            {
                TestHelper.ValidateResult(null, response, manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetFakeAuthorization_Integration_CallWebAPI_NotFail_Test()
        {
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);


            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.AuthenticationService.GetFakeAuthorizationAsync((Client.AuthenticationResponse response) =>
            {
                try
                {
                    Assert.IsNotNull(response);
                    Assert.IsNotNull(response.Token);
                }
                catch (Exception ex)
                {
                    exceptionResult = ex;
                }
                finally
                {
                    manualResetEvent.Set();
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }
    }
}
