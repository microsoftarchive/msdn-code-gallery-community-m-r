using Microsoft.Practices.Unity;
using System.Configuration;
using System.Globalization;
using System;
using MyEvents.Domain.EventDefinitions;
using MyEvents.Domain.Sessions;
using MyEvents.Data;
using MyEvents.Domain.RegisteredUsers;
using System.Collections.Generic;

namespace MyEvents.IoC
{
    /// <summary>
    /// DI container accesor
    /// </summary>
    public class Container : IContainer
    {
        #region Properties

        static IUnityContainer currentContainer;

        /// <summary>
        /// Get the current configured container
        /// </summary>
        /// <returns>Configured container</returns>
        public static IUnityContainer Current
        {
            get
            {
                return currentContainer;
            }
        }

        #endregion

        #region Constructor

        static Container()
        {
            ConfigureContainer();
        }

        #endregion

        #region Methods

        static void ConfigureContainer()
        {
            currentContainer = new UnityContainer();

            // Domain Layer
            currentContainer.RegisterType<IEventDefinitionService, EventDefinitionService>();
            currentContainer.RegisterType<ISessionService, SessionService>();
            currentContainer.RegisterType<IRegisteredUserService, RegisteredUserService>();

            // Repositories
            currentContainer.RegisterType<IRegisteredUserRepository, RegisteredUserRepository>();
            currentContainer.RegisterType<IEventDefinitionRepository, EventDefinitionRepository>();
            currentContainer.RegisterType<IMaterialRepository, MaterialRepository>();
            currentContainer.RegisterType<ISessionRepository, SessionRepository>();
            currentContainer.RegisterType<ICommentRepository, CommentRepository>();
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Resolve<TService>()
        {
            return currentContainer.Resolve<TService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return currentContainer.Resolve(type, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll(Type type)
        {
            return currentContainer.ResolveAll(type, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void RegisterType(Type type)
        {
            currentContainer.RegisterType(type, new TransientLifetimeManager());
        }

        #endregion
    }
}