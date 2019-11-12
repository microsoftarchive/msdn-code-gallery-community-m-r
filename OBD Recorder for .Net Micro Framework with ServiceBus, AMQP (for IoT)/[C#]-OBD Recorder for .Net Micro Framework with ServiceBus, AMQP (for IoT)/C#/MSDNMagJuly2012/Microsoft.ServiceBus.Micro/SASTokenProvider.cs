// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  

// #undef MF_FRAMEWORK_VERSION_V4_2 // Contains NET MF4.2 specific code

namespace Microsoft.ServiceBus.Micro
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Text;
    using ElzeKool;

    public class SASTokenProvider : TokenProvider
    {

        string keyName { get { return base.issuerName; } }
        string keySecret { get { return base.issuerSecret; } }
        uint tokenExpiryInSeconds = 1200;

        const long ticksPerSecond = 1000000000 / 100; // 1 tick = 100 nano seconds

        public SASTokenProvider(string keyName, string keySecret, uint tokenExpiryInSeconds = 1200) 
            : base(keyName, keySecret)
        {
            this.tokenExpiryInSeconds = tokenExpiryInSeconds;
        }

        protected override TokenAndExpiration GetTokenNoCache(string serviceNamespace, string acsHostName, string sbHostName, string path)
        {
            string uri = "http://" + serviceNamespace + "." + sbHostName + path;
            var expiry = GetExpiry(this.tokenExpiryInSeconds); // Set token lifetime to 20 minutes. 
            string stringToSign = HttpUtility.UrlEncode(uri) + "\n" + expiry;

            var hmac = SHA.computeHMAC_SHA256(Encoding.UTF8.GetBytes(this.keySecret), Encoding.UTF8.GetBytes(stringToSign));
            string signatureString = Convert.ToBase64String(hmac);

#if MF_FRAMEWORK_VERSION_V4_2
            // Adjust for .NET MF 4.2 character set difference
            signatureString = Base64NetMf42ToRfc4648(signatureString);
#endif

            var tokenAndExpiration = new TokenAndExpiration();

            tokenAndExpiration.ExpirationTime = DateTime.UtcNow.AddSeconds(this.tokenExpiryInSeconds - 60); // Treat as expired 60 seconds earlier

            tokenAndExpiration.Token = "SharedAccessSignature sr=" + HttpUtility.UrlEncode(uri) + "&sig=" + HttpUtility.UrlEncode(signatureString) + "&se=" + expiry + "&skn=" + this.keyName;
            return tokenAndExpiration;
        }

        static uint GetExpiry(uint tokenLifetimeInSeconds) 
        { 
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0); 
            TimeSpan diff = DateTime.Now.ToUniversalTime() - origin; 
            return ((uint) (diff.Ticks / ticksPerSecond)) + tokenLifetimeInSeconds; 
        } 

    }
}