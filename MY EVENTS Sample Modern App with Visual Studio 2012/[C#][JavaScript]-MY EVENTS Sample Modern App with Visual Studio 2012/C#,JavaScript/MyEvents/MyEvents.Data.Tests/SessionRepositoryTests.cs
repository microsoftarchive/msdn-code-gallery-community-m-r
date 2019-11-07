using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Model;
using System.Linq;

namespace MyEvents.Data.Test
{
    [TestClass]
    public class SessionRepositoryTests
    {
        [TestMethod]
        public void GetAllSessions_Call_NotFail_Test()
        {
            var context = new MyEventsContext();
            int eventDefinitionId = context.Sessions.FirstOrDefault().EventDefinitionId;

            int expectedCount = context.Sessions.Count(q => q.EventDefinitionId == eventDefinitionId);

            ISessionRepository target = new SessionRepository();

            IEnumerable<Session> results = target.GetAll(eventDefinitionId);

            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count());
        }

        [TestMethod]
        public void GetAllSessions_Call_CalculateScore_Test()
        {
            var context = new MyEventsContext();
            int eventDefinitionId = context.Sessions.FirstOrDefault().EventDefinitionId;

            int expectedCount = context.Sessions.Count(q => q.EventDefinitionId == eventDefinitionId);

            ISessionRepository target = new SessionRepository();

            IEnumerable<Session> results = target.GetAll(eventDefinitionId);

            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count());

            foreach (var session in results)
                ValidateScore(session.SessionId, session.Score);
        }

        private void ValidateScore(int sessionId, double score)
        {
            var context = new MyEventsContext();
            var session = context.Sessions.Include("SessionRegisteredUsers").FirstOrDefault(q => q.SessionId == sessionId);
            double expected = session.SessionRegisteredUsers.Where(sr => sr.Rated).Average(sr => sr.Score);

            Assert.AreEqual(expected, session.Score);
        }

        [TestMethod]
        public void GetSession_Call_NotFail_Test()
        {
            var context = new MyEventsContext();
            int sessionId = context.Sessions.FirstOrDefault().SessionId;

            ISessionRepository target = new SessionRepository();

            Session result = target.Get(sessionId);

            Assert.IsNotNull(result);
            Assert.AreEqual(sessionId, result.SessionId);
        }

        [TestMethod]
        public void GetSession_CalculateScore_Test()
        {
            var context = new MyEventsContext();
            var session = context.Sessions.Include("SessionRegisteredUsers").FirstOrDefault(q => q.SessionRegisteredUsers.Any());
            double expected = session.SessionRegisteredUsers.Where(sr => sr.Rated).Average(sr => sr.Score);

            ISessionRepository target = new SessionRepository();

            Session result = target.Get(session.SessionId);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Score);
        }

        [TestMethod]
        public void AddSession_Added_NotFail_Test()
        {
            var context = new MyEventsContext();
            int eventDefinitionId = context.EventDefinitions.FirstOrDefault().EventDefinitionId;

            int expected = context.Sessions.Count() + 1;

            ISessionRepository target = new SessionRepository();

            Session session = new Session();
            session.EventDefinitionId = eventDefinitionId;
            session.Title = Guid.NewGuid().ToString();
            session.Description = Guid.NewGuid().ToString();
            session.Speaker = Guid.NewGuid().ToString();
            session.Biography = Guid.NewGuid().ToString();
            session.TwitterAccount = Guid.NewGuid().ToString();
            session.StartTime = DateTime.Now;
            session.TimeZoneOffset = 2;
            session.Duration = 60;

            target.Add(session);

            int actual = context.Sessions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateSession_Updated_NotFail_Test()
        {
            var context = new MyEventsContext();
            var sessionToUpdate = context.Sessions.FirstOrDefault();

            int expected = context.Sessions.Count() + 1;

            ISessionRepository target = new SessionRepository();

            sessionToUpdate.Title = Guid.NewGuid().ToString();
            target.Update(sessionToUpdate);

            var sessionUpdated = context.Sessions.FirstOrDefault(q => q.SessionId == sessionToUpdate.SessionId);

            Assert.AreEqual(sessionToUpdate.Title, sessionUpdated.Title);
        }

        [TestMethod]
        public void DeleteSession_Deleted_NotFail_Test()
        {
            var context = new MyEventsContext();
            var session = context.Sessions.FirstOrDefault();
            int expected = context.Sessions.Count() - 1;

            ISessionRepository target = new SessionRepository();
            target.Delete(session.SessionId);

            int actual = context.Sessions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteSession_NoExists_NotFail_Test()
        {
            var context = new MyEventsContext();
            var session = context.Sessions.FirstOrDefault();
            int expected = context.Sessions.Count();

            ISessionRepository target = new SessionRepository();
            target.Delete(0);

            int actual = context.Sessions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSessionOrganizerId_Call_GetResult_Test()
        {
            var context = new MyEventsContext();
            var session = context.Sessions.Include("EventDefinition").FirstOrDefault();

            ISessionRepository target = new SessionRepository();

            int organizerId = target.GetOrganizerId(session.SessionId);

            Assert.AreEqual(organizerId, session.EventDefinition.OrganizerId);
        }

    }
}
