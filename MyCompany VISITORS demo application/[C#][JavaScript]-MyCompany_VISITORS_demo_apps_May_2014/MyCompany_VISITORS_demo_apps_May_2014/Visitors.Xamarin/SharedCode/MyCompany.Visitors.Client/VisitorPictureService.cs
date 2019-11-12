namespace MyCompany.Visitors.Client
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Visitors.Client.Web;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="MyCompany.Visitors.Client.IVisitorPictureService"/>
    /// </summary>
    internal class VisitorPictureService : BaseRequest, IVisitorPictureService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public VisitorPictureService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitorPictureService"/>
        /// </summary>
        /// <param name="visitorPicture"><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></returns>
        public async Task<int> Add(VisitorPicture visitorPicture)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitorpictures", _urlPrefix);

            return await base.PostAsync<int, VisitorPicture>(url, visitorPicture);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitorPictureService"/>
        /// </summary>
        /// <param name="visitorPicture"><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></returns>
        public async Task Update(VisitorPicture visitorPicture)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitorpictures", _urlPrefix);

            await base.PutAsync<VisitorPicture>(url, visitorPicture);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitorPictureService"/>
        /// </summary>
        /// <param name="visitorPictures"><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></returns>
        public async Task AddOrUpdatePictures(ICollection<VisitorPicture> visitorPictures)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitorpictures/addOrUpdatePictures", _urlPrefix);

            await base.PostAsync<ICollection<VisitorPicture>>(url, visitorPictures);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitorPictureService"/>
        /// </summary>
        /// <param name="visitorPictureId"><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></returns>
        public async Task Delete(int visitorPictureId)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitorpictures/{1}", _urlPrefix, visitorPictureId);

            await base.DeleteAsync(url);
        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.IVisitorPictureService"/>
        /// </summary>
        /// <param name="visitorId"><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></param>
        /// <returns><see cref="MyCompany.Visitors.Client.IVisitorPictureService"/></returns>
        public async Task<byte[]> GetByVisitor(int visitorId, PictureType pictureType)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/visitorpictures/{1}/{2}", _urlPrefix, visitorId, (int)pictureType);

            return await base.GetAsync<byte[]>(url);
        }
    }
}
