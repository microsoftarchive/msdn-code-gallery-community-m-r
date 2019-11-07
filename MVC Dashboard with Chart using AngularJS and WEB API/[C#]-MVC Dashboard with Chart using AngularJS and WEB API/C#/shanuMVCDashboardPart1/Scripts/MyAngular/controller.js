// <reference path="../angular.js" />  
/// <reference path="../angular.min.js" />   
/// <reference path="../angular-animate.js" />   
/// <reference path="../angular-animate.min.js" />   
var app;
(function () {
    app = angular.module("dashbordModule", ['ngAnimate']);
})();


app.controller("AngularJs_Controller", function ($scope, $filter, $timeout, $rootScope, $window, $http) {
// Declaration for Dashboard Details
    $scope.date = new Date();
    $scope.MyName = "shanu";

    $scope.isQuerys = false;
    $scope.Querys = "";
   


    $scope.ColumnNames = "ID,ItemNo,ItemName,Comments,Price";
    $scope.TableNames = "ItemDetail";

    $scope.isCondition = false;
    $scope.whereCondition = 0;
    $scope.Conditions = "";

    $scope.isGroupBy = false;
    $scope.GroupBy = 0;
    $scope.GroupBys = "";

    $scope.isOrderBy = false;
    $scope.OrderBy = 0;
    $scope.OrderBys = "";
    // Array value to check for SQL Injection
    $scope.sqlInjectionArray = ['create', 'drop', 'delete', 'insert', 'update', 'truncate',
                                'grant', 'print', 'sp_executesql', 'objects', 'declare',
                                'table', 'into', 'sqlcancel', 'sqlsetprop', 'sqlexec',
                                'sqlcommit', 'revoke', 'rollback', 'sqlrollback', 'values',
                                'sqldisconnect', 'sqlconnect', 'system_user', 'schema_name',
                                'schemata', 'information_schema', 'dbo', 'guest', 'db_owner',
				                'db_', 'table', '@@', 'Users', 'execute', 'sysname', 'sp_who',
                                'sysobjects', 'sp_', 'sysprocesses', 'master', 'sys', 'db_',
                                'is_', 'exec', 'end', 'xp_', '; --', 'alter', 'begin', 'cursor',
                                'kill', '--', 'tabname', 'sys'];

    // Declaration for Chart
     $scope.chartQuerys = "Select ItemName as Name,SUM(Price) as Value FROM ItemDetail GROUP BY  ItemName ORDER BY Value,Name";

    $scope.sItemName = "";
    $scope.itemCount = 5;
    $scope.selectedItem = "MOUSE";
    $scope.chartTitle = "SHANU Item Sales Chart";
    $scope.waterMark = "SHANU";
    $scope.ItemValues = 0;
    $scope.ItemNames = "";
    $scope.minsnew = 0;
    $scope.maxnew = 0;
    
    //search Details
    $scope.searchDetails = function () {
       
       // 1. Check for Select Query -> In this fucntion we check for SQL injection in user entered select query if any key word from the array list is found then we give msg to user to entert he valid select query
        if ($scope.isQuerys == true) {
        if ($scope.Querys != "") {
            $scope.whereCondition = 1;
            for (var i = 0; i < $scope.sqlInjectionArray.length-1; i++) {
                if ($filter('lowercase')($scope.Querys).match($scope.sqlInjectionArray[i])) {
                    alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in select query");
                    return;
                }
            }
            searchTableDetails($scope.Querys, $scope.ColumnNames, $scope.TableNames, $scope.whereCondition, $scope.Conditions, $scope.GroupBy, $scope.GroupBys, $scope.OrderBy, $scope.OrderBys);

            return;
        }
        else {
            alert("Enter Your Select Query !");
            return;
        }
        }
        else
        {
            $scope.Querys = "";
        }

        // 2. Check for Column Names -> If user entered the valid column names the details will be checkd and binded in page
        if ($scope.ColumnNames == "") {
            alert("Enter the Column Details !");
            return;
        }
        else
        {
            for (var i = 0; i < $scope.sqlInjectionArray.length - 1; i++) {
                if ($filter('lowercase')($scope.ColumnNames).match($scope.sqlInjectionArray[i])) {
                    alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in Column Names");
                    return;
                }
            }
        }

        // 3. Check for Table Names -> If user entered the valid Table names the details will be checkd and binded in page
        if ($scope.TableNames == "") {
            alert("Enter the Table Details !");
            return;
        }
        else {
            for (var i = 0; i < $scope.sqlInjectionArray.length - 1; i++) {
                if ($filter('lowercase')($scope.TableNames).match($scope.sqlInjectionArray[i])) {
                    alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in Table Names");
                    return;
                }
            }
        }

       
        // 4. Check for Where condition -> If user check the Where condition check box, the user entered where condition will be added to the select query 
        if ($scope.isCondition == true) {
            if ($scope.Conditions == "") {
                alert("Enter the Where Condition !");
                return;
            }
            else {
                for (var i = 0; i < $scope.sqlInjectionArray.length - 1; i++) {
                    if ($filter('lowercase')($scope.Conditions).match($scope.sqlInjectionArray[i])) {
                        alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in Where Condition");
                        return;
                    }
                }
                $scope.whereCondition = 1;
            }
                
            }
            else {
                $scope.whereCondition = 0;
            }

        // 5. Check for GroupBy condition -> If user check the GroupBy condition check box, the user entered GroupBy condition will be added to the select query 
        if ($scope.isGroupBy == true) {

            if ($scope.GroupBys == "") {
                alert("Enter the Group By Details !");
                return;
            }
            else {
                for (var i = 0; i < $scope.sqlInjectionArray.length - 1; i++) {
                    if ($filter('lowercase')($scope.GroupBys).match($scope.sqlInjectionArray[i])) {
                        alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in GroupBy");
                        return;
                    }
                }
                $scope.GroupBy = 1;
            }
               
            }
            else {
                $scope.GroupBy = 0;
            }

        // 6. Check for OrderBy condition -> If user check the OrderBy condition check box, the user entered OrderBy condition will be added to the select query 
        if ($scope.isOrderBy == true) {

            if ($scope.OrderBys == "") {
                alert("Enter the Group By details !");
                return;
            }
            else {
                for (var i = 0; i < $scope.sqlInjectionArray.length - 1; i++) {
                    if ($filter('lowercase')($scope.OrderBys).match($scope.sqlInjectionArray[i])) {
                        alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in OrderBy");
                        return;
                    }
                }
                $scope.OrderBy = 1;
            }
               
            }
            else {
                $scope.OrderBy = 0;
            }

            searchTableDetails($scope.Querys, $scope.ColumnNames, $scope.TableNames, $scope.whereCondition, $scope.Conditions, $scope.GroupBy, $scope.GroupBys, $scope.OrderBy, $scope.OrderBys);
           

        // 7. Check for Chart Select Query -> In this fucntion we check for SQL injection in user entered select query if any key word from the array list is found then we give msg to user to entert he valid select query

            if ($scope.chartQuerys != "") {
                $scope.whereCondition = 0;
                for (var i = 0; i < $scope.sqlInjectionArray.length - 1; i++) {
                    if ($filter('lowercase')($scope.chartQuerys).match($scope.sqlInjectionArray[i])) {
                        alert("Sorry " + $scope.sqlInjectionArray[i] + " keyword is not accepted in select query");
                        return;
                    }
                }
                searchbildChartData($scope.chartQuerys, $scope.ColumnNames, $scope.TableNames, $scope.whereCondition, $scope.Conditions, $scope.GroupBy, $scope.GroupBys, $scope.OrderBy, $scope.OrderBys);

                return;
            }
            else {
                alert("Enter Your Chart Select Query !");
                return;
            }
    }


    // Main Select and Bind function
    //All query details entered by user after validation this method will be called to bind the result to the Dashboard page.
    function searchTableDetails(sqlQuery, columnName, tableNames, isCondition, conditionList, isGroupBY, groupBYList, isOrderBY, orderBYList) {
       
        $http.get('/api/DashboardAPI/getDashboardDetails/', { params: { sqlQuery: sqlQuery, columnName: columnName, tableNames: tableNames, isCondition: isCondition, conditionList: conditionList, isGroupBY: isGroupBY, groupBYList: groupBYList, isOrderBY: isOrderBY, orderBYList: orderBYList } }).success(function (data) {

            $scope.dashBoadData = angular.fromJson(data);;
            //alert($scope.dashBoadData.length);

            //if ($scope.dashBoadData.length > 0) {

            //}
        })
   .error(function () {
       $scope.error = "An Error has occured while loading posts!";
   });
    }


    // For binding the Chart result to Listbox before bind result to Chart
    function searchbildChartData(sqlQuery, columnName, tableNames, isCondition, conditionList, isGroupBY, groupBYList, isOrderBY, orderBYList) {
 
        $http.get('/api/DashboardAPI/getDashboardDetails/', { params: { sqlQuery: sqlQuery, columnName: columnName, tableNames: tableNames, isCondition: isCondition, conditionList: conditionList, isGroupBY: isGroupBY, groupBYList: groupBYList, isOrderBY: isOrderBY, orderBYList: orderBYList } }).success(function (data) {

            $scope.itemData = angular.fromJson(data);           
            $scope.itemCount = $scope.itemData.length;          
            $scope.selectedItem = $scope.itemData[0].Name;           
            $scope.minsnew = $scope.itemData[0].Value;            
            $scope.maxnew = $scope.itemData[$scope.itemData.length-1].Value;    
        })
   .error(function () {
       $scope.error = "An Error has occured while loading posts!";
   });
    }

});