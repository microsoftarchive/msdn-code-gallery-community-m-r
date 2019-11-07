# MVC Dynamic Menu Creation Using AngularJS and WCF Rest
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- WCF
- ASP.NET MVC
- AngularJS
## Topics
- WCF
- ASP.NET MVC
- AngularJS
- WCF Rest Service
## Updated
- 01/06/2016
## Description

<h1>Introduction</h1>
<div><em><img id="141033" src="141033-shanumenuangular.gif" alt="" width="601" height="312"></em></div>
<div><em>&nbsp;&nbsp;This article shows how to create a menu dynamically from a database using AngularJS, MVC and WCF Rest service.</em></div>
<h4 style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Why we need to create a Dynamic Menu</span></strong></h4>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
If we are working on a simple web site creation with very few pages and only one programmer is working to create a website then in that case we can create a static menu and use it in our web site.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
Let's now consider we need to work for a big web application project. Let's consider development of an ERP Web application.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
However if more than two developers are working and perhaps the number&nbsp;of pages is greater than 50 to 100 then it will be hard to maintain a static menu.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
And also there will be a greater chance of removing and adding a new menu item to the web project, for example our client can ask to add 5 more new menus or remove 1 menu item.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
In this case it will be a hard and difficult task to remove the menu Items that are live now.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
And also for large web projects like ERP we need to display the menu depending on the user roles. If we use a static menu then it will be very difficult to manage the users for the menu.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
To avoid all this we create a Menu Management with a user role setting.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px">Who can manage the Menu</strong></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
This is a very important part since an Admin or Super user can Add/Edit/Delete a menu and a user.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
When an Admin is logged in he can add a new menu, edit an existing menu and delete a menu item to be displayed.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
This article is not focused on menu management but in this article we will see in detail how to create a Menu Master and Menu Detail Table. Insert a sample Menu Item to our Database Tables. Display the Menu from the database dynamically to our MVC Web page
 using AngularJS and WCF Rest Service.<span style="outline:0px"><span>&nbsp;</span>This article will explain:</span></div>
<ol style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px">How to create a WCF Rest service and retrieve data from a database.
</li><li style="outline:0px">How to install the AngularJS Package into a MVC application.
</li><li style="outline:0px">How to create our AngularJS application for Dynamic Menu Creation.
</li><li style="outline:0px">How to use a WCS service in AngularJS to display dynamic Menu.
</li></ol>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Note:</span></strong><span style="outline:0px"><span>&nbsp;</span>the prerequisites are Visual Studio 2013 (if you don't have Visual Studio 2013, you can download it from<span>&nbsp;</span><a href="http://www.visualstudio.com/en-us/products/visual-studio-community-vs" target="_blank" style="outline:0px; text-decoration:none">Microsoft</a>.(You
 can download latest Visual Studio 2015 and use for the same).<br style="outline:0px">
Here we can see some basics and reference links for Windows Communication Foundation (WCF). WCF is a framework for building service-oriented applications.<br style="outline:0px">
<strong style="outline:0px"><br style="outline:0px">
<strong style="outline:0px"><span style="outline:0px">Service-oriented application:<span>&nbsp;</span></span></strong></strong>Using this protocol the service can be shared and used over a network.<br style="outline:0px">
<br style="outline:0px">
For example, let's consider that we are now working on a project and we need to create some common database function and those functions need to be used in multiple projects and the projects are in multiple places and connected via a network such as the internet.<br style="outline:0px">
<br style="outline:0px">
In this case we can create a WCF service and we can write all our common database functions in our WCF service class. We can deploy our WCF in IIS and use the URL in our application to do DB functions. In the code part let's see how to create a WCF REST service
 and use it in our AngularJS application.<span>&nbsp;</span><br style="outline:0px">
<br style="outline:0px">
If you are interested in reading more details about WCF then kindly go to this<span>&nbsp;</span><a href="https://msdn.microsoft.com/en-in/library/dd203052.aspx" target="_blank" style="outline:0px; text-decoration:none">link</a>.<br style="outline:0px">
<br style="outline:0px">
<strong style="outline:0px"><span style="outline:0px">AngularJS<span>&nbsp;</span></span></strong><strong style="outline:0px"><br style="outline:0px">
</strong><br style="outline:0px">
We might be be familiar with what Model, View and View Model (MVVM) is and what Model, View and Controller (MVC) is. AngularJS is a JavaScript framework that is purely based on HTML, CSS and JavaScript.<br style="outline:0px">
<br style="outline:0px">
Similar to the MVC and MVVM patterns AngularJS uses the Model, View and Whatever (MVW) pattern.<br style="outline:0px">
<br style="outline:0px">
In our example I have used a Model, View and Service. In the code part let's see how to install and create AngularJS in our MVC application.<span>&nbsp;</span><br style="outline:0px">
<br style="outline:0px">
If you are interested in reading more details about AngularJS then kindly go to this link.</span></div>
<h1><span>Building the Sample</span></h1>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">We will create a<span>&nbsp;</span></span><span style="outline:0px">MenuMaster<span>&nbsp;</span></span><span style="outline:0px"><span style="outline:0px">MenuDetails<span>&nbsp;</span></span><span style="outline:0px">table under
 the Database MenuDB.</span></span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Note: The MenuMaster and MenuDetail are the important tables that will use to load our menu dynamically. We need to understand how to insert menu details to these tables to display our menu properly.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">In this article I have displayed a 3-level hierarchical display of menus. Here you can see the 3-level hierarchical sample.</span></div>
<div><span style="font-size:20px; font-weight:bold"><img id="141035" src="141035-m4.jpg" alt="" width="336" height="108"></span></div>
<div><span style="font-size:20px; font-weight:bold">&nbsp;</span>&nbsp;</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Here we can see that the first level of the hierarchy is the Inventory.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">The 3<sup style="outline:0px">rd</sup><span>&nbsp;</span>level of the hierarchy is the Finished Goods Receipt and Finished Goods Issue.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Now let's see how to create a table relationship to create the master and detail menus.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Menu Master Table</span></strong></div>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">1st Level hierarchy Insert</strong>
</li></ul>
<div style="outline:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Root','Inventory','Shanu',getdate()-23) </pre>
<div class="preview">
<pre class="mysql"><span class="sql__keyword">Insert</span><span class="sql__keyword">into</span><span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Root'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;</pre>
</div>
</div>
</div>
</div>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">2nd Level hierarchies Insert</strong>
</li></ul>
<div style="outline:0px"><strong><span style="color:#333333">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('Inventory','INV001','Shanu',getdate()-23) </pre>
<div class="preview">
<pre class="mysql"><span class="sql__keyword">Insert</span><span class="sql__keyword">into</span><span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Inventory'</span>,<span class="sql__string">'INV001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;</pre>
</div>
</div>
</div>
</span></strong></div>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">3rd Level hierarchies Insert</strong>
</li></ul>
<div style="outline:0px"><strong><span style="color:#333333">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('INV001','FG001','Shanu',getdate()-23)  
  
