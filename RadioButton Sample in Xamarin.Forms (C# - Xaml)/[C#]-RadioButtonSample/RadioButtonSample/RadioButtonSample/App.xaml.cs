using System;
using RadioButtonSample.Views;
using Xamarin.Forms;

namespace RadioButtonSample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new HomePage();
        }
    }
}
