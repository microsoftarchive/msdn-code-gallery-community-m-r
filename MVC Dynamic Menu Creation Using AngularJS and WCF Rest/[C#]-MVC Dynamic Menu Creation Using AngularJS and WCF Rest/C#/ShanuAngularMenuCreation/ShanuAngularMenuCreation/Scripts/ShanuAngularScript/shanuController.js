/// <reference path="../angular.js" />  
/// <reference path="../angular.min.js" />   
/// <reference path="Modules.js" />   
/// <reference path="Services.js" />   


app.controller("AngularJsShanu_WCFController", function ($scope, $window, AngularJs_WCFService) {
    $scope.date = new Date();

    $scope.showDetails = false;
    $scope.showSubDetails = false;
    $scope.subChildIDS = "ITM001";

    $scope.Imagename = "R1.png";

    getAllMenuDetails();
    //To Get All Records   
    function getAllMenuDetails() {

        var promiseGet = AngularJs_WCFService.geMenuDetails();
        promiseGet.then(function (pl) {
            $scope.MenuDetailsDisp = pl.data
        },
             function (errorPl) {

             });
    }
  
    $scope.showMenu = function (showMenus) {
     
        if (showMenus == 1) {
            $scope.Imagename = "R2.png"
            $scope.showDetails = true;

        }
        else {
            $scope.Imagename = "R1.png"
            $scope.showDetails = false;
        }
    }

    $scope.showsubMenu = function (showMenus,ids) {
      
        if (showMenus == 1) {
            $scope.subChildIDS = ids;
         
            $scope.showSubDetails = true;          
        }
        else if(showMenus == 0) {
            $scope.showSubDetails = false;          
        }     
        else {
         
            $scope.showSubDetails = true;
         
        }
     
       
    }



});