# Model binding  and Validating in Asp.Net MVC using simple Login Application.
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- SQL Server 2012
- HTML5/JavaScript
- ASP.NET MVC 5
- Entity Framework 6
## Topics
- Model Binding
- Demo login App using Model validation
- Depth understandng Data anottaion property
- Understanding Model in MVC
- Login web App in MVC with Database first approcah
## Updated
- 03/19/2017
## Description

<h1>Introduction</h1>
<p class="paragraph">In this article I am going to explain you about&nbsp; model validation using data annotataion property with model binding from scratch (here I am not going to be use any scaffolding to bind the model).</p>
<p class="paragraph">&nbsp;</p>
<p class="paragraph">What we are going to learn;&nbsp;</p>
<ul>
<li>Understanding Model validation in MVC&nbsp; </li><li>Different data anotation proeprty in MVC&nbsp; </li><li>Understanding of Entity framework&nbsp;database first approach. </li><li>Understanding model and binding to view.&nbsp; </li><li>Understading each data annotation property.&nbsp; </li><li>Data annotation rendering inside page.&nbsp; </li></ul>
<h1><span>Building the Sample</span></h1>
<p><em>Are there special requirements or instructions for building the sample?</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p class="paragraph">Model validation in MVC :-&nbsp;</p>
<p class="paragraph">Before proceding to talk about model validation; let me first tell you what is a model ?</p>
<p class="paragraph">In simple term model is a class which provide input to the view.</p>
<p class="paragraph">When we say about model validation it directly indicating that we are going to validate all the data member of that particular model/class.</p>
<p class="paragraph">In asp.net MVC if we can achive this by using data-annotation attributes.</p>
<p class="paragraph">Now we need to understand what is data-annotation attributes ?</p>
<p class="paragraph">Data-annotation attributes are the predefined class through which we can validate model class. These data-annotation classes are present under System.ComponentModel.DataAnnotations namespace.</p>
<p class="paragraph">The purpose of validating model is; as I said above that model provide input to the view so before doing any action using model our first goal is to check whether the data is correct or not ?</p>
<p class="paragraph">Inside model&nbsp; we defind different properties which can bind throough view. Before to bind the property to view we first need to check wheter the data is correct or not ?</p>
<p class="paragraph">This can be done by two ways;</p>
<p class="paragraph">&egrave; You can check all the input data manually using java script.</p>
<p class="paragraph">&egrave; You can use model binding.</p>
<p class="paragraph">In our discussion I will going to tell you about second approach;</p>
<p class="paragraph">Let&rsquo;s understand this with demo;</p>
<p class="paragraph">In this demo will going to create a simple login in app using data base first approach.</p>
<p><em>. &nbsp;&nbsp;</em></p>
<p class="paragraph">Database Part:-</p>
<p>Run the bellow sql query :-</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">--******************************************--

--Author              : Suryakant
--Created Date        : 4-03-2017
--Description         : Creating Database

--******************************************--

--Create Datbase

Create Database DemoWebAppDB

Use DemoWebAppDB

--Create Table

