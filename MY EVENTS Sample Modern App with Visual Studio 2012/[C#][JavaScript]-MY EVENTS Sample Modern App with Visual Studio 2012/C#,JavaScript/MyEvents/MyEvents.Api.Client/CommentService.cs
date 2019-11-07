using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// <see cref="MyEvents.Api.Client.ICommentService"/>
    /// </summary>
    internal class CommentService : BaseRequest, ICommentService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public CommentService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {

        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ICommentService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.ICommentService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ICommentService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ICommentService"/></returns>
        public IAsyncResult GetAllCommentsAsync(int sessionId, Action<IList<Comment>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/comments/{1}", _urlPrefix, sessionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ICommentService"/>
        /// </summary>
        /// <param name="comment"><see cref="MyEvents.Api.Client.ICommentService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ICommentService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ICommentService"/></returns>
        public IAsyncResult AddCommentAsync(Comment comment, Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/comments", _urlPrefix);

            return base.DoPost(url, comment, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ICommentService"/>
        /// </summary>
        /// <param name="commentId"><see cref="MyEvents.Api.Client.ICommentService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ICommentService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ICommentService"/></returns>
        public IAsyncResult DeleteCommentAsync(int commentId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/comments/{1}", _urlPrefix, commentId);

            return base.DoDelete(url, commentId, callback);
        }
    }
}
