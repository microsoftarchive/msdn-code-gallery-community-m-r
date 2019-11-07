vacationApp.factory('menuService', ['$q', 'dataService', 'enums', 'context',
    function ($q, dataService, enums, context) {

        var getMenuOptions = function (scope) {
            var deferred = $q.defer();
            var isNoAuth = location.href.toLowerCase().indexOf('noauth') != -1;
            var employee = context.getCurrentUser() || {};

            var rootPath = isNoAuth ? 'noauth#' : '#';
            var pages = [
              { display: 'overlaps', path: '/overlaps', iconClass: 'team-vacation-overlaps-icon', visible: employee.isManager },
              { display: 'team vacations', path: '/teamVacation', iconClass: 'team-vacation-list-icon', visible: employee.isManager },
              { display: 'my calendar', path: '/', iconClass: 'mycalendar-icon', visible: true }
            ];

            pages.forEach(function (page) {
                page.url = rootPath + page.path;
            });

            deferred.resolve(pages);

            return deferred.promise;
        };

        var menuService = {
            getMenuOptions: getMenuOptions
        };

        return menuService;
    }]);