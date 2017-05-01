CREATE TABLE [dbo].[Configuration]
(
	[ConfigID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    [IsEncrypted] BIT NULL, 
    [CanExpire] BIT NULL, 
    [ExpiryDays] INT NULL, 
    [ModuleID] INT NULL, 
    [LastModified] DATETIME NOT NULL
)
