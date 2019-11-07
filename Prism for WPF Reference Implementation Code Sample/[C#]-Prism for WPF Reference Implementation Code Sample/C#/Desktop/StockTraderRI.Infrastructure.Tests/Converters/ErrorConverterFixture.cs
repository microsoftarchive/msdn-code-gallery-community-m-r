// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Converters;

namespace StockTraderRI.Infrastructure.Tests.Converters
{
    [TestClass]
    public class ErrorConverterFixture
    {
        [TestMethod]
        public void ShouldReturnEmptyStringIfValueIsNull()
        {
            ErrorConverter converter = new ErrorConverter();
            object errors = null;

            object result = converter.Convert(errors, null, null, null);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ShouldReturnEmptyStringIfCollectionIsEmpty()
        {
            ErrorConverter converter = new ErrorConverter();

            List<ValidationError> errors = new List<ValidationError>();

            object result = converter.Convert(errors.AsReadOnly(), null, null, null);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ShouldReturnTheExceptionMessageOfTheFirstItemInTheCollection()
        {
            ErrorConverter converter = new ErrorConverter();

            List<ValidationError> errors = new List<ValidationError>();
            ValidationError error = new ValidationError(new ExceptionValidationRule(), new object());
            error.Exception = new Exception("TestError");
            errors.Add(error);

            object result = converter.Convert(errors.AsReadOnly(), null, null, null);

            Assert.AreEqual("TestError", result);
        }

        [TestMethod]
        public void ShouldReturnTheInnerExceptionMessageOfATargetInvocationException()
        {
            ErrorConverter converter = new ErrorConverter();

            List<ValidationError> errors = new List<ValidationError>();
            ValidationError error = new ValidationError(new ExceptionValidationRule(), new object());
            error.Exception = new TargetInvocationException(null, new Exception("TestError"));
            errors.Add(error);

            object result = converter.Convert(errors.AsReadOnly(), null, null, null);

            Assert.AreEqual("TestError", result);
        }

        [TestMethod]
        public void ShouldReturnTheErrorContentOfTheFirstItemInTheCollection()
        {
            ErrorConverter converter = new ErrorConverter();

            List<ValidationError> errors = new List<ValidationError>();
            ValidationError error = new ValidationError(new ExceptionValidationRule(), new object());
            error.ErrorContent = "TestError";
            errors.Add(error);

            object result = converter.Convert(errors.AsReadOnly(), null, null, null);

            Assert.AreEqual("TestError", result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowNotImplementedExceptionInConvertBack()
        {
            ErrorConverter converter = new ErrorConverter();

            var convertedValue = converter.ConvertBack(null, null, null, null);
        }
    }
}
