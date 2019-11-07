-- 1) Stored procedure to Select OrderMaster

-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderMaster                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                               
-- =============================================   
-- exec USP_OrderMaster_Select '',''
-- =============================================                                                           
Create PROCEDURE [dbo].[USP_OrderMaster_Select]                                              
   (                            
     @OrderNo           VARCHAR(100)     = '',
     @Table_ID               VARCHAR(100)     = ''    
      )                                                        
AS                                                                
BEGIN        
         Select [Order_No],
                [Table_ID],
                [Description],
                [Order_DATE],
                [Waiter_Name]
            FROM 
                OrderMasters 
            WHERE
                Order_No like  @OrderNo +'%'
                AND Table_ID like @Table_ID +'%'
            ORDER BY
                Table_ID    
END

-- 2) Stored procedure to insert OrderMaster

-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderMaster                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                                  
-- =============================================    
-- exec USP_OrderMaster_Insert 'T4','Table 4','SHANU'
-- =============================================                                                          
Create PROCEDURE [dbo].[USP_OrderMaster_Insert]                                              
   (                       
     @Table_ID           VARCHAR(100)     = '',
     @Description               VARCHAR(100)     = '',
     @Waiter_Name               VARCHAR(20)     = ''
      )                                                        
AS                                                                
BEGIN        
        IF NOT EXISTS (SELECT * FROM OrderMasters WHERE Table_ID=@Table_ID)
            BEGIN

                  INSERT INTO [OrderMasters] 
          ([Table_ID] ,[Description],[Order_DATE],[Waiter_Name]) 
    VALUES 
          (@Table_ID,@Description,GETDATE(),@Waiter_Name ) 
                               
                    Select 'Inserted' as results
                        
            END
         ELSE
             BEGIN
                     Select 'Exists' as results
              END

END

-- 3) Stored procedure to Update OrderMaster
    
-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderMaster                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                                
-- =============================================      
-- exec USP_OrderMaster_Update 4,'T4','Table 4 wer','SHANU'
-- =============================================                                                           
CREATE PROCEDURE [dbo].[USP_OrderMaster_Update]                                              
   (  @OrderNo               Int=0,                           
      @Table_ID           VARCHAR(100)     = '',
      @Description               VARCHAR(100)     = '',
      @Waiter_Name               VARCHAR(20)     = ''
      )                                                        
AS                                                                
BEGIN        
        IF NOT EXISTS (SELECT * FROM OrderMasters WHERE Order_No!=@OrderNo AND Table_ID=@Table_ID)
            BEGIN
                    UPDATE OrderMasters
                    SET    [Table_ID]=@Table_ID ,
					[Description]=@Description,
					[Order_DATE]=GETDATE(),
					[Waiter_Name]=@Waiter_Name
                    WHERE
                        Order_No=@OrderNo
                               
                    Select 'updated' as results                        
            END
         ELSE
             BEGIN
                     Select 'Exists' as results
              END
END

-- 4) Stored procedure to Delete OrderMaster
    
-- Author      : Shanu                                                                
-- Create date : 2015-10-26                                                               
-- Description : Order Master                                              
-- Tables used :  OrderMaster                                                               
-- Modifier    : Shanu                                                                
-- Modify date : 2015-10-26                                                                 
-- =============================================  
-- exec USP_OrderMaster_Delete '3'
-- =============================================                                                           
CREATE PROCEDURE [dbo].[USP_OrderMaster_Delete]                                              
   (  @OrderNo               Int=0 )                                                        
AS                                                                
BEGIN        
        DELETE FROM OrderMasters WHERE	Order_No=@OrderNo             

		DELETE from OrderDetails WHERE	Order_No=@OrderNo    

		 Select 'Deleted' as results
            
END