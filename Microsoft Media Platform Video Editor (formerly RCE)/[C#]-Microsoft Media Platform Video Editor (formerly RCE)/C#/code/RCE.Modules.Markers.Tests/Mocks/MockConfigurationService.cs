// <copyright file="MockConfigurationService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockConfigurationService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Infrastructure;

    using RCE.Infrastructure.Services;

    public class MockConfigurationService : IConfigurationService
    {
        public MockConfigurationService()
        {
            this.GetParameterValueReturnFunction = (parameter) => null;
        }

        public event EventHandler ConfigurationChanged;

        public Func<string, string> GetParameterValueReturnFunction { get; set; }

        public string GetParameterValueArgument { get; set; }

        public bool GetParameterValueCalled { get; set; }

        public string GetParameterValue(string parameter)
        {
            this.GetParameterValueCalled = true;
            this.GetParameterValueArgument = parameter;
            return this.GetParameterValueReturnFunction.Invoke(parameter);
        }

        public void UpdateParameters(IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}