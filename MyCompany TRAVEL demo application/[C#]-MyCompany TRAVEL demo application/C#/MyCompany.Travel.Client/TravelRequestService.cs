
namespace MyCompany.Travel.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Travel.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
    /// </summary>
    internal class TravelRequestService : BaseRequest, ITravelRequestService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public TravelRequestService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<TravelRequest> Get(int travelRequestId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/{1}/{2}", _urlPrefix, travelRequestId, (int)pictureType);

            return await base.GetAsync<TravelRequest>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<IList<TravelRequest>> GetUserTravelRequests(string filter, int status, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/user?filter={1}&status={2}&pageSize={3}&pageCount={4}", _urlPrefix, filter, status, pageSize, pageCount);

            return await base.GetAsync<IList<TravelRequest>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<IList<TravelRequest>> GetNotFinishedUserTravelRequests(string filter, int status)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/unfinished/user?filter={1}&status={2}", _urlPrefix, filter, status);

            return await base.GetAsync<IList<TravelRequest>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<IList<TravelRequest>> GetTeamTravelRequests(string filter, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/team?filter={1}&status={2}&pageSize={3}&pageCount={4}&pictureType={5}", _urlPrefix, filter, status, pageSize, pageCount, (int)pictureType);

            return await base.GetAsync<IList<TravelRequest>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<IList<TravelRequest>> GetNotFinishedTeamTravelRequests(string filter, int status, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/unfinished/team?filter={1}&status={2}&pictureType={3}", _urlPrefix, filter, status, (int)pictureType);

            return await base.GetAsync<IList<TravelRequest>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<IList<TravelRequest>> GetAllTravelRequests(string filter, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/all?filter={1}&status={2}&pageSize={3}&pageCount={4}&pictureType={5}", _urlPrefix, filter, status, pageSize, pageCount, (int)pictureType);

            return await base.GetAsync<IList<TravelRequest>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<IList<TravelDistribution>> GetTeamTravelDistribution()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/team/distribution/{1}", _urlPrefix, 5);

            return await base.GetAsync<IList<TravelDistribution>>(url);
        }

        

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<int> GetUserCount(string filter, int status)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/user/count?filter={1}&status={2}", _urlPrefix, filter, status);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<int> GetTeamCount(string filter, int status)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/team/count?filter={1}&status={2}", _urlPrefix, filter, status);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<int> GetAllCount(string filter, int status)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/all/count?filter={1}&status={2}", _urlPrefix, filter, status);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="travelRequest"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task<int> Add(TravelRequest travelRequest)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests", _urlPrefix);

            return await base.PostAsync<int, TravelRequest>(url, travelRequest);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="travelRequest"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task Update(TravelRequest travelRequest)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests", _urlPrefix);

            await base.PutAsync<TravelRequest>(url, travelRequest);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <param name="comments"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task UpdateStatus(int travelRequestId, TravelRequestStatus status, string comments)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/update?travelRequestId={1}&status={2}&comments={3}", _urlPrefix, travelRequestId, (int)status, comments);

            await base.GetAsync(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelRequestService"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Client.ITravelRequestService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelRequestService"/></returns>
        public async Task Delete(int travelRequestId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelrequests/{1}", _urlPrefix, travelRequestId);

            await base.DeleteAsync(url);
        }
    }
}
