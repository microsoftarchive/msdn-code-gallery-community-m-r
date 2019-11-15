# NetDash Server Monitoring
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- C#
- IIS
- C# Language
- ASP.NET MVC 5
## Topics
- C#
- ASP.NET
- IIS
- ASP.NET MVC
- Performance
- Networking
- Storage
## Updated
- 05/16/2014
## Description

<p><a href="https://camo.githubusercontent.com/74af56174bd80da7b7eca4ca203914cb61938973/687474703a2f2f6936302e74696e797069632e636f6d2f3272356474687a2e706e67" target="_blank"><img src="https://camo.githubusercontent.com/74af56174bd80da7b7eca4ca203914cb61938973/687474703a2f2f6936302e74696e797069632e636f6d2f3272356474687a2e706e67" alt="NetDash Logo"></a></p>
<p><a href="https://camo.githubusercontent.com/74af56174bd80da7b7eca4ca203914cb61938973/687474703a2f2f6936302e74696e797069632e636f6d2f3272356474687a2e706e67" target="_blank"></a>A small web-based monitoring dashboard for your Windows pc/server writen in C#
 and MVC &#43; Chart.js.</p>
<p>The dashboard is built using only C# libraries available in the main C# distribution, trying to create a small list of dependencies without the need of installing many packages or libraries.</p>
<p><strong>Current dependencies</strong>:</p>
<ul class="task-list">
<li>Net Framework 4 </li><li>C# MVC </li><li>AttributeRouting </li><li>SQLite </li></ul>
<p><strong>Localization</strong>:</p>
<ul class="task-list">
<li>(1) &quot;App_Data\Localization\en-EN.txt&quot; copy this file and translate the language you want to use.
</li><li>(2) &quot;App_Data\Setting.ini&quot; LANGUAGE line in the file that you want to use the written language. Example: LANGUAGE=en-EN or LANGUAGE=tr-TR
</li><li>(3) Started to use in their own language </li></ul>
<p><strong>Login info</strong></p>
<pre><code>user: admin
pass: admin123
</code></pre>
<p><a href="https://camo.githubusercontent.com/25acf97487243789a2f982a46e8d5d00f34467bb/687474703a2f2f6935392e74696e797069632e636f6d2f777566316e362e706e67" target="_blank"><img src="https://camo.githubusercontent.com/25acf97487243789a2f982a46e8d5d00f34467bb/687474703a2f2f6935392e74696e797069632e636f6d2f777566316e362e706e67" alt="NetDash"></a></p>
<p><strong>NetDash Settings</strong></p>
<p>The only settings currently available which you can modify are the refresh rates for the different data tables. There are 3 different refresh settings under&nbsp;<em>netdash/App_data/Setting.ini</em>&nbsp;and values are in miliseconds:</p>
<ul class="task-list">
<li>TIME_JS_REFRESH = 30000 #30 seconds </li><li>TIME_JS_REFRESH_LONG = 120000 #120 seconds </li><li>TIME_JS_REFRESH_NET = 2000 #2 seconds </li></ul>
<p>You can modify any of the values to whatever you would like the new refresh rate to be. Restart the webserver after each update to the Setting.ini file.</p>
<p>The tables and the refresh settings are as follows:</p>
<ul class="task-list">
<li>Memory Usage - TIME_JS_REFRESH </li><li>Load Average - TIME_JS_REFRESH </li><li>CPU Usage - TIME_JS_REFRESH </li><li>Traffic Usage - TIME_JS_REFRESH_NET </li><li>Disk Reads/Writes - TIME_JS_REFRESH_NET </li><li>Uptime - TIME_JS_REFRESH_LONG </li><li>Disk Usage - TIME_JS_REFRESH_LONG </li><li>Online Users - TIME_JS_REFRESH_LONG </li><li>Processes - TIME_JS_REFRESH_LONG </li><li>Netstat - TIME_JS_REFRESH_LONG </li></ul>
<p><strong>Remote data retrieval</strong></p>
<p>NetDash remote data retrieval</p>
<p><strong>App_Data\Setting.ini</strong></p>
<pre><code>; Remote or Local server name (Default local name machine name : .)
; Sample remote server SERVERNAME=0.0.0.0
SERVERNAME=ipaddress

; Remote server login info (Ony remote server)

REMOTE_DOMAIN=domain.com
REMOTE_USERNAME=Administrator
REMOTE_PASSWORD=password
</code></pre>
<p>NetDash will allow you to retrieve data remotely if needed. This can be useful if you want to store any of the data in a database or another application.</p>
<ul class="task-list">
<li>/info/uptime/ - Uptime </li><li>/info/platform/hostname/ - Hostname </li><li>/info/platform/osname/ - OS Name </li><li>/info/platform/kernel/ - Kernel </li><li>/info/getcpus/cpucount/ - Number of CPU cores </li><li>/info/getcpus/cputype/ - Type/Name of CPU </li><li>/info/memory/ - Memory Usage </li><li>/info/cpuusage/ - CPU Usage in percentage(%), free and used </li><li>/info/getdisk/ - Disk Usage </li><li>/info/getusers/ - Online Users </li><li>/info/getips/ - IP Addresses </li><li>/info/gettraffic/ - Internet Traffic </li><li>/info/getdiskio/ - Disk Reads/Writes </li><li>/info/proc/ - Running Processes </li><li>/info/loadaverage/ - Load Average </li><li>/info/getnetstat/ - Netstat </li></ul>
<p>To see the format of the JSON returned datasets or data you can access any of the URLs from your browser ex.&nbsp;<a href="http://domain.com/info/uptime/">http://domain.com/info/uptime/</a></p>
<p><strong>OS Support</strong></p>
<ul class="task-list">
<li>NetDash was tested and runs under the following OSs: </li><li>Windows 2000 NT </li><li>Windows 2003 Server </li><li>Windows 2008 Server </li><li>Windows 2012 Server </li></ul>
<p><strong>Credits</strong></p>
<p>Dashboard Template, Bootstrap, Font Awesome</p>
<p><strong>LICENSE</strong></p>
<p>The MIT License (MIT)</p>
<p>Copyright (c) 2014 Yasin Kuyu -&nbsp;<a href="https://twitter.com/yasinkuyu">https://twitter.com/yasinkuyu</a>&nbsp;-&nbsp;<a href="https://github.com/yasinkuyu/">https://github.com/yasinkuyu/</a></p>
<p>Permission is hereby granted, free of charge, to any person obtaining a copy<br>
of this software and associated documentation files (the &quot;Software&quot;), to deal<br>
in the Software without restriction, including without limitation the rights<br>
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell<br>
copies of the Software, and to permit persons to whom the Software is<br>
furnished to do so, subject to the following conditions:</p>
<p>The above copyright notice and this permission notice shall be included in all<br>
copies or substantial portions of the Software.</p>
<p>THE SOFTWARE IS PROVIDED &quot;AS IS&quot;, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR<br>
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,<br>
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE<br>
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER<br>
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,<br>
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE<br>
SOFTWARE.</p>
