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
    /// <see cref="MyEvents.Api.Client.IMaterialService"/>
    /// </summary>
    internal class MaterialService : BaseRequest, IMaterialService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public MaterialService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {

        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IMaterialService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IMaterialService"/></returns>
        public IAsyncResult GetAllMaterialsAsync(int sessionId, Action<IList<Material>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/materials?sessionId={1}", _urlPrefix, sessionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IMaterialService"/>
        /// </summary>
        /// <param name="materialId"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IMaterialService"/></returns>
        public IAsyncResult GetMaterialAsync(int materialId, Action<Material> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/materials/{1}", _urlPrefix, materialId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IMaterialService"/>
        /// </summary>
        /// <param name="material"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IMaterialService"/></returns>
        public IAsyncResult AddMaterialAsync(Material material, Action<int> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/materials", _urlPrefix);

            return base.DoPost(url, material, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IMaterialService"/>
        /// </summary>
        /// <param name="materialId"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IMaterialService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IMaterialService"/></returns>
        public IAsyncResult DeleteMaterialAsync(int materialId, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/materials/{1}", _urlPrefix, materialId);

            return base.DoDelete(url, materialId, callback);
        }

    }
}
