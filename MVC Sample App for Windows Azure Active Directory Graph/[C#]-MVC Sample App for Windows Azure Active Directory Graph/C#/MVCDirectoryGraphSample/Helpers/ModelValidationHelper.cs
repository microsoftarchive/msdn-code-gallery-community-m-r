using System;
using System.Web.Mvc;
using System.Xml;

namespace MvcDirectoryGraphSample.Helpers
{
    /// <summary>
    /// Provides some basic validation helpers for properties.
    /// </summary>
    public static class ModelValidationHelper
    {
        internal static void ValidateStringProperty(ModelStateDictionary modelState, string propertyValue, string propertyName, string errorMessage)
        {
            if (String.IsNullOrEmpty(propertyValue))
            {
                modelState.AddModelError(propertyName, errorMessage);
            }
        }

        internal static void ValidateProperty(ModelStateDictionary modelState, object propertyValue, string propertyName, string errorMessage)
        {
            if (propertyValue == null)
            {
                modelState.AddModelError(propertyName, errorMessage);
            }
        }

        /// <summary>
        /// Loads the error message from Data Service Exception and returns the error code and error message if the message is valid
        /// Xml message.
        /// </summary>
        public static void GetErrorCodeAndMessageForGraphServiceError(string exceptionMessage, out string errorCode, out string errorMessage)
        {
            errorCode = null;
            errorMessage = null;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(exceptionMessage);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("m", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
                errorCode = doc.DocumentElement.SelectSingleNode("m:code", nsmgr).InnerText;
                errorMessage = doc.DocumentElement.SelectSingleNode("m:message", nsmgr).InnerText;

            }
            catch (XmlException)
            {
            }
            return;
        }
    }
}