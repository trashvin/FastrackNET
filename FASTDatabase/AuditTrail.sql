CREATE TABLE [dbo].[AuditTrail]
(
	[AuditTrailID] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [EmployeeID] INT NOT NULL, 
    [Action] NVARCHAR(50) NOT NULL, 
    [AdditionalInformation] NVARCHAR(MAX) NOT NULL, 
    [AssetTag] NVARCHAR(50) NOT NULL, 
    [AssignmentID] NVARCHAR(50) NOT NULL
)
