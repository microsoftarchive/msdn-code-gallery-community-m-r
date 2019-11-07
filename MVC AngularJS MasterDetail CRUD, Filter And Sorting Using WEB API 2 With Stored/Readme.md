# MVC AngularJS Master/Detail CRUD, Filter And Sorting Using WEB API 2 With Stored
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- ASP.NET MVC
- ASP.NET Web API
- AngularJS
## Topics
- ASP.NET MVC
- ASP.NET Web API
- AngularJS
## Updated
- 08/31/2016
## Description

<h1>Introduction</h1>
<p><img id="144275" src="144275-o1.png" alt="" width="580" height="331"></p>
<p>In one of my article I explained about&nbsp;<a href="http://www.codeproject.com/Articles/995737/MVC-Angular-JS-and-WCF-Rest-Service-for-Master-Det"><span>how to create a Master/Detail HTML GRID</span></a>&nbsp;using MVC and AngularJS. Few members requested
 me to write an article for Master/Detail html grid with CRUD (Insert, Update, Select and Delete) for both Master and Detail grid. As a result here I have created a simple demo program with the following features.</p>
<p><span>This article will explain:</span></p>
<ul>
<li><span>How to Create Order Master and Order Detail table with sample records inserted.</span>
</li><li><span>Create Stored Procedure to perform Insert/Update/Select and Delete both Order Master and Order Detail table.</span>
</li><li><span>Create Entity Framework and add all the Stored Procedures.</span> </li><li><span>Create a separate WEB API for both Order Master and Order Detail to execute all our Stored Procedures from AngularJS Controller.</span>
</li><li><span>Create AngularJS Controller to perform all business logic part to display our Master/Detail HTML grid.</span>
</li><li><span>Add Sorting /Filtering features for both Master and Detail HTML grid.</span>
</li><li><span>Display Total Row for each Child Detail Grid.</span> </li><li><span>Add/Edit/ and Delete each Order Master and Order Detail from grid.</span>
</li><li><span>Search Order Master Details.</span>&nbsp; </li></ul>
<h1><span>Building the Sample</span></h1>
<p><strong><span><span>Prerequisites</span></span></strong></p>
<p><strong>Visual Studio 2015</strong><span>&nbsp;- You can download it from&nbsp;</span><span><a href="https://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs.aspx"><span><span>here</span></span></a></span><span>.</span></p>
<p>You can also view my previous articles related to AngularJs using MVC and the WCF Rest Serice.</p>
<ul>
<li><em><a href="https://code.msdn.microsoft.com/Dynamic-scheduling-using-35328360" target="_blank">https://code.msdn.microsoft.com/Dynamic-scheduling-using-35328360</a></em>
</li><li><a href="https://code.msdn.microsoft.com/MVC-Angular-JS-CRUD-using-b4845edc" target="_blank">https://code.msdn.microsoft.com/MVC-Angular-JS-CRUD-using-b4845edc</a>
</li><li><a href="https://code.msdn.microsoft.com/AngularJS-Shopping-Cart-8d4dde90" target="_blank">https://code.msdn.microsoft.com/AngularJS-Shopping-Cart-8d4dde90</a>
</li><li><a href="https://code.msdn.microsoft.com/MVC-AngularJS-and-WCF-Rest-27d239b4" target="_blank">https://code.msdn.microsoft.com/MVC-AngularJS-and-WCF-Rest-27d239b4</a>
</li><li><a href="https://code.msdn.microsoft.com/MVC-Web-API-and-Angular-JS-36302919" target="_blank">https://code.msdn.microsoft.com/MVC-Web-API-and-Angular-JS-36302919</a>
</li><li><a href="https://code.msdn.microsoft.com/AngularJS-Filter-Sorting-1fe023c3" target="_blank">https://code.msdn.microsoft.com/MVC-Web-API-and-Angular-JS-36302919</a>
</li><li><a href="https://code.msdn.microsoft.com/Image-Preview-using-MVC-792d881c" target="_blank">https://code.msdn.microsoft.com/Image-Preview-using-MVC-792d881c</a>
</li></ul>
<p><span><br>
</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><strong>1. Create Database and Table</strong></p>
<p><span>We will create an Order Master and Order Detail table to be used for the Master and Detail Grid data binding.&nbsp;</span><span>The following is the script to create a database, table and sample insert query.&nbsp;</span><span>Run this script in your
 SQL Server. I have used SQL Server 2014.</span>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">use master
--create DataBase
-- 1) Check for the Database Exists .If the database is exist then drop and create new DB
IF EXISTS (SELECT [name] FROM sys.databases WHERE [name] = 'OrderManagement' )
DROP DATABASE OrderManagement
GO

CREATE DATABASE OrderManagement
GO

USE OrderManagement
GO

-- Create OrderMasters Table

CREATE TABLE [dbo].[OrderMasters](
[Order_No] INT IDENTITY PRIMARY KEY,
[Table_ID] [varchar](20) NOT NULL,
[Description] [varchar](200) NOT NULL,
[Order_DATE] [datetime] NOT NULL,
[Waiter_Name] [varchar](20) NOT NULL
)

-- Insert OrderMasters sample data

INSERT INTO [OrderMasters]
          ([Table_ID] ,[Description],[Order_DATE],[Waiter_Name])
    VALUES
          ('T1','Order for Table T1',GETDATE(),'SHANU' )   

INSERT INTO [OrderMasters]
          ([Table_ID] ,[Description],[Order_DATE],[Waiter_Name])
    VALUES
           ('T2','Order for Table T2',GETDATE(),'Afraz' )        

INSERT INTO [OrderMasters]
          ([Table_ID] ,[Description],[Order_DATE],[Waiter_Name])
     VALUES
             ('T3','Order for Table T3',GETDATE(),'Afreen')       
         

CREATE TABLE [dbo].[OrderDetails](
  [Order_Detail_No] INT IDENTITY PRIMARY KEY,
 [Order_No] INT,
 [Item_Name] [varchar](20) NOT NULL, 
 [Notes] [varchar](200) NOT NULL,
[QTY]  INT NOT NULL,
 [Price] INT NOT NULL
 )

--Now let&amp;rsquo;s insert the 3 items for the above Order No 'Ord_001'.

