using System.Collections.Generic;
using System.Web.Mvc;

namespace MyEvents.Web.Validators
{
    /// <summary>
    /// Greater than validation attribute.
    /// </summary>
    public class DateGreaterThanAttribute : Model.Validators.DateGreaterThanAttribute, IClientValidatable
    {
        private readonly string _propertyName;

        /// <summary>
        /// Greater than attribute constructor.
        /// </summary>
        /// <param name="propertyName"></param>
        public DateGreaterThanAttribute(string propertyName)
            : base(propertyName)
        {
            _propertyName = propertyName;
        }

        /// <summary>
        /// Returns client validation rules.
        /// </summary>
        /// 
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        /// <param name="metadata">The model metadata.</param><param name="context">The controller context.</param>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var validationRule = new ModelClientValidationRule();
            validationRule.ErrorMessage = ErrorMessageString;
            validationRule.ValidationType = "dategreaterthan";
            validationRule.ValidationParameters.Add("pastdate", _propertyName);

            yield return validationRule;
        }
    }
}
