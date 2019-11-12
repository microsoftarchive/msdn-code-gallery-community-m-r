# MVVM with Silverlight (Begin to End)
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- Silverlight 4
- Silverlight
- ViewModel pattern (MVVM)
## Topics
- ViewModel pattern (MVVM)
## Updated
- 11/06/2011
## Description

<p><strong>Why we use MVVM?</strong><br>
<br>
&nbsp;1. MVVM provide application to code reuse.<br>
2. Simplifies the application.<br>
3. Easy to maintenance and provide maximum support for the testing.<br>
<br>
<strong>What is MVVM? </strong><br>
Model-View-ViewModel (MVVM) is a design pattern which can be use in WPF and Silverlight.<br>
<br>
Model : Entities (Most of the time directly connect with the application backend.)<br>
View : Your silverlight screens.<br>
ViewModel : The glue between the Model and View. <br>
<br>
In Silverlight View and ViewModel will located in the client side and Model will located in server side.So We have to use Communication method to communicate between the Client and server side. Most of the time we user WCF Service for this.<br>
<br>
Now we will implement a simple silverlight application using MVVM pattern.This example I will explain the Command and Property Binding using MVVM.<br>
<br>
<strong>Business Requirement : </strong>We want to find the person information using person ID and we want to display person information in web form..Here Person information is stored in the server side.
<br>
<br>
</p>
<div class="separator" style="clear:both; text-align:center"><a href="http://1.bp.blogspot.com/-Eok6Tm4LuIc/Tqzt1zti6CI/AAAAAAAAAXA/JpcLLcVfB7A/s1600/FinalOutput.JPG" style="margin-left:1em; margin-right:1em"><img src="-finaloutput.jpg" border="0" alt="" width="400" height="207"></a></div>
<p><br>
<br>
<strong>Solution :</strong><br>
1. Create a new Silverlight application and name it as MVVMSample.Now Visual Studio will create Silverlight project and Web Application project for you.</p>
<div class="separator" style="clear:both; text-align:center"><a href="http://1.bp.blogspot.com/-8DgbvFpoCxE/TqzvoulNT-I/AAAAAAAAAXI/OlOR5kL8WP4/s1600/1.JPG" style="margin-left:1em; margin-right:1em"><img src="-1.jpg" border="0" alt=""></a></div>
<p><br>
<br>
2<strong>. </strong>Now right click on the MVVMSample.Web project and add New Class named by PersonData and implement the class as bellow.<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">namespace</span> Index.Web
{
    <span class="kwrd">public</span> <span class="kwrd">class</span> PersonData
    {
        <span class="kwrd">public</span> <span class="kwrd">int</span> PersonID { get; set; }
        <span class="kwrd">public</span> <span class="kwrd">string</span> FirstName { get; set; }
        <span class="kwrd">public</span> <span class="kwrd">string</span> LastName { get; set; }
        <span class="kwrd">public</span> <span class="kwrd">int</span> Age { get; set; }
        <span class="kwrd">public</span> <span class="kwrd">string</span> Address { get; set; }
    }
}</pre>
<p><br>
<br>
3.Again Right Click on the MVVMSample.Web project and add New Class named by PersonRepository and implement the class as bellow.<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Collections.Generic.aspx" target="_blank" title="Auto generated link to System.Collections.Generic">System.Collections.Generic</a>;

<span class="kwrd">namespace</span> Index.Web
{
    <span class="kwrd">public</span> <span class="kwrd">class</span> PersonRepository
    {
        <span class="kwrd">public</span> IList&lt;PersonData&gt; Person{get;set;}

        <span class="kwrd">public</span> PersonRepository()
        {
            GeneratePersonList();
        }

        <span class="kwrd">private</span> <span class="kwrd">void</span> GeneratePersonList()
        {
            Person = <span class="kwrd">new</span> List&lt;PersonData&gt;() 
            {
                <span class="kwrd">new</span> PersonData() { PersonID = 1, FirstName = <span class="str">&quot;Erandika&quot;</span>,    LastName = <span class="str">&quot;Sandaruwan&quot;</span>, Age=25,     Address = <span class="str">&quot;Delgoda&quot;</span>    },
                <span class="kwrd">new</span> PersonData() { PersonID = 2, FirstName = <span class="str">&quot;Niluka&quot;</span>,      LastName = <span class="str">&quot;Dilani&quot;</span>,     Age = 30,   Address = <span class="str">&quot;Kandy&quot;</span>      },
                <span class="kwrd">new</span> PersonData() { PersonID = 3, FirstName = <span class="str">&quot;Chathura&quot;</span>,    LastName = <span class="str">&quot;Achini&quot;</span>,     Age = 27,   Address = <span class="str">&quot;Colombo&quot;</span>    },
                <span class="kwrd">new</span> PersonData() { PersonID = 4, FirstName = <span class="str">&quot;Florina&quot;</span>,     LastName = <span class="str">&quot;Breban&quot;</span>,     Age = 25,   Address = <span class="str">&quot;Romania&quot;</span>    },
            };
        }
    }
}</pre>
<p><br>
<br>
4.Finally we want to add our silverlight enable WCF service to MVVMSample.Web project.To do that right click on the MVVMSample.Web project Select the Add New Item and then in Installed Template under Visual C# select the Silverlight.Now select the&nbsp; Silverlight-enable
 WCF Service and name it as PersonDataService.svc and click on the Add button.<br>
