using System.Windows.Controls;

namespace MyEvents.Client.Organizer.Desktop.Validation
{
    /// <summary>
    /// Validate if a text is empty.
    /// </summary>
    public class EmpyTextValidation : ValidationRule
    {
        /// <summary>
        /// Validate text.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, string.Empty);

            string text = value.ToString();

            if (string.IsNullOrEmpty(text))
                return new ValidationResult(false, string.Empty);

            return new ValidationResult(true, string.Empty);
        }
    }
}
