# Modeless Window in WPF
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- WPF
## Topics
- WPF
- Modeless
- Modeless Dialog
## Updated
- 11/04/2011
## Description

<p>&nbsp;</p>
<p>&nbsp;<img src="45572-untitled.png" alt="" width="609" height="480"></p>
<p><strong><span style="font-size:medium">What is modeless window in system?</span></strong> Please refer to this article&nbsp;<a href="http://www.usabilityfirst.com/glossary/modeless/">http://www.usabilityfirst.com/glossary/modeless/</a>. In short, we could
 say one modal window only belongs one parent windowm and in the modal status, we could continue to active other windows, we can say the modal window is modeless.</p>
<p>In MFC, we could implement the modeless form via Windows APIs:</p>
<p>SetForegroundWindow function: <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx">
http://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx</a></p>
<p>and DestroyWindow function: <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms632682(v=vs.85).aspx">
http://msdn.microsoft.com/en-us/library/windows/desktop/ms632682(v=vs.85).aspx</a></p>
<p>Two links can provide helps:</p>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/zhk0y9cw.aspx">http://msdn.microsoft.com/en-us/library/zhk0y9cw.aspx</a>
</li><li><a href="http://www.codeproject.com/KB/dialog/gettingmodeless.aspx">http://www.codeproject.com/KB/dialog/gettingmodeless.aspx</a>
</li></ul>
<p>But in WPF, we only have the modal dialog in default, use ShowDialog method (<a href="http://msdn.microsoft.com/en-us/library/system.windows.window.showdialog.aspx">http://msdn.microsoft.com/en-us/library/system.windows.window.showdialog.aspx</a>)&nbsp;we
 could show any Windows as modal dialog.</p>
<p><span style="font-size:small"><strong>If you want to implement the modeless dialog in WPF, my sample can help you.</strong></span> My sample uses three APIs to control the window handle.</p>
<ul>
<li>EnableWindow function - <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms646291(v=vs.85).aspx">
http://msdn.microsoft.com/en-us/library/windows/desktop/ms646291(v=vs.85).aspx</a>
</li><li>SetForegroundWindow function - <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx">
http://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx</a>
</li><li>GetWindowLong function - <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633584(v=vs.85).aspx">
http://msdn.microsoft.com/en-us/library/windows/desktop/ms633584(v=vs.85).aspx</a>
</li></ul>
<p>In the parent Window, we could start to show a modal window via the below code:</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">      ownerHandle = (new System.Windows.Interop.WindowInteropHelper(this.Owner)).Handle;
      handle = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;
      NativeMethods.EnableWindow(handle, true);
      NativeMethods.SetForegroundWindow(handle);

...

    public bool? ShowModelessDialog()
    {
      NativeMethods.EnableWindow(ownerHandle, false);
      new ShowAndWaitHelper(this).ShowAndWait();
      return ModalDialogResult;
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ownerHandle&nbsp;=&nbsp;(<span class="cs__keyword">new</span>&nbsp;System.Windows.Interop.WindowInteropHelper(<span class="cs__keyword">this</span>.Owner)).Handle;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;handle&nbsp;=&nbsp;(<span class="cs__keyword">new</span>&nbsp;System.Windows.Interop.WindowInteropHelper(<span class="cs__keyword">this</span>)).Handle;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NativeMethods.EnableWindow(handle,&nbsp;<span class="cs__keyword">true</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NativeMethods.SetForegroundWindow(handle);&nbsp;
&nbsp;
...&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>?&nbsp;ShowModelessDialog()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NativeMethods.EnableWindow(ownerHandle,&nbsp;<span class="cs__keyword">false</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;ShowAndWaitHelper(<span class="cs__keyword">this</span>).ShowAndWait();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ModalDialogResult;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;And <span style="font-size:small"><strong>what is the ShowAndWaitHelper class?</strong></span> It is a helper class can help us to block the WPF current thread dispatcher, then we could make the modeless dialog block the thread,
 and wait its return dialog result:</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">  internal sealed class ShowAndWaitHelper
  {
    private readonly Window _window;
    private DispatcherFrame _dispatcherFrame;
    internal ShowAndWaitHelper(Window window)
    {
      if (window == null)
      {
        throw new ArgumentNullException(&quot;panel&quot;);
      }
      this._window = window;
    }
    internal void ShowAndWait()
    {
      if (this._dispatcherFrame != null)
      {
        throw new InvalidOperationException(&quot;Cannot call ShowAndWait while waiting for a previous call to ShowAndWait to return.&quot;);
      }
      this._window.Closed &#43;= new EventHandler(this.OnPanelClosed);
      _window.Show();
      this._dispatcherFrame = new DispatcherFrame();
      Dispatcher.PushFrame(this._dispatcherFrame);
    }
    private void OnPanelClosed(object source, EventArgs eventArgs)
    {
      this._window.Closed -= new EventHandler(this.OnPanelClosed);
      if (this._dispatcherFrame == null)
      {
        return;
      }
      this._window.Closed -= new EventHandler(this.OnPanelClosed);
      this._dispatcherFrame.Continue = false;
      this._dispatcherFrame = null;
    }
  }</pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;internal&nbsp;sealed&nbsp;class&nbsp;ShowAndWaitHelper&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;readonly&nbsp;Window&nbsp;_window;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;DispatcherFrame&nbsp;_dispatcherFrame;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;internal&nbsp;ShowAndWaitHelper(Window&nbsp;window)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(window&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">throw</span>&nbsp;<span class="js__operator">new</span>&nbsp;ArgumentNullException(<span class="js__string">&quot;panel&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._window&nbsp;=&nbsp;window;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;internal&nbsp;<span class="js__operator">void</span>&nbsp;ShowAndWait()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__operator">this</span>._dispatcherFrame&nbsp;!=&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">throw</span>&nbsp;<span class="js__operator">new</span>&nbsp;InvalidOperationException(<span class="js__string">&quot;Cannot&nbsp;call&nbsp;ShowAndWait&nbsp;while&nbsp;waiting&nbsp;for&nbsp;a&nbsp;previous&nbsp;call&nbsp;to&nbsp;ShowAndWait&nbsp;to&nbsp;return.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._window.Closed&nbsp;&#43;=&nbsp;<span class="js__operator">new</span>&nbsp;EventHandler(<span class="js__operator">this</span>.OnPanelClosed);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_window.Show();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._dispatcherFrame&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DispatcherFrame();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dispatcher.PushFrame(<span class="js__operator">this</span>._dispatcherFrame);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;<span class="js__operator">void</span>&nbsp;OnPanelClosed(object&nbsp;source,&nbsp;EventArgs&nbsp;eventArgs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._window.Closed&nbsp;-=&nbsp;<span class="js__operator">new</span>&nbsp;EventHandler(<span class="js__operator">this</span>.OnPanelClosed);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__operator">this</span>._dispatcherFrame&nbsp;==&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._window.Closed&nbsp;-=&nbsp;<span class="js__operator">new</span>&nbsp;EventHandler(<span class="js__operator">this</span>.OnPanelClosed);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._dispatcherFrame.Continue&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._dispatcherFrame&nbsp;=&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;When we close the modeless dialog, we should set the parent settings back via the following code:</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.Closing -= new System.ComponentModel.CancelEventHandler(Window_Closing);
      NativeMethods.EnableWindow(handle, false);
      NativeMethods.EnableWindow(ownerHandle, true);
    }</pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">void</span>&nbsp;Window_Closing(object&nbsp;sender,&nbsp;System.ComponentModel.CancelEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.Closing&nbsp;-=&nbsp;<span class="js__operator">new</span>&nbsp;System.ComponentModel.CancelEventHandler(Window_Closing);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NativeMethods.EnableWindow(handle,&nbsp;false);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NativeMethods.EnableWindow(ownerHandle,&nbsp;true);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
