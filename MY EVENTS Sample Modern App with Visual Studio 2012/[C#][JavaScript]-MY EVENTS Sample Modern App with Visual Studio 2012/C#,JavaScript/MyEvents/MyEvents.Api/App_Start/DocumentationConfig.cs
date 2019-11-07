using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using MyEvents.Api.Documentation;

namespace MyEvents.Api.App_Start
{
    /// <summary>
    /// Configure WebAPI documentation
    /// </summary>
    public class DocumentationConfig
    {
        /// <summary>
        /// Configure WebAPI documentation
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void Configure(HttpConfiguration config)
        {
            // Notice that XmlCommentDocumentationProvider needs to know the path of your XML documentation file.
            config.Services.Replace(typeof(IDocumentationProvider),
                 new XmlCommentDocumentationProvider(HttpContext.Current.Server.MapPath("~/bin/MyEvents.Api.xml")));
        }
    }
}