using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Web.Areas.Organizer.Controllers;
using MyEvents.Data.Fakes;
using System.Web.Mvc;
using MyEvents.Web.Areas.Organizer.Models;
using MyEvents.Model;
using MyEvents.Web.Authentication;
using System.Web.SessionState;
using System.Web;
using MyEvents.Web.Services;
using MyEvents.Web.Services.Fakes;
using MyEvents.Web.Tests.Helpers;
using Microsoft.QualityTools.Testing.Fakes;

namespace MyEvents.Web.Tests.Organizer
{
    [TestClass]
    public class EventControllerTests
    {
        [TestMethod]
        public void Create_ShouldReturnAEditEventViewModelWithAnEmptyEventDefinition()
        {
            int expectedEventDefinitionId = 0;
            var eventRepository = new StubIEventDefinitionRepository();
            var authenticationService = new StubIAuthorizationService();


            var controller = new EventController(eventRepository, authenticationService);

            var result = controller.Create() as ViewResult;
            var model = result.Model as EditEventViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(expectedEventDefinitionId, model.EventDefinitionId);
        }

        [TestMethod]
        public void Edit_ShouldReturnAEditEventViewModelWithTheEventDefinitonData()
        {
            int expectedEventDefinitionId = 1;
            int organizerId = 1;
            var eventRepository = new StubIEventDefinitionRepository();
            var authenticationService = new StubIAuthorizationService();
            var eventDefinition = GetEventDefinition(expectedEventDefinitionId, organizerId);

            eventRepository.GetByIdInt32 = (eventDefinitionId) =>
            {
                return eventDefinition;
            };

            var identity = GetIdentity(organizerId);
            var controller = new EventController(eventRepository, authenticationService);
            controller.SetFakeContext();
            controller.AddSessionValue("CURRENT_LOGO", null);

            var result = controller.Edit(identity, expectedEventDefinitionId) as ViewResult;
            var model = result.Model as EditEventViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(expectedEventDefinitionId, model.EventDefinitionId);
        }

        private MyEventsIdentity GetIdentity(int userId)
        {
            return new MyEventsIdentity()
            {
                UserId = userId
            };
        }

        private EventDefinition GetEventDefinition(int eventDefinitionId, int organizerId)
        {
            return new EventDefinition
            {
                EventDefinitionId = eventDefinitionId,
                OrganizerId = organizerId
            };
        }
    }
}
