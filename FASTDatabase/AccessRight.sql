CREATE TABLE [dbo].[AccessRight]
(
	[AccessID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeID] INT NOT NULL, 
    [DepartmentID] INT NOT NULL, 
    [AccessLevel] TINYINT NOT NULL
)
