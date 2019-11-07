// <copyright file="KeyboardManagerService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: KeyboardManagerService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    /// Manage mapping to keyboard keys.
    /// </summary>
    public abstract class KeyboardManagerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardManagerService"/> class.
        /// </summary>
        public KeyboardManagerService()
        {
            this.Mappings = new Dictionary<Tuple<Key, ModifierKeys, KeyboardActionContext>, KeyboardAction>();
        }

        /// <summary>
        /// Gets or sets Mapping keys with actions and context.
        /// </summary>
        public IDictionary<Tuple<Key, ModifierKeys, KeyboardActionContext>, KeyboardAction> Mappings { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether Strict.
        /// </summary>
        /// <value>The strict.</value>
        public virtual bool Strict
        {
            get { return false; }
        }
        
        /// <summary>
        /// Map a key with an action.
        /// </summary>
        /// <param name="key">The key pressed.</param>
        /// <param name="modifierKeys">The modifier keys pressed.</param>
        /// <param name="keyboardAction">The keyboard action.</param>
        /// <param name="keyboardActionContext">The keyboard action context.</param>
        public void Map(Key key, ModifierKeys modifierKeys, KeyboardAction keyboardAction, KeyboardActionContext keyboardActionContext)
        {
            Tuple<Key, ModifierKeys, KeyboardActionContext> tuple = new Tuple<Key, ModifierKeys, KeyboardActionContext>(key, modifierKeys, keyboardActionContext);

            if (!this.Mappings.ContainsKey(tuple))
            {
                this.Mappings.Add(tuple, keyboardAction);
            }
        }

        /// <summary>
        /// Return a KeyboardAction based on keys pressed and context.
        /// </summary>
        /// <param name="key">The key pressed.</param>
        /// <param name="modifierKeys">The modifier keys pressed.</param>
        /// <param name="keyboardActionContext">The keyboard action context.</param>
        /// <returns>The keyboard action.</returns>
        public KeyboardAction GetKeyboardAction(Key key, ModifierKeys modifierKeys, KeyboardActionContext keyboardActionContext)
        {
            Tuple<Key, ModifierKeys, KeyboardActionContext> tuple = new Tuple<Key, ModifierKeys, KeyboardActionContext>(key, modifierKeys, keyboardActionContext);

            return this.Mappings.ContainsKey(tuple) ? this.Mappings[tuple] : KeyboardAction.None;
        }
    }
}
