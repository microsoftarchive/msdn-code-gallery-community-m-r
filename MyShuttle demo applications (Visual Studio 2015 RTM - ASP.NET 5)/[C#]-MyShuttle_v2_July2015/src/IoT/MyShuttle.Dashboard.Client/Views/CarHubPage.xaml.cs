using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Windows.UI.Xaml.Shapes;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace MyShuttle.Dashboard.Client.Views
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Animation;
    using MyShuttle.Dashboard.Client.ViewModels;
    using Windows.UI.Xaml.Controls;
    using MyShuttle.Dashboard.Client.Helpers;

    public sealed partial class CarHubPage
    {
        private CarHubPageViewModel vm;

        public CarHubPage()
        {
            InitializeComponent();
            this.DataContextChanged += CarHubPage_DataContextChanged;
        }

        private void CarHubPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            vm = DataContext as CarHubPageViewModel;
            vm.VehicleDataLoaded += Vm_VehicleDataLoaded;
        }

        private void Vm_VehicleDataLoaded(object sender, System.EventArgs e)
        {
            var stackpanel = ControlTreeHelper.FindNameInVisualTree<StackPanel>(VehicleHubSection, "VehicleHubStackPanel");
            var storyboard = ((Storyboard)stackpanel.Resources["CarLoadStoryBoard"]);
            storyboard.Begin();

            var chart = ControlTreeHelper.FindNameInVisualTree<Chart>(ChartHubSection, "MyChart");

            Style gridLineStyle = new Style(typeof(Line));
            gridLineStyle.Setters.Add(new Setter(Shape.StrokeProperty, "White"));
            gridLineStyle.Setters.Add(new Setter(Shape.StrokeThicknessProperty, 1));
            gridLineStyle.Setters.Add(new Setter(Shape.OpacityProperty, 1));
            gridLineStyle.Setters.Add(new Setter(Shape.StrokeDashArrayProperty, "6,4"));

            ((StackedColumnSeries)chart.Series[0]).DependentAxis = new LinearAxis
            {
                Minimum = 0,
                Title = "miles",
                Orientation = AxisOrientation.Y,
                ShowGridLines = true,
                MajorTickMarkStyle = gridLineStyle,
                MinorTickMarkStyle = gridLineStyle
            };

        }


    }
}
