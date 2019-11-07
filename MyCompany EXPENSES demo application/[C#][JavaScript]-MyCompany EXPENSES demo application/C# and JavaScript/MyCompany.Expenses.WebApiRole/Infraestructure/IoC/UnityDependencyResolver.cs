namespace MyCompany.Expenses.WebApiRole
{
    using Microsoft.Practices.Unity;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.EventBusPlugin;
    using MyCompany.Expenses.WebApiRole.Services;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Dependencies;

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
                 /*MyCompany.Expenses.Data*/
                typeof(IEmployeeRepository).Assembly,
                /*MyCompany.Expenses.WebApiRole*/
                typeof(ISecurityHelper).Assembly,
            };

            _unityContainer.RegisterTypes(
                   AllClasses.FromAssemblies(assembliesToScan),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.Transient);

            // Event Bus
            _unityContainer.RegisterType<IEventBus, AzureExpensesEventBus>();

            //bool eventBusEnabled = false;
            //Boolean.TryParse(RoleEnvironment.GetConfigurationSettingValue("EventBusEnabled"), out eventBusEnabled);
            //_unityContainer.RegisterType<IExpenseRepository, ExpenseRepository>(new InjectionConstructor(typeof(IEventBus), eventBusEnabled, typeof(MyCompanyContext)));

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