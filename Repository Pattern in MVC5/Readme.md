# Repository Pattern in MVC5
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- Entity Framework
- Visual Studio 2013
- MVC5
- EF6
## Topics
- Repository Pattern
- Design Patterns
- DAL
## Updated
- 08/23/2013
## Description

<h1>Introduction</h1>
<p>Repository pattern is a very useful and powerful pattern when manipulating data. It enhance the maintainability and testability, as the adata is accessed and edited from a unique access point which is the Repository. In this application, I used it inside
 an MVC5 app. So, whenever you want to make changes to the way you manipulate the data, you just change code in the repository, and you do yhat once.</p>
<p><em>You can see this application running on line here&nbsp;<a href="http://houssemdellai-mvc5.azurewebsites.net/" target="_blank">http://houssemdellai-mvc5.azurewebsites.net/</a>.</em></p>
<h1>Video</h1>
<p>I made a short video to show the steps to follow to make this demo.</p>
<p><a href="http://www.youtube.com/watch?v=SExnyXhX3gk">http://www.youtube.com/watch?v=SExnyXhX3gk</a></p>
<p class="projectSummary">&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p><em>Are there special requirements or instructions for building the sample?</em></p>
<p><em>This application requires Visual Studio 2013 or Express for Windows 8.</em></p>
<p><em>You can download it from&nbsp;<a href="http://www.microsoft.com/visualstudio/11/en-us">http://www.microsoft.com/visualstudio/11/en-us</a>.</em></p>
<p><em>Then, just open the solution, and click F5 to run it immediately.</em></p>
<h1>Description</h1>
<p><em><em>You can get more informations about it on&nbsp;<em><a href="http://asp.net/vnext" target="_blank">http://www.asp.net/vnext</a>.</em></em></em></p>
<p><em>I started building this demo from this tutorial&nbsp;<a href="http://www.asp.net/web-api/overview/creating-web-apis/using-web-api-with-entity-framework/using-web-api-with-entity-framework,-part-1" target="_blank"></a><a href="http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started">http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started</a>.</em></p>
<p><em>&nbsp;&nbsp;</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden"> public class GenericRepository&lt;TEntity&gt; : IGenericRepository&lt;TEntity&gt; where TEntity : class
    {
        protected DbSet&lt;TEntity&gt; DbSet;

        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set&lt;TEntity&gt;();
        }

        public GenericRepository()
        {
        }

        public IQueryable&lt;TEntity&gt; GetAll()
        {
            return DbSet;
        }

        public async Task&lt;TEntity&gt; GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public IQueryable&lt;TEntity&gt; SearchFor(Expression&lt;Func&lt;TEntity, bool&gt;&gt; predicate)
        {
            return DbSet.Where(predicate);
        }

        public async Task EditAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {

            DbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;GenericRepository&lt;TEntity&gt;&nbsp;:&nbsp;IGenericRepository&lt;TEntity&gt;&nbsp;where&nbsp;TEntity&nbsp;:&nbsp;<span class="cs__keyword">class</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;DbSet&lt;TEntity&gt;&nbsp;DbSet;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;DbContext&nbsp;_dbContext;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;GenericRepository(DbContext&nbsp;dbContext)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dbContext&nbsp;=&nbsp;dbContext;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbSet&nbsp;=&nbsp;_dbContext.Set&lt;TEntity&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;GenericRepository()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IQueryable&lt;TEntity&gt;&nbsp;GetAll()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;DbSet;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;async&nbsp;Task&lt;TEntity&gt;&nbsp;GetByIdAsync(<span class="cs__keyword">int</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;await&nbsp;DbSet.FindAsync(id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IQueryable&lt;TEntity&gt;&nbsp;SearchFor(Expression&lt;Func&lt;TEntity,&nbsp;<span class="cs__keyword">bool</span>&gt;&gt;&nbsp;predicate)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;DbSet.Where(predicate);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;async&nbsp;Task&nbsp;EditAsync(TEntity&nbsp;entity)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dbContext.Entry(entity).State&nbsp;=&nbsp;EntityState.Modified;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;await&nbsp;_dbContext.SaveChangesAsync();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;async&nbsp;Task&nbsp;InsertAsync(TEntity&nbsp;entity)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbSet.Add(entity);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;await&nbsp;_dbContext.SaveChangesAsync();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;async&nbsp;Task&nbsp;DeleteAsync(TEntity&nbsp;entity)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbSet.Remove(entity);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;await&nbsp;_dbContext.SaveChangesAsync();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1>Source Code Files</h1>
<ul>
<li><em>User.cs &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; : the model to be mapped to the database using the Entity Framework.</em>
</li><li><em>UsersController.cs : the main controller that will be used to manage the CRUD operations.</em>
</li><li><em>GenericRepository.cs : the implementation of the Repository pattern. It's a generic class, so it could be used
<span style="white-space:pre">&nbsp;</span>&nbsp; &nbsp; &nbsp;&nbsp;with any type of entities.<br>
</em></li></ul>
<h1>More Information</h1>
<p><em>For more information, y<em>ou can post on the Q&amp;A area or contact me on: houssem.dellai@live.com.</em></em></p>
<address>Please don't forget to rate my application and to&nbsp;<a href="http://code.msdn.microsoft.com/site/search?f%5B0%5D.Type=User&f%5B0%5D.Value=Houssem%20Dellai" target="_blank">see my other samples here</a>.</address>
