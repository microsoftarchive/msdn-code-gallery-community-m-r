// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModernNavigationService.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   The ModernNavigationService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using GalaSoft.MvvmLight.Views;

namespace ModernUIForWPFSample.Navigation.Services
{
    /// <summary>
    /// The ModernNavigationService interface.
    /// </summary>
    public interface IModernNavigationService : INavigationService
    {
        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        object Parameter { get; }
    }
}
