namespace MyCompany.Travel.Client.Desktop.Controls
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomDialog()
        {
            InitializeComponent();
        }

        private void CancelButton_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                this.CancelButton.Focus();
        }

    }
}
