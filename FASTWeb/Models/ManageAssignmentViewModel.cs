
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System;

using FASTProcess;
using Provider.ExtensionMethod;

namespace FASTWeb.Models
{
    public class ManageAssignmentViewModel
    {
       

        [Display(Name = "Assignment ID")]
        public int AssetAssignmentID { get; set; }

        [Display(Name = "Employee ID")]
        public Nullable<int> EmployeeID { get; set; }

        [Display(Name = "Asset ID")]
        public Nullable<int> FixAssetID { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset Tag")]
        public string AssetTag { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Asset Remarks")]
        public string AssetRemarks { get; set; }

        [Display(Name = "Class ID")]
        public Nullable<int> AssetClassID { get; set; }

        [Display(Name = "Class Description")]
        public string ClassDescription { get; set; }

        [Display(Name = "Asset Status ID")]
        public Nullable<int> AssetStatusID { get; set; }

        [Display(Name = "Asset Status")]
        public string StatusDescription { get; set; }

        [Display(Name = "Asset Type ID")]
        public Nullable<int> AssetTypeID { get; set; }

        [Display(Name = "Asset Type")]
        public string TypeDescription { get; set; }

        [Display(Name = "Location ID")]
        public Nullable<int> LocationID { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Assignment Status ID")]
        public Nullable<int> AssignmentStatusID { get; set; }

        [Display(Name = "Assignment Status")]
        public string AssignmentStatus { get; set; }

        [Display(Name = "Date Assigned")]
        public Nullable<System.DateTime> DateAssigned { get; set; }

        [Display(Name = "Date Released")]
        public Nullable<System.DateTime> DateReleased { get; set; }

        [Display(Name = "Assignment Remarks")]
        public string AssignmentRemarks { get; set; }

        [Display(Name = "From ID")]
        public string FromID { get; set; }

        [Display(Name = "To ID")]
        public string ToID { get; set; }

        private string GetEmployee(int employeeID)
        {
            if (employeeID > 0)
            {
                EmployeeProcess process = new EmployeeProcess();
                
                Common.Employee employee= process.GetByID(employeeID);

                return String.Format("{0} , {1} {2}", employee.LastName, employee.FirstName, employee.MiddleName);
            }

            return String.Empty;
        }

        public string GetOwner()
        {
            return GetEmployee(this.EmployeeID.Value);
        }

        public string GetFrom()
        {
            return GetEmployee(this.FromID.ToInteger());
        }

        public string GetTo()
        {
            return GetEmployee(this.ToID.ToInteger());
        }

        public void FillData(int assignID)
        {
            //AssetAssignmentModel assignment = assignProcess.GetAssignmentByID(assignID);

            //this.AssetAssignmentID = assignment.AssetAssignmentID;
            //this.AssetClassID = assignment.AssetClassID;
            //this.AssetRemarks = assignment.AssetRemarks;
            //this.AssetStatusID = assignment.AssetStatusID;
            //this.AssetTag = assignment.AssetTag;
            //this.AssetTypeID = assignment.AssetTypeID;
            //this.AssignmentRemarks = assignment.AssignmentRemarks;
            //this.AssignmentStatus = assignment.AssignmentStatus;
            //this.AssignmentStatusID = assignment.AssignmentStatusID;
            //this.Brand = assignment.Brand;
            //this.ClassDescription = assignment.ClassDescription;
            //this.Country = assignment.Country;
            //this.DateAssigned = assignment.DateAssigned;
            //this.DateReleased = assignment.DateReleased;
            //this.EmployeeID = assignment.EmployeeID;
            //this.FixAssetID = assignment.FixAssetID;
            //this.FromID = assignment.FromID;
            //this.LocationID = assignment.LocationID;
            //this.LocationName = assignment.LocationName;
            //this.Model = assignment.Model;
            //this.SerialNumber = assignment.SerialNumber;
            //this.StatusDescription = assignment.StatusDescription;
            //this.ToID = assignment.ToID;
            //this.TypeDescription = assignment.TypeDescription;
        }
    }
}