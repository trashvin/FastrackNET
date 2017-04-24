using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FASTWeb.Models
{
    public class AssetAssignmentViewModel
    {
    }

    public class AssetAssignmentUploadModel
    {
        public int EmployeeID { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
    }
}