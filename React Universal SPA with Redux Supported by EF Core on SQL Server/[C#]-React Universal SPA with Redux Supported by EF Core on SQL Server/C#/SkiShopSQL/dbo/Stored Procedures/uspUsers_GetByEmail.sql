CREATE PROCEDURE [dbo].[uspUsers_GetByEmail]
	@email NVARCHAR(100)
AS
	SET NOCOUNT ON;
	
	SELECT UserId, FirstName + SPACE(1) + LastName AS FullName, Email, ScreenName 
	FROM dbo.UserIdentities WHERE Email = @email