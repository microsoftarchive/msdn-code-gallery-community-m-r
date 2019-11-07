define(['durandal/system', 'services/logger', 'durandal/plugins/router', 'config', 'services/context', 'viewmodels/base'],
    function (system, logger, router, config, context, base) {
        var shell = {
            activate: activate,
            router: router,
            isBusy: base.isBusy,
            home: home
        };
        return shell;

        function activate() {
            logger.log('MyCompany Loaded', null, system.getModuleId(shell));

            router.map(config.configureRoutes());

            return router.activate(config.startModule);
        }

        function getParams()
        {
            var prmstr = window.location.search.substr(1);
            var prmarr = prmstr.split ("&");
            var params = {};

            for ( var i = 0; i < prmarr.length; i++) {
                var tmparr = prmarr[i].split("=");
                params[tmparr[0]] = tmparr[1];
            }

            return params;
        }

        function home() {
            handleNoAuthLink("", true);
        }

        function handleNoAuthLink(route, executeIfNoAuth) {
            var noauthIndex = window.location.href.indexOf("noauth");
            var url;
            if (noauthIndex != -1) {
                if (window.location.pathname)
                    url = window.location.protocol + "//" + window.location.host + window.location.pathname;
                else
                    url = window.location.protocol + "//" + window.location.host + "/noauth/";

                if (executeIfNoAuth) {
                    window.location.href = url;
                }
            }
            else {
                if (window.location.pathname)
                    url = window.location.protocol + "//" + window.location.host + window.location.pathname + route;
                else
                    url = window.location.protocol + "//" + window.location.host + "/" + route;

                window.location.href = url;
            }
        };
        
    }
);