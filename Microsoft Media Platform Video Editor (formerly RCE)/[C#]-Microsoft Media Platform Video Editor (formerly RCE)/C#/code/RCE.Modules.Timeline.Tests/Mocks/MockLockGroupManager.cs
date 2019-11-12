// <copyright file="MockLockGroupManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLockGroupManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;
    using RCE.Modules.Timeline.Locking;

    public class MockLockGroupManager : ILockGroupManager
    {
        IList<LockGroup> ILockGroupManager.LockGroups
        {
            get
            {
                return new List<LockGroup>();
            }
        }

        public IEnumerable<TimelineElement> ElementsLocked { get; set; }

        public TimelineElement ElementUnlocked { get; set; }

        public IEnumerable<TimelineElement> GetGroupedElements(TimelineElement element)
        {
            return new[] { element };
        }

        public void UnlockElementLockGroup(TimelineElement element)
        {
            this.ElementUnlocked = element;
        }

        public int LockElements(IEnumerable<TimelineElement> elements)
        {
            this.ElementsLocked = elements;
            return 0;
        }
    }
}
