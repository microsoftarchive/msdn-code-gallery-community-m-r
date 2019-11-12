// <copyright file="MockSequencesViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSequencesViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using System;

    using RCE.Modules.Timeline.Views;

    public class MockSequencesViewModel : ISequencesViewModel
    {
        private object view = new MockSequencesView();

        public object View
        {
            get
            {
                return this.view;
            }
        }

        public string HeaderInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
