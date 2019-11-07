CREATE PROCEDURE [dbo].[uspStyles_GetPopularAndClearance]
AS
	SET NOCOUNT ON;

	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular,
		AverageRatings, ReviewCount, SoldOut
	FROM dbo.vwStyle
	WHERE StyleId IN (9, 17, 24);
	
	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular,
		AverageRatings, ReviewCount, SoldOut
	FROM dbo.vwStyle
	WHERE StyleId IN (3, 8, 22);