namespace MyCompany.Travel.Client.Desktop.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using MyCompany.Travel.Client.Desktop.Services.Navigation;
    using MyCompany.Travel.Client.Desktop.Services.SampleData;
    using MyCompany.Travel.Client.Desktop.Services.Security;
    using System;

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
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<INavigationService, NavigationService>(true);
            SimpleIoc.Default.Register<ISampleDataService, SampleDataService>(true);
            SimpleIoc.Default.Register<IMyCompanyClient>(() =>
            {
                if (ViewModelBase.IsInDesignModeStatic)
                    return null;

                return new MyCompanyClient(SecurityService.UrlBase, SecurityService.AccessToken);
            });

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TravelListViewModel>();
            SimpleIoc.Default.Register<TravelRequestFormViewModel>();
        }

        /// <summary>
        /// Main Window ViewModel
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Travel list ViewModel
        /// </summary>
        public TravelListViewModel TravelListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TravelListViewModel>();
            }
        }

        /// <summary>
        /// Travel request form view model
        /// </summary>
        public TravelRequestFormViewModel TravelRequestVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TravelRequestFormViewModel>();
            }
        }
    }
}