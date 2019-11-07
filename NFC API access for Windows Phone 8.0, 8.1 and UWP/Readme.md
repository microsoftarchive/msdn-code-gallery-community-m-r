# NFC API access for Windows Phone 8.0, 8.1 and UWP
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- nfc
- proximity
- Windows Phone 8.1
## Topics
- RFID
- nfc
- NDEF
## Updated
- 03/07/2016
## Description

<p><span style="font-size:medium"><strong>NFC API on the Windows Platform</strong></span></p>
<p><span style="font-size:small">NFC (Near Field Communication) is a short range wireless RFID technology which is gaining a lot of popularity within the mobile device market as it has a wide range of applications in mobile scenarios for example: payment at
 checkpoints or contact sharing just by touching two NC devices together. The Tech works either by placing the device 1-4cm near an RFID tag pr 1-4cm near another NFC enabled device. Now, we can read and write message to tags. However to achieve this we have
 to know what kind of technology does the tag / card supports. We have card techs such as NDEF, MIFARE classic and so on. Knowing what kind of tech does the card support is crucial because we will have to write code that would format our messages in the given
 format / card tech.</span></p>
<p><span style="font-size:medium"><strong>The scope of this code sample</strong></span></p>
<p><span style="font-size:small">We will not do any kind of NFC message exchange between the device and the tag yet. Unfortunately, this is in itself a topic of a blog post that I am currently working on and will soon be available (with the code of course).
 In this current code sample, I will only show you how to initialize the Proximity sensor (responsible of handing the NFC communications on windows phone platform) and I will also implement the events the can be fired when an NFC device in the range for communication.</span></p>
<p><span style="font-size:medium"><strong>What you need to follow along</strong></span></p>
<p><span style="font-size:small">Well it would be great if you had a windows phone with NFC enable with some RFID tag, but if you don&rsquo;t, no worries. There is a workaround; it turns out that the windows phone emulators which come with windows SDK (Visual
 studio will install it by default) support NFC sensor emulation. And this is a great thing as we can make our testing right from our PC with our actual development environment. Now, how do we get a virtual tag or something like that? Well there is this tool:(
<a href="https://proximitytapper.codeplex.com/">https://proximitytapper.codeplex.com/</a> ) which you can use. You will need two emulators running with NFC enable, then from the tool you can simulate NFC taps.</span></p>
<p><span style="font-size:medium"><strong>Notice</strong></span></p>
<p><span style="font-size:small">As you can see, I don&rsquo;t explain code samples in the description as I want that all the users of my code to have the required context first rather than dumping all the code snippets here and start explain what the code
 does. Most of time, my codes are self-explanatory and can be easily read. So&hellip; happy coding&hellip;</span></p>
