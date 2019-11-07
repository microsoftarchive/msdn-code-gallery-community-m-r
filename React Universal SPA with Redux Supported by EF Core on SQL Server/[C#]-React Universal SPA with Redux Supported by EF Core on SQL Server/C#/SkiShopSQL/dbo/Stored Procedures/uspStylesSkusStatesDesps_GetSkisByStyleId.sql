CREATE PROCEDURE [dbo].[uspStylesSkusStatesDesps_GetSkisByStyleId]
	@styleId int
AS
	SET NOCOUNT ON;

	SELECT StyleId, ImageBig FROM dbo.Styles 
		WHERE StyleId = @styleId;

	SELECT AverageRatings, ReviewCount, SoldOut FROM dbo.StyleStates
		WHERE StyleId = @styleId;

	SELECT SkuId, Size, Quantity FROM Skus
		WHERE StyleId = @styleId AND SoftDeleted = 0;

	SELECT DisplayIndex, DescText FROM Descriptions
		WHERE StyleId = @styleId