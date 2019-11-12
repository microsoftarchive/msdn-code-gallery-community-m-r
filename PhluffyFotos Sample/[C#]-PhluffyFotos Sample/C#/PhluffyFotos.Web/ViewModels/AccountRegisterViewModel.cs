namespace PhluffyFotos.Web.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using PhluffyFotos.Web.Validations;

    public class AccountRegisterViewModel
    {
        [Required]
        [DisplayName("Username")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "The username is too short.")]
        [RegularExpression(@"^([a-z]|[A-Z]|\d)*$", ErrorMessage = "The username can only contain alphanumeric characters")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&\*\+/\?_`\{\}~]+)*)@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [MinRequiredPasswordLengthAttribute]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Album { get; set; }
    }
}