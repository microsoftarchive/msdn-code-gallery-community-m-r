// <copyright file="MockProjectMenuButtonView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockProjectMenuButtonView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects.Tests.Mocks
{
    using RCE.Infrastructure.Menu;

    public class MockProjectMenuButtonView : IMenuButtonView
    {
        public object ViewModelArgument { get; set; }

        public void SetViewModel(object viewModel)
        {
            this.ViewModelArgument = viewModel;
        }
    }
}
