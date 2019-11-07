namespace MyCompany.Travel.Client.Desktop.Validations
{
    using MyCompany.Travel.Client.Desktop.ViewModel;
    using System;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Rule for required fields
    /// </summary>
    public class SelectedEmployeeValidationRule : ValidationRule
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
            if (((TravelRequestFormViewModel)((((BindingExpression)(value)).DataItem))).SelectedEmployee == null)
                return new ValidationResult(false, "field required");

            return new ValidationResult(true, null);
        }
    }
}

