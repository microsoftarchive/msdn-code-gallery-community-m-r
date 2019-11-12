// <copyright file="ConfigurationServiceExtensions.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ConfigurationServiceExtensions.cs                     
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
    using System.Collections.Generic;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    /// <summary>
    /// Defines extensions methods for the <see cref="IConfigurationService"/> service.
    /// </summary>
    public static class ConfigurationServiceExtensions
    {
        /// <summary>
        /// Retrieves the parameter value as datetime based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static DateTime? GetParameterValueAsDateTime(this IConfigurationService configurationService, string parameter)
        {
            DateTime result;
            string parameterValue = configurationService.GetParameterValue(parameter);

            if (DateTime.TryParse(parameterValue, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the parameter value as int based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static int? GetParameterValueAsInt(this IConfigurationService configurationService, string parameter)
        {
            int result;
            string parameterValue = configurationService.GetParameterValue(parameter);

            if (int.TryParse(parameterValue, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the parameter value as long based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static long? GetParameterValueAsLong(this IConfigurationService configurationService, string parameter)
        {
            long result;
            string parameterValue = configurationService.GetParameterValue(parameter);

            if (long.TryParse(parameterValue, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the parameter value as double based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static double? GetParameterValueAsDouble(this IConfigurationService configurationService, string parameter)
        {
            double result;
            string parameterValue = configurationService.GetParameterValue(parameter);

            if (double.TryParse(parameterValue, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the parameter value as Uri based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static Uri GetParameterValueAsUri(this IConfigurationService configurationService, string parameter)
        {
            return GetParameterValueAsUri(configurationService, parameter, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Retrieves the parameter value as Uri based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// /// <param name="kind">The kind of the Uri.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static Uri GetParameterValueAsUri(this IConfigurationService configurationService, string parameter, UriKind kind)
        {
            Uri result;
            string parameterValue = configurationService.GetParameterValue(parameter);

            if (Uri.TryCreate(parameterValue, kind, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the parameter value as IList of string based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// /// <param name="separator">The parameter separator.</param>
        /// <returns>The parameter values if the parameter exists;othwerwise an empty list.</returns>
        public static IList<string> GetParameterValueAsList(this IConfigurationService configurationService, string parameter, string separator)
        {
            IList<string> values = new List<string>();

            string parameterValue = configurationService.GetParameterValue(parameter);
            
            if (!string.IsNullOrEmpty(parameterValue))
            {
                string[] keyPairs = parameterValue.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string value in keyPairs)
                {
                    values.Add(value);
                }
            }

            return values;
        }

        /// <summary>
        /// Retrieves the parameter value as boolean based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public static bool? GetParameterValueAsBoolean(this IConfigurationService configurationService, string parameter)
        {
            bool result;
            string parameterValue = configurationService.GetParameterValue(parameter);

            if (bool.TryParse(parameterValue, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the current username.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The current username.</returns>
        public static string GetUserName(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValue("UserName");
        }

        /// <summary>
        /// Retrieves if gaps should be treated as errors
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>Wheter gaps should be treated as errors or not.</returns>
        public static bool GetTreatGapAsError(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsBoolean("TreatGapAsError").GetValueOrDefault(false);
        }

        /// <summary>
        /// Retrieves if fragments boundaries should be shown in RCE and snap to them when setting mark in/out.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>Wheter fragments boundaries be shown in RCE and snap to them when setting mark in/out or not.</returns>
        public static bool GetSnapToFragmentBoundaries(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsBoolean("SnapToFragmentBoundaries").GetValueOrDefault(false);
        }

        /// <summary>
        /// Gets the default asset frame rate for the Sub-Clip Tool window.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The default asset frame rate for the Sub-Clip Tool window.</returns>
        public static SmpteFrameRate GetDefaultFrameRate(this IConfigurationService configurationService)
        {
            var frameRate = configurationService.GetParameterValue("DefaultFrameRate");
            SmpteFrameRate smpteFrameRate;
            if (frameRate == null || !Enum.TryParse(frameRate, true, out smpteFrameRate))
            {
                smpteFrameRate = SmpteFrameRate.Smpte2997NonDrop;
            }

            return smpteFrameRate;
        }

        /// <summary>
        /// Gets the Uri for the Edge Time Beacon
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>Uri for the Edge Time Beacon service.</returns>
        public static Uri GetEdgeTimeBeaconUri(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsUri("EdgeTimeBeaconUrl");
        }

        /// <summary>
        /// Retrieves the ccontent distribution network prefix.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The current content distribution network prefix.</returns>
        public static string GetContentDistributionNetworkPrefix(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValue("CDN");
        }

        /// <summary>
        /// Retrieves the current project id <seealso cref="Uri"/>.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The current project id uri.</returns>
        public static Uri GetProjectId(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsUri("ProjectId");
        }

        /// <summary>
        /// Retrieves the metadata fields to show in the asset metadata information window.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The metadata fields to show.</returns>
        public static IList<string> GetMetadataFields(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsList("MetadataFields", ";");
        }

        /// <summary>
        /// Retrieves the max number of items to show in the media library.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The max number of items to show.</returns>
        public static int GetMaxNumberOfItems(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsInt("MaxNumberOfItems").GetValueOrDefault();
        }

        /// <summary>
        /// Retrieves the comments types available to use in the application.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The list of comments types.</returns>
        public static IList<string> GetCommentTypes(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsList("CommentTypes", ";");
        }

        /// <summary>
        /// Retrieves the level of undo supported by the application.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The level of undo.</returns>
        public static int GetUndoLevel(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsInt("UndoLevel").GetValueOrDefault();
        }

        /// <summary>
        /// Retrieves the title templates.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The available title templates is located.</returns>
        public static IDictionary<string, Uri> GetTitleTemplates(this IConfigurationService configurationService)
        {
            IDictionary<string, Uri> titleTemplates = new Dictionary<string, Uri>();

            string parameterValue = configurationService.GetParameterValue("TitleTemplates");

            if (!string.IsNullOrEmpty(parameterValue))
            {
                string[] keyPairs = parameterValue.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string keyPair in keyPairs)
                {
                    string[] titlePair = keyPair.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    Uri titleUri;

                    if (Uri.TryCreate(titlePair[1], UriKind.Absolute, out titleUri))
                    {
                        titleTemplates.Add(titlePair[0], titleUri);
                    }
                }
            }

            return titleTemplates;
        }

        /// <summary>
        /// Retrieves the values.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The available items is located.</returns>
        public static IEnumerable<KeyboardMapping> GetKeyboardMappings(this IConfigurationService configurationService)
        {
            IList<KeyboardMapping> keyboardMappings = new List<KeyboardMapping>();

            string parameterValue = configurationService.GetParameterValue("KeyboardMappings");

            if (!string.IsNullOrEmpty(parameterValue))
            {
                string[] keyPairs = parameterValue.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string keyPair in keyPairs)
                {
                    string[] mappingPair = keyPair.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    if (Uri.IsWellFormedUriString(mappingPair[1], UriKind.Absolute))
                    {
                        keyboardMappings.Add(new KeyboardMapping(mappingPair[0], mappingPair[1]));
                    }
                }
            }

            return keyboardMappings;
        }

        /// <summary>
        /// Retrieves the title template uri based on the title template name.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <param name="titleTemplate">The title template name.</param>
        /// <returns>The uri where the title template is located.</returns>
        public static Uri GetTitleTemplate(this IConfigurationService configurationService, string titleTemplate)
        {
            IDictionary<string, Uri> titleTemplates = configurationService.GetTitleTemplates();

            if (titleTemplates.ContainsKey(titleTemplate))
            {
                return titleTemplates[titleTemplate];
            }

            return null;
        }

        /// <summary>
        /// Retrieves the comment duration for the comments.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The comment duration.</returns>
        public static int? GetCommentDuration(this IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsInt("CommentDurationInSeconds");
        }
    }
}
