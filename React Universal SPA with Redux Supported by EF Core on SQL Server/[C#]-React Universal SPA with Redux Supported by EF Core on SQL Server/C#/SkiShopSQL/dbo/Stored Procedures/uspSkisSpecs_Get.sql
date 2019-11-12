CREATE PROCEDURE [dbo].[uspSkisSpecs_Get]
	@styleId int
AS
	SET NOCOUNT ON;

	DECLARE @a dbo.SpecMultiValueTableType, @b dbo.SpecMultiValueTableType, @c dbo.SpecMultiValueTableType,
@d dbo.SpecMultiValueTableType, @e dbo.SpecMultiValueTableType

	INSERT INTO @a 
	SELECT DisplayIndex_IdealFor AS DisplayIndex, 'Ideal for' AS SpecKeyName, IdealForSpec AS SpecText
	FROM SpecMultiValues AS sm
	JOIN StyleIdealFors AS si
	ON sm.StyleId = si.StyleId
	JOIN IdealFors AS i
	ON si.IdealForId = i.IdealForId
	WHERE sm.StyleId = @styleId

	INSERT INTO @b
	SELECT DisplayIndex_Ability AS DisplayIndex, 'Abilities' AS SpecKeyName, AbilitySpec AS SpecText
	FROM SpecMultiValues AS sm
	JOIN StyleAbilities AS sa
	ON sm.StyleId = sa.StyleId
	JOIN SpecAbilities spec
	ON sa.AbilityId = spec.AbilityId
	WHERE sm.StyleId = @styleId

	INSERT INTO @c
	SELECT DisplayIndex_SnowCondition AS DisplayIndex, 'Snow conditions' AS SpecKeyName, SnowConditionSpec AS SpecText
	FROM SpecMultiValues AS sm
	JOIN StyleSnowConditions ss
	ON sm.StyleId = ss.StyleId
	JOIN SpecSnowConditions AS spec
	ON ss.SnowConditionId = spec.SnowConditionId
	WHERE sm.StyleId = @styleId

	INSERT INTO @d
	SELECT DisplayIndex_Terrain AS DisplayIndex, 'Terrains' AS SpecKeyName, TerrainSpec AS SpecText
	FROM SpecMultiValues AS sm
	JOIN StyleTerrains AS st
	ON sm.StyleId = st.StyleId
	JOIN SpecTerrains AS spec
	ON st.TerrainId = spec.TerrainId
	WHERE sm.StyleId = @styleId

	INSERT INTO @e
	SELECT DisplayIndex_RockerCamberProfile AS DisplayIndex, 'Rocker/camber Profile' AS SpecKeyName, RockerCamberProfileSpec AS SpecText
	FROM SpecMultiValues AS sm
	JOIN StyleRockerCamberProfiles AS sr
	ON sm.StyleId = sr.StyleId
	JOIN SpecRockerCamberProfiles AS spec
	ON sr.RockerCamberProfileId = spec.RockerCamberProfileId
	WHERE sm.StyleId = @styleId

	SELECT * FROM dbo.tvFunc_StringAgg(@a)
	UNION ALL 
	SELECT * FROM dbo.tvFunc_StringAgg(@b)
	UNION ALL 
	SELECT * FROM dbo.tvFunc_StringAgg(@c)
	UNION ALL
	SELECT * FROM dbo.tvFunc_StringAgg(@d)
	UNION ALL
	SELECT * FROM dbo.tvFunc_StringAgg(@e)

	UNION ALL
	(SELECT DisplayIndex, SpecKeyName, IIF(SpecValue=1, 'Yes', 'No') AS SpecText
	FROM SpecBitValues AS sb
	JOIN SpecKeys AS sk
	ON sb.SpecKeyId = sk.SpecKeyId
	WHERE StyleId = @styleId)
	UNION ALL
	(SELECT DisplayIndex, SpecKeyName, TextValue AS SpecText
	FROM SpecTextValues AS st
	JOIN SpecKeys AS sk
	ON st.SpecKeyId = sk.SpecKeyId
	WHERE StyleId = @styleId)
	UNION all
	(SELECT DisplayIndex_Core AS DisplayIndex, 'Core' AS SpecKeyName, CoreSpec AS SpecText
	FROM SpecSingleValues AS ss
	JOIN SpecCores AS sc
	ON ss.CoreId = sc.CoreId
	where StyleId = @styleId)
	UNION ALL
	(SELECT DisplayIndex_Construction AS DisplayIndex, 'Construction' AS SpecKeyName, ConstructionSpec AS SpecText
	FROM SpecSingleValues AS ss
	JOIN SpecConstructions AS st
	ON ss.ConstructionId = st.ConstructionId
	WHERE StyleId = @styleId)
	UNION all
	(SELECT DisplayIndex_MadeIn AS DisplayIndex, 'Made in' AS SpecKeyName, CountryName AS SpecText
	FROM SpecSingleValues AS ss
	JOIN Countries AS c
	ON ss.MadeInId = c.CountryId
	WHERE StyleId = @styleId)