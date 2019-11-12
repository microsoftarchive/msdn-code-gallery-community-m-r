using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
 
namespace MyEvents.Api.Authentication
{
    /// <summary>
    /// Storage My Events sample token. 
    /// THIS IS A VERY SIMPLE WAY TO CREATE AND ENCRYPT A TOKEN, FOR DEMO PORPOUSES. 
    /// FOR A REAL ENVIRONMENT BE SURE TO USE USE A BETTER SOLUTION
    /// <see href="http://codebetter.com/johnvpetersen/2012/04/02/making-your-asp-net-web-apis-secure/"></see>
    /// <see href="http://zamd.net/2012/05/04/claim-based-security-for-asp-net-web-apis-using-dotnetopenauth/?goback=%2Egde_4477233_member_121979238"></see>
    /// </summary>
    public class MyEventsToken
    {
        // DO NOT USE THE PUBLIC/PRIVATE KEYS IN THIS EXAMPLE IN YOUR PRODUCTION APPLICATION
        private static string _privateKey = "<RSAKeyValue><Modulus>s6lpjspk+3o2GOK5TM7JySARhhxE5gB96e9XLSSRuWY2W9F951MfistKRzVtg0cjJTdSk5mnWAVHLfKOEqp8PszpJx9z4IaRCwQ937KJmn2/2VyjcUsCsor+fdbIHOiJpaxBlsuI9N++4MgF/jb0tOVudiUutDqqDut7rhrB/oc=</Modulus><Exponent>AQAB</Exponent><P>3J2+VWMVWcuLjjnLULe5TmSN7ts0n/TPJqe+bg9avuewu1rDsz+OBfP66/+rpYMs5+JolDceZSiOT+ACW2Neuw==</P><Q>0HogL5BnWjj9BlfpILQt8ajJnBHYrCiPaJ4npghdD5n/JYV8BNOiOP1T7u1xmvtr2U4mMObE17rZjNOTa1rQpQ==</Q><DP>jbXh2dVQlKJznUMwf0PUiy96IDC8R/cnzQu4/ddtEe2fj2lJBe3QG7DRwCA1sJZnFPhQ9svFAXOgnlwlB3D4Gw==</DP><DQ>evrP6b8BeNONTySkvUoMoDW1WH+elVAH6OsC8IqWexGY1YV8t0wwsfWegZ9IGOifojzbgpVfIPN0SgK1P+r+kQ==</DQ><InverseQ>LeEoFGI+IOY/J+9SjCPKAKduP280epOTeSKxs115gW1b9CP4glavkUcfQTzkTPe2t21kl1OrnvXEe5Wrzkk8rA==</InverseQ><D>HD0rn0sGtlROPnkcgQsbwmYs+vRki/ZV1DhPboQJ96cuMh5qeLqjAZDUev7V2MWMq6PXceW73OTvfDRcymhLoNvobE4Ekiwc87+TwzS3811mOmt5DJya9SliqU/ro+iEicjO4v3nC+HujdpDh9CVXfUAWebKnd7Vo5p6LwC9nIk=</D></RSAKeyValue>";
        private static string _publicKey = "<RSAKeyValue><Modulus>s6lpjspk+3o2GOK5TM7JySARhhxE5gB96e9XLSSRuWY2W9F951MfistKRzVtg0cjJTdSk5mnWAVHLfKOEqp8PszpJx9z4IaRCwQ937KJmn2/2VyjcUsCsor+fdbIHOiJpaxBlsuI9N++4MgF/jb0tOVudiUutDqqDut7rhrB/oc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static UnicodeEncoding _encoder = new UnicodeEncoding();
        private static DateTime _expiredTime = DateTime.MinValue;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token"></param>
        public MyEventsToken(string token)
        {
            string decrypt = Decrypt(token);
            string[] tokens = decrypt.Split('@');
            string id = tokens[0];
            int registeredUserId;
            if (!Int32.TryParse(id, out registeredUserId))
            {
                RegisteredUserId = 0;
            }
            else
            {
                RegisteredUserId = registeredUserId;
                DateTime dt;
                if (DateTime.TryParse(tokens[1], out dt))
                    _expiredTime = dt;
            }
        }

        /// <summary>
        /// true if the token is expired
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            return _expiredTime < DateTime.UtcNow;
        }

        /// <summary>
        /// Create token to return to the client
        /// </summary>
        /// <param name="registeredUserId"></param>
        /// <returns></returns>
        public static string CreateToken(int registeredUserId)
        {
            DateTime expiredTime = DateTime.UtcNow.AddHours(1);
            string token = String.Format(CultureInfo.InvariantCulture, "{0}@{1}", registeredUserId, expiredTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return Encrypt(token);
        }

        /// <summary>
        /// Get the user id from HTTP Headers
        /// </summary>
        /// <returns>registeredUserId</returns>
        public static MyEventsToken GetTokenFromHeader()
        {
            // Validate that the Authorization header has a valid token
            if (HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization"))
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];
                if (!String.IsNullOrEmpty(authHeader) && authHeader.Length > 6)
                {
                    string authToken = authHeader.Substring(6);
                    if (!String.IsNullOrEmpty(authToken))
                        return new MyEventsToken(authToken);
                }
            }

            return null;
        }

        /// <summary>
        /// RegisteredUserId
        /// </summary>
        public int RegisteredUserId { get; set; }

        private static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(_privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return _encoder.GetString(decryptedByte);
        }

        private static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }

    }
}