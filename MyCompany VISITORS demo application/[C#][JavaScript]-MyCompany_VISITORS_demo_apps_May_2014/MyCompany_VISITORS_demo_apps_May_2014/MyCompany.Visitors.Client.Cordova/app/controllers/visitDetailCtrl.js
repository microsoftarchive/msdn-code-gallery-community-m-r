(function () {
    'use strict';

    angular.module('VisitorsApp').controller('VisitDetailCtrl', [
        '$scope',
        '$routeParams',
        '$q',
        'dataSvc',
        'deviceSvc',
        'pictureManagementSvc',
        'enums',
        visitDetailCtrl
    ]);

    function visitDetailCtrl($scope, $routeParams, $q, dataSvc, deviceSvc, pictureManagementSvc, enums) {

        var smallImage = null,
            picturesToUpdate = [];

        var init = function () {
            $scope.showLoading();

            dataSvc.getVisit($routeParams.visitId).then(
                function (result) {
                    $scope.visit = result;
                    $scope.hideLoading();
                },
                function (error) {
                    $scope.hideLoading();
                }
            );
        };


        $scope.visibleMenu = false;

        $scope.goBack = function () {
            window.history.back();
        }

        $scope.updatePicture = function () {
            deviceSvc.takePicture().then(function (result) {
                if (result) {
                    $scope.showLoading();

                    var resizeSmallPicturePromise = pictureManagementSvc.resizePicture(result, 100, 100),
                        resizeBigPicturePromise = pictureManagementSvc.resizePicture(result, 280, 380);

                    $q.all([resizeSmallPicturePromise, resizeBigPicturePromise]).then(function (results) {

                        if (results[0] && results[1]) {

                            smallImage = results[0];

                            picturesToUpdate.push({
                                VisitorId: $scope.visit.visitor.id,
                                Content: results[0].replace('data:image/jpeg;base64,', ''),
                                PictureType: enums.pictureType.small,
                                VisitorPictureId: 0
                            });
                            picturesToUpdate.push({
                                VisitorId: $scope.visit.visitor.id,
                                Content: results[1].replace('data:image/jpeg;base64,', ''),
                                PictureType: enums.pictureType.big,
                                VisitorPictureId: 0
                            });

                            dataSvc.updateVisitorPictures(picturesToUpdate).then(function (result) {
                                $scope.visit.visitor.image = smallImage;
                                picturesToUpdate = [];
                                $scope.hideLoading();
                            }, function (error) {
                                picturesToUpdate = [];
                                $scope.hideLoading();
                            });
                        }

                    });
                }
            });
        }

        init();
    };

}());