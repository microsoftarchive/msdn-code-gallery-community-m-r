# Print Spooler
## Requires
- Visual Studio 2008
## License
- Apache License, Version 2.0
## Technologies
- Printing
## Topics
- Printing
## Updated
- 05/26/2012
## Description

<p><img src="57814-27-05-2012%2004.48.45.jpg" alt="" width="488" height="126"></p>
<p>This is a simple Print Spooler application that was requested in the Code Gallery.</p>
<p>In computer science, spool refers to the process of placing data in a temporary working area for another program to process. The most common use is in writing files on a magnetic tape or disk and entering them in the work queue (possibly just linking it
 to a designated folder in the file system) for another process. Spooling is useful because devices access data at different rates. Spooling allows one program to assign work to another without directly communicating with it.</p>
<p>It's a simple application, which after running &#43; selecting a spooler folder, starts printing, then deleting the *.txt files in that folder, &#43; also sets up a FileSystemWatcher that watches that folder for newly added text files.<br>
it works with the ThreadPool, sending any existing &#43; any newly added text files to the ProcessItem method, which opens (ProcessWindowStyle.Hidden), prints, then deletes the files.</p>
<p>If you have any questions, or any suggestions for improvement, please let me know in the Questions &#43; Answer section of this page. If you find this example useful, take the time to rate this article. Thanks for taking the time to look at this example.</p>
