// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Behaviors;

namespace StockTraderRI.Infrastructure.Tests
{
    [TestClass]
    public class ReturnKeyFixture
    {
        [TestMethod]
        public void ShouldGetAndSeReturnKeytProperties()
        {
            var textBox = new TextBox();
            const string defaultText = "Default Text";

            var command = new MockCommand();

            ReturnKey.SetCommand(textBox, command);
            Assert.AreEqual(command, ReturnKey.GetCommand(textBox));

            ReturnKey.SetDefaultTextAfterCommandExecution(textBox, defaultText);
            Assert.AreEqual(defaultText, ReturnKey.GetDefaultTextAfterCommandExecution(textBox));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionInGetCommandIfTextboxIsNull()
        {
            ReturnKey.GetCommand(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionInSetCommandIfTextboxIsNull()
        {
            ReturnKey.SetCommand(null, new MockCommand());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionInGetDefaultTextAfterCommandExecutionIfTextboxIsNull()
        {
            ReturnKey.GetDefaultTextAfterCommandExecution(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionInSetDefaultTextAfterCommandExecutionIfTextboxIsNull()
        {
            ReturnKey.SetDefaultTextAfterCommandExecution(null, "Some Text");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfTextboxIsNull()
        {
            var rcb = new ReturnCommandBehavior(null);
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
    }
}