using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// Material Controller
    /// </summary>
    public class MaterialsController : ApiController
    {
        private readonly IMaterialRepository _materialRepository = null;
        private readonly ISessionRepository _sessionRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="materialRepository">IMaterialRepository dependency</param>
        /// <param name="sessionRepository">ISessionRepository dependency</param>
        public MaterialsController(IMaterialRepository materialRepository , ISessionRepository sessionRepository)
        {
            if (sessionRepository == null)
                throw new ArgumentNullException("sessionRepository");

            if (materialRepository == null)
                throw new ArgumentNullException("materialRepository");

            _materialRepository = materialRepository;
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Get All Materials
        /// </summary>
        /// <param name="sessionId">sessionId</param>
        /// <returns>List of Materials</returns>
        public IList<Material> GetAllMaterials(int sessionId)
        {
            return _materialRepository.GetAll(sessionId);
        }

        /// <summary>
        /// Get Material
        /// </summary>
        /// <param name="id">materialId</param>
        /// <returns>Material</returns>
        public Material Get(int id)
        {
            return _materialRepository.Get(id);
        }

        /// <summary>
        /// Add new material
        /// </summary>
        /// <param name="material">material information</param>
        /// <returns>materialId</returns>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public int Post(Material material)
        {
            if (material == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            ValidateSessionAuthorization(material.SessionId);

            return _materialRepository.Add(material);
        }

        /// <summary>
        /// Delete Material
        /// </summary>
        /// <param name="id">Material</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void Delete(int id)
        {
            ValidateMaterialAuthorization(id);

            _materialRepository.Delete(id);
        }

        private void ValidateSessionAuthorization(int sessionId)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            int organizerId = _sessionRepository.GetOrganizerId(sessionId);
            if (token.RegisteredUserId != organizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }

        private void ValidateMaterialAuthorization(int materialId)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            int organizerId = _materialRepository.GetOrganizerId(materialId);
            if (token.RegisteredUserId != organizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }
    }

}
