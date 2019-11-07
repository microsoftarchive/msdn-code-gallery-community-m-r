// <copyright file="GroupLockManagerFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: GroupLockManagerFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Locking
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Models;
    using RCE.Modules.Timeline.Locking;

    [TestClass]
    public class GroupLockManagerFixture
    {
        [TestMethod]
        public void ShouldAddElementToLockGroupWhenAdded()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();
            var element3 = new TimelineElement();

            var elements = new List<TimelineElement> { element1, element2, element3 };

            Assert.AreEqual(0, manager.LockGroups.Count);

            manager.LockElements(elements);

            Assert.AreEqual(1, manager.LockGroups.Count);
            Assert.AreEqual(3, manager.LockGroups[0].Count);
            Assert.IsTrue(manager.LockGroups[0].Contains(element1));
            Assert.IsTrue(manager.LockGroups[0].Contains(element2));
            Assert.IsTrue(manager.LockGroups[0].Contains(element3));
        }

        [TestMethod]
        public void ShouldAddElementsToOtherGroupIfNoElementsAreInCommon()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();
            
            var element3 = new TimelineElement();
            var element4 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };

            Assert.AreEqual(0, manager.LockGroups.Count);

            manager.LockElements(group1);
            manager.LockElements(group2);

            Assert.AreEqual(2, manager.LockGroups.Count);
            Assert.AreEqual(2, manager.LockGroups[0].Count);
            Assert.AreEqual(2, manager.LockGroups[1].Count);

            Assert.IsTrue(manager.LockGroups[0].Contains(element1));
            Assert.IsTrue(manager.LockGroups[0].Contains(element2));

            Assert.IsTrue(manager.LockGroups[1].Contains(element3));
            Assert.IsTrue(manager.LockGroups[1].Contains(element4));
        }

        [TestMethod]
        public void ShouldMergeElementInSingleGroupIfElementsFromMultipleGroupsAreLockedTogether()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();

            var element3 = new TimelineElement();
            var element4 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };

            manager.LockElements(group1);
            manager.LockElements(group2);

            var group3 = new List<TimelineElement> { element1, element4 };

            manager.LockElements(group3);

            Assert.AreEqual(1, manager.LockGroups.Count);

            Assert.AreEqual(4, manager.LockGroups[0].Count);

            Assert.IsTrue(manager.LockGroups[0].Contains(element1));
            Assert.IsTrue(manager.LockGroups[0].Contains(element2));
            Assert.IsTrue(manager.LockGroups[0].Contains(element3));
            Assert.IsTrue(manager.LockGroups[0].Contains(element4));
        }

        [TestMethod]
        public void ShouldUpdateGroupToWhichElementBelongsToAfterGroupsAreMerged()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();

            var element3 = new TimelineElement();
            var element4 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };

            manager.LockElements(group1);
            manager.LockElements(group2);

            var group3 = new List<TimelineElement> { element1, element4 };

            manager.LockElements(group3);

            var elements = manager.GetGroupedElements(element3);
            
            Assert.IsTrue(elements.Contains(element1));
            Assert.IsTrue(elements.Contains(element2));
            Assert.IsTrue(elements.Contains(element3));
            Assert.IsTrue(elements.Contains(element4));
        }

        [TestMethod]
        public void ShouldGetElementsInSameLockGroupWhenRequested()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();

            var element3 = new TimelineElement();
            var element4 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };

            manager.LockElements(group1);
            manager.LockElements(group2);

            IEnumerable<TimelineElement> elementsGroupedWithElement2 = manager.GetGroupedElements(element2);
            
            Assert.AreEqual(2, elementsGroupedWithElement2.Count());
            Assert.IsTrue(elementsGroupedWithElement2.Contains(element1));
            Assert.IsTrue(elementsGroupedWithElement2.Contains(element2));

            IEnumerable<TimelineElement> elementsGroupedWithElement4 = manager.GetGroupedElements(element4);

            Assert.IsTrue(elementsGroupedWithElement4.Contains(element3));
            Assert.IsTrue(elementsGroupedWithElement4.Contains(element4));

            Assert.AreEqual(2, elementsGroupedWithElement4.Count());
        }

        [TestMethod]
        public void ShouldNotCreateWhenGroupingZeroOrOneElement()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();

            var group1 = new List<TimelineElement>();

            Assert.AreEqual(0, manager.LockGroups.Count);

            manager.LockElements(group1);

            Assert.AreEqual(0, manager.LockGroups.Count);

            group1.Add(element1);

            manager.LockElements(group1);

            Assert.AreEqual(0, manager.LockGroups.Count);
        }

        [TestMethod]
        public void ShouldReturnElementWhenTryingToGetElementInGroupOfNotGroupedElement()
        {
            var element = new TimelineElement();
            ILockGroupManager manager = this.CreateGroupLockManager();
            var elements = manager.GetGroupedElements(element);
            Assert.AreEqual(1, elements.Count());
            Assert.AreSame(element, elements.First());
        }

        [TestMethod]
        public void ShouldReturnNegativeOneWhenLockingZeroOrOneElement()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();

            var group1 = new List<TimelineElement>();

            Assert.AreEqual(0, manager.LockGroups.Count);

            int groupId = manager.LockElements(group1);

            Assert.AreEqual(-1, groupId);

            group1.Add(element1);

            groupId = manager.LockElements(group1);

            Assert.AreEqual(-1, groupId);
        }

        [TestMethod]
        public void ShouldReturnGroupLastNumberWhenLockingElements()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();

            var element3 = new TimelineElement();
            var element4 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };

            int groupId = manager.LockElements(group1);

            Assert.AreEqual(0, groupId);
            
            groupId = manager.LockElements(group2);
            
            Assert.AreEqual(1, groupId);

            var group3 = new List<TimelineElement> { element1, element4 };

            groupId = manager.LockElements(group3);

            Assert.AreEqual(0, groupId);
        }

        [TestMethod]
        public void ShouldReturnSingleElementAfterUnlockingGroup()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };

            manager.LockElements(group1);

            manager.UnlockElementLockGroup(element2);

            Assert.AreEqual(0, manager.LockGroups.Count);

            var elements = manager.GetGroupedElements(element1);
            Assert.AreEqual(1, elements.Count());

            elements = manager.GetGroupedElements(element2);
            Assert.AreEqual(1, elements.Count());
        }

        [TestMethod]
        public void ShouldNotUpdateGroupIdsAfterUnlockingGroup()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();
            var element3 = new TimelineElement();
            var element4 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };

            manager.LockElements(group1);
            manager.LockElements(group2);

            Assert.AreEqual(1, manager.LockGroups.Last().Id);

            manager.UnlockElementLockGroup(element2);

            Assert.AreEqual(1, manager.LockGroups.Count);

            Assert.AreEqual(1, manager.LockGroups.Last().Id);
        }

        [TestMethod]
        public void ShouldProvideLowestAvailableGroupIdWhenGroupingNewElements()
        {
            ILockGroupManager manager = this.CreateGroupLockManager();

            var element1 = new TimelineElement();
            var element2 = new TimelineElement();
            var element3 = new TimelineElement();
            var element4 = new TimelineElement();
            var element5 = new TimelineElement();
            var element6 = new TimelineElement();
            var element7 = new TimelineElement();
            var element8 = new TimelineElement();

            var group1 = new List<TimelineElement> { element1, element2 };
            var group2 = new List<TimelineElement> { element3, element4 };
            var group3 = new List<TimelineElement> { element5, element6 };
            var group4 = new List<TimelineElement> { element7, element8 };

            manager.LockElements(group1);
            manager.LockElements(group2);
            manager.LockElements(group3);

            manager.UnlockElementLockGroup(element1);

            manager.LockElements(group4);

            Assert.AreEqual(0, manager.LockGroups.Last().Id);
        }

        public ILockGroupManager CreateGroupLockManager()
        {
            return new LockGroupManager();
        }
    }
}
