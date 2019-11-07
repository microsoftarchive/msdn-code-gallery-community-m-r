# Random Numbers
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- C#
- Console
- Visual Studio 2013
## Topics
- For Loop
- Random Number Generator
## Updated
- 05/05/2015
## Description

<h1>Introduction</h1>
<p><em>This is a console application that simulates two dice being rolled 100 times (as a pair).&nbsp; After the dice are rolled 100 times, any doubles that are rolled are displayed.<br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>There are no special requirements for building this application other than having Visual Studio installed.&nbsp; This is a C# console application, so the code will differ on a form application.<br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><em>This simulator helps developers understand how the Random() class is used ot generate sudo-random numbers and how to use loops to generate more than one random number.&nbsp; This will help solve problems for programs that need to generate random
 numbers for a number of reasons, including lottery programs or ID numbers.&nbsp; The code snippet will show the loop for generating more than one random number, as well as the types used to define the variables for die 1 and die 2.&nbsp; Generating random
 numbers can be useful for mathematical problems such as proability or statistical analysis.<br>
</em></em></p>
<p><em><em>&nbsp;</em></em><br>
<em>Below is the console application that will simulate 100 dice rolls for 2 dice and output each roll number (e.g &quot;Roll 1:&quot;) along with the numbers on the dice that were rolled (5 2).&nbsp; If any doubles are rolled, they are announced as well.</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEyes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare random number varable
            Random randNum = new Random();
            int rollNum = 1;
            int die1;
            int die2;
            
            /*
             * The random number sequence starts at 1
             * and while it's less than 101 it increments
             * by one more random number.  This will
             * give us a total of 100 random numbers
             * between 1 and 6.
             * 
             */

            for (rollNum = 1; rollNum &lt; 101; rollNum&#43;&#43;)
            {
                // Generate random number
                die1 = randNum.Next(1, 7);
                die2 = randNum.Next(1, 7);

                Console.WriteLine(&quot;Roll &quot; &#43; rollNum &#43; &quot;: &quot; &#43; die1 &#43; &quot; &quot; &#43; die2);

                /*
                 * If doubles are rolled, display what number roll they were rolled on
                 * as well as the double numbers on the dice
                 * 
                 */

                if (die1 == die2)
                {
                    // Outputs the doubles rolled to the console for each pass of the loop
                    Console.WriteLine(&quot;&quot;);
                    Console.WriteLine(&quot;On roll &quot; &#43; rollNum &#43; &quot; you rolled doubles: &quot; &#43; die1 &#43; &quot; &quot; &#43; die2);
                    Console.WriteLine(&quot;&quot;);
                }
            }
        }
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Threading.Tasks;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;SnakeEyes&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Declare&nbsp;random&nbsp;number&nbsp;varable</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Random&nbsp;randNum&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Random();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;rollNum&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;die1;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;die2;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__mlcom">/*&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;The&nbsp;random&nbsp;number&nbsp;sequence&nbsp;starts&nbsp;at&nbsp;1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;and&nbsp;while&nbsp;it's&nbsp;less&nbsp;than&nbsp;101&nbsp;it&nbsp;increments&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;by&nbsp;one&nbsp;more&nbsp;random&nbsp;number.&nbsp;&nbsp;This&nbsp;will&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;give&nbsp;us&nbsp;a&nbsp;total&nbsp;of&nbsp;100&nbsp;random&nbsp;numbers&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;between&nbsp;1&nbsp;and&nbsp;6.&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*/</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(rollNum&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;rollNum&nbsp;&lt;&nbsp;<span class="cs__number">101</span>;&nbsp;rollNum&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Generate&nbsp;random&nbsp;number</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;die1&nbsp;=&nbsp;randNum.Next(<span class="cs__number">1</span>,&nbsp;<span class="cs__number">7</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;die2&nbsp;=&nbsp;randNum.Next(<span class="cs__number">1</span>,&nbsp;<span class="cs__number">7</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Roll&nbsp;&quot;</span>&nbsp;&#43;&nbsp;rollNum&nbsp;&#43;&nbsp;<span class="cs__string">&quot;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;die1&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;die2);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__mlcom">/*&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;If&nbsp;doubles&nbsp;are&nbsp;rolled,&nbsp;display&nbsp;what&nbsp;number&nbsp;roll&nbsp;they&nbsp;were&nbsp;rolled&nbsp;on&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;as&nbsp;well&nbsp;as&nbsp;the&nbsp;double&nbsp;numbers&nbsp;on&nbsp;the&nbsp;dice&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*/</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(die1&nbsp;==&nbsp;die2)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Outputs&nbsp;the&nbsp;doubles&nbsp;rolled&nbsp;to&nbsp;the&nbsp;console&nbsp;for&nbsp;each&nbsp;pass&nbsp;of&nbsp;the&nbsp;loop</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;On&nbsp;roll&nbsp;&quot;</span>&nbsp;&#43;&nbsp;rollNum&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;you&nbsp;rolled&nbsp;doubles:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;die1&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;die2);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<h1>More Information</h1>
<p><em>For more information on the Random() class, see <a href="https://msdn.microsoft.com/en-us/library/system.random%28v=vs.110%29.aspx">
https://msdn.microsoft.com/en-us/library/system.random%28v=vs.110%29.aspx</a></em></p>
<p><em>For more information on the for loop, see <a href="https://msdn.microsoft.com/en-us/library/ch45axte.aspx">
https://msdn.microsoft.com/en-us/library/ch45axte.aspx</a></em></p>
