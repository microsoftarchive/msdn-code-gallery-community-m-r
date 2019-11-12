using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{

    /// <summary>
    /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
    /// </summary>
    internal class EventDefinitionService : BaseRequest, IEventDefinitionService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public EventDefinitionService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {

        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult GetEventDefinitionCountAsync(Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/eventdefinitions/GetEventDefinitionCount", _urlPrefix);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="pageSize"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="pageIndex"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult GetAllEventDefinitionsAsync(int pageSize, int pageIndex, Action<IList<EventDefinition>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/eventdefinitions?pageSize={1}&pageIndex={2}", _urlPrefix, pageSize, pageIndex);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="number"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult GetLastEventDefinitionsAsync(int number, Action<IList<EventDefinition>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/eventdefinitions?number={1}", _urlPrefix, number);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// Get the event that happens today ordered by name
        /// </summary>
        /// <param name="callback">CallBack func to get list of event definitions</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult GetCurrentEventDefinitionsAsync(Action<IList<EventDefinition>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions/GetCurrent", _urlPrefix);

            return base.DoGet(url, callback);
        }

       
        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="filter"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult GetEventDefinitionCountByOrganizerIdAsync(int organizerId, string filter, Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions?organizerId={1}&filter={2}"
                    , _urlPrefix, organizerId, filter);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="filter"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="pageSize"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="pageIndex"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult GetEventDefinitionByOrganizerIdAsync(int organizerId, string filter, int pageSize, int pageIndex, Action<IList<EventDefinition>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions?organizerId={1}&filter={2}&pageSize={3}&pageIndex={4}"
                    , _urlPrefix, organizerId, filter, pageSize, pageIndex);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult GetEventDefinitionByIdAsync(int eventDefinitionId, Action<EventDefinition> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions/{1}", _urlPrefix, eventDefinitionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="eventDefinition"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult AddEventDefinitionAsync(EventDefinition eventDefinition, Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions", _urlPrefix);

            return base.DoPost(url, eventDefinition, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="eventDefinition"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult UpdateEventDefinitionAsync(EventDefinition eventDefinition, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions", _urlPrefix);

            return base.DoPut(url, eventDefinition, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IEventDefinitionService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IEventDefinitionService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IEventDefinitionService"/></returns>
        public IAsyncResult DeleteEventDefinitionAsync(int eventDefinitionId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/eventdefinitions/{1}", _urlPrefix, eventDefinitionId);

            return base.DoDelete(url, eventDefinitionId, callback);
        }

    }
}
