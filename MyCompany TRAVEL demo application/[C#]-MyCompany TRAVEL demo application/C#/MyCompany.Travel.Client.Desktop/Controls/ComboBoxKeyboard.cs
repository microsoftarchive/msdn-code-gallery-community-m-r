namespace MyCompany.Travel.Client.Desktop.Controls
{
    using MyCompany.Travel.Client.Desktop.Helpers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    /// <summary>
    /// Combobox with virtual Keyboard
    /// </summary>.
    public class ComboBoxKeyboard : ComboBox
    {
        private TabtipHelper tabtipHelper = TabtipHelper.Instance;

        /// <summary>
        /// On got focus.
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            tabtipHelper.TryCreateTabtipProcess();
            base.OnGotKeyboardFocus(e);
        }

        /// <summary>
        /// On lost focus.
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            tabtipHelper.TryKillTabtipProcess();
            base.OnLostKeyboardFocus(e);
        }
    }
}
