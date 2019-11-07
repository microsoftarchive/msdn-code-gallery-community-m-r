using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Api.Authentication;
using MyEvents.Api.Authentication.Fakes;
using MyEvents.Api.Client;
using MyEvents.Api.Controllers;
using MyEvents.Data;
using MyEvents.Data.Fakes;

namespace MyEvents.Api.Tests.Controllers
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        [TestMethod]
        public void EventDefinitionController_Contructor_NotFail_Test()
        {
            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository();
            IFacebookService facebookService = new StubIFacebookService();
            var target = new AuthenticationController(eventDefinitionService, facebookService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventDefinitionController_Contructor_IRegisteredUserRepositoryIsNull_Test()
        {
            IFacebookService facebookService = new StubIFacebookService();
            var target = new AuthenticationController(null, facebookService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventDefinitionController_Contructor_IFacebookServiceIsNull_Test()
        {
            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository();
            var target = new AuthenticationController(eventDefinitionService, null);
        }

        [TestMethod]
        public void GetFakeAuthorization_Coverage_NotFail_Test()
        {
            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository();
            IFacebookService facebookService = new StubIFacebookService();
            var target = new AuthenticationController(eventDefinitionService, facebookService);

            MyEvents.Api.Authentication.AuthenticationResponse response = target.GetFakeAuthorization();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Token);
        }

        [TestMethod]
        public void LogOn_Logged_Test()
        {
            string token = "mytoken";

            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository()
            {
                AddRegisteredUser = (user) =>
                    {
                        Assert.AreEqual(user.FacebookId, "facebookId");
                        return 10;
                    }
            };
            IFacebookService facebookService = new StubIFacebookService()
            {
                GetUserInformationString = (facebookToken) =>
                    {
                        return new Model.RegisteredUser() { FacebookId = "facebookId" };
                    }
            };
            var target = new AuthenticationController(eventDefinitionService, facebookService);

            MyEvents.Api.Authentication.AuthenticationResponse response = target.LogOn(token);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Token);
        }

        [TestMethod]
        public void LogOn_TokenNotValid_Test()
        {
            string token = "mytoken";

            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository()
            {
                GetString = (facebookId) =>
                {
                    Assert.Fail();
                    return null;
                }
            };
            IFacebookService facebookService = new StubIFacebookService()
            {
                GetUserInformationString = (facebookToken) =>
                {
                    return null;
                }
            };
            var target = new AuthenticationController(eventDefinitionService, facebookService);

            MyEvents.Api.Authentication.AuthenticationResponse response = target.LogOn(token);

            Assert.IsNull(response);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void LogOn_TokenException_Test()
        {
            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository();
            IFacebookService facebookService = new StubIFacebookService();
            var target = new AuthenticationController(eventDefinitionService, facebookService);

            target.LogOn(string.Empty);
        }

        [TestMethod]
        public void LogOn_UserNotExists_Test()
        {
            string token = "mytoken";

            IRegisteredUserRepository eventDefinitionService = new StubIRegisteredUserRepository()
            {
                AddRegisteredUser = (user) =>
                {
                    Assert.AreEqual(user.FacebookId, "facebookId");
                    return 0;
                }
            };
            IFacebookService facebookService = new StubIFacebookService()
            {
                GetUserInformationString = (facebookToken) =>
                {
                    return new Model.RegisteredUser() { FacebookId = "facebookId" };
                }
            };
            var target = new AuthenticationController(eventDefinitionService, facebookService);

            MyEvents.Api.Authentication.AuthenticationResponse response = target.LogOn(token);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetUserInformation_FromFacebook_TokenNotValid_Test()
        {
            IFacebookService facebookService = new FacebookService();
            Model.RegisteredUser user = facebookService.GetUserInformation("invalidtoken");
            Assert.IsNull(user);
        }
    }
}
