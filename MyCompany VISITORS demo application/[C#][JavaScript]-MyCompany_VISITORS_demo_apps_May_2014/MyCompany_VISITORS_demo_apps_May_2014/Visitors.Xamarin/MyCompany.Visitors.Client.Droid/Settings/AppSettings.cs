using Android.Content;

namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client;
    using System;

    /// <summary>
    /// Class to store application settings across a user session.
    /// </summary>
    public static class AppSettings
    {
		public static ISharedPreferences Prefs { get; set; }


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
                authUri = "https://login.windows.net/mycompanydemos.onmicrosoft.com";
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
                string apiUri = Prefs.GetString("serverurl", "http://localhost:31330/");
                if (!apiUri.EndsWith("/"))
                    apiUri = string.Format("{0}/", apiUri);

                return new Uri(apiUri);
            }
	        set
	        {
				Prefs.Edit().PutString("serverurl", value.AbsoluteUri).Commit();
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
        /// LoginHint
        /// </summary>
        public static string LoginHint
        {
            get
            {
                return "cesardl@mycompanydemos.com";
            }
        }

        /// <summary>
        /// Client Id for authentication process
        /// </summary>
        public static string ClientId
        {
            get
            {
                return "ec5bff30-bc31-4de4-8b04-c812a8b27e53";
            }
        }

        /// <summary>
        /// Test mode.
        /// </summary>
        public static bool TestMode
        {
            get
            {             
                return true;
            }
            set
            {
            }
        }

        /// <summary>
        /// Obtained token from authentication.
        /// </summary>
        public static string SecurityToken { get; set; }

        /// <summary>
        /// SecurityToken Expiration DateTime
        /// </summary>
        public static DateTimeOffset SecurityTokenExpirationDateTime
        {
            get
            {
                return DateTime.MinValue;
            }
            set
            {
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
        }

        /// <summary>
        /// update the value of the given key in the local settings.
        /// </summary>
        /// <param name="keyToUpdate"></param>
        /// <param name="value"></param>
        public static void UpdateKeyValue(string keyToUpdate, string value)
        {
        }
    }
}
