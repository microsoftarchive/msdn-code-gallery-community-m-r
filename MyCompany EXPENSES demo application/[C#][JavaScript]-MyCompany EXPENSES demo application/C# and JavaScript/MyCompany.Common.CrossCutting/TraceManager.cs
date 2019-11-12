using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace MyCompany.Common.CrossCutting
{
    /// <summary>
    /// Trace helper for application's logging
    /// </summary>
    public static class TraceManager
    {
        #region Members

        #endregion

        #region Private Methods

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

            Trace.TraceInformation(Concat(message));
        }

        /// <summary>
        /// Trace warning message to trace repository
        /// </summary>
        /// <param name="message">Warning message to trace</param>
        public static void TraceWarning(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            Trace.TraceWarning(Concat(message));
        }

        /// <summary>
        /// Trace error message to trace repository
        /// </summary>
        /// <param name="message">Error message to trace</param>
        public static void TraceError(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            Trace.TraceError(Concat(message));
        }

        /// <summary>
        /// Trace exception
        /// </summary>
        /// <param name="exception">Exception to trace</param>
        public static void TraceError(Exception exception)
        {
            TraceManager.TraceError(exception.ToString());
        }

        /// <summary>
        /// Trace critical message to trace repository
        /// </summary>
        /// <param name="message">Critical message to trace</param>
        public static void TraceCritical(params string[] message)
        {
            if (message == null)
                throw new ArgumentNullException("message","Trace message is null or empty string");

            Trace.TraceWarning(Concat(message));
        }

        #endregion
    }
}
