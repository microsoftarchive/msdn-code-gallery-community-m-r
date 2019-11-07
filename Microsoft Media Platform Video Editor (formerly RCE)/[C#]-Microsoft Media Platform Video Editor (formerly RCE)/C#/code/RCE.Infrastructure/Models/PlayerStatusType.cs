// <copyright file="PlayerStatusType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerStatusType.cs                     
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
    public enum PlayerStatusType
    {
        // Represents a ready to play player. Manifest is not needed to be generated
        Ready,

        // Represents a not ready to play player. Manifest needs to be generated
        NotReady,

        // Represents a loading manifest player. Manifest was generated and now media is being opened.
        Loading
    }
}
