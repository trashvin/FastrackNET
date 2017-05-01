CREATE TABLE [dbo].[Table]
(
	[AssetAssignmentID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeID] INT NOT NULL, 
    [FixAssetID] INT NOT NULL, 
    [AssignmentStatusID] INT NOT NULL, 
    [DateAssigned] DATETIME NULL, 
    [DateReleased] DATETIME NULL, 
    [Remarks] NVARCHAR(MAX) NOT NULL, 
    [FromID] NVARCHAR(50) NOT NULL, 
    [ToID] NVARCHAR(50) NOT NULL, 
    [LastActivity] DATETIME NULL
)
