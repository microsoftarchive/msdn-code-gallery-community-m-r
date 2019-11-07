// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

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
            AppearanceManager.Current.AccentColor = Colors.Orange;

            ContentSource = MenuLinkGroups.First().Links.First().Source;
        }
    }
}
