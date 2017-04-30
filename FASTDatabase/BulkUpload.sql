CREATE TABLE [dbo].[BulkUpload]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeID] INT NOT NULL, 
    [FilePath] NVARCHAR(MAX) NOT NULL, 
    [TotalRecords] INT NULL, 
    [TotalInserts] INT NULL, 
    [Status] INT NOT NULL, 
    [RequestDate] DATETIME NOT NULL, 
    [Type] INT NULL
)
