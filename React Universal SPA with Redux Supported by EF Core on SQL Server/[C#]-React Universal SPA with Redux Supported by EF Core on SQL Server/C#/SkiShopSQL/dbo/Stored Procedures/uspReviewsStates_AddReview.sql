CREATE PROCEDURE [dbo].[uspReviewsStates_AddReview]
	@styleId INT,
	@userId INT,
	@rating TINYINT,
	@reviewText NVARCHAR (MAX),
	@createdDateTime DATETIME2(7)
AS
	SET NOCOUNT ON;

	DECLARE @reviewId TABLE(ReviewId INT);

	INSERT INTO dbo.Reviews (StyleId, UserId, Rating, ReviewText, CreatedDateTime)
		OUTPUT inserted.ReviewId INTO @reviewId
		VALUES (@styleId, @userId, @rating, @reviewText, @createdDateTime);

	SELECT ReviewId FROM @reviewId;
	
	SELECT AverageRatings, ReviewCount, SoldOut 
		FROM dbo.StyleStates 
		WHERE StyleId = @styleId;