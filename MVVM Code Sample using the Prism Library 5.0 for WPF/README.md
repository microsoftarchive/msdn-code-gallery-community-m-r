# MVVM Code Sample using the Prism Library 5.0 for WPF
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- C#
- XAML
- .NET Framwork
## Topics
- MVVM
- Prism
## Updated
- 08/21/2014
## Description

<h1>Introduction</h1>
<p class="ppBodyText">The Model-View-ViewModel (MVVM) QuickStart provides sample code that demonstrates how to separate the state and logic that support a view into a separate class named&nbsp;<strong>ViewModel&nbsp;</strong>using the Prism Library. The view
 model sits on top of the application data model to provide the state or data needed to support the view, insulating the view from needing to know about the full complexity of the application. The view model also encapsulates the interaction logic for the view
 that does not directly depend on the view elements themselves. This QuickStart provides a tutorial on implementing the MVVM pattern.</p>
<p class="ppBodyText">A common approach to designing the views and view models in an MVVM application is the first sketch&nbsp;a storyboard for what a view looks like on the screen. Then you analyze that screen to identify what properties the view model needs
 to expose to support the view, without worrying about how that data will get into the view model. After you define what the view model needs to expose to the view and implement that, you can then dive into how to get the data into the view model. Often, this
 involves the view model calling to a service to retrieve the data, and sometimes data can be pushed into a view model from some other code such as an application controller.</p>
<p class="ppBodyText">This QuickStart leads you through the following steps:</p>
<ul>
<li>Analyzing the view to decide what state is needed from a view model to support it
</li><li>Defining the view model class with the minimum implementation to support the view
</li><li>Defining the bindings in the view that point to view model properties </li><li>Attaching the view to the view model </li></ul>
<p class="ppBodyText">&nbsp;</p>
<h1>Building the Sample</h1>
<p class="ppBodyText">This QuickStart requires Microsoft Visual Studio 2012 or later and the .NET Framework 4.5.1.</p>
<p class="ppProcedureStart">To build and run the MVVM QuickStart</p>
<ol>
<li>In Visual Studio, open the solution file BasicMVVMQuickstart_Desktop\BasicMVVMQuickstart_Desktop.sln.
</li><li>In the&nbsp;<strong>Build</strong>&nbsp;menu, click&nbsp;<strong>Rebuild Solution</strong>.
</li><li>Press F5 to run the QuickStart. </li></ol>
<p>&nbsp;</p>
<h1>More Information</h1>
<p><em>For more information on the MVVM QuickStart, see the associated&nbsp;<a href="http://aka.ms/prism-wpf-QSMVVMDoc">documentation&nbsp;</a>on MSDN.&nbsp;</em></p>
