// <copyright file="MockCommentsBarPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPlayerViewPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Composite.Presentation.Commands;

    public class MockCommentsBarPresenter : ICommentsBarPresenter
    {
        private ICommentsBarView view = new MockCommentsBarView();

        public DelegateCommand<string> OptionSelectedCommand
        {
            get { throw new NotImplementedException(); }
        }

        public ICommentsBarView View
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

        public IList<string> Options
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool IsOptionMenuVisible
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}