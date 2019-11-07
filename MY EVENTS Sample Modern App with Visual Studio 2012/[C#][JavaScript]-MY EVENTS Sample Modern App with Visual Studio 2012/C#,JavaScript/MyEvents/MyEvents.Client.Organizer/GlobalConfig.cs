using Windows.Storage;

namespace MyEvents.Client.Organizer
{
    /// <summary>
    /// Static class containing application configuration info.
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// base API service adress
        /// </summary>
        public static string ApiUrlPrefix 
        { 
            get 
            {   
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("ApiUrlPrefix"))
                {
                    return ApplicationData.Current.LocalSettings.Values["ApiUrlPrefix"].ToString();
                }
                else
                {
                    return "http://localhost:2565/";
                }
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["ApiUrlPrefix"] = value;
            }
        }

        /// <summary>
        /// Web URL service adress
        /// </summary>
        public static string WebUrlPrefix
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("WebUrlPrefix"))
                {
                    return ApplicationData.Current.LocalSettings.Values["WebUrlPrefix"].ToString();
                }
                else
                {
                    return "http://localhost:2407/";
                }
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["WebUrlPrefix"] = value;
            }
        }

        /// <summary>
        /// true if offline mode is activated
        /// </summary>
        public static bool IsOfflineMode
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("IsOfflineMode"))
                {
                    return (bool)ApplicationData.Current.LocalSettings.Values["IsOfflineMode"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsOfflineMode"] = value;
            }
        }

        /// <summary>
        /// Twitter Consumer Key
        /// </summary>
        public static string TwitterConsumerKey
        {
            get
            {
                return "A2FdjQTBNccPQSnotTxZg";
            }
        }

        /// <summary>
        /// Twitter Consumer Secret
        /// </summary>
        public static string TwitterConsumerSecret
        {
            get
            {
                return "3re3Y4L3QKzJo4m1qRIlF8FzMLTHtfxWNaVW8drg";
            }
        }

        /// <summary>
        /// Twitter Return Url
        /// </summary>
        public static string TwitterReturnUrl
        {
            get
            {
                return "http://127.0.0.1:3000/auth/twitter/callback";
            }
        }

        /// <summary>
        /// FacebookAppId
        /// </summary>
        public static string OffllineFakeUserName
        {
            get
            {
                return "Orville McDonald";
            }
        }

        /// <summary>
        /// FacebookAppId
        /// </summary>
        public static string FacebookAppId
        {
            get
            {
                return "512549695437523";
            }
        }
    }
}
