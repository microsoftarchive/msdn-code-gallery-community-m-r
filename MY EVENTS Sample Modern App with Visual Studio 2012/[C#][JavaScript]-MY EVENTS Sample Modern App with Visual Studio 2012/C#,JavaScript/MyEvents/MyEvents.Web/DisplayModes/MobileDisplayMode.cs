using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;

namespace MyEvents.Web.DisplayModes
{
    /// <summary>
    /// Display mode for mobile.
    /// </summary>
    public class MobileDisplayMode : DefaultDisplayMode
    {
        //list of user agents: http://www.useragentstring.com/pages/Mobile%20Browserlist/
        private readonly List<string> _mobileUserAgents = new List<string>
        {
            "Android",
            "Mobile",
            "Opera Mobi",
            "Samsung",
            "HTC",
            "Nokia",
            "Ericsson",
            "SonyEricsson",
            "iPhone",
            "Windows Phone"
        };

        /// <summary>
        /// Constructor for mobile display mode.
        /// </summary>
        public MobileDisplayMode()
            : base("Mobile")
        {
            ContextCondition = (context => IsMobile(context.GetOverriddenUserAgent()));
        }

        private bool IsMobile(string userAgent)
        {
            return _mobileUserAgents
                .Any(val => userAgent.IndexOf(val, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }
    }
}