USE OrderManagement
GO

-- 1) Stored procedure to Select OrderDetails

-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : OrderDetails                                            
-- Tables used :  OrderDetails                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                               
-- =============================================   
-- exec USP_OrderDetail_Select '1'
-- =============================================                                                           
Create PROCEDURE [dbo].[USP_OrderDetail_Select]                                              
   (                            
     @OrderNo           VARCHAR(100)     = ''  
      )                                                        
AS                                                                
BEGIN        
         Select Order_Detail_No,
				[Order_No],
                [Item_Name],
                [Notes],
                [QTY],
                [Price]
            FROM 
                OrderDetails 
            WHERE
                Order_No like  @OrderNo +'%'             
            ORDER BY
                Item_Name    
END

-- 2) Stored procedure to insert OrderDetail

-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderDetail                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                                  
-- =============================================    
-- exec USP_OrderDetail_Insert 4,'cadburys','cadburys Chocolate','50',50
-- =============================================                                                          
Create PROCEDURE [dbo].[USP_OrderDetail_Insert]                                              
   (  
     @Order_No			  VARCHAR(10),                     
     @Item_Name           VARCHAR(100)     = '',
     @Notes               VARCHAR(100)     = '',
     @QTY                 VARCHAR(20)     = '',
	 @Price               VARCHAR(20)     = ''
      )                                                        
AS                                                                
BEGIN        
        IF NOT EXISTS (SELECT * FROM OrderDetails WHERE Order_No=@Order_No AND Item_Name=@Item_Name)
            BEGIN

                  INSERT INTO [OrderDetails] 
          ( [Order_No],[Item_Name],[Notes],[QTY] ,[Price]) 
    VALUES 
          ( @Order_No,@Item_Name,@Notes,@QTY ,@Price ) 
                               
                    Select 'Inserted' as results
                        
            END
         ELSE
             BEGIN
                     Select 'Exists' as results
              END

END

-- 3) Stored procedure to Update OrderDetail
    
-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderDetail                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                                
-- =============================================      
-- exec USP_OrderDetail_Update 8,4,'Cadburys','cadburys Chocolate','50',50
-- =============================================                                                           
CREATE PROCEDURE [dbo].[USP_OrderDetail_Update]                                              
   (  @Order_Detail_No   Int=0,                           
      @Order_No			  VARCHAR(10),                     
      @Item_Name           VARCHAR(100)     = '',
      @Notes               VARCHAR(100)     = '',
      @QTY                 VARCHAR(20)     = '',
	  @Price               VARCHAR(20)     = ''
      )                                                        
AS                                                                
BEGIN        
        IF NOT EXISTS (SELECT * FROM OrderDetails WHERE Order_Detail_No!=@Order_Detail_No AND Order_No=@Order_No)
            BEGIN
                    UPDATE OrderDetails
                    SET   [Item_Name]=@Item_Name,
					[Notes]=@Notes,
					[QTY] =@QTY,
					[Price]=@Price
                    WHERE
                       Order_Detail_No=@Order_Detail_No
					   AND  Order_No=@Order_No
                               
                    Select 'updated' as results                        
            END
         ELSE
             BEGIN
                     Select 'Exists' as results
              END
END

-- 4) Stored procedure to Delete OrderDetail
    
-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderDetail                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                                 
-- =============================================  
-- exec USP_OrderDetail_Delete '8'
-- =============================================                                                           
CREATE PROCEDURE [dbo].[USP_OrderDetail_Delete]                                              
   (  @Order_Detail_No               Int=0 )                                                        
AS                                                                
BEGIN        
     
		DELETE from OrderDetails WHERE	 Order_Detail_No=@Order_Detail_No

		 Select 'Deleted' as results
            
END