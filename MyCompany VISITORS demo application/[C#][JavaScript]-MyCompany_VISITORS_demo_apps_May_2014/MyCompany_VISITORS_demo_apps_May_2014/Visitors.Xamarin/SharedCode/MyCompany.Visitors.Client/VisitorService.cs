namespace MyCompany.Visitors.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Visitors.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Visitors.Client.IVisitorService"/>
    /// </summary>
    internal class VisitorService : BaseRequest, IVisitorService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public VisitorService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitorId"></param>
        /// <param name="pictureType"></param>
        /// <returns></returns>
        public async Task<Visitor> Get(int visitorId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitors/{1}/{2}", _urlPrefix, visitorId, (int)pictureType);

            return await base.GetAsync<Visitor>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pictureType"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public async Task<IList<Visitor>> GetVisitors(string filter, PictureType pictureType, int pageSize, int pageCount)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitors?filter={1}&pictureType={2}&pageSize={3}&pageCount={4}", _urlPrefix, filter, (int)pictureType, pageSize, pageCount);

            return await base.GetAsync<IList<Visitor>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> GetCount(string filter)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitors/count?filter={1}", _urlPrefix, filter);

            return await base.GetAsync<int>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public async Task<int> Add(Visitor visitor)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitors", _urlPrefix);

            return await base.PostAsync<int, Visitor>(url, visitor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public async Task Update(Visitor visitor)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitors", _urlPrefix);

            await base.PutAsync<Visitor>(url, visitor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        public async Task Delete(int visitorId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitors/{1}", _urlPrefix, visitorId);

            await base.DeleteAsync(url);
        }
    }
}
