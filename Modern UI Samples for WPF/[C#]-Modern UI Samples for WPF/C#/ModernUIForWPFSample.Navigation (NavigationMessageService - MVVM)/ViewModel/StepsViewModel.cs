// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepsViewModel.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   This class contains properties that the main View can data bind to.
//   <para>
//   Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
//   </para>
//   <para>
//   You can also use Blend to data bind with the tool's support.
//   </para>
//   <para>
//   See http://www.galasoft.ch/mvvm
//   </para>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ModernUIForWPFSample.Navigation.ViewModel
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
    public class StepsViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepsViewModel"/> class. 
        /// </summary>
        public StepsViewModel()
        {
            ResourcesCommand = new RelayCommand(ShowResources);
        }

        /// <summary>
        /// Gets or sets the resources command.
        /// </summary>
        /// <value>The resources command.</value>
        public ICommand ResourcesCommand { get; set; }

        /// <summary>
        /// Shows the resources.
        /// </summary>
        private void ShowResources()
        {
            Messenger.Default.Send(new NavigationMessage()
            {
                Page = "Views/ResourcesControl.xaml"
            }); 
        }
    }
}