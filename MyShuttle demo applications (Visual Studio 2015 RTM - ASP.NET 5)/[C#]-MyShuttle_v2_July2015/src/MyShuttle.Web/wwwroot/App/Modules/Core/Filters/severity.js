'use strict';

angular.module('myShuttleCore').filter('severity', function () {

    var severities = [
        { display: 'Low', css: 'severityLow' },
        { display: 'Medium', css: 'severityMedium' },
        { display: 'High', css: 'severityHigh' }
    ];

    return function (index) {
        return severities[index];
    };
});