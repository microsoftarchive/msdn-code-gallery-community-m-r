// <copyright file="ITransitionsMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ITransitionsMetadataViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Views
{
    public interface ITransitionsMetadataViewPresentationModel
    {
        /// <summary>
        /// Gets or sets the value of the [View] as ITransitionsMetadataView.
        /// </summary>
        /// <value>The metadata view.</value>
        ITransitionsMetadataView View { get; set; }
    }
}