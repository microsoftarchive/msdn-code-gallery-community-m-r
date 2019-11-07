using System;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.UI.Xaml;
using WinRtBehaviors;

namespace MyEvents.Client.Organizer.ViewModel.Base
{
    /// <summary>
    /// A behavior to imitate an EventToCommand trigger
    /// </summary>
    public class TapEventToCommandBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            var evt = AssociatedObject.GetType().GetRuntimeEvent(Event);
            if (evt != null)
            {
                Observable.FromEventPattern<RoutedEventArgs>(AssociatedObject, Event)
                  .Subscribe(se => FireCommand(se.EventArgs));
            }
        }

        private void FireCommand(RoutedEventArgs e)
        {
            var dataContext = AssociatedObject.DataContext;
            if (dataContext != null)
            {
                if (Command != null)
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        #region Event

        /// <summary>
        /// Event Property name
        /// </summary>
        public const string EventPropertyName = "Event";

        public string Event
        {
            get { return (string)GetValue(EventProperty); }
            set { SetValue(EventProperty, value); }
        }

        /// <summary>
        /// Event Property definition
        /// </summary>
        public static readonly DependencyProperty EventProperty = DependencyProperty.Register(
            EventPropertyName,
            typeof(string),
            typeof(TapEventToCommandBehavior),
            new PropertyMetadata(default(string)));

        #endregion

        #region Command

        /// <summary>
        /// Command Property name
        /// </summary>
        public const string CommandPropertyName = "Command";

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Command Property definition
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            CommandPropertyName,
            typeof(ICommand),
            typeof(TapEventToCommandBehavior),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region CommandParameter

        /// <summary>
        /// CommandParameter Property name
        /// </summary>
        public const string CommandParameterPropertyName = "CommandParameter";

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// CommandParameter Property definition
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            CommandParameterPropertyName,
            typeof(object),
            typeof(TapEventToCommandBehavior),
            new PropertyMetadata(default(object)));

        #endregion
    }
}
