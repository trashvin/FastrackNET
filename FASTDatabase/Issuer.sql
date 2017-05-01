CREATE TABLE [dbo].[Issuer]
(
	[IssuerID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IssuerType] INT NOT NULL, 
    [IssuerName] NVARCHAR(50) NOT NULL
)
