namespace MyCompany.Travel.Client.Desktop.Validations
{
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Rule for required fields
    /// </summary>
    public class RequiredValidationRule : ValidationRule
    {
        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult"/> object.
        /// </returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null || String.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, "field required");

            return new ValidationResult(true, null);
        }
    }
}

