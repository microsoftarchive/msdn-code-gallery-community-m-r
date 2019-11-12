namespace MyCompany.Visitors.Client.UniversalApp.Controls
{
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Employee panel control.
    /// </summary>
    public sealed partial class EmployeePanel : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmployeePanel()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            if (Window.Current.Bounds.Width < AppSettings.SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, "Snapped", true);
            else
                VisualStateManager.GoToState(this, "FullScreenLandscape", true);
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            if (Window.Current.Bounds.Width < AppSettings.SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, "Snapped", true);
            else
                VisualStateManager.GoToState(this, "FullScreenLandscape", true);
        }
    }
}
