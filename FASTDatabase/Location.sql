CREATE TABLE [dbo].[Location]
(
	[LocationID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LocationName] NVARCHAR(50) NOT NULL, 
    [Country] NCHAR(10) NOT NULL
)
