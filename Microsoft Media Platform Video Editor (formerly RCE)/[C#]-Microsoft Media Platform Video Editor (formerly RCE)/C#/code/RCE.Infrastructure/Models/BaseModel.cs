// <copyright file="BaseModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BaseModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System.ComponentModel;

    /// <summary>
    /// Implements the <see cref="INotifyPropertyChanged"/>. 
    /// All the classes which require binding support are derived from this class.
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                try
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
                catch
                {
                }
            }
        }

        // public void OnPropertyChanged(Expression<Func<object>> expression)
        // {
        //    PropertyChangedEventHandler handler = this.PropertyChanged;
        //    if (handler == null)
        //    {
        //        return;
        //    }
        //    LambdaExpression lambda = expression;
        //    MemberExpression memberExpression = null;
        //    UnaryExpression unaryExpression = lambda.Body as UnaryExpression;
        //    if (unaryExpression != null)
        //    {
        //        memberExpression = unaryExpression.Operand as MemberExpression;
        //    }
        //    else
        //    {
        //        memberExpression = lambda.Body as MemberExpression;
        //    }
        //    PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
        //    try
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyInfo.Name));
        //    }
        //    catch
        //    {
        //    }
        // }
    }
}