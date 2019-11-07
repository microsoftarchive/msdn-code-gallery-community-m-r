// <copyright file="PlayByPlay.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlay.cs                     
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

    using RCE.Services.Contracts;

    public class PlayByPlay : Marker
    {
        private readonly long offset;
        
        public PlayByPlay(long offset)
        {
            this.offset = offset;
        }

        public long TimeWithOffset 
        { 
            get
            {
                return this.Time + this.offset;
            }
        }

        public Guid TimelineId { get; set; }
    }
}
