using GalaSoft.MvvmLight;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Users details view model
    /// </summary>
    public class SessionUserDetailsViewModel : ViewModelBase
    {
        private string _photo;

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User description
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// User score
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// User photo
        /// </summary>
        public string Photo 
        {
            get
            {
                if (GlobalConfig.IsOfflineMode)
                    return "/Assets/UserPhoto.png";
                else
                    return _photo;
            }
            set
            {
                _photo = value;
                RaisePropertyChanged(() => Photo);
            }
        }

        /// <summary>
        /// User rating
        /// </summary>
        public bool Rated { get; set; }
    }
}
