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

namespace RCE.Modules.Services.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    public class MockUnityContainer : IUnityContainer
    {
        private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Tuple<string, Type>> mappingInformation = new Dictionary<Type, Tuple<string, Type>>();

        private readonly Dictionary<Type, object> bag = new Dictionary<Type, object>();

        public Dictionary<Type, object> Bag
        {
            get { return this.bag; }
        }

        public Dictionary<Type, Type> Types
        {
            get { return this.types; }
        }

        public Dictionary<Type, Tuple<string, Type>> MappingInformation
        {
            get { return this.mappingInformation; }
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

        public int RegisterTypeWithNameCalls { get; set; }

        public int ResolveCalls { get; set; }

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
            if (string.IsNullOrEmpty(name))
            {
                this.Types.Add(from, to);
            }
            else
            {
                this.MappingInformation.Add(from, new Tuple<string, Type>(name, to));
            }
            
            this.RegisterTypeWithNameCalls++;
            return this;
        }

        IUnityContainer IUnityContainer.RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            throw new System.NotImplementedException();
        }

        public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
        {
            this.ResolveCalls++;
            return this.bag[t];
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
            return null;
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
            return (T)this.bag[typeof(T)];
        }

        public T Resolve<T>()
        {
            return (T)this.bag[typeof(T)];
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