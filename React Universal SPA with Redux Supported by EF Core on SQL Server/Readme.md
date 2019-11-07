# React Universal SPA with Redux Supported by EF Core on SQL Server
## Requires
- Visual Studio 2017
## License
- Apache License, Version 2.0
## Technologies
- SQL Server
- ReactJS
- ASP.NET Core
- EF Core
- Redux
## Topics
- Data Access
- Test Driven Development
- Database
- Web API
- Universal Web App
## Updated
- 03/13/2019
## Description

<h1>Introduction</h1>
<p><span style="font-size:xx-small">This ski shop web app is like most online stores that have&nbsp;</span></p>
<ol>
<li><span style="font-size:xx-small">An authentication function for login/logout.
</span></li><li><span style="font-size:xx-small">A navigation system where customers can browse skis with page controls filtered by category, brand, gender, what they are ideal for and sorted by price, check their descriptions, tech specs and reviews, as well as add their
 own reviews.</span> </li><li><span style="font-size:xx-small">A shopping cart where customers can add, remove skis and update quantities</span>
</li><li><span style="font-size:xx-small">A checkout system where users or guests can fill in their shipping details, place their orders, or get messages if any item is sold out or over stock.</span>
</li><li><span style="font-size:xx-small">An order system where users or guests can review their orders.</span>
</li></ol>
<p><span style="font-size:xx-small">On the backend, we use</span></p>
<ul>
<li><span style="font-size:xx-small">T-SQL to create a database on the MS SQL Server 2016 (LocalDB in this sample, also works on Express or other versions) and to seed its data.
</span></li><li><span style="font-size:xx-small">EF Core to query the database and map the results back to C# models.
</span></li><li><span style="font-size:xx-small">ASP.NET Core to create a Web API that passes data between the server and client sides.</span>
</li><li><span style="font-size:xx-small">ASP.NET Core Identity to authenticate users</span>
</li><li><span style="font-size:xx-small">SSDT SQL Server unit tests on SQL Server</span>
</li><li><span style="font-size:xx-small">XUnit to test data access and Web API</span>
</li></ul>
<p><span style="font-size:xx-small">The client-side ES6 code is bundled by webpack for the development environment which covers:</span></p>
<ul>
<li><span style="font-size:xx-small">Component-based UI</span> </li><li><span style="font-size:xx-small">Navigation system with navigation history management</span>
</li><li><span style="font-size:xx-small">App state management</span> </li><li><span style="font-size:xx-small">API result cache and async data flow </span>
</li><li><span style="font-size:xx-small">Responsive design for desktop, tablet and mobile screens
</span></li><li><span style="font-size:xx-small">Type checking</span> </li><li><span style="font-size:xx-small">Dynamically<strong> </strong>loading components and packages</span>
</li><li><span style="font-size:xx-small">Hot Module Replacement (HMR)</span> </li></ul>
<p>&nbsp;</p>
<h1>Prerequirements:</h1>
<ul>
<li><span style="font-size:xx-small">Visual Studio 2017</span> </li><li><span style="font-size:xx-small">VS Code (recommended)</span> </li><li><span style="font-size:xx-small">SQL Server 2016 or up (LocalDB, Express or other versions)</span>
</li><li><span style="font-size:xx-small">Node.js</span> </li><li><span style="font-size:xx-small">ASP.Net Core 2.2.0 or up</span> </li></ul>
<h1>Running the Sample</h1>
<p><span style="font-size:xx-small">It will take a while for VS2017 to automatically install all the npm and .NET dependencies when you first open the app. &nbsp;</span></p>
<p><span style="font-size:xx-small">Step 1: publish the database (and the test database)</span></p>
<p><span style="font-size:xx-small">The publish profiles in the folder SkiShopSQL-&gt;Publishes &nbsp;work for SQL Server 2016 LocalDB. Please update the data source if you use SQL Server 2016 Express or other versions.</span></p>
<p><span style="font-size:xx-small"><img id="220170" src="220170-publish_db_0.png" alt="" width="600" height="319"></span></p>
<p><span style="font-size:xx-small">Step 2:&nbsp;Create the identity database using NuGet Package Manager Console&nbsp;</span></p>
<p><span style="font-size:xx-small">Please select &ldquo;WebApiEfCore&rdquo; as the Starup project first.</span></p>
<p><img id="220171" src="220171-pmc_auth_1.png" alt="" width="612" height="179"></p>
<p><span style="font-size:xx-small">Step 3: Make webpack bundles</span></p>
<p><span style="font-size:xx-small">TOOLS -&gt; Node.js Tools -&gt; Node.js Interactive Window</span></p>
<p><img id="220172" src="220172-npm_webpack_0.png" alt="" width="600" height="105"></p>
<p><span style="font-size:xx-small">Please check package.json for more scripts for bundle analysis and type checking.</span></p>
<p><span style="font-size:xx-small">Step 4 Run testing code in Test Explorer</span></p>
<p><span style="font-size:xx-small">All tests point to the test database.</span></p>
<p><img id="220173" src="220173-test_results_0.png" alt="" width="300" height="263"></p>
<p><span style="font-size:xx-small"><br>
</span></p>
<p><object width="350" height="300" data="data:application/x-silverlight-2," type="application/x-silverlight-2"> <param name="source" value="/Content/Common/videoplayer.xap" /> <param name="initParams" value="deferredLoad=false,duration=0,m=https://i1.code.msdn.s-msft.com/react-redux-spa-on-asp-net-e910901f/image/file/220179/1/skishopreactefcore.wmv,autostart=false,autohide=true,showembed=true"
 /> <param name="background" value="#00FFFFFF" /> <param name="minRuntimeVersion" value="3.0.40624.0" /> <param name="enableHtmlAccess" value="true" /> <param name="src" value="https://i1.code.msdn.s-msft.com/react-redux-spa-on-asp-net-e910901f/image/file/220179/1/skishopreactefcore.wmv"
 /> <param name="id" value="220179" /> <param name="name" value="SkiShopReactEFCore.wmv" /><span><a href="http://go.microsoft.com/fwlink/?LinkID=149156" style="text-decoration:none"><img src="-?linkid=108181" alt="Get Microsoft Silverlight" style="border-style:none"></a></span>
 </object> <br>
