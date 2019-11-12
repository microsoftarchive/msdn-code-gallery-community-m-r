'use strict';

angular.module('myShuttleCore').directive('msCrop', [function () {
    return {
        restrict: 'AE',
        replace: true,
        scope: {
            picture: '=msCropModelProperty',
            imageSize: '=?msImageSize',
            aspectRatio: '=?msAspectRatio'
        },
        link: function (scope, element, attrs) {
            scope.myImage = '';
            scope.previewImage = '';
            scope.areaType = attrs.msAreaType || 'square';

            var btnFilePicker = element.find('.change-photo'),
                browserBtnFilePicker = element.find('.file-input'),
                modalInstanceCtrl = element.find('#imageModal'),
                modalButtons = element.find('.btn'),
                file;

            btnFilePicker.bind('click', function () {
                browserBtnFilePicker.click();
            });
            browserBtnFilePicker.bind('click', function () {
                this.value = null;
            });
            browserBtnFilePicker.bind('change', function (event) {
                file = event.currentTarget.files[0];
                if (file){
                    modalInstanceCtrl.modal({backdrop: 'static', keyboard: false});
                }
            });
            modalInstanceCtrl.bind('shown.bs.modal', function () {
                var reader = new FileReader();
                reader.onload = function (event) {
                    scope.$apply(function () {
                        scope.myImage = event.target.result;
                    });
                };
                reader.readAsDataURL(file);
            });
            modalButtons.bind('click', function () {
                browserBtnFilePicker.value = '';
                modalInstanceCtrl.modal('hide');
            });

            scope.closeModal = function () {
                scope.previewImage = '';
                scope.myImage = '';
            };
            scope.cancelImage = function () {
                scope.closeModal();
            };
            scope.saveImage = function () {
                var resultImage = angular.copy(scope.previewImage);
                if (resultImage.indexOf('data:image/png;base64,') === 0) {
                    resultImage = resultImage.split('base64,')[1];
                }
                scope.picture = resultImage;
                scope.closeModal();
            };
        },
        templateUrl: 'App/Modules/Core/Views/cropTemplate.html'
    };
}]);
