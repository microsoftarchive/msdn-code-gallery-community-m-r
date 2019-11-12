namespace MyCompany.Travel.Client.Desktop.Controls
{
    using MyCompany.Travel.Client.Desktop.Helpers;
    using MyCompany.Travel.Client.Desktop.Views;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Windows.Devices.Input;

    /// <summary>
    /// Textbox with virtual Keyboard
    /// </summary>.
    public partial class TextBoxKeyboard : TextBox
    {
        private TabtipHelper tabtipHelper = TabtipHelper.Instance;

        /// <summary>
        /// On got focus.
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnGotFocus(System.Windows.RoutedEventArgs e)
        {
            tabtipHelper.TryCreateTabtipProcess();
            base.OnGotFocus(e);
        }

        /// <summary>
        /// On lost focus.
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnLostFocus(System.Windows.RoutedEventArgs e)
        {
            tabtipHelper.TryKillTabtipProcess();
            base.OnLostFocus(e);
        }
    }
}