Insert into MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate) values('INV001','FG002','Shanu',getdate()-23)  </pre>
<div class="preview">
<pre class="js">Insert&nbsp;into&nbsp;MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate)&nbsp;values(<span class="js__string">'INV001'</span>,<span class="js__string">'FG001'</span>,<span class="js__string">'Shanu'</span>,getdate()-<span class="js__num">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
Insert&nbsp;into&nbsp;MenuMaster(Menu_RootID,Menu_ChildID,UserID,CreatedDate)&nbsp;values(<span class="js__string">'INV001'</span>,<span class="js__string">'FG002'</span>,<span class="js__string">'Shanu'</span>,getdate()-<span class="js__num">23</span>)&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Here we can see the fields
 for the Menu Master. I have used the following fields:</span></span></strong></div>
<div style="outline:0px"><strong><span style="color:#333333"><img id="141036" src="141036-14.jpg" alt="" width="546" height="226"></span></strong></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Menu Detail Table</span></strong></div>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">1st Level hierarchy Insert</strong>
</li></ul>
<div><span style="font-size:20px; font-weight:bold">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,  
MenuURL,UserID,CreatedDate)  
values('Inventory','Inventory','Inventory','Index','Inventory',  
'Shanu',getdate()-23)  </pre>
<div class="preview">
<pre class="js">Insert&nbsp;into&nbsp;MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,&nbsp;&nbsp;&nbsp;
MenuURL,UserID,CreatedDate)&nbsp;&nbsp;&nbsp;
values(<span class="js__string">'Inventory'</span>,<span class="js__string">'Inventory'</span>,<span class="js__string">'Inventory'</span>,<span class="js__string">'Index'</span>,<span class="js__string">'Inventory'</span>,&nbsp;&nbsp;&nbsp;
<span class="js__string">'Shanu'</span>,getdate()-<span class="js__num">23</span>)&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">2nd Level hierarchies Insert</strong>
</li></ul>
</div>
<div style="outline:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,  
MenuURL,UserID,CreatedDate)  
values('INV001','Inventory','Inventory Details','Index','Inventory',  
'Shanu',getdate()-23)  </pre>
<div class="preview">
<pre class="js">Insert&nbsp;into&nbsp;MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,&nbsp;&nbsp;&nbsp;
MenuURL,UserID,CreatedDate)&nbsp;&nbsp;&nbsp;
values(<span class="js__string">'INV001'</span>,<span class="js__string">'Inventory'</span>,<span class="js__string">'Inventory&nbsp;Details'</span>,<span class="js__string">'Index'</span>,<span class="js__string">'Inventory'</span>,&nbsp;&nbsp;&nbsp;
<span class="js__string">'Shanu'</span>,getdate()-<span class="js__num">23</span>)&nbsp;&nbsp;</pre>
</div>
</div>
</div>
</div>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">3rd Level hierarchies Insert</strong>
</li></ul>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,  
MenuURL,UserID,CreatedDate)  
values('FG001','FGIN','FG Receipt','FGIN','Inventory','Shanu',getdate()-43)  
Insert into MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,  
MenuURL,UserID,CreatedDate)  
values('FG002','FGOUT','FG Issue','FGOUT','Inventory','Shanu',getdate()-13)  </pre>
<div class="preview">
<pre class="js">Insert&nbsp;into&nbsp;MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,&nbsp;&nbsp;&nbsp;
MenuURL,UserID,CreatedDate)&nbsp;&nbsp;&nbsp;
values(<span class="js__string">'FG001'</span>,<span class="js__string">'FGIN'</span>,<span class="js__string">'FG&nbsp;Receipt'</span>,<span class="js__string">'FGIN'</span>,<span class="js__string">'Inventory'</span>,<span class="js__string">'Shanu'</span>,getdate()-<span class="js__num">43</span>)&nbsp;&nbsp;&nbsp;
Insert&nbsp;into&nbsp;MenuDetails(Menu_ChildID,MenuName,MenuDisplayTxt,MenuFileName,&nbsp;&nbsp;&nbsp;
MenuURL,UserID,CreatedDate)&nbsp;&nbsp;&nbsp;
values(<span class="js__string">'FG002'</span>,<span class="js__string">'FGOUT'</span>,<span class="js__string">'FG&nbsp;Issue'</span>,<span class="js__string">'FGOUT'</span>,<span class="js__string">'Inventory'</span>,<span class="js__string">'Shanu'</span>,getdate()-<span class="js__num">13</span>)&nbsp;&nbsp;</pre>
</div>
</div>
</div>
</ul>
<div class="endscriptcode"><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Here we can see the
 field for the Menu Detail. I have used the following fields</span><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">.</span></div>
</span>
<div class="endscriptcode">&nbsp;<img id="141037" src="141037-15.jpg" alt="" width="546" height="365"></div>
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">The following is the script
 to create a database, table and sample insert query. Run this script in your SQL Server. I have used SQL Server 2012.</span>
</ul>
</div>
<div>&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">-- =============================================                                  
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
                 ON A.Menu_ChildID=B.Menu_ChildID </pre>
