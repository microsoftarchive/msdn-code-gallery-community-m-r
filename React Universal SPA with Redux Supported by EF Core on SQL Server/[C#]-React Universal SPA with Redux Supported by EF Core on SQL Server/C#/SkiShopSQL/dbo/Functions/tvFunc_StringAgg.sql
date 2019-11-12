CREATE FUNCTION [dbo].[tvFunc_StringAgg]
(
	@specMultiValues dbo.SpecMultiValueTableType READONLY
)
RETURNS @returntable TABLE
(
	DisplayIndex TINYINT,
	SpecKeyName CHAR(30),
	SpecText NVARCHAR(1000)
)
AS

BEGIN
	INSERT @returntable
	SELECT TOP(1) DisplayIndex, 
		SpecKeyName , 
		SUBSTRING((SELECT ',' + SpecText FROM @specMultiValues FOR XML PATH ('')), 2, 1000) AS SpecText 
		FROM @specMultiValues
	RETURN
END