using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ServiceAgents;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.Core.ViewModels;
using MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection;

namespace MyShuttle.Client.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            // Register the storage provider
            Mvx.RegisterType<IMyShuttleClient, MyShuttleClient>();

            Mvx.RegisterType<IVehiclesByDistanceViewModel, VehiclesByDistanceViewModel>();
            Mvx.RegisterType<IVehiclesByPriceViewModel, VehiclesByPriceViewModel>();
            Mvx.RegisterType<IVehiclesInMapViewModel, VehiclesInMapViewModel>();
            Mvx.RegisterType<IMyRidesViewModel, MyRidesViewModel>();
            Mvx.RegisterType<ICompanyRidesViewModel, CompanyRidesViewModel>();
            Mvx.RegisterType<ISettingsViewModel, SettingsViewModel>();

            // Services
            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsDynamic();

            // Singleton services
            CreatableTypes().EndingWith("ServiceSingleton").AsInterfaces().RegisterAsLazySingleton();

            // Set the start point
            RegisterAppStart<MainViewModel>();

        }
    }
}