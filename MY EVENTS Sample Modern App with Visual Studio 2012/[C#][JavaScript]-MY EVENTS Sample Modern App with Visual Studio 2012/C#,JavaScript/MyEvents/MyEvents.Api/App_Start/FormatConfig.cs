using System.Web.Http;
using MyEvents.Api.Formatters;

namespace MyEvents.Api.App_Start
{
    /// <summary>
    /// Configure 
    /// </summary>
    public class FormatConfig
    {
        /// <summary>
        /// The following code restricts the web API responses to JSON formatter
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void ConfigureFormats(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;

            // Preserve object references in JSON
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Write indented JSON
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            config.Formatters.Insert(0, new JsonpMediaTypeFormatter());

        }
    }
}