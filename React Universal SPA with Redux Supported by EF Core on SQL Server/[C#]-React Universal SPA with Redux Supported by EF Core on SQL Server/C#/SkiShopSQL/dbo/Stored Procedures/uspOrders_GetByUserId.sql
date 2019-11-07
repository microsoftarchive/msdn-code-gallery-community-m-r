CREATE PROCEDURE [dbo].[uspOrders_GetByUserId]
	@userId int
AS
	SET NOCOUNT ON;

	SELECT OrderId, CustomerOrderId, City, TotalValue, CONVERT(VARCHAR, CreatedDateTime, 100) AS CreatedDateTime 
	FROM dbo.Orders
	WHERE UserId = @userId
	ORDER BY CreatedDateTime DESC;