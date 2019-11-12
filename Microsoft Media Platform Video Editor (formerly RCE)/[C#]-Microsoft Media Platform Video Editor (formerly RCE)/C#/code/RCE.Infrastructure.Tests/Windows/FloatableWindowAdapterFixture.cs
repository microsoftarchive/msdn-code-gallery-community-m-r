// <copyright file="FloatableWindowAdapterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FloatableWindowAdapterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Windows
{
    using System.Windows;
    using System.Windows.Controls;

    using Microsoft.Silverlight.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Windows;

    [TestClass]
    public class FloatableWindowAdapterFixture : SilverlightTest
    {
        private Panel rootPanel;

        [TestInitialize]
        public void TestInitialize()
        {
            this.rootPanel = new Canvas();
        }

        [TestMethod]
        public void ShouldShowNonModalWindowWhenShowIsCalled()
        {
            IWindow window = this.CreateAdapter();

            Assert.IsFalse(window.IsOpen);

            window.Show(this.rootPanel);

            Assert.IsTrue(window.IsOpen);
            Assert.IsFalse(window.IsModal);
        }

        [TestMethod]
        public void ShouldShowModalWindowWhenShowIsCalled()
        {
            IWindow window = this.CreateAdapter();

            Assert.IsFalse(window.IsOpen);

            window.ShowDialog(this.rootPanel);

            Assert.IsTrue(window.IsOpen);
            Assert.IsTrue(window.IsModal);

            window.Close();
        }

        [TestMethod]
        public void ShouldCloseModalWindowWhenCloseIsCalled()
        {
            IWindow window = this.CreateAdapter();

            window.ShowDialog(this.rootPanel);

            Assert.IsTrue(window.IsOpen);

            window.Close();

            Assert.IsFalse(window.IsOpen);
        }

        [TestMethod]
        public void ShouldSetContentWhenSetContentIsCalled()
        {
            TestableFloatableWindowAdapter window = this.CreateTestableAdapter();

            TextBlock t = new TextBlock { Text = "TestText" };

            window.Content = t;

            Assert.AreSame(t, window.WrappedWindow.Content);
        }

        [TestMethod]
        public void ShouldUpdateCanvasLeftPropertyWhenSettingLeftInNonModalWindow()
        {
            TestableFloatableWindowAdapter window = this.CreateTestableAdapter();

            double left = (double)window.WrappedWindow.GetValue(Canvas.LeftProperty);
            
            Assert.AreEqual(0, left);
            Assert.AreEqual(0, window.Left);

            window.Left = 15;

            window.Show(this.rootPanel);
            
            left = (double)window.WrappedWindow.GetValue(Canvas.LeftProperty);

            Assert.AreEqual(15, left);
        }

        [TestMethod]
        public void ShouldUpdateCanvasTopPropertyWhenSettingTopInNonModalWindow()
        {
            TestableFloatableWindowAdapter window = this.CreateTestableAdapter();

            double top = (double)window.WrappedWindow.GetValue(Canvas.TopProperty);

            Assert.AreEqual(0, top);
            Assert.AreEqual(0, window.Top);

            window.Top = 15;

            window.Show(this.rootPanel);
            
            top = (double)window.WrappedWindow.GetValue(Canvas.TopProperty);

            Assert.AreEqual(15, top);
        }

        [TestMethod]
        public void ShouldSetWrappedWindowStyleWhenSettingStyle()
        {
            TestableFloatableWindowAdapter window = this.CreateTestableAdapter();

            Assert.IsNull(window.WrappedWindow.Style);

            Style windowStyle = new Style(typeof(FloatableWindow));

            window.Style = windowStyle;

            Assert.AreSame(window.Style, window.WrappedWindow.Style);
            Assert.AreSame(windowStyle, window.Style);
        }

        [TestMethod]
        public void ShouldRaiseClosedEventWhenWindowIsClosed()
        {
            IWindow window = this.CreateAdapter();

            this.TestPanel.Children.Add(this.rootPanel);
            
            window.ShowDialog(this.rootPanel);

            bool wasCalled = false;

            window.Closed += (s, a) => { wasCalled = true; };

            window.Close();

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void ShouldSetWrappedWindowTitleWhenSettingTitle()
        {
            TestableFloatableWindowAdapter window = this.CreateTestableAdapter();

            Assert.IsNull(window.WrappedWindow.Title);

            window.Title = "Window";

            Assert.AreEqual("Window", window.Title);
            Assert.AreEqual("Window", window.WrappedWindow.Title);
        }

        [TestMethod]
        public void ShouldSetResizeModeBasedOnResizeDirection()
        {
            var window = this.CreateTestableAdapter();

            window.ResizeDirection = ResizeDirection.Both;

            Assert.AreEqual(ResizeDirection.Both, window.ResizeDirection);

            Assert.AreEqual(ResizeMode.CanResize, window.WrappedWindow.ResizeMode);

            window.ResizeDirection = ResizeDirection.None;

            Assert.AreEqual(ResizeDirection.None, window.ResizeDirection);

            Assert.AreEqual(ResizeMode.NoResize, window.WrappedWindow.ResizeMode);

            window.ResizeDirection = ResizeDirection.Horizontal;

            Assert.AreEqual(ResizeDirection.Horizontal, window.ResizeDirection);

            Assert.AreEqual(ResizeMode.CanResizeHorizontally, window.WrappedWindow.ResizeMode);

            window.ResizeDirection = ResizeDirection.Vertical;

            Assert.AreEqual(ResizeDirection.Vertical, window.ResizeDirection);

            Assert.AreEqual(ResizeMode.CanResizeVertically, window.WrappedWindow.ResizeMode);
        }

        [TestMethod]
        public void ShouldSetWidthAndHeightWhenSizeIsSet()
        {
            var window = this.CreateTestableAdapter();

            window.Size = new Size(40, 80);

            Assert.AreEqual(40, window.WrappedWindow.Width);
            Assert.AreEqual(80, window.WrappedWindow.Height);

            Assert.AreEqual(40, window.Size.Width);
            Assert.AreEqual(80, window.Size.Height);
        }

        private IWindow CreateAdapter()
        {
            return new FloatableWindowAdapter();
        }

        private TestableFloatableWindowAdapter CreateTestableAdapter()
        {
            return new TestableFloatableWindowAdapter();
        }

        private class TestableFloatableWindowAdapter : FloatableWindowAdapter
        {
            public FloatableWindow WrappedWindow
            { 
                get
                {
                    return this.Window;
                } 
            }
        }
    }
}