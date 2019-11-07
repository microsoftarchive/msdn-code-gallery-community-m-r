// <copyright file="RegionWindowBehaviors.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RegionWindowBehaviors.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows.Behaviors
{
    using System.ComponentModel;
    using System.Windows;

    using Microsoft.Practices.Composite.Presentation.Regions;
    using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Declares the Attached Properties and Behaviors for implementing Window regions.
    /// </summary>
    public static class RegionWindowBehaviors
    {
        /// <summary>
        /// The name of the Popup <see cref="IRegion"/>.
        /// </summary>
        public static readonly DependencyProperty CreateWindowRegionWithNameProperty =
            DependencyProperty.RegisterAttached("CreateWindowRegionWithName", typeof(string), typeof(RegionWindowBehaviors), new PropertyMetadata(CreateWindowRegionWithNamePropertyChanged));

        /// <summary>
        /// The <see cref="Style"/> to set to the Popup.
        /// </summary>
        public static readonly DependencyProperty ContainerWindowStyleProperty =
          DependencyProperty.RegisterAttached("ContainerWindowStyle", typeof(Style), typeof(RegionWindowBehaviors), null);

        /// <summary>
        /// Gets the name of the Window <see cref="IRegion"/>.
        /// </summary>
        /// <param name="owner">Owner of the Window.</param>
        /// <returns>The name of the Window <see cref="IRegion"/>.</returns>
        public static string GetCreateWindowRegionWithName(DependencyObject owner)
        {
            return owner.GetValue(CreateWindowRegionWithNameProperty) as string;
        }

        /// <summary>
        /// Sets the name of the region.<see cref="IRegion"/>.
        /// </summary>
        /// <param name="owner">Owner of the window region.</param>
        /// <param name="value">Name of the window region. <see cref="IRegion"/>.</param>
        public static void SetCreateWindowRegionWithName(DependencyObject owner, string value)
        {
            owner.SetValue(CreateWindowRegionWithNameProperty, value);
        }

        /// <summary>
        /// Gets the <see cref="Style"/> for the Popup.
        /// </summary>
        /// <param name="owner">Owner of the Popup.</param>
        /// <returns>The <see cref="Style"/> for the Popup.</returns>
        public static Style GetContainerWindowStyle(DependencyObject owner)
        {
            return owner.GetValue(ContainerWindowStyleProperty) as Style;
        }

        /// <summary>
        /// Sets the <see cref="Style"/> for the Windows.
        /// </summary>
        /// <param name="owner">Owner of the Windows.</param>
        /// <param name="style"><see cref="Style"/> for the Windows.</param>
        public static void SetContainerWindowStyle(DependencyObject owner, Style style)
        {
            owner.SetValue(ContainerWindowStyleProperty, style);
        }

        /// <summary>
        /// Creates a new <see cref="IRegion"/> and registers it in the default <see cref="IRegionManager"/>
        /// attaching to it a <see cref="DialogActivationBehavior"/> behavior.
        /// </summary>
        /// <param name="owner">The owner of the Popup.</param>
        /// <param name="regionName">The name of the <see cref="IRegion"/>.</param>
        /// <remarks>
        /// This method would typically not be called directly, instead the behavior 
        /// should be set through the Attached Property <see cref="CreateWindowRegionWithNameProperty"/>.
        /// </remarks>
        public static void RegisterNewWindowRegion(DependencyObject owner, string regionName)
        {
            // Creates a new region and registers it in the default region manager.
            // Another option if you need the complete infrastructure with the default region behaviors
            // is to extend DelayedRegionCreationBehavior overriding the CreateRegion method and create an 
            // instance of it that will be in charge of registering the Region once a RegionManager is
            // set as an attached property in the Visual Tree.
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager != null)
            {
                IRegion region = new AllActiveRegion();
                IHostAwareRegionBehavior behavior = ServiceLocator.Current.GetInstance<DialogActivationBehavior>();
                behavior.HostControl = owner;

                region.Behaviors.Add(DialogActivationBehavior.BehaviorKey, behavior);
                regionManager.Regions.Add(regionName, region);
            }
        }

        private static void CreateWindowRegionWithNamePropertyChanged(DependencyObject hostControl, DependencyPropertyChangedEventArgs e)
        {
            if (IsInDesignMode(hostControl))
            {
                return;
            }

            RegisterNewWindowRegion(hostControl, e.NewValue as string);
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            // Due to a known issue in Cider, GetIsInDesignMode attached property value is not enough to know if it's in design mode.
            return DesignerProperties.GetIsInDesignMode(element) || Application.Current == null
                   || Application.Current.GetType() == typeof(Application);
        }
    }
}
