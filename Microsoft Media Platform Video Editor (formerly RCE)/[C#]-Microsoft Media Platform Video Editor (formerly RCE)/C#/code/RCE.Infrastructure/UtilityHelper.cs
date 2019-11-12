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

namespace RCE.Infrastructure
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Windows;

    using RCE.Infrastructure.Models;

    /// <summary>
    /// Used for having some utilities functions which can be used through out the application.
    /// </summary>
    public sealed class UtilityHelper
    {
        /// <summary>
        /// Interval for position update.
        /// </summary>
        public const long PositionUpdateIntervalMillis = 250;

        /// <summary>
        /// The time diffrence between two mouse clicks for double click of the mouse.
        /// </summary>
        private const int MouseDoubleClickDuration = 5000000;

        /// <summary>
        /// The extention for the live smooth streaming file.
        /// </summary>
        private const string LiveSmoothStreamingExtension = ".ISML";

        /// <summary>
        /// The extention for the smooth streaming file.
        /// </summary>
        private const string SmoothStreamingExtension = ".ISM";

        /// <summary>
        /// The extention for the composite smooth streaming file.
        /// </summary>
        private const string CompositeSmoothStreamingExtension = ".CSM";

        /// <summary>
        /// Prevents a default instance of the UtilityHelper class from being created.
        /// </summary>
        private UtilityHelper()
        {
        }

        /// <summary>
        /// Gets the timedifference for mouse double click.
        /// </summary>
        /// <value>The duration of the mouse double click.</value>
        public static int MouseDoubleClickDurationValue
        {
            get { return MouseDoubleClickDuration; }
        }

        /// <summary>
        /// Returns the aspect ratio size corresponding to the given enum.
        /// </summary>
        /// <param name="selectedAspectRatio">AspectRatio enum value.</param>
        /// <returns><c>Size(4, 3)</c> If [Wide]; otherwise, <c>Size(16, 9)</c>.</returns>
        public static Size GetSelectedAspectRatio(AspectRatio selectedAspectRatio)
        {
            switch (selectedAspectRatio)
            {
                case AspectRatio.Wide:
                    return new Size(16, 9);
                default:
                    return new Size(4, 3);
            }
        }

        /// <summary>
        /// Returns the best fit size that can fit into the given max area maintaining the aspect ratio.
        /// </summary>
        /// <param name="maxWidth">Max possible width.</param>
        /// <param name="maxHeight">Max possible height.</param>
        /// <param name="width">Actual width of the asest.</param>
        /// <param name="height">Actual height of the asset.</param>
        /// <returns>Returns the best fit size in the given max area.</returns>
        public static Size GetBestFitSizeMaintainingAspectRatio(double maxWidth, double maxHeight, double width, double height)
        {
            Size size = new Size();
            if (height == 0 || width == 0)
            {
                size.Width = maxWidth;
                size.Height = maxHeight;
            }
            else
            {
                // Width is deciding factor. 
                size.Width = maxWidth;
                size.Height = maxWidth * (height / width);
                if (size.Height <= maxHeight)
                {
                    return size;
                }

                // Height is deciding factor.
                size.Height = maxHeight;
                size.Width = maxHeight * (width / height);
            }

            return size;
        }

        /// <summary>
        /// Checks if the property with the given name exists in
        /// <see cref="T:RCE.Infrastructure.Models.ImageAsset" />, <see cref="T:RCE.Infrastructure.Models.AudioAsset" />, 
        /// <see cref="T:RCE.Infrastructure.Models.VideoAsset" />, <see cref="T:RCE.Infrastructure.Models.FolderAsset" />.
        /// </summary>
        /// <param name="metadataField">Name of the field.</param>
        /// <returns>true if the field with the given name exist else false.</returns>
        public static bool IsMetadataFieldExist(string metadataField)
        {
            metadataField = metadataField.Replace(" ", string.Empty);

            if (typeof(ImageAsset).GetProperty(metadataField) != null)
            {
                return true;
            }

            if (typeof(AudioAsset).GetProperty(metadataField) != null)
            {
                return true;
            }

            if (typeof(VideoAsset).GetProperty(metadataField) != null)
            {
                return true;
            }

            if (typeof(FolderAsset).GetProperty(metadataField) != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Formats an exception message to be logged.
        /// </summary>
        /// <param name="ex">The exception being formatted.</param>
        /// <returns>The exception message formatted.</returns>
        public static string FormatExceptionMessage(Exception ex)
        {
            return FormatExceptionDetailMessage(ex.Message, ex.StackTrace, ex.InnerException);
        }

        /// <summary>
        /// Formats an exception message to be logged.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="stackTrace">The exception stack trace.</param>
        /// <returns>The exception message formatted.</returns>
        public static string FormatExceptionDetailMessage(string message, string stackTrace)
        {
            return FormatExceptionDetailMessage(message, stackTrace, null);
        }

        /// <summary>
        /// Formats an exception message to be logged.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="stackTrace">The exception stack trace.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <returns>The exception message formatted.</returns>
        public static string FormatExceptionDetailMessage(string message, string stackTrace, Exception innerException)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(CultureInfo.InvariantCulture, "Exception: {0}{1}", message, Environment.NewLine);
            builder.AppendFormat(CultureInfo.InvariantCulture, "Stack Trace: {0}", stackTrace);

            if (innerException != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0}Inner Exception: {1}{2}", Environment.NewLine, innerException.Message, Environment.NewLine);
                builder.AppendFormat(CultureInfo.InvariantCulture, "Inner Stack Trace: {0}", innerException.StackTrace);
            }

            return builder.ToString();
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