<span style="font-size:xx-small"><a id="https://i1.code.msdn.s-msft.com/react-redux-spa-on-asp-net-e910901f/image/file/220179/1/skishopreactefcore.wmv" href="https://i1.code.msdn.s-msft.com/react-redux-spa-on-asp-net-e910901f/image/file/220179/1/skishopreactefcore.wmv">Download
 video</a></span></p>
<p><span style="font-size:xx-small">or go to</span>&nbsp;</p>
<p><span style="font-size:xx-small"><a href="https://www.youtube.com/watch?v=laXNkReBlDo&t=40s">https://www.youtube.com/watch?v=laXNkReBlDo&amp;t=40s</a></span></p>
<p>&nbsp;</p>
<p><span style="font-size:xx-small">Pictures</span></p>
<p>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<img id="220181" src="220181-filter_mobile_1.gif" alt="" width="123" height="248">&nbsp;
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<img id="220182" src="220182-skis_mobile_2.gif" alt="" width="122" height="248"></p>
<p>&nbsp; &nbsp;&nbsp;<img id="220183" src="220183-home_mobile_0.png" alt="" width="120" height="247">&nbsp; &nbsp; &nbsp;&nbsp;<img id="220184" src="220184-login_mobile_0.png" alt="" width="120" height="247">&nbsp;
 &nbsp; &nbsp;&nbsp;<img id="220185" src="220185-orderhistory_mobile_0.png" alt="" width="120" height="247">&nbsp; &nbsp; &nbsp;&nbsp;<img id="220186" src="220186-orderdetail_mobile_1.png" alt="" width="120" height="247"></p>
<p><img id="220187" src="220187-combine_images_1.png" alt="" width="612" height="349"></p>
<p><img id="220188" src="220188-combine_images_2.png" alt="" width="612" height="350"></p>
<p>&nbsp;</p>
<h1>Sample Codes:</h1>
<p><span style="font-size:xx-small">Login.jsx</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';

import FormErrorMsg from '../../viewComponents/formErrorMsg/FormErrorMsg';
import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';

class Login extends React.Component {
    constructor(props) {
        super(props);
    }

    redirectToLastPage() {
        const { user, history } = this.props;

        if (user &amp;&amp; user.userId &gt; 0) history.goBack();
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory } = this.props;

        selectDefaultCategory(selectedCategoryId, selectCategory);

