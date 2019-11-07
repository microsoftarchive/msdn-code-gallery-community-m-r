using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using MyEvents.Model;

namespace MyEvents.Api.Authentication
{
     /// <summary>
    /// Service to get information from facebook.
    /// </summary>
    public class FacebookService : IFacebookService 
    {
        /// <summary>
        /// Obtains the user information for the given token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>FacebookAppId</returns>
        public RegisteredUser GetUserInformation(string token)
        {
            return Parse(GetInformation(token));
        }

        private static string GetInformation(string token)
        {
            var client = new WebClient();
            string meUrl = string.Format("https://graph.facebook.com/me?access_token={0}&fields=name%2Cemail%2Cbio%2Clocation", token);

            string userInformationAsJson;

            try
            {
                userInformationAsJson = client.DownloadString(meUrl);
            }
            catch (Exception)
            {
                return null;
            }

            return userInformationAsJson;
        }

        private static RegisteredUser Parse(string userInformationAsJson)
        {
            if (String.IsNullOrEmpty(userInformationAsJson))
                return null;

            var jsSerializer = new JavaScriptSerializer();
            var userInformation = jsSerializer.Deserialize<Dictionary<string, object>>(userInformationAsJson);

            RegisteredUser information = new RegisteredUser()
            {
                FacebookId = userInformation["id"].ToString(),
                Name = userInformation["name"].ToString(),
                Email = userInformation["email"].ToString(),
                Bio = userInformation.ContainsKey("bio") ? userInformation["bio"].ToString() : string.Empty,
            };


            if (userInformation.ContainsKey("location"))
            {
                var location = userInformation["location"] as Dictionary<string, object>;
                if (null != location)
                {
                    information.City = location["name"].ToString();
                }
            }

            return information;
        }
    }

   
}