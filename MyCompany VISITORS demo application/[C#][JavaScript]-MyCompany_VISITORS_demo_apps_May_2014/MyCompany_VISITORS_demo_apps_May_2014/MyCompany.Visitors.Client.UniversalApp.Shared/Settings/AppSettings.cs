namespace MyCompany.Visitors.Client.UniversalApp.Settings
{
    using MyCompany.Visitors.Client;
    using System;
    using Windows.Storage;

    /// <summary>
    /// Class to store application settings across a user session.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Auth uri storage key.
        /// </summary>
        public const string STORAGEKEY_AUTHURI = "authUri";
        /// <summary>
        /// api uri storage key.
        /// </summary>
        public const string STORAGEKEY_APIURI = "apiUri";
        /// <summary>
        /// test mode storage key.
        /// </summary>
        public const string STORAGEKEY_TESTMODE = "testMode";
        /// <summary>
        /// security token storage key.
        /// </summary>
        public const string STORAGEKEY_SECURITYTOKEN = "securityToken";
        /// <summary>
        /// security token expiration date storage key.
        /// </summary>
        public const string STORAGEKEY_SECURITYTOKEN_EXPIRATION = "securityTokenExpirationDateTime";
        /// <summary>
        /// Width of snapped view
        /// </summary>
        public const int SNAPPED_WIDTH = 500;

        /// <summary>
        /// Transition between two apps to snapped.
        /// </summary>
        public const int MEDIUM_SNAPPED_WIDTH = 800;

        /// <summary>
        /// Uri for authetication.
        /// </summary>
        public static string AuthenticationUri 
        {
            get
            {
                string authUri;
                if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(STORAGEKEY_AUTHURI))
                {
                    authUri = "https://login.windows.net/mycompanydemo.onmicrosoft.com";
                    ApplicationData.Current.LocalSettings.Values[STORAGEKEY_AUTHURI] = authUri;
                }
                else
                {
                    authUri = ApplicationData.Current.LocalSettings.Values[STORAGEKEY_AUTHURI].ToString();
                }
                return authUri;
            }
        }

        /// <summary>
        /// API uri for authentication
        /// </summary>
        public static Uri ApiUri 
        {
            get
            {
                string apiUri;
                if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(STORAGEKEY_APIURI))
                {                 
                    apiUri = "http://localhost:31330/";                    
                    ApplicationData.Current.LocalSettings.Values[STORAGEKEY_APIURI] = apiUri;
                }
                else
                {
                    apiUri = ApplicationData.Current.LocalSettings.Values[STORAGEKEY_APIURI].ToString();
                }


                if (!apiUri.EndsWith("/"))
                    apiUri = string.Format("{0}/", apiUri);

                return new Uri(apiUri);
            }
        }

        /// <summary>
        /// Reply Uri for authentication process
        /// </summary>
        public static string ReplyUri
        {
            get
            {               
               return "http://localhost:31330/";
            }
        }

        /// <summary>
        /// Client Id for authentication process
        /// </summary>
        public static string ClientId
        {
            get
            {
                return "bb300cb2-f97b-449d-b7c3-590067f4c7f7";
            }
        }

        /// <summary>
        /// Test mode.
        /// </summary>
        public static bool TestMode
        {
            get
            {
                bool isTest = true;
                
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(STORAGEKEY_TESTMODE))
                {
                    bool.TryParse(ApplicationData.Current.LocalSettings.Values[STORAGEKEY_TESTMODE].ToString(), out isTest);
                    return isTest;
                }

                return isTest;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values[STORAGEKEY_TESTMODE] = value.ToString();
            }
        }

        /// <summary>
        /// Obtained token from authentication.
        /// </summary>
        public static string SecurityToken 
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(STORAGEKEY_SECURITYTOKEN))
                    return ApplicationData.Current.LocalSettings.Values[STORAGEKEY_SECURITYTOKEN].ToString();

                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ApplicationData.Current.LocalSettings.Values[STORAGEKEY_SECURITYTOKEN] = value;
                }
            }
        }

        /// <summary>
        /// SecurityToken Expiration DateTime
        /// </summary>
        public static DateTimeOffset SecurityTokenExpirationDateTime
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(STORAGEKEY_SECURITYTOKEN_EXPIRATION))
                {
                    return (DateTimeOffset)ApplicationData.Current.LocalSettings.Values[STORAGEKEY_SECURITYTOKEN_EXPIRATION];
                }

                return DateTime.MinValue;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values[STORAGEKEY_SECURITYTOKEN_EXPIRATION] = value;
            }
        }

        /// <summary>
        /// Hides the second crop in visitor creation.
        /// </summary>
        public static bool HideSmallImageCrop
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Authenticated employee information.
        /// </summary>
        public static Employee EmployeeInformation { get; set; }

        /// <summary>
        /// Remove the key and its value from the local settings.
        /// </summary>
        /// <param name="keyToDelete"></param>
        public static void RemoveKeyValue(string keyToDelete)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(keyToDelete))
            {
                ApplicationData.Current.LocalSettings.Values.Remove(keyToDelete);
            }   
        }

        /// <summary>
        /// update the value of the given key in the local settings.
        /// </summary>
        /// <param name="keyToUpdate"></param>
        /// <param name="value"></param>
        public static void UpdateKeyValue(string keyToUpdate, string value)
        {
            ApplicationData.Current.LocalSettings.Values[keyToUpdate] = value;
        }
    }
}
