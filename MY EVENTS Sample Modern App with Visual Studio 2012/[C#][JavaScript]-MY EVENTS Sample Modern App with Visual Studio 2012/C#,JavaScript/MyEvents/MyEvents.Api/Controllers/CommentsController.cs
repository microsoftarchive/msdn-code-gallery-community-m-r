using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// Comments Controller
    /// </summary>
    public class CommentsController : ApiController
    {
        private readonly ICommentRepository _commentRepository = null;
        private readonly ISessionRepository _sessionRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commentRepository">ICommentRepository dependency</param>
        /// <param name="sessionRepository">ISessionRepository dependency</param>
        public CommentsController(ICommentRepository commentRepository, ISessionRepository sessionRepository)
        {
            if (sessionRepository == null)
                throw new ArgumentNullException("sessionRepository");

            if (commentRepository == null)
                throw new ArgumentNullException("commentRepository");

            _commentRepository = commentRepository;
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Get All Comments
        /// </summary>
        /// <param name="id">sessionId</param>
        /// <returns>List of Comments</returns>
        public IList<Comment> Get(int id)
        {
            return _commentRepository.GetAll(id);
        }

        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="comment">Comment information</param>
        /// <returns>CommentId</returns>
        public int Post(Comment comment)
        {
            if (comment == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            return _commentRepository.Add(comment);
        }

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="id">Comment id to delete</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void Delete(int id)
        {
            ValidateCommentAuthorization(id);

            _commentRepository.Delete(id);
        }

        private void ValidateSessionAuthorization(int sessionId)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            int organizerId = _sessionRepository.GetOrganizerId(sessionId);
            if (token.RegisteredUserId != organizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }

        private void ValidateCommentAuthorization(int commentId)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            int organizerId = _commentRepository.GetOrganizerId(commentId);
            if (token.RegisteredUserId != organizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }

    }
}
