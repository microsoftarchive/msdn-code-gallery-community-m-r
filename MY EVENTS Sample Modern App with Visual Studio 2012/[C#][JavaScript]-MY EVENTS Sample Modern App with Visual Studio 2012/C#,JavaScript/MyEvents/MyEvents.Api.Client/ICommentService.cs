using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to access to the Comment Controller exposed by MyEvents.API
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Get All Comments
        /// </summary>
        /// <param name="sessionId">sessionId</param>
        /// <param name="callback">CallBack func to get all comments</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllCommentsAsync(int sessionId, Action<IList<Comment>> callback);

        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="comment">Comment information</param>
        /// <param name="callback">CallBack func to new comment Id</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddCommentAsync(Comment comment, Action<int> callback);

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="commentId">Comment</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteCommentAsync(int commentId, Action<HttpStatusCode> callback);

    }
}
