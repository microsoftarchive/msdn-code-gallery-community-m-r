using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Api.Authentication.Fakes;
using MyEvents.Api.Controllers;
using MyEvents.Data;
using MyEvents.Data.Fakes;
using MyEvents.Model;

namespace MyEvents.Api.Tests.Controllers
{
    [TestClass]
    public class MaterialsControllerTests
    {
        [TestMethod]
        public void MaterialsController_Contructor_NotFail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository();
            var target = new MaterialsController(materialRepository, sessionRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialsController_Contructor_MaterialArgumentNullException_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository();
            var target = new MaterialsController(null, sessionRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialsController_Contructor_SessionArgumentNullException_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository();
            var target = new MaterialsController(materialRepository, null);
        }

        [TestMethod]
        public void GetAllMaterials_GetEmptyResults_NotFail_Test()
        {
            int expectedSessionId = 10;
            bool called = false;
            var expected = new List<Material>();

            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                GetAllInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                    return expected;
                }
            };

            var target = new MaterialsController(materialRepository, sessionRepository);

            IEnumerable<Material> actual = target.GetAllMaterials(expectedSessionId);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }

        [TestMethod]
        public void GetAllMaterials_GetResults_NotFail_Test()
        {
            int expectedSessionId = 10;
            bool called = false;
            var expected = new List<Material>() { new Material() };

            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                GetAllInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                    return expected;
                }
            };

            var target = new MaterialsController(materialRepository, sessionRepository);

            IEnumerable<Material> actual = target.GetAllMaterials(expectedSessionId);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }

        [TestMethod]
        public void GetMaterials_NotFail_Test()
        {
            bool called = false;
            var expected = new Material() { MaterialId = 10 };

            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                GetInt32 = materialId =>
                {
                    Assert.AreEqual(expected.MaterialId, materialId);
                    called = true;
                    return expected;
                }
            };

            var target = new MaterialsController(materialRepository, sessionRepository);

            Material actual = target.Get(expected.MaterialId);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void PostMaterial_NotFail_Test()
        {
            bool called = false;
            var expectedmaterial = new Material() { MaterialId = 1 };
            int organizerId = 10;
            
            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetOrganizerIdInt32 = (sessionId) =>
                {
                    return organizerId;
                }
            };

            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                AddMaterial = material =>
                {
                    Assert.AreEqual(expectedmaterial.MaterialId, material.MaterialId);
                    called = true;
                    return expectedmaterial.MaterialId;
                },
                GetInt32 = materialId =>
                {
                    return expectedmaterial;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return organizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new MaterialsController(materialRepository, sessionRepository);

                var actual = target.Post(expectedmaterial);

                Assert.IsTrue(called);
                Assert.AreEqual(expectedmaterial.MaterialId, actual);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostMaterial_UnauthorizedException_Test()
        {
            var expectedmaterial = new Material() { MaterialId = 1 };
            int organizerId = 10;

            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetOrganizerIdInt32 = (sessionId) =>
                {
                    return organizerId;
                }
            };

            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                GetInt32 = materialId =>
                {
                    return expectedmaterial;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new MaterialsController(materialRepository, sessionRepository);

                var actual = target.Post(expectedmaterial);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostMaterial_ArgumentNullException_Test()
        {
            ICommentRepository commentRepository = new StubICommentRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository();

            var target = new MaterialsController(materialRepository, sessionRepository);

            target.Post(null);
        }

        [TestMethod]
        public void DeleteMaterial_Deleted_NotFail_Test()
        {
            bool called = false;
            var expectedmaterial = new Material() { MaterialId = 1, SessionId = 1 };
            int organizerId = 10;

            ISessionRepository sessionRepository = new StubISessionRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                DeleteInt32 = materialId =>
                {
                    Assert.AreEqual(expectedmaterial.MaterialId, materialId);
                    called = true;
                },
                GetOrganizerIdInt32 = materialId =>
                {
                    return organizerId;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return organizerId;  };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };
                var target = new MaterialsController(materialRepository, sessionRepository);

                target.Delete(expectedmaterial.MaterialId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteMaterial_UnauthorizedException_Test()
        {
            var expectedmaterial = new Material() { MaterialId = 1, SessionId = 1 };
            int organizerId = 10;

            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetOrganizerIdInt32 = (sessionId) =>
                {
                    return organizerId;
                }
            };

            IMaterialRepository materialRepository = new StubIMaterialRepository()
            {
                GetInt32 = materialId =>
                {
                    return expectedmaterial;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 100000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };
                var target = new MaterialsController(materialRepository, sessionRepository);

                target.Delete(expectedmaterial.MaterialId);
            }
        }
    }
}
