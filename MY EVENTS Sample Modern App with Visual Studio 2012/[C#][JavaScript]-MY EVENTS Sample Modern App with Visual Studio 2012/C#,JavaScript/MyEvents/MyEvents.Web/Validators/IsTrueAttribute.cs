using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyEvents.Web.Validators
{
    /// <summary>
    /// Validates if a boolean is true.
    /// </summary>
    public class IsTrueAttribute : ValidationAttribute
    {
        #region Overrides of ValidationAttribute

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// 
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.
        /// </returns>
        /// <param name="value">The value to validate.</param><param name="validationContext">The context information about the validation operation.</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isValid = (bool)value;

            if (!isValid)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            return null;
        }

        #endregion
    }
}



