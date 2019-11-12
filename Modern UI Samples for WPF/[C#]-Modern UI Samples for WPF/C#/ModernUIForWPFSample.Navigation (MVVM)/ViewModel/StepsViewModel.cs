// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepsViewModel.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   This class contains properties that the main View can data bind to.
//   Use the mvvminpc snippet to add bindable properties to this ViewModel.
//   You can also use Blend to data bind with the tool's support.
//   See http://www.galasoft.ch/mvvm
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using GalaSoft.MvvmLight.Command;

namespace ModernUIForWPFSample.Navigation__MVVM_.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StepsViewModel : ModernViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepsViewModel"/> class. 
        /// </summary>
        public StepsViewModel()
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
            Debug.WriteLine("StepsViewModel - LoadData");
        }

        /// <summary>
        /// Navigateds from.
        /// </summary>
        private void NavigatedFrom()
        {
            // called when we navigated to another view
            Debug.WriteLine("StepsViewModel - NavigatedFrom");
        }

        /// <summary>
        /// Navigateds to.
        /// </summary>
        private void NavigatedTo()
        {
            // called when we navigate to the view related with this view model.
            Debug.WriteLine("StepsViewModel - NavigatedTo");
        }

        /// <summary>
        /// Fragments the navigation.
        /// </summary>
        private void FragmentNavigation()
        {
            Debug.WriteLine("StepsViewModel - FragmentNavigation");
        }

        /// <summary>
        /// Navigatings from.
        /// </summary>
        private void NavigatingFrom()
        {
            // Called when we will navigate to new view
            Debug.WriteLine("StepsViewModel - NavigatingFrom");
        }
    }
}