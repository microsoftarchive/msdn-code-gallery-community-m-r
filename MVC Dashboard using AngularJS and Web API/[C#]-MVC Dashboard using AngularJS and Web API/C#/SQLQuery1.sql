---- =============================================                             
---- Author      : Shanu                           
---- Create date : 2016-04-12                             
---- Description : To Create Database,Table and Sample Insert Query                            
---- Latest                             
---- Modifier    : Shanu                              
---- Modify date : 2016-04-12                         
---- =============================================
----Script to create DB,Table and sample Insert data

USE MASTER
GO

 --1) Check for the Database Exists .If the database is exist then drop and create new DB

IF EXISTS (SELECT [name] FROM sys.databases WHERE [name] = 'DashboardDB' )
DROP DATABASE DashboardDB

GO

CREATE DATABASE DashboardDB
GO

USE DashboardDB
GO


-- 1) //////////// ItemDetails table

-- Create Table ItemDetails,This table will be used to store the details like Item Information
IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'ItemDetail' )
DROP TABLE ItemDetail
GO

CREATE TABLE [dbo].[ItemDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemNo] [varchar](100) NOT NULL ,
	[ItemName] [varchar](100) NOT NULL,
	[Comments] [varchar](100) NOT NULL,
	[Price] INT NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


Insert into ItemDetail(ItemNo,ItemName,Comments,Price) values
('101','NoteBook',	'HP Notebook 15 Inch',	24500)


Insert into ItemDetail(ItemNo,ItemName,Comments,Price) values
('102','MONITOR',	'SAMSNG',	'8500')

Insert into ItemDetail(ItemNo,ItemName,Comments,Price) values
('103','MOBILE',	'SAMSUNG NOTE 5',	42500)

Insert into ItemDetail(ItemNo,ItemName,Comments,Price) values
('104','MOBILE',	'SAMSUNG S7 Edge',	56000)

Insert into ItemDetail(ItemNo,ItemName,Comments,Price) values
('105','MOUSE',	'ABKO',	780)

Insert into ItemDetail(ItemNo,ItemName,Comments,Price) values
('106','HDD'	,'LG',	3780)

select * from ItemDetail


select ItemName,SUM(convert(int,Price)) as totalCost 
from ItemDetail
GROUP BY ItemName



-- 2) Student Master 

IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'UserDetails' )
DROP TABLE UserDetails
GO

CREATE TABLE [dbo].UserDetails(
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[UserType] [varchar](100) NOT NULL,
	[Phone] [varchar](20) NOT NULL,	
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

Insert into UserDetails(UserName,UserType,Phone) values
('SHANU','Admin','01039124503')

Insert into UserDetails(UserName,UserType,Phone) values
('Afraz','user','01039120984')

Insert into UserDetails(UserName,UserType,Phone) values
('Afreen','user','01039120005')

Insert into UserDetails(UserName,UserType,Phone) values
('Raj','Admin','01039120006')

Insert into UserDetails(UserName,UserType,Phone) values
('Mak','Manager','01039124567')

Insert into UserDetails(UserName,UserType,Phone) values
('Jack','Manager','01039120238')

Insert into UserDetails(UserName,UserType,Phone) values
('Pak','User','01039125409')

Insert into UserDetails(UserName,UserType,Phone) values
('Ninu','Accountant','01039126810')

Insert into UserDetails(UserName,UserType,Phone) values
('Nanu','Accountant','01039152011')

select * from UserDetails
