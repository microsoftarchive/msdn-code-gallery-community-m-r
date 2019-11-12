CREATE PROCEDURE [dbo].[uspReviewsStates_GetByStyleId]
	@styleId INT
AS
	SET NOCOUNT ON;
	
	SELECT AverageRatings, ReviewCount, SoldOut	from dbo.StyleStates 
		Where StyleId = @styleId;

	SELECT ReviewId, ScreenName, Rating, ReviewText, CONVERT(VARCHAR, CreatedDateTime, 100) AS CreatedDateTime 
		FROM dbo.Reviews AS r
		INNER JOIN dbo.UserIdentities AS u
		ON r.UserId = u.UserId
		WHERE r.StyleId = @styleId
		ORDER BY r.CreatedDateTime DESC;