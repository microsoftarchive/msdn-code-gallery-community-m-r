using System.Web.Optimization;

namespace MyEvents.Web
{
    /// <summary>
    /// Transform to process the less files
    /// </summary>
    public class JsNoTransform: IBundleTransform
    {
        /// <summary>
        /// Processes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="response">The response.</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = "text/javascript";
        }
    }
}