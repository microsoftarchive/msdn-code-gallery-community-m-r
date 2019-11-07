using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// <see cref="MyEvents.Api.Client.ISessionService"/>
    /// </summary>
    internal class SessionService : BaseRequest, ISessionService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public SessionService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {

        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ISessionService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ISessionService"/></returns>
        public IAsyncResult GetAllSessionsAsync(int eventDefinitionId, Action<IList<Session>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/sessions?eventDefinitionId={1}", _urlPrefix, eventDefinitionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ISessionService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ISessionService"/></returns>
        public IAsyncResult GetSessionAsync(int sessionId, Action<Session> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/sessions/{1}", _urlPrefix, sessionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ISessionService"/>
        /// </summary>
        /// <param name="session"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ISessionService"/></returns>
        public IAsyncResult AddSessionAsync(Session session, Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/sessions", _urlPrefix);

            return base.DoPost(url, session, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ISessionService"/>
        /// </summary>
        /// <param name="session"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ISessionService"/></returns>
        public IAsyncResult UpdateSessionAsync(Session session, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/sessions", _urlPrefix);

            return base.DoPut(url, session, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ISessionService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ISessionService"/></returns>
        public IAsyncResult DeleteSessionAsync(int sessionId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/sessions/{1}", _urlPrefix, sessionId);

            return base.DoDelete(url, sessionId, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.ISessionService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="startTime"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="duration"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.ISessionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.ISessionService"/></returns>
        public IAsyncResult UpdateSessionPeriodAsync(int sessionId, string startTime, int duration, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/sessions?sessionId={1}&startTime={2}&duration={3}", _urlPrefix, sessionId, startTime, duration);

            return base.DoPut(url, callback);
        }

    }
}
