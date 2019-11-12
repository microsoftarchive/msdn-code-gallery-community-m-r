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
    public class UserInformationViewModelFake
    {
        /// <summary>
        /// Current loged user full name
        /// </summary>
        public string UserName
        {
            get
            {
                return "Orville McDonald";
            }
        }

        /// <summary>
        /// Current loged user photo
        /// </summary>
        public string UserPhoto
        {
            get
            {
                return string.Format("https://graph.facebook.com/{0}/picture", "100004295251408");
            }
        }
    }
}
