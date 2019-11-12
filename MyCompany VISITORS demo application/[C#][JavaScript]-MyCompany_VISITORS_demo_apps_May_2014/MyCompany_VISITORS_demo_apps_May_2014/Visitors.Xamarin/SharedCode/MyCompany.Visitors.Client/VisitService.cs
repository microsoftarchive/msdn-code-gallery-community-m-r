namespace MyCompany.Visitors.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Visitors.Client.Web;
    using MyCompany.Visitors.Client.DocumentResponse;

    /// <summary>
    /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
    /// </summary>
    internal class VisitService : BaseRequest, IVisitService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public VisitService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="visitId"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<Visit> Get(int visitId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits/{1}/{2}", _urlPrefix, visitId, (int)pictureType);

            return await base.GetAsync<Visit>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="dateFilter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="toDate"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<IList<Visit>> GetVisits(string filter, PictureType pictureType, int pageSize, int pageCount, DateTime? dateFilter, DateTime? toDate)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/visits/company?filter={1}&pictureType={2}&pageSize={3}&pageCount={4}&dateFilter={5}&toDate={6}", _urlPrefix, filter, (int)pictureType, pageSize, pageCount, dateFilter, toDate);

            return await base.GetAsync<IList<Visit>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="dateFilter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<IList<Visit>> GetVisitsFromDate(string filter, PictureType pictureType, int pageSize, int pageCount, DateTime dateFilter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/visits/company/fromdate?filter={1}&pictureType={2}&pageSize={3}&pageCount={4}&dateFilter={5}", _urlPrefix, filter, (int)pictureType, pageSize, pageCount, dateFilter);

            return await base.GetAsync<IList<Visit>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="dateFilter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<IList<Visit>> GetUserVisits(string filter, PictureType pictureType, int pageSize, int pageCount, DateTime dateFilter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits/user?filter={1}&pictureType={2}&pageSize={3}&pageCount={4}&dateFilter={5}", _urlPrefix, filter, (int)pictureType, pageSize, pageCount, dateFilter);

            return await base.GetAsync<IList<Visit>>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="dateFilter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="toDate"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<int> GetCount(string filter, DateTime? dateFilter, DateTime? toDate)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/visits/company/count?filter={1}&dateFilter={2}&toDate={3}", _urlPrefix, filter, dateFilter, toDate);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="dateFilter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<int> GetCountFromDate(string filter, DateTime dateFilter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/visits/company/count/fromdate?filter={1}&dateFilter={2}", _urlPrefix, filter, dateFilter);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="dateFilter"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<int> GetUserCount(string filter, DateTime dateFilter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits/user/count?filter={1}&dateFilter={2}", _urlPrefix, filter, dateFilter);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="visit"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task<int> Add(Visit visit)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits", _urlPrefix);

            return await base.PostAsync<int, Visit>(url, visit);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="visit"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task Update(Visit visit)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits", _urlPrefix);

            await base.PutAsync<Visit>(url, visit);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="status"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <param name="visitId"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task UpdateStatus(int visitId, VisitStatus status)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits/{1}/update/{2}", _urlPrefix, visitId, (int)status);

            await base.PutAsync<VisitStatus>(url, status);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitService"/>
        /// </summary>
        /// <param name="visitId"><see cref="MyCompany.Visitors.Client.IVisitService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitService"/></returns>
        public async Task Delete(int visitId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visits/{1}", _urlPrefix, visitId);

            await base.DeleteAsync(url);
        }
    }
}
