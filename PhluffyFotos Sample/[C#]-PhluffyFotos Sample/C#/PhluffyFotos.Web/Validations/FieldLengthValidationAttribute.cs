namespace PhluffyFotos.Web.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using PhluffyFotos.Web.Controllers;

    public class MinRequiredPasswordLengthAttribute : ValidationAttribute
    {
        private int minPasswordLength;

        public MinRequiredPasswordLengthAttribute()
        {
            this.minPasswordLength = new AccountMembershipService().MinPasswordLength;
            this.ErrorMessage = string.Format(CultureInfo.CurrentCulture, "You must specify a password of {0} or more characters.", this.minPasswordLength);
        }

        public override bool IsValid(object value)
        {
            string strValue = value as string;

            if (!string.IsNullOrEmpty(strValue))
            {
                int len = strValue.Length;
                return len >= this.minPasswordLength;
            }

            return true;
        }
    }
}