CREATE VIEW [dbo].[vwStyle]
	WITH SCHEMABINDING
	AS SELECT s.StyleId, s.StyleName, c.CategoryId, c.CategoryName, b.BrandId, b.BrandName, g.GenderId, g.GenderName, 
		s.ImageSmall, s.ImageBig, s.PriceCurrent, s.PriceRegular, s.SoftDeleted, ss.AverageRatings, ss.ReviewCount, 
		ss.SoldOut
	FROM dbo.Styles s
	INNER JOIN dbo.Categories c ON s.CategoryId = c.CategoryId
	INNER JOIN dbo.Brands b ON s.BrandId = b.BrandId
	INNER JOIN dbo.Genders g ON s.GenderId = g.GenderId
	INNER JOIN dbo.StyleStates ss ON s.StyleId = ss.StyleId
	WHERE s.SoftDeleted = 0