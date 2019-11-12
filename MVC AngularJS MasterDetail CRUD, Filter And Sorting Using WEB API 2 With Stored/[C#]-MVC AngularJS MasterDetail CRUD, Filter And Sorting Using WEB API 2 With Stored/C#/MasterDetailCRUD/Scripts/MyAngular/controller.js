// <reference path="../angular.js" />  
/// <reference path="../angular.min.js" />   
/// <reference path="../angular-animate.js" />   
/// <reference path="../angular-animate.min.js" />  
var app;

(function () {
    app = angular.module("OrderModule", ['ngAnimate']);
})();


app.controller("AngularJsOrderController", function ($scope, $timeout, $rootScope, $window, $http) {
    $scope.date = new Date();
    $scope.MyName = "shanu";

    //For Order Master Search 
    $scope.OrderNos = "";
    $scope.Table_IDs = "";   

    //This variable will be used for Insert/Edit/Delete OrderMasters Table.
    $scope.OrderNo = 0;
    $scope.Table_ID = "";
    $scope.Description = "";
    $scope.Waiter_Name = "";

    //Show Hide OrderMaster Table
    $scope.showOrderMasterAdd = true;
    $scope.addEditOrderMaster = false;
    $scope.OrderMasterList = true;
    $scope.showItem = true;

    //This variable will be used for Insert/Edit/Delete OrderDetail Table.
    $scope.Order_Detail_No = 0;
    $scope.Item_Name ="";
    $scope.Notes = "";
    $scope.QTY = "1";
    $scope.Price = "0";

   
    $scope.addEditOrderDetail = false;
    $scope.expandImg = "expand.png";  

    selectOrderMasters($scope.OrderNos, $scope.Table_IDs);

    function selectOrderMasters(OrderNos, Table_IDs) {


        $http.get('/api/OrderAPI/', { params: { OrderNO: OrderNos, TableID: Table_IDs } }).success(function (data) {
            $scope.OrderMasters = data;

            $scope.showOrderMasterAdd = true;
            $scope.addEditOrderMaster = false;
            $scope.OrderMasterList = true;
            $scope.showItem = true;
            $scope.addEditOrderDetail = false;

            if ($scope.OrderMasters.length > 0) {

            }
        })
   .error(function () {
       $scope.error = "An Error has occured while loading posts!";
   });
    }


    //Search
    $scope.searchOrderMasters = function () {

        selectOrderMasters($scope.OrderNos, $scope.Table_IDs);
    }

    //Edit Student Details
    $scope.OrderMasterEdit = function OrderMasterEdit(OrderNoss, Table_IDss, Descriptionss, Waiter_Namess) {
        cleardetails();
        $scope.OrderNo = OrderNoss;
        $scope.Table_ID = Table_IDss
        $scope.Description = Descriptionss;
        $scope.Waiter_Name = Waiter_Namess;
     
        $scope.addEditOrderDetail = false;
        $scope.showOrderMasterAdd = true;
        $scope.addEditOrderMaster = true;
        $scope.OrderMasterList = true;
        $scope.showItem = true;
    }

    //Delete Dtudent Detail
    $scope.OrderMasterDelete = function OrderMasterDelete(OrderNoss) {
        cleardetails();
        $scope.OrderNo = OrderNoss;
       
        var delConfirm = confirm("Are you sure you want to delete the Order Master " + OrderNoss + " ?");
        if (delConfirm == true) {
         //   alert($scope.OrderNo);
            $http.get('/api/OrderAPI/deleteOrderMaster/', { params: { OrderNo: $scope.OrderNo } }).success(function (data) {
               // alert(data);
                $scope.orderMasterDeleted= data;
                alert($scope.orderMasterDeleted);
                cleardetails();
                selectOrderMasters('', '');
            })
      .error(function () {
          $scope.error = "An Error has occured while loading posts!";
      });

        }
    }

    // New Student Add Details
    $scope.showOrderMasters = function () {
        cleardetails();
        $scope.addEditOrderDetail = false;
        $scope.showOrderMasterAdd = true;
        $scope.addEditOrderMaster = true;
        $scope.OrderMasterList = true;
        $scope.showItem = true;

    }

    //clear all the control values after insert and edit.
    function cleardetails() {
        $scope.OrderNo = 0;
        $scope.Table_ID = "";
        $scope.Description = "";
        $scope.Waiter_Name = "";
      
    }

    //Form Validation
    $scope.Message1 = "";
    $scope.IsFormSubmitted1 = false;

    $scope.IsFormValid1 = false;

    $scope.$watch("f2.$valid", function (isValid) {
        $scope.IsFormValid1 = isValid;

    });

    $scope.Message = "";
    $scope.IsFormSubmitted = false;

    $scope.IsFormValid = false;

    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;

    });

    //Save Student
    $scope.saveDetails = function () {
        $scope.IsFormSubmitted1 = true;
        if ($scope.IsFormValid1) {
         
            //if the Student ID=0 means its new Student insert here i will call the Web api insert method
            if ($scope.OrderNo == 0) {

                $http.get('/api/OrderAPI/insertOrderMaster/', { params: { Table_ID: $scope.Table_ID, Description: $scope.Description, Waiter_Name: $scope.Waiter_Name } }).success(function (data) {

                    $scope.orderMasterInserted = data;
                    alert($scope.orderMasterInserted);


                    cleardetails();
                    selectOrderMasters('', '');

                })
         .error(function () {
             $scope.error = "An Error has occured while loading posts!";
         });
            }
            else {  // to update to the  details
                $http.get('/api/OrderAPI/updateOrderMaster/', { params: { OrderNo: $scope.OrderNo, Table_ID: $scope.Table_ID, Description: $scope.Description, Waiter_Name: $scope.Waiter_Name } }).success(function (data) {
                    $scope.orderMasterUpdated = data;
                    alert($scope.orderMasterUpdated);

                    cleardetails();
                    selectOrderMasters('', '');

                })
        .error(function () {
            $scope.error = "An Error has occured while loading posts!";
        });
            }
        }
        else {
            $scope.Message1 = "All the fields are required.";
        }
       


    }

    // To Hide and Display Detail Table
    $scope.isRowHidden = false;
    Hidetables()
    function Hidetables() {

        $scope.isRowHidden = false;
    }

    $scope.neworderNumber = 0;
    $scope.main = this;
    this.activeRow = "0";

    //Display Order Detail Grid
    $scope.totalPrice = 0;
    $scope.totalQty = 0;
    $scope.OrderDetailDisplay = function OrderDetailDisplay(OrderNoss) {
       
        $scope.OrderNo = OrderNoss;
        this.activeRow = 0;
      
        if ($scope.isRowHidden == false)
        {
            this.activeRow = OrderNoss;
            $scope.isRowHidden = true;           
        }
        else
        {
            this.activeRow = 0;
            $scope.isRowHidden = false;          
        }          

        $http.get('/api/DetailAPI/', { params: { OrderNO: OrderNoss } }).success(function (data) {              
            $scope.OrderDetailDisp = data;
            $scope.totalPrice = 0;
            $scope.totalQty = 0;          
           
            for (count = 0; count < $scope.OrderDetailDisp.length; count++) {
                $scope.totalPrice += parseInt($scope.OrderDetailDisp[count].Price);
                $scope.totalQty += ($scope.OrderDetailDisp[count].QTY);             
            }
            })
      .error(function () {
          $scope.error = "An Error has occured while loading posts!";
      });
    }
    

    //Save Order Details

    //clear all the control values after insert and edit.
    function clearOrderdetails() {
        $scope.Order_Detail_No = 0;
        $scope.Item_Name = "";
        $scope.Notes = "";
        $scope.QTY = "1";
        $scope.Price = "0";
    }

   

    //Save Student
    $scope.saveOrderDetails = function () {

        $scope.IsFormSubmitted = true;
        if ($scope.IsFormValid) {
           // alert($scope.Order_Detail_No);
            //if the Student ID=0 means its new Student insert here i will call the Web api insert method
            if ($scope.Order_Detail_No == 0) {
              //  alert($scope.OrderNo)
                $http.get('/api/DetailAPI/insertOrderDetail/', { params: { Order_No: $scope.OrderNo, Item_Name: $scope.Item_Name, Notes: $scope.Notes, QTY: $scope.QTY, Price: $scope.Price } }).success(function (data) {

                    $scope.orderDetailInserted = data;
                    alert($scope.orderDetailInserted);


                    clearOrderdetails();
                    selectOrderMasters('', '');

                })
         .error(function () {
             $scope.error = "An Error has occured while loading posts!";
         });
            }
            else {  // to update to the  details
                $http.get('/api/DetailAPI/updateOrderDetail/', { params: { Order_Detail_No: $scope.Order_Detail_No, Order_No: $scope.Order_No, Item_Name: $scope.Item_Name, Notes: $scope.Notes, QTY: $scope.QTY, Price: $scope.Price } }).success(function (data) {
                    $scope.orderDetailUpdated = data;
                    alert($scope.orderDetailUpdated);

                    clearOrderdetails();
                    selectOrderMasters('', '');

                })
        .error(function () {
            $scope.error = "An Error has occured while loading posts!";
        });
            }

        }
        else {
            $scope.Message = "All the fields are required.";
        }


    }
    // New Detail Add 
    $scope.showNewOrderDetails = function () {
        
        clearOrderdetails();
        $scope.showOrderMasterAdd = false;
        $scope.addEditOrderMaster = false;
        $scope.OrderMasterList = true;
        $scope.showItem = true;

        $scope.addEditOrderDetail = true;

    }

    //Edit Student Details
    $scope.OrderDetailEdit = function OrderMasterEdit(Order_Detail_No,Order_No,Item_Name,Notes,QTY,Price) {
        clearOrderdetails();

        $scope.Order_Detail_No = Order_Detail_No
        $scope.Order_No = Order_No
        $scope.Item_Name = Item_Name;
        $scope.Notes = Notes;
        $scope.QTY = QTY;
        $scope.Price = Price;


        $scope.showOrderMasterAdd = false;
        $scope.addEditOrderMaster = false;
        $scope.OrderMasterList = true;
        $scope.showItem = true;

        $scope.addEditOrderDetail = true;
    }

    //Delete Dtudent Detail
    $scope.OrderDetailDelete = function OrderMasterDelete(Order_Detail_No) {
        clearOrderdetails();
        $scope.Order_Detail_No = Order_Detail_No;

        var delConfirm = confirm("Are you sure you want to delete the Order Master " + Order_Detail_No + " ?");
        if (delConfirm == true) {

            //alert($scope.OrderNo);
            $http.get('/api/DetailAPI/deleteOrderDetail/', { params: { Order_Detail_No: $scope.Order_Detail_No } }).success(function (data) {
              
                $scope.orderDetailDeleted = data;
                alert($scope.orderDetailDeleted);
                clearOrderdetails();
                selectOrderMasters('', '');
            })
      .error(function () {
          $scope.error = "An Error has occured while loading posts!";
      });

        }
    }
});