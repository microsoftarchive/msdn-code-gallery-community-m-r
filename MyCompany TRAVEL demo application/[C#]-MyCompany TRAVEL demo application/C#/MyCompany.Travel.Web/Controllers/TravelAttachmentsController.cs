namespace MyCompany.Travel.Web.Controllers
{
    using System;
    using System.Web.Http;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Model;
    using System.Threading.Tasks;
    using System.Web;
    using System.IO;
    using System.Net.Http;
    using System.Net;
    using System.Net.Http.Headers;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Web.Infraestructure.Security;

    /// <summary>
    /// Travel Attachment Controller
    /// </summary>
    [RoutePrefix("api/travelattachments")]
    [MyCompanyAuthorization]
    public class TravelAttachmentsController : ApiController
    {
        private readonly ITravelAttachmentRepository _travelAttachmentRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="travelAttachmentRepository">ITravelAttachmentRepository dependency</param>
        public TravelAttachmentsController(ITravelAttachmentRepository travelAttachmentRepository)
        {
            if (travelAttachmentRepository == null)
                throw new ArgumentNullException("travelAttachmentRepository");

            _travelAttachmentRepository = travelAttachmentRepository;
        }

        /// <summary>
        /// Add new  travel attachment.
        /// </summary>
        /// <param name="travelAttachment"> travel attachment information</param>
        /// <returns>AtachmentId</returns>
        [WebApiOutputCacheAttribute(false, true)]
        [Route("add")]
        [Route("~/noauth/api/travelattachments/add")]
        [HttpPost()]
        public async Task<int> Add(TravelAttachment travelAttachment)
        {
            if (travelAttachment == null)
                throw new ArgumentNullException("travelAttachment");

            return await _travelAttachmentRepository.AddAsync(travelAttachment);
        }

        /// <summary>
        /// Update travel attachment.
        /// </summary>
        /// <param name="travelAttachment"> travel attachment information</param>
        [HttpPut]
        [WebApiOutputCacheAttribute(false, true)]
        public async Task Update(TravelAttachment travelAttachment)
        {
            if (travelAttachment == null)
                throw new ArgumentNullException("travelAttachment");

            await _travelAttachmentRepository.UpdateAsync(travelAttachment);
        }

        /// <summary>
        /// Add new attachment.
        /// </summary>
        /// <param name="travelAttachmentId">travel AttachmentId</param>
        [WebApiOutputCacheAttribute(false, true)]
        [Route("{travelAttachmentId:int:min(1)}")]
        [Route("~/noauth/api/travelattachments/{travelAttachmentId:int:min(1)}")]
        [HttpDelete()]
        public async Task Delete(int travelAttachmentId)
        {
            await _travelAttachmentRepository.DeleteAsync(travelAttachmentId);
        }

        /// <summary>
        /// Gets attachment.
        /// </summary>
        /// <param name="travelAttachmentId">travel AttachmentId</param>
        [WebApiOutputCacheAttribute()]
        [Route("attachment/{travelAttachmentId:int:min(1)}")]
        [Route("~/noauth/api/travelattachments/attachment/{travelAttachmentId:int:min(1)}")]
        public async Task<TravelAttachment> Get(int travelAttachmentId)
        {
            return await _travelAttachmentRepository.GetAsync(travelAttachmentId);
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        /// <exception cref="System.ArgumentNullException">visitor</exception>
        [Route("files")]
        [Route("~/noauth/api/travelattachments/files")]
        [HttpPost]
        [ActionName("UploadFile")]
        public async Task<int> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            int travelRequestId = 0;
            string fileFriendlyName = string.Empty;
            int travelAttachmentId = 0;

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var directory = new DirectoryInfo(root);
            if (!directory.Exists)
                directory.Create();

            var provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);            

            var serializer = new Newtonsoft.Json.JsonSerializer();
            
            foreach (var key in provider.FormData.AllKeys)
            {
                var val = provider.FormData.GetValues(key)[0];

                if (key == "travelRequestId")
                {
                    travelRequestId = (int)serializer.Deserialize(new StringReader(val), typeof(int));
                }

                if (key == "fileFriendlyName")
                {
                    fileFriendlyName = val;
                }
            }

            if (travelRequestId <= 0)
                throw new ArgumentNullException("travelRequestId");

            if (provider.FileData != null && provider.FileData.Count > 0)
            {
                byte[] file = File.ReadAllBytes(provider.FileData[0].LocalFileName);

                TravelAttachment attachment = new TravelAttachment()
                {
                    Name = fileFriendlyName,
                    FileName = Path.GetFileName(UnquoteFileName(provider.FileData[0].Headers.ContentDisposition.FileName)),
                    TravelRequestId = travelRequestId,
                    Content = file
                };

                travelAttachmentId = await Add(attachment);

                File.Delete(provider.FileData[0].LocalFileName);
            }

            return travelAttachmentId;
        }

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="travelAttachmentId">travelAttachmentId</param>
        /// <returns>the file</returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        /// <exception cref="System.ArgumentNullException">visitor</exception>
        [Route("files/{travelAttachmentId:int:min(1)}")]
        [Route("~/noauth/api/travelattachments/files/{travelAttachmentId:int:min(1)}")]
        [HttpGet]
        [HttpPost]
        [ActionName("DownloadFile/{travelAttachmentId:int:min(1)}")]
        public async Task<HttpResponseMessage> DownloadFile(int travelAttachmentId)
        {
            TravelAttachment attachment = await _travelAttachmentRepository.GetAsync(travelAttachmentId);
            if (attachment == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(attachment.Content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = attachment.FileName;

            return result;
        }

        internal string UnquoteFileName(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }
    }
}