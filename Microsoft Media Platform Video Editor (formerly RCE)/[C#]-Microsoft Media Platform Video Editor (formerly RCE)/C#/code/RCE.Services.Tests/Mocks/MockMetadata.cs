// <copyright file="MockMetadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMetadata.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests.Mocks
{
    using System;
    using Contracts;
    using SMPTETimecode;

    public class MockMetadata : Metadata
    {
        public override string Title
        {
            get { throw new NotImplementedException(); }
        }

        public override TimeSpan Duration
        {
            get { throw new NotImplementedException(); }
        }

        public override SmpteFrameRate FrameRate
        {
            get { throw new NotImplementedException(); }
        }

        public override int? Width
        {
            get { throw new NotImplementedException(); }
        }

        public override int? Height
        {
            get { throw new NotImplementedException(); }
        }
    }
}