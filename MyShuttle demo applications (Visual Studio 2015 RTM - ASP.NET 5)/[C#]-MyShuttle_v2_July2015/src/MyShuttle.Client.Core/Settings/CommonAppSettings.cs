using Microsoft.WindowsAzure.MobileServices;
namespace MyShuttle.Client.Core.Settings
{
    public static class CommonAppSettings
    {
        private readonly static string _MobileServiceUrl = "http://YOUR_SITE.azure-mobile.net/";
        private readonly static string _MobileServiceKey = "YOUR_MOBILE_SERVICE_KEY";

        private static readonly string _SignalRUrl = "http://YOUR_SITE.azurewebsites.net/";

        private static MobileServiceClient mobileService = null;

        private readonly static int _FixedDriverId = 5;
        private readonly static string _FixedEmployeeId = "c7a9c8a2-a5e3-48d4-99e4-4117599af048";

        public static string MobileServiceUrl
        {
            get
            {
                return _MobileServiceUrl;
            }
        }

        public static string SignalRUrl
        {
            get { return _SignalRUrl; }
        }

        public static string MobileServiceKey
        {
            get
            {
                return _MobileServiceKey;
            }
        }

        public static MobileServiceClient MobileService
        {
            get
            {
                if (mobileService == null)
                {
                    mobileService = new MobileServiceClient(CommonAppSettings.MobileServiceUrl,
                        CommonAppSettings.MobileServiceKey);
                }

                return mobileService;
            }
        }

        public static string FixedEmployeeId
        {
            get
            {
                return _FixedEmployeeId;
            }
        }
        public static int FixedDriverId
        {
            get
            {
                return _FixedDriverId;
            }
        }
    }
}
