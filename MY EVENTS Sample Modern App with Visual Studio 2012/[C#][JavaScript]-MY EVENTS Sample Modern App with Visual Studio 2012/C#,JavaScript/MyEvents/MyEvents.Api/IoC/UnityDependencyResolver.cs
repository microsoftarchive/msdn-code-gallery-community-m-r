using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using MyEvents.Api.Authentication;
using MyEvents.Api.Controllers;
using MyEvents.Data;

namespace MyEvents.Api.IoC
{
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

            // controllers
            _unityContainer.RegisterType<RegisteredUsersController>();
            _unityContainer.RegisterType<EventDefinitionsController>();
            _unityContainer.RegisterType<SessionsController>();
            _unityContainer.RegisterType<TagsController>();
            _unityContainer.RegisterType<SpeakersController>();
            _unityContainer.RegisterType<AuthenticationController>();
            _unityContainer.RegisterType<MaterialsController>();
            _unityContainer.RegisterType<CommentsController>();
            _unityContainer.RegisterType<RoomPointsController>();

            // Repositories
            _unityContainer.RegisterType<IRegisteredUserRepository, RegisteredUserRepository>();
            _unityContainer.RegisterType<IEventDefinitionRepository, EventDefinitionRepository>();
            _unityContainer.RegisterType<IMaterialRepository, MaterialRepository>();
            _unityContainer.RegisterType<ISessionRepository, SessionRepository>();
            _unityContainer.RegisterType<ICommentRepository, CommentRepository>();

            // Service
            _unityContainer.RegisterType<IFacebookService, FacebookService>();
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