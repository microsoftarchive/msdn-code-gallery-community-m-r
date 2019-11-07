// <copyright file="MockSettingsMenuButtonViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSettingsMenuButtonViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Mocks
{
    using RCE.Infrastructure.Menu;

    public class MockSettingsMenuButtonViewModel : IMenuButtonViewModel
    {
        public IMenuButtonView View { get; set; }

        public object ViewToDisplay { get; set; }

        public bool IsViewActive { get; set; }

        public string Text { get; set; }
    }
}
