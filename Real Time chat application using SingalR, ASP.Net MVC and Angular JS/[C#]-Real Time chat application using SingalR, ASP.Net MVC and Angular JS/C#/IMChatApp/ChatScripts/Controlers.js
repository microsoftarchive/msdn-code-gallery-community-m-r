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
  
        $scope.SendPrivateMessage = function (id,name)
        {          
            //  if ($scope.UserLeft == false) { 
            var txt = $("#txt" + id)
            if (txt.val().trim() != '') {
                //$("#txt392331de-393d-427d-a631-3f52caf06dd1").val()
                var txt = $("#txt" + id)
                console.log(txt);
                signalR.SendPrivateMessage(id, txt.val().trim(), name);
              //  $scope.pvtmessage = '';
                txt.val('');
                }
           // }
            //else {
            //    Flash.create('danger', 'User is offline');
            //}
        }
        $scope.OnlineUsers = [];
        signalR.GetOnlineUsers(function (onlineUsers) {
            $scope.OnlineUsers = $.parseJSON(onlineUsers);
            //console.log($scope.OnlineUsers);
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
        $scope.SkeyPress = function (e, id) {
            //debugger;
            if (e.which == 13)
            {   
                    $scope.SendPrivateMessage(id,name);
                    $scope.usertyping = ''                  
            }
            else if (e.which == 46 || e.which == 8) {
                signalR.UserTyping(id, 'Deleting..');
                window.setTimeout(function () {
                    $scope.usertyping = '';
                }, 500);
            }
            else {
                //$scope.UserInPrivateChat.ConnectionId
                signalR.UserTyping(id, 'Typing..');
                window.setTimeout(function () {
                    $scope.usertyping = '';
                }, 500);
            }
        }
        // PrivateMessage($index)
        $scope.openPvtChat = function (index)
        {
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
           // $scope.ShowPrivateWindow = true;
            $scope.UserInPrivateChat = user;
           // console.log($scope.OnlineUsers);    
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
          // if ($scope.ShowPrivateWindow == false) {
           //     $scope.ShowPrivateWindow = true;
            //}
            debugger;
           $scope.openchats = [];
           // var msgBdy = { room: r, msgx: { message: msg.message, sender: msg.sender, css: msg.css } };
            //$scope.chatHistory.push(msgBdy);
          // var user = $scope.OnlineUsers[connectionId];
            // console.log(user);
           $scope.PrivateMessages.push({ to: toname, from: fromname, message: msg, ConnectionId: connectionId });
          
            if ($rootScope.RootPrivateMessages!=undefined)
               $rootScope.RootPrivateMessages.push({ to: toname, from: fromname, message: msg, ConnectionId: connectionId });
            if ($scope.$parent.UserName != fromname) // otheruser's pm
            {
                if ($scope.UserInPrivateChat == null)
                {
                    $scope.UserInPrivateChat = { name: fromname, ConnectionId: toname };
                }
            }
            console.log($scope.PrivateMessages);
            //if (fromname!=$rootScope.)
            $scope.register_popup(connectionId, fromname);
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
        //$scope.$parent.UserName
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

            var chatBoxElemet = '<div class="panel-body" style="overflow-y:scroll; min-height:200px; max-height:200px;">'
            chatBoxElemet += '<ul class="chat">'
            chatBoxElemet += '<li ng-class="(msg.from == ' + name + ')? \'left\' : \'right\'" class="clearfix" ng-repeat="msg in PrivateMessages | filter:({ConnectionId: \'' + id + '\'})">'
            chatBoxElemet += '<div class="chat-body clearfix">'
            chatBoxElemet += '<div class="header">'
            chatBoxElemet += '<i ng-class="(msg.from != ' + name + ' )? \'sender\' : \'reciver\'" class="fa fa-user"></i>'
            chatBoxElemet += '<strong class="primary-font">{{msg.from}}</strong>';
            chatBoxElemet += '<small ng-class="(msg.from == ' + name + ' )? \'pull-left\' : \'pull-right\'"';
            chatBoxElemet += 'class="text-muted"></small>';
            chatBoxElemet += '</div>';
            chatBoxElemet += '<div class="chat-message">';
           chatBoxElemet += '{{msg.message}}';
            chatBoxElemet += '</div>';
            chatBoxElemet += '</div>';
            chatBoxElemet += '</li>';
            chatBoxElemet += '</ul>';
            chatBoxElemet += '</div>';
            chatBoxElemet += '<div class="panel-footer">';
            chatBoxElemet += '<div style="min-height:9px; font-size:7px;" id="div'+id+'">';
            chatBoxElemet += '{{usertyping}}';
            chatBoxElemet += '</div>';
            chatBoxElemet += '<div class="input-group">';
            //ng-model="pvtmessage"
            chatBoxElemet += '<input id="txt'+ id+'" type="text" class="form-control input-sm"  placeholder="Type your message here..." ng-keypress="SkeyPress($event,\'' + id + '\',\'' + name + '\')" />';
            chatBoxElemet += '<span class="input-group-btn">';
            chatBoxElemet += '<button class="btn btn-warning btn-sm" id="btn-chat" ng-click="SendPrivateMessage(\''+id+'\',\''+name+'\')">';
            chatBoxElemet += 'Send';
            chatBoxElemet += '</button>';
            chatBoxElemet += '</span>';
            console.log(id);
            var elementE = '<div class="popup-box chat-popup" id="' + id + '">';
            elementE = elementE + '<div class="popup-head">';
            elementE = elementE + '<div class="popup-head-left">' + name + '</div>';
            elementE = elementE + '<div class="popup-head-right"><a  data-ng-click="closePvtChat(\'' + id + '\')">&#10005;</a></div>';
            elementE = elementE + '<div style="clear: both"></div></div><div class="popup-messages">';
            elementE = elementE + chatBoxElemet;
            elementE = elementE + '</div></div>';
            angular.element(document.getElementById('ChatRoomsContainer')).append($compile(elementE)($scope));
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