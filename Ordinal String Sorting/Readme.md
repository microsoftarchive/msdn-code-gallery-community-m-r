# Ordinal String Sorting
## Requires
- Visual Studio 2010
## License
- MS-LPL
## Technologies
- C# Language
## Topics
- Sorting
- String Methods
## Updated
- 02/18/2016
## Description

<div class="WordSection1">
<h2>Ordinal String Sorting</h2>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal">Have you ever desired to have in your code a way to order a sequence of strings in the same way as Windows does for files whose name contains a mix of letters and numbers? Ordinal string sorting is not natively supported in C# but can
 be easily implemented by specialising a string comparer and adding a few extensions to the enumerable string collection.</p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal"><strong>The ordinal order</strong></p>
<p class="MsoNormal"><a href="http://en.wikipedia.org/wiki/Ordinal_number_(linguistics)">Ordinal numbers</a> are the words representing the rank of a number with respect to some order, in particular order or position (i.e. first, second, third, etc.).</p>
<p class="MsoNormal">As humans, we learn to put numbers in the correct order as a primary skill, according to their ordinality.&nbsp; We can also order strings containing letters and numbers very easily just by looking at the alphabetical order of letters
 and the ordinality of a number. For example, the string &ldquo;Year 1&rdquo; precedes &ldquo;Year 2&rdquo;, which also comes before &ldquo;Year 10&rdquo; in this natural order. Basically, we know how to separate the alphabetical part of the sentence from the
 numerical one, and then we are able to sort the full string by applying two different &ldquo;sorting algorithms&rdquo; (alphabetical and ordinal, respectively). Variations of these algorithms may apply in different cultures, but the principle is the same.</p>
<p class="MsoNormal">Computers, on the converse, use the <a href="http://en.wikipedia.org/wiki/ASCII">
ASCII table</a> for sorting strings, from the dawn of the informatics era. This table presents a sequence of letters, numbers, punctuation and control characters in a fixed order that tends to mirror the natural order of the alphabet and numbers, and associates
 every entry in the table (a character) to a number. For example, number 65 corresponds to character &ldquo;A&rdquo;, &ldquo;B&rdquo; is 66, &ldquo;C&rdquo; is 67 and so on. Likewise, also characters 0 to 9 have an ASCII number: &ldquo;0&rdquo; is 48, &ldquo;1&rdquo;
 is 49 until &ldquo;9&rdquo;, which is 57. Numbers after 9 corresponds to two or more characters (10 is &ldquo;1&rdquo; and &ldquo;0&rdquo;).</p>
<p class="MsoNormal">Sorting characters according to the ASCII table, which is what computers do, basically implies sorting each character individually according to their position in the table: same &ldquo;sorting algorithm&rdquo; irrespective of the type
 of the character, whether letter, number, punctuation character or other.</p>
<p class="MsoNormal">By applying this logic, strings are compared character by character and then sorted according to the numerical position of the character in the ASCII table. The string &ldquo;Year 1&rdquo; would still be ordered before &ldquo;Year 2&rdquo;,
 as character &ldquo;1&rdquo; appears before &ldquo;2&rdquo; in the table. But &ldquo;Year 10&rdquo; is before &ldquo;Year 2&rdquo; because the &ldquo;1&rdquo; character in &ldquo;10&rdquo; is compared first with &ldquo;2&rdquo; and, again, &ldquo;1&rdquo;
 takes precedence over &ldquo;2&rdquo;, irrespective of the following characters.</p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal"><strong>Standard order in C#</strong></p>
<p class="MsoNormal">Let&rsquo;s make an example in C# of displaying a sequence of strings in the standard order supported by the OrderBy() LINQ extension to enumerable collections.</p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">List&lt;string&gt; files = new List&lt;string&gt;()
{
       &quot;File 1.txt&quot;,
       &quot;File 2.jpg&quot;,
       &quot;File 3.doc&quot;,
       &quot;File 10.txt&quot;,
       &quot;File 11.csv&quot;,
       &quot;File 20.xls&quot;,
       &quot;File 21.ppt&quot;
};
 
foreach (var file in files.OrderBy(&lambda; =&gt; &lambda;))
{
       Console.WriteLine(file);
}</pre>
<div class="preview">
<pre class="csharp">List&lt;<span class="cs__keyword">string</span>&gt;&nbsp;files&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;1.txt&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;2.jpg&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;3.doc&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;10.txt&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;11.csv&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;20.xls&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;File&nbsp;21.ppt&quot;</span>&nbsp;
};&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;file&nbsp;<span class="cs__keyword">in</span>&nbsp;files.OrderBy(&lambda;&nbsp;=&gt;&nbsp;&lambda;))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(file);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="background-color:#ffffff">This simple snippet of C# code orders all strings in the &ldquo;files&rdquo; list by using the OrderBy() LINQ extension, and then prints the result on the output console. As it is visible in
 the following picture, the output of this operation is a list of file names sorted according to the ASCII rule of order of characters.</span></div>
