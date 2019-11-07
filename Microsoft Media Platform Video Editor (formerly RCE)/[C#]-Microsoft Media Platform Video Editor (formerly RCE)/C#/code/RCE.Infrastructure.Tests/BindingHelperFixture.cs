// <copyright file="BindingHelperFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BindingHelperFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="BindingHelper"/>.
    /// </summary>
    [TestClass]
    public class BindingHelperFixture
    {
        /// <summary>
        /// Should raise OnPropertyChanged event when value is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenValueIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var bindingHelper = new BindingHelper();
            bindingHelper.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            bindingHelper.Value = new object();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Value", propertyChangedEventArgsArgument);
        }
    }
}
