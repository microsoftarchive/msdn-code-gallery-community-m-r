// <copyright file="MockCompositeCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCompositeCommand.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Tests.Mocks
{
    using Microsoft.Practices.Composite.Presentation.Commands;

    public class MockCompositeCommand : CompositeCommand
    {
        public object Parameter { get; set; }

        public bool CommandExecuted { get; set; }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            this.CommandExecuted = true;
            this.Parameter = parameter;
        }
    }
}
