// <copyright file="MockProjectViewPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockProjectViewPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects.Tests.Mocks
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;

    public class MockProjectViewPresenter : IProjectViewPresenter
    {
        private IProjectView view = new MockProjectView();

        public IProjectView View
        {
            get
            {
                return this.view;
            }

            set
            {
                this.view = value;
            }
        }

        public string HeaderIconOn
        {
            get { throw new System.NotImplementedException(); }
        }

        public string HeaderIconOff
        {
            get { throw new System.NotImplementedException(); }
        }

        public ObservableCollection<Project> Projects { get; set; }

        public DelegateCommand<object> DeleteCommand { get; set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string HeaderInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
