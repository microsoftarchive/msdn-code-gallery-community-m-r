// <copyright file="DesignerProperties.cs" company="Microsoft Corporation">
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.
// </copyright>
// Code from the Silverlight Toolkit project.
// http://www.codeplex.com/Silverlight
// http://blogs.msdn.com/delay/archive/2009/02/26/designerproperties-getisindesignmode-forrealz-how-to-reliably-detect-silverlight-design-mode-in-blend-and-visual-studio.aspx

namespace RCE
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <summary>
    /// Provides a custom implementation of DesignerProperties.GetIsInDesignMode
    /// to work around an issue.
    /// </summary>
    public static class DesignerProperties
    {
        /// <summary>
        /// Stores the computed InDesignMode value.
        /// </summary>
        private static bool? isInDesignMode;

        /// <summary>
        /// Gets a value indicating whether the application is in DesignMode.
        /// </summary>
        /// <value>A true if the application is in design mode;otherwise false.</value>
        public static bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual); }
        }

        /// <summary>
        /// Returns whether the control is in design mode (running under Blend
        /// or Visual Studio).
        /// </summary>
        /// <param name="element">The element from which the property value is
        /// read.</param>
        /// <returns>True if in design mode.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "element", Justification = "Matching declaration of System.ComponentModel.DesignerProperties.GetIsInDesignMode (which has a bug and is not reliable).")]
        private static bool GetIsInDesignMode(DependencyObject element)
        {
            if (!isInDesignMode.HasValue)
            {
                isInDesignMode =
                    (null == Application.Current) ||
                    Application.Current.GetType() == typeof(Application);
            }

            return isInDesignMode.Value;
        }
    }
}