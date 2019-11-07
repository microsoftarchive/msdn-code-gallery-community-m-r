MyCompany
=========
The MyCompany Visitors demo is an application released by Microsoft that is composed of several client applications that allow users to request and manage visits and visitors for your company.  Different features are available depending on your role in the organization and the client application being used (Web app, Windows store apps or Phone app)

This repository hosts source for iPad and Android client applications developed in C# with [Xamarin.iOS](http://xamarin.com/ios) and [Xamarin.Android](http://xamarin.com/android)

The source for Windows Server, Windows Store, and Windows Phone apps can be found here: http://code.msdn.microsoft.com/MyCompany-VISITORS-demo-776d9447

### Running the app

First, download the Windows source from the [MyCompany page on code.msdn.microsoft.com](http://code.msdn.microsoft.com/MyCompany-VISITORS-demo-776d9447), and then build and deploy the server.

The iOS and Android clients can be built and deployed from the MyCompany.Visitors.sln in Visual Studio 2013.  To build and deploy the iOS app, you will need to have network access to a Mac running the Xamarin.iOS build server.

Update the default server in Settings/AppSettings.cs to point to your local server.  (Once the app has been deployed, you can also reconfigure the server address for the Visitors app from within the iOS Settings app.)

