namespace MyCompany.Travel.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using Microsoft.Practices.Unity;
    using MyCompany.Travel.Data;
    using MyCompany.Common.Notification;
    using MyCompany.Travel.Web.Notifications;
    using MyCompany.Travel.Web.Security;
    using MyCompany.Travel.Data.Repositories;

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

            _unityContainer.RegisterType<ISecurityHelper, SecurityHelper>();
            _unityContainer.RegisterType<IADGraphApi, ADGraphApi>();

            // Repositories
            _unityContainer.RegisterType<IEmployeeRepository, EmployeeRepository>();
            _unityContainer.RegisterType<ITravelRequestRepository, TravelRequestRepository>();
            _unityContainer.RegisterType<ITravelAttachmentRepository, TravelAttachmentRepository>();
            _unityContainer.RegisterType<ITeamRepository, TeamRepository>();

            // Notification service
            _unityContainer.RegisterType<ITextMerger, TextMerger>();
            _unityContainer.RegisterType<IEmailTemplatesRepository, EmailTemplatesInFileRepository>();
            _unityContainer.RegisterType<IEmailer, Emailer>();
            _unityContainer.RegisterType<INotificationService, BaseNotificationService>();
            _unityContainer.RegisterType<ITravelNotificationService, TravelNotificationService>();

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