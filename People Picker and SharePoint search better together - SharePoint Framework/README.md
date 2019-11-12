# People Picker and SharePoint search better together - SharePoint Framework
## License
- MIT
## Technologies
- Sharepoint Online
- SharePoint Framework
## Topics
- SharePoint Framework
## Updated
- 06/10/2017
## Description

<h1>Introduction</h1>
<p><em>This sample demonstrate how is possible to implement the Office UI Fabric Peoplepicker on SharePoint Framework.</em></p>
<p><em><span><span>In this article I'll be focused on&nbsp;</span><a href="https://dev.office.com/fabric" target="_blank">Office UI Fabric</a><span>, the official Office and Office 365 front-end framework, in particular I'll talk about the&nbsp;</span><a href="https://dev.office.com/fabric#/components/peoplepicker" target="_blank">People
 Picker</a><span>, an important component to have a user friendly and comfortable functionality for the end user.</span></span></em></p>
<p><em><span><span>&nbsp;</span>If you lost the previous article please take a look here&nbsp;</span><a href="http://www.delucagiuliano.com/office-ui-fabric-people-picker-and-sharepoint-search-better-together-part-1/" target="_blank">Office UI Fabric People
 Picker and SharePoint search better together part 1 - SharePoint&nbsp;Add-In</a><span>.</span><br>
<span>As promise, I released a SharePoint Framework solution with the Office UI Fabric People Picker, like for the Add-In solution, the App across the SharePoint Search API is able to retrieve people.</span><br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><img src=":-preview1.gif" alt="" width="700px"></p>
<p><em>The solution has been created for SharePoint Framework and the fornt end Framework used is Reactjs.</em></p>
<p><em><em>Regarding the logic, every time that user will write&nbsp;something in the text field, the App will perform a&nbsp;<a href="https://msdn.microsoft.com/en-us/library/office/jj163876.aspx" target="_blank">REST API call on the SharePoint Search</a>&nbsp;to
 grab a result filtered.</em><br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>Here a&nbsp;<em><strong>code snippet</strong></em>&nbsp;of the React Component:&nbsp;</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js">import&nbsp;*&nbsp;as&nbsp;React&nbsp;from&nbsp;<span class="js__string">'react'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;css&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react'</span>;&nbsp;
