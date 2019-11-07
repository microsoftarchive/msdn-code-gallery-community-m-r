using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Model;
using System.Linq;

namespace MyEvents.Data.Test
{
    [TestClass]
    public class RegisteredUserRepositoryTests
    {
        [TestMethod]
        public void GetRegisteredUser_Call_GetResults_Test()
        {
            var context = new MyEventsContext();
            string registeredFacebookId = context.RegisteredUsers.FirstOrDefault().FacebookId;

            IRegisteredUserRepository target = new RegisteredUserRepository();
            RegisteredUser registeredUser = target.Get(registeredFacebookId);

            Assert.IsNotNull(registeredUser);
            Assert.AreEqual(registeredFacebookId, registeredUser.FacebookId);
        }

        [TestMethod]
        public void GetRegisteredUserById_Call_GetResults_Test()
        {
            var context = new MyEventsContext();
            int registeredUserId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;

            IRegisteredUserRepository target = new RegisteredUserRepository();
            RegisteredUser registeredUser = target.GetById(registeredUserId);

            Assert.IsNotNull(registeredUser);
            Assert.AreEqual(registeredUserId, registeredUser.RegisteredUserId);
        }

        [TestMethod]
        public void GetAllByEventId_Call_NotFail_Test()
        {
            var context = new MyEventsContext();

            int eventDefinitionId = context.RegisteredUsers.Include("AttendeeEventDefinitions")
                .FirstOrDefault(q => q.AttendeeEventDefinitions.Any()).AttendeeEventDefinitions.First().EventDefinitionId;

            int expectedCount = context.RegisteredUsers.Include("AttendeeEventDefinitions")
                .Count(q => q.AttendeeEventDefinitions.Any(s => s.EventDefinitionId == eventDefinitionId));

            IRegisteredUserRepository target = new RegisteredUserRepository();

            IEnumerable<RegisteredUser> results = target.GetAllByEventId(eventDefinitionId);

            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count());
        }

