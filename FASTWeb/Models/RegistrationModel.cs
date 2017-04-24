using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FASTWeb.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage="Employee ID is a required field.")]
        [Display(Name="Employee ID")]
        public int EmployeeID { get; set; }
    }
}