using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace technet.MVC.Validators
{
    public class MinimumFileSizeValidator
        : ValidationAttribute, IClientValidatable
    {        
        private string _errorMessage = "{0} can not be smaller than {1} MB";     

        /// <summary>
        /// Minimum file size in MB
        /// </summary>
        public double MinimumFileSize { get; private set; }

        /// <param name="minimumFileSize">MinimumFileSize file size in MB</param>
        public MinimumFileSizeValidator(
           double minimumFileSize)
            : base()
        {
            MinimumFileSize = minimumFileSize;
        }

        public override bool IsValid(
             object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!IsValidMinimumFileSize((value as HttpPostedFileBase).ContentLength))
            {
                ErrorMessage = String.Format(_errorMessage, "{0}", MinimumFileSize);
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return String.Format(_errorMessage, name, MinimumFileSize);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "minimumfilesize"
            };

            clientValidationRule.ValidationParameters.Add("size", MinimumFileSize);

            return new[] { clientValidationRule };
        }

        private bool IsValidMinimumFileSize(
            int fileSize)
        {
            return ConvertBytesToMegabytes(fileSize) >= MinimumFileSize;
        }

        private double ConvertBytesToMegabytes(
            int bytes)
        {
            return (bytes / 1024f) / 1024f;
        }        
    }
}