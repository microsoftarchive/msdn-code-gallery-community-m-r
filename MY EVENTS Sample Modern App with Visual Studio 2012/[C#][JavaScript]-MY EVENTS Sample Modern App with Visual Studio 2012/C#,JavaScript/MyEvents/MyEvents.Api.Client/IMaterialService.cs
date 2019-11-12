using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to access to the Material Controller exposed by MyEvents.API
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// Get All Materials
        /// </summary>
        /// <param name="sessionId">sessionId</param>
        /// <param name="callback">CallBack func to get all materials</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllMaterialsAsync(int sessionId, Action<IList<Material>> callback);

        /// <summary>
        /// Get Material By Id
        /// </summary>
        /// <param name="materialId">materialId</param>
        /// <param name="callback">CallBack func to get the material</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetMaterialAsync(int materialId, Action<Material> callback);

        /// <summary>
        /// Add new material
        /// </summary>
        /// <param name="material">material information</param>
        /// <param name="callback">CallBack func to get new material Id</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddMaterialAsync(Material material, Action<int> callback);

        /// <summary>
        /// Delete Material
        /// </summary>
        /// <param name="materialId">Material</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteMaterialAsync(int materialId, Action<HttpStatusCode> callback);
    }
}