<p></p>
<p class="MsoNormal"><img id="67158" src="67158-picture1.png" alt="" width="237" height="126"></p>
<p class="MsoNormal">Not the result we would like to have, and definitely not user-friendly. So, what can we do about it?</p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal"><strong>Ordinal order in C#</strong></p>
<p class="MsoNormal">Sticking to the example of the file names, Windows already supports ordinal sorting of files by name, as in the following screenshot taken on Windows 7.</p>
<p class="MsoNormal"><img id="67159" src="67159-picture2.png" alt="" width="207" height="218"></p>
<p class="MsoNormal">Much better, isn&rsquo;t it? So Windows is clever enough to think like a human and understand the alphabetical and numerical part of a string separately. Can we do the same in C#?</p>
<p class="MsoNormal">The approach to take for introducing ordinal ordering of a collection of strings in C# starts from the implementation of an ordinal string comparer that contains the logic for understanding what is alphabetical and what is numerical within
 a string. After that, we will introduce a few extensions in the same style as LINQ does, for having a shortcut in an enumerable collection of strings to the ordinal sorting methods.</p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal"><strong>The OrdinalStringComparer class</strong></p>
<p class="MsoNormal">We have seen in the previous example that the easiest way to sort a collection of strings is to use the LINQ extension OrderBy() on an enumerable object. The OrderBy() method has an overload that accepts an IComparer&lt;TKey&gt; for specifying
 a bespoke way of ordering objects of type TKey.</p>
<p class="MsoNormal">The signature of this overload is:</p>
<p class="MsoNormal"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public static IOrderedEnumerable&lt;TSource&gt; OrderBy&lt;TSource, TKey&gt;(
   this IEnumerable&lt;TSource&gt; source,
   Func&lt;TSource, TKey&gt; keySelector,
   IComparer&lt;TKey&gt; comparer);
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;IOrderedEnumerable&lt;TSource&gt;&nbsp;OrderBy&lt;TSource,&nbsp;TKey&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>&nbsp;IEnumerable&lt;TSource&gt;&nbsp;source,&nbsp;
&nbsp;&nbsp;&nbsp;Func&lt;TSource,&nbsp;TKey&gt;&nbsp;keySelector,&nbsp;
&nbsp;&nbsp;&nbsp;IComparer&lt;TKey&gt;&nbsp;comparer);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
<span style="background-color:#ffffff">The key to perform custom comparisons of strings is to implement the IComparer interface, then. This interface exposes only one method, Compare, with the following signature:</span></p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">int Compare(
      T x,
      T y
  );</pre>
<div class="preview">
<pre class="js">int&nbsp;Compare(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;T&nbsp;x,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;T&nbsp;y&nbsp;
&nbsp;&nbsp;);</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;The implementation of Compare in our custom comparer class should perform a comparison of the two objects and return a value indicating whether one is less than, equal to, or greater than the other. A value less than zero
 is returned if x is less than y; zero is returned is x equals y; a value greater than zero is returned if x is greater than y. Comparing null with any reference type is allowed and does not generate an exception. A null reference is considered to be less than
 any reference that is not null.</div>
<p></p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class OrdinalStringComparer : IComparer&lt;string&gt;
{
       private bool _ignoreCase = true;
 
       public OrdinalStringComparer()
              : this(true)
       {
       }
 
       public OrdinalStringComparer(bool ignoreCase)
       {
              _ignoreCase = ignoreCase;
       }
 
       public int Compare(string x, string y)
       {
              // full implementation on the attached solution
       }
}
</pre>
<div class="preview">
<pre class="js">public&nbsp;class&nbsp;OrdinalStringComparer&nbsp;:&nbsp;IComparer&lt;string&gt;&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;bool&nbsp;_ignoreCase&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;OrdinalStringComparer()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="js__operator">this</span>(true)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;OrdinalStringComparer(bool&nbsp;ignoreCase)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_ignoreCase&nbsp;=&nbsp;ignoreCase;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;int&nbsp;Compare(string&nbsp;x,&nbsp;string&nbsp;y)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;full&nbsp;implementation&nbsp;on&nbsp;the&nbsp;attached&nbsp;solution</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
<span style="background-color:#ffffff">The OrdinalStringComparer implements the IComparer&lt;string&gt; interface, and specifically its method Compare(), and also presents an overloade</span><span style="background-color:#ffffff">d constructor for discerning
 between a case-sensitive or insensitive comparison. Clever! </span><span style="font-family:Wingdings">J</span></p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal"><strong>Extensions</strong></p>
