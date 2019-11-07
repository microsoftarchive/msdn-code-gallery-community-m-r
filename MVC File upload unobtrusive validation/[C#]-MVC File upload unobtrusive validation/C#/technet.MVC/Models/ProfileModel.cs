using System;
using System.Web;
using technet.MVC.Validators;

namespace technet.MVC.Models
{
    public class ProfileModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MinimumFileSizeValidator(0.5)]
        [MaximumFileSizeValidator(2.4)]
        [ValidFileTypeValidator("png")]
        //[FileUploadValidator(0.5, 2.4, "png")]
        public HttpPostedFileBase Avatar { get; set; }
    }
}