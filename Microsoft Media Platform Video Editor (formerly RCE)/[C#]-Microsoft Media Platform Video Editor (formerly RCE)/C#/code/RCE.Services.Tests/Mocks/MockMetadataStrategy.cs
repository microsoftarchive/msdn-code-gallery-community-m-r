// <copyright file="MockMetadataStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMetadataStrategy.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests.Mocks
{
    using System;
    using Contracts;

    public class MockMetadataStrategy : IMetadataStrategy
    {
        public MockMetadataStrategy()
        {
            this.CanRetrieveMetadataFunction = (object target) => false;
            this.GetMetadataFunction = (object target) => null;
        }

        public bool CanRetrieveMetadataCalled { get; set; }

        public object CanRetrieveMetadataArgument { get; set; }

        public Func<object, bool> CanRetrieveMetadataFunction { get; set; }

        public bool GetMetadataCalled { get; set; }

        public object GetMetadataArgument { get; set; }

        public Func<object, Metadata> GetMetadataFunction { get; set; }

        public bool CanRetrieveMetadata(object target)
        {
            this.CanRetrieveMetadataCalled = true;
            this.CanRetrieveMetadataArgument = target;
            return this.CanRetrieveMetadataFunction(target);
        }

        public Metadata GetMetadata(object target)
        {
            this.GetMetadataCalled = true;
            this.GetMetadataArgument = target;
            return this.GetMetadataFunction(target);
        }
    }
}
