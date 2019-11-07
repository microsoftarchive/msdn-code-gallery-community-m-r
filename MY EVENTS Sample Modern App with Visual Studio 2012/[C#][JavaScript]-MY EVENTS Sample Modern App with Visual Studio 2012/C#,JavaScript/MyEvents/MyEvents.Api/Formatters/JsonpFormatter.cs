using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace MyEvents.Api.Formatters
{
    /// <summary>
    /// Formatter for jsonp.
    /// </summary>
    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private string callbackQueryParameter;

        /// <summary>
        /// JsonpMediaTypeFormatter constructor.
        /// </summary>
        public JsonpMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(DefaultMediaType);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", DefaultMediaType));
        }

        /// <summary>
        /// The callback query parameter.
        /// </summary>
        public string CallbackQueryParameter
        {
            get { return callbackQueryParameter ?? "callback"; }
            set { callbackQueryParameter = value; }
        }

        /// <summary>
        /// Writes to stream async.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        /// <param name="contentHeaders"></param>
        /// <param name="transportContext"></param>
        /// <returns></returns>
        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
        {
            string callback;

            if (IsJsonpRequest(out callback))
            {
                return Task.Factory.StartNew(() =>
                {
                    var writer = new StreamWriter(stream);
                    writer.Write(callback + "(");
                    writer.Flush();

                    base.WriteToStreamAsync(type, value, stream, contentHeaders, transportContext).Wait();

                    writer.Write(")");
                    writer.Flush();
                });
            }
            else
            {
                return base.WriteToStreamAsync(type, value, stream, contentHeaders, transportContext);
            }
        }


        private bool IsJsonpRequest(out string callback)
        {
            callback = null;

            if (HttpContext.Current.Request.HttpMethod != "GET")
                return false;

            callback = HttpContext.Current.Request.QueryString[CallbackQueryParameter];

            return !string.IsNullOrEmpty(callback);
        }
    }
}