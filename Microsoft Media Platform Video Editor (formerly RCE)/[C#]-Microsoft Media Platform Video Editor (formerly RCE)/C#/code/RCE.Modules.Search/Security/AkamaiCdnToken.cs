/**
 * Package:     URL-Based Authentication Token Generator for C#
 * Title:       URLToken
 * Description: Implements token generation functionality necessary for
 *              URLs using URL-Based Authentication.
 * Copyright:   Copyright (c) Akamai Technologies, Inc. 2005
 * Company:     Akamai Technologies, Inc.
 * 
 * Updated to fix Source Analysis Issues.
 * Refactored to make code more readable
 */

namespace RCE.Modules.Search.Security
{
    using System;
    using System.Linq;
    using System.Text;
#if !SILVERLIGHT
    using System.Security.Cryptography;
#endif

    public class AkamaiCdnToken : ICdnToken
    {
        private readonly string token;

        private readonly string url;

        private readonly string param;

        private readonly long expires;

        public AkamaiCdnToken(string inUrl, long inWindow, string salt, string extract, long time, string inParam)
        {
            if (string.IsNullOrEmpty(inUrl))
            {
                throw new TokenException("URL is empty or null");
            }

            if (inWindow < 0)
            {
                throw new TokenException("Window is negative");
            }

            if (string.IsNullOrEmpty(salt))
            {
                throw new TokenException("Salt is empty or null");
            }

            this.url = inUrl;
            this.param = inParam;
            extract = string.IsNullOrEmpty(extract) ? null : extract;

            if (time <= 0)
            {
                time = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            }

            if (string.IsNullOrEmpty(this.param))
            {
                this.param = "__gda__";
            }

            if (this.param.Length < 5 || this.param.Length > 12)
            {
                throw new TokenException("Parameter must be between 5 and 12 characters in length");
            }

            this.expires = time + inWindow;

            var expBytes = BitConverter.GetBytes((int)(time + inWindow));

            /*
             * The token generated has this structure:
             *  MD5(salt|MD5(Url|extract|salt))
             */
            var sb = new StringBuilder();
            sb.Append(this.url);
            sb.Append(extract);
            sb.Append(salt);

            byte[] dataBytes = Encoding.UTF8.GetBytes(sb.ToString());

            byte[] buffer1 = new byte[expBytes.Length + sb.Length];

            Array.Copy(expBytes, buffer1, expBytes.Length);
            Array.Copy(dataBytes, 0, buffer1, expBytes.Length, dataBytes.Length);

            MD5Managed hashComputer = new MD5Managed();

            byte[] digest1 = hashComputer.ComputeHash(buffer1);

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            byte[] binaryString = new byte[saltBytes.Length + digest1.Length];
            Array.Copy(saltBytes, binaryString, saltBytes.Length);
            Array.Copy(digest1, 0, binaryString, saltBytes.Length, digest1.Length);

            byte[] binaryToken = hashComputer.ComputeHash(binaryString);

            // Generate a token string from the byte array
            var tokenString = string.Concat(binaryToken.Select(b => string.Format("{0:x2}", b)));

            this.token = tokenString;
        }

        public string Token
        {
            get
            {
                return this.token;
            }
        }

        public string AssetUrlQueryString
        {
            get
            {
                return string.Format("{0}={1}_{2}", this.param, this.expires, this.token);
            }
        }
    }
}