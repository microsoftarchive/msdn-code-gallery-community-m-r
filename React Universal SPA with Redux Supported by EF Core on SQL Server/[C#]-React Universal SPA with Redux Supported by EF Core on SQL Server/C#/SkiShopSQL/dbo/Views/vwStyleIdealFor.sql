CREATE VIEW [dbo].[vwStyleIdealFor]
	AS 
	SELECT st.StyleId, StyleName, CategoryId, CategoryName, BrandId, BrandName, GenderId, GenderName, 
		ImageSmall, ImageBig, PriceCurrent, PriceRegular, SoftDeleted, AverageRatings, ReviewCount, 
		SoldOut, si.IdealForId, IdealForSpec
	FROM dbo.vwStyle AS st
	JOIN dbo.StyleIdealFors AS si
		ON st.StyleId = si.StyleId
	JOIN dbo.IdealFors AS ideal
		ON si.IdealForId = ideal.IdealForId