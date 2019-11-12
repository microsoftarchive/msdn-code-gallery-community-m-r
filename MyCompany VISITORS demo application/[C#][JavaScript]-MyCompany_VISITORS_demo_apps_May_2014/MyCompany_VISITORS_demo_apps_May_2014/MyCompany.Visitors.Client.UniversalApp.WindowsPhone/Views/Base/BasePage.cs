using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views.Base
{
    public class BasePage : Page
    {
        public BasePage()
        {
            this.Loaded += (sender, e) =>
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            };

            this.Unloaded += (sender, e) =>
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            };
        }

        public virtual void GoBack()
        {                       
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

        public virtual bool CanGoBack()
        {
            return (this.Frame != null && this.Frame.CanGoBack);
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (this.CanGoBack()) 
            {
                e.Handled = true;
                this.GoBack();          
            }
        }
    }
}
