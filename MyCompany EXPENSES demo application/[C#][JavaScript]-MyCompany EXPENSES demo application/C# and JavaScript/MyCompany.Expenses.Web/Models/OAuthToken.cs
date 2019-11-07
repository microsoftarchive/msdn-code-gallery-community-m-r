namespace MyCompany.Expenses.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    /// <summary>
    /// OAuthToken
    /// </summary>
    [DataContract]
    public class OAuthToken
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// TokenType
        /// </summary>
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
    }
}