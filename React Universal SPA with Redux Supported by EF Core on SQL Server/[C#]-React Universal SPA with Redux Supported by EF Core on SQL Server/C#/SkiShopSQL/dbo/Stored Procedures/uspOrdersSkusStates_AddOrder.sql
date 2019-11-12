CREATE PROCEDURE [dbo].[uspOrdersSkusStates_AddOrder]
	@customerOrderId UNIQUEIDENTIFIER,
	@email NVARCHAR (100),
	@userId INT = NULL,
	@fullName NVARCHAR (100),
	@provinceId SMALLINT,
	@city NVARCHAR (50),
	@addressLine NVARCHAR (100),
	@postalCode NVARCHAR (7),
	@totalValue DECIMAL (10, 2),
	@createdDateTime DATETIME2 (7),
	@orderItems dbo.OrderItemTableType READONLY
AS
	SET NOCOUNT ON;

	DECLARE @skuIdsOverStock TABLE(SkuId INT);
	DECLARE @orderId TABLE(OrderId INT);
    
	INSERT INTO @skuIdsOverStock(SkuId)
	SELECT oi.SkuId FROM @orderItems AS oi
	INNER JOIN dbo.Skus as sk
		ON oi.SkuId = sk.SkuId
	WHERE sk.Quantity < oi.Quantity

	IF EXISTS (SELECT TOP(1) SkuId from @skuIdsOverStock)
		BEGIN
			INSERT INTO @orderId(OrderId) VALUES(-1);
		END
	ELSE
		BEGIN
			INSERT INTO dbo.Orders(CustomerOrderId, Email, UserId, FullName, ProvinceId, City, AddressLine, PostalCode, 
				TotalValue, CreatedDateTime)
			OUTPUT inserted.OrderId INTO @orderId
			VALUES (@customerOrderId, @email, @userId, @fullName, @provinceId, @city, @addressLine, @postalCode,
				@totalValue, @createdDateTime);

			INSERT INTO dbo.OrderItems(OrderId, SkuId, Price, Quantity) 
			SELECT (SELECT TOP(1) OrderId FROM @orderId), SkuId, Price, Quantity from @orderItems;

		END
	
	SELECT SkuId FROM @skuIdsOverStock;

	SELECT TOP(1) OrderId FROM @orderId;

	DECLARE @styleIds TABLE(StyleId INT);
	INSERT INTO @styleIds(StyleId) 
	SELECT DISTINCT StyleId 
	FROM @orderItems AS oi 
	INNER JOIN dbo.Skus AS sk 
	ON oi.SkuId = sk.SkuId;

	SELECT SkuId, sk.StyleId, Quantity, Size 
	FROM @styleIds AS st 
	INNER JOIN dbo.Skus AS sk 
	ON st.StyleId = sk.StyleId;

	SELECT ss.StyleId, AverageRatings, ReviewCount, SoldOut
	FROM @styleIds AS st 
	INNER JOIN dbo.StyleStates AS ss 
	ON st.StyleId = ss.StyleId;