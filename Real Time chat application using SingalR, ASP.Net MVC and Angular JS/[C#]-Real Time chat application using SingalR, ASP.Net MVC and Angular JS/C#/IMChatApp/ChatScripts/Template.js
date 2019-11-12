
var chatBoxElemet=' <div class="container"><div class="row"><div class="panel panel-primary">'
chatBoxElemet +='<div class="panel-heading"> <span class="glyphicon glyphicon-comment"></span> {{UserInPrivateChat.name}} '
chatBoxElemet +='<div class="btn-group pull-right">'
chatBoxElemet +='<button type="button" class="btn btn-default btn-xs" ng-click="ClosePrivateWindow()">'
chatBoxElemet +='<span class="glyphicon glyphicon-remove"> </span>'
chatBoxElemet +='</button>'
chatBoxElemet +='</div>'
chatBoxElemet +='</div>'
chatBoxElemet +='<div class="panel-body" style="overflow-y:scroll; min-height:200px; max-height:200px;">'
chatBoxElemet +='<ul class="chat">'
chatBoxElemet +='<li ng-class="(msg.from == UserInPrivateChat.name )? \'left\' : \'right\'" class="clearfix" ng-repeat="msg in PrivateMessages | filter:({to: UserInPrivateChat.ConnectionId})">'
chatBoxElemet +='<div class="chat-body clearfix">'
chatBoxElemet +='<div class="header">'
chatBoxElemet +='<i ng-class="(msg.from == UserInPrivateChat.name )? \'sender\' : \'reciver\'" class="fa fa-user"></i>'
chatBoxElemet +='<strong class="primary-font">{{msg.from}}</strong>';
chatBoxElemet +='<small ng-class="(msg.from == UserInPrivateChat.name )? \'pull-left\' : \'pull-right\'"';
chatBoxElemet +='class="text-muted"></small>';
chatBoxElemet +='</div>';
chatBoxElemet +='<div class="chat-message">';
chatBoxElemet +='{{msg.message}}';
chatBoxElemet +='</div>';
chatBoxElemet +='</div>';
chatBoxElemet +='</li>';
chatBoxElemet +='</ul>';
chatBoxElemet +='</div>';
chatBoxElemet +='<div class="panel-footer">';
chatBoxElemet +='<div style="min-height:9px; font-size:7px;">';
chatBoxElemet +='{{usertyping}}';
chatBoxElemet +='</div>';
chatBoxElemet +='<div class="input-group">';
chatBoxElemet +='<input id="btn-input" type="text" class="form-control input-sm" ng-model="pvtmessage" placeholder="Type your message here..." ng-keypress="SkeyPress($event)" />';
chatBoxElemet += '<span class="input-group-btn">';
chatBoxElemet +='<button class="btn btn-warning btn-sm" id="btn-chat" ng-click="SendPrivateMessage()">';
chatBoxElemet += 'Send';
chatBoxElemet += '</button>';
chatBoxElemet += '</span>';
chatBoxElemet += '</div></div></div> </div> </div>';