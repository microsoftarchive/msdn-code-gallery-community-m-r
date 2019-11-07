# MVC 3, 4 web image handler  barcode
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- ASP.NET MVC 3
- ASP.NET MVC 4
- https://www.youtube.com/user/techhousevideos
## Topics
- http://www.nha3mien.com/help/default.aspx
- https://www.youtube.com/user/techhousevideos
## Updated
- 05/29/2014
## Description

<h1>Introduction</h1>
<p>In this post we are going to introduce you how to <strong>created barcode</strong> in ASP.NET MVC 3, MVC 4. If you haven't already installed ASP.NET MVC 3 use can download it from here:
<a title="ASP.NET MVC 3" href="http://www.asp.net/mvc/MVC%203" target="_blank">http://www.asp.net/mvc/MVC 3</a>.</p>
<p>In this post we are introduce create image handler with MVC3, MVC4 too.</p>
<p><img id="73679" src="73679-12-28-2012%2010-20-26%20am.png" alt="" width="401" height="358" style="display:block; margin-left:auto; margin-right:auto"></p>
<p>&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p><em>I try to use Visual Studio 2012 RC to build Magazine website with front page and admin page. It is just a small sample, so I try to make it as simple as possible.</em></p>
<h2>Creating Your First Application</h2>
<p>You can create applications using either Visual Basic or Visual C# as the programming language. Select Visual C# on the left and then select
<strong>ASP.NET MVC 3 Web Application</strong>. Name your project &quot;MvcMovie&quot; and then click
<strong>OK</strong>.</p>
<p>Notice that the keyboard shortcut to start debugging is F5.</p>
<p>F5 causes Visual Web Developer to start IIS Express and run your web application. Visual Web Developer then launches a browser and opens the application's home page. Notice that the address bar of the browser says
<code>localhost</code> and not something like <code>example.com</code>. That's because
<code>localhost</code> always points to your own local computer, which in this case is running the application you just built. When Visual Web Developer runs a web project, a random port is used for the web server. In the image below, the port number is 41788.
 When you run the application, you'll probably see a different port number.</p>
<p>Right out of the box this default template gives you&nbsp; Home, Contact and About pages. It also provides support to register and log in, and links to Facebook and Twitter. The next step is to change how this application works and learn a little bit about
 ASP.NET MVC. Close your browser and let's change some code.</p>
<p>MVC stands for<span class="Apple">&nbsp;</span><em style="background-color:transparent; margin:0px; outline-width:0px; font-size:13px; vertical-align:baseline; border-width:0px; padding:0px">model-view-controller</em>. MVC is a pattern for developing applications
 that are well architected, testable and easy to maintain. MVC-based applications contain:</p>
