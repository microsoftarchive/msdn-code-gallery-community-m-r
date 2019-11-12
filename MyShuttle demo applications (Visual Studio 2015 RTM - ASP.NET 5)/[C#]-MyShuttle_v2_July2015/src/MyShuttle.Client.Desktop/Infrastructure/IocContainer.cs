using Autofac;
using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.Core.ServiceAgents;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.Desktop.Infrastructure;
using MyShuttle.Client.Desktop.ViewModels;
using MyShuttle.Client.Desktop.Views;

namespace MyShuttle.Client.Desktop
{
    public static class IocContainer
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<StatisticsViewModel>();
            builder.RegisterType<DetailsViewModel>();

            builder.RegisterType<MainView>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<CameraView>();
            builder.RegisterType<CameraViewModel>();

            builder.RegisterType<MvxMessengerHub>().As<IMvxMessenger>().SingleInstance();
            builder.RegisterType<ApplicationDataRepository>().As<IApplicationDataRepository>();

            builder.RegisterAssemblyTypes(typeof(MyShuttleClient).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(MyShuttleClient).Assembly)
                .Where(t => t.Name.EndsWith("ServiceSingleton"))
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<MyShuttleClient>().As<IMyShuttleClient>();

            return builder.Build();
        }
    }
}
