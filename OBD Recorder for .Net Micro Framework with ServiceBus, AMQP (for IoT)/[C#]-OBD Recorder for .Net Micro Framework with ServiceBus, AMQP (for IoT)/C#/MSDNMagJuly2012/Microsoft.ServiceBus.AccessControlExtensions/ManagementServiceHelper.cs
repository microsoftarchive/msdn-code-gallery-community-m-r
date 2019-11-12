// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement;

    /// <summary>
    ///   This class obtains a SWT token and adds it to the HTTP authorize header 
    ///   for every request to the management service.
    /// </summary>
    public class ManagementServiceHelper
    {
        static string cachedSwtToken;

        /// <summary>
        ///   Creates and returns a ManagementService object. This is the only 'interface' used by other classes.
        /// </summary>
        /// <returns>An instance of the ManagementService.</returns>
        public static ManagementService CreateManagementServiceClient(AccessControlSettings settings)
        {
            var managementServiceEndpoint = String.Format(
                                                          CultureInfo.InvariantCulture,
                                                          "https://{0}.{1}/{2}",
                                                          settings.ServiceNamespace,
                                                          settings.AccessControlServiceAddress,
                                                          settings.AccessControlManagementPath);
            var managementService = new ManagementService(new Uri(managementServiceEndpoint));
            managementService.SendingRequest += (o, e) => AddManagementTokenWithWritePermission((HttpWebRequest) e.Request, settings);
            return managementService;
        }


        /// <summary>
        ///   Helper function for the event handler above, adding the SWT token to the HTTP 'Authorization' header. 
        ///   The SWT token is cached so that we don't need to obtain a token on every request.
        /// </summary>
        /// <param name = "args">Event arguments.</param>
        public static void AddManagementTokenWithWritePermission(HttpWebRequest args, AccessControlSettings settings)
        {
            if (cachedSwtToken == null)
            {
                cachedSwtToken = GetManagementToken(settings);
            }
            args.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + cachedSwtToken);
        }

        /// <summary>
        ///   Obtains a SWT token from ACSv2.
        /// </summary>
        /// <returns>A token  from ACS.</returns>
        static string GetManagementToken(AccessControlSettings settings)
        {
            try
            {
                //
                // Request a token from ACS
                //
                var client = new WebClient();
                client.BaseAddress = string.Format(
                                                   CultureInfo.CurrentCulture,
                                                   "https://{0}.{1}",
                                                   settings.ServiceNamespace,
                                                   settings.AccessControlServiceAddress);

                var scopeUri = new Uri(client.BaseAddress + settings.AccessControlManagementPath);
                var values = new NameValueCollection();
                values.Add("grant_type", "client_credentials");
                values.Add("client_id", settings.ManagementServiceIdentityName);
                values.Add("client_secret", settings.ManagementServiceIdentityKey);
                values.Add("scope", scopeUri.ToString());

                var responseBytes = client.UploadValues("/v2/OAuth2-13", "POST", values);

                //
                // Extract the access token and return it.
                //
                using (var responseStream = new MemoryStream(responseBytes))
                {
                    var tokenResponse = (OAuth2TokenResponse) new DataContractJsonSerializer(typeof (OAuth2TokenResponse)).ReadObject(responseStream);
                    return tokenResponse.access_token;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [DataContract]
        class OAuth2TokenResponse
        {
            [DataMember]
            public string access_token;
        }
    }
}