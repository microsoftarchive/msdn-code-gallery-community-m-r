using System.Web.Http;
using System.Web.Http.Tracing;
using MyEvents.Api.TraceWriters;

namespace MyEvents.Api.App_Start
{
    /// <summary>
    /// Add Trace Listener
    /// Note that you can only set a single trace writer
    /// By default, Web API adds a "no-op" tracer that does nothing. 
    /// Call  HttpConfiguration.Services.Replace to replace this default tracer with your own
    /// </summary>
    public class TraceConfig
    {
        /// <summary>
        /// Register trace listener
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void RegisterListener(HttpConfiguration config)
        {
            config.Services.Replace(typeof(ITraceWriter), new MyEventsTracer());
        }
    }
}