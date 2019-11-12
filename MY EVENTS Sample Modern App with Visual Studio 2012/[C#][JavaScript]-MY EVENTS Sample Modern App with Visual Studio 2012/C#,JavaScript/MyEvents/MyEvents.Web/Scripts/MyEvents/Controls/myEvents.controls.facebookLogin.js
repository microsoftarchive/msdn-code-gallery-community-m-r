var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};

myEvents.controls.facebookLogin = (function () {

    $(document).ready(function () {
        $('#fb-login-button').click(function () {
            login();
        });

        $('#fb-logout-button').click(function () {
            logout();
        });
    });

    window.fbAsyncInit = function () {
        FB.init({
            appId: myEvents.app.settings.facebookAppId,
            status: true,
            cookie: true,
            xfbml: true
        });     
    };

    function login() {
        var form = document.createElement("form");
        form.setAttribute("method", 'post');
        form.setAttribute("action", '/Account/Login');
        document.body.appendChild(form);

        var returnUrl = window.location.href;
        var returnUrlField = document.createElement("input");
        returnUrlField.setAttribute("type", "hidden");
        returnUrlField.setAttribute("name", 'returnUrl');
        returnUrlField.setAttribute("value", returnUrl);
        form.appendChild(returnUrlField);

        form.submit();
    }

    function logout() {
        var form = document.createElement("form");
        form.setAttribute("method", 'post');
        form.setAttribute("action", '/Account/LogOut');
        document.body.appendChild(form);
        form.submit();
    }

    // Load the SDK Asynchronously
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));


    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.search);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    return {
        login: login,
        logout: logout
    };
}());