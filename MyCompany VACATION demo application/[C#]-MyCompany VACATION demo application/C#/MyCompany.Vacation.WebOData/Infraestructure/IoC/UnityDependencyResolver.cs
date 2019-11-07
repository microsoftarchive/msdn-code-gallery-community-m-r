namespace MyCompany.Vacation.Web
{
    using Microsoft.Practices.Unity;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.AzureEventBusPlugin;
    using MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Configuration;
    using System.Web.Http.Dependencies;
    using MyCompany.Common.Notification;
    using MyCompany.Vacation.Data.Services;

    /// <summary>
    /// The unity dependency resolver
    /// </summary>
    /// http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
    public class UnityDependencyResolver
        : IDependencyResolver
    {
        #region Members

        IUnityContainer _unityContainer;

        /// <summary>
        /// Container
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                return _unityContainer;
            }
        }

        #endregion

        #region Constructor


        /// <summary>
        /// Create a new instance of Unity Dependency Resolver
        /// </summary>
        public UnityDependencyResolver()
        {
            //create and configure container
            ConfigureContainer();
        }

        #endregion

        #region Private Methods

        void ConfigureContainer()
        {
            _unityContainer = new UnityContainer();

            //register items by convention scanning
            //specific assemblies
            var assembliesToScan = new Assembly[]
            {
                 /*MyCompany.Vacation.Data*/
                typeof(IEmployeeRepository).Assembly,
                /*MyCompany.Vacation.Web*/
                typeof(ISecurityHelper).Assembly,
                /*MyCompany.Common.Notification*/
                typeof(INotificationService).Assembly,
            };

            _unityContainer.RegisterTypes(
                   AllClasses.FromAssemblies(assembliesToScan),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.Transient);


            // Event Bus
            _unityContainer.RegisterType<IEventBus, AzureVacationEventBus>();

            bool eventBusEnabled = false;
            Boolean.TryParse(WebConfigurationManager.AppSettings["EventBusEnabled"], out eventBusEnabled);
            _unityContainer.RegisterType<IVacationRequestRepository, VacationRequestRepository>(new InjectionConstructor(typeof(MyCompanyContext), typeof(IEventBus), typeof(IWorkingDaysCalculator), eventBusEnabled));

            // Notification service
            _unityContainer.RegisterType<IEmailTemplatesRepository, EmailTemplatesInFileRepository>();
            _unityContainer.RegisterType<INotificationService, BaseNotificationService>();

        }

        #endregion

        #region IDependencyResolver Members

        /// <summary>
        /// Creates a new ScopeContainer instance
        /// The Unity container also has the concept of a child container, so we initialize the child ScopeContainer with a Unity child container
        /// This application does not support child scopes, so we simply return 'this'.
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            // This example does not support child scopes, so we simply return 'this'. 
            return this;
        }

        /// <summary>
        /// Dispose method disposes of the Unity child container
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            _unityContainer.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            if ((serviceType.IsClass && !serviceType.IsAbstract) || _unityContainer.IsRegistered(serviceType))
            {
                return _unityContainer.Resolve(serviceType);
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if ((serviceType.IsClass && !serviceType.IsAbstract) || _unityContainer.IsRegistered(serviceType))
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            else
                return Enumerable.Empty<object>();
        }

        #endregion
    }
}