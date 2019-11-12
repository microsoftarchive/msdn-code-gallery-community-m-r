using System;
using System.Net.Http;
using System.Web.Http.Tracing;

namespace MyEvents.Api.TraceWriters
{
    /// <summary>
    /// ASP.NET Web API is designed so that you can use your choice of tracing/logging library, whether that is ETW, NLog, log4net, or simply  System.Diagnostics.Trace
    /// You can use this feature to trace what the Web API framework does before and after it invokes your controller. ç
    /// You can also use it to trace your own code
    /// </summary>
    public class MyEventsTracer : ITraceWriter
    {
        /// <summary>
        /// The ITraceWriter.Trace method creates a trace
        /// The caller specifies a category and trace level. The category can be any user-defined string
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <param name="traceAction"></param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            WriteTrace(rec);
        }

        /// <summary>
        /// This method will be removed from the ITraceWriter interface.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(string category, TraceLevel level)
        {
            return true;
        }

        /// <summary>
        /// Write trace
        /// </summary>
        /// <param name="rec"></param>
        protected void WriteTrace(TraceRecord rec)
        {
            var message = string.Format("{0};{1};{2}",
                rec.Operator, rec.Operation, rec.Message);
            System.Diagnostics.Trace.WriteLine(message, rec.Category);
        }
    }
}