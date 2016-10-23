CREATE PROCEDURE [dbo].[Bookmark_GetByIdAndUser]
	@Id int,
	@UserId nvarchar(50)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM Bookmark WHERE Id = @Id AND UserId = @UserId
