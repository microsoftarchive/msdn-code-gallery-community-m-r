using Microsoft.Practices.Unity;
using MyShuttle.API.Data;
using MyShuttle.API.Data.DocumentDb;
using MyShuttle.API.Data.DocumentDb.Factories;
using MyShuttle.API.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API
{
    public static class UnityRegistration
    {
        public static void RegisterTypes(IUnityContainer container, IDictionary<string, string> appSettings)
        {

            container.RegisterType<MyShuttleDashboardContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDriverQueryFactory, DriverFactoryQuery>(new TransientLifetimeManager());

            container.RegisterType<DocumentDbContext>(new HierarchicalLifetimeManager(),
                new InjectionConstructor(appSettings["DocumentDb:EndpointUrl"],
                appSettings["DocumentDb:AccessKey"], 
                appSettings["DocumentDb:DatabaseId"]));

            container.RegisterType<IDrivingStyleDocumentsFactory, DrivingStyleDocumentsFactory>(new TransientLifetimeManager());
            container.RegisterType<IVehicleDocumentsFactory, VehicleDocumentsFactory>(new TransientLifetimeManager());
            container.RegisterType<IRideFactoryQuery, RideFactoryQuery>(new TransientLifetimeManager());
            container.RegisterType<ITrackedRidesDocumentsFactory, TrackedRidesDocumentsFactory>(new TransientLifetimeManager());
        }
    }
}