        this.redirectToLastPage();
    }

    componentDidUpdate() {
        this.redirectToLastPage();
    }

    render() {
        const { user, login} = this.props;

        return (
            &lt;div className=&quot;container h-100 mt-5 border align-content-center&quot;&gt;
                &lt;h4 className=&quot;mb-2&quot;&gt;Log in&lt;/h4&gt;
                &lt;Formik
                    initialValues={{
                        email: '',
                        password: ''
                    }}
                    validationSchema={loginSchema}
                    onSubmit={(values, actions) =&gt; {
                        const loginModel = {
                            email: values.email,
                            password: values.password,
                            rememberMe: true,
                            lockoutFailure: true
                        }

                        login(loginModel);

                        actions.setSubmitting(false);
                    }}
                    render={({ status, isSubmitting }) =&gt; (
                        &lt;Form&gt;
                            &lt;FieldGroup labelName=&quot;Email&quot; fieldName=&quot;email&quot; fieldType=&quot;email&quot; /&gt;
                            &lt;FieldGroup labelName=&quot;Password&quot; fieldName=&quot;password&quot; fieldType=&quot;password&quot; /&gt;
                            {status &amp;&amp; status.msg &amp;&amp; &lt;div className=&quot;text-danger&quot;&gt;{status.msg}&lt;/div&gt;}
                            {user &amp;&amp; user.userId === -1 &amp;&amp; &lt;div className=&quot;text-danger mb-3&quot;&gt;{user.screenName}&lt;/div&gt;}
                            &lt;button type=&quot;submit&quot; className=&quot;btn btn-primary mb-3&quot; disabled={isSubmitting}&gt;
                                Submit
                            &lt;/button&gt;
                        &lt;/Form&gt;
                    )
                    }
                &gt;
                &lt;/Formik&gt;
            &lt;/div&gt;
        );
    }
} 

const loginSchema = Yup.object().shape({
    email: Yup.string()
        .email('Invalid email')
        .required('Required'),
    password: Yup.string()
        .required('Required')
});

const FieldGroup = ({ labelName, fieldName, fieldType }) =&gt; (
    &lt;div className=&quot;form-group&quot;&gt;
        &lt;strong&gt;{labelName}&lt;sup&gt;*&lt;/sup&gt;&lt;/strong&gt;
        &lt;Field name={fieldName} type={fieldType} className=&quot;form-control&quot; /&gt;
        &lt;FormErrorMsg name={fieldName} /&gt;
    &lt;/div&gt;
);

