// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   This class contains static references to all the view models in the
//   application and provides an entry point for the bindings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ModernUIForWPFSample.Navigation.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// The resource page key.
        /// </summary>
        public const string ResourcePageKey = "ResourceView";

        /// <summary>
        /// The steps page key.
        /// </summary>
        public const string StepsPageKey = "StepsView";
        
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<StepsViewModel>();
        }

        /// <summary>
        /// Gets the steps view model.
        /// </summary>
        /// <value>
        /// The steps view model.
        /// </value>
        public StepsViewModel StepsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StepsViewModel>();
            }
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}