// <copyright file="RegionManagerExtensions.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RegionManagerExtensions.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.Composite.Regions;

    /// <summary>
    /// Extenstion methods for the <see cref="IRegionManager"/>.
    /// </summary>
    public static class RegionManagerExtensions
    {
        /// <summary>
        /// Registers the view with index.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="view">The view to be inserted in the region.</param>
        /// <param name="index">The index of the view.</param>
        /// <returns>The <see cref="IRegionManager"/>.</returns>
        public static IRegionManager RegisterViewWithRegionInIndex(this IRegionManager regionManager, string regionName, object view, int index)
        {
            IRegion region = regionManager.Regions[regionName];

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "Tried to add a view in a negative index.");
            }

            object activeView = null;

            if (region.ActiveViews != null)
            {
                 activeView = region.ActiveViews.FirstOrDefault();
            }

            // Save reference to each view existing in the RegionManager after the index to insert.
            List<object> views = region.Views.SkipWhile((x, removeFrom) => removeFrom < index).ToList();

            // Remove elements from region that are after index to insert.
            foreach (var existentView in views)
            {
                region.Remove(existentView);
            }

            regionManager.Regions[regionName].Add(view);

            views.ForEach(x => region.Add(x));

            if (activeView != null)
            {
                region.Activate(activeView);
            }

            return regionManager;
        }
    }
}
