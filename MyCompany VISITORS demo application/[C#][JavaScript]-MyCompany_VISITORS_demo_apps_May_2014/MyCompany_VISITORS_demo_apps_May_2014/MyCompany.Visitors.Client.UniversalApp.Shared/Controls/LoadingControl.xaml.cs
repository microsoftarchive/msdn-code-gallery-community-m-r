namespace MyCompany.Visitors.Client.UniversalApp.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    
    /// <summary>
    /// Loading control.
    /// </summary>
    public sealed partial class LoadingControl : UserControl
    {
        /// <summary>
        /// Is busy dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(LoadingControl), new PropertyMetadata(false, new PropertyChangedCallback((d, e) =>
            {
                if (e.NewValue != e.OldValue)
                {
                    bool isBusy = false;
                    bool.TryParse(e.NewValue.ToString(), out isBusy);
                    if (isBusy)
                        (d as UIElement).Visibility = Visibility.Visible;
                    else
                        (d as UIElement).Visibility = Visibility.Collapsed;
                }
            })));

        /// <summary>
        /// Is busy.
        /// </summary>
        public bool IsBusy
        {
            get 
            {
                bool isBusy;
                bool.TryParse(this.GetValue(IsBusyProperty).ToString(), out isBusy);
                return isBusy;
            }
            set
            {
                this.SetValue(IsBusyProperty, value);
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoadingControl()
        {
            this.InitializeComponent();
        }
    }
}
