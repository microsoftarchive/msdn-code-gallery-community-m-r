using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
    /// </summary>
    internal class RegisteredUserService : BaseRequest, IRegisteredUserService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public RegisteredUserService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="facebookId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult GetRegisteredUserAsync(string facebookId, Action<RegisteredUser> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
               , "{0}api/registeredusers?facebookId={1}", _urlPrefix, facebookId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult GetAllRegisteredUsersByEventIdAsync(int eventDefinitionId, Action<IList<RegisteredUser>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/registeredusers?eventDefinitionId={1}", _urlPrefix, eventDefinitionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult GetAllRegisteredUsersBySessionIdAsync(int sessionId, Action<IList<RegisteredUser>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/registeredusers?sessionId={1}", _urlPrefix, sessionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult GetRegisteredUserEventDefinitionsAsync(int registeredUserId, Action<IList<EventDefinition>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/registeredusers?registeredUserId={1}", _urlPrefix, registeredUserId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUser"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult AddRegisteredUserCreateIfNotExistsAsync(RegisteredUser registeredUser, Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/registeredusers", _urlPrefix);

            return base.DoPost(url, registeredUser, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult AddRegisteredUserToEventAsync(int registeredUserId, int eventDefinitionId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/registeredusers/PostRegisteredUserToEvent?RegisteredUserId={1}&eventDefinitionId={2}", _urlPrefix, registeredUserId, eventDefinitionId);

            return base.DoPost(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult DeleteRegisteredUserFromEventAsync(int registeredUserId, int sessionId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/registeredusers/DeleteRegisteredUserFromEvent?RegisteredUserId={1}&eventDefinitionId={2}", _urlPrefix, registeredUserId, sessionId);

            return base.DoDelete(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult AddRegisteredUserToSessionAsync(int registeredUserId, int sessionId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/registeredusers/PostRegisteredUserToSession?RegisteredUserId={1}&sessionId={2}", _urlPrefix, registeredUserId, sessionId);

            return base.DoPost(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="score"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult AddRegisteredUserScoreAsync(int registeredUserId, int sessionId, double score, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/registeredusers/PostRegisteredUserScore?RegisteredUserId={1}&sessionId={2}&score={3}", _urlPrefix, registeredUserId, sessionId, score);

            return base.DoPost(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult DeleteRegisteredUserFromSessionAsync(int registeredUserId, int sessionId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/registeredusers/DeleteRegisteredUserFromSession?RegisteredUserId={1}&sessionId={2}", _urlPrefix, registeredUserId, sessionId);

            return base.DoDelete(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRegisteredUserService"/>
        /// </summary>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRegisteredUserService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRegisteredUserService"/></returns>
        public IAsyncResult GetFakeRegisteredUserAsync(Action<RegisteredUser> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/registeredusers/GetFakeRegisteredUser", _urlPrefix);

            return base.DoGet(url, callback);
        }

    }
}
