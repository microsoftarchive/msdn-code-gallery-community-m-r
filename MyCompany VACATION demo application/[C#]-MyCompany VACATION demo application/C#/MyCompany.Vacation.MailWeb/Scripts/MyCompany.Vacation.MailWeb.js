
// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before updating the span elements with values from the current message
Office.initialize = function () {
    $(document).ready(function () {
        var item = Office.context.mailbox.item;
        if (session.length == 0) {
            redirect(item.normalizedSubject);
        }
        else {
            window.location.replace('home.aspx?subject=' + item.normalizedSubject);
        }
    });
};
