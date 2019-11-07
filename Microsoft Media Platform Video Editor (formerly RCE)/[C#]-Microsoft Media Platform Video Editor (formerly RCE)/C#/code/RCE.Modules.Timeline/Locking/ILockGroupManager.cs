// <copyright file="ILockGroupManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ILockGroupManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Locking
{
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;

    public interface ILockGroupManager
    {
        IList<LockGroup> LockGroups { get; }

        IEnumerable<TimelineElement> GetGroupedElements(TimelineElement element);

        void UnlockElementLockGroup(TimelineElement element);

        int LockElements(IEnumerable<TimelineElement> elements);
    }
}
