CREATE PROCEDURE [dbo].[uspStylesStatesSkus_getByStyleIds]
	@styleIds dbo.IdIntTableType READONLY
AS
	SET NOCOUNT ON;
	
	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular
	FROM dbo.vwStyle WHERE StyleId IN (SELECT Id FROM @styleIds);
	
	SELECT StyleId, ImageBig FROM dbo.Styles WHERE StyleId IN (SELECT Id FROM @styleIds);

	SELECT StyleId, AverageRatings, ReviewCount, SoldOut 
	FROM dbo.StyleStates WHERE StyleId IN (SELECT Id FROM @styleIds);

	SELECT StyleId, SkuId, Size, Quantity FROM dbo.Skus WHERE StyleId IN (SELECT Id FROM @styleIds);