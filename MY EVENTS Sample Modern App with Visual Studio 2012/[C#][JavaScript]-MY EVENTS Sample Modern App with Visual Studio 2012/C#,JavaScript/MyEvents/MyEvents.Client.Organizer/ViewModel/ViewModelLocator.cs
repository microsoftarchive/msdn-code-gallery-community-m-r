using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Services.Navigation;
using MyEvents.Client.Organizer.Services.Twitter;
using MyEvents.Client.Organizer.Services.UserInterface;

namespace MyEvents.Client.Organizer.ViewModel
{
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

            Register();
        }

        /// <summary>
        /// Returns the registered instance of our MainViewModel class.
        /// </summary>
        public object Main
        {
            get
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<MainViewModel>();
                else
                    return new MainViewModelFake();
            }
        }

        /// <summary>
        /// Returns the registered instance of our EventDetailViewModel class.
        /// </summary>
        public object EventDetail
        {
            get
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<EventDetailViewModel>();
                else
                    return new EventDetailViewModelFake();
            }
        }

        /// <summary>
        /// Returns the registered instance of our SessionDetailViewModel class.
        /// </summary>
        public object SessionDetail
        {
            get
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<SessionDetailViewModel>();
                else
                    return new SessionDetailViewModelFake();                
            }
        }

        /// <summary>
        /// Return the registered instance of our SplashScreen class.
        /// </summary>
        public SplashScreenViewModel SplashScreen
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SplashScreenViewModel>();
            }
        }

        /// <summary>
        /// Return the registered instance of our AppSettings class.
        /// </summary>
        public AppSettingsViewModel AppSettings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AppSettingsViewModel>();
            }
        }

        /// <summary>
        /// View model for the user information.
        /// </summary>
        public object UserInformation
        {
            get
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<UserInformationViewModel>();
                else
                    return new UserInformationViewModelFake();
            }
        }

        /// <summary>
        /// Register all viewmodels and dependencies with SimpleIoC.
        /// </summary>
        public static void Register()
        {
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EventDetailViewModel>();
            SimpleIoc.Default.Register<SessionDetailViewModel>();
            SimpleIoc.Default.Register<SplashScreenViewModel>();
            SimpleIoc.Default.Register<AppSettingsViewModel>();
            SimpleIoc.Default.Register<UserInformationViewModel>();

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
                SimpleIoc.Default.Register<INavigationService, NavigationService>();

            if (!SimpleIoc.Default.IsRegistered<ITwitterService>())
                SimpleIoc.Default.Register<ITwitterService, TwitterService>();

            if (!SimpleIoc.Default.IsRegistered<IMyEventsClient>())
                SimpleIoc.Default.Register<IMyEventsClient>(() => new MyEventsClient(GlobalConfig.ApiUrlPrefix));

            if (!SimpleIoc.Default.IsRegistered<IUIService>())
                SimpleIoc.Default.Register<IUIService, UIService>();
        }
    }
}