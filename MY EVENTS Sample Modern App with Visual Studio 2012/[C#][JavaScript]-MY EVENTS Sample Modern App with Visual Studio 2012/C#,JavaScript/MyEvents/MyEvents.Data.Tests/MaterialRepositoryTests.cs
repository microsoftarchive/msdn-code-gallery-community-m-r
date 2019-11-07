using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data.Test
{
    [TestClass]
    public class MaterialRepositoryTests
    {
        [TestMethod]
        public void GetMaterial_Call_NotFail_Test()
        {
            var context = new MyEventsContext();
            int materialId = context.Materials.FirstOrDefault().MaterialId;

            IMaterialRepository target = new MaterialRepository();

            Material material = target.Get(materialId);

            Assert.IsNotNull(material);

        }

        [TestMethod]
        public void GetMaterial_Call_GetResults_Test()
        {
            var context = new MyEventsContext();
            int sessionId = context.Materials.FirstOrDefault().SessionId;
            int expectedCount = context.Materials.Count(q => q.SessionId == sessionId);

            IMaterialRepository target = new MaterialRepository();

            IEnumerable<Material> results = target.GetAll(sessionId);

            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count());
        }

        [TestMethod]
        public void AddMaterial_Added_NotFail_Test()
        {
            var context = new MyEventsContext();
            int sessionId = context.Sessions.FirstOrDefault().SessionId;
            int expected = context.Materials.Count() + 1;

            IMaterialRepository target = new MaterialRepository();
            Material material = new Material();
            material.SessionId = sessionId;
            material.Name = Guid.NewGuid().ToString();
            material.ContentType = "image/jpeg";
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            material.Content = encoding.GetBytes("content");
            
            target.Add(material);

            int actual = context.Materials.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteMaterial_Deleted_NotFail_Test()
        {
            var context = new MyEventsContext();
            var material = context.Materials.FirstOrDefault();
            int expected = context.Materials.Count() - 1;

            MaterialRepository target = new MaterialRepository();
            target.Delete(material.MaterialId);

            int actual = context.Materials.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteMaterial_NoExists_NotFail_Test()
        {
            var context = new MyEventsContext();
            var material = context.Materials.FirstOrDefault();
            int expected = context.Materials.Count();

            MaterialRepository target = new MaterialRepository();
            target.Delete(0);

            int actual = context.Materials.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMaterialOrganizerId_Call_GetResult_Test()
        {
            var context = new MyEventsContext();
            var material = context.Materials.Include("Session.EventDefinition").FirstOrDefault();

            MaterialRepository target = new MaterialRepository();

            int organizerId = target.GetOrganizerId(material.MaterialId);

            Assert.AreEqual(organizerId, material.Session.EventDefinition.OrganizerId);
        }

    }
}
