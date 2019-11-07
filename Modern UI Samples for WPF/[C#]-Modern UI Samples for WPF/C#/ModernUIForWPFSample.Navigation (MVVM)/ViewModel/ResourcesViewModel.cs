// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesViewModel.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   The resources view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using GalaSoft.MvvmLight.Command;

namespace ModernUIForWPFSample.Navigation__MVVM_.ViewModel
{
    /// <summary>
    /// The resources view model.
    /// </summary>
    public class ResourcesViewModel : ModernViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesViewModel"/> class. 
        /// </summary>
        public ResourcesViewModel()
        {
            NavigatingFromCommand = new RelayCommand(NavigatingFrom);
            NavigatedFromCommand = new RelayCommand(NavigatedFrom);
            NavigatedToCommand = new RelayCommand(NavigatedTo);
            FragmentNavigationCommand = new RelayCommand(FragmentNavigation);
            LoadedCommand = new RelayCommand(LoadData);
            IsVisibleChangedCommand = new RelayCommand(VisibilityChanged);
        }

        /// <summary>
        /// Visibilities the changed.
        /// </summary>
        private void VisibilityChanged()
        {
            Debug.WriteLine("ResourcesViewModel - VisibilityChanged");
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData()
        {
            Debug.WriteLine("ResourcesViewModel - LoadData");
        }

        /// <summary>
        /// Navigateds from.
        /// </summary>
        private void NavigatedFrom()
        {
            // called when we navigated to another view
            Debug.WriteLine("ResourcesViewModel - NavigatedFrom");
        }

        /// <summary>
        /// Navigateds to.
        /// </summary>
        private void NavigatedTo()
        {
            // called when we navigate to the view related with this view model.
            Debug.WriteLine("ResourcesViewModel - NavigatedTo");
        }

        /// <summary>
        /// Fragments the navigation.
        /// </summary>
        private void FragmentNavigation()
        {
            Debug.WriteLine("ResourcesViewModel - FragmentNavigation");
        }

        /// <summary>
        /// Navigatings from.
        /// </summary>
        private void NavigatingFrom()
        {
            // Called when we will navigate to new view
            Debug.WriteLine("ResourcesViewModel - NavigatingFrom");
        }
    }
}
