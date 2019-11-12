// <copyright file="HelpView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: HelpView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Microsoft.Practices.ServiceLocation;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Window for displaying all the shortcut keys used in the application.
    /// </summary>
    public partial class HelpView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpView"/> class.
        /// </summary>
        public HelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// List of all keyboar shortcuts.
        /// </summary>
        /// <returns><see cref="List{T}"/> of all keyboar shortcuts.</returns>
        private static List<KeyboardCommands> KeyBoardCommands()
        {
            KeyboardManagerService keyboardManagerService = ServiceLocator.Current.GetInstance<KeyboardManagerService>();

            return (from mapping in keyboardManagerService.Mappings
                    let tuple = mapping.Key
                    let keyName = tuple.Item2 != ModifierKeys.None ? string.Format("{0} + {1}", tuple.Item2, tuple.Item1) : string.Format("{0}", tuple.Item1)
                    select new KeyboardCommands
                        {
                            KeyName = keyName, Context = tuple.Item3.ToString(), Action = mapping.Value.ToString()
                        }).ToList();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            KeyBoardCommands().ForEach(x => this.KeyBoardCommandsList.Items.Add(x));
        }
    }
}
