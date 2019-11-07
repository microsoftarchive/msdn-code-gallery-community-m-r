// <copyright file="MockCommentView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCommentView.cs                     
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
    using Models;

    public class MockCommentView : ICommentView
    {
        public bool ShowErrorMessageCalled { get; set; }

        public bool ClearInkCommentCalled { get; set; }

        public bool SetInkEditingModeCalled { get; set; }

        public string ShowErrorMessageArgument { get; private set; }

        public ICommentViewPresentationModel Model { get; set; }

        public void ShowErrorMessage(string message)
        {
            this.ShowErrorMessageCalled = true;
            this.ShowErrorMessageArgument = message;
        }

        public void ClearInkComment()
        {
            this.ClearInkCommentCalled = true;
        }

        public void SetInkEditingMode(InkEditingMode mode)
        {
            this.SetInkEditingModeCalled = true;
        }
    }
}
