using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Services.Navigation;
using System;
using System.Windows.Input;
using Windows.Storage;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// User information viewmodel
    /// </summary>
    public class UserInformationViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navService"></param>
        public UserInformationViewModel()
        {
        }

        /// <summary>
        /// Current loged user full name
        /// </summary>
        public string UserName
        {
            get
            {
                return UserCredentials.Current.CurrentUser.FullName;
            }
        }

        /// <summary>
        /// Current loged user photo
        /// </summary>
        public string UserPhoto
        {
            get
            {
                if (GlobalConfig.IsOfflineMode)
                    return "/Assets/UserPhoto.png";

                return string.Format("https://graph.facebook.com/{0}/picture", UserCredentials.Current.CurrentUser.FacebookId);
            }
        }
    }
}
