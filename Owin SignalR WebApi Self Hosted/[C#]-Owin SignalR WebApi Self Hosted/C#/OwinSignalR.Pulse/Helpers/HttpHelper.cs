using System;

namespace OwinSignalR.Pulse.Helpers
{
    public static class HttpHelper
    {
        public static string GetUrlReferer(
           string referer)
        {
            if (string.IsNullOrEmpty(referer))
            {
                return string.Empty;
            }

            referer = referer.IndexOf("://") > -1 ? referer.Substring(referer.IndexOf("://") + 3) : referer;
            referer = referer.IndexOf("/") > -1 ? referer.Substring(0, referer.IndexOf("/") - 1) : referer;
            referer = referer.IndexOf(":") > -1 ? referer.Split(':')[0] : referer;

            return referer;
        }
    }
}
