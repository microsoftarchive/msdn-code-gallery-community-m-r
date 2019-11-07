CREATE PROCEDURE [dbo].[uspOrdersItems_GetByIdEmail]
	@customerOrderId UNIQUEIDENTIFIER,
	@email NVARCHAR (100)
AS
	SET NOCOUNT ON;
    
	SELECT OrderId, CustomerOrderId, FullName, ProvinceName, City, AddressLine, PostalCode, TotalValue, os.CreatedDateTime,
		JSON_QUERY ((SELECT BrandName + '-' + StyleName + '-' + GenderName +'-' + Size AS Skis, oi.SkuId, Price, oi.Quantity, SubTotal
			FROM dbo.OrderItems AS oi 
			JOIN dbo.Skus AS sk ON oi.SkuId = sk.SkuId 
			JOIN dbo.Styles AS st ON sk.StyleId = st.StyleId 
			JOIN dbo.Brands AS br ON st.BrandId = br.BrandId
			JOIN dbo.Genders AS gd ON st.GenderId = gd.GenderId
			WHERE oi.OrderId = os.OrderId
			FOR JSON PATH )) AS OrderItems
	FROM dbo.orders AS os
	JOIN dbo.Provinces AS pr
	ON os.ProvinceId = pr.ProvinceId
	WHERE CustomerOrderId = @customerOrderId and Email = @email
	FOR JSON PATH, without_array_wrapper