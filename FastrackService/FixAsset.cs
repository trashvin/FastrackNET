
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace FASTService
{

using System;
    using System.Collections.Generic;
    
public partial class FixAsset
{

    public int FixAssetID { get; set; }

    public string Model { get; set; }

    public string SerialNumber { get; set; }

    public string AssetTag { get; set; }

    public string Brand { get; set; }

    public string Remarks { get; set; }

    public Nullable<System.DateTime> AcquisitionDate { get; set; }

    public Nullable<System.DateTime> ExpiryDate { get; set; }

    public Nullable<int> IssuerID { get; set; }

    public Nullable<int> LocationID { get; set; }

    public int AssetTypeID { get; set; }

    public int AssetStatusID { get; set; }

    public int AssetClassID { get; set; }

}

}
