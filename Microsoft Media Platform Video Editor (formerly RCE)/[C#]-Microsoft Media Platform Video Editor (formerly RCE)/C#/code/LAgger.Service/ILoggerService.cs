// <copyright file="ILoggerService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ILoggerService.cs                     
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
    using System.ServiceModel;

    /// <summary>
    /// Defines the service contract to use for LAgger.
    /// </summary>
    [ServiceContract(Namespace = "http://schemas.microsoft.com/rce/2.1/LAgger/")]
    public interface ILoggerService
    {
        /// <summary>
        /// Asynchronous method to collect Entry objects for backend processing.
        /// </summary>
        /// <param name="entries">The <see cref="LogEntry"/> object for backend processing.</param>
        [OperationContract]
        void LogEntries(Entry[] entries);

        /// <summary>
        /// This method is used to load configuration stettings for LAgger.
        /// </summary>
        /// <returns>The <see cref="LoggingConfiguration"/> is used to load configuration settings for LAgger.</returns>
        [OperationContract]
        LoggingConfiguration DistributeConfiguration();
    }
}
