CREATE PROCEDURE [dbo].[uspProvinces_GetAll]
AS
	SET NOCOUNT ON;

	SELECT ProvinceId, ProvinceName FROM dbo.Provinces