// <copyright file="ConfigurationLoadException.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ConfigurationLoadException.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger
{
    using System;

    /// <summary>
    /// A class used for throwing an exception when the configuration fails to load for LAgger.
    /// </summary>
    public class ConfigurationLoadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationLoadException"/> class.
        /// </summary>
        public ConfigurationLoadException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationLoadException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ConfigurationLoadException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationLoadException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ConfigurationLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
