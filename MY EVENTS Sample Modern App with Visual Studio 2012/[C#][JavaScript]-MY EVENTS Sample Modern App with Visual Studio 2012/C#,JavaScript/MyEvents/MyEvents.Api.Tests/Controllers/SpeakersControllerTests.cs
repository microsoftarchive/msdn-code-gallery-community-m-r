using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Api.Controllers;
using MyEvents.Data;
using MyEvents.Data.Fakes;
using MyEvents.Model;

namespace MyEvents.Api.Tests.Controllers
{
    [TestClass]
    public class SpeakersControllerTests
    {
        [TestMethod]
        public void SpeakersController_Contructor_NotFail_Test()
        {
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            var target = new SpeakersController(eventDefinitionService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SpeakersController_ContructorWithNullParameter_ReturnsException_Test()
        {
            var target = new SpeakersController(null);
        }

        [TestMethod]
        public void GetTopSpeakers_GetResult_NotFail_Test()
        {
            var expected = new List<Speaker>() { new Speaker() };
            bool called = false;
            int organizerId = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetTopSpeakersInt32 = (id) =>
                {
                    called = true;
                    return expected;
                }
            };

            var target = new SpeakersController(eventDefinitionService);

            IEnumerable<Speaker> actual = target.GetTopSpeakers(organizerId);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }

    }
}
