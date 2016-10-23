CREATE PROCEDURE [dbo].[Bookmark_Create]
	@Name nvarchar(50),
	@UserId nvarchar(50),
	@Uri nvarchar(MAX)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Bookmark (Name, UserId, Uri) SELECT @Name, @UserId, @Uri
	
	SELECT * FROM Bookmark WHERE Id = SCOPE_IDENTITY()
