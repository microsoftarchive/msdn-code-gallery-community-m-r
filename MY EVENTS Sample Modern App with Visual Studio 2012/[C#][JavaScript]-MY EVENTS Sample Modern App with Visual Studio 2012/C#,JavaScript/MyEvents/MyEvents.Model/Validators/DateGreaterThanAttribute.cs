using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyEvents.Model.Validators
{
    /// <summary>
    /// Greater than validation attribute.
    /// </summary>
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        /// <summary>
        /// Greater than attribute constructor.
        /// </summary>
        /// <param name="propertyName"></param>
        public DateGreaterThanAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

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
            var propertyInfo = validationContext.ObjectType.GetProperty(_propertyName);

            var pastDate = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance, null);
            var futureDate = (DateTime)value;

            bool isValid = futureDate > pastDate;

            if (!isValid)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }
            
            return null;
        }
    }
}
