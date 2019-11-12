// <copyright file="MockCommentsBarView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCommentsBarView.cs                     
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

    using SMPTETimecode;

    public class MockCommentsBarView : ICommentsBarView
    {
        public event EventHandler<CommentEventArgs> CommentEvent;

        public bool SetDurationCalled { get; set; }

        public TimeCode SetDurationArgument { get; set; }

        public bool ShowOptionsCalled { get; set; }

        public double? ShowOptionsArgument { get; set; }

        public bool RefreshPreviewsCalled { get; set; }

        public double RefreshPreviewsArgument { get; set; }

        public bool AddPreviewCalled { get; set; }

        public object AddPreviewPreviewArgument { get; set; }

        public double? AddPreviewPositionArgument { get; set; }

        public object AddPreviewEditBoxArgument { get; set; }

        public bool UpdatePreviewCalled { get; set; }

        public object UpdatePreviewPreviewArgument { get; set; }

        public double? UpdatePreviewPositionArgument { get; set; }

        public object UpdatePreviewEditBoxArgument { get; set; }

        public bool RemovePreviewCalled { get; private set; }

        public object RemovePreviewPreviewArgument { get; private set; }

        public object RemovePreviewEditBoxArgument { get; private set; }

        public ICommentsBarPresenter Model { get; set; }

        public void RefreshPreviews(double width)
        {
            this.RefreshPreviewsCalled = true;
            this.RefreshPreviewsArgument = width;
        }

        public void SetDuration(TimeCode duration)
        {
            this.SetDurationCalled = true;
            this.SetDurationArgument = duration;
        }

        public void AddPreview(object preview, double position, object editBox, object displayBox)
        {
            this.AddPreviewCalled = true;
            this.AddPreviewPreviewArgument = preview;
            this.AddPreviewPositionArgument = position;
            this.AddPreviewEditBoxArgument = editBox;
        }

        public void RemovePreview(object preview, object editBox)
        {
            this.RemovePreviewCalled = true;
            this.RemovePreviewPreviewArgument = preview;
            this.RemovePreviewEditBoxArgument = editBox;
        }

        public void UpdatePreview(object preview, double position, object editBox, object displayBox)
        {
            this.UpdatePreviewCalled = true;
            this.UpdatePreviewPreviewArgument = preview;
            this.UpdatePreviewPositionArgument = position;
            this.UpdatePreviewEditBoxArgument = editBox;
        }

        public void ShowOptions(double seconds)
        {
            this.ShowOptionsCalled = true;
            this.ShowOptionsArgument = seconds;
        }

        public void InvokeCommentEvent(CommentEventArgs e)
        {
            EventHandler<CommentEventArgs> handler = this.CommentEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void SetEditBoxMargins(int right, int up, int left, int down)
        {
        }

        public void SetEditBoxZeeIndex(int zetaIndex)
        {
        }

        public void RemoveAllPreviews()
        {
        }

        public void CloseOptions()
        {
        }
    }
}