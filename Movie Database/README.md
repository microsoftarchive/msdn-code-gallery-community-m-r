# Movie Database
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- SQL Server 2008 R2 Express Edition 64 bit
## Topics
- Windows Forms
## Updated
- 12/31/2013
## Description

<h1>Introduction</h1>
<p>It solves the problem of finding a movie information easily from movie database.</p>
<h1><span>Building the Sample</span></h1>
<p>.NET Framework 4.5 is used for this project.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>Movies are released every year. So if we want to know which movie is released in which year and other information this movie database can help to find details about the selected movie.</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Form1.cs - This&nbsp; is the login form. There are two types of user - 1) admin, 2)normal. If some one has no account then he/she can create an account. Login form verify the username, password and usertype with stored username and password in sql database
 table tbl_login. <br>
</em></li><li><em>Form7.cs - This form is for creating an account. This form stores the new username, password and usertype to the database table tbl_login.</em>
</li><li><em>Form3.cs - This form is for normal user type. Normal user can search a movie by it's name and year or by only using year from a combobox. A data grid view shows the movie details. If user click a row in the data grid view then he/she can see details
 in the textboxes. There is a picture box to show movie related image if any. Exit button is used to exit form the system.</em>
</li><li><em>Form2.cs - An admin user can see this page. Admin user can insert, update or delete a movie from the database. There are four buttons - 1)insert, 2)update, 3)delete, 4)close. Close button is to exit from the system.
<br>
</em></li><li>Form6.cs - This form is for inserting movie information to the database table tbl_movie. Load button is used to load an image for this movie in the picture box. Insert button insert the movie information. Back button takes the user from insert form to Form2.cs.
</li><li>Form4.cs - This form is about updating movie information in the tbl_movie in the sql database. When the form is loaded all the movie information is loaded in the datagridview. When user select a row all the information is showed in the textbox. So user
 can easily update information also can upload a new movie image in the picture box using modify button. Update button is used for updating information.&nbsp; Back button takes the user from insert form to Form2.cs.
</li><li><em><em>Form5.cs - Admin user can delete a movie using this form. When the form is loaded all the movie information is loaded in the datagridview. When user clicks a row movie name and year is showed in the textbox. So user can delete the movie by pressing
 delete button.</em></em> </li><li><em>DataAccess.cs - This is a class file used for inserting, retrieving data from database using sql database stored procedures.</em>
