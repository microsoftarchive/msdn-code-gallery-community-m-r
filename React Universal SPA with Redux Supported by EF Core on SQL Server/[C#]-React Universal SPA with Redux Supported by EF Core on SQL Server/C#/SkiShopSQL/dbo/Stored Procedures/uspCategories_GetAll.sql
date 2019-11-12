CREATE PROCEDURE [dbo].[uspCategories_GetAll]
AS
	SET NOCOUNT ON;
	
	SELECT CategoryId, CategoryName FROM dbo.Categories;