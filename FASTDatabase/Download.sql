﻿CREATE TABLE [dbo].[Download]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Link] NVARCHAR(MAX) NOT NULL, 
    [QRCode] IMAGE NOT NULL, 
    [ReleaseNotes] NVARCHAR(MAX) NOT NULL
)
