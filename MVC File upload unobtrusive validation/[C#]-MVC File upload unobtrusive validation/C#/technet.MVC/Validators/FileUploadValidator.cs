using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;
using System.Web.Mvc;

using technet.MVC.ExtentionMethods;

namespace technet.MVC.Validators
{   
    public class FileUploadValidator
        : ValidationAttribute, IClientValidatable
    {   
        private MinimumFileSizeValidator _minimumFileSizeValidator;
        private MaximumFileSizeValidator _maximumFileSizeValidator;
        private ValidFileTypeValidator   _validFileTypeValidator;                 
        
        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public FileUploadValidator(              
              params string[] validFileTypes)
            : base()
        {
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }
        
        /// <param name="maximumFileSize">Maximum file size in MB</param>
        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public FileUploadValidator(              
              double maximumFileSize
            , params string[] validFileTypes)
            : base()
        {
            _maximumFileSizeValidator = new MaximumFileSizeValidator(maximumFileSize);
            _validFileTypeValidator   = new ValidFileTypeValidator(validFileTypes);
        }

        /// <param name="minimumFileSize">MinimumFileSize file size in MB</param>
        /// <param name="maximumFileSize">Maximum file size in MB</param>
        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public FileUploadValidator(
              double minimumFileSize
            , double maximumFileSize
            , params string[] validFileTypes)
            : base()
        {
            _minimumFileSizeValidator = new MinimumFileSizeValidator(minimumFileSize);
            _maximumFileSizeValidator = new MaximumFileSizeValidator(maximumFileSize);
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }

        protected override ValidationResult IsValid(
              object value
            , ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            try
            {
                if (value.GetType() != typeof(HttpPostedFileWrapper))
                {
                    throw new InvalidOperationException("");
                }

                var errorMessage = new StringBuilder();
                var file = value as HttpPostedFileBase;

                if (_minimumFileSizeValidator != null)
                {
                    if (!_minimumFileSizeValidator.IsValid(file))
                    {
                        errorMessage.Append(String.Format("{0}. ", _minimumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)));                        
                    }
                }

                if (_maximumFileSizeValidator!=null)
                {
                    if (!_maximumFileSizeValidator.IsValid(file))
                    {
                        errorMessage.Append(String.Format("{0}. ", _maximumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)));
                    }
                }

                if (_validFileTypeValidator != null)
                {
                    if (!_validFileTypeValidator.IsValid(file))
                    {
                        errorMessage.Append(String.Format("{0}. ", _validFileTypeValidator.FormatErrorMessage(validationContext.DisplayName)));
                    }
                }

                if (String.IsNullOrEmpty(errorMessage.ToString()))
                {
                    return ValidationResult.Success;
                }
                else 
                {
                    return new ValidationResult(errorMessage.ToString());
                }
            }
            catch(Exception excp)
            {
                return new ValidationResult(excp.Message);
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "fileuploadvalidator"
            };
            
            var clientvalidationmethods  = new List<string>();
            var parameters               = new List<string>();
            var errorMessages            = new List<string>();

            if (_minimumFileSizeValidator != null) 
            {
                clientvalidationmethods.Add(_minimumFileSizeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(_minimumFileSizeValidator.MinimumFileSize.ToString());
                errorMessages.Add(_minimumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            if (_maximumFileSizeValidator != null)
            {
                clientvalidationmethods.Add(_maximumFileSizeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(_maximumFileSizeValidator.MaximumFileSize.ToString());
                errorMessages.Add(_maximumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            if (_validFileTypeValidator != null)
            {
                clientvalidationmethods.Add(_validFileTypeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(String.Join(",", _validFileTypeValidator.ValidFileTypes));
                errorMessages.Add(_validFileTypeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            clientValidationRule.ValidationParameters.Add("clientvalidationmethods", clientvalidationmethods.ToConcatenatedString(","));
            clientValidationRule.ValidationParameters.Add("parameters"             , parameters.ToConcatenatedString("|"));
            clientValidationRule.ValidationParameters.Add("errormessages"          , errorMessages.ToConcatenatedString(","));

            yield return clientValidationRule;
        }              

        private double ConvertBytesToMegabytes(
            long bytes) 
        {
            return (bytes / 1024f) / 1024f;
        } 
    }
}