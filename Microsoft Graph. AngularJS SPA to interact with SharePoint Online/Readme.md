# Microsoft Graph. AngularJS SPA to interact with SharePoint Online
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Microsoft Azure
- Office 365
- Sharepoint Online
- Graph API
- AngularJS
- ADAL
## Topics
- Sharepoint Online
- Bootstrap
- AngularJS
- Microsoft Graph
## Updated
- 10/13/2016
## Description

<h1>Introduction</h1>
<p><span style="font-size:small"><em>Single Page web Application built on AngularJS consuming Microsoft API to interact with SharePoint Online. The application has no any server-side code.</em></span></p>
<p><span style="font-size:small"><em><img id="161989" src="161989-spo-app.png" alt="" width="1049" height="564"><br>
</em></span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small"><em>The solution is built using Visual Studio 2015. Visual Studio 2015 could not be used at all, cause the app coded using only HTML, CSS and&nbsp;JavaScript. Visual Studio 2015 used only for&nbsp;convenience&nbsp;features (built-in
 bower package manager, code formatting, etc.)</em></span></p>
<h2><span style="font-size:medium">Registering your apps with Azure AD</span></h2>
<p><span style="font-size:small">First of all it's necessary to register you app with Azure AD to grant it access to users' OneDrive in your Office 365 tenant.</span></p>
<p><span style="font-size:small">Instruction how to do it:&nbsp;<a title="Manually register your app with Azure AD so it can access Office 365 APIs" href="https://msdn.microsoft.com/en-us/office/office365/howto/add-common-consent-manually" target="_blank">Manually
 register your app with Azure AD so it can access Office 365 APIs</a></span></p>
<p><span style="font-size:small">Permissions required for the App:</span></p>
<ul>
<li><span style="font-size:small">Sites.Read.All </span></li><li><span style="font-size:small">Sites.ReadWrite.All</span> </li></ul>
<h2><span>Application</span></h2>
<h2>Authentication</h2>
<p><span style="font-size:small">The application uses Azure AD Authentication Library (ADAL) for user authetication. In case of AngularJS reference to the following js-files is sufficient to implament Azure AD authentication:</span></p>
<p><span>&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;script src=&quot;https://secure.aadcdn.microsoftonline-p.com/lib/1.0.7/js/adal.min.js&quot;&gt;&lt;/script&gt; 
&lt;script src=&quot;https://secure.aadcdn.microsoftonline-p.com/lib/1.0.7/js/adal-angular.min.js&quot;&gt;&lt;/script&gt;</pre>
<div class="preview">
<pre class="js">&lt;script&nbsp;src=<span class="js__string">&quot;https://secure.aadcdn.microsoftonline-p.com/lib/1.0.7/js/adal.min.js&quot;</span>&gt;&lt;/script&gt;&nbsp;&nbsp;
&lt;script&nbsp;src=<span class="js__string">&quot;https://secure.aadcdn.microsoftonline-p.com/lib/1.0.7/js/adal-angular.min.js&quot;</span>&gt;&lt;/script&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">After that authorization is just a one line of code:</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">adalAuthenticationService.login();</pre>
<div class="preview">
<pre class="js">adalAuthenticationService.login();</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-size:small">and logout accordingly:</span></div>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">adalAuthenticationService.logOut();</pre>
<div class="preview">
<pre class="js">adalAuthenticationService.logOut();</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;
<div class="endscriptcode">
<h2 class="endscriptcode">REST API</h2>
<div class="endscriptcode"><span style="font-size:small">Microsoft API single endpoint will be used&nbsp;to access SharePoint Online. It facilitates the work of the development. For example to&nbsp;retrieve items you're simply to send the request:</span></div>
</div>
<div class="endscriptcode"></div>
<pre class="endscriptcode"><span style="font-size:small"><strong>GET:</strong> https://graph.microsoft.com/<strong>beta/sharepoint/{siteCollectionId},{siteId}/lists/{listId}/items</strong></span></pre>
<h2><em>Factory</em></h2>
<p><span style="font-size:small">To implement interaction between Office 365 and our SPA there is AngularJS factory. It's just HTTP-request wrapper indeed:</span></p>
</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">angular
    .module('MicrosoftGraphSPA')
    .constant('graphBetaUrl', 'https://graph.microsoft.com/beta')
    .factory('spFactory',
    [
        '$http', 'graphBetaUrl',
        function($http, graphBetaUrl) {

            var spFactory = {};

            // Получение списка сайтов
            spFactory.getSites = function(siteId) {
                var url = graphBetaUrl &#43; '/sharepoint/sites';
                if (siteId) {
                    url = url &#43; '/' &#43; siteId &#43; '/sites';
                }
                return $http({
                    method: 'GET',
                    url: url
                });
            };

            // Получение инфор&#1084;ации о сайте
            spFactory.getSite = function(siteId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl &#43; '/sharepoint/sites/' &#43; siteId
                });
            };

            // Получение списков сайта
            spFactory.getLists = function(siteId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl &#43; '/sharepoint/sites/' &#43; siteId &#43; '/lists'
                });
            };

            // ...

            return spFactory;
        }
    ]);</pre>
