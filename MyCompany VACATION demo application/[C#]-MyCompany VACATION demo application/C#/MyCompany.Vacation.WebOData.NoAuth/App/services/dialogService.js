vacationApp.factory('dialogService', ['$dialog', 'enums',
    function ($dialog, enums) {
        var defaultOptions = {
            dialogClass: 'modal-msgBox',
            backdrop: true,
            keyboard: true,
            backdropClick: true,
            templateUrl: 'App/views/messageBox.html',
            controller: 'MsgBoxController'
        };

        var messageBox = function (message, options) {
            var dialogOptions = angular.copy(defaultOptions);

            dialogOptions.resolve = {
                model: function () {
                    return {
                        message: message,
                        options: options || [enums.options.ok]
                    };
                }
            };
            var dialog = $dialog.dialog(dialogOptions);
            return dialog.open();
        };

        var open = function (view, controller, model) {
            var dialogOptions = angular.copy(defaultOptions);
            dialogOptions.templateUrl = view;
            dialogOptions.controller = controller;

            dialogOptions.resolve = {
                model: function () {
                    return model;
                }
            };
            var dialog = $dialog.dialog(dialogOptions);
            return dialog.open();
        };

        return {
            messageBox: messageBox,
            open: open
        };
    }]);
