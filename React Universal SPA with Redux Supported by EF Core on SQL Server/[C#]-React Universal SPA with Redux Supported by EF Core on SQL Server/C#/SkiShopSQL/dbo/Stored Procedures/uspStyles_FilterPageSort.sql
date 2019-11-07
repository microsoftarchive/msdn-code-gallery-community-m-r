CREATE PROCEDURE [dbo].[uspStyles_FilterPageSort]
	@categoryId TINYINT,
	@pageNumber INT = 1,
	@pageSize INT = 6,
	@sort TINYINT = 0,
	@brandIds dbo.IdSmallintTableType READONLY,
	@genderIds dbo.IdTinyIntTableType READONLY,
	@idealForIds dbo.IdTinyIntTableType READONLY
AS
	SET NOCOUNT ON;

	DECLARE @styles TABLE(
		StyleId INT,
		BrandId SMALLINT,
		GenderId TINYINT,
		IdealForId TINYINT
	);

	DECLARE @styleIds dbo.IdIntTableType;
	INSERT INTO @styleIds(Id)
	SELECT DISTINCT StyleId
	FROM dbo.vwStyleIdealFor
	WHERE CategoryId = @categoryId
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @brandIds) OR BrandId IN (SELECT Id FROM @brandIds))
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @genderIds) OR GenderId in (SELECT Id FROM @genderIds))
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @idealForIds) OR IdealForId in (SELECT Id FROM @idealForIds));

	IF EXISTS (SELECT TOP(1) Id FROM @styleIds)
		BEGIN
			SELECT COUNT(Id) AS TotalCount
			FROM @styleIds
		END
	ELSE
		SELECT 0 AS TotalCount

	DECLARE @brandIdsByCategory dbo.IdSmallintTableType
	INSERT INTO @brandIdsByCategory(Id)
	SELECT DISTINCT BrandId 
	FROM dbo.Styles
	WHERE CategoryId = @categoryId

	SELECT br.BrandId, MIN(br.BrandName) AS BrandName, COUNT(DISTINCT StyleId) AS BrandCount
	FROM dbo.Brands AS br
	JOIN @brandIdsByCategory AS bc
	ON br.BrandId = bc.Id
	LEFT JOIN dbo.vwStyleIdealFor si
	ON br.BrandId = si.BrandId
	AND CategoryId = @categoryId
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @genderIds) OR GenderId in (SELECT Id FROM @genderIds))
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @idealForIds) OR IdealForId in (SELECT Id FROM @idealForIds))
	GROUP BY br.BrandId;

	DECLARE @GenderIdsByCategory dbo.IdTinyIntTableType
	INSERT INTO @GenderIdsByCategory(Id)
	SELECT DISTINCT GenderId
	FROM dbo.Styles
	WHERE CategoryId = @categoryId

	SELECT gd.GenderId, MIN(gd.GenderName) AS GenderName, COUNT(DISTINCT StyleId) AS GenderCount
	FROM dbo.Genders as gd
	join @GenderIdsByCategory gc
	ON gd.GenderId = gc.Id
	left join dbo.vwStyleIdealFor si
	ON gd.GenderId = si.GenderId
	AND CategoryId = @categoryId
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @brandIds) OR BrandId IN (SELECT Id FROM @brandIds))
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @idealForIds) OR IdealForId in (SELECT Id FROM @idealForIds))
	GROUP BY gd.GenderId;

	DECLARE @IdealForIdsByCategory dbo.IdTinyIntTableType
	INSERT INTO @IdealForIdsByCategory(Id)
	SELECT DISTINCT IdealForId
	FROM dbo.vwStyleIdealFor
	WHERE CategoryId = @categoryId

	SELECT ideal.IdealForId, MIN(ideal.IdealForSpec) AS IdealForSpec, COUNT(DISTINCT StyleId) AS IdealForCount
	FROM dbo.IdealFors AS ideal
	JOIN @IdealForIdsByCategory AS ic
	on ideal.IdealForId = ic.Id
	left join dbo.vwStyleIdealFor as si
	on ideal.IdealForId = si.IdealForId
	WHERE CategoryId = @categoryId
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @brandIds) OR BrandId IN (SELECT Id FROM @brandIds))
	AND (NOT EXISTS (SELECT TOP(1) Id FROM @genderIds) OR GenderId in (SELECT Id FROM @genderIds))
	GROUP BY ideal.IdealForId;

	SELECT StyleId, StyleName, CategoryId, BrandName, GenderName, ImageSmall, PriceCurrent, PriceRegular,
		AverageRatings, ReviewCount, SoldOut
	FROM @styleIds AS si
	JOIN dbo.vwStyle AS st
	ON si.Id = st.StyleId
	ORDER BY 
		CASE WHEN @sort = 1 THEN PriceCurrent END,
		CASE WHEN @sort = 2 THEN PriceCurrent END DESC
	OFFSET (@pageNumber - 1) * @pageSize ROWS
	FETCH NEXT @pageSize ROWS ONLY;