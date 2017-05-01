CREATE TABLE [dbo].[Department]
(
	[DepartmentID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DepartmentName] NVARCHAR(50) NOT NULL, 
    [GroupName] NVARCHAR(50) NOT NULL
)
