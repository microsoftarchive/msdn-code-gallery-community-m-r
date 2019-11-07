CREATE PROCEDURE [dbo].[uspStyles_getByStyleId]
	@styleId int
AS
	SET NOCOUNT ON;

	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular
	FROM dbo.vwStyle WHERE StyleId = @styleId