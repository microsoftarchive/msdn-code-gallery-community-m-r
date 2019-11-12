# Micro Framework Tetris
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- Games
- .NET Micro Framework
## Topics
- C#
- Games
## Updated
- 10/28/2014
## Description

<p><span style="font-size:2em">Introduction</span></p>
<p><em>Tetris game developed using C# and .NET Micro Framework port for <span>STM32F429</span> discovery board. The poprose of this application is to show capabilities of NETMF with graphical displays.</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>For building the sample&nbsp;<em>STM32F429 &nbsp;discovery board is required. It is highly avaliable in most countries at average cost below 30$. It is also possible to run application on other .NET Micro Framewotk devices, by changing setting class
 but be aware: game use fixed sized bitmaps so that it is decicated for 240x320 screens.</em></em></p>
<p><em><em>Sample was compiled using Visual Studio 2012 and .NET Micro Framework version 4.3.</em></em></p>
<p><em><em><em><em>Application uses .NET Micro Framework port avaliable at ST site(check links below). Instructions how to install the port on the board are also avaliable on this site.</em></em><br>
</em></em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>Application is simple tetris game written on STM32F4 processor using C#. Is uses only
<span>peripherals avaliable on discovery board, no external components are needed. Sample screen from the working application are provided below:</span></p>
<p><span><img id="126302" src="126302-uberfocia.jpg" alt="" width="812" height="437"><br>
</span></p>
<h1>Game Controlls</h1>
<p><span style="font-size:xx-small">During game initialization and after finishing the game: just tap the screen to move to the next game state.</span></p>
<p><span style="font-size:xx-small">During the game:</span></p>
<p><span style="font-size:xx-small">Screen is divided fot three sectors:</span></p>
<p><span style="font-size:xx-small"><img id="126299" src="126299-screen.bmp" alt="" width="150" height="215"></span></p>
<ul>
<li><span style="font-size:xx-small">Tapping on sector 3 makes brick rapidly fall to the end of the screen</span>
</li><li><span style="font-size:xx-small">Tapping sector 1 or 2 while board user button released makes brick move left-or-right</span>
</li><li><span style="font-size:xx-small">Tapping sector 1 or 2 while board user button pressed makes brick rotate left-or-right</span>
</li></ul>
<h1><span>Source Code Files</span></h1>
<p><span>Folders:</span></p>
<ul>
<li>Game - Gamplay objects like brick, game board ... </li><li><em>Helpers - Helper classes</em> </li><li><em>Presentation - Presentaion objects including main window</em> </li><li><em>Resources - Classes managing resources</em> </li></ul>
<p>Also, there are many comments in source code.</p>
<h1>More Information</h1>
<ul>
<li><a href="http://www.st.com/web/en/catalog/tools/PF260087">.NET Micro Framework port for
<span>STM32F429 discovery board</span></a> </li><li><a href="http://www.st.com/web/catalog/tools/FM116/SC959/SS1532/PF259090">STM32F429 discovery board description</a>
</li><li><a href="http://www.netmf.com/">.NET Micro Framework Site</a> </li><li><a href="http://en.wikipedia.org/wiki/Tetris">Information about tetris game</a>
</li><li><a href="http://warsztat.gd/projects/Micro_Framework_Tetris">Project on polish gamedev site - (pl.)</a>
</li></ul>
