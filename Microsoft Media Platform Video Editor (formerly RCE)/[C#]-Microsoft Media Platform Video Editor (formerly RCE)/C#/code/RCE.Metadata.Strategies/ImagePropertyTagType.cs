// <copyright file="ImagePropertyTagType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImagePropertyTagType.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Metadata.Strategies
{
    using System.Drawing.Imaging;

    /// <summary>
    /// Defines the data types of the <seealso cref="PropertyItem"/> defined at
    /// <see cref="http://msdn.microsoft.com/en-us/library/ms534414(VS.85).aspx"/>
    /// </summary>
    public enum ImagePropertyTagType
    {
        /// <summary>
        /// Specifies that the format is 4 bits per pixel, indexed.
        /// </summary>
        PixelFormat4bppIndexed = 0,
        
        /// <summary>
        /// Specifies that the value data member is an array of bytes.
        /// </summary>
        Byte = 1,

        /// <summary>
        /// Specifies that the value data member is a null-terminated ASCII string. 
        /// If you set the type data member of a PropertyItem object to PropertyTagTypeASCII, 
        /// you should set the length data member to the length of the string including the NULL terminator. For example, the string HELLO would have a length of 6.
        /// </summary>
        ASCII = 2,

        /// <summary>
        /// Specifies that the value data member is an array of unsigned short (16-bit) integers.
        /// </summary>
        Short = 3,

        /// <summary>
        /// Specifies that the value data member is an array of unsigned long (32-bit) integers.
        /// </summary>
        Long = 4,

        /// <summary>
        /// Specifies that the value data member is an array of pairs of unsigned long integers. 
        /// Each pair represents a fraction; the first integer is the numerator and the second integer is the denominator.
        /// </summary>
        Rational = 5,

        /// <summary>
        /// Specifies that the value data member is an array of bytes that can hold values of any data type. 
        /// </summary>
        Undefined = 7,

        /// <summary>
        /// Specifies that the value data member is an array of signed long (32-bit) integers.
        /// </summary>
        LONG = 9,

        /// <summary>
        /// Specifies that the value data member is an array of pairs of signed long integers. 
        /// Each pair represents a fraction; the first integer is the numerator and the second integer is the denominator.
        /// </summary>
        SRational = 10
    }
}