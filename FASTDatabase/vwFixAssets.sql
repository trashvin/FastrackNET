<<<<<<< HEAD
﻿CREATE VIEW [dbo].[vwFixAssets]
	AS SELECT        f.FixAssetID, f.Model, f.SerialNumber, f.AssetTag, f.Brand, f.Remarks, f.AcquisitionDate, f.ExpiryDate, f.IssuerID, i.IssuerName, i.IssuerType, f.LocationID, l.LocationName, l.Country, f.AssetTypeID, 
                         at.TypeDescription, f.AssetClassID, ac.ClassDescription, f.AssetStatusID, ast.StatusDescription
FROM            dbo.FixAsset AS f LEFT OUTER JOIN
                         dbo.AssetType AS at ON f.AssetTypeID = at.AssetTypeID LEFT OUTER JOIN
                         dbo.AssetStatus AS ast ON f.AssetStatusID = ast.AssetStatusID LEFT OUTER JOIN
                         dbo.AssetClass AS ac ON f.AssetClassID = ac.AssetClassID LEFT OUTER JOIN
                         dbo.Location AS l ON f.LocationID = l.LocationID LEFT OUTER JOIN
=======
﻿CREATE VIEW [dbo].[vwFixAssets]
	AS SELECT        f.FixAssetID, f.Model, f.SerialNumber, f.AssetTag, f.Brand, f.Remarks, f.AcquisitionDate, f.ExpiryDate, f.IssuerID, i.IssuerName, i.IssuerType, f.LocationID, l.LocationName, l.Country, f.AssetTypeID, 
                         at.TypeDescription, f.AssetClassID, ac.ClassDescription, f.AssetStatusID, ast.StatusDescription
FROM            dbo.FixAsset AS f LEFT OUTER JOIN
                         dbo.AssetType AS at ON f.AssetTypeID = at.AssetTypeID LEFT OUTER JOIN
                         dbo.AssetStatus AS ast ON f.AssetStatusID = ast.AssetStatusID LEFT OUTER JOIN
                         dbo.AssetClass AS ac ON f.AssetClassID = ac.AssetClassID LEFT OUTER JOIN
                         dbo.Location AS l ON f.LocationID = l.LocationID LEFT OUTER JOIN
>>>>>>> origin/master
                         dbo.Issuer AS i ON f.IssuerID = i.IssuerID