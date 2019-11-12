'use strict';

angular.module('myShuttleCore').filter('imageBase64', function () {
    return function (bytes) {
        if(bytes) {
            if(bytes.slice(0,11) !== 'data:image/'){
                bytes= 'data:image/png;base64,' + bytes;
            }
            return bytes;
        }else{
            return '';
        }
    };
});