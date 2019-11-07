using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace IMChatApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginViewModel
    {
       // [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]        
        [Display(Name = "Name")]
        public string UserNick { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public bool UserType { get; set; }

       // [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

       // [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
  public enum Status
    {
        Online,
        Away,
        Busy,
        Offline
    }
    public class Css
    {
        public string color { get; set; }
        public string underline { get; set; }
        public string fstyle { get; set; }
        public string italic { get; set; }
        public string bold { get; set; }
    }


    public class user
    {
        //id, name, type, fontName, fontSize, fontColor, sex, age, friendsList, status, memberType

      public  string ConnectionId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<user> friendsList { get; set; }
        public string fontName { get; set; }
        public string fontSize { get; set; }
        public string fontColor { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string status { get; set; }
        public string memberType { get; set; }
        public string avator { get; set; }
            public string ContextName { get; set; }

        //public user Login(string UserName)
        //{ 


        //}

    }
    public class UsersRestricted
    {

        public int id { get; set; }
        public string name { get; set; }
        public string roomName { get; set; }
        public Restriction restriction { get; set; }

        public DateTime time { get; set; }

        public string restrictekBy { get; set; }
    }
 public  enum Restriction
    {
        BAN, MUTE, KICK
    }
  }