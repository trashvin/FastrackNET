CREATE TABLE [dbo].[ReasonCode]
(
	[ReasonID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ReasonDescription] NVARCHAR(MAX) NOT NULL, 
    [ModuleID] INT NULL
)
