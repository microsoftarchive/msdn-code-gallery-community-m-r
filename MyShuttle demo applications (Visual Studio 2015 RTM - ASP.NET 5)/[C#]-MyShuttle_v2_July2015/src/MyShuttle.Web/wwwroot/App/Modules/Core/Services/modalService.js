'use strict';

angular.module('myShuttleCore').service('modalService', ['$modal', function ($modal) {
    var service = this;

    var modalOptions = {
        backdrop: true,
        keyboard: true,
        modalFade: true,
        templateUrl: 'App/Modules/Core/Views/modal.html'
    };

    service.show = function (customModel) {
        var tempModel = {};

        var model = {
            cancelActionText: 'Cancel',
            confirmActionText: 'OK',
            headerText: 'Proceed?',
            bodyText: 'Perform this action?'
        };

        //Map modal.html $scope custom properties to defaults defined in service
        angular.extend(tempModel, model, customModel);

        modalOptions.controller = function ($scope, $modalInstance) {
            $scope.model = tempModel;
        };

        return $modal.open(modalOptions).result;
    };

    service.confirm = function(title, text){
        var model = {
            cancelActionText: 'No',
            confirmActionText: 'Yes',
            headerText: title,
            bodyText: text
        };

        modalOptions.controller = function ($scope) {
            $scope.model = model;
        };

        return $modal.open(modalOptions).result;
    };
}]);