INSERT INTO [OrderDetails]
          ( [Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (1,'Ice Cream','Need very Cold',2 ,160)

INSERT INTO [OrderDetails]
          ([Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (1,'Coffee','Hot and more Suger',1 ,80)
        
          INSERT INTO [OrderDetails]
          ([Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (1,'Burger','Spicy',3 ,140)
        
          INSERT INTO [OrderDetails]
          ([Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (2,'Pizza','More Chees and Large',1 ,350)        

          INSERT INTO [OrderDetails]
          ([Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (2,'Cola','Need very Cold',3 ,50)
        
          INSERT INTO [OrderDetails]
          ([Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (3,'IDLY','Hot',3 ,40)

          INSERT INTO [OrderDetails]
          ([Order_No],[Item_Name],[Notes],[QTY] ,[Price])
   VALUES
          (3,'Thosa','Hot',3 ,50)

-- To Select and test Order Master and Details

Select * FROM OrderMasters
Select * From OrderDetails</pre>
<div class="preview">
<pre class="mysql"><span class="sql__keyword">use</span>&nbsp;<span class="sql__keyword">master</span>&nbsp;
--<span class="sql__keyword">create</span>&nbsp;<span class="sql__keyword">DataBase</span>&nbsp;
<span class="sql__com">--&nbsp;1)&nbsp;Check&nbsp;for&nbsp;the&nbsp;Database&nbsp;Exists&nbsp;.If&nbsp;the&nbsp;database&nbsp;is&nbsp;exist&nbsp;then&nbsp;drop&nbsp;and&nbsp;create&nbsp;new&nbsp;DB</span>&nbsp;
<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(<span class="sql__keyword">SELECT</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">sys</span>.<span class="sql__keyword">databases</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;=&nbsp;<span class="sql__string">'OrderManagement'</span>&nbsp;)&nbsp;
<span class="sql__keyword">DROP</span>&nbsp;<span class="sql__keyword">DATABASE</span>&nbsp;<span class="sql__id">OrderManagement</span>&nbsp;
<span class="sql__id">GO</span>&nbsp;
&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">DATABASE</span>&nbsp;<span class="sql__id">OrderManagement</span>&nbsp;
<span class="sql__id">GO</span>&nbsp;
&nbsp;
<span class="sql__keyword">USE</span>&nbsp;<span class="sql__id">OrderManagement</span>&nbsp;
<span class="sql__id">GO</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;OrderMasters&nbsp;Table</span>&nbsp;
&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">OrderMasters</span>](&nbsp;
[<span class="sql__id">Order_No</span>]&nbsp;<span class="sql__keyword">INT</span>&nbsp;<span class="sql__id">IDENTITY</span>&nbsp;<span class="sql__keyword">PRIMARY</span>&nbsp;<span class="sql__keyword">KEY</span>,&nbsp;
[<span class="sql__id">Table_ID</span>]&nbsp;[<span class="sql__keyword">varchar</span>](<span class="sql__number">20</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
[<span class="sql__id">Description</span>]&nbsp;[<span class="sql__keyword">varchar</span>](<span class="sql__number">200</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
[<span class="sql__id">Order_DATE</span>]&nbsp;[<span class="sql__keyword">datetime</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
[<span class="sql__id">Waiter_Name</span>]&nbsp;[<span class="sql__keyword">varchar</span>](<span class="sql__number">20</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>&nbsp;
)&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;Insert&nbsp;OrderMasters&nbsp;sample&nbsp;data</span>&nbsp;
&nbsp;
<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderMasters</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Table_ID</span>]&nbsp;,[<span class="sql__id">Description</span>],[<span class="sql__id">Order_DATE</span>],[<span class="sql__id">Waiter_Name</span>])&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__string">'T1'</span>,<span class="sql__string">'Order&nbsp;for&nbsp;Table&nbsp;T1'</span>,<span class="sql__id">GETDATE</span>(),<span class="sql__string">'SHANU'</span>&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderMasters</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Table_ID</span>]&nbsp;,[<span class="sql__id">Description</span>],[<span class="sql__id">Order_DATE</span>],[<span class="sql__id">Waiter_Name</span>])&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__string">'T2'</span>,<span class="sql__string">'Order&nbsp;for&nbsp;Table&nbsp;T2'</span>,<span class="sql__id">GETDATE</span>(),<span class="sql__string">'Afraz'</span>&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderMasters</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Table_ID</span>]&nbsp;,[<span class="sql__id">Description</span>],[<span class="sql__id">Order_DATE</span>],[<span class="sql__id">Waiter_Name</span>])&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__string">'T3'</span>,<span class="sql__string">'Order&nbsp;for&nbsp;Table&nbsp;T3'</span>,<span class="sql__id">GETDATE</span>(),<span class="sql__string">'Afreen'</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">OrderDetails</span>](&nbsp;
&nbsp;&nbsp;[<span class="sql__id">Order_Detail_No</span>]&nbsp;<span class="sql__keyword">INT</span>&nbsp;<span class="sql__id">IDENTITY</span>&nbsp;<span class="sql__keyword">PRIMARY</span>&nbsp;<span class="sql__keyword">KEY</span>,&nbsp;
&nbsp;[<span class="sql__id">Order_No</span>]&nbsp;<span class="sql__keyword">INT</span>,&nbsp;
&nbsp;[<span class="sql__id">Item_Name</span>]&nbsp;[<span class="sql__keyword">varchar</span>](<span class="sql__number">20</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;
&nbsp;[<span class="sql__id">Notes</span>]&nbsp;[<span class="sql__keyword">varchar</span>](<span class="sql__number">200</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
[<span class="sql__id">QTY</span>]&nbsp;&nbsp;<span class="sql__keyword">INT</span>&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;[<span class="sql__id">Price</span>]&nbsp;<span class="sql__keyword">INT</span>&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>&nbsp;
&nbsp;)&nbsp;
&nbsp;
--<span class="sql__id">Now</span>&nbsp;<span class="sql__id">let</span>&amp;<span class="sql__id">rsquo</span>;<span class="sql__id">s</span>&nbsp;<span class="sql__keyword">insert</span>&nbsp;<span class="sql__id">the</span>&nbsp;<span class="sql__number">3</span>&nbsp;<span class="sql__id">items</span>&nbsp;<span class="sql__keyword">for</span>&nbsp;<span class="sql__id">the</span>&nbsp;<span class="sql__id">above</span>&nbsp;<span class="sql__keyword">Order</span>&nbsp;<span class="sql__keyword">No</span>&nbsp;<span class="sql__string">'Ord_001'</span>.&nbsp;
&nbsp;
<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;[<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">1</span>,<span class="sql__string">'Ice&nbsp;Cream'</span>,<span class="sql__string">'Need&nbsp;very&nbsp;Cold'</span>,<span class="sql__number">2</span>&nbsp;,<span class="sql__number">160</span>)&nbsp;
&nbsp;
<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">1</span>,<span class="sql__string">'Coffee'</span>,<span class="sql__string">'Hot&nbsp;and&nbsp;more&nbsp;Suger'</span>,<span class="sql__number">1</span>&nbsp;,<span class="sql__number">80</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">1</span>,<span class="sql__string">'Burger'</span>,<span class="sql__string">'Spicy'</span>,<span class="sql__number">3</span>&nbsp;,<span class="sql__number">140</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">2</span>,<span class="sql__string">'Pizza'</span>,<span class="sql__string">'More&nbsp;Chees&nbsp;and&nbsp;Large'</span>,<span class="sql__number">1</span>&nbsp;,<span class="sql__number">350</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">2</span>,<span class="sql__string">'Cola'</span>,<span class="sql__string">'Need&nbsp;very&nbsp;Cold'</span>,<span class="sql__number">3</span>&nbsp;,<span class="sql__number">50</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">3</span>,<span class="sql__string">'IDLY'</span>,<span class="sql__string">'Hot'</span>,<span class="sql__number">3</span>&nbsp;,<span class="sql__number">40</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__number">3</span>,<span class="sql__string">'Thosa'</span>,<span class="sql__string">'Hot'</span>,<span class="sql__number">3</span>&nbsp;,<span class="sql__number">50</span>)&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;To&nbsp;Select&nbsp;and&nbsp;test&nbsp;Order&nbsp;Master&nbsp;and&nbsp;Details</span>&nbsp;
&nbsp;
<span class="sql__keyword">Select</span>&nbsp;*&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">OrderMasters</span>&nbsp;
<span class="sql__keyword">Select</span>&nbsp;*&nbsp;<span class="sql__keyword">From</span>&nbsp;<span class="sql__id">OrderDetails</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;After creating our Table we will create a Stored Procedure to do our CRUD Operations.
<p>First we create stored procedure for Order Master Table to perform CRUD.</p>
</div>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">-- 1) Stored procedure to Select OrderMaster
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
                Order_No like  @OrderNo &#43;'%'
                AND Table_ID like @Table_ID &#43;'%'
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
        IF NOT EXISTS (SELECT Table_ID FROM OrderMasters WHERE Table_ID=@Table_ID)
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
        IF NOT EXISTS (SELECT Table_ID FROM OrderMasters WHERE Order_No!=@OrderNo AND Table_ID=@Table_ID)
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
        DELETE FROM OrderMasters WHERE           Order_No=@OrderNo            
                           DELETE from OrderDetails WHERE  Order_No=@OrderNo    

                            Select 'Deleted' as results

END</pre>
<div class="preview">
<pre class="mysql"><span class="sql__com">--&nbsp;1)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;Select&nbsp;OrderMaster</span>&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderMaster_Select&nbsp;'',''</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">Create</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderMaster_Select</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;[<span class="sql__id">Order_No</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Table_ID</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Description</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Order_DATE</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Waiter_Name</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">FROM</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">OrderMasters</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Order_No</span>&nbsp;<span class="sql__keyword">like</span>&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&#43;<span class="sql__string">'%'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">AND</span>&nbsp;<span class="sql__id">Table_ID</span>&nbsp;<span class="sql__keyword">like</span>&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>&nbsp;&#43;<span class="sql__string">'%'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ORDER</span>&nbsp;<span class="sql__keyword">BY</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Table_ID</span>&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">END</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;2)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;insert&nbsp;OrderMaster</span>&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderMaster_Insert&nbsp;'T4','Table&nbsp;4','SHANU'</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">Create</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderMaster_Insert</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Description</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Waiter_Name</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(<span class="sql__keyword">SELECT</span>&nbsp;<span class="sql__id">Table_ID</span>&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">OrderMasters</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;<span class="sql__id">Table_ID</span>=<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderMasters</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;([<span class="sql__id">Table_ID</span>]&nbsp;,[<span class="sql__id">Description</span>],[<span class="sql__id">Order_DATE</span>],[<span class="sql__id">Waiter_Name</span>])&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>,<span class="sql__keyword">@</span><span class="sql__variable">Description</span>,<span class="sql__id">GETDATE</span>(),<span class="sql__keyword">@</span><span class="sql__variable">Waiter_Name</span>&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Inserted'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ELSE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Exists'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
<span class="sql__keyword">END</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;3)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;Update&nbsp;OrderMaster</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderMaster_Update&nbsp;4,'T4','Table&nbsp;4&nbsp;wer','SHANU'</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderMaster_Update</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Int</span>=<span class="sql__number">0</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Description</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Waiter_Name</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(<span class="sql__keyword">SELECT</span>&nbsp;<span class="sql__id">Table_ID</span>&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">OrderMasters</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;<span class="sql__id">Order_No</span>!=<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;<span class="sql__keyword">AND</span>&nbsp;<span class="sql__id">Table_ID</span>=<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">UPDATE</span>&nbsp;<span class="sql__id">OrderMasters</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">SET</span>&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Table_ID</span>]=<span class="sql__keyword">@</span><span class="sql__variable">Table_ID</span>&nbsp;,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Description</span>]=<span class="sql__keyword">@</span><span class="sql__variable">Description</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Order_DATE</span>]=<span class="sql__id">GETDATE</span>(),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Waiter_Name</span>]=<span class="sql__keyword">@</span><span class="sql__variable">Waiter_Name</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Order_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'updated'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ELSE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Exists'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
<span class="sql__keyword">END</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;4)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;Delete&nbsp;OrderMaster&nbsp;</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderMaster_Delete&nbsp;'3'</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderMaster_Delete</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Int</span>=<span class="sql__number">0</span>&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">DELETE</span>&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">OrderMasters</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Order_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">DELETE</span>&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">OrderDetails</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;&nbsp;<span class="sql__id">Order_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Deleted'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;
&nbsp;
<span class="sql__keyword">END</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><span>Next we create stored procedure for Order Detail Table to perform CRUD.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">USE OrderManagement
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
                Order_No like  @OrderNo &#43;'%'            
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
     @Order_No                                          VARCHAR(10),                    
     @Item_Name           VARCHAR(100)     = '',
     @Notes               VARCHAR(100)     = '',
     @QTY                 VARCHAR(20)     = '',
     @Price               VARCHAR(20)     = ''
      )                                                       
AS                                                              
BEGIN       
        IF NOT EXISTS (SELECT Item_Name FROM OrderDetails WHERE Order_No=@Order_No AND Item_Name=@Item_Name)
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
ALTER PROCEDURE [dbo].[USP_OrderDetail_Update]                                             
   (  @Order_Detail_No   Int=0,                          
      @Order_No                                        VARCHAR(10),                    
      @Item_Name           VARCHAR(100)     = '',
      @Notes               VARCHAR(100)     = '',
      @QTY                 VARCHAR(20)     = '',
      @Price               VARCHAR(20)     = ''
      )                                                      
AS                                                                
BEGIN       
        IF NOT EXISTS (SELECT Item_Name FROM OrderDetails WHERE Order_Detail_No!=@Order_Detail_No AND Item_Name=@Item_Name)
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
-- ============================================                                                          
CREATE PROCEDURE [dbo].[USP_OrderDetail_Delete]                                             
   (  @Order_Detail_No               Int=0 )                                                       
AS                                                               
BEGIN       
                             DELETE from OrderDetails WHERE  Order_Detail_No=@Order_Detail_No
                            Select 'Deleted' as results          

END</pre>
<div class="preview">
<pre class="mysql"><span class="sql__keyword">USE</span>&nbsp;<span class="sql__id">OrderManagement</span>&nbsp;
<span class="sql__id">GO</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;1)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;Select&nbsp;OrderDetails</span>&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;OrderDetails&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderDetails&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderDetail_Select&nbsp;'1'</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">Create</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderDetail_Select</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__id">Order_Detail_No</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Order_No</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Item_Name</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Notes</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">QTY</span>],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Price</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">FROM</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">OrderDetails</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Order_No</span>&nbsp;<span class="sql__keyword">like</span>&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">OrderNo</span>&nbsp;&#43;<span class="sql__string">'%'</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ORDER</span>&nbsp;<span class="sql__keyword">BY</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Item_Name</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">END</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;2)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;insert&nbsp;OrderDetail</span>&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderDetail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderDetail_Insert&nbsp;4,'cadburys','cadburys&nbsp;Chocolate','50',50</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">Create</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderDetail_Insert</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Order_No</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">10</span>),&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Item_Name</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Notes</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">QTY</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Price</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(<span class="sql__keyword">SELECT</span>&nbsp;<span class="sql__id">Item_Name</span>&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">OrderDetails</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;<span class="sql__id">Order_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">Order_No</span>&nbsp;<span class="sql__keyword">AND</span>&nbsp;<span class="sql__id">Item_Name</span>=<span class="sql__keyword">@</span><span class="sql__variable">Item_Name</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;[<span class="sql__id">OrderDetails</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;[<span class="sql__id">Order_No</span>],[<span class="sql__id">Item_Name</span>],[<span class="sql__id">Notes</span>],[<span class="sql__id">QTY</span>]&nbsp;,[<span class="sql__id">Price</span>])&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Order_No</span>,<span class="sql__keyword">@</span><span class="sql__variable">Item_Name</span>,<span class="sql__keyword">@</span><span class="sql__variable">Notes</span>,<span class="sql__keyword">@</span><span class="sql__variable">QTY</span>&nbsp;,<span class="sql__keyword">@</span><span class="sql__variable">Price</span>&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Inserted'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ELSE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Exists'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
<span class="sql__keyword">END</span>&nbsp;
&nbsp;
<span class="sql__com">--&nbsp;3)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;Update&nbsp;OrderDetail</span>&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderDetail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderDetail_Update&nbsp;8,4,'Cadburys','cadburys&nbsp;Chocolate','50',50</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">ALTER</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderDetail_Update</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Order_Detail_No</span>&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Int</span>=<span class="sql__number">0</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Order_No</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">10</span>),&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Item_Name</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Notes</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">QTY</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Price</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="sql__string">''</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(<span class="sql__keyword">SELECT</span>&nbsp;<span class="sql__id">Item_Name</span>&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">OrderDetails</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;<span class="sql__id">Order_Detail_No</span>!=<span class="sql__keyword">@</span><span class="sql__variable">Order_Detail_No</span>&nbsp;<span class="sql__keyword">AND</span>&nbsp;<span class="sql__id">Item_Name</span>=<span class="sql__keyword">@</span><span class="sql__variable">Item_Name</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">UPDATE</span>&nbsp;<span class="sql__id">OrderDetails</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">SET</span>&nbsp;&nbsp;&nbsp;[<span class="sql__id">Item_Name</span>]=<span class="sql__keyword">@</span><span class="sql__variable">Item_Name</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Notes</span>]=<span class="sql__keyword">@</span><span class="sql__variable">Notes</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">QTY</span>]&nbsp;=<span class="sql__keyword">@</span><span class="sql__variable">QTY</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">Price</span>]=<span class="sql__keyword">@</span><span class="sql__variable">Price</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">Order_Detail_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">Order_Detail_No</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">AND</span>&nbsp;&nbsp;<span class="sql__id">Order_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">Order_No</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'updated'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ELSE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">BEGIN</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Exists'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">END</span>&nbsp;
<span class="sql__keyword">END</span>&nbsp;
<span class="sql__com">--&nbsp;4)&nbsp;Stored&nbsp;procedure&nbsp;to&nbsp;Delete&nbsp;OrderDetail</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;Order&nbsp;Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Tables&nbsp;used&nbsp;:&nbsp;&nbsp;OrderDetail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-10-26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;exec&nbsp;USP_OrderDetail_Delete&nbsp;'8'</span>&nbsp;
<span class="sql__com">--&nbsp;============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">PROCEDURE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">USP_OrderDetail_Delete</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;(&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Order_Detail_No</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Int</span>=<span class="sql__number">0</span>&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">AS</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">BEGIN</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">DELETE</span>&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">OrderDetails</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;&nbsp;<span class="sql__id">Order_Detail_No</span>=<span class="sql__keyword">@</span><span class="sql__variable">Order_Detail_No</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">Select</span>&nbsp;<span class="sql__string">'Deleted'</span>&nbsp;<span class="sql__keyword">as</span>&nbsp;<span class="sql__id">results</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
<span class="sql__keyword">END</span></pre>
</div>
</div>
</div>
<p><strong>2.&nbsp;Create your MVC Web Application in Visual Studio 2015</strong></p>
<p><span>After installing our Visual Studio 2015 click Start, then Programs and select Visual Studio 2015 - Click Visual Studio 2015.</span><br>
<span>Click New, then Project, select Web and click ASP.NET Web Application. Select your project location and enter your web application name.</span></p>
<p>Click New, then Project, select Web and click ASP.NET Web Application. Select your project location and enter your web application name.</p>
<p><img id="144276" src="144276-1.png" alt="" width="593" height="354"></p>
<p><span>Select&nbsp;</span><em>MVC&nbsp;</em><span>and in&nbsp;</span><em>Add Folders and Core reference for s</em><span>elect the&nbsp;</span><em>Web API</em><span>&nbsp;and click&nbsp;</span><em>OK</em><span>.</span></p>
<p><img id="144277" src="144277-2.png" alt="" width="589" height="367"></p>
<p><strong>Add Database using ADO.NET Entity Data Model</strong></p>
<p>Right click our project and click&nbsp;<em>Add,&nbsp;</em>then&nbsp;<em>New Item</em>.</p>
<p><img id="144278" src="144278-3.png" alt="" width="338" height="312"></p>
<p><span>Select&nbsp;</span><em>Data</em><span>, then&nbsp;</span><em>ADO.NET Entity Data Model&nbsp;</em><span>and give the name for our EF and click&nbsp;</span><em>Add</em><span>.</span></p>
<p><img id="144279" src="144279-4.png" alt="" width="495" height="354"></p>
<p><span>Select EF Designer from the database and click&nbsp;</span><em>Next&nbsp;</em><span>&gt;</span></p>
<p><img id="144280" src="144280-5.png" alt="" width="587" height="513"></p>
<p><span>Here click New Connection and provide your SQL Server - Server Name and connect to your database.</span></p>
<p><img id="144282" src="144282-6.png" alt="" width="528" height="373"></p>
<p><span>Here we can see I have given my SQL server name, Id and PWD and after it connected I selected the database as&nbsp;OrderManagement as we have created the Database using my SQL Script.</span></p>
<p><img id="144283" src="144283-7.png" alt="" width="590" height="324"></p>
<p><span>Click next and select the tables and all Stored Procedures need to be used and click finish.</span></p>
<p><img id="144284" src="144284-8.png" alt="" width="421" height="340"></p>
<p><span>Here we can see now we have created our&nbsp;</span><em>OrderDetailModel</em><span>.</span></p>
<p><img id="144285" src="144285-9.png" alt="" width="575" height="343"></p>
<p><span>Once the Entity has been created the next step is to add a Web API to our controller and write function to Select/Insert/Update and Delete.</span><br>
<br>
<strong>Procedure to add our Web API Controller</strong><br>
<br>
<span>Right-click the Controllers folder, click Add and then click Controller.</span></p>
<p><img id="144287" src="144287-10.png" alt="" width="603" height="461"></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>Select Controller and add an Empty Web API 2 Controller. Provide your name to the Web API controller and click OK. Here for my Web API Controller I have given the name &ldquo;OrderAPIController&rdquo; .In this demo project I have created 2 different controller
 for Order master and order Detail.<br>
<br>
As we all know Web API is a simple and easy way to build HTTP Services for Browsers and Mobiles.<br>
Web API has the following four methods as&nbsp;<strong>Get/Post/Put and Delete</strong>&nbsp;where:</p>
<ul>
<li><strong>Get</strong>&nbsp;is to request for the data. (Select) </li><li><strong>Post</strong>&nbsp;is to create a data. (Insert) </li><li><strong>Put</strong>&nbsp;is to update the data. </li><li><strong>Delete</strong>&nbsp;is to delete data. </li></ul>
<p><br>
<strong>Get Method</strong><br>
<br>
In our example I have used only a Get method since I am using only a Stored Procedure. We need to create an object for our Entity and write our Get Method to do Select/Insert/Update and Delete operations.&nbsp;<br>
<br>
<strong>Select Operation</strong><br>
We use a get method to get all the details of the&nbsp;<strong>OrderMasters</strong>&nbsp;table using an entity object and we return the result as an IEnumerable. We use this method in our AngularJs and display the result in an MVC page from the AngularJs controller.
 Using Ng-Repeat we can bind the details.<br>
<br>
Here we can see in the get method I have passed the search parameter to the USP_OrderMaster_Select Stored Procedure. In the Stored Procedure I used like &quot;%&quot; to return all the records if the search parameter is empty.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">OrderManagementEntities objapi = new OrderManagementEntities();

                           // to Search Student Details and display the result
                           [HttpGet]
  public IEnumerable&lt;USP_OrderMaster_Select_Result&gt; Get(string OrderNO, string TableID)
      {
            if (OrderNO == null)
                   OrderNO = &quot;&quot;;
             if (TableID == null)
                   TableID = &quot;&quot;;
           return objapi.USP_OrderMaster_Select(OrderNO, TableID).AsEnumerable();
                           }</pre>
<div class="preview">
<pre class="csharp">OrderManagementEntities&nbsp;objapi&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OrderManagementEntities();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;to&nbsp;Search&nbsp;Student&nbsp;Details&nbsp;and&nbsp;display&nbsp;the&nbsp;result</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;USP_OrderMaster_Select_Result&gt;&nbsp;Get(<span class="cs__keyword">string</span>&nbsp;OrderNO,&nbsp;<span class="cs__keyword">string</span>&nbsp;TableID)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(OrderNO&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OrderNO&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(TableID&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TableID&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;objapi.USP_OrderMaster_Select(OrderNO,&nbsp;TableID).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span>Here in my example I have used the get method for Select/Insert/Update and Delete operations, since in my Stored Procedure after insert/update and delete I have returned the message from the database.</span><br>
<br>
<strong>Insert Operation</strong><br>
<span>The same as select I passed all the parameters to the insert procedure. This insert method will return the result from the database as a record is inserted or maybe not. I will get the result and display it from the AngularJs Controller to MVC application.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// To Insert new Student Details
               [HttpGet]
   public IEnumerable&lt;string&gt; insertOrderMaster(string Table_ID,string Description,string Waiter_Name)
      {
           return objapi.USP_OrderMaster_Insert( Table_ID, Description, Waiter_Name).AsEnumerable();
      }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;To&nbsp;Insert&nbsp;new&nbsp;Student&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;insertOrderMaster(<span class="cs__keyword">string</span>&nbsp;Table_ID,<span class="cs__keyword">string</span>&nbsp;Description,<span class="cs__keyword">string</span>&nbsp;Waiter_Name)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;objapi.USP_OrderMaster_Insert(&nbsp;Table_ID,&nbsp;Description,&nbsp;Waiter_Name).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Update Operation</strong><br>
<br>
<span>The same as Insert I have passed all the parameter to the insert procedure. This Update method will return the result from the database as a record is updated or maybe not. I will pass the OrderNo to the update procedure to update the record for the OrderNo.
 I will get the result and display it from the AngularJs Controller to the MVC application.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">//to Update Student Details
               [HttpGet]
    public IEnumerable&lt;string&gt; updateOrderMaster(int OrderNo, string Table_ID, string Description, string Waiter_Name)
      {
    return objapi.USP_OrderMaster_Update(OrderNo, Table_ID, Description, Waiter_Name).AsEnumerable();
       }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//to&nbsp;Update&nbsp;Student&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;updateOrderMaster(<span class="cs__keyword">int</span>&nbsp;OrderNo,&nbsp;<span class="cs__keyword">string</span>&nbsp;Table_ID,&nbsp;<span class="cs__keyword">string</span>&nbsp;Description,&nbsp;<span class="cs__keyword">string</span>&nbsp;Waiter_Name)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;objapi.USP_OrderMaster_Update(OrderNo,&nbsp;Table_ID,&nbsp;Description,&nbsp;Waiter_Name).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Delete Operation</strong><br>
<br>
<span>The same as update I have passed the OrderNo to the procedure to delete the record.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">//to Delete Student Details
   [HttpGet]
   public IEnumerable&lt;string&gt; deleteOrderMaster(int OrderNo)
    {
        return objapi.USP_OrderMaster_Delete(OrderNo).AsEnumerable();
    }</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//to&nbsp;Delete&nbsp;Student&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;public&nbsp;IEnumerable&lt;string&gt;&nbsp;deleteOrderMaster(int&nbsp;OrderNo)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;objapi.USP_OrderMaster_Delete(OrderNo).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span>Same like OrderMasterController I have created another controller as &ldquo;DetailAPI&rdquo; for Detail table CRUD Operations.Here is the complete code for detailController.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class DetailAPIController : ApiController
    {
          OrderManagementEntities objapi = new OrderManagementEntities();
       // to Search Student Details and display the result
 [HttpGet]
   public IEnumerable&lt;USP_OrderDetail_Select_Result&gt; Get(string OrderNO)
   {
         if (OrderNO == null)
            OrderNO = &quot;0&quot;;
           return objapi.USP_OrderDetail_Select(OrderNO).AsEnumerable();
           }
             // To Insert new Student Details
               [HttpGet]
 public IEnumerable&lt;string&gt; insertOrderDetail(string Order_No, string Item_Name, string Notes, string QTY, string Price)
  {
  return objapi.USP_OrderDetail_Insert(Order_No, Item_Name, Notes, QTY, Price).AsEnumerable();
               }

               //to Update Student Details
               [HttpGet]
               public IEnumerable&lt;string&gt; updateOrderDetail(int Order_Detail_No, string Order_No, string Item_Name, string Notes, string QTY, string Price)
               {
          return objapi.USP_OrderDetail_Update(Order_Detail_No, Order_No, Item_Name, Notes, QTY, Price).AsEnumerable();
               }

               //to Delete Student Details
  [HttpGet]
   public IEnumerable&lt;string&gt; deleteOrderDetail(int Order_Detail_No)
         {
             return objapi.USP_OrderDetail_Delete(Order_Detail_No).AsEnumerable();
      }
       }</pre>
<div class="preview">
<pre class="js">public&nbsp;class&nbsp;DetailAPIController&nbsp;:&nbsp;ApiController&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OrderManagementEntities&nbsp;objapi&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;OrderManagementEntities();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;to&nbsp;Search&nbsp;Student&nbsp;Details&nbsp;and&nbsp;display&nbsp;the&nbsp;result</span>&nbsp;
&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;public&nbsp;IEnumerable&lt;USP_OrderDetail_Select_Result&gt;&nbsp;Get(string&nbsp;OrderNO)&nbsp;
&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(OrderNO&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OrderNO&nbsp;=&nbsp;<span class="js__string">&quot;0&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;objapi.USP_OrderDetail_Select(OrderNO).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;To&nbsp;Insert&nbsp;new&nbsp;Student&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;public&nbsp;IEnumerable&lt;string&gt;&nbsp;insertOrderDetail(string&nbsp;Order_No,&nbsp;string&nbsp;Item_Name,&nbsp;string&nbsp;Notes,&nbsp;string&nbsp;QTY,&nbsp;string&nbsp;Price)&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;objapi.USP_OrderDetail_Insert(Order_No,&nbsp;Item_Name,&nbsp;Notes,&nbsp;QTY,&nbsp;Price).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//to&nbsp;Update&nbsp;Student&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;IEnumerable&lt;string&gt;&nbsp;updateOrderDetail(int&nbsp;Order_Detail_No,&nbsp;string&nbsp;Order_No,&nbsp;string&nbsp;Item_Name,&nbsp;string&nbsp;Notes,&nbsp;string&nbsp;QTY,&nbsp;string&nbsp;Price)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;objapi.USP_OrderDetail_Update(Order_Detail_No,&nbsp;Order_No,&nbsp;Item_Name,&nbsp;Notes,&nbsp;QTY,&nbsp;Price).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//to&nbsp;Delete&nbsp;Student&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;[HttpGet]&nbsp;
&nbsp;&nbsp;&nbsp;public&nbsp;IEnumerable&lt;string&gt;&nbsp;deleteOrderDetail(int&nbsp;Order_Detail_No)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;objapi.USP_OrderDetail_Delete(Order_Detail_No).AsEnumerable();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span>Now we have created our Web API Controller Class. The next step is to create our AngularJs Module and Controller. Let's see how to create our AngularJs Controller. In Visual Studio 2015 it's much easier to add our AngularJs
 Controller. Let's see step-by-step how to create and write our AngularJs Controller.</span><br>
<br>
<strong>Creating AngularJs Controller</strong><br>
<br>
<span>First create a folder inside the Script Folder and I have given the folder name as &ldquo;MyAngular&rdquo;.</span></div>
<p><img id="144288" src="144288-11.png" alt="" width="255" height="246"></p>
<p><span>Now add your Angular Controller inside the folder.</span><br>
<br>
<span>Right-click the MyAngular folder and click Add and New Item. Select Web and then AngularJs Controller and provide a name for the Controller. I have named my AngularJs Controller &ldquo;Controller.js&rdquo;.</span></p>
<p><img id="144289" src="144289-12.png" alt="" width="550" height="295"></p>
<p><span>Once the AngularJs Controller is created, we can see by default the controller will have the code with the default module definition and all.&nbsp;</span></p>
<p><img id="144290" src="144290-13.png" alt="" width="602" height="308"></p>
<p><span>I have changed the preceding code like adding a Module and controller as in the following.</span><br>
<br>
<span>If the AngularJs package is missing, then add the package to your project.</span><br>
<br>
<span>Right-click your MVC project and click Manage NuGet Packages. Search for AngularJs and click Install.</span></p>
<p><img id="144291" src="144291-12.png" alt="" width="550" height="395"></p>
<p><strong>Procedure to Create AngularJs Script Files</strong><br>
<br>
<strong>Modules.js:</strong>&nbsp;Here we will add the reference to the AngularJs JavaScript and create an Angular Module named &ldquo;RESTClientModule&rdquo;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">// &lt;reference path=&quot;../angular.js&quot; /&gt; 
/// &lt;reference path=&quot;../angular.min.js&quot; /&gt;  
/// &lt;reference path=&quot;../angular-animate.js&quot; /&gt;  
/// &lt;reference path=&quot;../angular-animate.min.js&quot; /&gt; 
var app;
(function () {
    app = angular.module(&quot;RESTClientModule&quot;, ['ngAnimate']);
})();</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.js&quot;&nbsp;/&gt;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular-animate.min.js&quot;&nbsp;/&gt;&nbsp;</span><span class="js__statement">var</span>&nbsp;app;&nbsp;
(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;app&nbsp;=&nbsp;angular.module(<span class="js__string">&quot;RESTClientModule&quot;</span>,&nbsp;[<span class="js__string">'ngAnimate'</span>]);&nbsp;
<span class="js__brace">}</span>)();</pre>
</div>
</div>
</div>
<p><strong>Controllers:</strong>&nbsp;In AngularJs Controller I have done all the business logic and returned the data from Web API to our MVC HTML page.</p>
<p><strong>1. Variable declarations</strong><br>
<br>
First I declared all the local variables that need to be used.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">app.controller(&quot;AngularJs_studentsController&quot;, function ($scope, $timeout, $rootScope, $window, $http) {

    $scope.date = new Date();
    $scope.MyName = &quot;shanu&quot;;

    //For Order Master Search
    $scope.OrderNos = &quot;&quot;;
    $scope.Table_IDs = &quot;&quot;;  

    //This variable will be used for Insert/Edit/Delete OrderMasters Table.
    $scope.OrderNo = 0;
    $scope.Table_ID = &quot;&quot;;
    $scope.Description = &quot;&quot;;
    $scope.Waiter_Name = &quot;&quot;;
    //Show Hide OrderMaster Table

    $scope.showOrderMasterAdd = true;
    $scope.addEditOrderMaster = false;
    $scope.OrderMasterList = true;
    $scope.showItem = true;

    //This variable will be used for Insert/Edit/Delete OrderDetail Table.
    $scope.Order_Detail_No = 0;
    $scope.Item_Name =&quot;&quot;;
    $scope.Notes = &quot;&quot;;
    $scope.QTY = &quot;1&quot;;
    $scope.Price = &quot;0&quot;;  

    $scope.addEditOrderDetail = false;
    $scope.expandImg = &quot;expand.png&quot;;</pre>
<div class="preview">
<pre class="js">app.controller(<span class="js__string">&quot;AngularJs_studentsController&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;($scope,&nbsp;$timeout,&nbsp;$rootScope,&nbsp;$window,&nbsp;$http)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.date&nbsp;=&nbsp;<span class="js__operator">new</span><span class="js__object">Date</span>();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.MyName&nbsp;=&nbsp;<span class="js__string">&quot;shanu&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//For&nbsp;Order&nbsp;Master&nbsp;Search</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderNos&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Table_IDs&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//This&nbsp;variable&nbsp;will&nbsp;be&nbsp;used&nbsp;for&nbsp;Insert/Edit/Delete&nbsp;OrderMasters&nbsp;Table.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderNo&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Table_ID&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Description&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Waiter_Name&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Show&nbsp;Hide&nbsp;OrderMaster&nbsp;Table</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showOrderMasterAdd&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderMaster&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasterList&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showItem&nbsp;=&nbsp;true;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//This&nbsp;variable&nbsp;will&nbsp;be&nbsp;used&nbsp;for&nbsp;Insert/Edit/Delete&nbsp;OrderDetail&nbsp;Table.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Order_Detail_No&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Item_Name&nbsp;=<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Notes&nbsp;=&nbsp;<span class="js__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.QTY&nbsp;=&nbsp;<span class="js__string">&quot;1&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Price&nbsp;=&nbsp;<span class="js__string">&quot;0&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderDetail&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.expandImg&nbsp;=&nbsp;<span class="js__string">&quot;expand.png&quot;</span>;</pre>
</div>
</div>
</div>
<p><strong>2. Methods</strong></p>
<p>Select Method&nbsp;</p>
<p>In the select method I have used $http.get to get the details from Web API. In the get method I will provide our API Controller name and method to get the details. Here we can see I have passed the search parameter of OrderNO and TableID using:</p>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">{ params: { OrderNO: OrderNos, TableID: Table_IDs }</pre>
<div class="preview">
<pre class="js"><span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;OrderNO:&nbsp;OrderNos,&nbsp;TableID:&nbsp;Table_IDs&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span>The final result will be displayed to the MVC HTML page using&nbsp;</span><strong>data-ng-repeat.</strong></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">$http.get('/api/OrderAPI/', { params: { OrderNO: OrderNos, TableID: Table_IDs } }).success(function (data) {

            $scope.OrderMasters = data;
            $scope.showOrderMasterAdd = true;
            $scope.addEditOrderMaster = false;
            $scope.OrderMasterList = true;
            $scope.showItem = true;
            $scope.addEditOrderDetail = false;
            if ($scope.OrderMasters.length &gt; 0) {
            }
        })
   .error(function () {
       $scope.error = &quot;An Error has occured while loading posts!&quot;;
   });
    }</pre>
<div class="preview">
<pre class="js">$http.get(<span class="js__string">'/api/OrderAPI/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;OrderNO:&nbsp;OrderNos,&nbsp;TableID:&nbsp;Table_IDs&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasters&nbsp;=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showOrderMasterAdd&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderMaster&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasterList&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showItem&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderDetail&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.OrderMasters.length&nbsp;&gt;&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Search Button Click</strong></div>
<div class="endscriptcode"><strong>&nbsp;</strong><br>
<img id="144292" src="144292-search.gif" alt="" width="600" height="318"></div>
<div class="endscriptcode"><br>
<span>In the search button click I will call the SearchMethod to bind the result. Here we can see in the search text box I have used&nbsp;</span><strong>ng-model=&quot;</strong><span>OrderNos</span><strong>&quot;</strong><span>. Using&nbsp;</span><strong>ng-model</strong><span>&nbsp;in
 the AngularJs Controller we can get the TextBox input value or we can set the value to the TextBox.</span></div>
</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;input type=&quot;text&quot; name=&quot;txtOrderNos&quot; ng-model=&quot;OrderNos&quot; value=&quot;&quot; /&gt;
&lt;input type=&quot;text&quot; name=&quot;txtTable_IDs&quot; ng-model=&quot;Table_IDs&quot; /&gt;&lt;input type=&quot;submit&quot; value=&quot;Search&quot; style=&quot;background-color:#336699;color:#FFFFFF&quot; ng-click=&quot;searchOrderMasters()&quot; /&gt;

//Search
    $scope.searchOrderMasters = function () {
        selectOrderMasters($scope.OrderNos, $scope.Table_IDs);
    }</pre>
<div class="preview">
<pre class="js">&lt;input&nbsp;type=<span class="js__string">&quot;text&quot;</span>&nbsp;name=<span class="js__string">&quot;txtOrderNos&quot;</span>&nbsp;ng-model=<span class="js__string">&quot;OrderNos&quot;</span>&nbsp;value=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&lt;input&nbsp;type=<span class="js__string">&quot;text&quot;</span>&nbsp;name=<span class="js__string">&quot;txtTable_IDs&quot;</span>&nbsp;ng-model=<span class="js__string">&quot;Table_IDs&quot;</span>&nbsp;<span class="js__reg_exp">/&gt;&lt;input&nbsp;type=&quot;submit&quot;&nbsp;value=&quot;Search&quot;&nbsp;style=&quot;background-color:#336699;color:#FFFFFF&quot;&nbsp;ng-click=&quot;searchOrderMasters()&quot;&nbsp;/</span>&gt;&nbsp;
&nbsp;
<span class="js__sl_comment">//Search</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.searchOrderMasters&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selectOrderMasters($scope.OrderNos,&nbsp;$scope.Table_IDs);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Insert new Order Master</strong><br>
<br>
<span>In the ADD New Student Detail button click I will make visible the StudentAdd table details where the user can enter the new student information. For a new student I will make the Student ID as 0. In the New Student save button click I will call the save
 method.</span></div>
</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">// New Student Add Details
    $scope.showOrderMasters = function () {
        cleardetails();
        $scope.addEditOrderDetail = false;
        $scope.showOrderMasterAdd = true;
        $scope.addEditOrderMaster = true;
        $scope.OrderMasterList = true;
        $scope.showItem = true;
    }</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;New&nbsp;Student&nbsp;Add&nbsp;Details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showOrderMasters&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderDetail&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showOrderMasterAdd&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderMaster&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasterList&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showItem&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span>In the Save method I will check for the OrderNo. If the OrderNo is &ldquo;0&rdquo; then it will insert the new Order Master details. Here I will call the Insert Web API method and if the OrderNo is &gt; 0 then that means
 that to update the Order record I will call the Update Web API method.</span></div>
</div>
<div class="endscriptcode"><img id="144294" src="144294-gma1.gif" alt="" width="600" height="328"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span>To the Insert Web API Method I will pass all the Input parameters. In my Stored Procedure I will check whether the Table Name for the Order already exists. If the Table name does not exist in the database then I will insert
 the records and return the success message as &ldquo;inserted&rdquo; and if the Table name already exists then I will return the message as &ldquo;Exists&rdquo;.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">//Save OrderMaster
    $scope.saveDetails = function () {
        $scope.IsFormSubmitted1 = true;
        if ($scope.IsFormValid1) {        
             if ($scope.OrderNo == 0) {
                $http.get('/api/OrderAPI/insertOrderMaster/', { params: { Table_ID: $scope.Table_ID, Description: $scope.Description, Waiter_Name: $scope.Waiter_Name } }).success(function (data) {
                    $scope.orderMasterInserted = data;
                    alert($scope.orderMasterInserted);
                    cleardetails();
                    selectOrderMasters('', '');
                })
         .error(function () {
             $scope.error = &quot;An Error has occured while loading posts!&quot;;
         });
            }
            else {  // to update to the student details
                $http.get('/api/OrderAPI/updateOrderMaster/', { params: { OrderNo: $scope.OrderNo, Table_ID: $scope.Table_ID, Description: $scope.Description, Waiter_Name: $scope.Waiter_Name } }).success(function (data) {
                    $scope.orderMasterUpdated = data;
                    alert($scope.orderMasterUpdated);
                    cleardetails();
                    selectOrderMasters('', '');
                })
        .error(function () {
            $scope.error = &quot;An Error has occured while loading posts!&quot;;
        });
            }
        }
        else {
            $scope.Message1 = &quot;All the fields are required.&quot;;
        }   
}</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//Save&nbsp;OrderMaster</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.saveDetails&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.IsFormSubmitted1&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.IsFormValid1)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.OrderNo&nbsp;==&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/OrderAPI/insertOrderMaster/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;Table_ID:&nbsp;$scope.Table_ID,&nbsp;Description:&nbsp;$scope.Description,&nbsp;Waiter_Name:&nbsp;$scope.Waiter_Name&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.orderMasterInserted&nbsp;=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert($scope.orderMasterInserted);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selectOrderMasters(<span class="js__string">''</span>,&nbsp;<span class="js__string">''</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;to&nbsp;update&nbsp;to&nbsp;the&nbsp;student&nbsp;details</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/OrderAPI/updateOrderMaster/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;OrderNo:&nbsp;$scope.OrderNo,&nbsp;Table_ID:&nbsp;$scope.Table_ID,&nbsp;Description:&nbsp;$scope.Description,&nbsp;Waiter_Name:&nbsp;$scope.Waiter_Name&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.orderMasterUpdated&nbsp;=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert($scope.orderMasterUpdated);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selectOrderMasters(<span class="js__string">''</span>,&nbsp;<span class="js__string">''</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Message1&nbsp;=&nbsp;<span class="js__string">&quot;All&nbsp;the&nbsp;fields&nbsp;are&nbsp;required.&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Update Order Master</strong></div>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><img id="144295" src="144295-gme2.gif" alt="" width="600" height="328"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span>The same as Insert I will display the update details for the user to edit the details and save it. In the Edit method I will get all the details for the row where the user clicks on the Edit Icon and sets all the results to
 the appropriate TextBox. In the Save button click I will call the save method to save all the changes to the database like Insert.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">//Edit Order Details
 $scope.OrderMasterEdit = function OrderMasterEdit(OrderNoss, Table_IDss, Descriptionss, Waiter_Namess) {
        cleardetails();
        $scope.OrderNo = OrderNoss;
        $scope.Table_ID = Table_IDss
        $scope.Description = Descriptionss;
        $scope.Waiter_Name = Waiter_Namess;   

        $scope.addEditOrderDetail = false;
        $scope.showOrderMasterAdd = true;
        $scope.addEditOrderMaster = true;
        $scope.OrderMasterList = true;
        $scope.showItem = true;
    }</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//Edit&nbsp;Order&nbsp;Details</span>&nbsp;
&nbsp;$scope.OrderMasterEdit&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;OrderMasterEdit(OrderNoss,&nbsp;Table_IDss,&nbsp;Descriptionss,&nbsp;Waiter_Namess)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderNo&nbsp;=&nbsp;OrderNoss;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Table_ID&nbsp;=&nbsp;Table_IDss&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Description&nbsp;=&nbsp;Descriptionss;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Waiter_Name&nbsp;=&nbsp;Waiter_Namess;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderDetail&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showOrderMasterAdd&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderMaster&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasterList&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showItem&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Delete Order Master Details</strong></div>
</div>
<div class="endscriptcode"><img id="144298" src="144298-gmd2.gif" alt="" width="600" height="324"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span>In the Delete button click, I will display the confirmation message to the user as to whether to delete the Order or not. If the user clicks the OK button I will pass the OrderNo to the delete method of the Web API to delete
 the record from the database.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">//Delete Order master Detail
    $scope.OrderMasterDelete = function OrderMasterDelete(OrderNoss) {
        cleardetails();
        $scope.OrderNo = OrderNoss;
    var delConfirm = confirm(&quot;Are you sure you want to delete the Order Master &quot; &#43; OrderNoss &#43; &quot; ?&quot;);
        if (delConfirm == true) {
         //   alert($scope.OrderNo);
            $http.get('/api/OrderAPI/deleteOrderMaster/', { params: { OrderNo: $scope.OrderNo } }).success(function (data) {
               // alert(data);
                $scope.orderMasterDeleted= data;
                alert($scope.orderMasterDeleted);
                cleardetails();
                selectOrderMasters('', '');
            })
      .error(function () {
          $scope.error = &quot;An Error has occured while loading posts!&quot;;
      });
        }
    }</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//Delete&nbsp;Order&nbsp;master&nbsp;Detail</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasterDelete&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;OrderMasterDelete(OrderNoss)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderNo&nbsp;=&nbsp;OrderNoss;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;delConfirm&nbsp;=&nbsp;confirm(<span class="js__string">&quot;Are&nbsp;you&nbsp;sure&nbsp;you&nbsp;want&nbsp;to&nbsp;delete&nbsp;the&nbsp;Order&nbsp;Master&nbsp;&quot;</span>&nbsp;&#43;&nbsp;OrderNoss&nbsp;&#43;&nbsp;<span class="js__string">&quot;&nbsp;?&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(delConfirm&nbsp;==&nbsp;true)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;&nbsp;&nbsp;alert($scope.OrderNo);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$http.get(<span class="js__string">'/api/OrderAPI/deleteOrderMaster/'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;params:&nbsp;<span class="js__brace">{</span>&nbsp;OrderNo:&nbsp;$scope.OrderNo&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__brace">}</span>).success(<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;alert(data);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.orderMasterDeleted=&nbsp;data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert($scope.orderMasterDeleted);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cleardetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selectOrderMasters(<span class="js__string">''</span>,&nbsp;<span class="js__string">''</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.error(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.error&nbsp;=&nbsp;<span class="js__string">&quot;An&nbsp;Error&nbsp;has&nbsp;occured&nbsp;while&nbsp;loading&nbsp;posts!&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Filter and Sorting Order Master</strong></div>
</div>
<div class="endscriptcode"><img id="144299" src="144299-gms3.gif" alt="" width="600" height="324"></div>
<div class="endscriptcode">
<p>The filters can be added with the ng-repeat using the pipe symbol.</p>
<p>Here we can see with ng-repeat we have added the filter and for the filter we have given the TextBox Model id. When the user presses the key on the TextBox the filter will be applied for the loop and display the appropriate value as in the following:</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;/tr&gt;  
&lt;tr style=&quot;height: 30px; background-color:#336699 ; color:#FFFFFF ;border: solid 1px #659EC7;&quot;&gt;  
    &lt;td width=&quot;100&quot; align=&quot;center&quot; colspan=&quot;3&quot;&gt; &lt;img src=&quot;~/Images/filter.png&quot; /&gt; Filter By &lt;/td&gt;  
    &lt;td width=&quot;180&quot; align=&quot;center&quot; style=&quot;border: solid 1px #FFFFFF; padding: 5px;table-layout:fixed;&quot;&gt;  
        &lt;input ng-model=&quot;search.Order_No&quot; placeholder=&quot;Order...&quot; width=&quot;90&quot;&gt; &lt;/td&gt;  
    &lt;td width=&quot;180&quot; align=&quot;center&quot; style=&quot;border: solid 1px #FFFFFF; padding: 5px;table-layout:fixed;&quot;&gt;  
        &lt;input ng-model=&quot;search.Table_ID&quot; placeholder=&quot;Table...&quot;&gt; &lt;/td&gt;  
    &lt;td width=&quot;200&quot; align=&quot;center&quot; style=&quot;border: solid 1px #FFFFFF; padding: 5px;table-layout:fixed;&quot;&gt; &lt;/td&gt;  
    &lt;td width=&quot;200&quot; align=&quot;center&quot; style=&quot;border: solid 1px #FFFFFF; padding: 5px;table-layout:fixed;&quot;&gt; &lt;/td&gt;  
    &lt;td width=&quot;200&quot; align=&quot;center&quot; style=&quot;border: solid 1px #FFFFFF; padding: 5px;table-layout:fixed;&quot;&gt;  
        &lt;input ng-model=&quot;search.Waiter_Name&quot; placeholder=&quot;Name...&quot;&gt; &lt;/td&gt;  
&lt;/tr&gt;  </pre>
<div class="preview">
<pre class="js">&lt;/tr&gt;&nbsp;&nbsp;&nbsp;
&lt;tr&nbsp;style=<span class="js__string">&quot;height:&nbsp;30px;&nbsp;background-color:#336699&nbsp;;&nbsp;color:#FFFFFF&nbsp;;border:&nbsp;solid&nbsp;1px&nbsp;#659EC7;&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;td&nbsp;width=<span class="js__string">&quot;100&quot;</span>&nbsp;align=<span class="js__string">&quot;center&quot;</span>&nbsp;colspan=<span class="js__string">&quot;3&quot;</span>&gt;&nbsp;&lt;img&nbsp;src=<span class="js__string">&quot;~/Images/filter.png&quot;</span>&nbsp;<span class="js__reg_exp">/&gt;&nbsp;Filter&nbsp;By&nbsp;&lt;/</span>td&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;td&nbsp;width=<span class="js__string">&quot;180&quot;</span>&nbsp;align=<span class="js__string">&quot;center&quot;</span>&nbsp;style=<span class="js__string">&quot;border:&nbsp;solid&nbsp;1px&nbsp;#FFFFFF;&nbsp;padding:&nbsp;5px;table-layout:fixed;&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;input&nbsp;ng-model=<span class="js__string">&quot;search.Order_No&quot;</span>&nbsp;placeholder=<span class="js__string">&quot;Order...&quot;</span>&nbsp;width=<span class="js__string">&quot;90&quot;</span>&gt;&nbsp;&lt;/td&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;td&nbsp;width=<span class="js__string">&quot;180&quot;</span>&nbsp;align=<span class="js__string">&quot;center&quot;</span>&nbsp;style=<span class="js__string">&quot;border:&nbsp;solid&nbsp;1px&nbsp;#FFFFFF;&nbsp;padding:&nbsp;5px;table-layout:fixed;&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;input&nbsp;ng-model=<span class="js__string">&quot;search.Table_ID&quot;</span>&nbsp;placeholder=<span class="js__string">&quot;Table...&quot;</span>&gt;&nbsp;&lt;/td&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;td&nbsp;width=<span class="js__string">&quot;200&quot;</span>&nbsp;align=<span class="js__string">&quot;center&quot;</span>&nbsp;style=<span class="js__string">&quot;border:&nbsp;solid&nbsp;1px&nbsp;#FFFFFF;&nbsp;padding:&nbsp;5px;table-layout:fixed;&quot;</span>&gt;&nbsp;&lt;/td&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;td&nbsp;width=<span class="js__string">&quot;200&quot;</span>&nbsp;align=<span class="js__string">&quot;center&quot;</span>&nbsp;style=<span class="js__string">&quot;border:&nbsp;solid&nbsp;1px&nbsp;#FFFFFF;&nbsp;padding:&nbsp;5px;table-layout:fixed;&quot;</span>&gt;&nbsp;&lt;/td&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;td&nbsp;width=<span class="js__string">&quot;200&quot;</span>&nbsp;align=<span class="js__string">&quot;center&quot;</span>&nbsp;style=<span class="js__string">&quot;border:&nbsp;solid&nbsp;1px&nbsp;#FFFFFF;&nbsp;padding:&nbsp;5px;table-layout:fixed;&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;input&nbsp;ng-model=<span class="js__string">&quot;search.Waiter_Name&quot;</span>&nbsp;placeholder=<span class="js__string">&quot;Name...&quot;</span>&gt;&nbsp;&lt;/td&gt;&nbsp;&nbsp;&nbsp;
&lt;/tr&gt;&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong>Sorting Order Master</strong></div>
<p>The same as for a filter we add the orderBy with field and reverse value in ng-repat using the pipe symbol.<br>
The OrderBy can be added with the ng-repeat using the pipe symbol, for example let's consider the preceding example.</p>
<p>And in ng-repeat we will be giving the search by filter which will be filters all the textbox values which we enter and produce the filtered result.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;tbody data-ng-repeat=&quot;stds in OrderMasters | filter:search | orderBy:predicate:reverse&quot;&gt;</pre>
<div class="preview">
<pre class="js">&lt;tbody&nbsp;data-ng-repeat=<span class="js__string">&quot;stds&nbsp;in&nbsp;OrderMasters&nbsp;|&nbsp;filter:search&nbsp;|&nbsp;orderBy:predicate:reverse&quot;</span>&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<strong>Displaying Order Detail</strong></div>
<p><img id="144301" src="144301-gddetail.gif" alt="" width="600" height="298"></p>
<p>Here we can see how I have displayed the Order Detail grid inside the Order Master by clicking the Detail button click.</p>
<p>In each Order Master Row click I will check for the active row. And then I will detail button click I call the showNewOrderDetails() method to display the details.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;tr ng-show=&quot;activeRow==stds.Order_No&quot; &gt;</pre>
<div class="preview">
<pre class="js">&lt;tr&nbsp;ng-show=<span class="js__string">&quot;activeRow==stds.Order_No&quot;</span>&nbsp;&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span>In detail button click I call the</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">&lt;input type=&quot;button&quot; value=&quot;Add Detail&quot; style=&quot;background-color:#439633;color:#FFFFFF;font-size:large;width:100px;border-color:#a2aabe;border-style:dashed;border-width:2px;&quot; ng-click=&quot;showNewOrderDetails()&quot; /&gt;

// New Detail Add
    $scope.showNewOrderDetails = function () {      
        clearOrderdetails();
        $scope.showOrderMasterAdd = false;
        $scope.addEditOrderMaster = false;
        $scope.OrderMasterList = true;
        $scope.showItem = true;
        $scope.addEditOrderDetail = true;
    }</pre>
<div class="preview">
<pre class="js">&lt;input&nbsp;type=<span class="js__string">&quot;button&quot;</span>&nbsp;value=<span class="js__string">&quot;Add&nbsp;Detail&quot;</span>&nbsp;style=<span class="js__string">&quot;background-color:#439633;color:#FFFFFF;font-size:large;width:100px;border-color:#a2aabe;border-style:dashed;border-width:2px;&quot;</span>&nbsp;ng-click=<span class="js__string">&quot;showNewOrderDetails()&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;New&nbsp;Detail&nbsp;Add</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showNewOrderDetails&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clearOrderdetails();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showOrderMasterAdd&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderMaster&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.OrderMasterList&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showItem&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.addEditOrderDetail&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p>For order Detail CRUD, Sorting and Filtering the same logic as we have seen for Order master has been used .here we will see output of</p>
<p><strong>Order Detail Add:</strong></p>
<p><img id="144302" src="144302-gda4.gif" alt="" width="592" height="300"></p>
<p><strong>Order Detail Edit:</strong></p>
<p><img id="144303" src="144303-gde5.gif" alt="" width="574" height="300"></p>
<p><strong>Order Detail Delete:</strong></p>
<p><img id="144304" src="144304-gdd7.gif" alt="" width="600" height="362"></p>
<p><strong>Order Detail Filtering and Sorting:</strong></p>
<p><img id="144305" src="144305-gds6.gif" alt="" width="600" height="372"></p>
<p><span style="font-size:2em">Source Code Files</span></p>
<ul>
<li><span>MasterDetailCRUDV1.1.zip</span> </li></ul>
<h1>More Information</h1>
<p><em><span>The main aim of this article is to create a simple MVC Web Based &nbsp;Master/Detail CRUD,Filtering and Sorting Operation using Angular JS WEB API 2 with Stored Procedure.</span></em></p>