<br>
</p>
<div class="separator" style="clear:both; text-align:center"><a href="http://3.bp.blogspot.com/-5JznMwS1oto/Tqz1Eh97khI/AAAAAAAAAXQ/KJ4AHnEybHQ/s1600/2.JPG" style="margin-left:1em; margin-right:1em"><img src="-2.jpg" border="0" alt="" width="640" height="388"></a></div>
<p><br>
<br>
Now implement the service class as bellow. <br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">using</span> System;
<span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Linq.aspx" target="_blank" title="Auto generated link to System.Linq">System.Linq</a>;
<span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Runtime.Serialization.aspx" target="_blank" title="Auto generated link to System.Runtime.Serialization">System.Runtime.Serialization</a>;
<span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.ServiceModel.aspx" target="_blank" title="Auto generated link to System.ServiceModel">System.ServiceModel</a>;
<span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.ServiceModel.Activation.aspx" target="_blank" title="Auto generated link to System.ServiceModel.Activation">System.ServiceModel.Activation</a>;

<span class="kwrd">namespace</span> Index.Web
{
    [ServiceContract(Namespace = <span class="str">&quot;&quot;</span>)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    <span class="kwrd">public</span> <span class="kwrd">class</span> PersonDataService
    {
        [OperationContract]
        <span class="kwrd">public</span> PersonData FindPerson(<span class="kwrd">int</span> personID)
        {
            <span class="kwrd">return</span> <span class="kwrd">new</span> PersonRepository().Person.Where(x =&gt; x.PersonID == personID).SingleOrDefault();
        }
    }
}</pre>
<p><br>
So here now we have completed our model part.So next we have to implement our View and ViewModel part.<br>
<br>
5.First We will add our PersonDataService to our silverlight project.To do that In MVVMSample project right click on References and click on Add Service Reference and then Add References Window will open for you and click on Discover button and VS will display
 all the services that have been added to current solution. But here we have only one service.Give the Namesapce as PersonDataService and click OK button.(Remember before this you have to build your web application project first) and then service reference
 will added to the your silverlight project.<br>
<br>
6. Now add new folder to Silverlight project and named it as ViewModel and add two classes named by MainPageViewModel. and ViewModelBase. Again add new folder called ServiceAgent and add a Interface called IPersonService.and implement the interface as bellow.
<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">using</span> System;
<span class="kwrd">using</span> MVVMSample.PersonDataService;

<span class="kwrd">namespace</span> MVVMSample.ServiceAgent
{
    <span class="kwrd">public</span> <span class="kwrd">interface</span> IPersonService
    {
        <span class="kwrd">void</span> FindPerson(<span class="kwrd">int</span> personID,EventHandler&lt;FindPersonCompletedEventArgs&gt; callBack);
    }
}</pre>
<p><br>
Now we want to implement PersonService class using IPersonService interface.So to do that add a new class to ServiceAgent folder and named it as PersonService and then implement the class as bellow.<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">using</span> System;
<span class="kwrd">using</span> MVVMSample.PersonDataService;

<span class="kwrd">namespace</span> MVVMSample.ServiceAgent
{
    <span class="kwrd">public</span> <span class="kwrd">class</span> PersonService:IPersonService
    {
        <span class="kwrd">private</span> PersonDataServiceClient client = <span class="kwrd">new</span> PersonDataServiceClient();

        <span class="kwrd">public</span> <span class="kwrd">void</span> FindPerson(<span class="kwrd">int</span> personID, EventHandler&lt;FindPersonCompletedEventArgs&gt; callBack)
        {
            client.FindPersonCompleted &#43;= callBack;
            client.FindPersonAsync(personID);
        }
    }
}</pre>
<p><br>
<br>
7. Now we have to implement our ViewModel.Note that here we want to use ICommand interface for the implement the command which it bind to Search button. Here we will use Prism Framework.So we do not want to implement the Command and we can directly use DelegateCommand.You
 can download the Prism framework from following URL<a href="http://www.blogger.com/goog_724230161">
</a><a href="http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=4922">http://www.microsoft.com/download/en/details.aspx?displaylang=en&amp;id=4922</a>
<br>
And you have to add as Microsoft.Practice.Prism.dll which is belongs to silverlight as reference to your silverlight project.Now we will start implement our MainPageViewModel and ViewModelBase classes.So First we will implement our ViewModelBase class as bellow.
<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.ComponentModel.aspx" target="_blank" title="Auto generated link to System.ComponentModel">System.ComponentModel</a>;

<span class="kwrd">namespace</span> MVVMSample.ViewModel
{
    <span class="kwrd">public</span> <span class="kwrd">class</span> ViewModelBase : INotifyPropertyChanged
    {
        <span class="kwrd">public</span> <span class="kwrd">event</span> PropertyChangedEventHandler PropertyChanged;
        <span class="kwrd">public</span> <span class="kwrd">bool</span> IsDesignTime
        {
            get
            {
                <span class="kwrd">return</span> DesignerProperties.IsInDesignTool;
            }
        }

        <span class="kwrd">protected</span> <span class="kwrd">virtual</span> <span class="kwrd">void</span> OnPropertyChanged(<span class="kwrd">string</span> propertyName)
        {
            var propertyChanged = PropertyChanged;

            <span class="kwrd">if</span> (propertyChanged != <span class="kwrd">null</span>)
            {
                propertyChanged(<span class="kwrd">this</span>,<span class="kwrd">new</span> PropertyChangedEventArgs(propertyName));
            }
        }


        
    }
}
</pre>
<p><br>
Here you can see that our ViewModelBase class in implemented from INotifyPropertyChanged interface.This is very important in silverlight data binding&nbsp; to notify&nbsp; controls and other objects when a bound property value changes.So by using our ViewModelBase
 class we can reuse it across the multiple ViewModel. So Now we will implement our MainPageViewModel as bellow.<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode"><span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Windows.Input.aspx" target="_blank" title="Auto generated link to System.Windows.Input">System.Windows.Input</a>;
<span class="kwrd">using</span> MVVMSample.PersonDataService;
<span class="kwrd">using</span> MVVMSample.ServiceAgent;
<span class="kwrd">using</span> <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/Microsoft.Practices.Prism.Commands.aspx" target="_blank" title="Auto generated link to Microsoft.Practices.Prism.Commands">Microsoft.Practices.Prism.Commands</a>;

<span class="kwrd">namespace</span> MVVMSample.ViewModel
{
    <span class="kwrd">public</span> <span class="kwrd">class</span> MainPageViewModel : ViewModelBase
    {

        <span class="kwrd">private</span> PersonService _personService;
        <span class="kwrd">public</span> MainPageViewModel()
        {
            <span class="kwrd">if</span> (IsDesignTime != <span class="kwrd">true</span>)
            {
                <span class="kwrd">this</span>._personService = <span class="kwrd">new</span> PersonService() ;
                <span class="kwrd">this</span>.FindPersonCommand = <span class="kwrd">new</span> DelegateCommand&lt;<span class="kwrd">object</span>&gt;(<span class="kwrd">this</span>.FindPersonByID);
            }
        }

        <span class="kwrd">private</span> PersonData _personData;
        <span class="kwrd">public</span> PersonData PersonData
        { 
            get
            {
                <span class="kwrd">return</span> _personData;
            }
            set
            {
                <span class="kwrd">if</span> (_personData != <span class="kwrd">value</span>)
                {
                    _personData = <span class="kwrd">value</span>;
                    OnPropertyChanged(<span class="str">&quot;PersonData&quot;</span>);
                }
            }
        }

        <span class="kwrd">private</span> <span class="kwrd">int</span> _personID;
        <span class="kwrd">public</span> <span class="kwrd">int</span> PersonID
        {
            get 
            {
                <span class="kwrd">return</span> _personID;
            } 
            set
            {
                <span class="kwrd">if</span> (_personID != <span class="kwrd">value</span>)
                {
                    _personID = <span class="kwrd">value</span>;
                    OnPropertyChanged(<span class="str">&quot;PersonID&quot;</span>);
                }
            } 
        }

        <span class="kwrd">public</span> ICommand FindPersonCommand{get;set;}


        <span class="kwrd">private</span> <span class="kwrd">void</span> FindPersonByID(<span class="kwrd">object</span> obj)
        {
            <span class="kwrd">if</span> (PersonID != 0)
            {
                _personService.FindPerson(PersonID, (s, e) =&gt; PersonData = e.Result);
            }
        }

    }
}
</pre>
<p><br>
You can see that we we inherit our MainPageViewModel class from ViewModelBase class&nbsp; and we reuse IsDesignTime property and OnPropertyChanged method in here. Also note that we are using DelegateCommand here.Which is come from Microsoft.Practice.Prism.dll
 and so we do not want to worry to implement this event.So now our MainPageViewModel is ready now and we have to bind data to our silverlight View.In this example we are going to bind PersonData and PersonID property and FindPersonCommand command to silverlight
 View.<br>
<br>
8. Now we will create our silverlight View and Bind the Data from our ViewModel.You implement our view as bellow.<br>
<br>
<br>
</p>
&lt;!-- .csharpcode, .csharpcode pre { font-size: small; color: black; font-family: Consolas, &quot;Courier New&quot;, Courier, Monospace; background-color: #ffffff; /*white-space: pre;*/ } .csharpcode pre { margin: 0em; } .csharpcode .rem { color: #008000; } .csharpcode
 .kwrd { color: #0000ff; } .csharpcode .str { color: #006080; } .csharpcode .op { color: #0000c0; } .csharpcode .preproc { color: #cc6633; } .csharpcode .asp { background-color: #ffff00; } .csharpcode .html { color: #800000; } .csharpcode .attr { color: #ff0000;
 } .csharpcode .alt { background-color: #f4f4f4; width: 100%; margin: 0em; } .csharpcode .lnum { color: #606060; } --&gt;
<p>&nbsp;</p>
<pre class="csharpcode">&lt;UserControl x:Class=<span class="str">&quot;MVVMSample.MainPage&quot;</span>
    xmlns=<span class="str">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span>
    xmlns:x=<span class="str">&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span>
    xmlns:d=<span class="str">&quot;http://schemas.microsoft.com/expression/blend/2008&quot;</span>
    xmlns:mc=<span class="str">&quot;http://schemas.openxmlformats.org/markup-compatibility/2006&quot;</span>
    mc:Ignorable=<span class="str">&quot;d&quot;</span>
    xmlns:viewModel=<span class="str">&quot;clr-namespace:MVVMSample.ViewModel&quot;</span>
    d:DesignHeight=<span class="str">&quot;300&quot;</span> d:DesignWidth=<span class="str">&quot;400&quot;</span> xmlns:sdk=<span class="str">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation/sdk&quot;</span>&gt;
    
    &lt;UserControl.DataContext&gt;
        &lt;viewModel:MainPageViewModel/&gt;
    &lt;/UserControl.DataContext&gt;
    &lt;Grid x:Name=<span class="str">&quot;LayoutRoot&quot;</span> Background=<span class="str">&quot;White&quot;</span>&gt;
        &lt;Grid.RowDefinitions&gt;
            &lt;RowDefinition Height=<span class="str">&quot;25&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;25&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;5&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;25&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;5&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;25&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;5&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;25&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;5&quot;</span>/&gt;
            &lt;RowDefinition Height=<span class="str">&quot;25&quot;</span>/&gt;
        &lt;/Grid.RowDefinitions&gt;
        
        &lt;Grid.ColumnDefinitions&gt;
            &lt;ColumnDefinition/&gt;
            &lt;ColumnDefinition Width=<span class="str">&quot;Auto&quot;</span>/&gt;
            &lt;ColumnDefinition/&gt;
        &lt;/Grid.ColumnDefinitions&gt;
        
        &lt;sdk:Label Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;1&quot;</span> Grid.Column=<span class="str">&quot;0&quot;</span> HorizontalAlignment=<span class="str">&quot;Right&quot;</span> Name=<span class="str">&quot;label1&quot;</span> VerticalAlignment=<span class="str">&quot;Center&quot;</span> Width=<span class="str">&quot;120&quot;</span> Content=<span class="str">&quot;Person ID :&quot;</span> /&gt;
        &lt;TextBox Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;1&quot;</span> Grid.Column=<span class="str">&quot;1&quot;</span>  HorizontalAlignment=<span class="str">&quot;Left&quot;</span> Name=<span class="str">&quot;txtPersonID&quot;</span> VerticalAlignment=<span class="str">&quot;Top&quot;</span> Width=<span class="str">&quot;150&quot;</span> Text=<span class="str">&quot;{Binding PersonID, Mode=TwoWay}&quot;</span> /&gt;
        &lt;Button Grid.Row=<span class="str">&quot;1&quot;</span> Grid.Column=<span class="str">&quot;2&quot;</span> Content=<span class="str">&quot;Search&quot;</span> Height=<span class="str">&quot;25&quot;</span> HorizontalAlignment=<span class="str">&quot;Left&quot;</span> Name=<span class="str">&quot;btnSearch&quot;</span> VerticalAlignment=<span class="str">&quot;Top&quot;</span> Width=<span class="str">&quot;75&quot;</span> Command=<span class="str">&quot;{Binding FindPersonCommand}&quot;</span> /&gt;
        
        &lt;sdk:Label Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;3&quot;</span> Grid.Column=<span class="str">&quot;0&quot;</span> HorizontalAlignment=<span class="str">&quot;Right&quot;</span> Name=<span class="str">&quot;label2&quot;</span> VerticalAlignment=<span class="str">&quot;Center&quot;</span> Width=<span class="str">&quot;120&quot;</span> Content=<span class="str">&quot;First Name :&quot;</span> /&gt;
        &lt;TextBox  Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;3&quot;</span> Grid.Column=<span class="str">&quot;1&quot;</span> HorizontalAlignment=<span class="str">&quot;Left&quot;</span> Name=<span class="str">&quot;txtFirstName&quot;</span> VerticalAlignment=<span class="str">&quot;Top&quot;</span> Width=<span class="str">&quot;200&quot;</span> Text=<span class="str">&quot;{Binding Path=PersonData.FirstName, Mode=TwoWay}&quot;</span> /&gt;
        
        &lt;sdk:Label Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;5&quot;</span> Grid.Column=<span class="str">&quot;0&quot;</span> HorizontalAlignment=<span class="str">&quot;Right&quot;</span> Name=<span class="str">&quot;label3&quot;</span> VerticalAlignment=<span class="str">&quot;Center&quot;</span> Width=<span class="str">&quot;120&quot;</span> Content=<span class="str">&quot;Last Name :&quot;</span> /&gt;
        &lt;TextBox  Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;5&quot;</span> Grid.Column=<span class="str">&quot;1&quot;</span> HorizontalAlignment=<span class="str">&quot;Left&quot;</span> Name=<span class="str">&quot;txtLastName&quot;</span> VerticalAlignment=<span class="str">&quot;Top&quot;</span> Width=<span class="str">&quot;200&quot;</span> Text=<span class="str">&quot;{Binding Path=PersonData.LastName, Mode=TwoWay}&quot;</span> /&gt;
        
        &lt;sdk:Label Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;7&quot;</span> Grid.Column=<span class="str">&quot;0&quot;</span> HorizontalAlignment=<span class="str">&quot;Right&quot;</span> Name=<span class="str">&quot;label4&quot;</span> VerticalAlignment=<span class="str">&quot;Center&quot;</span> Width=<span class="str">&quot;120&quot;</span> Content=<span class="str">&quot;Age :&quot;</span> /&gt;
        &lt;TextBox  Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;7&quot;</span> Grid.Column=<span class="str">&quot;1&quot;</span> HorizontalAlignment=<span class="str">&quot;Left&quot;</span> Name=<span class="str">&quot;txtAge&quot;</span> VerticalAlignment=<span class="str">&quot;Top&quot;</span> Width=<span class="str">&quot;200&quot;</span> Text=<span class="str">&quot;{Binding Path=PersonData.Age, Mode=TwoWay}&quot;</span> /&gt;
        
        &lt;sdk:Label Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;9&quot;</span> Grid.Column=<span class="str">&quot;0&quot;</span> HorizontalAlignment=<span class="str">&quot;Right&quot;</span> Name=<span class="str">&quot;label5&quot;</span> VerticalAlignment=<span class="str">&quot;Center&quot;</span> Width=<span class="str">&quot;120&quot;</span> Content=<span class="str">&quot;Address :&quot;</span> /&gt;
        &lt;TextBox Height=<span class="str">&quot;25&quot;</span> Grid.Row=<span class="str">&quot;9&quot;</span> Grid.Column=<span class="str">&quot;1&quot;</span> HorizontalAlignment=<span class="str">&quot;Left&quot;</span> Name=<span class="str">&quot;txtAddress&quot;</span> VerticalAlignment=<span class="str">&quot;Top&quot;</span> Width=<span class="str">&quot;200&quot;</span> Text=<span class="str">&quot;{Binding Path=PersonData.Address, Mode=TwoWay}&quot;</span> /&gt;
        
    &lt;/Grid&gt;
&lt;/UserControl&gt;</pre>
<p><br>
Now if all the things are working correctly you can run the application. And you will see the nice interface without having exceptions.<br>
<br>
You can download the source code from here for this example : <a href="http://www.4shared.com/file/sd9w-9Ns/MVVMSample.html">
Download Here </a></p>
