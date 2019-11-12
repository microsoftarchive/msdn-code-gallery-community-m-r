var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || { };

myEvents.controls.confirm = (function () {
    function createConfirms() {
        $("[data-confirm='true']").unbind('click');
        $("[data-confirm='true']").click(function () {
            var self = $(this);
            var message = self.data('confirm-text');
            var dialog = $('#confirm');
            var messageContainer = dialog.find('#confirm-dialog-message');
            var yesButton = dialog.find('#confirm-dialog-yes-button');
            var noButton = dialog.find('#confirm-dialog-no-button');
            var closeButton = dialog.find('#confirm-dialog-delete');

            yesButton.unbind();
            yesButton.click(function () {
                var parentForm = self.parent('form');
                parentForm.submit();
            });

            noButton.unbind();
            noButton.click(function () {
                dialog.hide();
            });

            closeButton.unbind();
            closeButton.click(function () {
                dialog.hide();
            });

            messageContainer.html(message);
            dialog.show();
        });
    };
    
    return {
        createConfirms: createConfirms
    };
}());

(function ($) {
    $(document).ready(function () {
        myEvents.controls.confirm.createConfirms();
    });
})(jQuery);
