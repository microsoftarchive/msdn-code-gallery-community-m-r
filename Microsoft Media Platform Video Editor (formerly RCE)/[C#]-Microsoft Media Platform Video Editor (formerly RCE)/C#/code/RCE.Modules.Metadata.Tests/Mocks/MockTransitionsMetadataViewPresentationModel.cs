// <copyright file="MockTransitionsMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTransitionsMetadataViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Tests.Mocks
{
    using System;

    using RCE.Modules.Metadata.Views;

    public class MockTransitionsMetadataViewPresentationModel : ITransitionsMetadataViewPresentationModel
    {
        public MockTransitionsMetadataViewPresentationModel()
        {
            this.View = new MockTransitionsMetadataView { Model = this };
        }

        public ITransitionsMetadataView View { get; set; }
    }
}
