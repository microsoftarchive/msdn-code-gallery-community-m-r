# MVVM pattern and Reactive programming sample
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- WPF
- MVVM
- ReactiveProperty
## Topics
- MVVM
## Updated
- 08/28/2015
## Description

<h1>Introduction</h1>
<p>This sample program has been made with Model View ViewModel pattern and Reactive Programming.&nbsp;</p>
<p>MVVM Light Toolkit is used for the Model View ViewModel pattern. Reactive Extensions is used for the Reactive Programming.ReactiveProperty is used for the MVVM &#43; Rx.</p>
<ul>
<li>MVVM Light Toolkit<br>
https://mvvmlight.codeplex.com/ </li><li>Reactive Extensions<br>
https://rx.codeplex.com/ </li><li>ReactiveProperty<br>
https://github.com/runceel/ReactiveProperty </li></ul>
<h2>How to use</h2>
<p>When 'add' button clicked, a new record is inserted into DataGrid.</p>
<p><img id="133818" alt="" src="133818-figure1.png" width="398" height="257"></p>
<p>When record is selected and 'edit' button is &nbsp;clicked, then edit dialog is displayed..</p>
<p><img id="133819" alt="" src="133819-figure2.png" width="397" height="257"></p>
<ul>
<li>Update button<br>
Update data and close window.&nbsp; </li><li>Delete button<br>
Delete data and close window.&nbsp; </li><li>Cancel button<br>
Close window.&nbsp; </li></ul>
<h1>Building the Sample</h1>
<ol>
<li>Download sample program. </li><li>Restore NuGet package. </li><li>Run application. </li></ol>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<h2>Model classes</h2>
<p>The PeopleMaster class loads Person class. The PersonDetail class provides editing functions for the Person class data.In this sample, Messenger class is used for collaborating with PeopleMaster class and PersonDetail class.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// PersonDetail class
public void Update()
{
    this.repository.Update(this.EditTarget);
    this.messenger.Send(new PersonChangeMessage(ChangeKind.Update, this.EditTarget));
}

public void Delete()
{
    this.repository.Delete(this.EditTarget.Id);
    this.messenger.Send(new PersonChangeMessage(ChangeKind.Delete, this.EditTarget));
}


// PeopleMaster class
private void PersonChangedReceived(PersonChangeMessage message)
{
    switch (message.ChangeKind)
    {
        case ChangeKind.Delete:
            this.People.Remove(this.People.First(x =&gt; x.Id == message.Content.Id));
            break;
        case ChangeKind.Update:
            var p = this.People.First(x =&gt; x.Id == message.Content.Id);
            p.Name = message.Content.Name;
            p.Age = message.Content.Age;
            break;
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;PersonDetail&nbsp;class</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Update()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.repository.Update(<span class="cs__keyword">this</span>.EditTarget);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.messenger.Send(<span class="cs__keyword">new</span>&nbsp;PersonChangeMessage(ChangeKind.Update,&nbsp;<span class="cs__keyword">this</span>.EditTarget));&nbsp;
}&nbsp;
&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Delete()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.repository.Delete(<span class="cs__keyword">this</span>.EditTarget.Id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.messenger.Send(<span class="cs__keyword">new</span>&nbsp;PersonChangeMessage(ChangeKind.Delete,&nbsp;<span class="cs__keyword">this</span>.EditTarget));&nbsp;
}&nbsp;
&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;PeopleMaster&nbsp;class</span>&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;PersonChangedReceived(PersonChangeMessage&nbsp;message)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(message.ChangeKind)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;ChangeKind.Delete:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.People.Remove(<span class="cs__keyword">this</span>.People.First(x&nbsp;=&gt;&nbsp;x.Id&nbsp;==&nbsp;message.Content.Id));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;ChangeKind.Update:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;p&nbsp;=&nbsp;<span class="cs__keyword">this</span>.People.First(x&nbsp;=&gt;&nbsp;x.Id&nbsp;==&nbsp;message.Content.Id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Name&nbsp;=&nbsp;message.Content.Name;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Age&nbsp;=&nbsp;message.Content.Age;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h2>ViewModel classes</h2>
<p>ViewModel classes use ReactiveProperty. ReactiveProperty instance is created from Model instance which implements INotifyPropertyChanged interface.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// Name is ReactiveProperty&lt;string&gt;.
this.Name = this.Person.ObserveProperty(x =&gt; x.Name)
    .ToReactiveProperty()
    .SetValidateAttribute(() =&gt; this.Name);
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Name&nbsp;is&nbsp;ReactiveProperty&lt;string&gt;.</span>&nbsp;
<span class="cs__keyword">this</span>.Name&nbsp;=&nbsp;<span class="cs__keyword">this</span>.Person.ObserveProperty(x&nbsp;=&gt;&nbsp;x.Name)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveProperty()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.SetValidateAttribute(()&nbsp;=&gt;&nbsp;<span class="cs__keyword">this</span>.Name);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>And ReactiveCommand is converted from IObservable&lt;bool&gt;. It performs the processing in the Subscribe method.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// MainWindowViewModel constructor.
this.EditCommand = this.SelectedPerson
    .Select(x =&gt; x != null)
    .ToReactiveCommand();
this.EditCommand.Subscribe(_ =&gt; 
    {
        app.Detail.SetEditTarget(this.SelectedPerson.Value.Person.Id);
        this.MessengerInstance.Send(new MessageBase(this, &quot;EditWindow&quot;));
    });
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;MainWindowViewModel&nbsp;constructor.</span>&nbsp;
<span class="cs__keyword">this</span>.EditCommand&nbsp;=&nbsp;<span class="cs__keyword">this</span>.SelectedPerson&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.Select(x&nbsp;=&gt;&nbsp;x&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ToReactiveCommand();&nbsp;
<span class="cs__keyword">this</span>.EditCommand.Subscribe(_&nbsp;=&gt;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;app.Detail.SetEditTarget(<span class="cs__keyword">this</span>.SelectedPerson.Value.Person.Id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.MessengerInstance.Send(<span class="cs__keyword">new</span>&nbsp;MessageBase(<span class="cs__keyword">this</span>,&nbsp;<span class="cs__string">&quot;EditWindow&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>ReactiveProperty can bind View - ViewModel - Model very easily. It's really powerful. Please try to use!</p>
