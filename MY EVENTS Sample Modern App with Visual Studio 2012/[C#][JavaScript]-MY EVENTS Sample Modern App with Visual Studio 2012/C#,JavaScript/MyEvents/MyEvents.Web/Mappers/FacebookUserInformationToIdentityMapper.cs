using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Web.Authentication;

namespace MyEvents.Web.Mappers
{
    /// <summary>
    /// Facebook user information to identity mapper.
    /// </summary>
    public class FacebookUserInformationToIdentityMapper
    {
        /// <summary>
        /// Maps the facebook user information into the identity.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public MyEventsIdentity Map(Dictionary<string, object> source, MyEventsIdentity target)
        {
            target.ThirdPartyUserId = source["id"].ToString();
            target.UserName = source["name"].ToString();
            target.Email = source["email"].ToString();
            target.Bio = source.ContainsKey("bio") ? source["bio"].ToString() : string.Empty;

            if (source.ContainsKey("location"))
            {
                var location = source["location"] as Dictionary<string, object>;
                if (null != location)
                {
                    target.City = location["name"].ToString();
                }
            }

            return target;
        }
    }
}