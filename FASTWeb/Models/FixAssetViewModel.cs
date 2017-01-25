using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FASTWeb.Models
{
    public class FixAssetViewModel
    {
        [Display(Name="Fix Asset ID")]
        public int FixAssetID { get; set; }

        [Required(ErrorMessage="This field is required.")]
        [Display(Name="Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Asset Tag")]
        public string AssetTag { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Acquisition Date")]
        [DataType(DataType.Date)]
        public DateTime? AcquisitionDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Issuer ID")]
        public int? IssuerID { get; set; }

        [Display(Name = "Issuer")]
        public string Issuer { get; set; }

        [Display(Name = "Location ID")]
        public int? LocationID { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Asset Type ID")]
        public int AssetTypeID { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetType { get; set; }

        [Display(Name = "Asset Status ID")]
        public int AssetStatusID { get; set; }

        [Display(Name = "Asset Status")]
        public string AssetStatus { get; set; }

        [Display(Name = "Asset Class")]
        public int AssetClassID { get; set; }

        [Display(Name = "Asset Class")]
        public string AssetClass { get; set; }
    }

    public class FixAssetExcelUploadModel
    {
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AssetTag { get; set; }
        public string Brand { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public int AssetTypeID { get; set; }
        public int AssetClassID { get; set; }
        public int AssetStatusID { get; set; }
    }
}