CREATE TABLE [dbo].[Bookmark]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] nvarchar(50) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Uri] nvarchar(MAX) NOT NULL
)
