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
using System.Windows;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

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
            RegisterNavigation();
            AppearanceManager.Current.AccentColor = Colors.Green;
            ContentSource = MenuLinkGroups.First().Links.First().Source;
        }

        /// <summary>
        /// Registers the navigation.
        /// </summary>
        private void RegisterNavigation()
        {
            Messenger.Default.Register<NavigationMessage>(this, p =>
            {
                var frame = GetDescendantFromName(this, "ContentFrame") as ModernFrame;
                 
                // Set the frame source, which initiates navigation
                if (frame != null)
                {
                    frame.Source = new Uri(p.Page, UriKind.Relative);
                }
            });
        }

        /// <summary>
        /// Gets the name of the descendant from.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>The FrameworkElement.</returns>
        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }
    }
}
