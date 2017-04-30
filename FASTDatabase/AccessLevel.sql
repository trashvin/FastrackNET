CREATE TABLE [dbo].[AccessLevel]
(
	[AccessLevelID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccessMode] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL
)
