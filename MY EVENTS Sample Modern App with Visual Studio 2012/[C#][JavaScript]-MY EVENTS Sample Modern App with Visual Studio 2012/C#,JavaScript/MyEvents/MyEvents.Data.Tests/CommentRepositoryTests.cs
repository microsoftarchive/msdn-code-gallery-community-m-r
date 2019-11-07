using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data.Test
{
    [TestClass]
    public class CommentRepositoryTests
    {
        [TestMethod]
        public void GetComment_Call_GetResults_Test()
        {
            var context = new MyEventsContext();
            int sessionId = context.Comments.FirstOrDefault().SessionId;
            int expectedCount = context.Comments.Count(q => q.SessionId == sessionId);

            ICommentRepository target = new CommentRepository();

            IEnumerable<Comment> results = target.GetAll(sessionId);

            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count());
        }

        [TestMethod]
        public void AddComment_Added_NotFail_Test()
        {
            var context = new MyEventsContext();
            int sessionId = context.Sessions.FirstOrDefault().SessionId;
            int registeredUserId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;
            int expected = context.Comments.Count() + 1;

            ICommentRepository target = new CommentRepository();
            Comment comment = new Comment();
            comment.SessionId = sessionId;
            comment.AddedDateTime = DateTime.UtcNow;
            comment.RegisteredUserId = registeredUserId;
            comment.Text = "sample comment";
            target.Add(comment);

            int actual = context.Comments.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteComment_Deleted_NotFail_Test()
        {
            var context = new MyEventsContext();
            var comment = context.Comments.FirstOrDefault();
            int expected = context.Comments.Count() - 1;

            ICommentRepository target = new CommentRepository();
            target.Delete(comment.CommentId);

            int actual = context.Comments.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteComment_NoExists_NotFail_Test()
        {
            var context = new MyEventsContext();
            var comment = context.Comments.FirstOrDefault();
            int expected = context.Comments.Count();

            ICommentRepository target = new CommentRepository();
            target.Delete(0);

            int actual = context.Comments.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCommentOrganizerId_Call_GetResult_Test()
        {
            var context = new MyEventsContext();
            var comment = context.Comments.Include("Session.EventDefinition").FirstOrDefault();

            ICommentRepository target = new CommentRepository();

            int organizerId = target.GetOrganizerId(comment.CommentId);

            Assert.AreEqual(organizerId, comment.Session.EventDefinition.OrganizerId);
        }

    }
}
