Location
Samples\iPhoneSample\iPhoneListSample\iPhoneListSample.xcodeproj

Description
This sample provides a simple iPhone application that demonstrates how to write an app to sync with a service that is created using Microsoft Sync Framework V4 October 2010 CTP service library. It accesses the same sample service as the Silverlight desktop and Windows Phone 7 samples, and synchronizes the same data (so data for the same user will flow between them!).

Warning:
In iPhoneListSample-Info.plist change ServiceRoot to the root URL of the sample service base url.  The sample asumes the both the login page and the sync service share the same base url.  (ie. If service is hosted at http://yoursite.com/DefaultScopeSyncService.svc, your ServiceRoot should be http://yoursite.com )

Prerequisites:
1.	Sample Service deployed in a location accessible to the client.
2.	Apple Xcode 3.2.2
3.  Apple iPhone SDK 3.2
4.  Intel-based Mac with Mac OS 10.5 or higher
