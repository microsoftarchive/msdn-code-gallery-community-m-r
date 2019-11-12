// <copyright file="Tracer.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Tracer.cs                     
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
    using System.Diagnostics;

    /// <summary>
    /// The tracer class is used to perform tracing inside of a Silverlight application. Just construct a new instance to start tracing and then dispose of it to complete tracing.
    /// </summary>
    public class Tracer : IDisposable
    {
        /// <summary>
        /// Tracks the start time of the trace.
        /// </summary>
        private readonly DateTime startTime;

        /// <summary>
        /// Stores information about this trace event.
        /// </summary>
        private readonly TraceEntry entry;

        /// <summary>
        /// Indicates if the class has been diposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tracer"/> class with the specified operationName name.
        /// </summary>
        /// <param name="operationName">Identifies the name of the operation that is being traced.</param>
        public Tracer(string operationName) : this(operationName, Guid.Empty)
        {
            // this constructor does not do any additional logic
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tracer"/> class with the specified operationName name and activityId.
        /// </summary>
        /// <param name="operationName">Identifies the name of the operation that is being traced.</param>
        /// <param name="activityId">Indicates the correlation activity id if it is required.</param>
        public Tracer(string operationName, Guid activityId)
        {
            if (!LoggerManager.Started)
            {
                throw new InvalidOperationException("Cannot perform a trace without first starting LoggerManager.");
            }

            this.startTime = DateTime.Now;
            this.entry = new TraceEntry
            {
                ActivityId = activityId,
                Operation = operationName,
                Method = GetExecutingMethodName()
            };
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Tracer"/> class.
        /// </summary>
        ~Tracer()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if Dispose is being called from the Dispose method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.entry.Duration = DateTime.Now - this.startTime;
                LoggerManager.Manager.Write(this.entry);
                this.disposed = true;
            }
        }

        /// <summary>
        /// Gets the name of the method that is using the <see cref="Tracer"/> class.
        /// </summary>
        /// <returns>The name of the method that is constructing a new instance of the Tracer class.</returns>
        /// <remarks>This method can only be called from the constructor, otherwise it will not work correctly.</remarks>
        private static string GetExecutingMethodName()
        {
            StackTrace trace = new StackTrace();
            var frames = trace.GetFrames();
            if (frames == null)
            {
                return null;
            }

            foreach (var frame in frames)
            {
                var method = frame.GetMethod();
                if (method.DeclaringType == typeof(Tracer))
                {
                    continue;
                }

                return method.Name;
            }

            return null;
        }
    }
}
