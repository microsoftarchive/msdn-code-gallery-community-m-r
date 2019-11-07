// <copyright file="VolumeLevelNode.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VolumeLevelNode.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class VolumeLevelNode
    {
        [DataMember]
        public double Position { get; set; }

        [DataMember]
        public double Volume { get; set; }
    }
}
