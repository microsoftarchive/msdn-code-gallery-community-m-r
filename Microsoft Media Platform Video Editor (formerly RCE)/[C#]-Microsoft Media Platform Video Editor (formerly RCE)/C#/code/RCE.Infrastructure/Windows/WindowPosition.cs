// <copyright file="WindowPosition.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WindowPosition.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows
{
    using System;

    public enum HorizontalWindowPosition
    {
        Center,
        Left,
        Right
    }

    public enum VerticalWindowPosition
    {
        Bottom,
        Center,
        Top,
    }

    public static class WindowPosition
    {
        public const double MarginsHeight = 25;

        public static double GetWindowLeft(HorizontalWindowPosition horizontalWindowPosition, double totalWidth)
        {
            switch (horizontalWindowPosition)
            {
                case HorizontalWindowPosition.Center:
                    return totalWidth / 3;
                case HorizontalWindowPosition.Right:
                    return totalWidth * 0.55;
                default:
                    return 0;
            }
        }

        public static double GetWindowTop(VerticalWindowPosition verticalWindowPosition, double totalHeight, double windowHeight)
        {
            switch (verticalWindowPosition)
            {
                case VerticalWindowPosition.Center:
                    if (double.IsNaN(windowHeight) || windowHeight == 0.0)
                    {
                        return totalHeight / 2.75;
                    }

                    return (totalHeight / 2) - (windowHeight / 2) - (MarginsHeight / 2);
                case VerticalWindowPosition.Bottom:
                    if (double.IsNaN(windowHeight) || windowHeight == 0.0)
                    {
                        return totalHeight * 0.5;
                    } 
                    
                    return totalHeight - windowHeight - MarginsHeight;
                default:
                    return 0;
            }
        }
    }
}
