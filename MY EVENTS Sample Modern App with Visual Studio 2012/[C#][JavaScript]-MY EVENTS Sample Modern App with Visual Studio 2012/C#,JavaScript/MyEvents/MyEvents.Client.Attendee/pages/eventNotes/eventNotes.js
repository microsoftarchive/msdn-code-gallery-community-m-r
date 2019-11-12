(function () {
    "use strict";

    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var localFolder = Windows.Storage.ApplicationData.current.localFolder;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var utils = WinJS.Utilities;

    function createContextControl(count) {
        var contextControl = document.createElement("div");
        contextControl.className = "contextControl";

        var isFlipping = false;
        function radioButtonClicked(eventObject) {
            if (eventObject.propertyName !== "checked" || !eventObject.srcElement.checked) {
                return;
            }
            if (isFlipping) {
                var currentPage = flipView.winControl.currentPage;
                radioButtons[currentPage].checked = true;

            } else {
                var targetPage = eventObject.srcElement.getAttribute("value");
                flipView.winControl.currentPage = parseInt(targetPage);
            }
        }

        flipView.winControl.addEventListener("pagevisibilitychanged", function (eventObject) {
            if (eventObject.detail.visible === true) {
                isFlipping = true;
            }
        }, false);

        var radioButtons = [];
        for (var i = 0; i < count; ++i) {
            var radioButton = document.createElement("input");
            radioButton.setAttribute("type", "radio");

            radioButton.setAttribute("name", "flipperContextGroup");

            radioButton.setAttribute("value", i);

            radioButton.setAttribute("aria-label", (i + 1) + " of " + count);

            radioButton.onpropertychange = radioButtonClicked;

            radioButtons.push(radioButton);

            contextControl.appendChild(radioButton);
        }

        if (count > 0) {
            radioButtons[flipView.winControl.currentPage].checked = true;
        }

        flipView.winControl.addEventListener("pageselected", function () {
            isFlipping = false;
            
            var currentPage = flipView.winControl.currentPage;
            radioButtons[currentPage].checked = true;
        }, false);

        var contextContainer = document.getElementById("contextContainer");
        contextContainer.appendChild(contextControl);
    }

    function bindNotes(eventDefinitionId, sessionId) {
        var fileValues = MyEvents.Storage.getSettingFromContainer(eventDefinitionId);

        if (fileValues) {
            var fileList = [];
            var value = fileValues.first();
            if (value) {
                do {
                    if (!sessionId || value.current.value.sessionId == sessionId) {
                        var note = {
                            comment: value.current.value.comment,
                            imageName: value.current.value.imageName,
                            date: value.current.value.date,
                            picture: value.current.value.picture
                        };

                        fileList.push(note);
                    }
                }
                while (value.moveNext());

                if (fileList.length == 0) {
                    // For demo porpouses sample data is addedd if there are no real notes
                    fileList = MyEvents.Data.Fake.getNotes();
                }
            }
        }
        else {
            // For demo porpouses sample data is addedd if there are no real notes
            fileList = MyEvents.Data.Fake.getNotes();
        }

        var notesList = new WinJS.Binding.List(fileList);
        var control = document.querySelector("#flipView").winControl;
        control.itemDataSource = notesList.dataSource;
        
        createContextControl(fileList.length);
    }

    function initAppBar() {
        var appBar = document.getElementById("appbar");
        if (appBar) {
            var appbarwin = appBar.winControl;
            appbarwin.disabled = true;
        }
    }

    ui.Pages.define(MyEvents.Enums.pages.eventNotes, {
        ready: function (element, options) {
            initAppBar();

            var eventDefinitionId = options && options.eventId;
            var sessionId = options && options.sessionId;
            bindNotes(eventDefinitionId, sessionId);
        }
    });
})();
