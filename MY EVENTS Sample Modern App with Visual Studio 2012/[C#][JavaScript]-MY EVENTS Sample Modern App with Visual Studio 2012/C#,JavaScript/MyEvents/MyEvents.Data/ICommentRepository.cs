using System.Collections.Generic;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// Repository to access to Comment entities
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Get comment by Id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Comment Get(int commentId);

        /// <summary>
        /// Get All Comments
        /// </summary>
        /// <param name="sessionId">sessionId</param>
        /// <returns>List of Comments</returns>
        IList<Comment> GetAll(int sessionId);

        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="comment">comment information</param>
        /// <returns>commentId</returns>
        int Add(Comment comment);

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="commentId">Comment to delete</param>
        void Delete(int commentId);

        /// <summary>
        /// Get the organizerId of the event
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        int GetOrganizerId(int commentId);

    }
}
