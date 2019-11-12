// <copyright file="ISequenceRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISequenceRegistry.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;

    public interface ISequenceRegistry
    {
        event EventHandler<DataEventArgs<ISequenceModel>> CurrentSequenceChanged;

        ISequenceModel CurrentSequenceModel { get; set; }

        Sequence CurrentSequence { get; }

        IEnumerable<ISequenceModel> Sequences { get; }
        
        ISequenceModel CreateSequence(Sequence sequence);

        ISequenceModel GetSequenceForTimeline(Sequence sequence);
    }
}
