/// <reference path="../angular.js" />  
/// <reference path="../angular.min.js" />   


/// <reference path="Modules.js" />   
   
app.service("AngularJs_WCFService", function ($http) {
    //Get Order Master Records  
    this.geMenuDetails = function () {
        return $http.get("http://localhost:3514/Service1.svc/GetMenuDetails");
    };

  

});