export default Login;</pre>
<div class="preview">
<pre class="js">import&nbsp;React&nbsp;from&nbsp;<span class="js__string">'react'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;Formik,&nbsp;Form,&nbsp;Field&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'formik'</span>;&nbsp;
import&nbsp;*&nbsp;as&nbsp;Yup&nbsp;from&nbsp;<span class="js__string">'yup'</span>;&nbsp;
&nbsp;
import&nbsp;FormErrorMsg&nbsp;from&nbsp;<span class="js__string">'../../viewComponents/formErrorMsg/FormErrorMsg'</span>;&nbsp;
import&nbsp;selectDefaultCategory&nbsp;from&nbsp;<span class="js__string">'../../reduxStore/helpers/selectDefaultCategory'</span>;&nbsp;
&nbsp;
class&nbsp;Login&nbsp;extends&nbsp;React.Component&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;constructor(props)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;super(props);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;redirectToLastPage()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">const</span>&nbsp;<span class="js__brace">{</span>&nbsp;user,&nbsp;history&nbsp;<span class="js__brace">}</span>&nbsp;=&nbsp;<span class="js__operator">this</span>.props;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(user&nbsp;&amp;&amp;&nbsp;user.userId&nbsp;&gt;&nbsp;<span class="js__num">0</span>)&nbsp;history.goBack();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;componentDidMount()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">const</span>&nbsp;<span class="js__brace">{</span>&nbsp;selectedCategoryId,&nbsp;selectCategory&nbsp;<span class="js__brace">}</span>&nbsp;=&nbsp;<span class="js__operator">this</span>.props;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selectDefaultCategory(selectedCategoryId,&nbsp;selectCategory);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.redirectToLastPage();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;componentDidUpdate()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.redirectToLastPage();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;render()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">const</span>&nbsp;<span class="js__brace">{</span>&nbsp;user,&nbsp;login<span class="js__brace">}</span>&nbsp;=&nbsp;<span class="js__operator">this</span>.props;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;className=<span class="js__string">&quot;container&nbsp;h-100&nbsp;mt-5&nbsp;border&nbsp;align-content-center&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;h4&nbsp;className=<span class="js__string">&quot;mb-2&quot;</span>&gt;Log&nbsp;<span class="js__operator">in</span>&lt;/h4&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;Formik&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;initialValues=<span class="js__brace">{</span><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;email:&nbsp;<span class="js__string">''</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;password:&nbsp;<span class="js__string">''</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validationSchema=<span class="js__brace">{</span>loginSchema<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onSubmit=<span class="js__brace">{</span>(values,&nbsp;actions)&nbsp;=&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">const</span>&nbsp;loginModel&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;email:&nbsp;values.email,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;password:&nbsp;values.password,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rememberMe:&nbsp;true,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lockoutFailure:&nbsp;true&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;login(loginModel);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;actions.setSubmitting(false);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;render=<span class="js__brace">{</span>(<span class="js__brace">{</span>&nbsp;status,&nbsp;isSubmitting&nbsp;<span class="js__brace">}</span>)&nbsp;=&gt;&nbsp;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;Form&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;FieldGroup&nbsp;labelName=<span class="js__string">&quot;Email&quot;</span>&nbsp;fieldName=<span class="js__string">&quot;email&quot;</span>&nbsp;fieldType=<span class="js__string">&quot;email&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;FieldGroup&nbsp;labelName=<span class="js__string">&quot;Password&quot;</span>&nbsp;fieldName=<span class="js__string">&quot;password&quot;</span>&nbsp;fieldType=<span class="js__string">&quot;password&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>status&nbsp;&amp;&amp;&nbsp;status.msg&nbsp;&amp;&amp;&nbsp;&lt;div&nbsp;className=<span class="js__string">&quot;text-danger&quot;</span>&gt;<span class="js__brace">{</span>status.msg<span class="js__brace">}</span>&lt;/div&gt;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>user&nbsp;&amp;&amp;&nbsp;user.userId&nbsp;===&nbsp;-<span class="js__num">1</span>&nbsp;&amp;&amp;&nbsp;&lt;div&nbsp;className=<span class="js__string">&quot;text-danger&nbsp;mb-3&quot;</span>&gt;<span class="js__brace">{</span>user.screenName<span class="js__brace">}</span>&lt;/div&gt;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;button&nbsp;type=<span class="js__string">&quot;submit&quot;</span>&nbsp;className=<span class="js__string">&quot;btn&nbsp;btn-primary&nbsp;mb-3&quot;</span>&nbsp;disabled=<span class="js__brace">{</span>isSubmitting<span class="js__brace">}</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Submit&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/button&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/Form&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/Formik&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;&nbsp;
&nbsp;
<span class="js__statement">const</span>&nbsp;loginSchema&nbsp;=&nbsp;Yup.object().shape(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;email:&nbsp;Yup.string()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.email(<span class="js__string">'Invalid&nbsp;email'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.required(<span class="js__string">'Required'</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;password:&nbsp;Yup.string()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.required(<span class="js__string">'Required'</span>)&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
<span class="js__statement">const</span>&nbsp;FieldGroup&nbsp;=&nbsp;(<span class="js__brace">{</span>&nbsp;labelName,&nbsp;fieldName,&nbsp;fieldType&nbsp;<span class="js__brace">}</span>)&nbsp;=&gt;&nbsp;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;div&nbsp;className=<span class="js__string">&quot;form-group&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;strong&gt;<span class="js__brace">{</span>labelName<span class="js__brace">}</span>&lt;sup&gt;*&lt;<span class="js__reg_exp">/sup&gt;&lt;/</span>strong&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;Field&nbsp;name=<span class="js__brace">{</span>fieldName<span class="js__brace">}</span>&nbsp;type=<span class="js__brace">{</span>fieldType<span class="js__brace">}</span>&nbsp;className=<span class="js__string">&quot;form-control&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;FormErrorMsg&nbsp;name=<span class="js__brace">{</span>fieldName<span class="js__brace">}</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/div&gt;&nbsp;
);&nbsp;
&nbsp;
export&nbsp;<span class="js__statement">default</span>&nbsp;Login;</pre>
</div>
</div>
</div>
<div class="endscriptcode">
<h1><strong>RoadMap</strong></h1>
<ul>
<li><span style="font-size:xx-small">Turn it into a progressive web app</span> </li><li><span style="font-size:xx-small">Add Jest testing on the client side</span> </li><li><span style="font-size:xx-small">Improve type checking on the client side</span>
</li></ul>
&nbsp;</div>
<div class="endscriptcode"></div>
<h1 class="endscriptcode">Updates:</h1>
<p><span style="font-size:xx-small">March 7, 2019</span></p>
<p><span style="font-size:xx-small">Upgrade to V2</span></p>
<p><span style="font-size:xx-small">August 22 2017:</span></p>
<p><span style="font-size:xx-small">Fixed StylesByCategory.jsxWhen no genders/idealFors are selected, they will be processed as ALL are selected.</span></p>
<p><span style="font-size:xx-small">November 22 2017</span></p>
<p><span style="font-size:xx-small">Fixed 404 error when refreshing or manually writing route.</span></p>
