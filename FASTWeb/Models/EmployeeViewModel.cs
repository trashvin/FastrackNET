using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FASTWeb.Models
{
    public class EmployeeViewModel : Common.vwEmployeeList
    {
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage="This is a required field")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }

        public string Department { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Position")]
        public int PositionID { get; set; }
        public string Position { get; set; }
        public short IsCompany { get; set; }
        public bool IsCompany_booleanVal { get; set; }
        public string CompanyName { get; set; }

        public int Status { get; set; }
        public bool Status_booleanVal { get; set; }
    }
   
}