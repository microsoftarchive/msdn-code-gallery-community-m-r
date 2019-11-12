// <copyright file="ISearchViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISearchViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search
{
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;

    public interface ISearchViewPresentationModel : IKeyboardAware
    {
        ISearchView View { get; }

        DelegateCommand<string> SearchCommand { get; }

        string Title { get; set; }
    }
}