/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MyEvents.Client.Organizer.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Configuration;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Facebook;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
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
        /// Event details viewmodel instance.
        /// </summary>
        public EventDetailsViewModel EventDetails
        {
            get
            {
                return SimpleIoc.Default.GetInstance<EventDetailsViewModel>();
            }
        }

        /// <summary>
        /// Event schedule viewmodel instance.
        /// </summary>
        public EventScheduleViewModel EventSchedule
        {
            get
            {
                return SimpleIoc.Default.GetInstance<EventScheduleViewModel>();
            }
        }

        /// <summary>
        /// Main desktop viewmodel instance.
        /// </summary>
        public MainDesktopViewModel MainDesktop
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainDesktopViewModel>();
            }
        }

        /// <summary>
        /// Main page viewmodel instance.
        /// </summary>
        public MainPageViewModel MainPage
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainPageViewModel>();
            }
        }

        /// <summary>
        /// Manage events viewmodel instance.
        /// </summary>
        public ManageEventsViewModel ManageEvents
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ManageEventsViewModel>();
            }
        }

        /// <summary>
        /// Manage map viewmodel instance.
        /// </summary>
        public ManageMapViewModel ManageMap
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ManageMapViewModel>();
            }
        }

        /// <summary>
        /// Manage sessions viewmodel instance.
        /// </summary>
        public ManageSessionsViewModel ManageSessions
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ManageSessionsViewModel>();
            }
        }

        /// <summary>
        /// Registered users viewmodel instance.
        /// </summary>
        public RegisteredUsersViewModel RegisteredUsers
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RegisteredUsersViewModel>();
            }
        }

        /// <summary>
        /// Edit session details viewmodel instance.
        /// </summary>
        public EditSessionDetailsViewModel EditSessionDetails
        {
            get
            {
                return SimpleIoc.Default.GetInstance<EditSessionDetailsViewModel>();
            }
        }

        /// <summary>
        /// Upload materials view model instance.
        /// </summary>
        public UploadMaterialViewModel UploadMaterial
        {
            get
            {
                return SimpleIoc.Default.GetInstance<UploadMaterialViewModel>();
            }
        }

        /// <summary>
        /// Register all viewmodels and dependencies with SimpleIoC.
        /// </summary>
        public static void Register()
        {
            SimpleIoc.Default.Reset();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<EventDetailsViewModel>();
            SimpleIoc.Default.Register<EventScheduleViewModel>();
            SimpleIoc.Default.Register<MainDesktopViewModel>();
            SimpleIoc.Default.Register<ManageEventsViewModel>();
            SimpleIoc.Default.Register<ManageMapViewModel>();
            SimpleIoc.Default.Register<ManageSessionsViewModel>();
            SimpleIoc.Default.Register<RegisteredUsersViewModel>();
            SimpleIoc.Default.Register<EditSessionDetailsViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<UploadMaterialViewModel>();
            if (!SimpleIoc.Default.IsRegistered<IMyEventsClient>())
                SimpleIoc.Default.Register<IMyEventsClient>(() => new MyEventsClient(ConfigurationManager.AppSettings.Get("ServicesAddress")));
            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
                SimpleIoc.Default.Register<INavigationService, NavigationService>();
            if (!SimpleIoc.Default.IsRegistered<IFacebookApi>())
                SimpleIoc.Default.Register<IFacebookApi>(() => new FacebookApi(ConfigurationManager.AppSettings.Get("FacebookAppId"), ConfigurationManager.AppSettings.Get("FacebookAppPermissions")));
        }

    }
}