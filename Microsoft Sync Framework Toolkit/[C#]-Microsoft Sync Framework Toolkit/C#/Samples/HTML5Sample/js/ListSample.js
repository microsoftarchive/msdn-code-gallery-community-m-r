// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

// ListSample.js contains UI logic for ListSample.htm

var _syncController = null;
var _syncOption = null;

// Helper functions to generate GUID
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function GUID() {
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}
function EnsureDigits(num, len) {
    var str = num.toString();
    while (str.length < len) {
        str = "0" + str;
    }
    return str;
}
// Helper functions to convert date/time between UTC format and JSON format
function UTCToDate(s) {
    //Expected UTC format "yy-mm-ddT00:00:00.000"
    var dt = s.split("T");
    if (dt.length > 0) {
        var d = dt[0].split("-");
        if (d.length == 3) {
            if (dt.length > 1) {
                var t = dt[1].split(":");
                if (t.length == 3) {
                    var s = t[2].split(".");
                    if (s.length == 2) {
                        return new Date(Date.UTC(parseInt(d[0]), parseInt(d[1]), parseInt(d[2]), parseInt(t[0]), parseInt(t[1]), parseInt(s[0]), parseInt(s[1])));
                    }
                    return new Date(Date.UTC(parseInt(d[0]), parseInt(d[1]), parseInt(d[2]), parseInt(t[0]), parseInt(t[1]), parseInt(t[2]), 0));
                }
            }
            return new Date(Date.UTC(parseInt(d[0]), parseInt(d[1]), parseInt(d[2]), 0, 0, 0, 0));
        }
    }
    return new Date(0);
}
function GetJsonDateTime(d) {
    return "\/Date(" + d.getTime() + ")\/";
}
function JsonDateTimeToUTC(jsonDate) {
    var now = new Date(jsonDate ? parseInt(jsonDate.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1')) : 0);
    return EnsureDigits(now.getUTCFullYear(), 4) + "-" +
            EnsureDigits(now.getUTCMonth() + 1, 2)+ "-" +
            EnsureDigits(now.getUTCDate(), 2) + "T" +
            EnsureDigits(now.getUTCHours(), 2) + ":" +
            EnsureDigits(now.getUTCMinutes(), 2) + ":" +
            EnsureDigits(now.getUTCSeconds(), 2);
}
// Global initialization function for ListSample.htm
function InitSyncOption(opt) {
    _syncOption = opt;
}
// Helper function to convert status/priority value to its text format
function GetFistEntityValue(type, filter, field) {
    var entities = _syncOption.storage.getEntities(type, filter);
    for (var i in entities) {
        return entities[i][field];
    }
    return null;
}
// Helper function to generate a dropdown box for status & priority
function GenerateOptions(type, id, name) {
    var entities = _syncOption.storage.getEntities(type, {});
    var options = "";
    for (var i in entities) {
        options += '<option value="' + entities[i][id] + '">' + entities[i][name] + "</option>";
    }
    return options;
}
// Helper function to generate a delete handler for entity
function GenerateDeleteHandler(entity) {
    return function() {
        $("#divDelete").dialog({
            autoOpen: true,
            height: 110,
            width: 200,
            modal: true,
            resizable: false,
            buttons: {
                "Yes": function() {
                    _syncOption.storage.deleteEntity(entity);
                    _syncOption.storage.commit();
                    OnSyncDone();
                    $("#divDelete").dialog("close");
                },
                "No": function() {
                    $("#divDelete").dialog("close");
                }
            }
        });
    }
}
// Helper function to dump a list of properties to HTML
function GenerateHTMLForProperties(properties) {
    var strHTML = "";
    for (var i in properties) {
        if (properties[i]) {
            strHTML += '<span><b>' + i + ':</b>&nbsp;' + properties[i] + '<span/><br/>';
        }
    }
    return strHTML;
}
// Display a dialog box to edit list entity
function DisplayEditListDialog(list) {
    $("#list-name").val(list.Name);
    $("#list-description").val(list.Description);
    $("#list-createddate").val(JsonDateTimeToUTC(list.CreatedDate));
    $("#frmList").dialog({
        autoOpen: true,
        width: 300,
        modal: true,
        resizable: false,
        buttons: {
            "OK": function () {
                list.Name = $("#list-name").val();
                list.Description = $("#list-description").val();
                list.__metadata.isDirty = true;
                _syncOption.storage.setEntity(list);
                _syncOption.storage.commit();
                OnSyncDone();
                $("#frmList").dialog("close");
            },
            "Cancel": function () {
                $("#frmList").dialog("close");
            }
        }
    });
}
// Enable all list tabs
function EnableListTab(sel) {
    $(sel).tabs({
        select: function (event, ui) {
            if (ui.panel.id == "tabs-add") {
                $("#btnAddList").click();
                return false;
            }
            return true;
        }
    });
}
// Enable the button to dump data in offline cache to HTML
function EnableDumpStorageButton(sel) {
    $(sel)
        .button()
        .click(function() {
            var strHtml = "";
            var schema = _syncOption.storage.getSchema();
            var metadata = ["type", "uri", "tempId", "isDeleted", "isDirty"];
            for (var i in schema) {
                strHtml += "<h2>" + i + "</h2><table class='dump'><tr><th class='dump'>Key</th>";
                for (var j in schema[i]) {
                    strHtml += "<th class='dump'>" + j + "</th>";
                }
                for (var j in metadata) {
                    strHtml += "<th class='dump'>" + metadata[j] + "</th>";
                }
                strHtml += "</tr>";
                var table = _syncOption.storage.getTable(i)
                for (var j in table.rows) {
                    strHtml += "<tr><td class='dump'>" + j + "</td>";
                    for (var k in schema[i]) {
                        strHtml += "<td class='dump'>" + table.rows[j][k] + "</td>";
                    }
                    for (var k in metadata) {
                        strHtml += "<td class='dump'>" + table.rows[j].__metadata[metadata[k]] + "</td>";
                    }
                    strHtml += "</tr>";
                }
                strHtml += "</table>";
            }
            $("#divStorage").html(strHtml);
        });
}
// Enable the button to add a new list
function EnableAddListButton(sel) {
    $(sel)
        .button()
        .click(function() {
            var id = GUID();
            var list = {
                "__metadata": {
                    "type": "DefaultScope.List",
                    "tempId": id
                },
                "ID": id,
                "UserID": _syncOption.user.id,
                "Name": "",
                "Description": "",
                "CreatedDate": GetJsonDateTime(new Date())
            };
            DisplayEditListDialog(list);
        });
}
// Display a dialog to edit item entity
function DisplayEditItemDialog(item) {
    $("#item-name").val(item.Name);
    $("#item-description").val(item.Description);
    $("#item-priority").val(item.Priority);
    $("#item-status").val(item.Status);
    $("#item-startdate").val(JsonDateTimeToUTC(item.StartDate)).datepicker({ dateFormat: "yy-mm-ddT00:00:00" });
    $("#item-enddate").val(JsonDateTimeToUTC(item.EndDate)).datepicker({ dateFormat: "yy-mm-ddT00:00:00" });
    $("#frmItem").dialog({
        autoOpen: true,
        width: 300,
        modal: true,
        resizable: false,
        buttons: {
            "OK": function () {
                item.Name = $("#item-name").val();
                item.Description = $("#item-description").val();
                item.Priority = $("#item-priority").val();
                item.Status = $("#item-status").val();
                item.StartDate = GetJsonDateTime(UTCToDate($("#item-startdate").val()));
                item.EndDate = GetJsonDateTime(UTCToDate($("#item-enddate").val()));
                item.__metadata.isDirty = true;
                _syncOption.storage.setEntity(item);
                _syncOption.storage.commit();
                OnSyncDone();
                $("#frmItem").dialog("close");
            },
            "Cancel": function () {
                $("#frmItem").dialog("close");
            }
        }
    });
}
// Generate a handle to add an item
function GenerateAddItemHandler(list) {
    return function () {
        var id = GUID();
        var item = {
            "__metadata": {
                "type": "DefaultScope.Item",
                "tempId": id
            },
            "ID": id,
            "ListID": list.ID,
            "UserID": _syncOption.user.id,
            "Name": "",
            "Description": "",
            "Priority": 0,
            "Status": 0,
            "StartDate": GetJsonDateTime(new Date()),
            "EndDate": GetJsonDateTime(new Date())
        };
        DisplayEditItemDialog(item);
    };
}
// Add an item entity into a list tab in UI
function AddListItem(sel, item) {
    $(sel)
        .append($('<h3><a href="#">' + item.Name + '</a></h3>'))
        .append($(
            '<div><table style="width:100%"><tr><td>' + GenerateHTMLForProperties({
                    "Description": item.Description,
                    "Priority": GetFistEntityValue("DefaultScope.Priority", { ID: item.Priority }, "Name"),
                    "Status": GetFistEntityValue("DefaultScope.Status", { ID: item.Status }, "Name"),
                    "Start Date": JsonDateTimeToUTC(item.StartDate),
                    "End Date": JsonDateTimeToUTC(item.EndDate)
                    }) +
                '</td></tr><tr><td><br><button id="edit-' + item.ID + '">Edit Item</button>&nbsp;<button id="delete-' + item.ID + '">Delete Item</button></td>' +
                '</tr></table></div>'
                ));
                    $("#edit-" + item.ID)
        .button()
        .click(function () {
            DisplayEditItemDialog(item);
        });
    $("#delete-" + item.ID)
        .button()
        .click(GenerateDeleteHandler(item));
}
// Add a list tab in UI
function AddListTab(list) {
    $("<li></li>")
            .append(
                $("<a></a>")
                    .attr("href", "#" + list.ID)
                    .text(list.Name)
            )
            .insertBefore($("#divList > ul > li:last"));
    $("<div></div>")
            .attr("id", list.ID)
            .html(
                '<table style="width:100%"><tr><td>' + GenerateHTMLForProperties({
                    "Description": list.Description,
                    "Created Date": JsonDateTimeToUTC(list.CreatedDate)
                    }) +
                '</td><td class="buttons"><button id="edit-' + list.ID + '">Edit List</button>&nbsp;<button id="delete-' + list.ID + '">Delete List</button>&nbsp;<button id="additem-' + list.ID + '">Add Item</button></td>' +
                '</tr></table>' +
                '<hr><h3>Items</h3><div id="items-' + list.ID + '"></div>'
            )
            .insertBefore($("#tabs-add"));
    var items = _syncOption.storage.getEntities("DefaultScope.Item", { ListID: list.ID });
    for (var i in items) {
        AddListItem("#items-" + list.ID, items[i]);
    }
    $("#edit-" + list.ID)
        .button()
        .click(function() {
            DisplayEditListDialog(list);
        });
    $("#delete-" + list.ID)
        .button()
        .click(GenerateDeleteHandler(list));
    $("#additem-" + list.ID)
        .button()
        .click(GenerateAddItemHandler(list));
    $("#items-" + list.ID).accordion({ autoHeight: false });
}
// Refresh UI when sync is done
function OnSyncDone() {
    var selectedTab = $("#divList").tabs('option', 'selected');
    $("#divList")
        .hide()
        .tabs("destroy")
        .html('<ul><li><a href="#tabs-add">+</a></li></ul><div id="tabs-add"><button id="btnAddList">Add List</button></div>');
    var lists = _syncOption.storage.getEntities("DefaultScope.List", {});
    for (var i in lists) {
        AddListTab(lists[i]);
    }
    $("#item-priority").html(GenerateOptions("DefaultScope.Priority", "ID", "Name"));
    $("#item-status").html(GenerateOptions("DefaultScope.Status", "ID", "Name"));
    EnableAddListButton("#btnAddList");
    EnableListTab("#divList");
    $("#divList").tabs('select', selectedTab);
    $("#divList").show();
}
// Kick off a sync when user logs in
function OnLoginReady(user) {
    if (user != null) {
        $.data($("#frmLogin")[0], "ReadyToClose", true);
        $("#frmLogin").dialog("close");
        $("#divList").tabs("disable");
        _syncOption.user = user;
        _syncOption.syncDoneCallback = OnSyncDone;
        _syncOption.storage.init(user);
        _syncController = new SyncController(_syncOption);
        _syncController.sync();
    }
    else {
        window.alert("Error occurs during login, please try again...");
    }
}
// Try to log in user when Login button is clicked
function OnLogin() {
    if ($("#user").val() != "") {
        $("#tabs-debug").html("");
        $("#frmLogin").dialog("disable");
        DoLogin($("#user").val(), _syncOption, OnLoginReady);
    }
    else {
        window.alert("Please type in your user name");
    }
}
// Display a dialog for user login
function DisplayLoginDialog() {
    $("#frmLogin").dialog({
        disabled: false,
        autoOpen: true,
        width: 250,
        height: 110,
        modal: true,
        resizable: false,
        buttons: {
            "Login": OnLogin
        },
        open: function () {
            $.data($("#frmLogin")[0], "ReadyToClose", false);
        },
        beforeclose: function () {
            return $.data($("#frmLogin")[0], "ReadyToClose");
        }
    });
}
// UI Initialization
$(function() {
    SetDebugOutput("#tabs-debug");
    EnableAddListButton("#btnAddList");
    EnableDumpStorageButton("#btnDumpStorage");
    EnableListTab("#divList");
    $("#divDebug").tabs();
    $("#frmList").dialog({ autoOpen: false });
    $("#frmItem").dialog({ autoOpen: false });
    $("#divDelete").dialog({ autoOpen: false });
    $("#frmLogin").submit(function() {
        OnLogin();
        return false;
    });
    $("#btnSync")
        .button()
        .click(function() {
            _syncController.sync();
        });
    $("#btnSwitch")
        .button()
        .click(function() {
            DisplayLoginDialog();
        });
    $("#btnClear")
        .button()
        .click(function() {
            _syncOption.storage.clearLocal();
            OnSyncDone();
            $("#btnSwitch").click();
        });
    $(".content").show();
    DisplayLoginDialog();
})