// <copyright file="MockCommentViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCommentViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Ink;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;

    public class MockCommentViewPresentationModel : ICommentViewPresentationModel
    {
        private ICommentView view = new MockCommentView();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommentView View
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

        public string HeaderInfo { get; set; }

        public bool PlayCommentCalled { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        public IList<string> CommentsTypes
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string SelectedCommentType
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public Comment CurrentComment { get; set; }

        public bool EditMode
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string FrameImage
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool IsTextualComment
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool ShowGlobalComments
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool ShowTimelineComments
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public StrokeCollection InkCommentStrokes
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public DelegateCommand<string> SearchCommand { get; set; }

        public DelegateCommand<Guid> SaveCommand { get; set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DelegateCommand<string> CancelCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public DelegateCommand<object> EditCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public DelegateCommand<object> PlayCommentCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public string Text
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public string HeaderIconOn
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public string HeaderIconOff
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public DelegateCommand<object> DeleteCommand { get; set; }

        public void PlayComment()
        {
            this.PlayCommentCalled = true;
        }
    }
}
