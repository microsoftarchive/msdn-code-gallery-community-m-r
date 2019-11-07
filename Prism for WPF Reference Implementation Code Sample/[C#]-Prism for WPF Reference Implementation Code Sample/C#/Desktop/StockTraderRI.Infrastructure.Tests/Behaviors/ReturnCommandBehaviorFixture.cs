// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Behaviors;

namespace StockTraderRI.Infrastructure.Tests.Behaviors
{
    [TestClass]
    public class ReturnCommandBehaviorFixture
    {
        [TestMethod]
        public void ShouldExecuteCommandOnEnter()
        {
            var textBox = new TextBox();
            textBox.Text = "MyParameter";
            textBox.AcceptsReturn = true;
            var command = new MockCommand();
            Assert.IsTrue(textBox.AcceptsReturn);

            TestableReturnCommandBehavior behavior = new TestableReturnCommandBehavior(textBox);
            Assert.IsFalse(textBox.AcceptsReturn);
            behavior.Command = command;

            behavior.InvokeKeyPressed(Key.Enter);

            Assert.IsTrue(command.ExecuteCalled);
            Assert.AreEqual("MyParameter", command.ExecuteParameter);
        }

        [TestMethod]
        public void ShouldClearTextOnFocus()
        {
            var textBox = new TextBox();
            textBox.Text = "DefaultText";
            var command = new MockCommand();

            TestableReturnCommandBehavior behavior = new TestableReturnCommandBehavior(textBox);
            behavior.Command = command;
            behavior.DefaultTextAfterCommandExecution = "DefaultText";

            textBox.Focus();

            Assert.AreEqual(string.Empty, textBox.Text);
        }

        [TestMethod]
        public void ShouldResetTextOnLostFocus()
        {
            var textBox = new TextBox();
            var textBox2 = new TextBox();
            var grid = new Grid();
            grid.Children.Add(textBox);
            grid.Children.Add(textBox2);
            var command = new MockCommand();

            TestableReturnCommandBehavior behavior = new TestableReturnCommandBehavior(textBox);
            behavior.Command = command;
            behavior.DefaultTextAfterCommandExecution = "DefaultText";

            textBox.Focus();

            DependencyObject scope = FocusManager.GetFocusScope(textBox);
            FocusManager.SetFocusedElement(scope, textBox2 as IInputElement);

            Assert.AreEqual("DefaultText", textBox.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfTextboxIsNull()
        {
            var rcb = new ReturnCommandBehavior(null);
        }

        [TestMethod]
        public void ShouldNotExecuteCommandOnKeyDifferentThanEnter()
        {
            var textBox = new TextBox();
            textBox.Text = "MyParameter";
            TestableReturnCommandBehavior behavior = new TestableReturnCommandBehavior(textBox);
            var command = new MockCommand();
            behavior.Command = command;

            behavior.InvokeKeyPressed(Key.Space);

            Assert.IsFalse(command.ExecuteCalled);
        }

        [TestMethod]
        public void ShouldSetEmptyTextAfterCommandExecution()
        {
            var textBox = new TextBox();
            var command = new MockCommand();

            TestableReturnCommandBehavior behavior = new TestableReturnCommandBehavior(textBox);
            behavior.Command = command;
            behavior.DefaultTextAfterCommandExecution = "MyDefaultText";

            behavior.InvokeKeyPressed(Key.Enter);

            Assert.AreEqual(string.Empty, textBox.Text);
        }
    }

    internal class MockCommand : ICommand
    {
        public bool ExecuteCalled;
        public object ExecuteParameter;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteCalled = true;
            ExecuteParameter = parameter;
        }
    }

    internal class TestableReturnCommandBehavior : ReturnCommandBehavior
    {
        public TestableReturnCommandBehavior(TextBox textBox)
            : base(textBox)
        {
        }

        public void InvokeKeyPressed(Key keyPressed)
        {
            base.KeyPressed(keyPressed);
        }
    }
}