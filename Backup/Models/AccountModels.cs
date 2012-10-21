using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace LifeTube.Models
{
    public class SentForgotPasswordLinkModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
            
    }
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
       // [StringLength(100, ErrorMessage = "The {0} must be at least {12} characters long.", MinimumLength = 12)]
        [Display(Name = "Reset PIN")]
        public string resetPin { get; set; }

    }

    public class ChangePasswordModel
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

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        //[Required]
        //[Display(Name = "User name")]
        //public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }


        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        public string phone { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int profileId{get;set;}
        public bool rootUser { get; set; }

        public int createdBy { get; set; }


        public DateTime createdDate { get; set; }

        public int lastModifiedBy{ get; set; }

    
        public DateTime lastModifiedDate { get; set; }


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
    public class EditProfileModel
    {
        public int profileId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
       // public string displayName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string homeTown { get; set; }
        public string studiedAt { get; set; }
        public string workAt { get; set; }
        public string personalContact { get; set; }
        public string homeContact { get; set; }
        public string bloodGroup { get; set; }
        public string birthPlace { get; set; }
        public string hobby { get; set; }
        public DateTime lastModifiedOn { get; set; }
    }
    public class Search
    {
        public int userid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class SearchUser
    {
        public int userid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
   
   
    public class UserCicle
    {
        public int userCircleId { get; set; }
    }
   
}
