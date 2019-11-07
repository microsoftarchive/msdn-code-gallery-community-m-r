// <copyright file="MockUnityContainer.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockUnityContainer.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;

    public class MockUnityContainer : IUnityContainer
    {
        private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();

        public Dictionary<Type, Type> Types
        {
            get { return this.types; }
        }

        public IUnityContainer Parent
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<ContainerRegistration> Registrations
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IUnityContainer RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            this.Types.Add(typeof(TFrom), typeof(TTo));
            return this;
        }

        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager) where TTo : TFrom
        {
            this.Types.Add(typeof(TFrom), typeof(TTo));
            return this;
        }

        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            return this.RegisterType<TFrom, TTo>(lifetimeManager);
        }

        public IUnityContainer RegisterType<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            return this.RegisterType<TFrom, TTo>();
        }

        IUnityContainer IUnityContainer.RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            this.Types.Add(from, to);
            return this;
        }

        IUnityContainer IUnityContainer.RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            throw new System.NotImplementedException();
        }

        public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer AddExtension(UnityContainerExtension extension)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer AddNewExtension<TExtension>() where TExtension : UnityContainerExtension, new()
        {
            throw new NotImplementedException();
        }

        public object BuildUp(Type t, object existing, string name)
        {
            throw new NotImplementedException();
        }

        public object BuildUp(Type t, object existing)
        {
            throw new NotImplementedException();
        }

        public T BuildUp<T>(T existing, string name)
        {
            throw new NotImplementedException();
        }

        public T BuildUp<T>(T existing)
        {
            throw new NotImplementedException();
        }

        public object Configure(Type configurationInterface)
        {
            throw new NotImplementedException();
        }

        public TConfigurator Configure<TConfigurator>() where TConfigurator : IUnityContainerExtensionConfigurator
        {
            throw new NotImplementedException();
        }

        public IUnityContainer CreateChildContainer()
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, string name, object instance)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, object instance, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, object instance)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(string name, TInterface instance, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(string name, TInterface instance)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(TInterface instance, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(TInterface instance)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type t, string name, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type t, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, string name)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType<T>(string name, LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType<TFrom, TTo>(string name, LifetimeManager lifetimeManager) where TTo : TFrom
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType<TFrom, TTo>(string name) where TTo : TFrom
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RemoveAllExtensions()
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type t, string name)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type t)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>(string name)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            throw new NotImplementedException();
        }

        public void Teardown(object o)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
