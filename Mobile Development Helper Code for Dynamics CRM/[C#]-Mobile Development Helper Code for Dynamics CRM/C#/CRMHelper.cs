// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Sdk.Samples;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;

namespace UniversalModernApp
{
    static class CRMHelper
    {
        // TODO Set these string values as approppriate for your app registration and organization.
        // For more information, see the SDK topic "Walkthrough: Register an app with Active Directory".
        public const string ServerUrl = "https://my-domain.crm.dynamics.com/";
        public const string ResourceName = "https://my-domain.crm.dynamics.com/";
        public static string RedirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
        public const string ClientId = "893262be-fbdc-4556-9325-9f863b69495b";

        public static string AuthorityUrl;

        static public OrganizationDataWebServiceProxy proxy;

        static CRMHelper()
        {
            proxy = new OrganizationDataWebServiceProxy();
            proxy.ServiceUrl = ServerUrl;
            proxy.EnableProxyTypes();
        }

        /// <summary>
        /// Method to get authority URL from organization’s SOAP endpoint.
        /// http://msdn.microsoft.com/en-us/library/dn531009.aspx#bkmk_oauth_discovery
        /// </summary>
        /// <param name="result">The Authority Url returned from HttpResponseMessage.</param>
        public static async System.Threading.Tasks.Task DiscoveryAuthority()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                // need to specify soap endpoint with client version,.
                HttpResponseMessage httpResponse = await httpClient.GetAsync(ServerUrl + "/XRMServices/2011/Organization.svc/web?SdkClientVersion=6.1.0.533");
                // For phone, we dont need oauth2/authorization part.
                AuthorityUrl = System.Net.WebUtility.UrlDecode(httpResponse.Headers.GetValues("WWW-Authenticate").FirstOrDefault().Split('=')[1]).Replace("oauth2/authorize", "");
            }
        }

        #region ADAL for Windows Phone 8/8.1

        static public AuthenticationContext authContext = null;

        public static async System.Threading.Tasks.Task GetTokenSilent()
        {
            if(String.IsNullOrEmpty(CRMHelper.AuthorityUrl))
                await CRMHelper.DiscoveryAuthority();

            // If authContext is null, then generate it.
            if (authContext == null)
#if WINDOWS_PHONE_APP
                // ADAL for Windows Phone 8.1 builds AuthenticationContext instances throuhg a factory, which performs authority validation at creation time
                authContext = AuthenticationContext.CreateAsync(CRMHelper.AuthorityUrl).GetResults();
            AuthenticationResult result = await authContext.AcquireTokenSilentAsync(CRMHelper.ResourceName, CRMHelper.ClientId);

#else
                authContext = new AuthenticationContext(CRMHelper.AuthorityUrl, false);
            AuthenticationResult result = await authContext.AcquireTokenAsync(CRMHelper.ResourceName, CRMHelper.ClientId);            
#endif

            if (result != null && result.Status == AuthenticationStatus.Success)
            {
                // A token was successfully retrieved. Then store it.
                StoreToken(result);
            }
            else
            {
#if WINDOWS_PHONE_APP
                // Clear the AccessToken first so that any Service Calls waits until it's filled.
                proxy.AccessToken = "";
                // In case credential was wrong, clear the token cache first.
                authContext.TokenCache.Clear();
                // Acquiring a token without user interaction was not possible. 
                // Trigger an authentication experience and specify that once a token has been obtained the StoreToken method should be called.
                authContext.AcquireTokenAndContinue(CRMHelper.ResourceName, CRMHelper.ClientId, new Uri(CRMHelper.RedirectUri), StoreToken);
#else
                DisplayErrorWhenAcquireTokenFails(result);
#endif
            }
        }

        public static void StoreToken(AuthenticationResult result)
        {
            if (result.Status == AuthenticationStatus.Success)
            {
                CRMHelper.proxy.AccessToken = result.AccessToken;
            }
            else
            {
                DisplayErrorWhenAcquireTokenFails(result);
            }
        }

        static private async void DisplayErrorWhenAcquireTokenFails(AuthenticationResult result)
        {
            MessageDialog dialog;

            switch (result.Error)
            {
                case "authentication_canceled":
                    // User cancelled, so no need to display a message.
                    break;
                case "temporarily_unavailable":
                case "server_error":
                    dialog = new MessageDialog("Please retry the operation. If the error continues, please contact your administrator.", "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
                default:
                    // An error occurred when acquiring a token. Show the error description in a MessageDialog. 
                    dialog = new MessageDialog(string.Format("If the error continues, please contact your administrator.\n\nError: {0}\n\nError Description:\n\n{1}", result.Error, result.ErrorDescription), "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
            }
        }

        #endregion

    }
}
