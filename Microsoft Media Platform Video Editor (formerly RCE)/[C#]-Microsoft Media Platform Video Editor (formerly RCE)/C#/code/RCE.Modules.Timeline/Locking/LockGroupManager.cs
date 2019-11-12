// <copyright file="LockGroupManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LockGroupManager.cs                     
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
    using System.Linq;

    using RCE.Infrastructure.Models;

    public class LockGroupManager : ILockGroupManager
    {
        private readonly IList<LockGroup> lockGroups;

        private readonly IDictionary<TimelineElement, LockGroup> elementLockGroups;

        private readonly IList<int> availableIds;

        public LockGroupManager()
        {
            this.lockGroups = new List<LockGroup>();
            this.availableIds = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            this.elementLockGroups = new Dictionary<TimelineElement, LockGroup>();
        }

        public IList<LockGroup> LockGroups
        {
            get
            {
                return this.lockGroups;
            }
        }

        public int LockElements(IEnumerable<TimelineElement> elements)
        {
            if (elements.Count() < 2)
            {
                return -1;
            }

            LockGroup group = new LockGroup();
            group.Id = this.availableIds.Min();

            foreach (var timelineElement in elements)
            {
                if (this.elementLockGroups.ContainsKey(timelineElement))
                {
                    var elementGroup = this.elementLockGroups[timelineElement];

                    foreach (var element in elementGroup)
                    {
                        if (element != timelineElement)
                        {
                            this.elementLockGroups[element] = group;
                        }
                    }

                    group.UnionWith(elementGroup);
                    this.lockGroups.Remove(elementGroup);
                    this.availableIds.Add(elementGroup.Id);
                }
                else
                {
                    group.Add(timelineElement);
                }

                this.elementLockGroups[timelineElement] = group;
            }

            group.Id = this.availableIds.Min();
            this.lockGroups.Add(group);
            this.availableIds.Remove(group.Id);

            return group.Id;
        }

        public IEnumerable<TimelineElement> GetGroupedElements(TimelineElement element)
        {
            if (!this.elementLockGroups.ContainsKey(element))
            {
                return new[] { element };
            }

            return this.elementLockGroups[element];
        }

        public void UnlockElementLockGroup(TimelineElement element)
        {
            if (this.elementLockGroups.ContainsKey(element))
            {
                LockGroup group = this.elementLockGroups[element];
                foreach (var groupedElement in group)
                {
                    this.elementLockGroups.Remove(groupedElement);
                }

                this.lockGroups.Remove(group);
                this.availableIds.Add(group.Id);
           }
        }
    }
}
