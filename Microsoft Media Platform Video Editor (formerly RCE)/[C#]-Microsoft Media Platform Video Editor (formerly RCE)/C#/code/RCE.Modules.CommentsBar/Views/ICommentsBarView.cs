// <copyright file="ICommentsBarView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICommentsBarView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using SMPTETimecode;

    /// <summary>
    /// Interface that defines a CommentsBar view presentation model.
    /// </summary>
    public interface ICommentsBarView
    {
        ICommentsBarPresenter Model { get; set; }

        /// <summary>
        /// Refreshes all the comments and it's layout in the comment bar.
        /// </summary>
        /// <param name="width">The width.</param>
        void RefreshPreviews(double width);
        
        /// <summary>
        /// Set the duration for the comment.
        /// </summary>
        /// <param name="currentDuration">The TimeCode.</param>
        void SetDuration(TimeCode currentDuration);

        void AddPreview(object preview, double position, object editBox, object displayBox);

        void RemovePreview(object preview, object editBox);

        void UpdatePreview(object preview, double position, object editBox, object displayBox);

        void ShowOptions(double seconds);

        void SetEditBoxMargins(int left, int up, int right, int down);

        void SetEditBoxZeeIndex(int zeeIndex);

        void RemoveAllPreviews();

        void CloseOptions();
    }
}