<ul class="auto" style="widows:2; text-transform:none; background-color:transparent; list-style-type:disc; text-indent:0px; outline-width:0px; letter-spacing:normal; font:13px/18px 'Segoe UI',Tahoma,Arial,Helvetica,sans-serif; white-space:normal; orphans:2; color:#44525e; vertical-align:baseline; word-spacing:0px; border-width:0px; padding:0px">
<li style="background-color:transparent; margin:0px 0px 5px; outline-width:0px; font-size:13px; vertical-align:baseline; border-width:0px; padding:0px">
<strong>M</strong>odels: Classes that represent the data of the application and that use validation logic to enforce business rules for that data.
</li><li style="background-color:transparent; margin:0px 0px 5px; outline-width:0px; font-size:13px; vertical-align:baseline; border-width:0px; padding:0px">
<strong>V</strong>iews: Template files that your application uses to dynamically generate HTML responses.
</li><li style="background-color:transparent; margin:0px 0px 5px; outline-width:0px; font-size:13px; vertical-align:baseline; border-width:0px; padding:0px">
<strong>C</strong>ontrollers: Classes that handle incoming browser requests, retrieve model data, and then specify view templates that return a response to the browser.
</li></ul>
<p style="widows:2; text-transform:none; background-color:transparent; text-indent:0px; margin:0px 0px 18px; outline-width:0px; letter-spacing:normal; font:13px/18px 'Segoe UI',Tahoma,Arial,Helvetica,sans-serif; white-space:normal; orphans:2; color:#44525e; vertical-align:baseline; word-spacing:0px; border-width:0px; padding:0px">
We'll be covering all these concepts in this tutorial series and show you how to use them to build an application.</p>
<p><strong>1. HomeController.cs &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong><br>
&nbsp; public class HomeController : Controller<br>
&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; public ActionResult Index()<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ViewBag.Message = &quot;MVC WEB BARCODE!!&quot;;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; string vCode = &quot;nha3mien.com&quot;;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; string barCode = BarCodeToHTML.get39(vCode, 2, 20);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ViewBag.htmlBarcode = barCode;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ViewBag.vCode = vCode;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; return View();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [HttpPost]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; public ActionResult Index(FormCollection f)<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ViewBag.Message = &quot;MVC WEB BARCODE!&quot;;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var vCode = f[&quot;txtcode&quot;];<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; string barCode = BarCodeToHTML.get39(vCode, 2, 20);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ViewBag.htmlBarcode = barCode;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ViewBag.vCode = vCode;<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; return View();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; public ActionResult About()<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; return View();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>
&nbsp;&nbsp; &nbsp; }</p>
<p><strong>2. Global.asax&nbsp;&nbsp; </strong><br>
public class MvcApplication : System.Web.HttpApplication<br>
&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; public static void RegisterGlobalFilters(GlobalFilterCollection filters)<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filters.Add(new HandleErrorAttribute());<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; public static void RegisterRoutes(RouteCollection routes)<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; routes.IgnoreRoute(&quot;{*BarCodeHandler}&quot;, new { BarCodeHandler = @&quot;(.*/)?Barcode.ashx(/.*)?&quot; });<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; routes.IgnoreRoute(&quot;{resource}.axd/{*pathInfo}&quot;);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; routes.MapRoute(<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;Default&quot;, // Route name<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;{controller}/{action}/{id}&quot;, // URL with parameters<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; new { controller = &quot;Home&quot;, action = &quot;Index&quot;, id = UrlParameter.Optional } // Parameter defaults<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; protected void Application_Start()<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AreaRegistration.RegisterAllAreas();<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; // Use LocalDB for Entity Framework by default<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Database.DefaultConnectionFactory = new SqlConnectionFactory(@&quot;Data Source=(localdb)\v11.0;&nbsp;&nbsp;&nbsp;&nbsp; Integrated Security=True; MultipleActiveResultSets=True&quot;);<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RegisterGlobalFilters(GlobalFilters.Filters);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RegisterRoutes(RouteTable.Routes);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>
&nbsp;&nbsp;&nbsp; }</p>
<p><strong>3. Web.config</strong></p>
<p><strong>&nbsp; - add attributer</strong></p>
<p>&nbsp;&nbsp; &lt;system.webServer&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;validation validateIntegratedModeConfiguration=&quot;false&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;modules runAllManagedModulesForAllRequests=&quot;true&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;handlers&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;add name=&quot;Barcode&quot; verb=&quot;*&quot; path=&quot;Barcode.ashx&quot; preCondition=&quot;integratedMode&quot;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; type=&quot;WebBarCodec.Core.BarCodeHandler&quot; /&gt;<br>
&nbsp;&nbsp; &nbsp;&nbsp; &lt;/handlers&gt;<br>
&nbsp; &lt;/system.webServer&gt;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>&nbsp;</p>
<p><em>&nbsp;</em></p>
<p>&nbsp; - Image hadler file : <strong>BarCodeHandler.cs</strong></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class BarCodeHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string _codeText = string.Empty;
            string _Magnify = string.Empty;
            string _height = string.Empty;
            if (context.Request[&quot;vCode&quot;] != null)
                _codeText = context.Request[&quot;vCode&quot;].Trim();
            if (context.Request[&quot;m&quot;] != null)
                _Magnify = context.Request[&quot;m&quot;];
            if (context.Request[&quot;h&quot;] != null)
                _height = context.Request[&quot;h&quot;];
            _codeText = string.IsNullOrEmpty(_codeText) ? &quot;123ABC4567890FWF&quot; : _codeText;
            _Magnify = string.IsNullOrEmpty(_Magnify) ? &quot;1&quot; : _Magnify;
            _height = string.IsNullOrEmpty(_height) ? &quot;120&quot; : _height;
            this.Height = int.Parse(_height);
            this.Magnify = byte.Parse(_Magnify);
            this.ViewFont = new Font(&quot;Arial&quot;, 20);
            Code39();
            System.Drawing.Image _CodeImage = this.GetCodeImage(_codeText, Code39Model.Code39Normal, true);
            context.Response.ContentType = &quot;image/jpeg&quot;;
            _CodeImage.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private Hashtable m_Code39 = new Hashtable();

        private byte m_Magnify = 0;
        public byte Magnify { get { return m_Magnify; } set { m_Magnify = value; } }

        private int m_Height = 40;
        public int Height { get { return m_Height; } set { m_Height = value; } }

        private Font m_ViewFont = null;
        public Font ViewFont { get { return m_ViewFont; } set { m_ViewFont = value; } }



        public void Code39()
        {

            m_Code39.Add(&quot;A&quot;, &quot;1101010010110&quot;);
            m_Code39.Add(&quot;B&quot;, &quot;1011010010110&quot;);
            m_Code39.Add(&quot;C&quot;, &quot;1101101001010&quot;);
            m_Code39.Add(&quot;D&quot;, &quot;1010110010110&quot;);
            m_Code39.Add(&quot;E&quot;, &quot;1101011001010&quot;);
            m_Code39.Add(&quot;F&quot;, &quot;1011011001010&quot;);
            m_Code39.Add(&quot;G&quot;, &quot;1010100110110&quot;);
            m_Code39.Add(&quot;H&quot;, &quot;1101010011010&quot;);
            m_Code39.Add(&quot;I&quot;, &quot;1011010011010&quot;);
            m_Code39.Add(&quot;J&quot;, &quot;1010110011010&quot;);
            m_Code39.Add(&quot;K&quot;, &quot;1101010100110&quot;);
            m_Code39.Add(&quot;L&quot;, &quot;1011010100110&quot;);
            m_Code39.Add(&quot;M&quot;, &quot;1101101010010&quot;);
            m_Code39.Add(&quot;N&quot;, &quot;1010110100110&quot;);
            m_Code39.Add(&quot;O&quot;, &quot;1101011010010&quot;);
            m_Code39.Add(&quot;P&quot;, &quot;1011011010010&quot;);
            m_Code39.Add(&quot;Q&quot;, &quot;1010101100110&quot;);
            m_Code39.Add(&quot;R&quot;, &quot;1101010110010&quot;);
            m_Code39.Add(&quot;S&quot;, &quot;1011010110010&quot;);
            m_Code39.Add(&quot;T&quot;, &quot;1010110110010&quot;);
            m_Code39.Add(&quot;U&quot;, &quot;1100101010110&quot;);
            m_Code39.Add(&quot;V&quot;, &quot;1001101010110&quot;);
            m_Code39.Add(&quot;W&quot;, &quot;1100110101010&quot;);
            m_Code39.Add(&quot;X&quot;, &quot;1001011010110&quot;);
            m_Code39.Add(&quot;Y&quot;, &quot;1100101101010&quot;);
            m_Code39.Add(&quot;Z&quot;, &quot;1001101101010&quot;);
            m_Code39.Add(&quot;0&quot;, &quot;1010011011010&quot;);
            m_Code39.Add(&quot;1&quot;, &quot;1101001010110&quot;);
            m_Code39.Add(&quot;2&quot;, &quot;1011001010110&quot;);
            m_Code39.Add(&quot;3&quot;, &quot;1101100101010&quot;);
            m_Code39.Add(&quot;4&quot;, &quot;1010011010110&quot;);
            m_Code39.Add(&quot;5&quot;, &quot;1101001101010&quot;);
            m_Code39.Add(&quot;6&quot;, &quot;1011001101010&quot;);
            m_Code39.Add(&quot;7&quot;, &quot;1010010110110&quot;);
            m_Code39.Add(&quot;8&quot;, &quot;1101001011010&quot;);
            m_Code39.Add(&quot;9&quot;, &quot;1011001011010&quot;);
            m_Code39.Add(&quot;&#43;&quot;, &quot;1001010010010&quot;);
            m_Code39.Add(&quot;-&quot;, &quot;1001010110110&quot;);
            m_Code39.Add(&quot;*&quot;, &quot;1001011011010&quot;);
            m_Code39.Add(&quot;/&quot;, &quot;1001001010010&quot;);
            m_Code39.Add(&quot;%&quot;, &quot;1010010010010&quot;);
            m_Code39.Add(&quot;contentquot&quot;, &quot;1001001001010&quot;);
            m_Code39.Add(&quot;.&quot;, &quot;1100101011010&quot;);
            m_Code39.Add(&quot; &quot;, &quot;1001101011010&quot;);

        }


        public enum Code39Model
        {
            Code39Normal,
            Code39FullAscII
        }
        public Bitmap GetCodeImage(string p_Text, Code39Model p_Model, bool p_StarChar)
        {
            string _ValueText = &quot;&quot;;
            string _CodeText = &quot;&quot;;
            char[] _ValueChar = null;
            switch (p_Model)
            {
                case Code39Model.Code39Normal:
                    _ValueText = p_Text.ToUpper();
                    break;
                default:
                    _ValueChar = p_Text.ToCharArray();
                    for (int i = 0; i != _ValueChar.Length; i&#43;&#43;)
                    {
                        if ((int)_ValueChar[i] &gt;= 97 &amp;&amp; (int)_ValueChar[i] &lt;= 122)
                        {
                            _ValueText &#43;= &quot;&#43;&quot; &#43; _ValueChar[i].ToString().ToUpper();

                        }
                        else
                        {
                            _ValueText &#43;= _ValueChar[i].ToString();
                        }
                    }
                    break;
            }


            _ValueChar = _ValueText.ToCharArray();

            if (p_StarChar == true) _CodeText &#43;= m_Code39[&quot;*&quot;];

            for (int i = 0; i != _ValueChar.Length; i&#43;&#43;)
            {
                if (p_StarChar == true &amp;&amp; _ValueChar[i] == '*') throw new Exception(&quot;The first symbol can not appear *&quot;);
                object _CharCode = m_Code39[_ValueChar[i].ToString()];
                if (_CharCode == null) throw new Exception(&quot;Characters unavailable&quot; &#43; _ValueChar[i].ToString());
                _CodeText &#43;= _CharCode.ToString();
            }


            if (p_StarChar == true) _CodeText &#43;= m_Code39[&quot;*&quot;];


            Bitmap _CodeBmp = GetImage(_CodeText);
            GetViewImage(_CodeBmp, p_Text);
            return _CodeBmp;
        }



        private Bitmap GetImage(string p_Text)
        {
            char[] _Value = p_Text.ToCharArray();


            Bitmap _CodeImage = new Bitmap(_Value.Length * ((int)m_Magnify &#43; 1), (int)m_Height);
            Graphics _Garphics = Graphics.FromImage(_CodeImage);

            _Garphics.FillRectangle(Brushes.White, new Rectangle(0, 0, _CodeImage.Width, _CodeImage.Height));

            int _LenEx = 0;
            for (int i = 0; i != _Value.Length; i&#43;&#43;)
            {
                int _DrawWidth = m_Magnify &#43; 1;
                if (_Value[i] == '1')
                {
                    _Garphics.FillRectangle(Brushes.Black, new Rectangle(_LenEx, 0, _DrawWidth, m_Height));

                }
                else
                {
                    _Garphics.FillRectangle(Brushes.White, new Rectangle(_LenEx, 0, _DrawWidth, m_Height));
                }
                _LenEx &#43;= _DrawWidth;
            }



            _Garphics.Dispose();
            return _CodeImage;
        }
        private void GetViewImage(Bitmap p_CodeImage, string p_Text)
        {
            if (m_ViewFont == null) return;
            Graphics _Graphics = Graphics.FromImage(p_CodeImage);
            SizeF _FontSize = _Graphics.MeasureString(p_Text, m_ViewFont);

            if (_FontSize.Width &gt; p_CodeImage.Width || _FontSize.Height &gt; p_CodeImage.Height - 20)
            {
                _Graphics.Dispose();
                return;
            }
            int _StarHeight = p_CodeImage.Height - (int)_FontSize.Height;

            _Graphics.FillRectangle(Brushes.White, new Rectangle(0, _StarHeight, p_CodeImage.Width, (int)_FontSize.Height));

            int _StarWidth = (p_CodeImage.Width - (int)_FontSize.Width) / 2;

            _Graphics.DrawString(p_Text, m_ViewFont, Brushes.Black, _StarWidth, _StarHeight);

            _Graphics.Dispose();

        }
    }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;BarCodeHandler&nbsp;:&nbsp;IHttpHandler,&nbsp;IRequiresSessionState&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ProcessRequest(HttpContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;_codeText&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;_Magnify&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;_height&nbsp;=&nbsp;<span class="cs__keyword">string</span>.Empty;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(context.Request[<span class="cs__string">&quot;vCode&quot;</span>]&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_codeText&nbsp;=&nbsp;context.Request[<span class="cs__string">&quot;vCode&quot;</span>].Trim();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(context.Request[<span class="cs__string">&quot;m&quot;</span>]&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Magnify&nbsp;=&nbsp;context.Request[<span class="cs__string">&quot;m&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(context.Request[<span class="cs__string">&quot;h&quot;</span>]&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_height&nbsp;=&nbsp;context.Request[<span class="cs__string">&quot;h&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_codeText&nbsp;=&nbsp;<span class="cs__keyword">string</span>.IsNullOrEmpty(_codeText)&nbsp;?&nbsp;<span class="cs__string">&quot;123ABC4567890FWF&quot;</span>&nbsp;:&nbsp;_codeText;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Magnify&nbsp;=&nbsp;<span class="cs__keyword">string</span>.IsNullOrEmpty(_Magnify)&nbsp;?&nbsp;<span class="cs__string">&quot;1&quot;</span>&nbsp;:&nbsp;_Magnify;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_height&nbsp;=&nbsp;<span class="cs__keyword">string</span>.IsNullOrEmpty(_height)&nbsp;?&nbsp;<span class="cs__string">&quot;120&quot;</span>&nbsp;:&nbsp;_height;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Height&nbsp;=&nbsp;<span class="cs__keyword">int</span>.Parse(_height);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Magnify&nbsp;=&nbsp;<span class="cs__keyword">byte</span>.Parse(_Magnify);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.ViewFont&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Font(<span class="cs__string">&quot;Arial&quot;</span>,&nbsp;<span class="cs__number">20</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code39();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Drawing.Image&nbsp;_CodeImage&nbsp;=&nbsp;<span class="cs__keyword">this</span>.GetCodeImage(_codeText,&nbsp;Code39Model.Code39Normal,&nbsp;<span class="cs__keyword">true</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.Response.ContentType&nbsp;=&nbsp;<span class="cs__string">&quot;image/jpeg&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_CodeImage.Save(context.Response.OutputStream,&nbsp;System.Drawing.Imaging.ImageFormat.Jpeg);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsReusable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Hashtable&nbsp;m_Code39&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Hashtable();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;m_Magnify&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">byte</span>&nbsp;Magnify&nbsp;{&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;m_Magnify;&nbsp;}&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;m_Magnify&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;}&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;m_Height&nbsp;=&nbsp;<span class="cs__number">40</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Height&nbsp;{&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;m_Height;&nbsp;}&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;m_Height&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;}&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Font&nbsp;m_ViewFont&nbsp;=&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Font&nbsp;ViewFont&nbsp;{&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;m_ViewFont;&nbsp;}&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;m_ViewFont&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;}&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Code39()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;A&quot;</span>,&nbsp;<span class="cs__string">&quot;1101010010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;B&quot;</span>,&nbsp;<span class="cs__string">&quot;1011010010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;C&quot;</span>,&nbsp;<span class="cs__string">&quot;1101101001010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;D&quot;</span>,&nbsp;<span class="cs__string">&quot;1010110010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;E&quot;</span>,&nbsp;<span class="cs__string">&quot;1101011001010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;F&quot;</span>,&nbsp;<span class="cs__string">&quot;1011011001010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;G&quot;</span>,&nbsp;<span class="cs__string">&quot;1010100110110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;H&quot;</span>,&nbsp;<span class="cs__string">&quot;1101010011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;I&quot;</span>,&nbsp;<span class="cs__string">&quot;1011010011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;J&quot;</span>,&nbsp;<span class="cs__string">&quot;1010110011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;K&quot;</span>,&nbsp;<span class="cs__string">&quot;1101010100110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;L&quot;</span>,&nbsp;<span class="cs__string">&quot;1011010100110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;M&quot;</span>,&nbsp;<span class="cs__string">&quot;1101101010010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;N&quot;</span>,&nbsp;<span class="cs__string">&quot;1010110100110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;O&quot;</span>,&nbsp;<span class="cs__string">&quot;1101011010010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;P&quot;</span>,&nbsp;<span class="cs__string">&quot;1011011010010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;Q&quot;</span>,&nbsp;<span class="cs__string">&quot;1010101100110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;R&quot;</span>,&nbsp;<span class="cs__string">&quot;1101010110010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;S&quot;</span>,&nbsp;<span class="cs__string">&quot;1011010110010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;T&quot;</span>,&nbsp;<span class="cs__string">&quot;1010110110010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;U&quot;</span>,&nbsp;<span class="cs__string">&quot;1100101010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;V&quot;</span>,&nbsp;<span class="cs__string">&quot;1001101010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;W&quot;</span>,&nbsp;<span class="cs__string">&quot;1100110101010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;X&quot;</span>,&nbsp;<span class="cs__string">&quot;1001011010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;Y&quot;</span>,&nbsp;<span class="cs__string">&quot;1100101101010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;Z&quot;</span>,&nbsp;<span class="cs__string">&quot;1001101101010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;0&quot;</span>,&nbsp;<span class="cs__string">&quot;1010011011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;1&quot;</span>,&nbsp;<span class="cs__string">&quot;1101001010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;2&quot;</span>,&nbsp;<span class="cs__string">&quot;1011001010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;3&quot;</span>,&nbsp;<span class="cs__string">&quot;1101100101010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;4&quot;</span>,&nbsp;<span class="cs__string">&quot;1010011010110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;5&quot;</span>,&nbsp;<span class="cs__string">&quot;1101001101010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;6&quot;</span>,&nbsp;<span class="cs__string">&quot;1011001101010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;7&quot;</span>,&nbsp;<span class="cs__string">&quot;1010010110110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;8&quot;</span>,&nbsp;<span class="cs__string">&quot;1101001011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;9&quot;</span>,&nbsp;<span class="cs__string">&quot;1011001011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;&#43;&quot;</span>,&nbsp;<span class="cs__string">&quot;1001010010010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;-&quot;</span>,&nbsp;<span class="cs__string">&quot;1001010110110&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;*&quot;</span>,&nbsp;<span class="cs__string">&quot;1001011011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;/&quot;</span>,&nbsp;<span class="cs__string">&quot;1001001010010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;%&quot;</span>,&nbsp;<span class="cs__string">&quot;1010010010010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;contentquot&quot;</span>,&nbsp;<span class="cs__string">&quot;1001001001010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;.&quot;</span>,&nbsp;<span class="cs__string">&quot;1100101011010&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_Code39.Add(<span class="cs__string">&quot;&nbsp;&quot;</span>,&nbsp;<span class="cs__string">&quot;1001101011010&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">enum</span>&nbsp;Code39Model&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code39Normal,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code39FullAscII&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Bitmap&nbsp;GetCodeImage(<span class="cs__keyword">string</span>&nbsp;p_Text,&nbsp;Code39Model&nbsp;p_Model,&nbsp;<span class="cs__keyword">bool</span>&nbsp;p_StarChar)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;_ValueText&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;_CodeText&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">char</span>[]&nbsp;_ValueChar&nbsp;=&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(p_Model)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Code39Model.Code39Normal:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_ValueText&nbsp;=&nbsp;p_Text.ToUpper();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">default</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_ValueChar&nbsp;=&nbsp;p_Text.ToCharArray();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;!=&nbsp;_ValueChar.Length;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;((<span class="cs__keyword">int</span>)_ValueChar[i]&nbsp;&gt;=&nbsp;<span class="cs__number">97</span>&nbsp;&amp;&amp;&nbsp;(<span class="cs__keyword">int</span>)_ValueChar[i]&nbsp;&lt;=&nbsp;<span class="cs__number">122</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_ValueText&nbsp;&#43;=&nbsp;<span class="cs__string">&quot;&#43;&quot;</span>&nbsp;&#43;&nbsp;_ValueChar[i].ToString().ToUpper();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_ValueText&nbsp;&#43;=&nbsp;_ValueChar[i].ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_ValueChar&nbsp;=&nbsp;_ValueText.ToCharArray();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(p_StarChar&nbsp;==&nbsp;<span class="cs__keyword">true</span>)&nbsp;_CodeText&nbsp;&#43;=&nbsp;m_Code39[<span class="cs__string">&quot;*&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;!=&nbsp;_ValueChar.Length;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(p_StarChar&nbsp;==&nbsp;<span class="cs__keyword">true</span>&nbsp;&amp;&amp;&nbsp;_ValueChar[i]&nbsp;==&nbsp;<span class="cs__string">'*'</span>)&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;Exception(<span class="cs__string">&quot;The&nbsp;first&nbsp;symbol&nbsp;can&nbsp;not&nbsp;appear&nbsp;*&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">object</span>&nbsp;_CharCode&nbsp;=&nbsp;m_Code39[_ValueChar[i].ToString()];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_CharCode&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;Exception(<span class="cs__string">&quot;Characters&nbsp;unavailable&quot;</span>&nbsp;&#43;&nbsp;_ValueChar[i].ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_CodeText&nbsp;&#43;=&nbsp;_CharCode.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(p_StarChar&nbsp;==&nbsp;<span class="cs__keyword">true</span>)&nbsp;_CodeText&nbsp;&#43;=&nbsp;m_Code39[<span class="cs__string">&quot;*&quot;</span>];&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bitmap&nbsp;_CodeBmp&nbsp;=&nbsp;GetImage(_CodeText);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetViewImage(_CodeBmp,&nbsp;p_Text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;_CodeBmp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Bitmap&nbsp;GetImage(<span class="cs__keyword">string</span>&nbsp;p_Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">char</span>[]&nbsp;_Value&nbsp;=&nbsp;p_Text.ToCharArray();&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bitmap&nbsp;_CodeImage&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Bitmap(_Value.Length&nbsp;*&nbsp;((<span class="cs__keyword">int</span>)m_Magnify&nbsp;&#43;&nbsp;<span class="cs__number">1</span>),&nbsp;(<span class="cs__keyword">int</span>)m_Height);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Graphics&nbsp;_Garphics&nbsp;=&nbsp;Graphics.FromImage(_CodeImage);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Garphics.FillRectangle(Brushes.White,&nbsp;<span class="cs__keyword">new</span>&nbsp;Rectangle(<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>,&nbsp;_CodeImage.Width,&nbsp;_CodeImage.Height));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;_LenEx&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;!=&nbsp;_Value.Length;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;_DrawWidth&nbsp;=&nbsp;m_Magnify&nbsp;&#43;&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_Value[i]&nbsp;==&nbsp;<span class="cs__string">'1'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Garphics.FillRectangle(Brushes.Black,&nbsp;<span class="cs__keyword">new</span>&nbsp;Rectangle(_LenEx,&nbsp;<span class="cs__number">0</span>,&nbsp;_DrawWidth,&nbsp;m_Height));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Garphics.FillRectangle(Brushes.White,&nbsp;<span class="cs__keyword">new</span>&nbsp;Rectangle(_LenEx,&nbsp;<span class="cs__number">0</span>,&nbsp;_DrawWidth,&nbsp;m_Height));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_LenEx&nbsp;&#43;=&nbsp;_DrawWidth;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Garphics.Dispose();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;_CodeImage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;GetViewImage(Bitmap&nbsp;p_CodeImage,&nbsp;<span class="cs__keyword">string</span>&nbsp;p_Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(m_ViewFont&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;<span class="cs__keyword">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Graphics&nbsp;_Graphics&nbsp;=&nbsp;Graphics.FromImage(p_CodeImage);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SizeF&nbsp;_FontSize&nbsp;=&nbsp;_Graphics.MeasureString(p_Text,&nbsp;m_ViewFont);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_FontSize.Width&nbsp;&gt;&nbsp;p_CodeImage.Width&nbsp;||&nbsp;_FontSize.Height&nbsp;&gt;&nbsp;p_CodeImage.Height&nbsp;-&nbsp;<span class="cs__number">20</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Graphics.Dispose();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;_StarHeight&nbsp;=&nbsp;p_CodeImage.Height&nbsp;-&nbsp;(<span class="cs__keyword">int</span>)_FontSize.Height;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Graphics.FillRectangle(Brushes.White,&nbsp;<span class="cs__keyword">new</span>&nbsp;Rectangle(<span class="cs__number">0</span>,&nbsp;_StarHeight,&nbsp;p_CodeImage.Width,&nbsp;(<span class="cs__keyword">int</span>)_FontSize.Height));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;_StarWidth&nbsp;=&nbsp;(p_CodeImage.Width&nbsp;-&nbsp;(<span class="cs__keyword">int</span>)_FontSize.Width)&nbsp;/&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Graphics.DrawString(p_Text,&nbsp;m_ViewFont,&nbsp;Brushes.Black,&nbsp;_StarWidth,&nbsp;_StarHeight);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_Graphics.Dispose();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>source code file name #1 - summary for this source code file.</em> </li><li><em><em>source code file name #2 - summary for this source code file.</em></em>
</li></ul>
<h1>More Information</h1>
<p><em>- Goto site : &lt;label&gt;https://www.youtube.com/user/techhousevideos&lt;/label&gt;</em></p>
<p><em>- Email: info@nha3mien.com</em></p>
<p><em>- Phone : 84903487428<br>
</em></p>
