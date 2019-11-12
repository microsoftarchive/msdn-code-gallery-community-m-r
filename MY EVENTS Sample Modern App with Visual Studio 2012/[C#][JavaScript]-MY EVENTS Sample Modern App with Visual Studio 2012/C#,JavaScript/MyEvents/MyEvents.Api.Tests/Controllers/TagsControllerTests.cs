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
    public class TagsControllerTests
    {
        [TestMethod]
        public void TagsController_Contructor_NotFail_Test()
        {
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            var target = new TagsController(eventDefinitionService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TagsController_ContructorWithNullParameter_ReturnsException_Test()
        {
            var target = new TagsController(null);
        }

        [TestMethod]
        public void GetTopTags_GetResult_NotFail_Test()
        {
            var expected = new List<Tag>() { new Tag() };
            bool called = false;
            int organizerId = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetTopTagsInt32 = (id) =>
                {
                    called = true;
                    return expected;
                }
            };

            var target = new TagsController(eventDefinitionService);

            IEnumerable<Tag> actual = target.GetTopTags(organizerId);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }
       
    }
}
