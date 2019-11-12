// <copyright file="MockUnityContainerExtensionConfigurator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockUnityContainerExtensionConfigurator.cs                     
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
    using Microsoft.Practices.Unity;

    public class MockUnityContainerExtensionConfigurator : IUnityContainerExtensionConfigurator
    {
        private readonly IUnityContainer container;

        public MockUnityContainerExtensionConfigurator(IUnityContainer container)
        {
            this.container = container;
        }

        public IUnityContainer Container
        {
            get { return this.container; }
        }
    }
}
