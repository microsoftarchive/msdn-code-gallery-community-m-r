// <copyright file="InputValidationException.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: InputValidationException.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;

    /// <summary>
    /// Exception for Input Validation.
    /// </summary>
    public class InputValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationException"/> class.
        /// </summary>
        public InputValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public InputValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="ex">The inner exception.</param>
        public InputValidationException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
