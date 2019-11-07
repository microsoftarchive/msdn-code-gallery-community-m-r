using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac;
using SampleArch.Modules;
using System.Data.Entity;
using SampleArch.Model;

namespace SampleArch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Autofac Configuration
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();


            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            //
            Database.SetInitializer<SampleArchContext>(null);
        }
    }
}
