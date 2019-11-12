// <copyright file="KeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: KeyboardBehavior.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    using Microsoft.Practices.ServiceLocation;

    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;

    public abstract class KeyboardBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(KeyboardBehavior),
            null);

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(KeyboardBehavior),
            null);

        private readonly KeyboardManagerService keyboardManagerService;

        private readonly IWindowManager windowManager;

        protected KeyboardBehavior()
        {
            if (!DesignerProperties.IsInDesignMode)
            {
                this.keyboardManagerService = ServiceLocator.Current.GetInstance<KeyboardManagerService>();
                this.windowManager = ServiceLocator.Current.GetInstance<IWindowManager>();
            }
        }

        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        protected virtual object GetCommandParameter()
        {
            return this.CommandParameter;
        }

        protected virtual bool CanHandleKey(KeyEventArgs e)
        {
            return true;
        }

        protected abstract KeyboardActionContext GetKeyboardActionContext();

        protected virtual void ExecuteAction(KeyEventArgs e)
        {
            var window = this.windowManager.GetWindowWithFocus();

            if (window == null)
            {
                // no window has focus => no localized command should be executed.
                return;
            }

            var view = window.Content as FrameworkElement; 

            if (view == null)
            {
                return;
            }

            var viewModel = view.DataContext as IKeyboardAware;

            KeyboardActionContext c;
            if (viewModel == null || viewModel.ActionContext != (c = this.GetKeyboardActionContext()))
            {
                return;
            }

            KeyboardAction keyboardAction = this.keyboardManagerService.GetKeyboardAction(e.Key, Keyboard.Modifiers, viewModel.ActionContext);

            Tuple<KeyboardAction, object> tuple = new Tuple<KeyboardAction, object>(keyboardAction, this.GetCommandParameter());

            if ((keyboardAction == KeyboardAction.ActivateModel || this.CanHandleKey(e)) && keyboardAction != KeyboardAction.None && this.Command != null && this.Command.CanExecute(tuple))
            {
                this.Command.Execute(tuple);

                e.Handled = true;
            }
        }
    }
}
