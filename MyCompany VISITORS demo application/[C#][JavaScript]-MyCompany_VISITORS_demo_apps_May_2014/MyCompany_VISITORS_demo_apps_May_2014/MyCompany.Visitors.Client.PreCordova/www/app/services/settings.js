(function () {
    'use strict';

    angular.module('VisitorsApp').constant('settings', {
       // basePath: 'http://192.168.1.33/visitors/noauth/api',
        basePath: 'http://localhost:31330/noauth/api',
        dateFormat: 'MM/DD/YYYY',
        timeFormat: 'HH:mm',
        maxResults: 20,
        defaultImagePath: 'app/images/no-photo-big.jpg'
    });

}());