CREATE TABLE [dbo].[tbl_RegisterUser] ( 
    [iId]          INT           IDENTITY (1, 1) NOT NULL, 
    [sUserName]    VARCHAR (50)  NULL, 
    [sEmail]       VARCHAR (100) NULL, 
    [sPassword]    VARCHAR (20)  NULL, 
    [sPhoneNumber] VARCHAR (10)  NULL, 
    PRIMARY KEY CLUSTERED ([iId] ASC), 
    CONSTRAINT [CK_PhoneNumber] CHECK ([sPhoneNumber] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') 
); 

--Create Store Procedure to Insert record in to table.

create procedure sp_InsertUser 
( 
  @UserName varchar(50), 
  @UserEmail varchar(100), 
  @sPassword varchar(20), 
  @PhoneNumber varchar(10) 
) 
As 
Begin 
insert into tbl_RegisterUser (sUserName,sEmail,sPassword,sPhoneNumber) values (@UserName,@UserEmail,@sPassword,@PhoneNumber) 
end

--Create Store Proceduere to check the User Criedential

create procedure sp_CheckLoginCredential 
@UserName varchar(50),@Email varchar(30),@password varchar(40) 
as 
if((select count(*) from tbl_RegisterUser  where sUserName=@UserName or sEmail=@Email)&gt;0) 
begin  
select count(*) from tbl_RegisterUser where sPassword=@password 
end 
</pre>
<div class="preview">
<pre class="mysql">--******************************************<span class="sql__com">--&nbsp;
&nbsp;
--Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;Suryakant</span>&nbsp;
--<span class="sql__id">Created</span>&nbsp;<span class="sql__keyword">Date</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="sql__number">4</span><span class="sql__number">-03</span><span class="sql__number">-2017</span>&nbsp;
--<span class="sql__id">Description</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="sql__id">Creating</span>&nbsp;<span class="sql__keyword">Database</span>&nbsp;
&nbsp;
--******************************************<span class="sql__com">--&nbsp;
&nbsp;
--Create&nbsp;Datbase</span>&nbsp;
&nbsp;
<span class="sql__keyword">Create</span>&nbsp;<span class="sql__keyword">Database</span>&nbsp;<span class="sql__id">DemoWebAppDB</span>&nbsp;
&nbsp;
<span class="sql__keyword">Use</span>&nbsp;<span class="sql__id">DemoWebAppDB</span>&nbsp;
&nbsp;
--<span class="sql__keyword">Create</span>&nbsp;<span class="sql__keyword">Table</span>&nbsp;
&nbsp;
<span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">tbl_RegisterUser</span>]&nbsp;(&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">iId</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">INT</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__id">IDENTITY</span>&nbsp;(<span class="sql__number">1</span>,&nbsp;<span class="sql__number">1</span>)&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">sUserName</span>]&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>&nbsp;(<span class="sql__number">50</span>)&nbsp;&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">sEmail</span>]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>&nbsp;(<span class="sql__number">100</span>)&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">sPassword</span>]&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">VARCHAR</span>&nbsp;(<span class="sql__number">20</span>)&nbsp;&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">sPhoneNumber</span>]&nbsp;<span class="sql__keyword">VARCHAR</span>&nbsp;(<span class="sql__number">10</span>)&nbsp;&nbsp;<span class="sql__value">NULL</span>,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">PRIMARY</span>&nbsp;<span class="sql__keyword">KEY</span>&nbsp;<span class="sql__id">CLUSTERED</span>&nbsp;([<span class="sql__id">iId</span>]&nbsp;<span class="sql__keyword">ASC</span>),&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="sql__keyword">CONSTRAINT</span>&nbsp;[<span class="sql__id">CK_PhoneNumber</span>]&nbsp;<span class="sql__keyword">CHECK</span>&nbsp;([<span class="sql__id">sPhoneNumber</span>]&nbsp;<span class="sql__keyword">like</span>&nbsp;<span class="sql__string">'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'</span>)&nbsp;&nbsp;
);&nbsp;&nbsp;
&nbsp;
--<span class="sql__keyword">Create</span>&nbsp;<span class="sql__id">Store</span>&nbsp;<span class="sql__keyword">Procedure</span>&nbsp;<span class="sql__keyword">to</span>&nbsp;<span class="sql__keyword">Insert</span>&nbsp;<span class="sql__id">record</span>&nbsp;<span class="sql__keyword">in</span>&nbsp;<span class="sql__keyword">to</span>&nbsp;<span class="sql__keyword">table</span>.&nbsp;
&nbsp;
<span class="sql__keyword">create</span>&nbsp;<span class="sql__keyword">procedure</span>&nbsp;<span class="sql__id">sp_InsertUser</span>&nbsp;&nbsp;
(&nbsp;&nbsp;
&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">UserName</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">50</span>),&nbsp;&nbsp;
&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">UserEmail</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">100</span>),&nbsp;&nbsp;
&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">sPassword</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">20</span>),&nbsp;&nbsp;
&nbsp;&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">PhoneNumber</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">10</span>)&nbsp;&nbsp;
)&nbsp;&nbsp;
<span class="sql__keyword">As</span>&nbsp;&nbsp;
<span class="sql__keyword">Begin</span>&nbsp;&nbsp;
<span class="sql__keyword">insert</span>&nbsp;<span class="sql__keyword">into</span>&nbsp;<span class="sql__id">tbl_RegisterUser</span>&nbsp;(<span class="sql__id">sUserName</span>,<span class="sql__id">sEmail</span>,<span class="sql__id">sPassword</span>,<span class="sql__id">sPhoneNumber</span>)&nbsp;<span class="sql__keyword">values</span>&nbsp;(<span class="sql__keyword">@</span><span class="sql__variable">UserName</span>,<span class="sql__keyword">@</span><span class="sql__variable">UserEmail</span>,<span class="sql__keyword">@</span><span class="sql__variable">sPassword</span>,<span class="sql__keyword">@</span><span class="sql__variable">PhoneNumber</span>)&nbsp;&nbsp;
<span class="sql__keyword">end</span>&nbsp;
&nbsp;
--<span class="sql__keyword">Create</span>&nbsp;<span class="sql__id">Store</span>&nbsp;<span class="sql__id">Proceduere</span>&nbsp;<span class="sql__keyword">to</span>&nbsp;<span class="sql__keyword">check</span>&nbsp;<span class="sql__id">the</span>&nbsp;<span class="sql__keyword">User</span>&nbsp;<span class="sql__id">Criedential</span>&nbsp;
&nbsp;
<span class="sql__keyword">create</span>&nbsp;<span class="sql__keyword">procedure</span>&nbsp;<span class="sql__id">sp_CheckLoginCredential</span>&nbsp;&nbsp;
<span class="sql__keyword">@</span><span class="sql__variable">UserName</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">50</span>),<span class="sql__keyword">@</span><span class="sql__variable">Email</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">30</span>),<span class="sql__keyword">@</span><span class="sql__variable">password</span>&nbsp;<span class="sql__keyword">varchar</span>(<span class="sql__number">40</span>)&nbsp;&nbsp;
<span class="sql__keyword">as</span>&nbsp;&nbsp;
<span class="sql__keyword">if</span>((<span class="sql__keyword">select</span>&nbsp;<span class="sql__function">count</span>(*)&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">tbl_RegisterUser</span>&nbsp;&nbsp;<span class="sql__keyword">where</span>&nbsp;<span class="sql__id">sUserName</span>=<span class="sql__keyword">@</span><span class="sql__variable">UserName</span>&nbsp;<span class="sql__keyword">or</span>&nbsp;<span class="sql__id">sEmail</span>=<span class="sql__keyword">@</span><span class="sql__variable">Email</span>)&gt;<span class="sql__number">0</span>)&nbsp;&nbsp;
<span class="sql__keyword">begin</span>&nbsp;&nbsp;&nbsp;
<span class="sql__keyword">select</span>&nbsp;<span class="sql__function">count</span>(*)&nbsp;<span class="sql__keyword">from</span>&nbsp;<span class="sql__id">tbl_RegisterUser</span>&nbsp;<span class="sql__keyword">where</span>&nbsp;<span class="sql__id">sPassword</span>=<span class="sql__keyword">@</span><span class="sql__variable">password</span>&nbsp;&nbsp;
<span class="sql__keyword">end</span>&nbsp;&nbsp;
</pre>
</div>
</div>
</div>
<p>Let&rsquo;s&nbsp; jump to visual studio and start creating new project :-</p>
<p>Step 1:- Click on new project from file menu.</p>
<p>Successfully we created a project.</p>
<p>Step-2 :-</p>
<p>In this step we are going to add ado.net entity; here we are using database first approach.</p>
<p>Before going to create entity model; let me first tell what is database first approach ?</p>
<p>Database first approach is a process through which Visual studio can generate model s by using database table.</p>
<p>Learn more detail&rsquo;s about this visit here (<a href="https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/setting-up-database">https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/setting-up-database</a>)</p>
<p>For code first approach more detail&rsquo;s visit here (</p>
<p>Remember here in entity frame work;</p>
<p>All table are treated as a Class.</p>
<p>All column of the table treated as properties.</p>
<p>Let&rsquo;s&nbsp; proceed to step-2 and add Ado.Net Entiy data model;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace DemoLoginApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; //Not Mapped
    using System.Web.Mvc;
    
    [Bind(Exclude=&quot;iId&quot;)]
    public partial class tbl_RegisterUser
    {
       [ScaffoldColumn(false)]
        [Key]
        public int iId { get; set; }

        [Required(ErrorMessage=&quot;Please enter User Name !!!&quot;)]
       [Display(Name=&quot;User Name :&quot;)]
        [StringLength(30,ErrorMessage=&quot;User name must be thirty charter long.&quot;)]
        public string sUserName { get; set; }

        [Required(ErrorMessage=&quot;Pleae Enter Email Adress !!!&quot;)]
        [EmailAddress(ErrorMessage=&quot;Please Enter valid Email Id !!!&quot;)]
        [Display(Name=&quot;Email Adress :&quot;)]
        [DataType(DataType.EmailAddress)]
        public string sEmail { get; set; }

        [Required(ErrorMessage=&quot;Please Enter Password !!!&quot;)]
        [Display(Name=&quot;Password :&quot;)]
        [StringLength(9,ErrorMessage=&quot;Password must be nine character long.&quot;)]
        public string sPassword { get; set; }

        [NotMapped]
        [Required(ErrorMessage = &quot;Please Enter Confirm Password !!!&quot;)]
        [Display(Name = &quot;Confirm Password :&quot;)]
        [System.ComponentModel.DataAnnotations.Compare(&quot;sPassword&quot;,ErrorMessage=&quot;Confirm Password must match with Password !!!&quot;)]
        public string sCnfPassword { get; set; }

        [Required(ErrorMessage=&quot;Please Enter Phone Number !!!&quot;)]
        [Display(Name=&quot;Phone Number :&quot;)]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10,ErrorMessage=&quot;Phone number must be 10 digit.&quot;,MinimumLength=10)]
        public string sPhoneNumber { get; set; }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;DemoLoginApp.Models&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.ComponentModel.DataAnnotations;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;System.ComponentModel.DataAnnotations.Schema;&nbsp;<span class="cs__com">//Not&nbsp;Mapped</span><span class="cs__keyword">using</span>&nbsp;System.Web.Mvc;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Bind(Exclude=<span class="cs__string">&quot;iId&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;tbl_RegisterUser&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[ScaffoldColumn(<span class="cs__keyword">false</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Key]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">int</span>&nbsp;iId&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage=<span class="cs__string">&quot;Please&nbsp;enter&nbsp;User&nbsp;Name&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Display(Name=<span class="cs__string">&quot;User&nbsp;Name&nbsp;:&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[StringLength(<span class="cs__number">30</span>,ErrorMessage=<span class="cs__string">&quot;User&nbsp;name&nbsp;must&nbsp;be&nbsp;thirty&nbsp;charter&nbsp;long.&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">string</span>&nbsp;sUserName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage=<span class="cs__string">&quot;Pleae&nbsp;Enter&nbsp;Email&nbsp;Adress&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[EmailAddress(ErrorMessage=<span class="cs__string">&quot;Please&nbsp;Enter&nbsp;valid&nbsp;Email&nbsp;Id&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Display(Name=<span class="cs__string">&quot;Email&nbsp;Adress&nbsp;:&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataType(DataType.EmailAddress)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">string</span>&nbsp;sEmail&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage=<span class="cs__string">&quot;Please&nbsp;Enter&nbsp;Password&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Display(Name=<span class="cs__string">&quot;Password&nbsp;:&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[StringLength(<span class="cs__number">9</span>,ErrorMessage=<span class="cs__string">&quot;Password&nbsp;must&nbsp;be&nbsp;nine&nbsp;character&nbsp;long.&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">string</span>&nbsp;sPassword&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[NotMapped]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;Please&nbsp;Enter&nbsp;Confirm&nbsp;Password&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Display(Name&nbsp;=&nbsp;<span class="cs__string">&quot;Confirm&nbsp;Password&nbsp;:&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[System.ComponentModel.DataAnnotations.Compare(<span class="cs__string">&quot;sPassword&quot;</span>,ErrorMessage=<span class="cs__string">&quot;Confirm&nbsp;Password&nbsp;must&nbsp;match&nbsp;with&nbsp;Password&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">string</span>&nbsp;sCnfPassword&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage=<span class="cs__string">&quot;Please&nbsp;Enter&nbsp;Phone&nbsp;Number&nbsp;!!!&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Display(Name=<span class="cs__string">&quot;Phone&nbsp;Number&nbsp;:&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataType(DataType.PhoneNumber)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[StringLength(<span class="cs__number">10</span>,ErrorMessage=<span class="cs__string">&quot;Phone&nbsp;number&nbsp;must&nbsp;be&nbsp;10&nbsp;digit.&quot;</span>,MinimumLength=<span class="cs__number">10</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">string</span>&nbsp;sPhoneNumber&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</ul>
<p>In the above class we saw that all the property has represented by data annotation attributes.</p>
<p>Let&rsquo;s talk about some important concept behind data annotation attributes;</p>
<p>In MVC all this data annotation attributes are present under &ldquo;System.ComponentModel.DataAnnotations&rdquo;.</p>
<p>Why to Use ?</p>
<p>The purpose of using Data annotation attribute is validating model in server &nbsp;through
<strong>Model State</strong> property of <strong>ModelStateDictionary Class</strong>.</p>
<p><strong>Model State :- T</strong>his property plays an important role while doing model validation using data annotation property. ModelStateDictionary class contain following important property and method which helps us validate the model .</p>
<p>&egrave; <strong>&nbsp;ModelState.Count :- </strong>It count all the key value pair present .Returns integer value.</p>
<p>&egrave; <strong>&nbsp;ModelState.IsValid:- </strong>This property tells whether the ModelState has error or not; if no error exist inside ModelState then it returns true other wise it returns false.</p>
<p>&egrave; <strong>ModelState.Clear() :- </strong>It help us to clear the state of model. It means it will clear all the error present inside the model.</p>
<p>&egrave; <strong>ModelState.Values:- </strong>This property holds all the errors which comes from data annotation attributes.ModelStae.Values is type of ModelState list. In side ModelState&nbsp; class it contain property called as Errors and Values.</p>
<p><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</strong>So by using ModelState.Values we can access all the errors present inside our model; we will see this later in our code.</p>
<p>Let&rsquo;s understand how model binding works;</p>
<p>In MVC we have a property called as ViewData; we all know about what is ViewData if you do n&rsquo;t know what is Viewdata read here.</p>
<p>We always directly passing the model to view and access the model by directly by using model property. That model property defind under ViewDataDictionary class. The ViewDataDictionary class has a property called as View data; so by&nbsp; using ViewData
 property we can access the model property.</p>
<p>We can also accesss other important property of model from ViewDataDictionary class; some of the important property of model are;</p>
<p>&egrave; Model State</p>
<p>&egrave; Model Meta Data</p>
<p>&egrave; SetModel(object value)&nbsp; //Method.</p>
<p>Let&rsquo;s crate our demo Login App;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace DemoLoginApp.Controllers
{
    public class LoginDemoController : Controller
    {

        SuryaTechDBEntities _dbEntites = new SuryaTechDBEntities();
        // GET: /LoginDemo/
        public ActionResult Index()
        
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Index([Bind(Exclude=&quot;iId&quot;)]tbl_RegisterUser model)
        {
            var type = Request.RequestType;
            if (ModelState.Values.ToArray()[0].Errors.Count==0 &amp;&amp; ModelState.Values.ToArray()[1].Errors.Count==0)
            {
                if (CheckUser(model.sUserName,model.sPassword))
                {
                    return RedirectToAction(&quot;Home&quot;, new { username=model.sUserName});
                }
                else
                {
                   
                    ModelState.Clear();
                    ModelState.AddModelError(&quot;&quot;, &quot;User Id or Password not exist; try again !!!&quot;);
                    return View(model);
                }
               
            }
            else
            {
                return View(model);
            }
            
        }

        private bool CheckUser(string UserId, string Password)
        {
            var User = _dbEntites.tbl_RegisterUser.FirstOrDefault(m =&gt; m.sUserName == UserId);
            if (User!=null)
            {
                if (User.sPassword == Password)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult Register(tbl_RegisterUser model)
         {
            List&lt;string&gt; errorMessage = new List&lt;string&gt;();
            if (ModelState.IsValid)
            {
                _dbEntites.tbl_RegisterUser.Add(model);
                _dbEntites.SaveChanges();
                return Json(&quot;&quot;, JsonRequestBehavior.AllowGet);
            }
            else
            {
                for (int i = 0; i &lt; ModelState.Values.Count; i&#43;&#43;)
                {
                    if (ModelState.Values.ToArray()[i].Errors.Count&gt;0)
                    {
                        errorMessage.Add(ModelState.Values.ToArray()[i].Errors[0].ErrorMessage);
                    }
                    else
                    {
                        errorMessage.Add(&quot;&quot;);
                    }
                }

                return Json(new {keys= ModelState.Keys,ErrorMessage=errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult Home(string username)
         {
            ViewData.Model = username;
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction(&quot;Index&quot;);
        }
	}
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;DemoLoginApp.Controllers&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">class</span>&nbsp;LoginDemoController&nbsp;:&nbsp;Controller&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SuryaTechDBEntities&nbsp;_dbEntites&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SuryaTechDBEntities();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;GET:&nbsp;/LoginDemo/</span><span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Index()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpPost]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Index([Bind(Exclude=<span class="cs__string">&quot;iId&quot;</span>)]tbl_RegisterUser&nbsp;model)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;type&nbsp;=&nbsp;Request.RequestType;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ModelState.Values.ToArray()[<span class="cs__number">0</span>].Errors.Count==<span class="cs__number">0</span>&nbsp;&amp;&amp;&nbsp;ModelState.Values.ToArray()[<span class="cs__number">1</span>].Errors.Count==<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(CheckUser(model.sUserName,model.sPassword))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;RedirectToAction(<span class="cs__string">&quot;Home&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;username=model.sUserName});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ModelState.Clear();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ModelState.AddModelError(<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__string">&quot;User&nbsp;Id&nbsp;or&nbsp;Password&nbsp;not&nbsp;exist;&nbsp;try&nbsp;again&nbsp;!!!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;View(model);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;View(model);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span><span class="cs__keyword">bool</span>&nbsp;CheckUser(<span class="cs__keyword">string</span>&nbsp;UserId,&nbsp;<span class="cs__keyword">string</span>&nbsp;Password)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;User&nbsp;=&nbsp;_dbEntites.tbl_RegisterUser.FirstOrDefault(m&nbsp;=&gt;&nbsp;m.sUserName&nbsp;==&nbsp;UserId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(User!=<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(User.sPassword&nbsp;==&nbsp;Password)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span><span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span><span class="cs__keyword">return</span><span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span><span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[HttpPost]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Register(tbl_RegisterUser&nbsp;model)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;&nbsp;errorMessage&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ModelState.IsValid)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dbEntites.tbl_RegisterUser.Add(model);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dbEntites.SaveChanges();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Json(<span class="cs__string">&quot;&quot;</span>,&nbsp;JsonRequestBehavior.AllowGet);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;ModelState.Values.Count;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ModelState.Values.ToArray()[i].Errors.Count&gt;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessage.Add(ModelState.Values.ToArray()[i].Errors[<span class="cs__number">0</span>].ErrorMessage);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessage.Add(<span class="cs__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Json(<span class="cs__keyword">new</span>&nbsp;{keys=&nbsp;ModelState.Keys,ErrorMessage=errorMessage&nbsp;},&nbsp;JsonRequestBehavior.AllowGet);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Home(<span class="cs__keyword">string</span>&nbsp;username)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ViewData.Model&nbsp;=&nbsp;username;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;View();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Logout()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;RedirectToAction(<span class="cs__string">&quot;Index&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p>Add the index view page by right click on index action method.</p>
<p>&nbsp;</p>
<p>After that add bellow code inside index View;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">@model DemoLoginApp.Models.tbl_RegisterUser
@{
    ViewBag.Title = &quot;Index&quot;;
    var model = Model;
}

&lt;h2&gt;Welcome To Login Page&lt;/h2&gt;

&lt;html&gt;
&lt;body&gt;
    &lt;div class=&quot;container&quot;&gt;
        &lt;div class=&quot;row&quot;&gt;
            &lt;div class=&quot;col-md-6 col-md-offset-3&quot;&gt;
                &lt;div class=&quot;panel panel-login&quot;&gt;
                    &lt;div class=&quot;panel-heading&quot;&gt;
                        &lt;div class=&quot;row&quot;&gt;
                            &lt;div class=&quot;col-xs-6&quot;&gt;
                                &lt;a href=&quot;#&quot; class=&quot;active&quot; id=&quot;login-form-link&quot;&gt;Login&lt;/a&gt;
                            &lt;/div&gt;
                            &lt;div class=&quot;col-xs-6&quot;&gt;
                                &lt;a href=&quot;#&quot; id=&quot;register-form-link&quot;&gt;Register&lt;/a&gt;
                            &lt;/div&gt;
                        &lt;/div&gt;
                        &lt;hr&gt;
                    &lt;/div&gt;
                    &lt;div class=&quot;panel-body&quot;&gt;
                        &lt;div class=&quot;row&quot;&gt;
                            &lt;div class=&quot;col-lg-12&quot;&gt;

                                @using (Html.BeginForm(&quot;Index&quot;, &quot;LoginDemo&quot;, FormMethod.Post, new { id = &quot;login-form&quot;, style = &quot;display:block&quot; }))
                                {
                                  
                                    @Html.AntiForgeryToken()
                                    &lt;div class=&quot;form-group&quot;&gt;
                                        @Html.TextBoxFor(m =&gt; m.sUserName, new { @class = &quot;form-control&quot;, id = &quot;UserName&quot;, placeholder = &quot;Enter User Name/EmailId&quot;, tabindex = &quot;1&quot; })
                                        @Html.ValidationMessageFor(m =&gt; m.sUserName)
                                    &lt;/div&gt;
                                    &lt;div class=&quot;form-group&quot;&gt;

                                        @Html.PasswordFor(m =&gt; m.sPassword, new { @class = &quot;form-control&quot;, id = &quot;password&quot;, placeholder = &quot;Enter Password&quot;, tabindex = &quot;2&quot; })
                                        @Html.ValidationMessageFor(m =&gt; m.sPassword)
                                    &lt;/div&gt;
                                    &lt;div class=&quot;form-group col-lg-offset-4&quot;&gt;
                                        &lt;input type=&quot;checkbox&quot; tabindex=&quot;3&quot; class=&quot;&quot; name=&quot;remember&quot; id=&quot;remember&quot;&gt;
                                        &lt;label for=&quot;remember&quot;&gt; Remember Me&lt;/label&gt;
                                    &lt;/div&gt;
                                    &lt;div class=&quot;form-group&quot;&gt;
                                        &lt;div class=&quot;row&quot;&gt;
                                            &lt;div class=&quot;col-sm-4 col-lg-offset-3&quot;&gt;
                                                &lt;input type=&quot;submit&quot; name=&quot;command&quot; id=&quot;login-submit&quot; tabindex=&quot;4&quot; class=&quot;form-control btn btn-default&quot; value=&quot;Log In&quot;&gt;
                                            &lt;/div&gt;
                                        &lt;/div&gt;
                                    &lt;/div&gt;
                                    &lt;div class=&quot;form-group&quot;&gt;
                                        &lt;div class=&quot;row&quot;&gt;
                                            &lt;div class=&quot;col-lg-12&quot; style=&quot;margin-left:30%&quot;&gt;
                                                    &lt;a href=&quot;#&quot; tabindex=&quot;5&quot; class=&quot;forgot-password&quot;&gt;Forgot Password?&lt;/a&gt;
                                            &lt;/div&gt;
                                        &lt;/div&gt;
                                    &lt;/div&gt;
                                    @Html.ValidationSummary(&quot;&quot;);
                                }
                                @using (Ajax .BeginForm(&quot;Register&quot;, &quot;LoginDemo&quot;, new AjaxOptions { OnSuccess = &quot;RegisterSuccess&quot;, HttpMethod = &quot;POST&quot;, UpdateTargetId = &quot;Message&quot; }, new { id = &quot;register-form&quot;, style = &quot;display:none&quot; }))
                                {
                                    @Html.AntiForgeryToken()
                                    @Html.ValidationSummary(true, &quot;&quot;, new { @class = &quot;text-danger&quot; })

                                    @Html.Partial(&quot;_RegisterPage&quot;)

                                }
                            &lt;/div&gt;
                        &lt;/div&gt;
                        &lt;label style=&quot;color:green&quot; id=&quot;Message&quot;&gt;&lt;/label&gt;

                    &lt;/div&gt;
                &lt;/div&gt;
            &lt;/div&gt;
        &lt;/div&gt;
    &lt;/div&gt;

&lt;/body&gt;
&lt;/html&gt;
&lt;script&gt;
  
    $(function () {
        debugger;
        $(&quot;#register-form-link&quot;).click(function () {
            debugger;
            $(&quot;#register-form&quot;).css('display', 'block');
            $(&quot;#login-form&quot;).css('display', 'none');
            $(&quot;#register-form-link&quot;).addClass('active');
            $(&quot;#login-form-link&quot;).removeClass('active');
            $(&quot;.field-validation-error&quot;).css('display', &quot;none&quot;);
            $(&quot;.input-validation-error&quot;).css(&quot;border&quot;, &quot;1px solid #cccccc&quot;);
        });
        $(&quot;#login-form-link&quot;).click(function () {
            $(&quot;#register-form&quot;).css('display', 'none');
            $(&quot;#login-form&quot;).css('display', 'block');
            $(&quot;#register-form-link&quot;).removeClass('active');
            $(&quot;#login-form-link&quot;).addClass('active');
            $(&quot;#Message&quot;).hide();
        });
    });
    function RegisterSuccess(data) {
        debugger;
        if (data!=&quot;&quot;) {
            for (var i = 0; i &lt; data.keys.length; i&#43;&#43;) {
                if (data.ErrorMessage[i]!=&quot;&quot;) {
                    document.getElementById(data.keys[i]).nextElementSibling.style.display = &quot;block&quot;;
                    document.getElementById(data.keys[i]).nextElementSibling.textContent = data.ErrorMessage[i];
                    document.getElementById(data.keys[i]).nextElementSibling.className = &quot;field-validation-error&quot; //field-validation-valid
                }
                else
                {
                    document.getElementById(data.keys[i]).nextElementSibling.style.display = &quot;none&quot;;
                }
            }
           
        }
        else
        {
            $(&quot;#Message&quot;).text(&quot;Registration successful !!!&quot;);
        }
       
    }
&lt;/script&gt;
</pre>
<div class="preview">
<pre class="csharp">@model&nbsp;DemoLoginApp.Models.tbl_RegisterUser&nbsp;
@{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ViewBag.Title&nbsp;=&nbsp;<span class="cs__string">&quot;Index&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;model&nbsp;=&nbsp;Model;&nbsp;
}&nbsp;
&nbsp;
&lt;h2&gt;Welcome&nbsp;To&nbsp;Login&nbsp;Page&lt;/h2&gt;&nbsp;
&nbsp;
&lt;html&gt;&nbsp;
&lt;body&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;container&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;row&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;col-md-6&nbsp;col-md-offset-3&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;panel&nbsp;panel-login&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;panel-heading&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;row&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;col-xs-6&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;a&nbsp;href=<span class="cs__string">&quot;#&quot;</span>&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;active&quot;</span>&nbsp;id=<span class="cs__string">&quot;login-form-link&quot;</span>&gt;Login&lt;/a&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;col-xs-6&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;a&nbsp;href=<span class="cs__string">&quot;#&quot;</span>&nbsp;id=<span class="cs__string">&quot;register-form-link&quot;</span>&gt;Register&lt;/a&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;hr&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;panel-body&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;row&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;col-lg-12&quot;</span>&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@<span class="cs__keyword">using</span>&nbsp;(Html.BeginForm(<span class="cs__string">&quot;Index&quot;</span>,&nbsp;<span class="cs__string">&quot;LoginDemo&quot;</span>,&nbsp;FormMethod.Post,&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;id&nbsp;=&nbsp;<span class="cs__string">&quot;login-form&quot;</span>,&nbsp;style&nbsp;=&nbsp;<span class="cs__string">&quot;display:block&quot;</span>&nbsp;}))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.AntiForgeryToken()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;form-group&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.TextBoxFor(m&nbsp;=&gt;&nbsp;m.sUserName,&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;@<span class="cs__keyword">class</span>&nbsp;=&nbsp;<span class="cs__string">&quot;form-control&quot;</span>,&nbsp;id&nbsp;=&nbsp;<span class="cs__string">&quot;UserName&quot;</span>,&nbsp;placeholder&nbsp;=&nbsp;<span class="cs__string">&quot;Enter&nbsp;User&nbsp;Name/EmailId&quot;</span>,&nbsp;tabindex&nbsp;=&nbsp;<span class="cs__string">&quot;1&quot;</span>&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.ValidationMessageFor(m&nbsp;=&gt;&nbsp;m.sUserName)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;form-group&quot;</span>&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.PasswordFor(m&nbsp;=&gt;&nbsp;m.sPassword,&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;@<span class="cs__keyword">class</span>&nbsp;=&nbsp;<span class="cs__string">&quot;form-control&quot;</span>,&nbsp;id&nbsp;=&nbsp;<span class="cs__string">&quot;password&quot;</span>,&nbsp;placeholder&nbsp;=&nbsp;<span class="cs__string">&quot;Enter&nbsp;Password&quot;</span>,&nbsp;tabindex&nbsp;=&nbsp;<span class="cs__string">&quot;2&quot;</span>&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.ValidationMessageFor(m&nbsp;=&gt;&nbsp;m.sPassword)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;form-group&nbsp;col-lg-offset-4&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;input&nbsp;type=<span class="cs__string">&quot;checkbox&quot;</span>&nbsp;tabindex=<span class="cs__string">&quot;3&quot;</span>&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;&quot;</span>&nbsp;name=<span class="cs__string">&quot;remember&quot;</span>&nbsp;id=<span class="cs__string">&quot;remember&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;label&nbsp;<span class="cs__keyword">for</span>=<span class="cs__string">&quot;remember&quot;</span>&gt;&nbsp;Remember&nbsp;Me&lt;/label&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;form-group&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;row&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;col-sm-4&nbsp;col-lg-offset-3&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;input&nbsp;type=<span class="cs__string">&quot;submit&quot;</span>&nbsp;name=<span class="cs__string">&quot;command&quot;</span>&nbsp;id=<span class="cs__string">&quot;login-submit&quot;</span>&nbsp;tabindex=<span class="cs__string">&quot;4&quot;</span>&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;form-control&nbsp;btn&nbsp;btn-default&quot;</span>&nbsp;<span class="cs__keyword">value</span>=<span class="cs__string">&quot;Log&nbsp;In&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;form-group&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;row&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;col-lg-12&quot;</span>&nbsp;style=<span class="cs__string">&quot;margin-left:30%&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;a&nbsp;href=<span class="cs__string">&quot;#&quot;</span>&nbsp;tabindex=<span class="cs__string">&quot;5&quot;</span>&nbsp;<span class="cs__keyword">class</span>=<span class="cs__string">&quot;forgot-password&quot;</span>&gt;Forgot&nbsp;Password?&lt;/a&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.ValidationSummary(<span class="cs__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@<span class="cs__keyword">using</span>&nbsp;(Ajax&nbsp;.BeginForm(<span class="cs__string">&quot;Register&quot;</span>,&nbsp;<span class="cs__string">&quot;LoginDemo&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;AjaxOptions&nbsp;{&nbsp;OnSuccess&nbsp;=&nbsp;<span class="cs__string">&quot;RegisterSuccess&quot;</span>,&nbsp;HttpMethod&nbsp;=&nbsp;<span class="cs__string">&quot;POST&quot;</span>,&nbsp;UpdateTargetId&nbsp;=&nbsp;<span class="cs__string">&quot;Message&quot;</span>&nbsp;},&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;id&nbsp;=&nbsp;<span class="cs__string">&quot;register-form&quot;</span>,&nbsp;style&nbsp;=&nbsp;<span class="cs__string">&quot;display:none&quot;</span>&nbsp;}))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.AntiForgeryToken()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.ValidationSummary(<span class="cs__keyword">true</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;{&nbsp;@<span class="cs__keyword">class</span>&nbsp;=&nbsp;<span class="cs__string">&quot;text-danger&quot;</span>&nbsp;})&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.Partial(<span class="cs__string">&quot;_RegisterPage&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;label&nbsp;style=<span class="cs__string">&quot;color:green&quot;</span>&nbsp;id=<span class="cs__string">&quot;Message&quot;</span>&gt;&lt;/label&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;
&lt;/body&gt;&nbsp;
&lt;/html&gt;&nbsp;
&lt;script&gt;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(function&nbsp;()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;debugger;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#register-form-link&quot;</span>).click(function&nbsp;()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;debugger;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#register-form&quot;</span>).css(<span class="cs__string">'display'</span>,&nbsp;<span class="cs__string">'block'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#login-form&quot;</span>).css(<span class="cs__string">'display'</span>,&nbsp;<span class="cs__string">'none'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#register-form-link&quot;</span>).addClass(<span class="cs__string">'active'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#login-form-link&quot;</span>).removeClass(<span class="cs__string">'active'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;.field-validation-error&quot;</span>).css(<span class="cs__string">'display'</span>,&nbsp;<span class="cs__string">&quot;none&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;.input-validation-error&quot;</span>).css(<span class="cs__string">&quot;border&quot;</span>,&nbsp;<span class="cs__string">&quot;1px&nbsp;solid&nbsp;#cccccc&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#login-form-link&quot;</span>).click(function&nbsp;()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#register-form&quot;</span>).css(<span class="cs__string">'display'</span>,&nbsp;<span class="cs__string">'none'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#login-form&quot;</span>).css(<span class="cs__string">'display'</span>,&nbsp;<span class="cs__string">'block'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#register-form-link&quot;</span>).removeClass(<span class="cs__string">'active'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#login-form-link&quot;</span>).addClass(<span class="cs__string">'active'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#Message&quot;</span>).hide();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;function&nbsp;RegisterSuccess(data)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;debugger;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(data!=<span class="cs__string">&quot;&quot;</span>)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(var&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;data.keys.length;&nbsp;i&#43;&#43;)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(data.ErrorMessage[i]!=<span class="cs__string">&quot;&quot;</span>)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.getElementById(data.keys[i]).nextElementSibling.style.display&nbsp;=&nbsp;<span class="cs__string">&quot;block&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.getElementById(data.keys[i]).nextElementSibling.textContent&nbsp;=&nbsp;data.ErrorMessage[i];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.getElementById(data.keys[i]).nextElementSibling.className&nbsp;=&nbsp;<span class="cs__string">&quot;field-validation-error&quot;</span>&nbsp;<span class="cs__com">//field-validation-valid</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.getElementById(data.keys[i]).nextElementSibling.style.display&nbsp;=&nbsp;<span class="cs__string">&quot;none&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="cs__string">&quot;#Message&quot;</span>).text(<span class="cs__string">&quot;Registration&nbsp;successful&nbsp;!!!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&lt;/script&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>Now run this app you will get;</p>
<p><img id="170927" src="170927-login%20page.png" alt="" width="1366" height="724"></p>
<h1><img id="170928" src="170928-register.png" alt="" width="1359" height="741"></h1>
<h1><img id="170929" src="170929-error.png" alt="" width="393" height="479"></h1>
<p><strong>After successfully login You get Home as bellow;</strong></p>
<p><img id="170930" src="170930-welcome.png" alt="" width="1327" height="354"></p>
<p>Conclusion:-</p>
<p>In this Demo login app we saw various data annotation attributes with model validations. So far we discus database first approach with inserting record and authenticating user.</p>
<p>&nbsp;</p>
<p>Hope above demo gives you a clear idea how to work with data base first approach with pure model validation .</p>
