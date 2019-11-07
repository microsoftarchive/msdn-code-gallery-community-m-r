(function () {
    'use strict'
    app.controller("chatController", function ($scope, $rootScope, signalR,Flash,$compile) {
    $scope.$parent.UserName = "";
    $scope.rooms = [];// RoomFactory.Rooms;
    $scope.$parent.UserName = $("div#userId").text();;  // prompt("Enter unique name :");
    
    signalR.startHub();  
    $scope.activeRoom = '';
    $scope.chatHistory = [];
    $scope.Users = []
    $scope.RoomsLoggedId = [];     
    $scope.typemsgdisable = true;  
    signalR.UserEntered(function (room, user,cid) {
         if ($scope.activeRoom == room&&user!='') {          
            var result = $.grep($scope.users, function (e) { return e.name == user; })
            console.log("----------");           
            console.log(result);
            if (result != undefined || result != null) {
                $scope.users.push({ name: user, ConnectionId: cid });
                $scope.$apply();
            }
        }
    });
    signalR.UserLoggedOut(function (room, user) {
        if ($scope.activeRoom == room && user != '') {          
            $scope.users = $scope.users.filter(function (themObjects) {
                return themObjects.name != user;
            });          
               $scope.$apply();
            }        
    });

  //  Flash.add('success', message, 'custom-class')
    signalR.Login($scope.$parent.UserName);
       
    ///////////////// server
    
   
    $scope.UsersCount = 0;
    $scope.bubblesCount = [];
    $scope.maxBubbles = 10;
            
        $scope.ClosePrivateWindow = function ()
        {
            $scope.ShowPrivateWindow = false;
            $scope.UserInPrivateChat = null;

           // close_popup();
        }
       // $("#PrivateChatArea").css("display", "block");
        $scope.UserInPrivateChat = null;
        $scope.ShowPrivateWindow = false;
        $scope.PrivateMessages = [];
        $scope.currentprivatemessages = {};
        $scope.pvtmessage = '';
      $scope.OpenPrivatewindow = function (index) {        
        var user = $scope.users[index];
      //  var conId = '#' + user.ConnectionId;
        $scope.ShowPrivateWindow = true;
        $scope.UserInPrivateChat = user;
        console.log("CID :" + $scope.UserInPrivateChat.Connectionid);
          //$scope.$apply();
        $scope.$evalAsync();
       // $scope.createPrivateChatWindow($scope.$parent.UserName, conId, user.name)
      }
  
        $scope.SendPrivateMessage = function ()
        {
             debugger;
          //  if ($scope.UserLeft == false) { 
                if ($scope.pvtmessage != '') {
                    signalR.SendPrivateMessage($scope.UserInPrivateChat.ConnectionId, $scope.pvtmessage, $scope.UserInPrivateChat.name);
                    $scope.pvtmessage = '';
                }
           // }
            //else {
            //    Flash.create('danger', 'User is offline');
            //}
        }
        $scope.OnlineUsers = [];
        signalR.GetOnlineUsers(function (onlineUsers) {
            $scope.OnlineUsers = $.parseJSON(onlineUsers);
            console.log($scope.OnlineUsers);
            //$scope.$apply();
            $scope.$evalAsync();
        });
        $scope.ChangeStatus = function (status) {
            signalR.UpdateStatus(status);
        }
        signalR.NewOnlineUser(function (user) {
            $scope.OnlineUsers.push(user);
            $scope.$apply();
            //var message = '<strong> !!</strong>' + user.name + ' in online';
            //Flash.create('success', message, 'custom-class');
        });
        signalR.UpdateConnectionId(function (oid,nid) {
            if ($scope.OnlineUsers[i].ConnectionId == oid)
                $scope.OnlineUsers[i].ConnectionId = nid;
            $scope.$evalAsync();
        });
        signalR.NewOfflineUser(function (user) {
            $.each($scope.OnlineUsers, function (i) {
                if ($scope.OnlineUsers[i]!=undefined)
                if ($scope.OnlineUsers[i].name === user.name && $scope.OnlineUsers[i].ConnectionId==user.ConnectionId) {
                        $scope.OnlineUsers.splice(i, 1);
                        var message = '<strong> !! ' + user.name + '</strong> left the chat ';              
                        Flash.create('danger', message); 
                    }
               });            
            if ($scope.UserInPrivateChat != undefined)
            {
                debugger;
                if ($scope.UserInPrivateChat.ConnectionId == user.ConnectionId) {
                    $scope.UserLeft = true;
                }
                else {
                    $scope.UserLeft = false;
                }
            }
           // $scope.OnlineUsers.push(user);
            $scope.$apply();
        });
        $scope.UserLeft = false;
        $scope.SkeyPress =function(e) {
            if (e.which == 13)
            {   
                    $scope.SendPrivateMessage();
                    $scope.usertyping = ''                  
            }
            else if (e.which == 46 || e.which == 8) {
                signalR.UserTyping($scope.UserInPrivateChat.ConnectionId, 'Deleting..');
                window.setTimeout(function () {
                    $scope.usertyping = '';
                }, 500);
            }
            else {              
                signalR.UserTyping($scope.UserInPrivateChat.ConnectionId, 'Typing..');
                window.setTimeout(function () {
                    $scope.usertyping = '';
                }, 500);
            }
        }
        // PrivateMessage($index)
        $scope.openPvtChat = function (index)
        {
            debugger;
            console.log(index);
            var user = $scope.OnlineUsers[index];
           $scope.register_popup(user.ConnectionId, user.name);
        }
        $scope.closePvtChat=function(id)
        {
            debugger;
            close_popup(id);
        }
        $scope.PrivateMessage = function (index) {
            debugger;
            var user = $scope.OnlineUsers[index];
            $scope.ShowPrivateWindow = true;
            $scope.UserInPrivateChat = user;
            console.log($scope.OnlineUsers);    
            //$scope.$apply();
            $scope.$evalAsync();   
        };
        $scope.usertyping = '';
        signalR.IsTyping(function (connectionid, msg) {
            if($scope.UserInPrivateChat!=undefined)
            if ($scope.UserInPrivateChat.ConnectionId == connectionid)
                $scope.usertyping = msg;
            else
                $scope.usertyping = '';
            // $scope.$apply();
            $scope.$evalAsync();
            window.setTimeout(function () {
                $scope.usertyping = '';
                //  $scope.$apply();
                $scope.$evalAsync();
            }, 500);
        });
        signalR.StatusChanged(function (connectionId, status)
        {
            $.each($scope.OnlineUsers, function (i) {
                if ($scope.OnlineUsers[i].ConnectionId === connectionId) {
                    $scope.OnlineUsers[i].status = status;                    
                }
            });
            // $scope.OnlineUsers.push(user);
            // $scope.$apply();
            $scope.$evalAsync();

        });
        //$rootScope.RootPrivateMessages = [];

        signalR.RecievingPrivateMessage(function (toname, fromname, msg, connectionId) {
           if ($scope.ShowPrivateWindow == false) {
                $scope.ShowPrivateWindow = true;
            }
           // var msgBdy = { room: r, msgx: { message: msg.message, sender: msg.sender, css: msg.css } };
            //$scope.chatHistory.push(msgBdy);
           $scope.PrivateMessages.push({ to: toname, from: fromname, message: msg });
           if ($rootScope.RootPrivateMessages!=undefined)
             $rootScope.RootPrivateMessages.push({ to: toname, from: fromname, message: msg });

            if ($scope.$parent.UserName != fromname) // otheruser's pm
            {
                if ($scope.UserInPrivateChat == null)
                {
                    $scope.UserInPrivateChat = { name: fromname, ConnectionId: toname }
                }
            }

            $scope.$evalAsync();
            // $scope.$evalAsync();
            $rootScope.$evalAsync();
            $rootScope.updateMessage(); //$evalAsync();
            /// To Scroll the message window.
            if ($("#PrivateChatArea div.panel-body").length == 1) {
            var    $container = $("#PrivateChatArea div.panel-body");
                $container[0].scrollTop = $container[0].scrollHeight;
                $container.animate({ scrollTop: $container[0].scrollHeight }, "fast");
            }          
           // $scope.AddMessageToRoom(msgBdy);
        });
        $("#PrivateChatArea").css("display", "block");

        /////////////////////////////New Code For Multiple Chat Windows////////////////////////
       // @$scope.popupsId=[];
        $scope.register_popup = function(id, name) {
            for (var iii = 0; iii < popups.length; iii++) {
                //already registered. Bring it to front.
                if (id == popups[iii]) {
                    Array.remove(popups, iii);
                    popups.unshift(id);
                    calculate_popups();
                    return;
                }
            }
            console.log(id);
            var elementE = '<div class="popup-box chat-popup" id="' + id + '">';
            elementE = elementE + '<div class="popup-head">';
            elementE = elementE + '<div class="popup-head-left">' + name + '</div>';
            elementE = elementE + '<div class="popup-head-right"><a  data-ng-click="closePvtChat(\'' + id + '\')">&#10005;</a></div>';
            elementE = elementE + '<div style="clear: both"></div></div><div class="popup-messages">';
            elementE = elementE +' <input id="btn-input" type="text" class="form-control input-sm" ng-model="pvtmessage" placeholder="Type your message here..." ng-keypress="SkeyPress($event)" />';
            elementE += '<span class="input-group-btn">';
            elementE += '<button class="btn btn-warning btn-sm" id="btn-chat" ng-click="SendPrivateMessage()">';
            elementE += 'Send';
            elementE += '</button>';

            elementE = elementE + '</div></div>';





            //angular.element("#ChatRoomsContainer").append(elementE);
            angular.element(document.getElementById('ChatRoomsContainer')).append($compile(elementE)($scope));
            console.log(elementE);
           // $compile(elementE)($scope);
           // angular.element("#ChatRoomsContainer").append(elementE);
          //  $("#ChatRoomsContainer").append(element);
            popups.unshift(id);
            calculate_popups();
        }
        function calculate_popups() {
            var width = window.innerWidth;
            if (width < 540) {
                total_popups = 0;
            }
            else {
                width = width - 200;
                //320 is width of a single popup box
                total_popups = parseInt(width / 320);
            }
            display_popups();
        }
        Array.remove = function (array, from, to) {
            var rest = array.slice((to || from) + 1 || array.length);
            array.length = from < 0 ? array.length + from : from;
            return array.push.apply(array, rest);
        };
        //this variable represents the total number of popups can be displayed according to the viewport width
        var total_popups = 0;
        //arrays of popups ids
        var popups = [];
        //this is used to close a popup
        function close_popup(id) {
            for (var iii = 0; iii < popups.length; iii++) {
                if (id == popups[iii]) {
                    Array.remove(popups, iii);
                    document.getElementById(id).style.display = "none";
                    calculate_popups();
                    return;
                }
            }
        }
        //displays the popups. Displays based on the maximum number of popups that can be displayed on the current viewport width
        function display_popups() {
            var right = 200;
            var iii = 0;
            for (iii; iii < total_popups; iii++) {
                if (popups[iii] != undefined) {
                    var element = document.getElementById(popups[iii]);
                    element.style.right = right + "px";
                    right = right + 300;
                    element.style.display = "block";
                }
            }
            for (var jjj = iii; jjj < popups.length; jjj++) {
                var element = document.getElementById(popups[jjj]);
                element.style.display = "none";
            }
        }       
});        
})();