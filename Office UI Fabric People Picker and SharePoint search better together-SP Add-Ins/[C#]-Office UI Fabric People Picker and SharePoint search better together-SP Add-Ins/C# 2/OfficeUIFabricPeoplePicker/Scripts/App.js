/// <reference path="PeoplePicker.js" />
'use strict';

var App = window.App || {};

App.initializePage = function () {
    // This code runs when the DOM is ready and creates a peoplepicker
    $(document).ready(function () {
        var peoplePicker = new OfficeUIfabric.PeoplePicker();
        peoplePicker.Components.PeoplePicker.init("_peoplePicker", false);
        peoplePicker.Components.PeoplePicker.init("_peoplePickerMulti", true);
    });
}

ExecuteOrDelayUntilScriptLoaded(App.initializePage, "sp.js");