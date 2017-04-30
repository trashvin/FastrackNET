CREATE TABLE [dbo].[FixAsset]
(
	[FixAssetID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Model] NVARCHAR(50) NOT NULL, 
    [SerialNumber] NVARCHAR(50) NOT NULL, 
    [AssetTag] NVARCHAR(50) NOT NULL, 
    [Brand] NVARCHAR(50) NOT NULL, 
    [Remarks] NVARCHAR(MAX) NOT NULL, 
    [AcquisitionDate] DATETIME NULL, 
    [ExpiryDate] DATETIME NULL, 
    [IssuerID] INT NULL, 
    [LocationID] INT NULL, 
    [AssetTypeID] INT NOT NULL, 
    [AssetStatusID] INT NOT NULL, 
    [AssetClassID] INT NOT NULL
)