<p class="MsoNormal">We can already use our OrdinalStringComparer when ordering a collection of strings via the OrderBy() extension; with reference to the list of file names presented at the beginning of this article, we can simply use the overload of the
 OrderBy() method that accepts an IComparer in input for obtaining the desired result:</p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">foreach (var file in files.OrderBy(&lambda; =&gt; &lambda;, new OrdinalStringComparer()))
{
       Console.WriteLine(file);
}</pre>
<div class="preview">
<pre class="js">foreach&nbsp;(<span class="js__statement">var</span>&nbsp;file&nbsp;<span class="js__operator">in</span>&nbsp;files.OrderBy(&lambda;&nbsp;=&gt;&nbsp;&lambda;,&nbsp;<span class="js__operator">new</span>&nbsp;OrdinalStringComparer()))&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(file);&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal"><img id="67160" src="67160-picture3.png" alt="" width="210" height="124"></p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal">If we were to order the file names in descending order, we would use the OrderByDescending() extension with the explicit call to OrdinalStringComparer:</p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">foreach (var file in files.OrderByDescending(&lambda; =&gt; &lambda;, new OrdinalStringComparer()))
{
       Console.WriteLine(file);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;file&nbsp;<span class="cs__keyword">in</span>&nbsp;files.OrderByDescending(&lambda;&nbsp;=&gt;&nbsp;&lambda;,&nbsp;<span class="cs__keyword">new</span>&nbsp;OrdinalStringComparer()))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(file);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="background-color:#ffffff">What about a more concise way of ordering a collection of strings by adding two more extensions?</span></div>
<p></p>
<p class="MsoListParagraphCxSpFirst" style="text-indent:-18.0pt"><span style="font-family:Symbol">&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span>OrderByOrdinal</p>
<p class="MsoListParagraphCxSpLast" style="text-indent:-18.0pt"><span style="font-family:Symbol">&middot;<span style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span>OrderByOrdinalDescending</p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal">The signature for these extensions, and their overload for allowing case-sensitive sorting, is the following; remember that extension methods must be declared as public static in a static class:</p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public static class StringExtensions
{
       public static IOrderedEnumerable&lt;string&gt; OrderByOrdinal(
              this IEnumerable&lt;string&gt; strings,
              Func&lt;string, string&gt; keySelector);
 
       public static IOrderedEnumerable&lt;string&gt; OrderByOrdinal(
              this IEnumerable&lt;string&gt; strings,
              Func&lt;string, string&gt; keySelector,
              bool ignoreCase);
 
       public static IOrderedEnumerable&lt;string&gt; OrderByOrdinalDescending(
              this IEnumerable&lt;string&gt; strings,
              Func&lt;string, string&gt; keySelector);
 
       public static IOrderedEnumerable&lt;string&gt; OrderByOrdinalDescending(
              this IEnumerable&lt;string&gt; strings,
              Func&lt;string, string&gt; keySelector,
              bool ignoreCase);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;StringExtensions&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;IOrderedEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;OrderByOrdinal(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;strings,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Func&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;keySelector);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;IOrderedEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;OrderByOrdinal(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;strings,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Func&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;keySelector,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;ignoreCase);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;IOrderedEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;OrderByOrdinalDescending(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;strings,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Func&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;keySelector);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;IOrderedEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;OrderByOrdinalDescending(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>&nbsp;IEnumerable&lt;<span class="cs__keyword">string</span>&gt;&nbsp;strings,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Func&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;keySelector,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;ignoreCase);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal">Internally, each method invokes the OrderBy() and OrderByDescending() extensions accordingly, with an explicit call to the OrdinalStringComparer class, using the respective overload for ignoring case-sensitive comparison. So, in our file
 names example, sorting a list of strings by ordinal order would be as simple as:</p>
<p class="MsoNormal" style="margin-bottom:.0001pt; line-height:normal; background:#F2F2F2; text-autospace:none">
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">foreach (var file in files.OrderByOrdinal(&lambda; =&gt; &lambda;))
{
       Console.WriteLine(file);
}</pre>
<div class="preview">
<pre class="js">foreach&nbsp;(<span class="js__statement">var</span>&nbsp;file&nbsp;<span class="js__operator">in</span>&nbsp;files.OrderByOrdinal(&lambda;&nbsp;=&gt;&nbsp;&lambda;))&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(file);&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<p class="MsoNormal">&nbsp;</p>
<p class="MsoNormal">Clever 2.0! <span style="font-family:Wingdings">J</span></p>
<p class="MsoNormal">&nbsp;</p>
</div>
