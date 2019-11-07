namespace PhluffyFotos.Web.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class AccountLogOnViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