<div class="preview">
<pre class="js">angular&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.module(<span class="js__string">'MicrosoftGraphSPA'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.constant(<span class="js__string">'graphBetaUrl'</span>,&nbsp;<span class="js__string">'https://graph.microsoft.com/beta'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.factory(<span class="js__string">'spFactory'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'$http'</span>,&nbsp;<span class="js__string">'graphBetaUrl'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>($http,&nbsp;graphBetaUrl)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;spFactory&nbsp;=&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;списка&nbsp;сайтов</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getSites&nbsp;=&nbsp;<span class="js__operator">function</span>(siteId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;url&nbsp;=&nbsp;graphBetaUrl&nbsp;&#43;&nbsp;<span class="js__string">'/sharepoint/sites'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(siteId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url&nbsp;=&nbsp;url&nbsp;&#43;&nbsp;<span class="js__string">'/'</span>&nbsp;&#43;&nbsp;siteId&nbsp;&#43;&nbsp;<span class="js__string">'/sites'</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;$http(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;method:&nbsp;<span class="js__string">'GET'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;url&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;инфор&#1084;ации&nbsp;о&nbsp;сайте</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getSite&nbsp;=&nbsp;<span class="js__operator">function</span>(siteId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;$http(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;method:&nbsp;<span class="js__string">'GET'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;graphBetaUrl&nbsp;&#43;&nbsp;<span class="js__string">'/sharepoint/sites/'</span>&nbsp;&#43;&nbsp;siteId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;списков&nbsp;сайта</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getLists&nbsp;=&nbsp;<span class="js__operator">function</span>(siteId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;$http(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;method:&nbsp;<span class="js__string">'GET'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;graphBetaUrl&nbsp;&#43;&nbsp;<span class="js__string">'/sharepoint/sites/'</span>&nbsp;&#43;&nbsp;siteId&nbsp;&#43;&nbsp;<span class="js__string">'/lists'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;...</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;spFactory;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;]);</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<h2><em>Controller</em></h2>
<p><span style="font-size:small">The controller to use the factory' methods and provide results to the view is simple:</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">(function () {
    angular
        .module('MicrosoftGraphSPA')
        .controller('SPController', [
            '$scope', '$rootScope', '$http', '$routeParams', 'spFactory',
            function ($scope, $rootScope, $http, $routeParams, spFactory) {
                // Пара&#1084;етры из роутинга
                $scope.siteId = $routeParams.siteId;
                $scope.listId = $routeParams.listId;
                $scope.itemId = $routeParams.itemId;

                $scope.sites = null;
                $scope.lists = null;
                $scope.items = null;
                $scope.item = null;
                $scope.site = null;
                $scope.list = null;

                // Загрузка данных
                $scope.init = function () {

                    // Очищае&#1084; результат предыдущего запроса
                    $scope.clearResponse();

                    // Получае&#1084; дочерние сайты или корневые
                    spFactory.getSites($scope.siteId).then(
                        function(response) {
                            $scope.sites = response.data.value;
                        },
                        $rootScope.responseError);

                    // Если выбран сайн
                    if ($scope.siteId) {

                        // Получение инфор&#1084;ации о сайте
                        spFactory.getSite($scope.siteId).then(
                            function (response) {
                                $scope.site = response.data;
                            },
                            $rootScope.responseError);

                        // Получение списков для сайта
                        spFactory.getLists($scope.siteId).then(
                            function(response) {
                                $scope.lists = response.data.value;
                            },
                            $rootScope.responseError);

                        // Если выбран список
                        if ($scope.listId) {

                            // Получение инфор&#1084;ации о списке
                            spFactory.getList($scope.siteId, $scope.listId).then(
                                function (response) {
                                    $scope.list = response.data;
                                },
                                $rootScope.responseError);

                            //Получение эле&#1084;ентов
                            spFactory.getItems($scope.siteId, $scope.listId).then(
                                function(response) {
                                    $scope.items = response.data.value;
                                },
                                $rootScope.responseError);
                        }

                        // Если выбран эле&#1084;ент
                        if ($scope.itemId) {

                            // Получение инфор&#1084;ации ою эле&#1084;енте
                            spFactory.getItem($scope.siteId, $scope.listId, $scope.itemId).then(
                                function(response) {
                                    $scope.item = response.data;
                                },
                                $rootScope.responseError);
                        }
                    }
                }

                $scope.init();
            }
        ]);
})();</pre>
<div class="preview">
<pre class="js">(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;angular&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.module(<span class="js__string">'MicrosoftGraphSPA'</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.controller(<span class="js__string">'SPController'</span>,&nbsp;[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'$scope'</span>,&nbsp;<span class="js__string">'$rootScope'</span>,&nbsp;<span class="js__string">'$http'</span>,&nbsp;<span class="js__string">'$routeParams'</span>,&nbsp;<span class="js__string">'spFactory'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;($scope,&nbsp;$rootScope,&nbsp;$http,&nbsp;$routeParams,&nbsp;spFactory)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Пара&#1084;етры&nbsp;из&nbsp;роутинга</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.siteId&nbsp;=&nbsp;$routeParams.siteId;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.listId&nbsp;=&nbsp;$routeParams.listId;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.itemId&nbsp;=&nbsp;$routeParams.itemId;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.sites&nbsp;=&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.lists&nbsp;=&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.items&nbsp;=&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.item&nbsp;=&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.site&nbsp;=&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.list&nbsp;=&nbsp;null;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Загрузка&nbsp;данных</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.init&nbsp;=&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Очищае&#1084;&nbsp;результат&nbsp;предыдущего&nbsp;запроса</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.clearResponse();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получае&#1084;&nbsp;дочерние&nbsp;сайты&nbsp;или&nbsp;корневые</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getSites($scope.siteId).then(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>(response)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.sites&nbsp;=&nbsp;response.data.value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$rootScope.responseError);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Если&nbsp;выбран&nbsp;сайн</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.siteId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;инфор&#1084;ации&nbsp;о&nbsp;сайте</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getSite($scope.siteId).then(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;(response)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.site&nbsp;=&nbsp;response.data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$rootScope.responseError);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;списков&nbsp;для&nbsp;сайта</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getLists($scope.siteId).then(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>(response)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.lists&nbsp;=&nbsp;response.data.value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$rootScope.responseError);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Если&nbsp;выбран&nbsp;список</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.listId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;инфор&#1084;ации&nbsp;о&nbsp;списке</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getList($scope.siteId,&nbsp;$scope.listId).then(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;(response)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.list&nbsp;=&nbsp;response.data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$rootScope.responseError);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Получение&nbsp;эле&#1084;ентов</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getItems($scope.siteId,&nbsp;$scope.listId).then(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>(response)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.items&nbsp;=&nbsp;response.data.value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$rootScope.responseError);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Если&nbsp;выбран&nbsp;эле&#1084;ент</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($scope.itemId)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Получение&nbsp;инфор&#1084;ации&nbsp;ою&nbsp;эле&#1084;енте</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;spFactory.getItem($scope.siteId,&nbsp;$scope.listId,&nbsp;$scope.itemId).then(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>(response)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.item&nbsp;=&nbsp;response.data;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$rootScope.responseError);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$scope.init();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;]);&nbsp;
<span class="js__brace">}</span>)();</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<h2 class="endscriptcode"><em>Views and routing&nbsp;</em></h2>
<p><span style="font-size:small">The Single Page App contains four views:</span></p>
<ul>
<li><span style="font-size:small">List root sites (list of site collections)</span>
</li><li><span style="font-size:small">Site content (subsites and lists)</span> </li><li><span style="font-size:small">List Items</span> </li><li><span style="font-size:small">Item</span> </li></ul>
<p><span style="font-size:small">So routing in angular config method:</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">.when('/sp', { //SharePoint
    templateUrl: 'views/sp.html',
    controller: 'SPController',
    controllerAs: 'controller'
})
.when('/sp/:siteId', { //SharePoint/Site
    templateUrl: 'views/sp/site.html',
    controller: 'SPController',
    controllerAs: 'controller'
})
.when('/sp/:siteId/:listId', { //SharePoint/Site/List
    templateUrl: 'views/sp/list.html',
    controller: 'SPController',
    controllerAs: 'controller'
})
.when('/sp/:siteId/:listId/:itemId', { //SharePoint/Site/List/Item
    templateUrl: 'views/sp/item.html',
    controller: 'SPController',
    controllerAs: 'controller'
})</pre>
<div class="preview">
<pre class="js">.when(<span class="js__string">'/sp'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__sl_comment">//SharePoint</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;templateUrl:&nbsp;<span class="js__string">'views/sp.html'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controller:&nbsp;<span class="js__string">'SPController'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controllerAs:&nbsp;<span class="js__string">'controller'</span>&nbsp;
<span class="js__brace">}</span>)&nbsp;
.when(<span class="js__string">'/sp/:siteId'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__sl_comment">//SharePoint/Site</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;templateUrl:&nbsp;<span class="js__string">'views/sp/site.html'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controller:&nbsp;<span class="js__string">'SPController'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controllerAs:&nbsp;<span class="js__string">'controller'</span>&nbsp;
<span class="js__brace">}</span>)&nbsp;
.when(<span class="js__string">'/sp/:siteId/:listId'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__sl_comment">//SharePoint/Site/List</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;templateUrl:&nbsp;<span class="js__string">'views/sp/list.html'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controller:&nbsp;<span class="js__string">'SPController'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controllerAs:&nbsp;<span class="js__string">'controller'</span>&nbsp;
<span class="js__brace">}</span>)&nbsp;
.when(<span class="js__string">'/sp/:siteId/:listId/:itemId'</span>,&nbsp;<span class="js__brace">{</span>&nbsp;<span class="js__sl_comment">//SharePoint/Site/List/Item</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;templateUrl:&nbsp;<span class="js__string">'views/sp/item.html'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controller:&nbsp;<span class="js__string">'SPController'</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;controllerAs:&nbsp;<span class="js__string">'controller'</span>&nbsp;
<span class="js__brace">}</span>)</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"><span style="font-size:small">That's it! About 330 lines of code (HTML &#43; JS) and it's done!</span></div>
<div class="endscriptcode"><span><br>
</span></div>
<h1>Source Code Files</h1>
<p><span style="font-size:small">Solution explorer' content:</span></p>
<img id="161991" src="161991-solution.png" alt=""></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<ul>
<li><span style="font-size:small">/js/controlles/SPController.js - controller used by all SP views</span>
</li><li><span style="font-size:small">/js/services/SPFactory.js = factory wrapping GET-request to Microsoft Graph</span>
</li><li><span style="font-size:small">/views/sp.html - list of site collections</span>
</li><li><span style="font-size:small">/views/sp/site.html - site content (subsites and lists)</span>
</li><li><span style="font-size:small">/views/sp/list.html - list items</span> </li><li><span style="font-size:small">/views/sp/item.html - item</span> </li></ul>
</div>
<p><span><br>
</span></p>
<h1>More Information</h1>
<p><span style="font-size:small">Base sample (interaction with OneDrive using Microsoft Graph):&nbsp;<a href="https://code.msdn.microsoft.com/OneDrive-App-built-on-18dac7f4" target="_blank">https://code.msdn.microsoft.com/OneDrive-App-built-on-18dac7f4</a>&nbsp;</span></p>
<p><span style="font-size:small"><em><em><span class="translation-chunk">Detailed description is available</span>&nbsp;<span class="translation-chunk">at my blog post (in Russian)</span>:&nbsp;<a href="http://blog.vitalyzhukov.ru/ru/microsoft-graph-sharepoint-webhooks.aspx" target="_blank">http://blog.vitalyzhukov.ru/ru/microsoft-graph-sharepoint-webhooks.aspx</a></em></em></span></p>
