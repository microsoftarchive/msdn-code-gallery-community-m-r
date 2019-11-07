using System;
using System.Collections.Generic;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// <see cref="MyEvents.Data.ICommentRepository"/>
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        /// <summary> 
        /// <see cref="MyEvents.Data.ICommentRepository"/>
        /// </summary>
        /// <param name="commentId"><see cref="MyEvents.Data.ICommentRepository"/></param>
        /// <returns></returns>
        public Comment Get(int commentId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Comments.Single(q => q.CommentId == commentId);
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ICommentRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.ICommentRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ICommentRepository"/></returns>
        public IList<Comment> GetAll(int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Comments.Include("registeredUser")
                    .Where(q => q.SessionId == sessionId)
                    .OrderByDescending(c => c.AddedDateTime)
                    .ToList()
                    .Select(c => new Comment()
                    {
                        CommentId = c.CommentId,
                        SessionId = c.SessionId,
                        Text = c.Text,
                        AddedDateTime = c.AddedDateTime,
                        RegisteredUserId = c.RegisteredUserId,
                        RegisteredUser = new RegisteredUser()
                        {
                            RegisteredUserId = c.RegisteredUserId,
                            Name = c.RegisteredUser.Name,
                        }
                    })
                    .ToList();
                    
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ICommentRepository"/>
        /// </summary>
        /// <param name="comment"><see cref="MyEvents.Data.ICommentRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ICommentRepository"/></returns>
        public int Add(Comment comment)
        {
            using (var context = new MyEventsContext())
            {
                comment.AddedDateTime = DateTime.UtcNow;
                context.Comments.Add(comment);
                context.SaveChanges();
                return comment.CommentId;
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ICommentRepository"/>
        /// </summary>
        /// <param name="commentId"><see cref="MyEvents.Data.ICommentRepository"/></param>
        public void Delete(int commentId)
        {
            using (var context = new MyEventsContext())
            {
                var comment = context.Comments.FirstOrDefault(q => q.CommentId == commentId);
                if (comment != null)
                {
                    context.Comments.Remove(comment);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        ///  <see cref="MyEvents.Data.ICommentRepository"/>
        /// </summary>
        /// <param name="commentId"> <see cref="MyEvents.Data.ICommentRepository"/></param>
        /// <returns> <see cref="MyEvents.Data.ICommentRepository"/></returns>
        public int GetOrganizerId(int commentId)
        {
            int id = 0;
            using (var context = new MyEventsContext())
            {
                var comment = context.Comments.Include("Session.EventDefinition")
                    .FirstOrDefault(q => q.CommentId == commentId);
                if (comment != null)
                {
                    id = comment.Session.EventDefinition.OrganizerId;
                }
            }
            return id;
        }
    }
}
