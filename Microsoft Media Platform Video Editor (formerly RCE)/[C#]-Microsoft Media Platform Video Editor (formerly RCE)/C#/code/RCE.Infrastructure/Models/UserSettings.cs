// <copyright file="UserSettings.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UserSettings.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
namespace RCE.Infrastructure.Models
{
    using System;

    public class UserSettings
    {
        public long MinBitrate { get; set; }

        public long MaxBitrate { get; set; }

        public bool IsSingleBitrate { get; set; }

        public bool TreatGapAsError { get; set; }

        public KeyboardMapping KeyboardMapping { get; set; }
    }
}
