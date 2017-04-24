using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FASTWeb.Models
{
    public class LoginCredentialModel
    {
        [Required(ErrorMessage="Employee ID is required.")]
        [Display(Name = "Employee ID")]
        public int Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [Display(Name = "Last Login")]
        public string LastLoginMessage { get; set; }
    }
}