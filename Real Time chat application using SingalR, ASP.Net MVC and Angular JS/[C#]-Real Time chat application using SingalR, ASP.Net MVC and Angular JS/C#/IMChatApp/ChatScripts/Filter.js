(function () {
    'use strict'
    app.filter('group', function () {
        return function (items, groupSize) {
            var groups = [],
               inner;
            for (var i = 0; i < items.length; i++) {
                if (i % groupSize === 0) {
                    inner = [];
                    groups.push(inner);
                }
                inner.push(items[i]);
            }
            return groups;
        };
    });
})();