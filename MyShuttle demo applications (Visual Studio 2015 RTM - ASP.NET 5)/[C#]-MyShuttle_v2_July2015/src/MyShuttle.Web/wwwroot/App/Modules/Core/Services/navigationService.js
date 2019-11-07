'use strict';

angular.module('myShuttleCore').service('navigationService', ['$state',
    function ($state) {
        var service = this;

        var menu = { options: []};

        service.isActiveRoute = function (path) {
            return $state.current.name === path;
        };

        service.getMenu = function () {
            return menu;
        };

        service.addMenuOption = function (path, text) {
            menu.options.push({
                display: text,
                path: path
            });
        };
    }
]);