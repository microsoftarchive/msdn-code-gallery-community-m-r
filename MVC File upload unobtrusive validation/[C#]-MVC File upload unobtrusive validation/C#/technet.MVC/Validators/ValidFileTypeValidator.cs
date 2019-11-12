using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using technet.MVC.ExtentionMethods;

namespace technet.MVC.Validators
{
    public class ValidFileTypeValidator
        : ValidationAttribute, IClientValidatable
    {        
        private string _errorMessage = "{0} must be one of the following file types: {1}";    

        /// <summary>
        /// Valid file extentions
        /// </summary>
        public string[] ValidFileTypes { get; private set; }

        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public ValidFileTypeValidator(
            params string[] validFileTypes)
        {
            ValidFileTypes = validFileTypes;
        }

        public override bool IsValid(
            object value)
        {
            var file = value as HttpPostedFileBase;

            if (value == null || String.IsNullOrEmpty(file.FileName))
            {
                return true;
            }

            if (ValidFileTypes != null)
            {
                var validFileTypeFound = false;

                foreach (var validFileType in ValidFileTypes)
                {
                    var fileNameParts = file.FileName.Split('.');
                    if (fileNameParts[fileNameParts.Length - 1] == validFileType)
                    {
                        validFileTypeFound = true;
                        break;
                    }
                }

                if (!validFileTypeFound)
                {
                    ErrorMessage = String.Format(_errorMessage, "{0}", ValidFileTypes.ToConcatenatedString(","));
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return String.Format(_errorMessage, name, ValidFileTypes.ToConcatenatedString(","));
        }

        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validfiletype"
            };

            clientValidationRule.ValidationParameters.Add("filetypes", ValidFileTypes.ToConcatenatedString(","));

            return new[] { clientValidationRule };
        }
    }
}