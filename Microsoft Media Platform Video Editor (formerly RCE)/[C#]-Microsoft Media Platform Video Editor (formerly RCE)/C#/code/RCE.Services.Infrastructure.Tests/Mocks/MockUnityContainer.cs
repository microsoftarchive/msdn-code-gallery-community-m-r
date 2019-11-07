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

namespace RCE.Services.Infrastructure.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;

    public class MockUnityContainer : IUnityContainer
    {
        public bool ResolveCalled { get; set; }

        public bool TeardownCalled { get; set; }

        public object TeardownArgument { get; set; }

        public IUnityContainer Parent
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public object Resolve(Type t)
        {
            this.ResolveCalled = true;

            return null;
        }

        public void Teardown(object o)
        {
            this.TeardownCalled = true;
            this.TeardownArgument = o;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<T>(params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(TInterface instance)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(TInterface instance, LifetimeManager lifetimeManager)
        {
            throw new System.NotImplementedException();
        }

        public T Resolve<T>()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public T BuildUp<T>(T existing)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer AddExtension(UnityContainerExtension extension)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer AddNewExtension<TExtension>() where TExtension : UnityContainerExtension, new()
        {
            throw new System.NotImplementedException();
        }

        public TConfigurator Configure<TConfigurator>() where TConfigurator : IUnityContainerExtensionConfigurator
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RemoveAllExtensions()
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer CreateChildContainer()
        {
            throw new System.NotImplementedException();
        }

        public object Configure(Type configurationInterface)
        {
            throw new System.NotImplementedException();
        }

        public object BuildUp(Type t, object existing, string name)
        {
            throw new System.NotImplementedException();
        }

        public object BuildUp(Type t, object existing)
        {
            throw new System.NotImplementedException();
        }

        public T BuildUp<T>(T existing, string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            throw new System.NotImplementedException();
        }

        public object Resolve(Type t, string name)
        {
            throw new System.NotImplementedException();
        }

        public T Resolve<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, string name, object instance)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, object instance, LifetimeManager lifetimeManager)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type t, object instance)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(string name, TInterface instance, LifetimeManager lifetimeManager)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterInstance<TInterface>(string name, TInterface instance)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type t, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type t, string name, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type t, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, string name, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType(Type t, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<T>(string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<T>(string name, params InjectionMember[] injectionMembers)
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<TFrom, TTo>(string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            throw new System.NotImplementedException();
        }

        public IUnityContainer RegisterType<TFrom, TTo>(string name, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            throw new System.NotImplementedException();
        }
    }
}