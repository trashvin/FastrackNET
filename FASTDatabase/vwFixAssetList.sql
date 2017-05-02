<<<<<<< HEAD
﻿CREATE VIEW [dbo].[vwFixAssetList]
	AS select f.FixAssetID, f.Model, f.SerialNumber, f.AssetTag, f.Brand, f.Remarks, f.AcquisitionDate, f.ExpiryDate,
f.IssuerID, i.IssuerName, i.IssuerType,
f.LocationID, l.LocationName, l.Country,
f.AssetTypeID, at.TypeDescription,
f.AssetClassID, ac.ClassDescription,
f.AssetStatusID , ast.StatusDescription--,*
from FixAsset f
left join AssetType at
on f.AssetTypeID = at.AssetTypeID
left join AssetStatus ast
on f.AssetStatusID = ast.AssetStatusID
left join AssetClass ac
on f.AssetClassID = ac.AssetClassID
left join Location l
on f.LocationID = l.LocationID
left join Issuer i
=======
﻿CREATE VIEW [dbo].[vwFixAssetList]
	AS select f.FixAssetID, f.Model, f.SerialNumber, f.AssetTag, f.Brand, f.Remarks, f.AcquisitionDate, f.ExpiryDate,
f.IssuerID, i.IssuerName, i.IssuerType,
f.LocationID, l.LocationName, l.Country,
f.AssetTypeID, at.TypeDescription,
f.AssetClassID, ac.ClassDescription,
f.AssetStatusID , ast.StatusDescription--,*
from FixAsset f
left join AssetType at
on f.AssetTypeID = at.AssetTypeID
left join AssetStatus ast
on f.AssetStatusID = ast.AssetStatusID
left join AssetClass ac
on f.AssetClassID = ac.AssetClassID
left join Location l
on f.LocationID = l.LocationID
left join Issuer i
>>>>>>> origin/master
on f.IssuerID = i.IssuerID