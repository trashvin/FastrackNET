using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FASTWeb.Models
{
    public class ChangePasswordModel
    {
        [Required (ErrorMessage="This field is required.")]
        [Display(Name="Employee ID")]
        public int Username { get; set; }

        [Required(ErrorMessage = "Old Password is required.")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


    }
}