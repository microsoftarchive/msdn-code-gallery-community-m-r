(function () {

    function appendChild(newElement) {
        if (document.body) {
            document.body.appendChild(newElement);
        } else {
            document.head.appendChild(newElement);
        }
    };

    var jscompat = document.createElement('script');
    jscompat.setAttribute('src', 'scripts/winstore-jscompat.js');
    appendChild(jscompat);

    // Hack for Bing Maps
    var veapicore = document.createElement('script');
    veapicore.setAttribute('src', 'scripts/Bing.Maps.JavaScript/js/veapicore.js');
    appendChild(veapicore);

    var veapiModules = document.createElement('script');
    veapiModules.setAttribute('src', 'scripts/Bing.Maps.JavaScript/js/veapiModules.js');
    appendChild(veapiModules);

    var remoteBingMaps = document.createElement('script');
    remoteBingMaps.setAttribute('src', 'scripts/Bing.Maps.JavaScript/js/remoteBingMaps.js');
    appendChild(remoteBingMaps);

    // Hack for PushPlugin
    var pushPluginProxy = document.createElement('script');
    pushPluginProxy.setAttribute('src', 'PushPluginProxy.js');
    appendChild(pushPluginProxy);

}());