// <copyright file="UtilityHelper.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UtilityHelper.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Provides Helper methods.
    /// </summary>
    public sealed class UtilityHelper
    {
        /// <summary>
        /// The extension for the live smooth streaming file.
        /// </summary>
        private const string LiveSmoothStreamingExtension = ".ISML";

        /// <summary>
        /// The extension for the smooth streaming file.
        /// </summary>
        private const string SmoothStreamingExtension = ".ISM";

        /// <summary>
        /// The extension for the composite smooth streaming file.
        /// </summary>
        private const string CompositeSmoothStreamingExtension = ".CSM";

        /// <summary>
        /// Prevents a default instance of the <see cref="UtilityHelper"/> class from being created.
        /// </summary>
        private UtilityHelper()
        {
        }

        /// <summary>
        /// Formats an exception message to be logged.
        /// </summary>
        /// <param name="exception">The exception being formatted.</param>
        /// <returns>The exception message formatted.</returns>
        public static string FormatExceptionMessage(Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(CultureInfo.InvariantCulture, "Exception: {0}{1}", exception.Message, Environment.NewLine);
            builder.AppendFormat(CultureInfo.InvariantCulture, "Stack Trace: {0}", exception.StackTrace);

            if (exception.InnerException != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0}Inner Exception: {1}{2}", Environment.NewLine, exception.InnerException.Message, Environment.NewLine);
                builder.AppendFormat(CultureInfo.InvariantCulture, "Inner Stack Trace: {0}", exception.InnerException.StackTrace);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines whether a file is a progressive download asset or not.
        /// </summary>
        /// <param name="file">The file being analyzed.</param>
        /// <returns>A true if the file is a progressive download asset;otherwise false.</returns>
        public static bool IsProgressiveDownloadFile(string file)
        {
            return !IsLiveAdaptiveStreaming(file) && !IsVodAdaptiveStreaming(file) && !IsCompositeAdaptiveStreaming(file);
        }

        /// <summary>
        /// Determines whether the a file is valid live smooth streaming uri.
        /// </summary>
        /// <param name="file">The file to be validated.</param>
        /// <returns>
        /// <c>True</c> if [is live adaptive streaming] [the specified file]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLiveAdaptiveStreaming(string file)
        {
            return !string.IsNullOrEmpty(file) && file.ToUpper(CultureInfo.InvariantCulture).Contains(LiveSmoothStreamingExtension);
        }

        /// <summary>
        /// Determines whether the file is valid smooth streaming uri.
        /// </summary>
        /// <param name="file">The file to be validated.</param>
        /// <returns>
        /// <c>True</c> if [is smooth streaming] [the specified file]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsVodAdaptiveStreaming(string file)
        {
            return !string.IsNullOrEmpty(file) && file.ToUpper(CultureInfo.InvariantCulture).Contains(SmoothStreamingExtension);
        }

        /// <summary>
        /// Determines whether the file is valid composite smooth streaming uri.
        /// </summary>
        /// <param name="file">The file to be validated.</param>
        /// <returns>
        /// <c>True</c> if [is composite smooth streaming] [the specified file]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompositeAdaptiveStreaming(string file)
        {
            return !string.IsNullOrEmpty(file) && file.ToUpper(CultureInfo.InvariantCulture).Contains(CompositeSmoothStreamingExtension);
        }

        /// <summary>
        /// Determines whether the <see cref="Uri"/> is valid live smooth streaming uri.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to be validated.</param>
        /// <returns>
        /// <c>True</c> if [is live adaptive streaming] [the specified URI]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLiveAdaptiveStreaming(Uri uri)
        {
            return uri != null && uri.OriginalString.ToUpper(CultureInfo.InvariantCulture).Contains(LiveSmoothStreamingExtension);
        }

        /// <summary>
        /// Determines whether the <see cref="Uri"/> is valid smooth streaming uri.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to be validated.</param>
        /// <returns>
        /// <c>True</c> if [is smooth streaming] [the specified URI]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAdaptiveStreaming(Uri uri)
        {
            return uri != null && uri.OriginalString.ToUpper(CultureInfo.InvariantCulture).Contains(SmoothStreamingExtension);
        }

        /// <summary>
        /// Determines whether the <see cref="Uri"/> is valid composite smooth streaming uri.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to be validated.</param>
        /// <returns>
        /// <c>True</c> if [is composite smooth streaming] [the specified URI]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompositeAdaptiveStreaming(Uri uri)
        {
            return uri != null && uri.OriginalString.ToUpper(CultureInfo.InvariantCulture).Contains(CompositeSmoothStreamingExtension);
        }
    }
}