</li></ul>
<h1>More Information</h1>
<p><em>CREATE TABLE <em>tbl_movie</em><br>
(<br>
<em>name varchar</em>(100) NOT NULL,<br>
<em>year int </em><em>NOT NULL</em>,<br>
<em>actor varchar</em>(50<em>&nbsp;</em>) NULL,</em></p>
<p><em><em><em>actress varchar</em>(50<em>&nbsp;</em>) NULL,</em><br>
<em><em>category varchar</em>(50<em>&nbsp;</em>) NULL,</em></em></p>
<p><em><em><em>quality varchar</em>(50<em>&nbsp;</em>) NULL,</em></em></p>
<p><em><em>&nbsp;</em><em><em>sound varchar</em>(30<em>&nbsp;</em>) NULL,</em></em></p>
<p><em><em>&nbsp;</em><em><em>language varchar</em>(30<em>&nbsp;</em>) NULL,</em></em></p>
<p><em><em>&nbsp;</em><em><em>myopinion varchar</em>(300<em></em>) NULL,</em></em></p>
<p><em><em></em><em><em>director varchar</em>(50<em></em>) NULL,</em></em></p>
<p><em><em><em>image image</em> NULL,</em></em></p>
<p><em><em></em>link varchar(150) NULL</em></p>
<p><em>);</em></p>
<p><em>CREATE TABLE <em>tbl_login</em><br>
(<br>
<em>name varchar</em>(20) NOT NULL,<br>
<em>password <em><em>varchar</em>(20)</em> </em><em>NOT NULL</em>,<br>
<em>user_type&nbsp;</em><em><em>varchar</em>(20)</em> NOT NULL</em></p>
<p><em>);</em></p>
<p>&nbsp;</p>
<p><em>Stored Procedures:<br>
1)</em></p>
<p><em>CREATE PROCEDURE addEditImage</em></p>
<p><em>(</em></p>
<p><em><em>@name varchar</em>(100) ,<br>
<em>@year int </em><em></em>,<br>
<em>@actor varchar</em>(50<em></em>) ,</em></p>
<p><em><em><em>@actress varchar</em>(50<em></em>) ,</em><br>
<em><em>@category varchar</em>(50<em></em>) ,</em></em></p>
<p><em><em><em>@quality varchar</em>(50<em></em>) ,</em></em></p>
<p><em><em></em><em><em>@sound varchar</em>(30<em></em>) ,</em></em></p>
<p><em><em></em><em><em>@language varchar</em>(30<em></em>) ,</em></em></p>
<p><em><em></em><em><em>@myopinion varchar</em>(300<em></em>) ,</em></em></p>
<p><em><em></em><em><em>@director varchar</em>(50<em></em>) ,</em></em></p>
<p><em><em><em>@image image</em> ,</em></em></p>
<p><em><em></em>@link varchar(150) <br>
</em></p>
<p><em>)AS</em></p>
<p><em>insert into tbl_movie(name, year, actor, actress, category, quality, sound, language, myopinion, director, image, link)<br>
values (@name, @year, @actor, @actress, @category, @quality, @sound, @language, @myopinion, @director, @image, @link)</em></p>
<p>&nbsp;</p>
<p><em>2)</em></p>
<p>&nbsp;</p>
<p><em>CREATE PROCEDURE getAllImages</em></p>
<p><em>AS</em></p>
<p><em>select * from tbl_movie</em></p>
<p>&nbsp;</p>
<p><em>3)</em></p>
<p><em>CREATE PROCEDURE getImage</em></p>
<p><em>(<br>
&nbsp;&nbsp;&nbsp; @name varchar(100),<br>
&nbsp;&nbsp;&nbsp; @year int<br>
)<br>
AS<br>
select * from tbl_movie where name = @name and year = @year</em></p>
<p>&nbsp;</p>
<p><em>4)</em></p>
<p><em>CREATE PROCEDURE getImage1</em></p>
<p><em>(<br>
&nbsp;&nbsp;&nbsp; @year int<br>
)<br>
AS<br>
select * from tbl_movie where year = @year</em></p>
<p>&nbsp;</p>
<p><em>5)</em></p>
<p><em><em>CREATE PROCEDURE updateTableMovie</em></em></p>
<p><em><em>(<br>
&nbsp;&nbsp;&nbsp; @name varchar(100),<br>
&nbsp;&nbsp;&nbsp; @year int,<br>
&nbsp;&nbsp;&nbsp; @actor varchar(50),<br>
&nbsp;&nbsp;&nbsp; @actress varchar(50),<br>
&nbsp;&nbsp;&nbsp; @category varchar(50),<br>
&nbsp;&nbsp;&nbsp; @quality varchar(50),<br>
&nbsp;&nbsp;&nbsp; @sound varchar(30),<br>
&nbsp;&nbsp;&nbsp; @language varchar(30),<br>
&nbsp;&nbsp;&nbsp; @myopinion varchar(300),<br>
&nbsp;&nbsp;&nbsp; @director varchar(50),<br>
&nbsp;&nbsp;&nbsp; @image Image,<br>
&nbsp;&nbsp;&nbsp; @updateName varchar(100),<br>
&nbsp;&nbsp;&nbsp; @updateYear int,<br>
&nbsp;&nbsp;&nbsp; @link varchar(150)<br>
)<br>
AS<br>
UPDATE tbl_movie SET name=@name, year=@year, actor=@actor, actress=@actress, category=@category, quality=@quality, sound=@sound, language=@language, myopinion=@myopinion, director=@director, image=@image, link=@link where name = @updateName and year=@updateYear<br>
</em></em></p>
<p>&nbsp;</p>
<p><em>***These are the stored procedure created in sql database******** <br>
</em></p>
<p><em><br>
</em></p>
<p><em><br>
</em></p>