        [TestMethod]
        public void GetAllBySessionId_Call_NotFail_Test()
        {
            var context = new MyEventsContext();
            int sessionId = context.Sessions.FirstOrDefault().SessionId;
            int expectedCount = context.SessionRegisteredUsers
                .Where(q => q.SessionId == sessionId).Count();
               
            IRegisteredUserRepository target = new RegisteredUserRepository();

            IEnumerable<RegisteredUser> results = target.GetAllBySessionId(sessionId);

            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count());
        }

        [TestMethod]
        public void GetEventDefinitions_Call_GetResults_NotFail_Test()
        {
            var context = new MyEventsContext();
            
            int registerUserId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;

            IRegisteredUserRepository target = new RegisteredUserRepository();

            IEnumerable<EventDefinition> results = target.GetEventDefinitions(registerUserId);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetSessions_Call_GetResults_NotFail_Test()
        {
            var context = new MyEventsContext();

            var session = context.Sessions
                .Include("SessionRegisteredUsers")
                .Where(q => q.SessionRegisteredUsers.Any())
                .FirstOrDefault();

            int sessionId = session.EventDefinitionId;
            int registeredUserId = session.SessionRegisteredUsers.First().RegisteredUserId;
            IRegisteredUserRepository target = new RegisteredUserRepository();

            IEnumerable<Session> results = target.GetSessions(session.EventDefinitionId, registeredUserId);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void AddRegisteredUser_Added_NotFail_Test()
        {
            IRegisteredUserRepository target = new RegisteredUserRepository();
            RegisteredUser registeredUser = new RegisteredUser();
            registeredUser.Name = Guid.NewGuid().ToString();
            registeredUser.FacebookId = Guid.NewGuid().ToString();
            registeredUser.Email = Guid.NewGuid().ToString();
            Assert.IsTrue(target.Add(registeredUser) > 0);
        }

        [TestMethod]
        public void AddRegisteredUser_NotAdded_AlreadyExists_Test()
        {
            var context = new MyEventsContext();
            var user = context.RegisteredUsers.FirstOrDefault();
            IRegisteredUserRepository target = new RegisteredUserRepository();
            RegisteredUser registeredUser = new RegisteredUser();
            registeredUser.Name = Guid.NewGuid().ToString();
            registeredUser.FacebookId = user.FacebookId;
            registeredUser.Email = Guid.NewGuid().ToString();
            int actual = target.Add(registeredUser);
            Assert.AreEqual(user.RegisteredUserId, actual );
        }

        [TestMethod]
        public void AddRegisteredUserToSession_Added_NotFail_Test()
        {
            var context = new MyEventsContext();
            int eventDefinitionId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;
            int sessionId = context.Sessions.FirstOrDefault().SessionId;

            int expected = context.SessionRegisteredUsers.Count() + 1;

            IRegisteredUserRepository target = new RegisteredUserRepository();
            target.AddRegisteredUserToSession(eventDefinitionId, sessionId);

            int actual = context.SessionRegisteredUsers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteRegisteredUserFromSession_Deleted_NotFail_Test()
        {
            var context = new MyEventsContext();
            SessionRegisteredUser sessionRegisteredUser = context.SessionRegisteredUsers.FirstOrDefault();

            int expected = context.SessionRegisteredUsers.Count() - 1;

            IRegisteredUserRepository target = new RegisteredUserRepository();
            target.DeleteRegisteredUserFromSession(sessionRegisteredUser.RegisteredUserId, sessionRegisteredUser.SessionId);

            int actual = context.SessionRegisteredUsers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddRegisteredUserToEvent_Added_NotFail_Test()
        {
            var context = new MyEventsContext();
            int registeredUserId = context.RegisteredUsers.First().RegisteredUserId;
            var eventDefinition = context.EventDefinitions.Include("RegisteredUsers")
                .Where(e => !e.RegisteredUsers.Any(r => r.RegisteredUserId == registeredUserId)).First();
            int eventDefinitionId = eventDefinition.EventDefinitionId;
            int expected = eventDefinition.RegisteredUsers.Count() + 1;

            IRegisteredUserRepository target = new RegisteredUserRepository();
            target.AddRegisteredUserToEvent(registeredUserId, eventDefinitionId);

            var contextAfter = new MyEventsContext();
            var eventDefinitionAfter = contextAfter.EventDefinitions.Include("RegisteredUsers").First(e => e.EventDefinitionId == eventDefinitionId);
            int actual = eventDefinitionAfter.RegisteredUsers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteRegisteredUserFromEvent_Deleted_NotFail_Test()
        {
            var context = new MyEventsContext();

            var eventDefinition = context.EventDefinitions.Include("RegisteredUsers")
                    .Where(e => e.RegisteredUsers.Any()).First();

            int registeredUserId = eventDefinition.RegisteredUsers.First().RegisteredUserId;
            int eventDefinitionId = eventDefinition.EventDefinitionId;
            int expected = eventDefinition.RegisteredUsers.Count() - 1; 

            IRegisteredUserRepository target = new RegisteredUserRepository();
            target.DeleteRegisteredUserFromEvent(registeredUserId, eventDefinitionId);

            var contextAfter = new MyEventsContext();
            var eventDefinitionAfter = contextAfter.EventDefinitions.Include("RegisteredUsers").First(e => e.EventDefinitionId == eventDefinitionId);
            int actual = eventDefinitionAfter.RegisteredUsers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetIfUserIsRegistered_NotRegistered_Test()
        {
            int eventDefinitionId = 10;
            int registeredUserId = 10000;
            IRegisteredUserRepository target = new RegisteredUserRepository();
            bool actual = target.GetIfUserIsRegistered(eventDefinitionId, registeredUserId);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GetIfUserIsRegistered_Test()
        {
            int eventDefinitionId = 0;
            int registeredUserId = 0;

            using (var context = new MyEventsContext())
            {
                var registeredUser = context.RegisteredUsers.Include("AttendeeEventDefinitions").FirstOrDefault(a => a.AttendeeEventDefinitions.Any());
                eventDefinitionId = registeredUser.AttendeeEventDefinitions.FirstOrDefault().EventDefinitionId;
                registeredUserId = registeredUser.RegisteredUserId;
            }

            IRegisteredUserRepository target = new RegisteredUserRepository();
            bool actual = target.GetIfUserIsRegistered(eventDefinitionId, registeredUserId);
            Assert.IsTrue(actual);
        }


    }
}
