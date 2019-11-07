# Pushpin Clustering with the Bing Maps WPF control
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- WPF
- Bing Maps
## Topics
- WPF
- Bing Maps
- Bing Maps Control for WPF
## Updated
- 10/31/2013
## Description

<h1>Introduction</h1>
<p>Often we want to load a lot of data on the map, for example if you were a real estate company you would likely want to load up all the properties that matched a user&rsquo;s search. This could potentially be a couple hundred, if not thousands of results.
 If you are zoomed in close to the map it will be pretty easy to identify each location on the map, but if you zoom out the pushpins will likely overlap and the map will become cluttered. Here is an example of 5000 pushpins that are unclustered:<br>
<br>
<img id="97772" src="97772-unclusteredmap.png" alt="" width="609" height="389"></p>
<p>Clustering is the process of grouping closely positioned locations together and representing them with a single pushpin. When you zoom in those locations will become further apart and will separate into their own pushpins. The client side part of &ldquo;Client
 Side Clustering&rdquo; is clustering the data on the fly in code rather than going back to the server to request more data. When you have a few thousand or less location to display on the map client side clustering can be significantly faster than server side
 clustering and also cuts down on request to your server thus making your application more scalable. Here is an example of the same 5000 pushpins from before being clustered:</p>
<h1><img id="97771" src="97771-clusteredmap.png" alt="" width="606" height="333"></h1>
<p>Note that this code sample requirs the <a href="http://www.microsoft.com/en-us/download/details.aspx?id=27165">
Bing Maps WPF SDK</a>.</p>
<h1><em>Building the Sample</em></h1>
<p>Open the MainWindow.xaml file and location update the map creential provider with your Bing Maps key where it says &quot;YOUR_BING_MAPS_KEY&quot;.</p>
<p><em>&nbsp;</em></p>
