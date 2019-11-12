namespace MyCompany.Visitors.Client.WP
{
    using System;
    using System.Windows;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MyCompany.Visitors.Client.WP.ViewModel;
    using MyCompany.Visitors.Client.WP.Model;
    using System.ComponentModel;

    /// <summary>
    /// Main Page
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        MainViewModel vm;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            vm = (MainViewModel)this.DataContext;
            vm.PropertyChanged += vm_PropertyChanged;
            vm_PropertyChanged(vm, new PropertyChangedEventArgs("ViewState"));
        }

        void vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ViewState")
            {
                ScreenStates state = vm.ViewState;
                VisualStateManager.GoToState(this, state.ToString(), true);

                if (state == ScreenStates.Editing)
                {
                    this.ApplicationBar.Buttons.Clear();
                    ApplicationBarIconButton button = new ApplicationBarIconButton(new Uri("/Assets/save.png", UriKind.Relative)) { Text = "save" };
                    button.Click += (s, args) => { vm.SaveInformationCommand.Execute(null); };
                    this.ApplicationBar.Buttons.Add(button);
                    this.ApplicationBar.Mode = ApplicationBarMode.Default;
                }
                else
                {
                    this.ApplicationBar.Buttons.Clear();
                    ApplicationBarIconButton buttonEdit = new ApplicationBarIconButton(new Uri("/Assets/edit.png", UriKind.Relative)) { Text = "edit" };
                    buttonEdit.Click += (s, args) => { vm.EditInformationCommand.Execute(null); };
                    this.ApplicationBar.Buttons.Add(buttonEdit);
                    this.ApplicationBar.Mode = ApplicationBarMode.Default;
                }
            }
        }

        /// <summary>
        /// Back key press logic.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if ((vm.ViewState == ScreenStates.Editing) && (vm.ExistInformation))
            {
                vm.ViewState = ScreenStates.Viewing;
                e.Cancel = true;
            }
        }
    }
}