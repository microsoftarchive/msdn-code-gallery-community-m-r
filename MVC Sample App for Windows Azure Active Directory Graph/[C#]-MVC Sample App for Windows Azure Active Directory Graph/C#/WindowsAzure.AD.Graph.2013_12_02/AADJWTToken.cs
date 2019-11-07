using System;
using System.Runtime.Serialization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Microsoft.WindowsAzure.ActiveDirectory.GraphHelper
{
    /// <summary>
    /// Class that will be instantiated using the token value fetched from Azure AD for talking to Graph.
    /// </summary>
    [DataContract]
    public class AADJWTToken 
    {
        [DataMember(Name = "adal_token")]
        public AuthenticationResult AdalToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_on")]
        public long ExpiresOn { get; set; }

        public long ExpiresIn 
        {
            get 
            {
              return (this.ExpiresOn - DateTimeOffset.Now.UtcTicks);            
            }        
        }

        /// <summary>
        /// Returns true if the token is expired and false otherwise.
        /// </summary>
        public bool IsExpired
        {
            get
            {
                return WillExpireIn(0);
            }
        }

        /// <summary>
        /// Returns true if the token will expire in the number of minutes passed in to the method.
        /// </summary>
        /// <param name="minutes">minutes in which the token is checked for expiration.</param>
        /// <returns></returns>
        public bool WillExpireIn(int minutes)
        {
            long futureTime = (DateTimeOffset.UtcNow.AddMinutes((double)minutes)).Ticks;
            return futureTime > ExpiresOn;
        }
    }

}
