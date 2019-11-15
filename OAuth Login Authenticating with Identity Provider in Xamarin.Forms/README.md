# OAuth Login Authenticating with Identity Provider in Xamarin.Forms
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Xamarin.Android
- Xamarin
- Xamarin.Forms
## Topics
- OAuth
- Xamarin/Android
- Xamarin
- Xamarin.Forms
- Register new OAuth Application
## Updated
- 10/20/2016
## Description

<h1><span style="font-size:x-large">Introduction:</span></h1>
<p dir="ltr"><span style="font-size:small">OAuth is an Authorization framework that enable application to obtain limited access to user accounts on HTTP service in Facebook, google and Microsoft etc. Nowadays no need to create registration logic alternatively
 you can choose to use identity provider login. In this case a person signs up for the app using identity provider Login, an account is created for them, and the authentication step is taken care of by identity provider.</span></p>
<p dir="ltr"><span style="font-size:small">In this article I will explain how to implement below oAuth identity provider in xamarin Forms and manage the authentication process in a xamarin Forms application</span></p>
<ol>
<li>GOOGLE </li><li>FACEBOOK </li><li>TWITTER </li><li>MICROSOFT </li><li>LINKEDIN </li><li>GITHUB </li><li>FLICKER </li><li>YAHOO </li><li>DROPBOX </li></ol>
<p dir="ltr"><span><img src="https://lh5.googleusercontent.com/B2_8dc1U1v_3e9YF1ALsN0RG7wZYcbrO0hHy8sEV1Z_VzdBBcwRIIuiXj5hA1w071_3Di6hl5-9vZG6yX0pPLjlNcAu5LzIxFuBpWz-62MXovdJNjqqfGMiJyaSe9boBM6JpvgvbzeNdZAXSQg" alt="" width="294" height="567"></span></p>
<h1 dir="ltr"><span style="font-size:large">Register Mobile App with Identity Provider:</span></h1>
<p dir="ltr"><span style="font-size:small">You can find my previous article for register mobile app with identity provider from
<a href="https://code.msdn.microsoft.com/Register-Identity-Provider-41955544">here</a></span></p>
<h1 dir="ltr"><span style="font-size:large">Step 1: Create New Xamarin.Forms Project:</span></h1>
<p dir="ltr"><span style="font-size:small">Let Start create new Xamarin Forms Project in Visual studio</span></p>
<p dir="ltr"><span style="font-size:small">Open Run ➔ Type Devenev.Exe and enter ➔ New Project (Ctrl&#43;Shift&#43;N)➔ select Blank Xamarin.Forms Portable template</span></p>
<p dir="ltr"><span><img src="https://lh5.googleusercontent.com/A7etBVGKpxedewD3XBnZKrOkOZ1lM9N4SBFa0FDo-uNVDRV1UMGueRfR4r5Z7ageqzTuYa9rSOHDvHx37F47DOXfve7W1OobYRez6BOcsrYYakbmk0YFI-mKg1PLShYjc0XXCALU3N29NA-F1g" alt="" width="624" height="433"></span></p>
<p dir="ltr"><span style="font-size:small">It will automatically create multiple project like Portable, Android, iOS, UWP but Here, I will be targeting only Android, as iOS and UWP implementation is similar.</span></p>
<h1><span style="font-size:large"><strong>Step 2: Install OAuth Client Components</strong></span></h1>
<p dir="ltr"><span style="font-size:small">Xamarin.Auth is a cross-platform SDK for authenticating users and storing their accounts. It includes OAuth authenticators that provide support for consuming identity providers.</span></p>
<p dir="ltr"><span style="font-size:small">Let's add the Xamarin.Auth component&nbsp;for OAuth. We will have to add this in all platform specific projects separately.</span></p>
<p dir="ltr"><span style="font-size:small">Go to Any project (DevEnVExeLogin.Droid) ➔ Components ➔ Right Click Get More Components
</span></p>
<p dir="ltr"><span style="font-size:small">If you are not login already, it will show login page.</span></p>
<p dir="ltr"><span style="font-size:small">Next, Search and double-click on&nbsp;Xamarin.Auth
<a href="https://components.xamarin.com/view/xamarin.auth">component</a> and click on Add to App</span></p>
<p dir="ltr"><span><img src="https://lh4.googleusercontent.com/-geAaDDbndS1RoEuEE6XZH1nDX2N259Lotw3JNNFqSR4G09IAKqqRuGXGLAkpVxD9MsTHa2yq8Kc-bYZ6q_SBx-tJSn9QPiq7L_uGImLSXRcShA6E-UEQNlWx-Rf-QFB5ZhxUyv6Os9_IF2caw" alt="" width="624" height="234"></span></p>
<p><br>
<br>
</p>
<h1 dir="ltr"><span style="font-size:large">Step 3: Create Base Login Page (LoginPage.Xaml)</span></h1>
<p dir="ltr"><span style="font-size:small">I have created quick and simple login screen .You can modify as per your requirement
</span></p>
<p dir="ltr"><span style="font-size:small">Right Click Portable Class Library ➔ Add New Item ➔ Select Xaml Page(Login Page)</span></p>
<p dir="ltr"><strong><span style="font-size:medium">LoginPage.Xaml</span></strong></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>

<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;?xml</span>&nbsp;<span class="xaml__attr_name">version</span>=<span class="xaml__attr_value">&quot;1.0&quot;</span>&nbsp;<span class="xaml__attr_name">encoding</span>=<span class="xaml__attr_value">&quot;utf-8&quot;</span>&nbsp;<span class="xaml__tag_start">?&gt;</span>&nbsp;
&nbsp;
<span class="xaml__tag_start">&lt;ContentPage</span>&nbsp;<span class="xaml__attr_name">xmlns</span>=<span class="xaml__attr_value">&quot;http://xamarin.com/schemas/2014/forms&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">x</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2009/xaml&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">local</span>=<span class="xaml__attr_value">&quot;clr-namespace:DevEnvExeLogin&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Class</span>=<span class="xaml__attr_value">&quot;DevEnvExeLogin.LoginPage&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;
&nbsp;<span class="xaml__tag_start">&lt;StackLayout</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Entry</span>&nbsp;<span class="xaml__attr_name">Placeholder</span>=<span class="xaml__attr_value">&quot;Username&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Entry</span>&nbsp;<span class="xaml__attr_name">IsPassword</span>=<span class="xaml__attr_value">&quot;true&quot;</span>&nbsp;<span class="xaml__attr_name">Placeholder</span>=<span class="xaml__attr_value">&quot;Password&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Login&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Google&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;GOOGLE.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;FaceBook&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;FACEBOOK.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Twitter&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;TWITTER.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Github&quot;</span>&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;GITHUB.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Yahoo&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;YAHOO.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;DropBox&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;DROPBOX.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;LinkedIn&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;LINKEDIN.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;50&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Flicker&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;FLICKER.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;40&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Button</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Twitter&quot;</span>&nbsp;&nbsp;<span class="xaml__attr_name">Clicked</span>=<span class="xaml__attr_value">&quot;LoginClick&quot;</span>&nbsp;<span class="xaml__attr_name">Image</span>=<span class="xaml__attr_value">&quot;MICROSOFT.png&quot;</span>&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;40&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;<span class="xaml__tag_end">&lt;/StackLayout&gt;</span>&nbsp;
&nbsp;
<span class="xaml__tag_end">&lt;/ContentPage&gt;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong><span style="font-size:medium">&nbsp;LoginPage.Xaml.CS</span></strong></div>
<p><span style="font-size:small">Add LoginClick event in login page code behind file and sender object will return button text name (eg:Facebook,Twitter..etc)</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Xamarin.Forms;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;DevEnvExeLogin&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;LoginPage&nbsp;:&nbsp;ContentPage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;LoginPage()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;LoginClick(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Button&nbsp;btncontrol&nbsp;=&nbsp;(Button)sender;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;providername&nbsp;=&nbsp;btncontrol.Text;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(OAuthConfig.User&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Navigation.PushModalAsync(<span class="cs__keyword">new</span>&nbsp;ProviderLoginPage(providername));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Need&nbsp;to&nbsp;create&nbsp;ProviderLoginPage&nbsp;so&nbsp;follow&nbsp;Step&nbsp;4&nbsp;and&nbsp;Step&nbsp;5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<h1 class="endscriptcode"><span style="font-size:large"><strong>Step 4: Create Identity Provider Login Page</strong></span></h1>
<p><span style="font-size:small">As we will be having platform specific&nbsp;LoginPage&nbsp;implementation of&nbsp;Xamarin.Auth, we don't need any specific implementation in the portable project.</span></p>
<p dir="ltr"><span style="font-size:small">We do need to add an empty&nbsp;ProviderLoginPage&nbsp;which will be resolved at runtime and substituted by actual implementation regarding this will explain on step 5</span></p>
<p dir="ltr"><span style="font-size:small">Right Click Portable Project ➔ Add New Item ➔Select Xaml page (ProviderLoginPage.Xaml&nbsp;)</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Xamarin.Forms;&nbsp;
&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;DevEnvExeLogin&nbsp;
&nbsp;
{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;ProviderLoginPage&nbsp;:&nbsp;ContentPage&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//we&nbsp;will&nbsp;refer&nbsp;providename&nbsp;from&nbsp;renderer&nbsp;page</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;&nbsp;ProviderName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ProviderLoginPage(<span class="cs__keyword">string</span>&nbsp;_providername)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProviderName&nbsp;=&nbsp;_providername;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
}&nbsp;
&nbsp;</pre>
</div>
</div>
</div>
<p dir="ltr"><span>** In Xaml page no Changes </span></p>
<h1 dir="ltr"><span style="font-size:large">Step 5: Create Platform Specific Login Renderer</span></h1>
<p dir="ltr"><span><img src="https://lh5.googleusercontent.com/-5MaMcW8qi5nz35lYJzO6tx2MuAFl4JieJIfSYTw4R105noq8x2cUnfRsl5UWb9JTSBJ8zSNK54z_HdWMlQn0mC_J3NO-3s0DIS09PUYSZSlLfi_d2k1avXR2erNm7eFxYuOSXPR2zE9yc85cg" alt="" width="351" height="501"></span></p>
<p dir="ltr"><span style="font-size:small">We need to create platform specific LoginRenderer Page so you can create platform specific Login page (loginRenderer.CS) to iOS, Android and UWP project.</span></p>
<p dir="ltr"><span style="font-size:small">We need to add&nbsp;LoginPageRenderer which will be used by&nbsp;Xamarin.Auth&nbsp;to display web view for OAuth Login Page</span></p>
<h1 dir="ltr"><span style="font-size:large">Code Snippet Explanation:</span></h1>
<p dir="ltr"><span style="font-size:small">The below code is Xamarin.Forms DependencyService which maps ProviderLoginPage to LoginRenderer.</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">[assembly:&nbsp;ExportRenderer(<span class="cs__keyword">typeof</span>(ProviderLoginPage),&nbsp;<span class="cs__keyword">typeof</span>(LoginRenderer))]</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;Get Identity ProviderName from Providerloginpage</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;var&nbsp;loginPage&nbsp;=&nbsp;Element&nbsp;<span class="cs__keyword">as</span>&nbsp;ProviderLoginPage;&nbsp;
&nbsp;
&nbsp;<span class="cs__keyword">string</span>&nbsp;providername&nbsp;=&nbsp;loginPage.ProviderName;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;Create OauthProviderSetting class from Portable Class Library with Oauth Implementation, regarding this I have explained in Step 6.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//Create&nbsp;OauthProviderSetting&nbsp;class&nbsp;with&nbsp;Oauth&nbsp;Implementation&nbsp;.Refer&nbsp;Step&nbsp;6</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthProviderSetting&nbsp;oauth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuthProviderSetting();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;auth&nbsp;=&nbsp;oauth.LoginWithProvider(providername);</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;Create Oauth event for provider login completed and canceled.</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">auth.Completed&nbsp;&#43;=&nbsp;(sender,&nbsp;eventArgs)&nbsp;=&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(eventArgs.IsAuthenticated)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__com">//Login&nbsp;Success&nbsp;&nbsp;&nbsp;}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;The&nbsp;user&nbsp;cancelled</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;If you want get and save user info. You can create UserEntity from Portable library and refer below code</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;DevEnvExeLogin&nbsp;
&nbsp;
{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;UserDetails&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;TwitterId&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Name&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;ScreenName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Token&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;TokenSecret&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsAuthenticated&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;!<span class="cs__keyword">string</span>.IsNullOrWhiteSpace(Token);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong><span style="font-size:medium">&nbsp;LoginRenderer.CS</span></strong></div>
<h1 class="endscriptcode"><strong><span style="font-size:medium">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Android.App;&nbsp;
&nbsp;
<span class="cs__keyword">using</span>&nbsp;Xamarin.Forms.Platform.Android;&nbsp;
&nbsp;
<span class="cs__keyword">using</span>&nbsp;DevEnvExeLogin;&nbsp;
&nbsp;
<span class="cs__keyword">using</span>&nbsp;Xamarin.Forms;&nbsp;
&nbsp;
<span class="cs__keyword">using</span>&nbsp;DevEnvExeLogin.Droid.PageRender;&nbsp;
&nbsp;
&nbsp;
[assembly:&nbsp;ExportRenderer(<span class="cs__keyword">typeof</span>(ProviderLoginPage),&nbsp;<span class="cs__keyword">typeof</span>(LoginRenderer))]&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;DevEnvExeLogin.Droid.PageRender&nbsp;
&nbsp;
{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">class</span>&nbsp;LoginRenderer&nbsp;:&nbsp;PageRenderer&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;showLogin&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span><span class="cs__keyword">override</span><span class="cs__keyword">void</span>&nbsp;OnElementChanged(ElementChangedEventArgs&lt;Page&gt;&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">base</span>.OnElementChanged(e);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Get&nbsp;and&nbsp;Assign&nbsp;ProviderName&nbsp;from&nbsp;ProviderLoginPage</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;loginPage&nbsp;=&nbsp;Element&nbsp;<span class="cs__keyword">as</span>&nbsp;ProviderLoginPage;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;providername&nbsp;=&nbsp;loginPage.ProviderName;&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;activity&nbsp;=&nbsp;<span class="cs__keyword">this</span>.Context&nbsp;<span class="cs__keyword">as</span>&nbsp;Activity;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(showLogin&nbsp;&amp;&amp;&nbsp;OAuthConfig.User&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showLogin&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Create&nbsp;OauthProviderSetting&nbsp;class&nbsp;with&nbsp;Oauth&nbsp;Implementation&nbsp;.Refer&nbsp;Step&nbsp;6</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthProviderSetting&nbsp;oauth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuthProviderSetting();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;auth&nbsp;=&nbsp;oauth.LoginWithProvider(providername);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;After&nbsp;facebook,google&nbsp;and&nbsp;all&nbsp;identity&nbsp;provider&nbsp;login&nbsp;completed</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;auth.Completed&nbsp;&#43;=&nbsp;(sender,&nbsp;eventArgs)&nbsp;=&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(eventArgs.IsAuthenticated)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthConfig.User&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;UserDetails();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;and&nbsp;Save&nbsp;User&nbsp;Details</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthConfig.User.Token&nbsp;=&nbsp;eventArgs.Account.Properties[<span class="cs__string">&quot;oauth_token&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthConfig.User.TokenSecret&nbsp;=&nbsp;eventArgs.Account.Properties[<span class="cs__string">&quot;oauth_token_secret&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthConfig.User.TwitterId&nbsp;=&nbsp;eventArgs.Account.Properties[<span class="cs__string">&quot;user_id&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthConfig.User.ScreenName&nbsp;=&nbsp;eventArgs.Account.Properties[<span class="cs__string">&quot;screen_name&quot;</span>];&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OAuthConfig.SuccessfulLoginAction.Invoke();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;The&nbsp;user&nbsp;cancelled</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;activity.StartActivity(auth.GetUI(activity));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
}<strong style="font-size:2em">&nbsp;</strong></pre>
</div>
</div>
</div>
</span></strong></h1>
<p><span style="font-size:x-small"><strong style="font-size:2em">Step 7: OAuth Implementation</strong></span></p>
<p dir="ltr"><span style="font-size:small">The&nbsp;OAuth2Authenticator&nbsp;class is responsible for managing the user interface and communicating with authentication services. It will support all the identity provider
</span></p>
<p dir="ltr"><span style="font-size:small">But In Twitter Oauth Authentication will support only on OAuth1Authenticator&nbsp;so you can use OAuth1Authenticator&nbsp;instead of&nbsp;OAuth2Authenticator.
</span></p>
<p dir="ltr"><span style="font-size:small">The&nbsp;OAuth2Authenticator&nbsp;and OAuth1Authenticator&nbsp;class requires a number of parameters, as shown in the following list</span></p>
<ul>
<li><span style="font-size:small">Client ID&nbsp;&ndash; Identity provider client ID, while register app you will unique client ID.</span>
</li><li><span style="font-size:small">Client Secret&nbsp;&ndash;identifies the client that is making the request. while register app you will unique client secret</span>
</li><li><span style="font-size:small">Scope&nbsp;&ndash; this identifies the API access being requested by the application, and the value informs the consent screen that is shown to the user. For more information about scopes,</span>
</li><li><span style="font-size:small">Authorize URL&nbsp;&ndash; this identifies the URL where the authorization code will be obtained from.</span>
</li><li><span style="font-size:small">Redirect URL&nbsp;&ndash; this identifies the URL where the response will be sent. The value of this parameter must match one of the values that appears in the Credentials page for the project.</span>
</li><li><span style="font-size:small">AccessToken Url&nbsp;&mdash; this identifies the URL used to request access tokens after an authorization code is obtained.</span>
</li></ul>
<h1><span style="font-size:large">Step 7.1: Access GOOGLE Account</span></h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Google&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientId&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientSecret&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Below&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;https://www.googleapis.com/auth/userinfo.email&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://accounts.google.com/o/oauth2/auth&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.devenvexe.com&quot;</span>),<span class="cs__com">//&nbsp;Set&nbsp;this&nbsp;property&nbsp;to&nbsp;the&nbsp;location&nbsp;the&nbsp;user&nbsp;will&nbsp;be&nbsp;redirected&nbsp;too&nbsp;after&nbsp;successfully&nbsp;authenticating</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://accounts.google.com/o/oauth2/token&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<h1><span style="font-size:large">&nbsp;Step 7.2: Access FACEBOOK Account:</span></h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientId:&nbsp;<span class="cs__string">&quot;MyAppId&quot;</span>,&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Facebook&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scope:&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;authorizeUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://m.facebook.com/dialog/oauth/&quot;</span>),&nbsp;<span class="cs__com">//&nbsp;These&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;redirectUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.facebook.com/connect/login_success.html&quot;</span>)<span class="cs__com">//&nbsp;These&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<h1 class="endscriptcode"><span style="font-size:large">&nbsp;<strong>Step 7.3: Access TWITTER Account:</strong></span></h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;Twitterauth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth1Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;consumerKey:&nbsp;<span class="cs__string">&quot;*****&quot;</span>,&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Twitter&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;consumerSecret:&nbsp;<span class="cs__string">&quot;****&quot;</span>,&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Twitter&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;requestTokenUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://api.twitter.com/oauth/request_token&quot;</span>),&nbsp;<span class="cs__com">//&nbsp;These&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;authorizeUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://api.twitter.com/oauth/authorize&quot;</span>),&nbsp;<span class="cs__com">//&nbsp;These&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;accessTokenUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://api.twitter.com/oauth/access_token&quot;</span>),&nbsp;<span class="cs__com">//&nbsp;These&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callbackUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.devenvexe.com&quot;</span>)&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Set&nbsp;this&nbsp;property&nbsp;to&nbsp;the&nbsp;location&nbsp;the&nbsp;user&nbsp;will&nbsp;be&nbsp;redirected&nbsp;too&nbsp;after&nbsp;successfully&nbsp;authenticating</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<h1 class="endscriptcode"><span style="font-size:large"><strong>&nbsp;Step 7.4 Access Microsoft Account:</strong></span></h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientId:&nbsp;<span class="cs__string">&quot;MY&nbsp;ID&quot;</span>,&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Micrsoft&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;http://www.c-sharpcorner.com/article/register-identity-provider-for-new-oauth-application/</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scope:&nbsp;<span class="cs__string">&quot;bingads.manage&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;authorizeUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://login.live.com/oauth20_authorize.srf?client_id=myid&amp;scope=bingads.manage&amp;response_type=token&amp;redirect_uri=https://login.live.com/oauth20_desktop.srf&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;redirectUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://adult-wicareerpathways-dev.azurewebsites.net/Account/ExternalLoginCallback&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<h1 class="endscriptcode"><span style="font-size:large">&nbsp;<strong>Step 7.5 Access LINKEDIN Account:</strong></span></h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientId:&nbsp;<span class="cs__string">&quot;**&quot;</span>,<span class="cs__com">//&nbsp;For&nbsp;LinkedIN&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientSecret:&nbsp;<span class="cs__string">&quot;**&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scope:&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;authorizeUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://www.linkedin.com/uas/oauth2/authorization&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;redirectUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://devenvexe.com/&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;accessTokenUrl:&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://www.linkedin.com/uas/oauth2/accessToken&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong>&nbsp;<span style="font-size:2em">Step 7.6 Access GITHUB Account:</span></strong></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;GITHUB&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientId&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientSecret&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Below&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://github.com/login/oauth/authorize&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.devenvexe.com&quot;</span>),<span class="cs__com">//&nbsp;Set&nbsp;this&nbsp;property&nbsp;to&nbsp;the&nbsp;location&nbsp;the&nbsp;user&nbsp;will&nbsp;be&nbsp;redirected&nbsp;too&nbsp;after&nbsp;successfully&nbsp;authenticating</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://github.com/login/oauth/access_token&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong>&nbsp;<span style="font-size:2em">Step 7.7 Access FLICKER Account:</span></strong></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Flicker&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientId&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientSecret&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Below&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://www.flickr.com/services/oauth/request_token&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.devenvexe.com&quot;</span>),<span class="cs__com">//&nbsp;Set&nbsp;this&nbsp;property&nbsp;to&nbsp;the&nbsp;location&nbsp;the&nbsp;user&nbsp;will&nbsp;be&nbsp;redirected&nbsp;too&nbsp;after&nbsp;successfully&nbsp;authenticating</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.flickr.com/services/oauth/access_token&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">&nbsp;Step 7.8 Access Yahoo Account:</h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;auth&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OAuth2Authenticator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;For&nbsp;Yahoo&nbsp;login,&nbsp;for&nbsp;configure&nbsp;refer&nbsp;https://code.msdn.microsoft.com/Register-Identity-Provider-41955544</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientId&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;ClientSecret&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Below&nbsp;values&nbsp;do&nbsp;not&nbsp;need&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://api.login.yahoo.com/oauth2/request_auth&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://www.devenvexe.com&quot;</span>),<span class="cs__com">//&nbsp;Set&nbsp;this&nbsp;property&nbsp;to&nbsp;the&nbsp;location&nbsp;the&nbsp;user&nbsp;will&nbsp;be&nbsp;redirected&nbsp;too&nbsp;after&nbsp;successfully&nbsp;authenticating</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;https://api.login.yahoo.com/oauth2/get_token&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">&nbsp;You can download source and replace client ID or AppID and Client secret</span></div>
<p>&nbsp;</p>
<p dir="ltr"><span><img src="https://lh5.googleusercontent.com/B2_8dc1U1v_3e9YF1ALsN0RG7wZYcbrO0hHy8sEV1Z_VzdBBcwRIIuiXj5hA1w071_3Di6hl5-9vZG6yX0pPLjlNcAu5LzIxFuBpWz-62MXovdJNjqqfGMiJyaSe9boBM6JpvgvbzeNdZAXSQg" alt="" width="206" height="397"></span><span><img src="https://lh3.googleusercontent.com/1jIG9R0qmO_DPABO53bhM098B228uD1MVTQ4mZfp-GqS8xXePeBe_5wFqS2jKurr2jAGfdANTLsOvd3JdvC3XAEMRnIQgQKWM6_3jvmdSDCpwRCyMz-L6jdd6qanqPcEa5H_YhPQw00SBW-iWA" alt="C:\Users\Suthahar\Pictures\twitter.PNG" width="206" height="401"></span><span><img src="https://lh4.googleusercontent.com/tgxaroVE1h-eQDvGUxfiQznJg64snpOuKs_UotqRUi6erqIrD_Xq_D9yVirqk2YDdKwVd0SWCJPdSzNyQYVKmuhyceClLxRgepXNvNQWY6oDVt59Xt4op_ToDhNlL7oIEnrauRm8zfxIpspM7A" alt="C:\Users\Suthahar\Pictures\twitterresult.PNG" width="198" height="402"></span></p>
<div><span><br>
</span></div>
