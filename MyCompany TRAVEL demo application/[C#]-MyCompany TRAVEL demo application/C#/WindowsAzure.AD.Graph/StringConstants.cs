namespace Microsoft.WindowsAzure.ActiveDirectory.GraphHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class StringConstants
    {
        internal const string AzureADSTSURL = "https://login.windows.net/{0}/oauth2/token?api-version=1.0";
        internal const string GraphPrincipalId = "https://graph.windows.net";
        internal const string DirectoryServiceURL = "https://graph.windows.net/";
        internal const string GraphServiceVersion = "2013-04-05";
    }
}
