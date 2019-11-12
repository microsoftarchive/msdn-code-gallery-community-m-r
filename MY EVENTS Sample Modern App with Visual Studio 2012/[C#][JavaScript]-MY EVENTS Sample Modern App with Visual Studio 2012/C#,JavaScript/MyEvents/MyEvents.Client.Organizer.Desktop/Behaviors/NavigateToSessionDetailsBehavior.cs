using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using GalaSoft.MvvmLight.Ioc;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.Behaviors
{
    /// <summary>
    /// Behavior to make possible click on any grid and execute a command from the viewmodel
    /// </summary>
    public class NavigateToSessionDetailsBehavior : Behavior<Grid>
    {
        /// <summary>
        /// Method call when the behavior is attached to the grid
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        /// <summary>
        /// Method call to deatach the event and avoid memory leaks
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            base.OnDetaching();
        }

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Session selectedSession = (Session)(sender as Grid).DataContext;
            var navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.NavigateToEventDetailsReadOnly(selectedSession);
        }
    }
}