import&nbsp;styles&nbsp;from&nbsp;<span class="js__string">'./OfficeUiFabricPeoplePicker.module.scss'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;IOfficeUiFabricPeoplePickerProps&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'./IOfficeUiFabricPeoplePickerProps'</span>;&nbsp;
&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;CompactPeoplePicker,&nbsp;
&nbsp;&nbsp;IBasePickerSuggestionsProps,&nbsp;
&nbsp;&nbsp;ListPeoplePicker,&nbsp;
&nbsp;&nbsp;NormalPeoplePicker&nbsp;
<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react/lib/Pickers'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;IPersonaProps&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react/lib/Persona'</span>;&nbsp;
<span class="js__statement">const</span>&nbsp;suggestionProps:&nbsp;IBasePickerSuggestionsProps&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;suggestionsHeaderText:&nbsp;<span class="js__string">'Suggested&nbsp;People'</span>,&nbsp;
&nbsp;&nbsp;noResultsFoundText:&nbsp;<span class="js__string">'No&nbsp;results&nbsp;found'</span>,&nbsp;
&nbsp;&nbsp;loadingText:&nbsp;<span class="js__string">'Loading'</span>&nbsp;
<span class="js__brace">}</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;BaseComponent,&nbsp;
&nbsp;&nbsp;assign,&nbsp;
&nbsp;&nbsp;autobind&nbsp;
<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react/lib//Utilities'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;people&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'./PeoplePickerExampleData'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;Label&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react/lib/Label'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;IPeopleDataResult&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'./IPeopleDataResult'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;IPersonaWithMenu&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react/lib/components/pickers/PeoplePicker/PeoplePickerItems/PeoplePickerItem.Props'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;IContextualMenuItem&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'office-ui-fabric-react/lib/ContextualMenu'</span>;&nbsp;
import&nbsp;<span class="js__brace">{</span>&nbsp;SPHttpClient,&nbsp;SPHttpClientResponse&nbsp;<span class="js__brace">}</span>&nbsp;from&nbsp;<span class="js__string">'@microsoft/sp-http'</span>;&nbsp;
&nbsp;
export&nbsp;interface&nbsp;IOfficeUiFabricPeoplePickerState&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;currentPicker?:&nbsp;number&nbsp;|&nbsp;string;&nbsp;
&nbsp;&nbsp;delayResults?:&nbsp;boolean;&nbsp;
<span class="js__brace">}</span>&nbsp;
export&nbsp;interface&nbsp;IPeopleSearchProps&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;JobTitle:&nbsp;string;&nbsp;
&nbsp;&nbsp;PictureURL:&nbsp;string;&nbsp;
&nbsp;&nbsp;PreferredName:&nbsp;string;&nbsp;
<span class="js__brace">}</span>&nbsp;
export&nbsp;<span class="js__statement">default</span>&nbsp;class&nbsp;OfficeUiFabricPeoplePicker&nbsp;extends&nbsp;React.Component&lt;IOfficeUiFabricPeoplePickerProps,&nbsp;IOfficeUiFabricPeoplePickerState&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;private&nbsp;_peopleList;&nbsp;
&nbsp;&nbsp;private&nbsp;contextualMenuItems:&nbsp;IContextualMenuItem[]&nbsp;=&nbsp;[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key:&nbsp;<span class="js__string">'newItem'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;icon:&nbsp;<span class="js__string">'circlePlus'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="js__string">'New'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key:&nbsp;<span class="js__string">'upload'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;icon:&nbsp;<span class="js__string">'upload'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="js__string">'Upload'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key:&nbsp;<span class="js__string">'divider_1'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="js__string">'-'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key:&nbsp;<span class="js__string">'rename'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="js__string">'Rename'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key:&nbsp;<span class="js__string">'properties'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="js__string">'Properties'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key:&nbsp;<span class="js__string">'disabled'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name:&nbsp;<span class="js__string">'Disabled&nbsp;item'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;disabled:&nbsp;true&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;];&nbsp;
&nbsp;&nbsp;constructor()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;super();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._peopleList&nbsp;=&nbsp;[];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;people.forEach((persona:&nbsp;IPersonaProps)&nbsp;=&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;let&nbsp;target:&nbsp;IPersonaWithMenu&nbsp;=&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assign(target,&nbsp;persona,&nbsp;<span class="js__brace">{</span>&nbsp;menuItems:&nbsp;<span class="js__operator">this</span>.contextualMenuItems&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>._peopleList.push(target);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.state&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;currentPicker:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;delayResults:&nbsp;false&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;public&nbsp;render():&nbsp;React.ReactElement&lt;IOfficeUiFabricPeoplePickerProps&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__operator">this</span>.props.typePicker&nbsp;==&nbsp;<span class="js__string">&quot;Normal&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;NormalPeoplePicker&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onResolveSuggestions=<span class="js__brace">{</span><span class="js__operator">this</span>._onFilterChanged<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;getTextFromItem=<span class="js__brace">{</span>(persona:&nbsp;IPersonaProps)&nbsp;=&gt;&nbsp;persona.primaryText<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pickerSuggestionsProps=<span class="js__brace">{</span>suggestionProps<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;className=<span class="js__brace">{</span><span class="js__string">'ms-PeoplePicker'</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key=<span class="js__brace">{</span><span class="js__string">'normal'</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;CompactPeoplePicker&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onResolveSuggestions=<span class="js__brace">{</span><span class="js__operator">this</span>._onFilterChanged<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;getTextFromItem=<span class="js__brace">{</span>(persona:&nbsp;IPersonaProps)&nbsp;=&gt;&nbsp;persona.primaryText<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pickerSuggestionsProps=<span class="js__brace">{</span>suggestionProps<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;className=<span class="js__brace">{</span><span class="js__string">'ms-PeoplePicker'</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key=<span class="js__brace">{</span><span class="js__string">'normal'</span><span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;@autobind&nbsp;
&nbsp;&nbsp;private&nbsp;_onFilterChanged(filterText:&nbsp;string,&nbsp;currentPersonas:&nbsp;IPersonaProps[],&nbsp;limitResults?:&nbsp;number)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(filterText)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(filterText.length&nbsp;&gt;&nbsp;<span class="js__num">4</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;<span class="js__operator">this</span>.searchPeople(filterText,&nbsp;<span class="js__operator">this</span>._peopleList);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;[];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;private&nbsp;searchPeople(terms:&nbsp;string,&nbsp;results:&nbsp;IPersonaProps[]):&nbsp;IPersonaProps[]&nbsp;|&nbsp;Promise&lt;IPersonaProps[]&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//return&nbsp;new&nbsp;Promise&lt;IPersonaProps[]&gt;((resolve,&nbsp;reject)&nbsp;=&gt;&nbsp;setTimeout(()&nbsp;=&gt;&nbsp;resolve(results),&nbsp;2000));</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;<span class="js__operator">new</span>&nbsp;Promise&lt;IPersonaProps[]&gt;((resolve,&nbsp;reject)&nbsp;=&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">this</span>.props.spHttpClient.get(`$<span class="js__brace">{</span><span class="js__operator">this</span>.props.siteUrl<span class="js__brace">}</span><span class="js__reg_exp">/_api/</span>search/query?querytext=<span class="js__string">'*${terms}*'</span>&amp;rowlimit=<span class="js__num">10</span>&amp;sourceid=<span class="js__string">'b09a7990-05ea-4af9-81ef-edfab16c4e31'</span>`,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SPHttpClient.configurations.v1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;headers:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'Accept'</span>:&nbsp;<span class="js__string">'application/json;odata=nometadata'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'odata-version'</span>:&nbsp;<span class="js__string">''</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.then((response:&nbsp;SPHttpClientResponse):&nbsp;Promise&lt;<span class="js__brace">{</span>&nbsp;PrimaryQueryResult:&nbsp;IPeopleDataResult&nbsp;<span class="js__brace">}</span>&gt;&nbsp;=&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;response.json();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.then((response:&nbsp;<span class="js__brace">{</span>&nbsp;PrimaryQueryResult:&nbsp;IPeopleDataResult&nbsp;<span class="js__brace">}</span>):&nbsp;<span class="js__operator">void</span>&nbsp;=&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;let&nbsp;relevantResults:&nbsp;any&nbsp;=&nbsp;response.PrimaryQueryResult.RelevantResults;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;let&nbsp;resultCount:&nbsp;number&nbsp;=&nbsp;relevantResults.TotalRows;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;let&nbsp;people&nbsp;=&nbsp;[];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;let&nbsp;persona:&nbsp;IPersonaProps&nbsp;=&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(resultCount&nbsp;&gt;&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;relevantResults.Table.Rows.forEach(<span class="js__operator">function</span>&nbsp;(row)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;row.Cells.forEach(<span class="js__operator">function</span>&nbsp;(cell)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//person[cell.Key]&nbsp;=&nbsp;cell.Value;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(cell.Key&nbsp;===&nbsp;<span class="js__string">'JobTitle'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;persona.secondaryText&nbsp;=&nbsp;cell.Value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(cell.Key&nbsp;===&nbsp;<span class="js__string">'PictureURL'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;persona.imageUrl&nbsp;=&nbsp;cell.Value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(cell.Key&nbsp;===&nbsp;<span class="js__string">'PreferredName'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;persona.primaryText&nbsp;=&nbsp;cell.Value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;people.push(persona);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;resolve(people);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;(error:&nbsp;any):&nbsp;<span class="js__operator">void</span>&nbsp;=&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reject(<span class="js__operator">this</span>._peopleList&nbsp;=&nbsp;[]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>));&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;private&nbsp;_filterPersonasByText(filterText:&nbsp;string):&nbsp;IPersonaProps[]&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;<span class="js__operator">this</span>._peopleList.filter(item&nbsp;=&gt;&nbsp;<span class="js__operator">this</span>._doesTextStartWith(item.primaryText,&nbsp;filterText));&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;private&nbsp;_removeDuplicates(personas:&nbsp;IPersonaProps[],&nbsp;possibleDupes:&nbsp;IPersonaProps[])&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;personas.filter(persona&nbsp;=&gt;&nbsp;!<span class="js__operator">this</span>._listContainsPersona(persona,&nbsp;possibleDupes));&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;private&nbsp;_listContainsPersona(persona:&nbsp;IPersonaProps,&nbsp;personas:&nbsp;IPersonaProps[])&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(!personas&nbsp;||&nbsp;!personas.length&nbsp;||&nbsp;personas.length&nbsp;===&nbsp;<span class="js__num">0</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;personas.filter(item&nbsp;=&gt;&nbsp;item.primaryText&nbsp;===&nbsp;persona.primaryText).length&nbsp;&gt;&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;private&nbsp;_filterPromise(personasToReturn:&nbsp;IPersonaProps[]):&nbsp;IPersonaProps[]&nbsp;|&nbsp;Promise&lt;IPersonaProps[]&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__operator">this</span>.state.delayResults)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;<span class="js__operator">this</span>._convertResultsToPromise(personasToReturn);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;personasToReturn;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;private&nbsp;_convertResultsToPromise(results:&nbsp;IPersonaProps[]):&nbsp;Promise&lt;IPersonaProps[]&gt;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;<span class="js__operator">new</span>&nbsp;Promise&lt;IPersonaProps[]&gt;((resolve,&nbsp;reject)&nbsp;=&gt;&nbsp;setTimeout(()&nbsp;=&gt;&nbsp;resolve(results),&nbsp;<span class="js__num">2000</span>));&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;private&nbsp;_doesTextStartWith(text:&nbsp;string,&nbsp;filterText:&nbsp;string):&nbsp;boolean&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;text.toLowerCase().indexOf(filterText.toLowerCase())&nbsp;===&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<p>The solution it's also available on github:</p>
<p><a href="https://github.com/giuleon/SPFx-OfficeUIFabric-PeoplePicker" target="_blank">https://github.com/giuleon/SPFx-OfficeUIFabric-PeoplePicker</a></p>
<p>Building the code:</p>
<pre>npm i
gulp serve</pre>
<h1>More Information</h1>
<p><a href="https://code.msdn.microsoft.com/Office-UI-Fabric-People-08de40e6" target="_blank">Office UI Fabric People Picker and SharePoint search better together - SP Add-In</a></p>
<p><em><em><em><em><em><a href="http://www.delucagiuliano.com/office-ui-fabric-people-picker-and-sharepoint-search-better-together-part-2-sharepoint-framework" target="_blank">Read this article on my website</a></em></em></em></em></em></p>
