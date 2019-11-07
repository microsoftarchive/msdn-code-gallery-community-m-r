// <copyright file="MockUserSettingsService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockUserSettingsService.cs                    
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
    using System;
    using System.Diagnostics.CodeAnalysis;

    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class MockUserSettingsService : IUserSettingsService
    {
        public event EventHandler SettingsChanged;

        public bool LoadSettingsCalled { get; private set; }

        public bool SaveSettingsCalled { get; private set; }

        public UserSettings GetSettings()
        {
            this.LoadSettingsCalled = true;

            return null;
        }

        public void SaveSettings(UserSettings userSettings)
        {
            this.SaveSettingsCalled = true;
        }
    }
}