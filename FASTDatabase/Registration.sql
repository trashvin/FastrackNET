CREATE TABLE [dbo].[Registration]
(
	[RegistrationID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeID] INT NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [Status] TINYINT NOT NULL, 
    [DateStamp] DATETIME NOT NULL
)
