# Pdf writer pipelinecomponent for BizTalk
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- BizTalk Server 2016
- FO.NET
## Topics
- Custom Pipeline Component
- PDF
- XSL-FO
## Updated
- 04/19/2016
## Description

<h1>Introduction</h1>
<p><em>I wanted to find something fun to create for my first BiZtalk 2016 project. As it so happens i had a requirement to create PDF documents from BizTalk.</em></p>
<p><em>Strange enough i could not find a pdf writer pipeline, so why not create one.</em></p>
<p><br>
<em>I wanted to use a technique that was close to BizTalk so i wanted to find a component that could create pdf content from xsl-fo source.</em></p>
<p><em>The component i found was found at codeplex, of course, https://fonet.codeplex.com/.</em></p>
<p><em>FO.NET is very easy to use and i can recommend it to anyone.</em></p>
<h1><span>Sample</span></h1>
<p><em>The attached file contains pipelinecomponent, sample BizTalk project that shows how you can create a simple table output plus a sample project to test xsl-fo messages before creating a map on it in BizTalk.</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><em>Use the sample &quot;test&quot; project to test xsl-fo messages before creating a map on it in BizTalk. The xslfo schema is very big so best to create a base to work on.</em></em></p>
<p>&nbsp;</p>
<h2><em><em>Layout master</em></em></h2>
<p><em><em>Every xsl-fo message must begin with a layout-master-set wich more or less set's the canvas size of one page.</em></em></p>
<p><em><em><br>
</em></em></p>
<p><em><img id="151098" src="151098-layout-master.jpg" alt="" width="753" height="203"></em></p>
<p><em>The rest of the xsl-fo i leave to you learn</em></p>
<p>&nbsp;</p>
<h2><em>Properties</em></h2>
<p>&nbsp;</p>
<p><em>I only added 3 properties in the pipeline component from the FO.NET component, it is possible to add more.</em></p>
<p><em>Tile,Subject and Password that lets you passwprd protect your pdf file.<br>
</em></p>
