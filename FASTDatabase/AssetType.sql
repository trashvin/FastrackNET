CREATE TABLE [dbo].[AssetType]
(
	[AssetTypeID] INT NOT NULL PRIMARY KEY, 
    [TypeDescription] NVARCHAR(50) NOT NULL, 
    [AssetClassID] INT NOT NULL
)
