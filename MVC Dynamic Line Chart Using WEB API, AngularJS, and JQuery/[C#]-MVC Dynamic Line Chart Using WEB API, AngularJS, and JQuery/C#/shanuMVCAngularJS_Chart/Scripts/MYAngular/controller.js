// <reference path="../angular.js" />  
/// <reference path="../angular.min.js" />   
/// <reference path="../angular-animate.js" />   
/// <reference path="../angular-animate.min.js" />   
var app;
(function () {
    app = angular.module("RESTClientModule", ['ngAnimate']);
})();


app.controller("AngularJs_Controller", function ($scope, $timeout, $rootScope, $window, $http) {
    $scope.date = new Date();
    $scope.MyName = "shanu";
    $scope.sItemName = "";
    $scope.itemCount = 5;
    $scope.selectedItem = "";
    $scope.chartTitle = "SHANU Bar Chart";
    $scope.waterMark = "SHANU";
    $scope.ItemValues = 0;
    $scope.ItemNames = "";
    $scope.showItemAdd = false;

    $scope.minsnew = 0;
    $scope.maxnew =0;


    // This method is to get all the Item  Details to bind in Combobox for plotting in Graph
    selectuerRoleDetails($scope.sItemName);
    // This method is to get all the Item  Details to bind in Combobox for plotting in Graph
    function selectuerRoleDetails(ItemName) { 
        $http.get('/api/ItemAPI/getItemDetails/', { params: { ItemName: ItemName } }).success(function (data) {
            $scope.itemData = data;
            $scope.itemCount = $scope.itemData.length;
            $scope.selectedItem = $scope.itemData[0].SaleCount;    

        })
  .error(function () {
      $scope.error = "An Error has occured while loading posts!";
  });

        $http.get('/api/ItemAPI/getItemMaxMinDetails/', { params: { ItemNM: $scope.sItemName } }).success(function (data) {
            $scope.itemDataMaxMin = data;
            $scope.minsnew = $scope.itemDataMaxMin[0].MinValue;
            $scope.maxnew = $scope.itemDataMaxMin[0].MaxValue;


        })
       .error(function () {
           $scope.error = "An Error has occured while loading posts!";
       });
       
    }


    // New Chart item Add
    $scope.showChartItemAdd = function () {
        cleardetails();
        $scope.showItemAdd = true;
    }

    //Form Validation
    $scope.Message = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFormValid = false;

    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;

    });

    //clear all the control values after insert and edit.
    function cleardetails() {
        $scope.ItemValues = 0;
        $scope.ItemNames = "";
        $scope.showItemAdd = false;
        $scope.IsFormSubmitted = false;
    }

    //Save File
    $scope.saveDetails = function () {
       
        $scope.IsFormSubmitted = true;

        $scope.Message = "";
        if ($scope.ItemNames == "")
        {
            alert("Enter Item Name");
            return;
        }

        if ($scope.ItemValues == "") {
            alert("Enter Item Value");
            return;
        }



        if ($scope.IsFormValid) {
            alert($scope.ItemNames);
            $http.get('/api/ItemAPI/insertItem/', { params: { itemName: $scope.ItemNames, SaleCount: $scope.ItemValues } }).success(function (data) {

                $scope.CharDataInserted = data;
                alert($scope.CharDataInserted);

                cleardetails();
                selectuerRoleDetails($scope.sItemName);
            })
             .error(function () {
                 $scope.error = "An Error has occured while loading posts!";
             });
        }
        else {
            $scope.Message = "All the fields are required.";
        }

    };

});
