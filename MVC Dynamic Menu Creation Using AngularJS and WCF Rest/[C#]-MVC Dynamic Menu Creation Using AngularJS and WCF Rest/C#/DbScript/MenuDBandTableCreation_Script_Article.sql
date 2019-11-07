-- =============================================                                
-- Author      : Shanu                                
-- Create date : 2015-03-20                                
-- Description : To Create Database,Table and Sample Insert Query                             
-- Latest                                
-- Modifier    : Shanu                                
-- Modify date : 2015-03-20                            
-- =============================================
--Script to create DB,Table and sample Insert data
USE MASTER
GO
-- 1) Check for the Database Exists .If the database is exist then drop and create new DB
IF EXISTS (SELECT [name] FROM sys.databases WHERE [name] = 'MenuDB' )
DROP DATABASE MenuDB
GO

CREATE DATABASE MenuDB
GO

USE MenuDB
GO

-- 1) //////////// ToysDetails table
-- Create Table  ToysDetails ,This table will be used to store the details like Toys Information 

IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'MenuMaster' )
DROP TABLE MenuMaster
GO

CREATE TABLE MenuMaster
(
   Menu_ID int identity(1,1),
   Menu_RootID VARCHAR(30)  NOT NULL,
   Menu_ChildID VARCHAR(30)  NOT NULL,
   UserID varchar(50),
   CreatedDate datetime
CONSTRAINT [PK_MenuMaster] PRIMARY KEY CLUSTERED      
(     
  [Menu_ID] ASC   ,
  [Menu_RootID] ASC,
  [Menu_ChildID] ASC  
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]     
) ON [PRIMARY]   

GO
--delete from MenuMaster
-- Insert the sample records to the ToysDetails Table
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Root','Home','Shanu',getdate()-23)
--Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Home','Home','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Home','About','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Home','Contact','Shanu',getdate()-23)

Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Root','Masters','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Masters','ITM001','Shanu',getdate()-23)
--Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('ITM001','ITM001','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('ITM001','ITM002','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('ITM001','ITM003','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Masters','CAT001','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('CAT001','CAT001','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('CAT001','CAT002','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('CAT001','CAT003','Shanu',getdate()-23)


Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Root','Inventory','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Inventory','INV001','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('INV001','FG001','Shanu',getdate()-23)
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('INV001','FG002','Shanu',getdate()-23)



select * from MenuMaster
-- 1) END //

-- 2) Cart Details Table
IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'MenuDetails' )
DROP TABLE MenuDetails
GO

CREATE TABLE MenuDetails
(
   MDetail_ID int identity(1,1),
   Menu_ChildID VARCHAR(20) NOT NULL,
   MenuName VARCHAR(100) NOT NULL,
   MenuDisplayTxt VARCHAR(200) NOT NULL,
   MenuFileName VARCHAR(100) NOT NULL, 
   MenuURL VARCHAR(500) NOT NULL,
   USE_YN Char(1) DEFAULT 'Y',
   UserID varchar(50),
   CreatedDate datetime
CONSTRAINT [PK_MenuDetails] PRIMARY KEY CLUSTERED      
(     
  [MDetail_ID] ASC,
  [Menu_ChildID] ASC       
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]     
) ON [PRIMARY]   

GO
----delete from MenuDetails

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('Root','Home','Shanu Home','Index','Home','Shanu',getdate()-23)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('Home','Home','Shanu Home','Index','Home','Shanu',getdate()-23)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('About','About','About Shanu','About','Home','Shanu',getdate()-43)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('Contact','Contact','Contact Shanu','Contact','Home','Shanu',getdate()-13)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('Masters','Masters','Masters','ItemDetails','Masters','Shanu',getdate()-13)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('ITM001','ItemMaster','Item Master','ItemDetails','Masters','Shanu',getdate()-13)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('ITM002','ItemDetail','Item Details','ItemDetails','Masters','Shanu',getdate()-13)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('ITM003','ItemManage','Item Manage','ItemManage','Masters','Shanu',getdate()-13)



Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('CAT001','CatMaster','Category Masters','CATDetails','Masters','Shanu',getdate()-13)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('CAT002','CATDetail','Category Details','CATDetails','Masters','Shanu',getdate()-13)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('CAT003','CATManage','Category Manage','CATManage','Masters','Shanu',getdate()-13)



Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('Inventory','Inventory','Inventory','Index','Inventory','Shanu',getdate()-23)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('INV001','Inventory','Inventory Details','Index','Inventory','Shanu',getdate()-23)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('FG001','FGIN','FG Receipt','FGIN','Inventory','Shanu',getdate()-43)

Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,MenuURL,UserID,CreatedDate) 
values('FG002','FGOUT','FG Issue','FGOUT','Inventory','Shanu',getdate()-13)


select * from MenuMaster

select * from MenuDetails

		select   A.Menu_RootID,
                 B.MDetail_ID,
                 B.Menu_ChildID,
                 B.MenuName,
                 B.MenuDisplayTxt,
                 B.MenuFileName,           
                 B.MenuURL ,           
                 B.UserID                                                      
                 FROM 
                 MenuMaster A
                 INNER JOIN MenuDetails B
                 ON A.Menu_ChildID=B.Menu_ChildID
                 
                 
                            
                            			