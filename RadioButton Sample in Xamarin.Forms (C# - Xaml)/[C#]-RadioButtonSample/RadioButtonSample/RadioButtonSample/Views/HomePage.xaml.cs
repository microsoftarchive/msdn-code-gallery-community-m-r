using RadioButtonSample.Controls;
using RadioButtonSample.ViewModels;
using Xamarin.Forms;

namespace RadioButtonSample.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel _homeViewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = _homeViewModel = new HomeViewModel();
            CreateRadioButton();
            RadioButtonBinding();
        }

        /// <summary>
        /// Creating RadioButton with assigned values (Bg, border, title, selection)
        /// </summary>
        void CreateRadioButton()
        {
            RadioButton radioButton = new RadioButton();
            radioButton.IsChecked = false;
            radioButton.IsVisible = true;
            radioButton.Title = "Japan";
            radioButton.BorderImageSource = "radioborder";
            radioButton.CheckedBackgroundImageSource = "radiocheckedbg";
            radioButton.CheckmarkImageSource = "radiocheckmark";
            stackPanel.Children.Add(radioButton);
        }

        /// <summary>
        /// RadioButton binding with homeViewModel
        /// </summary>
        void RadioButtonBinding()
        {
            Country country = new Country();
            country.Name = "Singapore";
            country.IsSelected = false;
            country.IsVisible = true;

            RadioButton radioButton = new RadioButton();
            radioButton.BindingContext = country;
            radioButton.SetBinding(RadioButton.IsCheckedProperty, "IsSelected", BindingMode.TwoWay);
            radioButton.SetBinding(RadioButton.IsVisibleProperty, "IsVisible");
            radioButton.SetBinding(RadioButton.TitleProperty, "Name");
            radioButton.BorderImageSource = "radioborder";
            radioButton.CheckedBackgroundImageSource = "radiocheckedbg";
            radioButton.CheckmarkImageSource = "radiocheckmark";
            stackPanel.Children.Add(radioButton);
        }
    }
}
