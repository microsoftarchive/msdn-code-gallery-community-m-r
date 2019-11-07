
namespace MyCompany.Travel.CrossCutting
{
    using System;
    using System.Diagnostics;
    using System.Security;
    using System.Security.Permissions;

    /// <summary>
    /// Trace helper for application's logging
    /// </summary>
    public static class TraceManager
    {
        #region Members

        static readonly TraceSource source = new TraceSource("VideoPlatform");

        static object syncRoot = new object();

        #endregion

        #region Private Methods

        /// <summary>
        /// Trace internal message in configured listeners
        /// </summary>
        /// <param name="eventType">Event type to trace</param>
        /// <param name="message">Message of event</param>
        static void TraceInternal(TraceEventType eventType, params string[] message)
        {
            if (source != null)
            {
                try
                {
                    lock (syncRoot)
                    {
                        source.TraceEvent(eventType, (int)eventType, Concat(message));
                    }
                }
                catch (SecurityException)
                {
                    //Cannot access to file listener or cannot have
                    //privileges to write in event log
                    //do not propagete this :-(
                }
            }
        }

        static string Concat(params string[] strings)
        {

            string result = string.Empty;

            for (int i = 0; i < strings.Length; i++)
            {

                if (i > 0)

                    result += " ";

                result += strings[i];

            }

            return result;

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts logical operation in trace repository
        /// </summary>
        /// <param name="operationName"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2135:SecurityRuleSetLevel2MethodsShouldNotBeProtectedWithLinkDemandsFxCopRule"),
        SecurityPermission(SecurityAction.LinkDemand)]
        static public void TraceStartLogicalOperation(string operationName)
        {
            if (String.IsNullOrEmpty(operationName))
                throw new ArgumentNullException("operationName","Trace message is null or empty string");

            System.Diagnostics.Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            System.Diagnostics.Trace.CorrelationManager.StartLogicalOperation(operationName);
        }

        /// <summary>
        /// Stops actual logical operation in trace repository
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2135:SecurityRuleSetLevel2MethodsShouldNotBeProtectedWithLinkDemandsFxCopRule"),
        SecurityPermission(SecurityAction.LinkDemand)]
        static public void TraceStopLogicalOperation()
        {
            try
            {
                System.Diagnostics.Trace.CorrelationManager.StopLogicalOperation();
            }
            catch (InvalidOperationException)
            {
                //stack empty
            }
        }

        /// <summary>
        /// Trace information message to trace repository
        /// </summary>
        /// <param name="message">Information message to trace</param>
        public static void TraceInfo(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            TraceInternal(TraceEventType.Information, message);
        }

        /// <summary>
        /// Trace warning message to trace repository
        /// </summary>
        /// <param name="message">Warning message to trace</param>
        public static void TraceWarning(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            TraceInternal(TraceEventType.Warning, message);
        }

        /// <summary>
        /// Trace error message to trace repository
        /// </summary>
        /// <param name="message">Error message to trace</param>
        public static void TraceError(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            TraceInternal(TraceEventType.Error, message);
        }

        /// <summary>
        /// Trace exception
        /// </summary>
        /// <param name="exception">Exception to trace</param>
        public static void TraceError(Exception exception)
        {
            TraceManager.TraceError(exception.Message);
            TraceManager.TraceError(exception.StackTrace);
            if (exception.InnerException != null)
                TraceManager.TraceError(exception.InnerException.Message);
        }

        /// <summary>
        /// Trace critical message to trace repository
        /// </summary>
        /// <param name="message">Critical message to trace</param>
        public static void TraceCritical(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            TraceInternal(TraceEventType.Critical, message);
        }

        #endregion
    }
}
