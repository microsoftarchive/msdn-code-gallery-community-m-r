(function () {
    'use strict';

    angular.module('VisitorsApp').factory('enums', [
        enums
    ]);

    function enums() {

        var pictureType = {
                unknown: 0,
                small: 1,
                big: 2,
                all: 3
            };

        var service = {
            pictureType: pictureType
        };

        return service;
    }

}());