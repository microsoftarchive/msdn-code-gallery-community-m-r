define(['durandal/system', 'services/logger', 'durandal/plugins/router', 'config', 'services/context', 'viewmodels/base'],
    function (system, logger, router, config, context, base) {
        var shell = {
            activate: activate,
            router: router,
            isBusy: base.isBusy
        };
        return shell;

        function activate() {
            logger.log('MyCompany Mobile Loaded', null, system.getModuleId(shell));

            router.map(config.configureRoutes());
            return router.activate(config.startModule);
        }
    }
);