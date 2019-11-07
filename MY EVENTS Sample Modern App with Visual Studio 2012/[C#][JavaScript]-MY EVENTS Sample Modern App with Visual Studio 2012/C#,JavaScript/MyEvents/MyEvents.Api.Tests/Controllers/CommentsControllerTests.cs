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
    public class CommentsControllerTests
    {
        [TestMethod]
        public void CommentsController_Contructor_NotFail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository();
            var target = new CommentsController(commentRepository, sessionRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CommentsController_ContructorSessionWithNullDependency_Fail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository();
            var target = new CommentsController(commentRepository, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CommentsController_ContructorCommentWithNullDependency_Fail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository();
            var target = new CommentsController(null, sessionRepository);
        }

        [TestMethod]
        public void GetAllComments_GetEmptyResults_NotFail_Test()
        {
            int expectedSessionId = 10;
            bool called = false;
            var expected = new List<Comment>();

            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository()
            {
                GetAllInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                    return expected;
                }
            };

            var target = new CommentsController(commentRepository, sessionRepository);

            IEnumerable<Comment> actual = target.Get(expectedSessionId);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }

        [TestMethod]
        public void GetAllComments_GetResults_NotFail_Test()
        {
            int expectedSessionId = 10;
            bool called = false;
            var expected = new List<Comment>() { new Comment() };

            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository()
            {
                GetAllInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                    return expected;
                }
            };

            var target = new CommentsController(commentRepository, sessionRepository);

            IEnumerable<Comment> actual = target.Get(expectedSessionId);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }

        [TestMethod]
        public void PostComment_NotFail_Test()
        {
            bool called = false;
            var expectedcomment = new Comment() { CommentId = 1 };

            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository()
            {
                AddComment = comment =>
                {
                    Assert.AreEqual(expectedcomment.CommentId, comment.CommentId);
                    called = true;
                    return expectedcomment.CommentId;
                }
            };

            using (ShimsContext.Create())
            {
                var target = new CommentsController(commentRepository, sessionRepository);
                var actual = target.Post(expectedcomment);

                Assert.IsTrue(called);
                Assert.AreEqual(expectedcomment.CommentId, actual);
            }
        }

       
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostComment_ArgumentNullException_Test()
        {
            ICommentRepository commentRepository = new StubICommentRepository();
            IMaterialRepository materialRepository = new StubIMaterialRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository();

            var target = new CommentsController(commentRepository, sessionRepository);

            target.Post(null);
        }

        [TestMethod]
        public void DeleteComment_Deleted_NotFail_Test()
        {
            bool called = false;
            var expectedcomment = new Comment() { CommentId = 1, SessionId = 10 };
            int organizerId = 10;

            ISessionRepository sessionRepository = new StubISessionRepository();
            ICommentRepository commentRepository = new StubICommentRepository()
            {
                DeleteInt32 = commentId =>
                {
                    Assert.AreEqual(expectedcomment.CommentId, commentId);
                    called = true;
                },
                GetOrganizerIdInt32 = commentId =>
                {
                    return organizerId;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return organizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new CommentsController(commentRepository, sessionRepository);

                target.Delete(expectedcomment.CommentId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteComment_UnauthorizedException_Test()
        {
            var expectedcomment = new Comment() { CommentId = 1, SessionId = 10 };
            int organizerId = 10;

            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetOrganizerIdInt32 = (sessionId) =>
                {
                    return organizerId;
                }
            };

            ICommentRepository commentRepository = new StubICommentRepository()
            {
                GetInt32 = commentId =>
                {
                    return expectedcomment;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 1000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new CommentsController(commentRepository, sessionRepository);

                target.Delete(expectedcomment.CommentId);
            }
        }
    }

}