<div class="preview">
<pre class="mysql"><span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;date&nbsp;:&nbsp;2015-03-20&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Description&nbsp;:&nbsp;To&nbsp;Create&nbsp;Database,Table&nbsp;and&nbsp;Sample&nbsp;Insert&nbsp;Query&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Latest&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modifier&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Shanu&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Modify&nbsp;date&nbsp;:&nbsp;2015-03-20&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;=============================================&nbsp;&nbsp;</span>&nbsp;
--<span class="sql__id">Script</span>&nbsp;<span class="sql__keyword">to</span>&nbsp;<span class="sql__keyword">create</span>&nbsp;<span class="sql__id">DB</span>,<span class="sql__keyword">Table</span>&nbsp;<span class="sql__keyword">and</span>&nbsp;<span class="sql__id">sample</span>&nbsp;<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">data</span>&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">USE</span>&nbsp;<span class="sql__keyword">MASTER</span>&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;1)&nbsp;Check&nbsp;for&nbsp;the&nbsp;Database&nbsp;Exists&nbsp;.If&nbsp;the&nbsp;database&nbsp;is&nbsp;exist&nbsp;then&nbsp;drop&nbsp;and&nbsp;create&nbsp;new&nbsp;DB&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(<span class="sql__keyword">SELECT</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">sys</span>.<span class="sql__keyword">databases</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;=&nbsp;<span class="sql__string">'MenuDB'</span>&nbsp;)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">DROP</span>&nbsp;<span class="sql__keyword">DATABASE</span>&nbsp;<span class="sql__id">MenuDB</span>&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">DATABASE</span>&nbsp;<span class="sql__id">MenuDB</span>&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">USE</span>&nbsp;<span class="sql__id">MenuDB</span>&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;1)&nbsp;////////////&nbsp;ToysDetails&nbsp;table&nbsp;&nbsp;</span>&nbsp;
<span class="sql__com">--&nbsp;Create&nbsp;Table&nbsp;&nbsp;ToysDetails&nbsp;,This&nbsp;table&nbsp;will&nbsp;be&nbsp;used&nbsp;to&nbsp;store&nbsp;the&nbsp;details&nbsp;like&nbsp;Toys&nbsp;Information&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(&nbsp;<span class="sql__keyword">SELECT</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">sys</span>.<span class="sql__keyword">tables</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;=&nbsp;<span class="sql__string">'MenuMaster'</span>&nbsp;)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">DROP</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;<span class="sql__id">MenuMaster</span>&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;<span class="sql__id">MenuMaster</span>&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">Menu_ID</span>&nbsp;<span class="sql__keyword">int</span>&nbsp;<span class="sql__id">identity</span>(<span class="sql__number">1</span>,<span class="sql__number">1</span>),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">Menu_RootID</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">30</span>)&nbsp;&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">Menu_ChildID</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">30</span>)&nbsp;&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">UserID</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">50</span>),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">CreatedDate</span>&nbsp;<span class="sql__keyword">datetime</span>&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">CONSTRAINT</span>&nbsp;[<span class="sql__id">PK_MenuMaster</span>]&nbsp;<span class="sql__keyword">PRIMARY</span>&nbsp;<span class="sql__keyword">KEY</span>&nbsp;<span class="sql__id">CLUSTERED</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;[<span class="sql__id">Menu_ID</span>]&nbsp;<span class="sql__keyword">ASC</span>&nbsp;&nbsp;&nbsp;,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;[<span class="sql__id">Menu_RootID</span>]&nbsp;<span class="sql__keyword">ASC</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;[<span class="sql__id">Menu_ChildID</span>]&nbsp;<span class="sql__keyword">ASC</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
)<span class="sql__keyword">WITH</span>&nbsp;(<span class="sql__id">PAD_INDEX</span>&nbsp;&nbsp;=&nbsp;<span class="sql__id">OFF</span>,&nbsp;<span class="sql__id">STATISTICS_NORECOMPUTE</span>&nbsp;&nbsp;=&nbsp;<span class="sql__id">OFF</span>,&nbsp;<span class="sql__id">IGNORE_DUP_KEY</span>&nbsp;=&nbsp;<span class="sql__id">OFF</span>,&nbsp;<span class="sql__id">ALLOW_ROW_LOCKS</span>&nbsp;&nbsp;=&nbsp;<span class="sql__keyword">ON</span>,&nbsp;<span class="sql__id">ALLOW_PAGE_LOCKS</span>&nbsp;&nbsp;=&nbsp;<span class="sql__keyword">ON</span>)&nbsp;<span class="sql__keyword">ON</span>&nbsp;[<span class="sql__keyword">PRIMARY</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
)&nbsp;<span class="sql__keyword">ON</span>&nbsp;[<span class="sql__keyword">PRIMARY</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
--<span class="sql__keyword">delete</span>&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">MenuMaster</span>&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;Insert&nbsp;the&nbsp;sample&nbsp;records&nbsp;to&nbsp;the&nbsp;ToysDetails&nbsp;Table&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Root'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
--<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Home'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Home'</span>,<span class="sql__string">'About'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Home'</span>,<span class="sql__string">'Contact'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Root'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Masters'</span>,<span class="sql__string">'ITM001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
--<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'ITM001'</span>,<span class="sql__string">'ITM001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'ITM001'</span>,<span class="sql__string">'ITM002'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'ITM001'</span>,<span class="sql__string">'ITM003'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Masters'</span>,<span class="sql__string">'CAT001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'CAT001'</span>,<span class="sql__string">'CAT001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'CAT001'</span>,<span class="sql__string">'CAT002'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'CAT001'</span>,<span class="sql__string">'CAT003'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Root'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'Inventory'</span>,<span class="sql__string">'INV001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'INV001'</span>,<span class="sql__string">'FG001'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuMaster</span>(<span class="sql__id">Menu_RootID</span>,<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;<span class="sql__keyword">values</span>(<span class="sql__string">'INV001'</span>,<span class="sql__string">'FG002'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">select</span>&nbsp;*&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">MenuMaster</span>&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;1)&nbsp;END&nbsp;//&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__com">--&nbsp;2)&nbsp;Cart&nbsp;Details&nbsp;Table&nbsp;&nbsp;</span>&nbsp;
<span class="sql__keyword">IF</span>&nbsp;<span class="sql__keyword">EXISTS</span>&nbsp;(&nbsp;<span class="sql__keyword">SELECT</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;<span class="sql__keyword">FROM</span>&nbsp;<span class="sql__id">sys</span>.<span class="sql__keyword">tables</span>&nbsp;<span class="sql__keyword">WHERE</span>&nbsp;[<span class="sql__keyword">name</span>]&nbsp;=&nbsp;<span class="sql__string">'MenuDetails'</span>&nbsp;)&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">DROP</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;<span class="sql__id">MenuDetails</span>&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;<span class="sql__id">MenuDetails</span>&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">MDetail_ID</span>&nbsp;<span class="sql__keyword">int</span>&nbsp;<span class="sql__id">identity</span>(<span class="sql__number">1</span>,<span class="sql__number">1</span>),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">Menu_ChildID</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">20</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">MenuName</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">MenuDisplayTxt</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">200</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">MenuFileName</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">100</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">MenuURL</span>&nbsp;<span class="sql__keyword">VARCHAR</span>(<span class="sql__number">500</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">USE_YN</span>&nbsp;<span class="sql__keyword">Char</span>(<span class="sql__number">1</span>)&nbsp;<span class="sql__keyword">DEFAULT</span>&nbsp;<span class="sql__string">'Y'</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">UserID</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">50</span>),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="sql__id">CreatedDate</span>&nbsp;<span class="sql__keyword">datetime</span>&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">CONSTRAINT</span>&nbsp;[<span class="sql__id">PK_MenuDetails</span>]&nbsp;<span class="sql__keyword">PRIMARY</span>&nbsp;<span class="sql__keyword">KEY</span>&nbsp;<span class="sql__id">CLUSTERED</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;[<span class="sql__id">MDetail_ID</span>]&nbsp;<span class="sql__keyword">ASC</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;[<span class="sql__id">Menu_ChildID</span>]&nbsp;<span class="sql__keyword">ASC</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
)<span class="sql__keyword">WITH</span>&nbsp;(<span class="sql__id">PAD_INDEX</span>&nbsp;&nbsp;=&nbsp;<span class="sql__id">OFF</span>,&nbsp;<span class="sql__id">STATISTICS_NORECOMPUTE</span>&nbsp;&nbsp;=&nbsp;<span class="sql__id">OFF</span>,&nbsp;<span class="sql__id">IGNORE_DUP_KEY</span>&nbsp;=&nbsp;<span class="sql__id">OFF</span>,&nbsp;<span class="sql__id">ALLOW_ROW_LOCKS</span>&nbsp;&nbsp;=&nbsp;<span class="sql__keyword">ON</span>,&nbsp;<span class="sql__id">ALLOW_PAGE_LOCKS</span>&nbsp;&nbsp;=&nbsp;<span class="sql__keyword">ON</span>)&nbsp;<span class="sql__keyword">ON</span>&nbsp;[<span class="sql__keyword">PRIMARY</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
)&nbsp;<span class="sql__keyword">ON</span>&nbsp;[<span class="sql__keyword">PRIMARY</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__id">GO</span>&nbsp;&nbsp;&nbsp;
----<span class="sql__keyword">delete</span>&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">MenuDetails</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'Root'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu&nbsp;Home'</span>,<span class="sql__string">'Index'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'Home'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu&nbsp;Home'</span>,<span class="sql__string">'Index'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'About'</span>,<span class="sql__string">'About'</span>,<span class="sql__string">'About&nbsp;Shanu'</span>,<span class="sql__string">'About'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">43</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'Contact'</span>,<span class="sql__string">'Contact'</span>,<span class="sql__string">'Contact&nbsp;Shanu'</span>,<span class="sql__string">'Contact'</span>,<span class="sql__string">'Home'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'Masters'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'ItemDetails'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'ITM001'</span>,<span class="sql__string">'ItemMaster'</span>,<span class="sql__string">'Item&nbsp;Master'</span>,<span class="sql__string">'ItemDetails'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'ITM002'</span>,<span class="sql__string">'ItemDetail'</span>,<span class="sql__string">'Item&nbsp;Details'</span>,<span class="sql__string">'ItemDetails'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'ITM003'</span>,<span class="sql__string">'ItemManage'</span>,<span class="sql__string">'Item&nbsp;Manage'</span>,<span class="sql__string">'ItemManage'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'CAT001'</span>,<span class="sql__string">'CatMaster'</span>,<span class="sql__string">'Category&nbsp;Masters'</span>,<span class="sql__string">'CATDetails'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'CAT002'</span>,<span class="sql__string">'CATDetail'</span>,<span class="sql__string">'Category&nbsp;Details'</span>,<span class="sql__string">'CATDetails'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'CAT003'</span>,<span class="sql__string">'CATManage'</span>,<span class="sql__string">'Category&nbsp;Manage'</span>,<span class="sql__string">'CATManage'</span>,<span class="sql__string">'Masters'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Index'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'INV001'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Inventory&nbsp;Details'</span>,<span class="sql__string">'Index'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">23</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'FG001'</span>,<span class="sql__string">'FGIN'</span>,<span class="sql__string">'FG&nbsp;Receipt'</span>,<span class="sql__string">'FGIN'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">43</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">MenuDetails</span>(<span class="sql__id">Menu_ChildID</span>,<span class="sql__id">MenuName</span>,<span class="sql__id">MenuDisplayTxt</span>,<span class="sql__id">MenuFileName</span>,<span class="sql__id">MenuURL</span>,<span class="sql__id">UserID</span>,<span class="sql__id">CreatedDate</span>)&nbsp;&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">values</span>(<span class="sql__string">'FG002'</span>,<span class="sql__string">'FGOUT'</span>,<span class="sql__string">'FG&nbsp;Issue'</span>,<span class="sql__string">'FGOUT'</span>,<span class="sql__string">'Inventory'</span>,<span class="sql__string">'Shanu'</span>,<span class="sql__id">getdate</span>()-<span class="sql__number">13</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">select</span>&nbsp;*&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">MenuMaster</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">select</span>&nbsp;*&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">MenuDetails</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">select</span>&nbsp;&nbsp;&nbsp;<span class="sql__id">A</span>.<span class="sql__id">Menu_RootID</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">MDetail_ID</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">Menu_ChildID</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">MenuName</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">MenuDisplayTxt</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">MenuFileName</span>,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">MenuURL</span>&nbsp;,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">B</span>.<span class="sql__id">UserID</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">FROM</span>&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">MenuMaster</span>&nbsp;<span class="sql__id">A</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INNER</span>&nbsp;<span class="sql__keyword">JOIN</span>&nbsp;<span class="sql__id">MenuDetails</span>&nbsp;<span class="sql__id">B</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">ON</span>&nbsp;<span class="sql__id">A</span>.<span class="sql__id">Menu_ChildID</span>=<span class="sql__id">B</span>.<span class="sql__id">Menu_ChildID</span>&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="font-size:20px; font-weight:bold">Description</span></div>
</div>
<div><em>Now we have created our Tables lest start with creating WCF Rest service to access the data abd display in our Angular JS MVC page.
<ul style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px">Create WCF REST Service</strong>
</li></ul>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Open Visual Studio 2013 then select &quot;File&quot; -&gt; &quot;New&quot; -&gt; &quot;Project...&quot; then select WCF Service Application then select your project path and name your WCF service and click OK.</span></div>
</em></div>
<h1><span><img id="141038" src="141038-1.jpg" alt="" width="450" height="268"></span></h1>
<h1><span><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Once we have
 created the WCF Service we can see &ldquo;IService.CS&rdquo; and &ldquo;Service1.svc&rdquo; in the Solution Explorer as in the following.</span></span></h1>
<h1><span><img id="141039" src="141039-2.jpg" alt="" width="240" height="187"></span></h1>
<ul type="disc" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><strong style="outline:0px"><span style="outline:0px">IService.CS</span></strong><span style="outline:0px">: In &ldquo;</span><strong style="outline:0px"><span style="outline:0px">IService.CS</span></strong><span style="outline:0px">&rdquo;
 we can see 3 contracts by default.</span> </li><li style="outline:0px"><span style="outline:0px">[</span><strong style="outline:0px"><span style="outline:0px">ServiceContract</span></strong><span style="outline:0px">]: Describes the methods or any operations available for the service. The Service Contract
 is an interface and methods can be declared inside the Service Interface using the Operation Contract attribute.</span>
</li><li style="outline:0px"><span style="outline:0px">[</span><strong style="outline:0px"><span style="outline:0px">OperationContract</span></strong><span style="outline:0px">]: is similar to the web service [WEBMETHOD].</span>
</li><li style="outline:0px"><span style="outline:0px">[</span><strong style="outline:0px"><span style="outline:0px">DataContract</span></strong><span style="outline:0px">]: describes the data exchange between the client and the service.</span>
</li><li style="outline:0px"><span style="outline:0px">[</span><strong style="outline:0px"><span style="outline:0px">ServiceContract</span></strong><span style="outline:0px">]</span>
</li></ul>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">The following code will be automatically created for all the IService.CS files. We can change and write our own code here.</span></div>
<h1><span>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public interface IService1  
{  
  
    [OperationContract]  
    string GetData(int value);  
  
    [OperationContract]  
    CompositeType GetDataUsingDataContract(CompositeType composite);  
  
    // TODO: Add your service operations here  
}  
// Use a data contract as illustrated in the sample below to add composite types to service operations.  
[DataContract]  
public class CompositeType  
{  
    bool boolValue = true;  
    string stringValue = &quot;Hello &quot;;  
  
    [DataMember]  
    public bool BoolValue  
    {  
        get { return boolValue; }  
        set { boolValue = value; }  
    }  
  
    [DataMember]  
    public string StringValue  
    {  
        get { return stringValue; }  
        set { stringValue = value; }  
    }  
}  </pre>
<div class="preview">
<pre class="js">public&nbsp;interface&nbsp;IService1&nbsp;&nbsp;&nbsp;
<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;GetData(int&nbsp;value);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CompositeType&nbsp;GetDataUsingDataContract(CompositeType&nbsp;composite);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;TODO:&nbsp;Add&nbsp;your&nbsp;service&nbsp;operations&nbsp;here&nbsp;&nbsp;</span>&nbsp;
<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
<span class="js__sl_comment">//&nbsp;Use&nbsp;a&nbsp;data&nbsp;contract&nbsp;as&nbsp;illustrated&nbsp;in&nbsp;the&nbsp;sample&nbsp;below&nbsp;to&nbsp;add&nbsp;composite&nbsp;types&nbsp;to&nbsp;service&nbsp;operations.&nbsp;&nbsp;</span>&nbsp;
[DataContract]&nbsp;&nbsp;&nbsp;
public&nbsp;class&nbsp;CompositeType&nbsp;&nbsp;&nbsp;
<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;bool&nbsp;boolValue&nbsp;=&nbsp;true;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;stringValue&nbsp;=&nbsp;<span class="js__string">&quot;Hello&nbsp;&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;bool&nbsp;BoolValue&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;get&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__statement">return</span>&nbsp;boolValue;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;set&nbsp;<span class="js__brace">{</span>&nbsp;boolValue&nbsp;=&nbsp;value;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;string&nbsp;StringValue&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;get&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__statement">return</span>&nbsp;stringValue;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;set&nbsp;<span class="js__brace">{</span>&nbsp;stringValue&nbsp;=&nbsp;value;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span>&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span style="outline:0px">Data
 Contract<span>&nbsp;</span></span></strong><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">In
 our example we need to get all the Menu Details from the database, so I have created the Data Contracts</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span>&nbsp;</span>&ldquo;</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">MenuDataContract</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">&rdquo;.</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span>&nbsp;</span>Here
 we can see we have decelerated our entire table column name as Data Member.</span></div>
</span></h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class MenuDataContract  
    {  
        [DataContract]  
        public class MenuMasterDataContract  
        {  
            [DataMember]  
            public string Menu_ID { get; set; }  
  
            [DataMember]  
            public string Menu_RootID { get; set; }  
  
            [DataMember]  
            public string Menu_ChildID { get; set; }  
  
            [DataMember]  
            public string UserID { get; set; }  
  
        }  
  
        [DataContract]  
        public class MenuDetailDataContract  
        {  
            [DataMember]  
            public string MDetail_ID { get; set; }  
  
            [DataMember]  
            public string Menu_RootID { get; set; }  
  
            [DataMember]  
            public string Menu_ChildID { get; set; }  
  
            [DataMember]  
            public string MenuName { get; set; }  
  
            [DataMember]  
            public string MenuDisplayTxt { get; set; }  
  
            [DataMember]  
            public string MenuFileName { get; set; }  
  
            [DataMember]  
            public string MenuURL { get; set; }  
  
            [DataMember]  
            public string UserID { get; set; }  
        }  
  
    }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MenuDataContract&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataContract]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MenuMasterDataContract&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Menu_ID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Menu_RootID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Menu_ChildID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;UserID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataContract]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MenuDetailDataContract&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;MDetail_ID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Menu_RootID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Menu_ChildID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;MenuName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;MenuDisplayTxt&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;MenuFileName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;MenuURL&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;UserID&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span style="outline:0px">Service
 Contract:&nbsp;</span></strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">In
 the Operation Contract we can see &ldquo;</span><strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">WebInvoke</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">&rdquo;
 and &ldquo;</span><strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">WebGet</strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">&rdquo;
 for retrieving the data from the database in the REST Serivce.&nbsp;</span></div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">RequestFormat = WebMessageFormat.Json,  
ResponseFormat = WebMessageFormat.Json,  </pre>
<div class="preview">
<pre class="csharp">RequestFormat&nbsp;=&nbsp;WebMessageFormat.Json,&nbsp;&nbsp;&nbsp;
ResponseFormat&nbsp;=&nbsp;WebMessageFormat.Json,&nbsp;&nbsp;</pre>
</div>
</div>
</div>
</h1>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
Here we can see both of the request and response formats. Here I have used the JavaScript Object Notation (JSON) format.</div>
<ul type="disc" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<li style="outline:0px"><span style="outline:0px">JSON is a lightweight data interchange format.</span>
</li><li style="outline:0px"><span style="outline:0px">UriTemplate: Here we provide our Method Name.</span>
</li></ul>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Here I have declared the 3 methods &ldquo;</span><strong style="outline:0px"><span style="outline:0px">GetMenuDetails</span></strong><span style="outline:0px">&rdquo;. The &ldquo;</span><strong style="outline:0px"><span style="outline:0px">GetMenuDetails</span></strong><span style="outline:0px">&rdquo;
 method gets all the Menu Master and Details that will be used in our AngularJS to display the menu using a filter for each hierarchy.</span></div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract]  
    public interface IService1  
    {  
[OperationContract]  
        [WebInvoke(Method = &quot;GET&quot;,  
           RequestFormat = WebMessageFormat.Json,  
           ResponseFormat = WebMessageFormat.Json,  
           UriTemplate = &quot;/GetMenuDetails/&quot;)]  
        List&lt;MenuDataContract.MenuDetailDataContract&gt; GetMenuDetails();  
 }  </pre>
<div class="preview">
<pre class="csharp">[ServiceContract]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;IService1&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
[OperationContract]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[WebInvoke(Method&nbsp;=&nbsp;<span class="cs__string">&quot;GET&quot;</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RequestFormat&nbsp;=&nbsp;WebMessageFormat.Json,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ResponseFormat&nbsp;=&nbsp;WebMessageFormat.Json,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;/GetMenuDetails/&quot;</span>)]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;MenuDataContract.MenuDetailDataContract&gt;&nbsp;GetMenuDetails();&nbsp;&nbsp;&nbsp;
&nbsp;}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span lang="EN-US" style="outline:0px">Add
 Database usingADO.NET Entity Data Model</span></strong>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span lang="EN-US" style="outline:0px">Right-click your WCF project and select Add New Item then select ADO.NETEntity Data Model and click Add.</span></div>
&nbsp;<img id="141040" src="141040-3.jpg" alt="" width="516" height="249"></div>
<div>&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Select EF Designer
 from Database and click Next.</span></div>
<div>&nbsp;<img id="141041" src="141041-4.jpg" alt="" width="415" height="293"></div>
<div>&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Click New Connection.</span></div>
<div>&nbsp;<img id="141042" src="141042-5.jpg" alt="" width="454" height="267"></div>
<div>&nbsp;<span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Here we can select our Database Server
 Name and enter the DB server SQL Server Authentication User ID and Password. We have already created our database as &ldquo;</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">MenuDB</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">&rdquo;
 so we can select the database and click OK.</span></div>
<div>&nbsp;<img id="141043" src="141043-6.jpg" alt="" width="397" height="360"></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Click Next and select the tables to be used and click Finish.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<div style="outline:0px"><span style="outline:0px">Here we can see we have now created our ShanuMenuModel.</span></div>
</div>
<div><img id="141044" src="141044-7.jpg" alt="" width="302" height="224">&nbsp;</div>
<div>&nbsp;<strong>Service1.SVC</strong></div>
<div>&ldquo;Service.SVC.CS&rdquo; implements the IService Interface and overrides and defines all the methods of the Operation Contract.&nbsp;For example here we can see I have implemented the&nbsp;<strong>IService1</strong>&nbsp;in the&nbsp;<strong>Service1</strong>&nbsp;class.
 I have created the object for our Entity model and in&nbsp;GetMenuDetails&nbsp;using a LINQ join query I get both Menu Master and Detaildatas.&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class Service1 : IService1  
    {  
        ShanuMenuCreation_WCF.MenuDBEntities OME;  
  
        public Service1()  
        {  
            OME = new ShanuMenuCreation_WCF.MenuDBEntities();  
        }  
  
        public List&lt;MenuDataContract.MenuDetailDataContract&gt; GetMenuDetails()  
        {  
            ////var query = (from a in OME.MenuDetails  
            ////             select a).Distinct();  
             var query = (from A in OME.MenuMaster     
                         join B in OME.MenuDetails on A.Menu_ChildID equals B.Menu_ChildID   
                         select new     
                         {     
                            A.Menu_RootID,  
                            B.MDetail_ID,  
                            B.Menu_ChildID,  
                            B.MenuName,  
                            B.MenuDisplayTxt,  
                            B.MenuFileName,  
                            B.MenuURL ,  
                            B.UserID  
                         }).ToList().OrderBy(q =&gt; q.MDetail_ID);    
  
            List&lt;MenuDataContract.MenuDetailDataContract&gt; MenuList = new List&lt;MenuDataContract.MenuDetailDataContract&gt;();  
  
            query.ToList().ForEach(rec =&gt;  
            {  
                MenuList.Add(new MenuDataContract.MenuDetailDataContract  
                {  
                    MDetail_ID = Convert.ToString(rec.MDetail_ID),  
                    Menu_RootID = rec.Menu_RootID,  
                    Menu_ChildID = rec.Menu_ChildID,  
                    MenuName = rec.MenuName,  
                    MenuDisplayTxt = rec.MenuDisplayTxt,  
                    MenuFileName = rec.MenuFileName,  
                    MenuURL = rec.MenuURL,  
                    UserID = rec.UserID,  
                });  
            });  
            return MenuList;  
        }        
    }  </pre>
<div class="preview">
<pre class="js">public&nbsp;class&nbsp;Service1&nbsp;:&nbsp;IService1&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ShanuMenuCreation_WCF.MenuDBEntities&nbsp;OME;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;Service1()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OME&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ShanuMenuCreation_WCF.MenuDBEntities();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;List&lt;MenuDataContract.MenuDetailDataContract&gt;&nbsp;GetMenuDetails()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">////var&nbsp;query&nbsp;=&nbsp;(from&nbsp;a&nbsp;in&nbsp;OME.MenuDetails&nbsp;&nbsp;</span><span class="js__sl_comment">////&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;select&nbsp;a).Distinct();&nbsp;&nbsp;</span><span class="js__statement">var</span>&nbsp;query&nbsp;=&nbsp;(from&nbsp;A&nbsp;<span class="js__operator">in</span>&nbsp;OME.MenuMaster&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;join&nbsp;B&nbsp;<span class="js__operator">in</span>&nbsp;OME.MenuDetails&nbsp;on&nbsp;A.Menu_ChildID&nbsp;equals&nbsp;B.Menu_ChildID&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;select&nbsp;<span class="js__operator">new</span><span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;A.Menu_RootID,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.MDetail_ID,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.Menu_ChildID,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.MenuName,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.MenuDisplayTxt,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.MenuFileName,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.MenuURL&nbsp;,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B.UserID&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>).ToList().OrderBy(q&nbsp;=&gt;&nbsp;q.MDetail_ID);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;MenuDataContract.MenuDetailDataContract&gt;&nbsp;MenuList&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;List&lt;MenuDataContract.MenuDetailDataContract&gt;();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;query.ToList().ForEach(rec&nbsp;=&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MenuList.Add(<span class="js__operator">new</span>&nbsp;MenuDataContract.MenuDetailDataContract&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MDetail_ID&nbsp;=&nbsp;Convert.ToString(rec.MDetail_ID),&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Menu_RootID&nbsp;=&nbsp;rec.Menu_RootID,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Menu_ChildID&nbsp;=&nbsp;rec.Menu_ChildID,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MenuName&nbsp;=&nbsp;rec.MenuName,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MenuDisplayTxt&nbsp;=&nbsp;rec.MenuDisplayTxt,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MenuFileName&nbsp;=&nbsp;rec.MenuFileName,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MenuURL&nbsp;=&nbsp;rec.MenuURL,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UserID&nbsp;=&nbsp;rec.UserID,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;MenuList;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<div style="outline:0px"><strong style="outline:0px"><span style="outline:0px">Web.Config</span></strong></div>
</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
In the WCF project's &ldquo;Web.Config&rdquo; make the following changes.<br style="outline:0px">
Change</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;add binding=&quot;basicHttpsBinding&quot; scheme=&quot;https&quot; /&gt; to &lt;add binding=&quot;webHttpBinding&quot; scheme=&quot;http&quot; /&gt; </pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">binding</span>=<span class="xml__attr_value">&quot;basicHttpsBinding&quot;</span>&nbsp;<span class="xml__attr_name">scheme</span>=<span class="xml__attr_value">&quot;https&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;to&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">binding</span>=<span class="xml__attr_value">&quot;webHttpBinding&quot;</span>&nbsp;<span class="xml__attr_name">scheme</span>=<span class="xml__attr_value">&quot;http&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Replace
 the behaviour as in the following:</span>&nbsp;</div>
</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;/behaviors&gt;  
   &lt;endpointBehaviors&gt;  
        &lt;behavior&gt;  
          &lt;webHttp helpEnabled=&quot;True&quot;/&gt;  
        &lt;/behavior&gt;  
    &lt;/endpointBehaviors&gt;  
&lt;/behaviors&gt;  </pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_end">&lt;/behaviors&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;endpointBehaviors</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;behavior</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;webHttp</span>&nbsp;<span class="xml__attr_name">helpEnabled</span>=<span class="xml__attr_value">&quot;True&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/behavior&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/endpointBehaviors&gt;</span>&nbsp;&nbsp;&nbsp;
<span class="xml__tag_end">&lt;/behaviors&gt;</span>&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span style="outline:0px">Run
 WCF Service:<span>&nbsp;</span></span></strong><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Now
 that we have created our WCF Rest service, let's run and test our service. In our service URL we can add our method name and we can see the JSON result data from the database.</span></div>
</div>
<div>&nbsp;<img id="141045" src="141045-8.jpg" alt="" width="622" height="292"></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Create MVC Web Application</span></strong></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">We can add a new project to our existing project and create a new MVC web application as in the following.</span><span style="outline:0px">Right-click the project in the solution and click Add New Project then enter your project name
 and click &quot;OK&quot;.</span></div>
<div>&nbsp;<img id="141046" src="141046-9.jpg" alt="" width="555" height="292"></div>
<div>&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Select MVC
 and click &quot;OK&quot;.</span></div>
<div>&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">We have now
 created our MVC application and it's time to add our WCF Service and install the AngularJS package to our solution.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span style="outline:0px">Add
 WCF Service</span></strong><span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">:
 Right-click MVC Solution and click Add then click Service Reference.</span></div>
<div>&nbsp;<img id="141047" src="141047-11.jpg" alt="" width="476" height="324"></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Enter your WCF URL and click GO. Here my WCF URL is<span>&nbsp;</span></span>http://localhost:3514/Service1.svc/</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Add your name and click OK.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Now we have successfully added our WCF Service to our MVC application.
</span></div>
<div><img id="141048" src="141048-12.jpg" alt="" width="256" height="90">&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;
<p><strong>Procedure to Install AngularJS package</strong></p>
<div>Right-click your MVC project and click Manage NuGet Packages.</div>
</div>
<div>&nbsp;</div>
<div>&nbsp;<span style="font:14px/21px Roboto,sans-serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; widows:1; background-color:#ffffff">Select Online
 and Search for AngularJS. Select the AngularJS and click Install.</span><br style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
Now we have Installed the AngularJS package into our MVC Project. Now let's create our AngularJS.</div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<ul type="disc" style="outline:0px">
<li style="outline:0px"><span style="outline:0px">Modules.js</span> </li><li style="outline:0px"><span style="outline:0px">shoppingController.js</span> </li><li style="outline:0px"><span style="outline:0px">Services.js</span> </li></ul>
<p style="outline:0px"><strong style="outline:0px"><span style="outline:0px">Procedure to Create AngularJS Script Files</span></strong></p>
<p style="outline:0px"><span style="outline:0px">Right-click the Script folder and create your own folder to create the AngularJS Model/Controller and Service JavaScript. In your script folder add three JavaScript files and name them Modules.js, Controllers.js
 and Services.js as in the following.</span></p>
</div>
</div>
<h1>&nbsp;<img id="141049" src="141049-13.jpg" alt="" width="185" height="76"></h1>
<h1>&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span style="outline:0px">Modules.js:<span>&nbsp;</span></span></strong><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Here
 we add the reference to the Angular.js JavaScript. We provide the module name as</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">&ldquo;RESTClientModule</span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">&rdquo;<span>&nbsp;</span></span><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">.</span>&nbsp;</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">/// &lt;reference path=&quot;../angular.js&quot; /&gt;   
/// &lt;reference path=&quot;../angular.min.js&quot; /&gt;     
  
var app;  
  
(function () {  
    app = angular.module(&quot;RESTClientModule&quot;, []);  
})();  </pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="js__statement">var</span>&nbsp;app;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;app&nbsp;=&nbsp;angular.module(<span class="js__string">&quot;RESTClientModule&quot;</span>,&nbsp;[]);&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span>)();&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span lang="EN-US" style="outline:0px">Services.js:</span></strong><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span>&nbsp;</span></span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Here
 we provide a name for our service and we use this name in shanucontroller.js. Here for the Angular service I have given the name &quot;<strong style="outline:0px"><span style="outline:0px">AngularJs_WCFService</span></strong>&quot;. You can provide your own name but
 be careful of changing the name in Controllers.js. AngularJS can receive JSON data. Here we can see I have provided our WCS service URL to get the Item details as JSON data. To insert an Item information result to the database we pass the data as JSON data
 to our WCFinsert method as a parameter.</span>&nbsp;</div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">/// &lt;reference path=&quot;../angular.js&quot; /&gt;    
