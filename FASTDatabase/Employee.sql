CREATE TABLE [dbo].[Employee]
(
	[EmployeeID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [ManagerID] INT NULL, 
    [Gender] NCHAR(10) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    [DepartmentID] INT NOT NULL, 
    [PositionID] INT NOT NULL, 
    [IsCompany] TINYINT NOT NULL, 
    [CompanyName] NVARCHAR(50) NOT NULL, 
    [Status] TINYINT NOT NULL
)
