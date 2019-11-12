vacationApp.factory('context',['messenger',
    function (messenger) {
        var currentUser;

        var setCurrentUser = function (data) {
            currentUser = data;
            messenger.send('userInfoUpdated');
        };

        var getCurrentUser = function (data) {
            return currentUser;
        };

        return {
            currentUser: currentUser,
            getCurrentUser: getCurrentUser,
            setCurrentUser: setCurrentUser
        };
    }]);
