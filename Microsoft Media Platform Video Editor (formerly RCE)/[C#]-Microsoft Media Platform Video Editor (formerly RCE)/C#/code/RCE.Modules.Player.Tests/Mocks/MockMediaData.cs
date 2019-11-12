// <copyright file="MockMediaData.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMediaData.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using Infrastructure.Models;
    using Player.Models;

    public class MockMediaData : MediaData
    {
        private MockMediaPlugin mediaPlugin;

        public MockMediaData()
        {
            this.mediaPlugin = new MockMediaPlugin();
        }

        public override Microsoft.SilverlightMediaFramework.Plugins.IMediaPlugin MediaPlugin
        {
            get
            {
                return this.mediaPlugin;
            }
        }

        public TimelineElement MockedTimelineElement { private get;  set; }

        public bool PlayCalled { get; set; }

        public bool ShowCalled { get; set; }

        public bool PauseCalled { get; set; }

        public override TimelineElement TimelineElement
        {
            get
            {
                return this.MockedTimelineElement;
            }
        }

        public override void Play()
        {
            this.PlayCalled = true;
            base.Play();
        }

        public override void Show()
        {
            this.ShowCalled = true;
            base.Show();
        }

        public override void Pause()
        {
            this.PauseCalled = true;
            base.Pause();
        }
    }
}
