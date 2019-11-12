
namespace MyCompany.Travel.Client
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Travel.Client.Web;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="MyCompany.Travel.Client.ITravelAttachmentService"/>
    /// </summary>
    internal class TravelAttachmentService : BaseRequest, ITravelAttachmentService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public TravelAttachmentService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelAttachmentService"/>
        /// </summary>
        /// <param name="travelAttachment"><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></returns>
        public async Task<int> Add(TravelAttachment travelAttachment)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelattachments/add", _urlPrefix);

            return await base.PostAsync<int, TravelAttachment>(url, travelAttachment);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelAttachmentService"/>
        /// </summary>
        /// <param name="travelAttachment"><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></returns>
        public async Task Update(TravelAttachment travelAttachment)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelattachments", _urlPrefix);

            await base.PutAsync<TravelAttachment>(url, travelAttachment);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelAttachmentService"/>
        /// </summary>
        /// <param name="travelAttachmentId"><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></returns>
        public async Task Delete(int travelAttachmentId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelattachments/{1}", _urlPrefix, travelAttachmentId);

            await base.DeleteAsync(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Client.ITravelAttachmentService"/>
        /// </summary>
        /// <param name="travelAttachmentId"><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></param>
        /// <returns><see cref="MyCompany.Travel.Client.ITravelAttachmentService"/></returns>
        public async Task<TravelAttachment> Get(int travelAttachmentId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/travelattachments/attachment/{1}", _urlPrefix, travelAttachmentId);

            return await base.GetAsync<TravelAttachment>(url);
        }
    }
}
