// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using ModernUIForWPFSample.Navigation.Services;
using ModernUIForWPFSample.Navigation.ViewModel;

namespace ModernUIForWPFSample.Navigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetupNavigation();
            AppearanceManager.Current.AccentColor = Colors.Green;
            ContentSource = MenuLinkGroups.First().Links.First().Source;
        }

        private void SetupNavigation()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(ViewModelLocator.ResourcePageKey, new Uri("Views/ResourcesView.xaml", UriKind.Relative));
            navigationService.Configure(ViewModelLocator.StepsPageKey, new Uri("Views/StepsView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IModernNavigationService>(() => navigationService);
        }
    }
}
