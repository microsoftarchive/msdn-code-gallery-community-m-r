using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Microsoft.WindowsAzure.ActiveDirectory.GraphHelper
{
    /// <summary>
    /// Helper class to fetch tokens from AAD for talking to AAD Graph Service.
    /// </summary>
    public static class DirectoryDataServiceAuthorizationHelper 
    {
        /// <summary>
        /// Methods for getting a token from ACS 
        /// Updated 10/21, to use Active Directory Authn Library (ADAL) 
        /// Method uses OAuth Client Credential Authn flow (2-legged OAuth)
        /// ADAL package avaialble from https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/1.0.0
        /// </summary>
        public static AADJWTToken GetAuthorizationToken(string tenantName, string appPrincipalId, string password) 
        {

            string authString = String.Format(StringConstants.AzureADSTSURL, tenantName);
            AuthenticationContext authenticationContext = new AuthenticationContext(authString);
            ClientCredential applicationCreds = new ClientCredential(appPrincipalId, password);

            try
            {
                AuthenticationResult authenticationResult = authenticationContext.AcquireToken(StringConstants.GraphPrincipalId.ToString(), applicationCreds);

                if (authenticationResult != null)
                {
                    AADJWTToken token = new AADJWTToken();
                    token.AccessToken = authenticationResult.AccessToken;
                    token.TokenType = authenticationResult.AccessTokenType;
                    token.ExpiresOn = authenticationResult.ExpiresOn.UtcTicks;
                    token.AdalToken = authenticationResult;
                    return token;
                }
                else
                    return null;
            }
            catch(Exception e)
            {
                //Console.WriteLine("Exception: " + e.Message + " " + e.InnerException);
                return null;
            }
        }


        /// <summary>
        /// Methods for getting a token from ACS 
        /// Updated 10/21, to use Active Directory Authn Library (ADAL) 
        /// Method uses OAuth Authorization Code Grant flow (3-legged OAuth)
        /// ADAL package avaialble from https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/1.0.0
        /// </summary>

        public static AADJWTToken GetAuthorizationToken(string tenantName, string appPrincipalId, Uri appUri)
        {

            string authString = String.Format(StringConstants.AzureADSTSURL, tenantName);
            AuthenticationContext authenticationContext = new AuthenticationContext(authString);
            try
            {
                AuthenticationResult authenticationResult = authenticationContext.AcquireToken(StringConstants.GraphPrincipalId.ToString(), appPrincipalId, appUri);
                if (authenticationResult != null)
                {
                    AADJWTToken token = new AADJWTToken();
                    token.AccessToken = authenticationResult.AccessToken;
                    token.TokenType = authenticationResult.AccessTokenType;
                    token.ExpiresOn = authenticationResult.ExpiresOn.UtcTicks;
                    token.AdalToken = authenticationResult;
                    return token;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Exception: " + e.Message + " " + e.InnerException);
                return null;
            }
        }


    }

} 