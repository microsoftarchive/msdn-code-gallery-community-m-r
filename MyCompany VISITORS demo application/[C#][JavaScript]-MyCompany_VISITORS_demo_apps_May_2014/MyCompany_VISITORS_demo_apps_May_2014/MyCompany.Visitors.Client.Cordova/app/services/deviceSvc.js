(function () {
    'use strict';

    angular.module('VisitorsApp').factory('deviceSvc', [
        '$q',
        deviceSvc
    ]);

    function deviceSvc($q) {

        function takePicture() {

            var deferred = $q.defer(),
                destinationType = navigator.camera.DestinationType,
                source = Camera.PictureSourceType.CAMERA;

            navigator.camera.getPicture(
                //On success
                function (imgData) {
                    deferred.resolve(imgData);
                },

                //On error
                function (imgError) {
                    deferred.reject();
                },

                //getPicture options
                {
                    quality: 100,
                    destinationType: destinationType.FILE_URI,
                    sourceType: source,
                    encodingType: Camera.EncodingType.JPEG,
                    targetWidth: 500,
                    targetHeight: 500
                }
            );

            return deferred.promise;
        }

        var service = {
            takePicture: takePicture
        };

        return service;
    }

}());