using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FASTWeb.Models
{
    public class AuditTrailFilter
    {
        [Display(Name="Start Date")]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "Employee ID")]
        [DataType(DataType.Text)]
        public int EmployeeID { get; set; }

        [Display(Name = "Fix Asset ID")]
        [DataType(DataType.Text)]
        public string AssetID { get; set; }

        [Display(Name = "Assignment ID")]
        [DataType(DataType.Text)]
        public string AssignmentID { get; set; }
    
    }
}