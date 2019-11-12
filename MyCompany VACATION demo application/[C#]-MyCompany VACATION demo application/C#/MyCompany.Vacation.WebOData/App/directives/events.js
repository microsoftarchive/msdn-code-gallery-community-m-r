vacationApp.directive('keypress', function () {
    var timeOut;
    return {
        restrict: 'A',
        link: function (scope, elem, attr, ctrl) {
            elem.bind('keypress', function () {
                if (timeOut) {
                    clearTimeout(timeOut);
                }
                timeOut = setTimeout(function () {
                    scope.$apply(function (s) {
                        s.$eval(attr.keypress);
                    });
                }, 300);
            });
        }
    };
});