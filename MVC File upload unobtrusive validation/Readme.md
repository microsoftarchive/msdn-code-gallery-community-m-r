# MVC File upload unobtrusive validation
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- ASP.NET MVC 5
- jQuery Unobtrusive Validation
## Topics
- jQuery Unobtrusive Validation
- MVC File upload
## Updated
- 09/12/2016
## Description

<h1>Introduction</h1>
<p><em>Validate file upload (HttpPostedFileBase) with ValidationAttributes</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>The solution contains four MVC server side validators that validates a HttpPostedFileBase minimum file size(MinimumFileSizeValidator), maximum file size(MaximumFileSizeValidator) and the file extension respectively(ValidFileTypeValidator). The last validator
 is a combination of the mentioned validators(FileUploadValidator).</em></p>
<p><em>The solution also includes the client side validators.</em></p>
<p><em>Each validator overwrites the IsValid Method of the ValidationAttribute class and implements their specific logic to perform the validation.</em></p>
<p><em><br>
To enable client side validation each validator implements IClientValidatable.<br>
</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public class MinimumFileSizeValidator
        : ValidationAttribute, IClientValidatable
    {        
        private string _errorMessage = &quot;{0} can not be smaller than {1} MB&quot;;     

        /// &lt;summary&gt;
        /// Minimum file size in MB
        /// &lt;/summary&gt;
        public double MinimumFileSize { get; private set; }

        /// &lt;param name=&quot;minimumFileSize&quot;&gt;MinimumFileSize file size in MB&lt;/param&gt;
        public MinimumFileSizeValidator(
           double minimumFileSize)
            : base()
        {
            MinimumFileSize = minimumFileSize;
        }

        public override bool IsValid(
             object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!IsValidMinimumFileSize((value as HttpPostedFileBase).ContentLength))
            {
                ErrorMessage = String.Format(_errorMessage, &quot;{0}&quot;, MinimumFileSize);
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return String.Format(_errorMessage, name, MinimumFileSize);
        }

        public IEnumerable&lt;ModelClientValidationRule&gt; GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = &quot;minimumfilesize&quot;
            };

            clientValidationRule.ValidationParameters.Add(&quot;size&quot;, MinimumFileSize);

            return new[] { clientValidationRule };
        }

        private bool IsValidMinimumFileSize(
            int fileSize)
        {
            return ConvertBytesToMegabytes(fileSize) &gt;= MinimumFileSize;
        }

        private double ConvertBytesToMegabytes(
            int bytes)
        {
            return (bytes / 1024f) / 1024f;
        }        
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MinimumFileSizeValidator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;ValidationAttribute,&nbsp;IClientValidatable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;_errorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;{0}&nbsp;can&nbsp;not&nbsp;be&nbsp;smaller&nbsp;than&nbsp;{1}&nbsp;MB&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Minimum&nbsp;file&nbsp;size&nbsp;in&nbsp;MB</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">double</span>&nbsp;MinimumFileSize&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;minimumFileSize&quot;&gt;MinimumFileSize&nbsp;file&nbsp;size&nbsp;in&nbsp;MB&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;MinimumFileSizeValidator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;minimumFileSize)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">base</span>()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MinimumFileSize&nbsp;=&nbsp;minimumFileSize;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsValid(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">object</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">value</span>&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!IsValidMinimumFileSize((<span class="cs__keyword">value</span>&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpPostedFileBase).ContentLength))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;=&nbsp;String.Format(_errorMessage,&nbsp;<span class="cs__string">&quot;{0}&quot;</span>,&nbsp;MinimumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;FormatErrorMessage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;name)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;String.Format(_errorMessage,&nbsp;name,&nbsp;MinimumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;ModelClientValidationRule&gt;&nbsp;GetClientValidationRules(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ModelMetadata&nbsp;metadata&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;ControllerContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientValidationRule&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ModelClientValidationRule()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;&nbsp;&nbsp;=&nbsp;FormatErrorMessage(metadata.GetDisplayName()),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationType&nbsp;=&nbsp;<span class="cs__string">&quot;minimumfilesize&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientValidationRule.ValidationParameters.Add(<span class="cs__string">&quot;size&quot;</span>,&nbsp;MinimumFileSize);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>[]&nbsp;{&nbsp;clientValidationRule&nbsp;};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsValidMinimumFileSize(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;fileSize)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ConvertBytesToMegabytes(fileSize)&nbsp;&gt;=&nbsp;MinimumFileSize;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">double</span>&nbsp;ConvertBytesToMegabytes(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;bytes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;(bytes&nbsp;/&nbsp;1024f)&nbsp;/&nbsp;1024f;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public class MaximumFileSizeValidator
        : ValidationAttribute, IClientValidatable
    {        
        private string _errorMessage = &quot;{0} can not be larger than {1} MB&quot;;        

        /// &lt;summary&gt;
        /// Maximum file size in MB
        /// &lt;/summary&gt;
        public double MaximumFileSize { get; private set; }

        /// &lt;param name=&quot;maximumFileSize&quot;&gt;Maximum file size in MB&lt;/param&gt;
        public MaximumFileSizeValidator(
            double maximumFileSize)
        {
            MaximumFileSize = maximumFileSize;
        }

        public override bool IsValid(
            object value)
        {
            if (value == null) 
            {
                return true;
            }

            if (!IsValidMaximumFileSize((value as HttpPostedFileBase).ContentLength))
            {
                ErrorMessage = String.Format(_errorMessage, &quot;{0}&quot;, MaximumFileSize);
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return String.Format(_errorMessage, name, MaximumFileSize);
        }

        public System.Collections.Generic.IEnumerable&lt;ModelClientValidationRule&gt; GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = &quot;maximumfilesize&quot;
            };

            clientValidationRule.ValidationParameters.Add(&quot;size&quot;, MaximumFileSize);

            return new[] { clientValidationRule };
        }

        private bool IsValidMaximumFileSize(
            int fileSize)
        {
            return (ConvertBytesToMegabytes(fileSize) &lt;= MaximumFileSize);
        }

        private double ConvertBytesToMegabytes(
           int bytes)
        {
            return (bytes / 1024f) / 1024f;
        }   
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;MaximumFileSizeValidator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;ValidationAttribute,&nbsp;IClientValidatable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;_errorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;{0}&nbsp;can&nbsp;not&nbsp;be&nbsp;larger&nbsp;than&nbsp;{1}&nbsp;MB&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Maximum&nbsp;file&nbsp;size&nbsp;in&nbsp;MB</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">double</span>&nbsp;MaximumFileSize&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;maximumFileSize&quot;&gt;Maximum&nbsp;file&nbsp;size&nbsp;in&nbsp;MB&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;MaximumFileSizeValidator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;maximumFileSize)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MaximumFileSize&nbsp;=&nbsp;maximumFileSize;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsValid(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">object</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">value</span>&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!IsValidMaximumFileSize((<span class="cs__keyword">value</span>&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpPostedFileBase).ContentLength))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;=&nbsp;String.Format(_errorMessage,&nbsp;<span class="cs__string">&quot;{0}&quot;</span>,&nbsp;MaximumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;FormatErrorMessage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;name)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;String.Format(_errorMessage,&nbsp;name,&nbsp;MaximumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;System.Collections.Generic.IEnumerable&lt;ModelClientValidationRule&gt;&nbsp;GetClientValidationRules(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ModelMetadata&nbsp;metadata&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;ControllerContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientValidationRule&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ModelClientValidationRule()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;&nbsp;&nbsp;=&nbsp;FormatErrorMessage(metadata.GetDisplayName()),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationType&nbsp;=&nbsp;<span class="cs__string">&quot;maximumfilesize&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientValidationRule.ValidationParameters.Add(<span class="cs__string">&quot;size&quot;</span>,&nbsp;MaximumFileSize);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>[]&nbsp;{&nbsp;clientValidationRule&nbsp;};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsValidMaximumFileSize(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;fileSize)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;(ConvertBytesToMegabytes(fileSize)&nbsp;&lt;=&nbsp;MaximumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">double</span>&nbsp;ConvertBytesToMegabytes(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;bytes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;(bytes&nbsp;/&nbsp;1024f)&nbsp;/&nbsp;1024f;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public class ValidFileTypeValidator
        : ValidationAttribute, IClientValidatable
    {        
        private string _errorMessage = &quot;{0} must be one of the following file types: {1}&quot;;    

        /// &lt;summary&gt;
        /// Valid file extentions
        /// &lt;/summary&gt;
        public string[] ValidFileTypes { get; private set; }

        /// &lt;param name=&quot;validFileTypes&quot;&gt;Valid file extentions(without the dot)&lt;/param&gt;
        public ValidFileTypeValidator(
            params string[] validFileTypes)
        {
            ValidFileTypes = validFileTypes;
        }

        public override bool IsValid(
            object value)
        {
            var file = value as HttpPostedFileBase;

            if (value == null || String.IsNullOrEmpty(file.FileName))
            {
                return true;
            }

            if (ValidFileTypes != null)
            {
                var validFileTypeFound = false;

                foreach (var validFileType in ValidFileTypes)
                {
                    var fileNameParts = file.FileName.Split('.');
                    if (fileNameParts[fileNameParts.Length - 1] == validFileType)
                    {
                        validFileTypeFound = true;
                        break;
                    }
                }

                if (!validFileTypeFound)
                {
                    ErrorMessage = String.Format(_errorMessage, &quot;{0}&quot;, ValidFileTypes.ToConcatenatedString(&quot;,&quot;));
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return String.Format(_errorMessage, name, ValidFileTypes.ToConcatenatedString(&quot;,&quot;));
        }

        public System.Collections.Generic.IEnumerable&lt;ModelClientValidationRule&gt; GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = &quot;validfiletype&quot;
            };

            clientValidationRule.ValidationParameters.Add(&quot;filetypes&quot;, ValidFileTypes.ToConcatenatedString(&quot;,&quot;));

            return new[] { clientValidationRule };
        }
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ValidFileTypeValidator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;ValidationAttribute,&nbsp;IClientValidatable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;_errorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;{0}&nbsp;must&nbsp;be&nbsp;one&nbsp;of&nbsp;the&nbsp;following&nbsp;file&nbsp;types:&nbsp;{1}&quot;</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Valid&nbsp;file&nbsp;extentions</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;ValidFileTypes&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;validFileTypes&quot;&gt;Valid&nbsp;file&nbsp;extentions(without&nbsp;the&nbsp;dot)&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ValidFileTypeValidator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">params</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;validFileTypes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidFileTypes&nbsp;=&nbsp;validFileTypes;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsValid(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">object</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;file&nbsp;=&nbsp;<span class="cs__keyword">value</span>&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpPostedFileBase;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">value</span>&nbsp;==&nbsp;<span class="cs__keyword">null</span>&nbsp;||&nbsp;String.IsNullOrEmpty(file.FileName))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ValidFileTypes&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;validFileTypeFound&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;validFileType&nbsp;<span class="cs__keyword">in</span>&nbsp;ValidFileTypes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;fileNameParts&nbsp;=&nbsp;file.FileName.Split(<span class="cs__string">'.'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(fileNameParts[fileNameParts.Length&nbsp;-&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;validFileType)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validFileTypeFound&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!validFileTypeFound)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;=&nbsp;String.Format(_errorMessage,&nbsp;<span class="cs__string">&quot;{0}&quot;</span>,&nbsp;ValidFileTypes.ToConcatenatedString(<span class="cs__string">&quot;,&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;FormatErrorMessage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;name)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;String.Format(_errorMessage,&nbsp;name,&nbsp;ValidFileTypes.ToConcatenatedString(<span class="cs__string">&quot;,&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;System.Collections.Generic.IEnumerable&lt;ModelClientValidationRule&gt;&nbsp;GetClientValidationRules(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ModelMetadata&nbsp;metadata&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;ControllerContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientValidationRule&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ModelClientValidationRule()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;&nbsp;&nbsp;=&nbsp;FormatErrorMessage(metadata.GetDisplayName()),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationType&nbsp;=&nbsp;<span class="cs__string">&quot;validfiletype&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientValidationRule.ValidationParameters.Add(<span class="cs__string">&quot;filetypes&quot;</span>,&nbsp;ValidFileTypes.ToConcatenatedString(<span class="cs__string">&quot;,&quot;</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>[]&nbsp;{&nbsp;clientValidationRule&nbsp;};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public class FileUploadValidator
        : ValidationAttribute, IClientValidatable
    {   
        private MinimumFileSizeValidator _minimumFileSizeValidator;
        private MaximumFileSizeValidator _maximumFileSizeValidator;
        private ValidFileTypeValidator   _validFileTypeValidator;                 
        
        /// &lt;param name=&quot;validFileTypes&quot;&gt;Valid file extentions(without the dot)&lt;/param&gt;
        public FileUploadValidator(              
              params string[] validFileTypes)
            : base()
        {
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }
        
        /// &lt;param name=&quot;maximumFileSize&quot;&gt;Maximum file size in MB&lt;/param&gt;
        /// &lt;param name=&quot;validFileTypes&quot;&gt;Valid file extentions(without the dot)&lt;/param&gt;
        public FileUploadValidator(              
              double maximumFileSize
            , params string[] validFileTypes)
            : base()
        {
            _maximumFileSizeValidator = new MaximumFileSizeValidator(maximumFileSize);
            _validFileTypeValidator   = new ValidFileTypeValidator(validFileTypes);
        }

        /// &lt;param name=&quot;minimumFileSize&quot;&gt;MinimumFileSize file size in MB&lt;/param&gt;
        /// &lt;param name=&quot;maximumFileSize&quot;&gt;Maximum file size in MB&lt;/param&gt;
        /// &lt;param name=&quot;validFileTypes&quot;&gt;Valid file extentions(without the dot)&lt;/param&gt;
        public FileUploadValidator(
              double minimumFileSize
            , double maximumFileSize
            , params string[] validFileTypes)
            : base()
        {
            _minimumFileSizeValidator = new MinimumFileSizeValidator(minimumFileSize);
            _maximumFileSizeValidator = new MaximumFileSizeValidator(maximumFileSize);
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }

        protected override ValidationResult IsValid(
              object value
            , ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            try
            {
                if (value.GetType() != typeof(HttpPostedFileWrapper))
                {
                    throw new InvalidOperationException(&quot;&quot;);
                }

                var errorMessage = new StringBuilder();
                var file = value as HttpPostedFileBase;

                if (_minimumFileSizeValidator != null)
                {
                    if (!_minimumFileSizeValidator.IsValid(file))
                    {
                        errorMessage.Append(String.Format(&quot;{0}. &quot;, _minimumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)));                        
                    }
                }

                if (_maximumFileSizeValidator!=null)
                {
                    if (!_maximumFileSizeValidator.IsValid(file))
                    {
                        errorMessage.Append(String.Format(&quot;{0}. &quot;, _maximumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)));
                    }
                }

                if (_validFileTypeValidator != null)
                {
                    if (!_validFileTypeValidator.IsValid(file))
                    {
                        errorMessage.Append(String.Format(&quot;{0}. &quot;, _validFileTypeValidator.FormatErrorMessage(validationContext.DisplayName)));
                    }
                }

                if (String.IsNullOrEmpty(errorMessage.ToString()))
                {
                    return ValidationResult.Success;
                }
                else 
                {
                    return new ValidationResult(errorMessage.ToString());
                }
            }
            catch(Exception excp)
            {
                return new ValidationResult(excp.Message);
            }
        }

        public IEnumerable&lt;ModelClientValidationRule&gt; GetClientValidationRules(
              ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = &quot;fileuploadvalidator&quot;
            };
            
            var clientvalidationmethods  = new List&lt;string&gt;();
            var parameters               = new List&lt;string&gt;();
            var errorMessages            = new List&lt;string&gt;();

            if (_minimumFileSizeValidator != null) 
            {
                clientvalidationmethods.Add(_minimumFileSizeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(_minimumFileSizeValidator.MinimumFileSize.ToString());
                errorMessages.Add(_minimumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            if (_maximumFileSizeValidator != null)
            {
                clientvalidationmethods.Add(_maximumFileSizeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(_maximumFileSizeValidator.MaximumFileSize.ToString());
                errorMessages.Add(_maximumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            if (_validFileTypeValidator != null)
            {
                clientvalidationmethods.Add(_validFileTypeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(String.Join(&quot;,&quot;, _validFileTypeValidator.ValidFileTypes));
                errorMessages.Add(_validFileTypeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            clientValidationRule.ValidationParameters.Add(&quot;clientvalidationmethods&quot;, clientvalidationmethods.ToConcatenatedString(&quot;,&quot;));
            clientValidationRule.ValidationParameters.Add(&quot;parameters&quot;             , parameters.ToConcatenatedString(&quot;|&quot;));
            clientValidationRule.ValidationParameters.Add(&quot;errormessages&quot;          , errorMessages.ToConcatenatedString(&quot;,&quot;));

            yield return clientValidationRule;
        }              

        private double ConvertBytesToMegabytes(
            long bytes) 
        {
            return (bytes / 1024f) / 1024f;
        } 
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;FileUploadValidator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;ValidationAttribute,&nbsp;IClientValidatable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;MinimumFileSizeValidator&nbsp;_minimumFileSizeValidator;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;MaximumFileSizeValidator&nbsp;_maximumFileSizeValidator;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;ValidFileTypeValidator&nbsp;&nbsp;&nbsp;_validFileTypeValidator;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;validFileTypes&quot;&gt;Valid&nbsp;file&nbsp;extentions(without&nbsp;the&nbsp;dot)&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;FileUploadValidator(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">params</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;validFileTypes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">base</span>()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_validFileTypeValidator&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidFileTypeValidator(validFileTypes);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;maximumFileSize&quot;&gt;Maximum&nbsp;file&nbsp;size&nbsp;in&nbsp;MB&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;validFileTypes&quot;&gt;Valid&nbsp;file&nbsp;extentions(without&nbsp;the&nbsp;dot)&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;FileUploadValidator(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;maximumFileSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;<span class="cs__keyword">params</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;validFileTypes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">base</span>()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_maximumFileSizeValidator&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MaximumFileSizeValidator(maximumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_validFileTypeValidator&nbsp;&nbsp;&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidFileTypeValidator(validFileTypes);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;minimumFileSize&quot;&gt;MinimumFileSize&nbsp;file&nbsp;size&nbsp;in&nbsp;MB&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;maximumFileSize&quot;&gt;Maximum&nbsp;file&nbsp;size&nbsp;in&nbsp;MB&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;validFileTypes&quot;&gt;Valid&nbsp;file&nbsp;extentions(without&nbsp;the&nbsp;dot)&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;FileUploadValidator(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;minimumFileSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;<span class="cs__keyword">double</span>&nbsp;maximumFileSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;<span class="cs__keyword">params</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;validFileTypes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">base</span>()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_minimumFileSizeValidator&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MinimumFileSizeValidator(minimumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_maximumFileSizeValidator&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MaximumFileSizeValidator(maximumFileSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_validFileTypeValidator&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidFileTypeValidator(validFileTypes);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;ValidationResult&nbsp;IsValid(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">object</span>&nbsp;<span class="cs__keyword">value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;ValidationContext&nbsp;validationContext)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">value</span>&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ValidationResult.Success;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">value</span>.GetType()&nbsp;!=&nbsp;<span class="cs__keyword">typeof</span>(HttpPostedFileWrapper))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;InvalidOperationException(<span class="cs__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;errorMessage&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StringBuilder();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;file&nbsp;=&nbsp;<span class="cs__keyword">value</span>&nbsp;<span class="cs__keyword">as</span>&nbsp;HttpPostedFileBase;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_minimumFileSizeValidator&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!_minimumFileSizeValidator.IsValid(file))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessage.Append(String.Format(<span class="cs__string">&quot;{0}.&nbsp;&quot;</span>,&nbsp;_minimumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)));&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_maximumFileSizeValidator!=<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!_maximumFileSizeValidator.IsValid(file))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessage.Append(String.Format(<span class="cs__string">&quot;{0}.&nbsp;&quot;</span>,&nbsp;_maximumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_validFileTypeValidator&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!_validFileTypeValidator.IsValid(file))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessage.Append(String.Format(<span class="cs__string">&quot;{0}.&nbsp;&quot;</span>,&nbsp;_validFileTypeValidator.FormatErrorMessage(validationContext.DisplayName)));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(String.IsNullOrEmpty(errorMessage.ToString()))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ValidationResult.Success;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidationResult(errorMessage.ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>(Exception&nbsp;excp)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidationResult(excp.Message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;ModelClientValidationRule&gt;&nbsp;GetClientValidationRules(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ModelMetadata&nbsp;metadata&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;ControllerContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientValidationRule&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ModelClientValidationRule()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ErrorMessage&nbsp;&nbsp;&nbsp;=&nbsp;FormatErrorMessage(metadata.GetDisplayName()),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationType&nbsp;=&nbsp;<span class="cs__string">&quot;fileuploadvalidator&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientvalidationmethods&nbsp;&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;parameters&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;errorMessages&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_minimumFileSizeValidator&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientvalidationmethods.Add(_minimumFileSizeValidator.GetClientValidationRules(metadata,&nbsp;context).First().ValidationType);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(_minimumFileSizeValidator.MinimumFileSize.ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessages.Add(_minimumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_maximumFileSizeValidator&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientvalidationmethods.Add(_maximumFileSizeValidator.GetClientValidationRules(metadata,&nbsp;context).First().ValidationType);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(_maximumFileSizeValidator.MaximumFileSize.ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessages.Add(_maximumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(_validFileTypeValidator&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientvalidationmethods.Add(_validFileTypeValidator.GetClientValidationRules(metadata,&nbsp;context).First().ValidationType);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(String.Join(<span class="cs__string">&quot;,&quot;</span>,&nbsp;_validFileTypeValidator.ValidFileTypes));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errorMessages.Add(_validFileTypeValidator.FormatErrorMessage(metadata.GetDisplayName()));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientValidationRule.ValidationParameters.Add(<span class="cs__string">&quot;clientvalidationmethods&quot;</span>,&nbsp;clientvalidationmethods.ToConcatenatedString(<span class="cs__string">&quot;,&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientValidationRule.ValidationParameters.Add(<span class="cs__string">&quot;parameters&quot;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;parameters.ToConcatenatedString(<span class="cs__string">&quot;|&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientValidationRule.ValidationParameters.Add(<span class="cs__string">&quot;errormessages&quot;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;errorMessages.ToConcatenatedString(<span class="cs__string">&quot;,&quot;</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;yield&nbsp;<span class="cs__keyword">return</span>&nbsp;clientValidationRule;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">double</span>&nbsp;ConvertBytesToMegabytes(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">long</span>&nbsp;bytes)&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;(bytes&nbsp;/&nbsp;1024f)&nbsp;/&nbsp;1024f;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class ProfileModel
{
   public string Username { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }

   [MinimumFileSizeValidator(0.5)]
   [MaximumFileSizeValidator(2.4)]
   [ValidFileTypeValidator(&quot;png&quot;)]
   //[FileUploadValidator(0.5, 2.4, &quot;png&quot;)]
   public HttpPostedFileBase Avatar { get; set; }
 }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ProfileModel&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Username&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;FirstName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;LastName&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;[MinimumFileSizeValidator(<span class="cs__number">0.5</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;[MaximumFileSizeValidator(<span class="cs__number">2.4</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;[ValidFileTypeValidator(<span class="cs__string">&quot;png&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__com">//[FileUploadValidator(0.5,&nbsp;2.4,&nbsp;&quot;png&quot;)]</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;HttpPostedFileBase&nbsp;Avatar&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">@model ProfileModel
@{
    ViewBag.Title = &quot;Profile&quot;;
}

@section scripts{
    @Scripts.Render(&quot;~/bundles/jqueryval&quot;)
    @Scripts.Render(&quot;~/bundles/custom-validators&quot;)
}

&lt;h2&gt;Profile&lt;/h2&gt;
@using (Html.BeginForm(&quot;Index&quot;, &quot;Profile&quot;, FormMethod.Post, new { @class = &quot;form-horizontal&quot;, enctype = &quot;multipart/form-data&quot; }))
{
    &lt;div class=&quot;form-group&quot;&gt;
        @Html.LabelFor(m =&gt; m.Username, new { @class = &quot;col-sm-2 control-label&quot; })
        &lt;div class=&quot;col-sm-10&quot;&gt;
            @Html.TextBoxFor(m =&gt; m.Username, new { @class = &quot;form-control&quot;, placeholder = &quot;Username&quot; })
        &lt;/div&gt;
    &lt;/div&gt;
    &lt;div class=&quot;form-group&quot;&gt;
        @Html.LabelFor(m =&gt; m.FirstName, new { @class = &quot;col-sm-2 control-label&quot; })
        &lt;div class=&quot;col-sm-10&quot;&gt;
            @Html.TextBoxFor(m =&gt; m.FirstName, new { @class = &quot;form-control&quot;, placeholder = &quot;First Name&quot; })
        &lt;/div&gt;
    &lt;/div&gt;
    &lt;div class=&quot;form-group&quot;&gt;
        @Html.LabelFor(m =&gt; m.LastName, new { @class = &quot;col-sm-2 control-label&quot; })
        &lt;div class=&quot;col-sm-10&quot;&gt;
            @Html.TextBoxFor(m =&gt; m.LastName, new { @class = &quot;form-control&quot;, placeholder = &quot;Last Name&quot; })
        &lt;/div&gt;
    &lt;/div&gt;
    &lt;div class=&quot;form-group&quot;&gt;
        @Html.LabelFor(m =&gt; m.Avatar, new { @class = &quot;col-sm-2 control-label&quot; })
        &lt;div class=&quot;col-sm-10&quot;&gt;
            @Html.TextBoxFor(m =&gt; m.Avatar, new { @class = &quot;form-control&quot;, type = &quot;file&quot; })
            @Html.ValidationMessageFor(m =&gt; m.Avatar)
        &lt;/div&gt;
    &lt;/div&gt;    
    &lt;div class=&quot;form-group&quot;&gt;
        &lt;div class=&quot;col-sm-offset-2 col-sm-10&quot;&gt;
            &lt;button type=&quot;submit&quot; class=&quot;btn btn-default&quot;&gt;Save&lt;/button&gt;
        &lt;/div&gt;
    &lt;/div&gt;
}</pre>
<div class="preview">
<pre class="html">@model&nbsp;ProfileModel&nbsp;
@{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ViewBag.Title&nbsp;=&nbsp;&quot;Profile&quot;;&nbsp;
}&nbsp;
&nbsp;
@section&nbsp;scripts{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;@Scripts.Render(&quot;~/bundles/jqueryval&quot;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;@Scripts.Render(&quot;~/bundles/custom-validators&quot;)&nbsp;
}&nbsp;
&nbsp;
<span class="html__tag_start">&lt;h2</span><span class="html__tag_start">&gt;</span>Profile<span class="html__tag_end">&lt;/h2&gt;</span>&nbsp;
@using&nbsp;(Html.BeginForm(&quot;Index&quot;,&nbsp;&quot;Profile&quot;,&nbsp;FormMethod.Post,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;form-horizontal&quot;,&nbsp;enctype&nbsp;=&nbsp;&quot;multipart/form-data&quot;&nbsp;}))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;form-group&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.LabelFor(m&nbsp;=&gt;&nbsp;m.Username,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;col-sm-2&nbsp;control-label&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;col-sm-10&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.TextBoxFor(m&nbsp;=&gt;&nbsp;m.Username,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;form-control&quot;,&nbsp;placeholder&nbsp;=&nbsp;&quot;Username&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;form-group&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.LabelFor(m&nbsp;=&gt;&nbsp;m.FirstName,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;col-sm-2&nbsp;control-label&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;col-sm-10&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.TextBoxFor(m&nbsp;=&gt;&nbsp;m.FirstName,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;form-control&quot;,&nbsp;placeholder&nbsp;=&nbsp;&quot;First&nbsp;Name&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;form-group&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.LabelFor(m&nbsp;=&gt;&nbsp;m.LastName,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;col-sm-2&nbsp;control-label&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;col-sm-10&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.TextBoxFor(m&nbsp;=&gt;&nbsp;m.LastName,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;form-control&quot;,&nbsp;placeholder&nbsp;=&nbsp;&quot;Last&nbsp;Name&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;form-group&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.LabelFor(m&nbsp;=&gt;&nbsp;m.Avatar,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;col-sm-2&nbsp;control-label&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;col-sm-10&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.TextBoxFor(m&nbsp;=&gt;&nbsp;m.Avatar,&nbsp;new&nbsp;{&nbsp;@class&nbsp;=&nbsp;&quot;form-control&quot;,&nbsp;type&nbsp;=&nbsp;&quot;file&quot;&nbsp;})&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Html.ValidationMessageFor(m&nbsp;=&gt;&nbsp;m.Avatar)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;form-group&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;col-sm-offset-2&nbsp;col-sm-10&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;button</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;submit&quot;</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;btn&nbsp;btn-default&quot;</span><span class="html__tag_start">&gt;</span>Save<span class="html__tag_end">&lt;/button&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span>&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">(function () {   
    $.validator.unobtrusive.adapters.addSingleVal(&quot;minimumfilesize&quot;, &quot;size&quot;);
    $.validator.unobtrusive.adapters.addSingleVal(&quot;maximumfilesize&quot;, &quot;size&quot;);
    $.validator.unobtrusive.adapters.addSingleVal(&quot;validfiletype&quot;, &quot;filetypes&quot;);

    $.validator.addMethod('minimumfilesize', function (value, element, minSize) {
        return convertBytesToMegabytes(element.files[0].size) &gt;= parseFloat(minSize);
    });

    $.validator.addMethod('maximumfilesize', function (value, element, maxSize) {
        return convertBytesToMegabytes(element.files[0].size) &lt;= parseFloat(maxSize);
    });

    $.validator.addMethod('validfiletype', function (value, element, validFileTypes) {
        if (validFileTypes.indexOf(',') &gt; -1) {
            validFileTypes = validFileTypes.split(',');
        } else {
            validFileTypes = [validFileTypes];
        }

        var fileType = value.split('.')[value.split('.').length - 1];

        for (var i = 0; i &lt; validFileTypes.length; i&#43;&#43;) {
            if (validFileTypes[i] === fileType) {
                return true;
            }
        }

        return false;
    });

    $.validator.unobtrusive.adapters.add('fileuploadvalidator', ['clientvalidationmethods', 'parameters', 'errormessages'], function (options) {
        options.rules['fileuploadvalidator'] = {
            clientvalidationmethods: options.params['clientvalidationmethods'].split(','),
            parameters: options.params['parameters'].split('|'),
            errormessages: options.params['errormessages'].split(',')
        };
    });

    $.validator.addMethod(&quot;fileuploadvalidator&quot;, function (value, element, param) {
        if (value == &quot;&quot; || value == null || value == undefined) {
            return true;
        }
        //array of jquery validation rule names
        var validationrules = param[&quot;clientvalidationmethods&quot;];

        //array of paramteres required by rules, in this case regex patterns
        var patterns = param[&quot;parameters&quot;];

        //array of error messages for each rule
        var rulesErrormessages = param[&quot;errormessages&quot;];

        var validNameErrorMessage = new Array();
        var index = 0

        for (i = 0; i &lt; patterns.length; i&#43;&#43;) {
            var valid = true;
            var pattern = patterns[i].trim();

            //get a jquery validator method.  
            var rule = $.validator.methods[validationrules[i].trim()];

            //create a paramtere object
            var parameter = new Object();
            parameter = pattern;

            //execute the rule
            var isValid = rule.call(this, value, element, parameter);

            if (!isValid) {
                //if rule fails, add error message
                validNameErrorMessage[index] = rulesErrormessages[i];
                index&#43;&#43;;
            }
        }
        //if we have more than on error message, one of the rule has failed
        if (validNameErrorMessage.length &gt; 0) {
            //update the error message for 'validname' rule
            $.validator.messages.fileuploadvalidator = validNameErrorMessage.toString();
            return false;
        }
        return true;
      }, &quot;This is not a valid individual name&quot;//default error message
    );

    function convertBytesToMegabytes(bytes) {
        return (bytes / 1024) / 1024;
    }
})();</pre>
<div class="preview">
<pre class="js">(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.unobtrusive.adapters.addSingleVal(<span class="js__string">&quot;minimumfilesize&quot;</span>,&nbsp;<span class="js__string">&quot;size&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.unobtrusive.adapters.addSingleVal(<span class="js__string">&quot;maximumfilesize&quot;</span>,&nbsp;<span class="js__string">&quot;size&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.unobtrusive.adapters.addSingleVal(<span class="js__string">&quot;validfiletype&quot;</span>,&nbsp;<span class="js__string">&quot;filetypes&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.addMethod(<span class="js__string">'minimumfilesize'</span>,&nbsp;<span class="js__operator">function</span>&nbsp;(value,&nbsp;element,&nbsp;minSize)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;convertBytesToMegabytes(element.files[<span class="js__num">0</span>].size)&nbsp;&gt;=&nbsp;<span class="js__function">parseFloat</span>(minSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.addMethod(<span class="js__string">'maximumfilesize'</span>,&nbsp;<span class="js__operator">function</span>&nbsp;(value,&nbsp;element,&nbsp;maxSize)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;convertBytesToMegabytes(element.files[<span class="js__num">0</span>].size)&nbsp;&lt;=&nbsp;<span class="js__function">parseFloat</span>(maxSize);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.addMethod(<span class="js__string">'validfiletype'</span>,&nbsp;<span class="js__operator">function</span>&nbsp;(value,&nbsp;element,&nbsp;validFileTypes)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(validFileTypes.indexOf(<span class="js__string">','</span>)&nbsp;&gt;&nbsp;-<span class="js__num">1</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validFileTypes&nbsp;=&nbsp;validFileTypes.split(<span class="js__string">','</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validFileTypes&nbsp;=&nbsp;[validFileTypes];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;fileType&nbsp;=&nbsp;value.split(<span class="js__string">'.'</span>)[value.split(<span class="js__string">'.'</span>).length&nbsp;-&nbsp;<span class="js__num">1</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;validFileTypes.length;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(validFileTypes[i]&nbsp;===&nbsp;fileType)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.unobtrusive.adapters.add(<span class="js__string">'fileuploadvalidator'</span>,&nbsp;[<span class="js__string">'clientvalidationmethods'</span>,&nbsp;<span class="js__string">'parameters'</span>,&nbsp;<span class="js__string">'errormessages'</span>],&nbsp;<span class="js__operator">function</span>&nbsp;(options)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;options.rules[<span class="js__string">'fileuploadvalidator'</span>]&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientvalidationmethods:&nbsp;options.params[<span class="js__string">'clientvalidationmethods'</span>].split(<span class="js__string">','</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameters:&nbsp;options.params[<span class="js__string">'parameters'</span>].split(<span class="js__string">'|'</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;errormessages:&nbsp;options.params[<span class="js__string">'errormessages'</span>].split(<span class="js__string">','</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.validator.addMethod(<span class="js__string">&quot;fileuploadvalidator&quot;</span>,&nbsp;<span class="js__operator">function</span>&nbsp;(value,&nbsp;element,&nbsp;param)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(value&nbsp;==&nbsp;<span class="js__string">&quot;&quot;</span>&nbsp;||&nbsp;value&nbsp;==&nbsp;null&nbsp;||&nbsp;value&nbsp;==&nbsp;<span class="js__property">undefined</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//array&nbsp;of&nbsp;jquery&nbsp;validation&nbsp;rule&nbsp;names</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;validationrules&nbsp;=&nbsp;param[<span class="js__string">&quot;clientvalidationmethods&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//array&nbsp;of&nbsp;paramteres&nbsp;required&nbsp;by&nbsp;rules,&nbsp;in&nbsp;this&nbsp;case&nbsp;regex&nbsp;patterns</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;patterns&nbsp;=&nbsp;param[<span class="js__string">&quot;parameters&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//array&nbsp;of&nbsp;error&nbsp;messages&nbsp;for&nbsp;each&nbsp;rule</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;rulesErrormessages&nbsp;=&nbsp;param[<span class="js__string">&quot;errormessages&quot;</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;validNameErrorMessage&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__object">Array</span>();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;index&nbsp;=&nbsp;<span class="js__num">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;patterns.length;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;valid&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;pattern&nbsp;=&nbsp;patterns[i].trim();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//get&nbsp;a&nbsp;jquery&nbsp;validator&nbsp;method.&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;rule&nbsp;=&nbsp;$.validator.methods[validationrules[i].trim()];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//create&nbsp;a&nbsp;paramtere&nbsp;object</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;parameter&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__object">Object</span>();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter&nbsp;=&nbsp;pattern;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//execute&nbsp;the&nbsp;rule</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;isValid&nbsp;=&nbsp;rule.call(<span class="js__operator">this</span>,&nbsp;value,&nbsp;element,&nbsp;parameter);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(!isValid)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//if&nbsp;rule&nbsp;fails,&nbsp;add&nbsp;error&nbsp;message</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validNameErrorMessage[index]&nbsp;=&nbsp;rulesErrormessages[i];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;index&#43;&#43;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//if&nbsp;we&nbsp;have&nbsp;more&nbsp;than&nbsp;on&nbsp;error&nbsp;message,&nbsp;one&nbsp;of&nbsp;the&nbsp;rule&nbsp;has&nbsp;failed</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(validNameErrorMessage.length&nbsp;&gt;&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//update&nbsp;the&nbsp;error&nbsp;message&nbsp;for&nbsp;'validname'&nbsp;rule</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.validator.messages.fileuploadvalidator&nbsp;=&nbsp;validNameErrorMessage.toString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;<span class="js__string">&quot;This&nbsp;is&nbsp;not&nbsp;a&nbsp;valid&nbsp;individual&nbsp;name&quot;</span><span class="js__sl_comment">//default&nbsp;error&nbsp;message</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;convertBytesToMegabytes(bytes)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;(bytes&nbsp;/&nbsp;<span class="js__num">1024</span>)&nbsp;/&nbsp;<span class="js__num">1024</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>)();</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</span></div>
<div class="endscriptcode"><span>Source Code Files</span></div>
</h1>
<ul>
<li><em>MinimumFileSizeValidator.cs - Contains validation code for minimum file size<br>
</em></li><li><em><em>MaximumFileSizeValidator.cs - <em>Contains validation code for maximum file size</em></em></em>
</li><li>ValidFileTypeValidator.cs - Contains validation code for valid file types </li><li>FileUploadValidator.cs - Contains code that combines all the above validators into one.
</li><li>custom-validators.js - Contains client side script that regsiters the client side unobtrusive validators
</li></ul>
<h1>Helpful links</h1>
<p><a title="validation-in-asp.net-mvc-3" href="http://www.devtrends.co.uk/blog/the-complete-guide-to-validation-in-asp.net-mvc-3-part-2" target="_blank">http://www.devtrends.co.uk/blog/the-complete-guide-to-validation-in-asp.net-mvc-3-part-2</a></p>
<p><a title="handling multiple validation rules in aspnet mvc using dataannotations and unobtrusive jquery validator" href="https://www.captechconsulting.com/blogs/handling-multiple-validation-rules-in-aspnet-mvc-using-dataannotations-and-unobtrusive-jquery-validator---part-2" target="_blank">https://www.captechconsulting.com/blogs/handling-multiple-validation-rules-in-aspnet-mvc-using-dataannotations-and-unobtrusive-jquery-validator---part-2</a></p>
