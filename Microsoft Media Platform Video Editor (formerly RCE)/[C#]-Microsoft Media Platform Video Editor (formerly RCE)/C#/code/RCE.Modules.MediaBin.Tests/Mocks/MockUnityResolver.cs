// <copyright file="MockUnityResolver.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockUnityResolver.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;

    public class MockUnityResolver : IUnityContainer
    {
        private readonly Dictionary<Type, object> bag = new Dictionary<Type, object>();

        public Dictionary<Type, object> Bag
        {
            get { return this.bag; }
        }

        IUnityContainer IUnityContainer.Parent
        {
            get { throw new System.NotImplementedException(); }
        }

        public IEnumerable<ContainerRegistration> Registrations
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T Resolve<T>()
        {
            return (T)this.bag[typeof(T)];
        }

        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager) where TTo : TFrom
        {
            // ignore
            return this;
        }

        public IUnityContainer RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            // ignore
            return this;
        }

        IUnityContainer IUnityContainer.RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            return this;
        }

        IUnityContainer IUnityContainer.RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            throw new System.NotImplementedException();
        }

        public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
        {
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

        void IUnityContainer.Teardown(object o)
        {
            throw new System.NotImplementedException();
        }

        IUnityContainer IUnityContainer.AddExtension(UnityContainerExtension extension)
        {
            throw new System.NotImplementedException();
        }

        object IUnityContainer.Configure(Type configurationInterface)
        {
            throw new System.NotImplementedException();
        }

        IUnityContainer IUnityContainer.RemoveAllExtensions()
        {
            throw new System.NotImplementedException();
        }

        IUnityContainer IUnityContainer.CreateChildContainer()
        {
            throw new System.NotImplementedException();
        }
       
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}