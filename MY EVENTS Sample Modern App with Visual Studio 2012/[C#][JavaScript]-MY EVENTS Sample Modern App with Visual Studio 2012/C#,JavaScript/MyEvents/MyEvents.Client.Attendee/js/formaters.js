(function () {
    "use strict";
        
    var formatVerboseDate = WinJS.Binding.converter(function (date){
        var utcDate = moment.utc(date);
        return utcDate.format("dddd D MMM YYYY");
    });

    var formatTime = WinJS.Binding.converter(function (date) {
        var utcDate = moment.utc(date);
        return utcDate.format("hh.mm A");
    });

    var formatShortTime = WinJS.Binding.converter(function (date) {
        var utcDate = moment.utc(date);
        return utcDate.format("HH.MM");
    });

    var formatDuration = function (source, sourceProperties, dest, destProperties) {
        var value = source[sourceProperties[0]];
        value = moment.duration(value, "minutes").humanize();
        dest[destProperties[0]] = "( Duration, " + value + " )";
    };

    var formatBoolToDisplay = WinJS.Binding.converter(function (boolValue) {
        if (boolValue)
            return "auto";
        else
            return "none";
    });

    var formatImage = WinJS.Binding.converter(function (logo) {
        return "data:image/jpg;base64," + logo;
    });
    
    var formatLogo = WinJS.Binding.converter(function (logo) {
        return "data:image/jpg;base64," + logo;
    });
    
    var facebookImage = WinJS.Binding.converter(function (facebookId) {
        var offline = MyEvents.Config.getOfflineMode();
        if (offline)
            return "/images/content/facebook-no-picture.png";
        else {
            return "https://graph.facebook.com/" + facebookId + "/picture";
        }                
    });

    var getMonth = WinJS.Binding.converter(function (date) {
        return moment.utc(date).format("MMMM");
    });

    var getDay = WinJS.Binding.converter(function (date) {
        return moment.utc(date).format("D");
    });

    var formatGMTMessage = WinJS.Binding.converter(function (timeZoneOffset) {
        var sign = ""; // negative sign comes already with the number
        if (timeZoneOffset >= 0) {
            sign = "+";
        }

        return "GMT" + sign + timeZoneOffset;
    });

    WinJS.Namespace.define("MyEvents.Formaters", {
        formatVerboseDate: formatVerboseDate,
        formatTime: formatTime,
        formatShortTime: formatShortTime,
        formatDuration: formatDuration,
        formatBoolToDisplay: formatBoolToDisplay,
        formatLogo: formatLogo,
        formatImage: formatImage,
        facebookImage: facebookImage,
        getMonth: getMonth,
        getDay: getDay,
        formatGMTMessage: formatGMTMessage
    });
    
    WinJS.Utilities.markSupportedForProcessing(MyEvents.Formaters.formatDuration);
})();