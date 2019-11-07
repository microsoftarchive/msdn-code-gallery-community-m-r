# Numeric Textbox Library
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- Visual C#
## Topics
- Windows Forms Controls
## Updated
- 03/05/2017
## Description

<h1>Introduction</h1>
<p><em>This control validates input according to selected numeric format, it helps both user and programmer</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>This sample is a Visual Studio 2013 C sharp solution.</em></p>
<ul>
<li><em>You can build only numerictextboxes.dll. Then <span>add the reference to your project using the Browse button in the&nbsp;</span><code>Add Reference</code><span>&nbsp;dialog.</span></em>
</li><li><em>Or you can add numerictextboxes project to your solution. a<span>nd then you add a reference to the your project's classes.</span></em>
</li></ul>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>This project bases from&nbsp;<strong>Reza_m_n_65 </strong>Numerical text box sample. I thank him.</p>
<p><em>Numerical text boxes<span>&nbsp;is a&nbsp;</span><code>System.Windows.Forms.TextBox</code><span>&nbsp;component extension. İt uses Text box onTextchanged event for controlling input.</span></em></p>
<p><em><span>you can select unsigned integer, integer, decimal and float input modes. Also you can restrict decimal digit count.</span></em></p>
<p><em><span>You can select thousands char and you can enable or disable using it.</span></em></p>
<p><em><span>You can check minimum and maximum values and checking enable or disable.</span></em></p>
<p><em><span>İt restricts more than one zero at beginning number.</span></em></p>
<p><em><span>It uses locale numberformatinfo. So there is no problem. local Decimal seperator can be dot or comma.</span></em></p>
<p><em><span>Programmer can obtain input text value from&nbsp;numericalTextBox.NumericValue property as double.</span></em></p>
<p><em><span>Properties seen below Numerictext category and has description &nbsp;in Visual studio designer</span></em></p>
<p>&nbsp;</p>
<p><em><span><br>
</span></em></p>
<p><em><span><img id="170647" src="170647-numer.png" alt="" width="739" height="393"><br>
</span></em></p>
<p><em><span>&nbsp;</span></em><em>NumerictextBox properties;</em></p>
<ul>
<li><em>DecimalNumber get set between 0 and 15 default 15 integer.</em> </li><li><em>Groupsep char get set. You can select every char except ( negative e, E, and decimal char) if user selects this forbidden chars locale group seperator char uses.&nbsp;</em>
</li><li><em>Maxcheck get set default false.</em> </li><li><em>MaxValue get set default 0 double&nbsp;</em> </li><li><em>MinCheck get set default False</em> </li><li><em>MinValue &nbsp;get set defalt 0 double</em> </li><li><em>Number format get set You can select enumarated list default float value</em>
</li><li><em>NumericValue get returns text value as double</em> </li><li><em>UsegroupSeperator false default&nbsp;</em> </li></ul>
<p><em>Numerical text box class has three methods;</em></p>
<p><em>protected override void <strong>OnTextChanged</strong>(EventArgs e) input changes detection, and writing back corrected sting.</em></p>
<p><em>protected string <strong>NormalTextToNumericString</strong>() &nbsp;input arranged here.</em></p>
<p><em>protected double <strong>txttoDouble</strong>(string txt) &nbsp;evaluating value of text<br>
</em></p>
<p><em>You can include <em><strong>code snippets,&nbsp;</strong></em><strong>images</strong>,
<strong>videos</strong>. &nbsp;&nbsp;</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">        protected override void OnTextChanged(EventArgs e)
        {
            if (!string.Equals(this.Text, _oldText)) //change only, if this method not changed this Text
            {                                        // avoid twice execution
                _cursorPositionPlus = 0;
                int SelectionStart = this.SelectionStart;
                int TextLength = this.Text.Length;
                int CursorPositionPlus;
                string Text = NormalTextToNumericString();
                CursorPositionPlus = _cursorPositionPlus;
                if ((!_maxCheck || _textValue &lt;= _maxValue) &amp;&amp; (!_minCheck || _textValue &gt;= _minValue))
                {
                    this.Text = _oldText = Text;
                    this.SelectionStart = SelectionStart &#43; CursorPositionPlus;
                }
                else
                {
                    this.Text = _oldText;
                }
            }
            base.OnTextChanged(e);
        }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OnTextChanged(EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!<span class="cs__keyword">string</span>.Equals(<span class="cs__keyword">this</span>.Text,&nbsp;_oldText))&nbsp;<span class="cs__com">//change&nbsp;only,&nbsp;if&nbsp;this&nbsp;method&nbsp;not&nbsp;changed&nbsp;this&nbsp;Text</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;avoid&nbsp;twice&nbsp;execution</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_cursorPositionPlus&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;SelectionStart&nbsp;=&nbsp;<span class="cs__keyword">this</span>.SelectionStart;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;TextLength&nbsp;=&nbsp;<span class="cs__keyword">this</span>.Text.Length;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;CursorPositionPlus;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;Text&nbsp;=&nbsp;NormalTextToNumericString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CursorPositionPlus&nbsp;=&nbsp;_cursorPositionPlus;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;((!_maxCheck&nbsp;||&nbsp;_textValue&nbsp;&lt;=&nbsp;_maxValue)&nbsp;&amp;&amp;&nbsp;(!_minCheck&nbsp;||&nbsp;_textValue&nbsp;&gt;=&nbsp;_minValue))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Text&nbsp;=&nbsp;_oldText&nbsp;=&nbsp;Text;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SelectionStart&nbsp;=&nbsp;SelectionStart&nbsp;&#43;&nbsp;CursorPositionPlus;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Text&nbsp;=&nbsp;_oldText;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">base</span>.OnTextChanged(e);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Class1.cs #1 - All in one source file.</em> </li><li><em><em>Form1.cs #2 - Using Numeric text box &nbsp;sample form file in numerictextboxsample project.</em></em>
</li></ul>
<h1>More Information</h1>
<p><em>For more information on X, see ...?</em></p>
