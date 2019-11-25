# Office UI Fabric People Picker and SharePoint search better together part 1

[Read the article on my blog](http://www.delucagiuliano.com/office-ui-fabric-people-picker-and-sharepoint-search-better-together-part-1)

In this article I'll be focused on Office UI Fabric, the official Office and Office 365 front-end framework, in particular I'll talk about the People Picker, an important component to have a user friendly and comfortable functionality for the end user.

On the Office UI Fabric website, there is a sample of this helpful control, however the demo is not so complete because we have static info about the people, I mean there is not a use case where is possible to retrieve the people across a search and display them in the people picker field dynamically.

So I decided to develop a solution to fill the gap, my intention is, to split this topic in two parts, I'll give you a first solution with SharePoint Add-In and another one with SharePoint Framework in another article.

![alt text](OfficeUIFabricPeoplePickerAdd-In.gif "People Picker Demo")

Let's go forward with the solution, I created a simple SharePoint Hosted App to achieve my goal.
In order to take advantage the SharePoint Search, is necessary specify in the App manifest the right permission:

![alt text](OfficeUIFabricPeoplePickerAdd-InAppManifest.png "SharePoint Add-In App Manifest")

Regarding the logic, every time that user will write something in the text field, the App will perform a REST API call on the SharePoint Search to grab a result filtered, and the syntax of the url will be something like that:

```
/_api/search/query?querytext='*" + "Characters to search" + "*'&rowlimit=10&sourceid='b09a7990-05ea-4af9-81ef-edfab16c4e31'
```

As you can see I specified a row limit to not overlook the performance and most important, I have narrowed the field of action to work only with people and user profiles.
Naturally the App is ready to go, then you can deploy directly on your SharePoint Online or On-Premise.
