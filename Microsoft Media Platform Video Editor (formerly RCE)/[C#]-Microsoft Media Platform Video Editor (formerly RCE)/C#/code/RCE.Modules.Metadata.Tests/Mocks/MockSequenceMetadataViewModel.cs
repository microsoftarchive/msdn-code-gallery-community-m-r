// <copyright file="MockSequenceMetadataViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSequenceMetadataViewModel.cs                     
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
    using RCE.Modules.Metadata.Views;

    public class MockSequenceMetadataViewModel : ISequenceMetadataViewModel
    {
        private ISequenceMetadataView view = new MockSequenceMetadataView();

        /// <summary>
        /// Gets or sets the value of the [View] as ISequenceMetadataView
        /// </summary>
        public ISequenceMetadataView View
        {
            get
            {
                return this.view;
            }

            set
            {
                this.view = value;
            }
        }
    }
}
