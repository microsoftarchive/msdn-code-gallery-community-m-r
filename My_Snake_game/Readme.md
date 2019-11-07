# My_Snake_game
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- Visual Studio 2012
- Graph API
- Graphics and Gaming
## Topics
- C#
- Set Operators
- C# Language Features
- Graphics Functions
## Updated
- 09/15/2014
## Description

<h1>Introduction</h1>
<p><em><strong>Snake</strong></em><span>&nbsp;is a video game concept which originated during the late 1970s in arcades</span><span>. The name applies to the general game design; the original was not named Snake, and there is no definitive version of the game.
 Its simplicity has led to many implementations of the Snake concept. After it became the standard pre-loaded game on Nokia Mobile Phones</span><span>&nbsp;in 1998,<span>Yes, again a snake game if you aren't bored yet. You must have seen a lot of snake games
 created in different languages. But have you ever seen a snake game that is controllable both by keyboard and joystick? Yes, this is the difference between snake games and&nbsp;</span><strong>Engerek</strong><span>.</span></span></p>
<h1><span>Building the Sample</span></h1>
<p><span><span>Update</span><br>
<span>-Check Input</span><br>
<span>-Update Player</span><br>
<span>-Check Collision</span><br>
<br>
<span>Render</span><br>
<span>-Draw Player</span><br>
<span>-Draw Fruit</span><br>
<span>-Draw Score</span><br>
</span></p>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><span>&quot;Snake&quot; is probably one of the most simplest games to understand (along with many other classic arcade games). Which is why it's a great example to game programming. It requires all of the necessary requirements for a game.</span><br>
<br>
<strong><em>Game Logic</em></strong><br>
<span>Update</span><br>
<span>-Check Input</span><br>
<span>-Update Player</span><br>
<span>-Check Collision</span><br>
<br>
<span>Render</span><br>
<span>-Draw Player</span><br>
<span>-Draw Fruit</span><br>
<span>-Draw Score</span><br>
<br>
<strong><em>Concept</em></strong><br>
<span>Snake is an easy game to understand. You start off as a &quot;head&quot; or a really, really small snake, roam around the playing field to collect food and you grow. You try to eat yourself, you die, if you run out of bounds, you die. Not much to it. So let's dig
 a bit deeper and understand how it works.</span><br>
<br>
<span>Each part of the snake can be considered an instance of an object, we'll call it &quot;SnakePart&quot;. Each part follows the part in front of it. The snake and it's body, usually follow along a grid-based path. Each part is assigned an X and a Y coordinate, then
 gets rendered to that coordinate on screen. The food can be an instance of &quot;SnakePart&quot; rather then creating a new class. Set it's X and Y randomly to fit into the screen and then render it.</span><br>
<br>
<span>That's basically it for the concept of the snake game, can't really go much deeper with it.</span></em></p>
<p>&nbsp;</p>
<p><span style="font-size:x-large"><strong><a class="title" title="Vote ME" href="http://mvp.microsoft.com/en-us/nominate-an-mvp.aspx">Vote me &nbsp;</a></strong></span></p>
<p><img id="125478" src="125478-game1.png" alt="" width="981" height="548"></p>
<p><img id="125479" src="125479-game2.png" alt="" width="992" height="550"></p>
<p><img id="125480" src="125480-high%20score%20mine.png" alt="" width="1074" height="546"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">Click here to add your code snippet. private List&lt;Circle&gt; Snake = new List&lt;Circle&gt;();
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            //Set settings to default
            new Settings();

            //Set game speed and start timer
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick &#43;= UpdateScreen;
            gameTimer.Start();

            //Start New game
            StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;

            //Set settings to default
            new Settings();

            //Create new player object
            Snake.Clear();
            
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);


            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

        }
</pre>
<div class="preview">
<pre class="csharp">Click&nbsp;here&nbsp;to&nbsp;add&nbsp;your&nbsp;code&nbsp;snippet.&nbsp;<span class="cs__keyword">private</span>&nbsp;List&lt;Circle&gt;&nbsp;Snake&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;Circle&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Circle&nbsp;food&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Circle();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Form1()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;settings&nbsp;to&nbsp;default</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Settings();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;game&nbsp;speed&nbsp;and&nbsp;start&nbsp;timer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gameTimer.Interval&nbsp;=&nbsp;<span class="cs__number">1000</span>&nbsp;/&nbsp;Settings.Speed;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gameTimer.Tick&nbsp;&#43;=&nbsp;UpdateScreen;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gameTimer.Start();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Start&nbsp;New&nbsp;game</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StartGame();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;StartGame()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lblGameOver.Visible&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Set&nbsp;settings&nbsp;to&nbsp;default</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;Settings();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Create&nbsp;new&nbsp;player&nbsp;object</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Snake.Clear();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Circle&nbsp;head&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Circle();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;head.X&nbsp;=&nbsp;<span class="cs__number">10</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;head.Y&nbsp;=&nbsp;<span class="cs__number">5</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Snake.Add(head);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lblScore.Text&nbsp;=&nbsp;Settings.Score.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GenerateFood();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>source code file name #1 - Form1.cs</em> </li><li><em><em>source code file name #2 - Program.cs</em></em> </li></ul>
<h1>More Information</h1>
<p><span>Creating the form is easy. All we need are two controls. A PictureBox, for rendering the game, and a timer, for updating the game every X miliseconds. Go ahead and drag a PictureBox and a Timer onto your form. We'll be modifying some of it's properties,
 we'll also be modifying some of the Form properties. (Feel free to change any of these properties, these are just for personal preference and you can design yours to however you choose.)</span><br>
<br>
<em>Form Properties</em><br>
<span>FormBorderStyle&nbsp; FixedSingle<br>
MaximizeBox&nbsp; &nbsp; &nbsp; False<br>
Size&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;362, 304<br>
StartPosition&nbsp; &nbsp; CenterScreen<br>
Text&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Snake</span><br>
<br>
<em>PictureBox Properties</em><br>
<span>(Name)&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;pbCanvas<br>
BackColor&nbsp; &nbsp; &nbsp; &nbsp; CornFlowerBlue<br>
Location&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;13, 13<br>
Margin&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;4, 4, 4, 4<br>
Size&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;320, 240</span><br>
<br>
<em>Timer Properties</em><br>
<span>(Name)&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;gameTimer<br>
(We're going to create the rest of the timer inside the actual code)</span></p>
