// <copyright file="WindowsMediaHeaderProperties.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WindowsMediaHeaderProperties.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts.Output
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class WindowsMediaHeaderProperties
    {
        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public string Copyright { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public string Genre { get; set; }
    }
}