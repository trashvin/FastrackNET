<<<<<<< HEAD
﻿CREATE VIEW [dbo].[vwAssetAssignments]
	AS SELECT        dbo.AssetAssignment.AssetAssignmentID, dbo.AssetAssignment.EmployeeID, dbo.AssetAssignment.FixAssetID, dbo.FixAsset.Model, dbo.FixAsset.SerialNumber, dbo.FixAsset.AssetTag, dbo.FixAsset.Brand, 
                         dbo.FixAsset.Remarks AS AssetRemarks, dbo.FixAsset.AssetClassID, dbo.AssetClass.ClassDescription, dbo.FixAsset.AssetStatusID, dbo.AssetStatus.StatusDescription, dbo.FixAsset.AssetTypeID, 
                         dbo.AssetType.TypeDescription, dbo.FixAsset.LocationID, dbo.Location.LocationName, dbo.Location.Country, dbo.AssetAssignment.AssignmentStatusID, 
                         dbo.AssignmentStatus.StatusDescription AS AssignmentStatus, dbo.AssetAssignment.DateAssigned, dbo.AssetAssignment.DateReleased, dbo.AssetAssignment.Remarks AS AssignmentRemarks, 
                         dbo.AssetAssignment.FromID, dbo.AssetAssignment.ToID
FROM            dbo.AssetStatus INNER JOIN
                         dbo.AssetType INNER JOIN
                         dbo.AssignmentStatus INNER JOIN
                         dbo.AssetAssignment ON dbo.AssignmentStatus.AssignmentStatusID = dbo.AssetAssignment.AssignmentStatusID INNER JOIN
                         dbo.FixAsset ON dbo.AssetAssignment.FixAssetID = dbo.FixAsset.FixAssetID FULL OUTER JOIN
                         dbo.Location ON dbo.FixAsset.LocationID = dbo.Location.LocationID ON dbo.AssetType.AssetTypeID = dbo.FixAsset.AssetTypeID INNER JOIN
=======
﻿CREATE VIEW [dbo].[vwAssetAssignments]
	AS SELECT        dbo.AssetAssignment.AssetAssignmentID, dbo.AssetAssignment.EmployeeID, dbo.AssetAssignment.FixAssetID, dbo.FixAsset.Model, dbo.FixAsset.SerialNumber, dbo.FixAsset.AssetTag, dbo.FixAsset.Brand, 
                         dbo.FixAsset.Remarks AS AssetRemarks, dbo.FixAsset.AssetClassID, dbo.AssetClass.ClassDescription, dbo.FixAsset.AssetStatusID, dbo.AssetStatus.StatusDescription, dbo.FixAsset.AssetTypeID, 
                         dbo.AssetType.TypeDescription, dbo.FixAsset.LocationID, dbo.Location.LocationName, dbo.Location.Country, dbo.AssetAssignment.AssignmentStatusID, 
                         dbo.AssignmentStatus.StatusDescription AS AssignmentStatus, dbo.AssetAssignment.DateAssigned, dbo.AssetAssignment.DateReleased, dbo.AssetAssignment.Remarks AS AssignmentRemarks, 
                         dbo.AssetAssignment.FromID, dbo.AssetAssignment.ToID
FROM            dbo.AssetStatus INNER JOIN
                         dbo.AssetType INNER JOIN
                         dbo.AssignmentStatus INNER JOIN
                         dbo.AssetAssignment ON dbo.AssignmentStatus.AssignmentStatusID = dbo.AssetAssignment.AssignmentStatusID INNER JOIN
                         dbo.FixAsset ON dbo.AssetAssignment.FixAssetID = dbo.FixAsset.FixAssetID FULL OUTER JOIN
                         dbo.Location ON dbo.FixAsset.LocationID = dbo.Location.LocationID ON dbo.AssetType.AssetTypeID = dbo.FixAsset.AssetTypeID INNER JOIN
>>>>>>> origin/master
                         dbo.AssetClass ON dbo.FixAsset.AssetClassID = dbo.AssetClass.AssetClassID ON dbo.AssetStatus.AssetStatusID = dbo.FixAsset.AssetStatusID