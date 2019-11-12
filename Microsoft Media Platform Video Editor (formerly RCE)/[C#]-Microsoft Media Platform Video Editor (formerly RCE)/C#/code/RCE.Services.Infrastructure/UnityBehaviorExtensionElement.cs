// <copyright file="UnityBehaviorExtensionElement.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UnityBehaviorExtensionElement.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Infrastructure
{
    using System;
    using System.Configuration;
    using System.ServiceModel.Configuration;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    /// <summary>
    /// Represents a configuration element that contains sub-elements that specify behavior extensions, which enable the user to customize service or endpoint behaviors.
    /// </summary>
    public class UnityBehaviorExtensionElement : BehaviorExtensionElement
    {
        /// <summary>
        /// The default path for the unity configuration.
        /// </summary>
        private const string DefaultUnityConfigurationSectionPath = "unity";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityBehaviorExtensionElement"/> class.
        /// </summary>
        public UnityBehaviorExtensionElement()
        {
            this.UnityConfigurationSectionPath = DefaultUnityConfigurationSectionPath;
        }

        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        /// <returns>A <see cref="T:System.Type" />.</returns>
        /// <value>The behavior type being used.</value>
        public override Type BehaviorType
        {
            get { return typeof(UnityServiceBehavior); }
        }

        /// <summary>
        /// Gets or sets the name of the container.
        /// </summary>
        /// <value>The name of the container.</value>
        [ConfigurationProperty("containerName")]
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the unity configuration section path.
        /// </summary>
        /// <value>The unity configuration section path.</value>
        [ConfigurationProperty("unityConfigurationSectionPath", DefaultValue = DefaultUnityConfigurationSectionPath)]
        public string UnityConfigurationSectionPath { get; set; }

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected override object CreateBehavior()
        {
            UnityConfigurationSection unitySection = ConfigurationManager.GetSection(this.UnityConfigurationSectionPath) as UnityConfigurationSection;
            
            if (unitySection == null)
            {
                throw new ArgumentException("unitySection");
            }

            IUnityContainer container = null;
            
            try
            {
                container = new UnityContainer();

                if (this.ContainerName == null)
                {
                    unitySection.Containers.Default.Configure(container);
                }
                else
                {
                    unitySection.Containers[this.ContainerName].Configure(container);
                }

                return container.Resolve<UnityServiceBehavior>();
            }
            finally
            {
                if (container != null)
                {
                    container.Dispose();
                }
            }
        }
    }
}