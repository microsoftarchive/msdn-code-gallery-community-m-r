namespace MyCompany.Visitors.Client.WP.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using MyCompany.Visitors.Client.WP.Services.NFC;
    using MyCompany.Visitors.Client.WP.Services.Photo;
    using MyCompany.Visitors.Client.WP.Services.Storage;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IStorageService, StorageService>(true);
            SimpleIoc.Default.Register<IPhotoService, PhotoService>(true);
            SimpleIoc.Default.Register<INfcService, NfcService>(true);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// Main viewmodel
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}