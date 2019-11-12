CREATE PROCEDURE [dbo].[uspStylesCategories_GetInitialHomePage]
AS
	SET NOCOUNT ON;

	SELECT CategoryId, CategoryName FROM dbo.Categories;

	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular,
		AverageRatings, ReviewCount, SoldOut
	FROM dbo.vwStyle
	WHERE StyleId IN (9, 17, 24)
	
	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular,
		AverageRatings, ReviewCount, SoldOut
	FROM dbo.vwStyle
	WHERE StyleId IN (3, 8, 22);