(function () {
    'use strict';

    angular.module('VisitorsApp').factory('pictureManagementSvc', [
        '$q',
        pictureManagementSvc
    ]);

    function pictureManagementSvc($q) {

        function resizePicture(img, width, height) {
            var deferred = $q.defer();

            var image = new Image();

            image.onload = function () {
                // Create a canvas with the desired dimensions
                var canvas = document.createElement("canvas");
                canvas.width = width;
                canvas.height = height;

                // Scale and draw the source image to the canvas
                canvas.getContext("2d").drawImage(this, 0, 0, width, height);

                var imageData = canvas.toDataURL('image/jpeg', 1);

                // Convert the canvas to a data URL in PNG format
                deferred.resolve(imageData);
            }

            image.src = img;

            return deferred.promise;
        }

        var service = {
            resizePicture: resizePicture
        };

        return service;
    }

}());