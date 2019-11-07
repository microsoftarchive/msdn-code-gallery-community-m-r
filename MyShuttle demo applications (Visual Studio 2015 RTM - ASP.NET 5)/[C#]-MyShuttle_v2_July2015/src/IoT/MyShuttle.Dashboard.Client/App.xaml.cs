using Windows.ApplicationModel.Resources;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;

namespace MyShuttle.Dashboard.Client
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Unity;
    using Repositories;
    using Repositories.Abstract;
    using Services;
    using Services.Abstract;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : MvvmAppBase
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public App()
        {
            this.InitializeComponent();
            this.RequestedTheme = ApplicationTheme.Dark;
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("MainHub", null);
            return Task.FromResult<object>(null);
        }

        protected override object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            _container.RegisterInstance(NavigationService);
            _container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());
            _container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));

            // Register repositories
            _container.RegisterType<IDriversRepository, DriversRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IVehiclesRepository, VehiclesRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IServicesRepository, ServicesRepository>(new ContainerControlledLifetimeManager());

            // Register web service proxies
            _container.RegisterType<IDriversService, DriversServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IVehiclesService, VehiclesServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IServicesService, ServicesServiceProxy>(new ContainerControlledLifetimeManager());
            
            return base.OnInitializeAsync(args);
        }
    }
}