/// &lt;reference path=&quot;../angular.min.js&quot; /&gt;     
  
/// &lt;reference path=&quot;Modules.js&quot; /&gt;     
     
app.service(&quot;AngularJs_WCFService&quot;, function ($http) {  
    //Get Order Master Records    
    this.geMenuDetails = function () {  
        return $http.get(&quot;http://localhost:3514/Service1.svc/GetMenuDetails&quot;);  
    };       
  
});  </pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;Modules.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
app.service(<span class="js__string">&quot;AngularJs_WCFService&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;($http)&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//Get&nbsp;Order&nbsp;Master&nbsp;Records&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__operator">this</span>.geMenuDetails&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span><span class="js__statement">return</span>&nbsp;$http.get(<span class="js__string">&quot;http://localhost:3514/Service1.svc/GetMenuDetails&quot;</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span>);&nbsp;&nbsp;</pre>
</div>
</div>
</div>
</h1>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">AngularJS Controller:<span>&nbsp;</span></span></strong><span style="outline:0px">Here we add the reference to the Angular.js JavaScript, our Module.js and Services.js. The same as for services. For the
 controller I have given the name &quot;</span><span style="outline:0px">AngularJsShanu_WCFController</span><span style="outline:0px">&quot;. In the Controller I have done all the business logic and returned the data from the WCF JSON data to our MVC HTML page.<br style="outline:0px">
<strong style="outline:0px"><span style="outline:0px">Variable declarations</span></strong><br style="outline:0px">
First I declared all the local variables that need to be used and the current date and store the date using $scope.date.<br style="outline:0px">
<strong style="outline:0px"><span style="outline:0px">Note:<span>&nbsp;</span></span></strong></span><span style="outline:0px">$scope.subChildIDS =<span>&nbsp;</span></span><span style="outline:0px">&quot;ITM001&quot;</span><span style="outline:0px">;</span><span style="outline:0px"><span>&nbsp;</span>this
 variable has been used to filter the 2<sup style="outline:0px">nd</sup><span>&nbsp;</span>level hierarchy.</span>
<span style="outline:0px"><br style="outline:0px">
<strong style="outline:0px"><span style="outline:0px">Methods</span></strong><br style="outline:0px">
</span><strong style="outline:0px"><span style="outline:0px">getAllMenuDetails()</span></strong><strong style="outline:0px"><span style="outline:0px">:<span>&nbsp;</span></span></strong><span style="outline:0px">In this method I will get all the Menu Detail
 as JSON and bind the result to the Main page.</span></div>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
&nbsp;<span style="outline:0px">$scope.showsubMenu =<span>&nbsp;</span></span><span style="outline:0px">function</span><span style="outline:0px"><span>&nbsp;</span>(showMenus,ids) {</span><span style="outline:0px">}</span>
<div style="outline:0px"><span style="outline:0px">In this method on mouse hover I will filter the 2<sup style="outline:0px">nd</sup><span>&nbsp;</span>level hierarchy menu details and add the menu items to the list.</span></div>
</div>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">shanuController.js full source code</span></strong></p>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">/// &lt;reference path=&quot;../angular.js&quot; /&gt;    
/// &lt;reference path=&quot;../angular.min.js&quot; /&gt;     
/// &lt;reference path=&quot;Modules.js&quot; /&gt;     
/// &lt;reference path=&quot;Services.js&quot; /&gt;     
    
app.controller(&quot;AngularJsShanu_WCFController&quot;, function ($scope, $window, AngularJs_WCFService) {  
    $scope.date = new Date();  
      $scope.showDetails = false;  
    $scope.showSubDetails = false;  
    $scope.subChildIDS = &quot;ITM001&quot;;  
    $scope.Imagename = &quot;R1.png&quot;;  
      getAllMenuDetails();  
    //To Get All Records     
    function getAllMenuDetails() {  
          var promiseGet = AngularJs_WCFService.geMenuDetails();  
        promiseGet.then(function (pl) {  
            $scope.MenuDetailsDisp = pl.data  
        },  
             function (errorPl) {  
               });  
    }      
    $scope.showMenu = function (showMenus) {  
               if (showMenus == 1) {  
            $scope.Imagename = &quot;R2.png&quot;  
            $scope.showDetails = true;  
          }  
        else {  
            $scope.Imagename = &quot;R1.png&quot;  
            $scope.showDetails = false;        }  
    }    
    $scope.showsubMenu = function (showMenus,ids) {        
        if (showMenus == 1) {  
            $scope.subChildIDS = ids; 
       $scope.showSubDetails = true;          
        } 
        else if(showMenus == 0) {  
            $scope.showSubDetails = false;           
        }       
        else {             
            $scope.showSubDetails = true;            
        }       
    }    
});  </pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;../angular.min.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;Modules.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__sl_comment">///&nbsp;&lt;reference&nbsp;path=&quot;Services.js&quot;&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
app.controller(<span class="js__string">&quot;AngularJsShanu_WCFController&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;($scope,&nbsp;$window,&nbsp;AngularJs_WCFService)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.date&nbsp;=&nbsp;<span class="js__operator">new</span><span class="js__object">Date</span>();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showDetails&nbsp;=&nbsp;false;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showSubDetails&nbsp;=&nbsp;false;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.subChildIDS&nbsp;=&nbsp;<span class="js__string">&quot;ITM001&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.Imagename&nbsp;=&nbsp;<span class="js__string">&quot;R1.png&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;getAllMenuDetails();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//To&nbsp;Get&nbsp;All&nbsp;Records&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span class="js__operator">function</span>&nbsp;getAllMenuDetails()&nbsp;<span class="js__brace">{</span><span class="js__statement">var</span>&nbsp;promiseGet&nbsp;=&nbsp;AngularJs_WCFService.geMenuDetails();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;promiseGet.then(<span class="js__operator">function</span>&nbsp;(pl)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.MenuDetailsDisp&nbsp;=&nbsp;pl.data&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;(errorPl)&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showMenu&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;(showMenus)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;(showMenus&nbsp;==&nbsp;<span class="js__num">1</span>)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Imagename&nbsp;=&nbsp;<span class="js__string">&quot;R2.png&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showDetails&nbsp;=&nbsp;true;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.Imagename&nbsp;=&nbsp;<span class="js__string">&quot;R1.png&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showDetails&nbsp;=&nbsp;false;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$scope.showsubMenu&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;(showMenus,ids)&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;(showMenus&nbsp;==&nbsp;<span class="js__num">1</span>)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.subChildIDS&nbsp;=&nbsp;ids;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showSubDetails&nbsp;=&nbsp;true;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__statement">if</span>(showMenus&nbsp;==&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showSubDetails&nbsp;=&nbsp;false;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">else</span><span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.showSubDetails&nbsp;=&nbsp;true;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__brace">}</span>);&nbsp;&nbsp;</pre>
</div>
</div>
</div>
</h1>
<p>So now we have created our AngularJS Module/Controller and Service. So what is next?</p>
<div>So now we have created our AngularJS Module, Controller and Service. So what is next?<br>
<br>
<strong>Create MVC Controland View</strong>&nbsp;to display our result.<br>
<br>
<strong>Add Controller</strong><br>
<br>
Right-click Controllers then select Add Controller then select MVC 5 Controller&ndash;Empty then click Add.</div>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Change the Controller name and here I have given it the name &ldquo;</span><span style="outline:0px">MastersController</span><span style="outline:0px">&rdquo; and click OK.<br style="outline:0px">
<br style="outline:0px">
<strong style="outline:0px"><span style="outline:0px">Add View</span></strong></span></p>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">Right-click on the Controller Index and click Add View.</span></p>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">MVC Controller CS File:<span>&nbsp;</span></span></strong><span style="outline:0px">Here we can in my MVC Controller, I have created 3 menus with sub items. For example I have created the 2 controllers MastersController.cs
 and InventoryController.cs.</span>&nbsp;</p>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class MastersController : Controller  
    {  
        // GET: Masters  
        public ActionResult Index()  
        { return View(); }    
  
        public ActionResult ItemDetails()  
        { return View(); }  
  
        public ActionResult ItemManage()  
        { return View(); }  
  
        public ActionResult CATDetails()  
        { return View(); }  
  
        public ActionResult CATManage()  
        { return View(); }  
    }  </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MastersController&nbsp;:&nbsp;Controller&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;GET:&nbsp;Masters&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Index()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;ItemDetails()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;ItemManage()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;CATDetails()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;CATManage()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">Add
 one more controller as<span>&nbsp;</span></span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">InventoryController.</span>&nbsp;</div>
</h1>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class InventoryController : Controller  
    {  
        // GET: Inventory  
        public ActionResult Index()  
        {  
            return View();  
        }  
  
        public ActionResult FGIN()  
        {   
            return View();  
        }  
        public ActionResult FGOUT()  
        {  
            return View();  
        }  
    }  </pre>
<div class="preview">
<pre class="js">public&nbsp;class&nbsp;InventoryController&nbsp;:&nbsp;Controller&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;GET:&nbsp;Inventory&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;ActionResult&nbsp;Index()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__statement">return</span>&nbsp;View();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;ActionResult&nbsp;FGIN()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__statement">return</span>&nbsp;View();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;ActionResult&nbsp;FGOUT()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__statement">return</span>&nbsp;View();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
</h1>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Index View</span></strong><strong style="outline:0px"><span style="outline:0px"><br style="outline:0px">
<br style="outline:0px">
</span></strong><span style="outline:0px">Name the View &ldquo;Index&rdquo;.<br style="outline:0px">
<br style="outline:0px">
In the View, design your page and reference angular.js, Modules.js, Services.js and Controllers.js.<br style="outline:0px">
<br style="outline:0px">
In AngularJS we use {{ }} to bind or display the data.<br style="outline:0px">
<br style="outline:0px">
Here we can see that I first created a table and something for that table.<br style="outline:0px">
<br style="outline:0px">
First in the table I used the data-ng-controller=&quot;AngularJsShanu_WCFController&quot; and here we can see data-ng-controller will be used to bind the data of the controller to the HTML table.<br style="outline:0px">
<br style="outline:0px">
Using &lt;</span><span style="outline:0px"><span>&nbsp;</span>li</span><span>&nbsp;</span><span style="outline:0px">data-ng-repeat</span><span style="outline:0px">=&quot;menus in MenuDetailsDisp | filter:{Menu_RootID:'Root'}&quot;</span><span style="outline:0px"><span>&nbsp;</span>&gt;
 we can get all the menu details and display the First Level hierarchy menu details as top menu.</span></p>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
Here we can see I have filtered the menu by the rootId &quot;Root&quot;. During our insert query I have already explained that for every top-level menu I will provide the Rootid as &lsquo;Root&rsquo;. First we add all the top-level menus and for each top-level menu if
 there is any 2<sup style="outline:0px">nd</sup><span>&nbsp;</span>level hierarchy then I will load the 2<sup style="outline:0px">nd</sup><span>&nbsp;</span>level menu details to filter by the 1<sup style="outline:0px">st</sup><span>&nbsp;</span>level RootID
 as in the following.&nbsp;</p>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;li data-ng-repeat=&quot;menus in MenuDetailsDisp | filter:{Menu_RootID:'Root'}&quot;&gt;  
                                        @{  
                                            var url = Url.Action(&quot;{{menus.MenuFileName}}&quot;, &quot;{{menus.MenuURL}}&quot;, new { id = &quot;{{id=menus.MenuURL}}&quot; });  
                                            url = HttpUtility.UrlDecode(url);  
                                        }  
                                        &lt;a data-ng-href=&quot;@url&quot;&gt;{{menus.MenuDisplayTxt}}&lt;/a&gt;  
  
                                        &lt;ul class=&quot;sub-menu&quot;&gt;  
                                            &lt;li data-ng-repeat=&quot;submenus in MenuDetailsDisp | filter:{Menu_RootID:menus.Menu_ChildID}&quot; ng-mouseover=&quot;showsubMenu(1,submenus.Menu_ChildID);&quot; ng-mouseout=&quot;showsubMenu(0,submenus.Menu_ChildID);&quot;&gt;  
                                                @{  
                                                    var url1 = Url.Action(&quot;{{submenus.MenuFileName}}&quot;, &quot;{{submenus.MenuURL}}&quot;, new { id = &quot;{{id=submenus.MenuURL}}&quot; });  
                                                    url1 = HttpUtility.UrlDecode(url1);  
                                                }  
                                                &lt;a data-ng-href=&quot;@url1&quot;&gt;{{submenus.MenuDisplayTxt}}&lt;/a&gt;  </pre>
<div class="preview">
<pre class="js">&lt;li&nbsp;data-ng-repeat=<span class="js__string">&quot;menus&nbsp;in&nbsp;MenuDetailsDisp&nbsp;|&nbsp;filter:{Menu_RootID:'Root'}&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;url&nbsp;=&nbsp;Url.Action(<span class="js__string">&quot;{{menus.MenuFileName}}&quot;</span>,&nbsp;<span class="js__string">&quot;{{menus.MenuURL}}&quot;</span>,&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__brace">{</span>&nbsp;id&nbsp;=&nbsp;<span class="js__string">&quot;{{id=menus.MenuURL}}&quot;</span>&nbsp;<span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url&nbsp;=&nbsp;HttpUtility.UrlDecode(url);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;a&nbsp;data-ng-href=<span class="js__string">&quot;@url&quot;</span>&gt;<span class="js__brace">{</span><span class="js__brace">{</span>menus.MenuDisplayTxt<span class="js__brace">}</span><span class="js__brace">}</span>&lt;/a&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;ul&nbsp;class=<span class="js__string">&quot;sub-menu&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;li&nbsp;data-ng-repeat=<span class="js__string">&quot;submenus&nbsp;in&nbsp;MenuDetailsDisp&nbsp;|&nbsp;filter:{Menu_RootID:menus.Menu_ChildID}&quot;</span>&nbsp;ng-mouseover=<span class="js__string">&quot;showsubMenu(1,submenus.Menu_ChildID);&quot;</span>&nbsp;ng-mouseout=<span class="js__string">&quot;showsubMenu(0,submenus.Menu_ChildID);&quot;</span>&gt;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;url1&nbsp;=&nbsp;Url.Action(<span class="js__string">&quot;{{submenus.MenuFileName}}&quot;</span>,&nbsp;<span class="js__string">&quot;{{submenus.MenuURL}}&quot;</span>,&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__brace">{</span>&nbsp;id&nbsp;=&nbsp;<span class="js__string">&quot;{{id=submenus.MenuURL}}&quot;</span>&nbsp;<span class="js__brace">}</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url1&nbsp;=&nbsp;HttpUtility.UrlDecode(url1);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;a&nbsp;data-ng-href=<span class="js__string">&quot;@url1&quot;</span>&gt;<span class="js__brace">{</span><span class="js__brace">{</span>submenus.MenuDisplayTxt<span class="js__brace">}</span><span class="js__brace">}</span>&lt;/a&gt;&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span lang="EN-US" style="outline:0px">Here
 we can see for the 2<sup style="outline:0px">nd</sup><span>&nbsp;</span>level hierarchyI have filtered by<span>&nbsp;</span></span></strong><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">data-ng-repeat</span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">=&quot;submenus
 in MenuDetailsDisp | filter :{Menu_RootID:menus.Menu_ChildID}&quot;</span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span>&nbsp;</span></span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">where<span>&nbsp;</span></span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">menus.Menu_ChildID</span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span>&nbsp;</span></span><span lang="EN-US" style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">is
 the top-level menu id.</span>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span lang="EN-US" style="outline:0px">Same for the 3<sup style="outline:0px">rd</sup>level hierarchy. I will provide the 2<sup style="outline:0px">nd</sup><span>&nbsp;</span>level hierarchy root id as in the following.</span>&nbsp;</div>
</div>
</h1>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;div style=&quot;overflow:visible;height:100px;&quot;&gt;  
                                &lt;ul class=&quot;menu&quot;&gt;  
                                    &lt;li data-ng-repeat=&quot;menus in MenuDetailsDisp | filter:{Menu_RootID:'Root'}&quot;&gt;  
                                        @{  
                                            var url = Url.Action(&quot;{{menus.MenuFileName}}&quot;, &quot;{{menus.MenuURL}}&quot;, new { id = &quot;{{id=menus.MenuURL}}&quot; });  
                                            url = HttpUtility.UrlDecode(url);  
                                        }  
                                        &lt;a data-ng-href=&quot;@url&quot;&gt;{{menus.MenuDisplayTxt}}&lt;/a&gt;  
  
                                        &lt;ul class=&quot;sub-menu&quot;&gt;  
                                            &lt;li data-ng-repeat=&quot;submenus in MenuDetailsDisp | filter:{Menu_RootID:menus.Menu_ChildID}&quot; ng-mouseover=&quot;showsubMenu(1,submenus.Menu_ChildID);&quot; ng-mouseout=&quot;showsubMenu(0,submenus.Menu_ChildID);&quot;&gt;  
                                                @{  
                                                    var url1 = Url.Action(&quot;{{submenus.MenuFileName}}&quot;, &quot;{{submenus.MenuURL}}&quot;, new { id = &quot;{{id=submenus.MenuURL}}&quot; });  
                                                    url1 = HttpUtility.UrlDecode(url1);  
                                                }  
                                                &lt;a data-ng-href=&quot;@url1&quot;&gt;{{submenus.MenuDisplayTxt}}&lt;/a&gt;  
  
                                                &lt;ul ng-show=&quot;showSubDetails&quot; class=&quot;sub-menu2&quot;&gt;  
                                                    &lt;li data-ng-repeat=&quot;sub1menus in MenuDetailsDisp  | filter:{Menu_RootID:subChildIDS}&quot; ng-mouseover=&quot;showsubMenu(3,9);&quot;&gt;  
                                                        @{  
                                                            var url2 = Url.Action(&quot;{{sub1menus.MenuFileName}}&quot;, &quot;{{sub1menus.MenuURL}}&quot;, new { id = &quot;{{id=sub1menus.MenuURL}}&quot; });  
                                                            url2 = HttpUtility.UrlDecode(url2);  
                                                        }  
                                                        &lt;a data-ng-href=&quot;@url2&quot;&gt;{{sub1menus.MenuDisplayTxt}}&lt;/a&gt;  
                                                    &lt;/li&gt;  
                                                &lt;/ul&gt;  
  
                                            &lt;/li&gt;  
                                        &lt;/ul&gt;  
  
                                    &lt;/li&gt;  
                                &lt;/ul&gt;  
  
                            &lt;/div&gt;  </pre>
<div class="preview">
<pre class="html"><span class="html__tag_start">&lt;div</span><span class="html__attr_name">style</span>=<span class="html__attr_value">&quot;overflow:visible;height:100px;&quot;</span><span class="html__tag_start">&gt;&nbsp;</span><span class="html__tag_start">&lt;ul</span><span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;menu&quot;</span><span class="html__tag_start">&gt;&nbsp;</span><span class="html__tag_start">&lt;li</span><span class="html__attr_name">data-ng-repeat</span>=<span class="html__attr_value">&quot;menus&nbsp;in&nbsp;MenuDetailsDisp&nbsp;|&nbsp;filter:{Menu_RootID:'Root'}&quot;</span><span class="html__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;url&nbsp;=&nbsp;Url.Action(&quot;{{menus.MenuFileName}}&quot;,&nbsp;&quot;{{menus.MenuURL}}&quot;,&nbsp;new&nbsp;{&nbsp;id&nbsp;=&nbsp;&quot;{{id=menus.MenuURL}}&quot;&nbsp;});&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url&nbsp;=&nbsp;HttpUtility.UrlDecode(url);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;a</span><span class="html__attr_name">data-ng-href</span>=<span class="html__attr_value">&quot;@url&quot;</span><span class="html__tag_start">&gt;{</span>{menus.MenuDisplayTxt}}<span class="html__tag_end">&lt;/a&gt;</span><span class="html__tag_start">&lt;ul</span><span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;sub-menu&quot;</span><span class="html__tag_start">&gt;&nbsp;</span><span class="html__tag_start">&lt;li</span><span class="html__attr_name">data-ng-repeat</span>=<span class="html__attr_value">&quot;submenus&nbsp;in&nbsp;MenuDetailsDisp&nbsp;|&nbsp;filter:{Menu_RootID:menus.Menu_ChildID}&quot;</span><span class="html__attr_name">ng-mouseover</span>=<span class="html__attr_value">&quot;showsubMenu(1,submenus.Menu_ChildID);&quot;</span><span class="html__attr_name">ng-mouseout</span>=<span class="html__attr_value">&quot;showsubMenu(0,submenus.Menu_ChildID);&quot;</span><span class="html__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;url1&nbsp;=&nbsp;Url.Action(&quot;{{submenus.MenuFileName}}&quot;,&nbsp;&quot;{{submenus.MenuURL}}&quot;,&nbsp;new&nbsp;{&nbsp;id&nbsp;=&nbsp;&quot;{{id=submenus.MenuURL}}&quot;&nbsp;});&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url1&nbsp;=&nbsp;HttpUtility.UrlDecode(url1);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;a</span><span class="html__attr_name">data-ng-href</span>=<span class="html__attr_value">&quot;@url1&quot;</span><span class="html__tag_start">&gt;{</span>{submenus.MenuDisplayTxt}}<span class="html__tag_end">&lt;/a&gt;</span><span class="html__tag_start">&lt;ul</span><span class="html__attr_name">ng-show</span>=<span class="html__attr_value">&quot;showSubDetails&quot;</span><span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;sub-menu2&quot;</span><span class="html__tag_start">&gt;&nbsp;</span><span class="html__tag_start">&lt;li</span><span class="html__attr_name">data-ng-repeat</span>=<span class="html__attr_value">&quot;sub1menus&nbsp;in&nbsp;MenuDetailsDisp&nbsp;&nbsp;|&nbsp;filter:{Menu_RootID:subChildIDS}&quot;</span><span class="html__attr_name">ng-mouseover</span>=<span class="html__attr_value">&quot;showsubMenu(3,9);&quot;</span><span class="html__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;url2&nbsp;=&nbsp;Url.Action(&quot;{{sub1menus.MenuFileName}}&quot;,&nbsp;&quot;{{sub1menus.MenuURL}}&quot;,&nbsp;new&nbsp;{&nbsp;id&nbsp;=&nbsp;&quot;{{id=sub1menus.MenuURL}}&quot;&nbsp;});&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url2&nbsp;=&nbsp;HttpUtility.UrlDecode(url2);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;a</span><span class="html__attr_name">data-ng-href</span>=<span class="html__attr_value">&quot;@url2&quot;</span><span class="html__tag_start">&gt;{</span>{sub1menus.MenuDisplayTxt}}<span class="html__tag_end">&lt;/a&gt;</span><span class="html__tag_end">&lt;/li&gt;</span><span class="html__tag_end">&lt;/ul&gt;</span><span class="html__tag_end">&lt;/li&gt;</span><span class="html__tag_end">&lt;/ul&gt;</span><span class="html__tag_end">&lt;/li&gt;</span><span class="html__tag_end">&lt;/ul&gt;</span><span class="html__tag_end">&lt;/div&gt;</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<div style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<p style="outline:0px"><strong style="outline:0px"><span style="outline:0px">Run the Program</span></strong></p>
<p style="outline:0px"><strong style="outline:0px"><span style="outline:0px">(Output Menu Type 1)</span></strong></p>
</div>
<p><img id="141050" src="141050-m1.jpg" alt="" width="607" height="241"></p>
<p>&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span lang="EN-US" style="outline:0px">Output
 Menu Type 2)</span></strong></p>
<p>&nbsp;<img id="141051" src="141051-m2.jpg" alt="" width="614" height="234"></p>
<p>&nbsp;<strong style="outline:0px; color:#333333; text-transform:none; line-height:21px; text-indent:0px; letter-spacing:normal; font-family:Roboto,sans-serif; font-size:14px; font-style:normal; font-variant:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff"><span style="outline:0px">Note:<span>&nbsp;</span></span></strong><span style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">This
 code can be used in our MVC View page or this code can be used in Shared&gt;Layout.Cshtml so that the menu will be a common menu that can be used for all pages globally. Here we can see in the preceding that the same menu code has been added to the Layout.cshtm
 page. By this the menu will be added globally and can be accessed in all pages.</span></p>
<p>&nbsp;<img id="141052" src="141052-m5.jpg" alt="" width="606" height="153"></p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li>SHANUAngularMenuCreation.zip </li></ul>
<h1>More Information</h1>
<div><em>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<span style="outline:0px">You can extend this application depending on your requirements and add more functionality, like user management, menu management and and so on.</span></p>
<p style="font:14px/21px Roboto,sans-serif; outline:0px; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; white-space:normal; widows:1; background-color:#ffffff">
<strong style="outline:0px"><span style="outline:0px">Supported Browsers: Chrome and Firefox.</span></strong>&nbsp;</p>
</em></div>
