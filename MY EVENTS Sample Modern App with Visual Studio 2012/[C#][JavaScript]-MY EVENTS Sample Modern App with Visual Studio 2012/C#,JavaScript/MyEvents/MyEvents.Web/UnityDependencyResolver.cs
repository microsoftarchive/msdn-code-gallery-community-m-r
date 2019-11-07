using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MyEvents.Data;
using MyEvents.Web.Services;
using MyEvents.Web.LifetimeManagers;


namespace MyEvents.Web
{
    /// <summary>
    /// The unity dependency resolver
    /// </summary>
    public class UnityDependencyResolver
        : IDependencyResolver, IDisposable
    {
        IUnityContainer _container;

        /// <summary>
        /// Create a new instance of Unity Dependency Resolver
        /// </summary>
        public UnityDependencyResolver(IUnityContainer unityContainer)
        {
            //create and configure container
            _container = unityContainer;
            ConfigureContainer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            if ((serviceType.IsClass && !serviceType.IsAbstract) || _container.IsRegistered(serviceType))
            {
                return _container.Resolve(serviceType);
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
            if ((serviceType.IsClass && !serviceType.IsAbstract) || _container.IsRegistered(serviceType))
            {
                return _container.ResolveAll(serviceType);
            }
            else
                return Enumerable.Empty<object>();
        }

        /// <summary>
        /// Dispose method disposes of the Unity child container
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        private void ConfigureContainer()
        {
            _container.RegisterType<IRegisteredUserRepository, RegisteredUserRepository>();
            _container.RegisterType<IEventDefinitionRepository, EventDefinitionRepository>();
            _container.RegisterType<IMaterialRepository, MaterialRepository>();
            _container.RegisterType<ISessionRepository, SessionRepository>();
            _container.RegisterType<ICommentRepository, CommentRepository>();
            _container.RegisterType<IAuthenticationService, AuthenticationService>();
            _container.RegisterType<IAuthorizationService, AuthorizationService>();
            _container.RegisterType<IThirdPartyOauthService, OfflineOauthService>("Offline");
            _container.RegisterType<IThirdPartyOauthService, FacebookOauthService>("Online");

            _container.RegisterType<IThirdPartyOauthService>(
                new InjectionFactory(c =>
                {
                    bool isOfflineMode = bool.Parse(ConfigurationManager.AppSettings["OfflineMode"]);
                    return _container.Resolve<IThirdPartyOauthService>(isOfflineMode ? "Offline" : "Online");
                }));
        }

        private void Dispose(bool disposing)
        {
            _container.Dispose();
        }
    }
}