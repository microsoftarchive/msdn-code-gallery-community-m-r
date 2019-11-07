(function () {
    'use strict'
    //if (app != undefined) {
    app.directive("addbuttonsbutton", function () {
        return {
            restrict: "E",
            template: "<button addbuttons>Click to add buttons</button>"
        }
    });

    //Directive for adding buttons on click that show an alert on click
    app.directive("addbuttons", function ($compile) {
        return function (scope, element, attrs) {
            element.bind("click", function () {
                scope.count++;

                var elementE = '<div class="popup-box chat-popup" id="' + id + '">';
                elementE = elementE + '<div class="popup-head">';
                elementE = elementE + '<div class="popup-head-left">' + name + '</div>';
                elementE = elementE + '<div class="popup-head-right"><a  data-ng-click="closePvtChat(\'' + id + '\')">&#10005;</a></div>';
                elementE = elementE + '<div style="clear: both"></div></div><div class="popup-messages"></div></div>';
                // document.getElementsByTagName("body")[0].innerHTML = document.getElementsByTagName("body")[0].innerHTML + element;
                //console.log(elementE);
                //$compile(elementE)($scope);
                //angular.element("#ChatRoomsContainer").append(elementE);
                angular.element(document.getElementById('ChatRoomsContainer')).append($compile(elementE)(scope));
            });
        };
    });

//    <unordered-list list-source="products" list-property="price | currency" />
//    angular.module("exampleApp", [])
//.directive("unorderedList", function () {
//    return {
//        link: function (scope, element, attrs) {
//            var data = scope[attrs["unorderedList"] || attrs["listSource"]];
//            var propertyExpression = attrs["listProperty"] || "price | currency";
//            if (angular.isArray(data)) {
//                var listElem = angular.element("<ul>");
//                if (element[0].nodeName == "#comment") {
//                    element.parent().append(listElem);
//                } else {
//                    element.append(listElem);
//                }

//                for (var i = 0; i < data.length; i++) {
//                    var itemElement = angular.element("<li>")
//                    .text(scope.$eval(propertyExpression, data[i]));
//                    listElem.append(itemElement);
//                }
//            }
//        },
//        restrict: "EACM"
//    }
    //Directive for showing an alert on click
    //app.directive("alert", function () {
    //    return function (scope, element, attrs) {
    //        element.bind("click", function () {
    //            console.log(attrs);
    //            alert("This is alert #" + attrs.alert);
    //        });
    //    };
    //});

})();
