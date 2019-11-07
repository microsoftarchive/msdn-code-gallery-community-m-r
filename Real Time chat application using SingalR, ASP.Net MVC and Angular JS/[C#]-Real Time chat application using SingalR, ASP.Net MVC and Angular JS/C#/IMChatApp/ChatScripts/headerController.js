(function () {
    'use strict'
    app.controller("headerController", function ($scope, $rootScope, $filter) {
        $rootScope.RootPrivateMessages = [];
        // $watchCollection
      //  $rootScope.RootPrivateMessages
        //$watchCollection('grouping', function () {
        //    $scope.groupedMessages = _.groupBy($rootScope.RootPrivateMessages, $scope.grouping);
        //});
      //  $scope.groups = [];
       // $watchCollection($rootScope.RootPrivateMessages, function () {
        $scope.groups = [];
        //});
        // $scope.grouping = "from";
            $rootScope.updateMessage = function ()
            {          
                $scope.groups = _.groupBy($rootScope.RootPrivateMessages, "from");
                $scope.messageCount = $rootScope.RootPrivateMessages.length;             
            }        
    });
})();