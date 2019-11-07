var config = (function () {
    var urlBase = 'http://localhost:31329/',
        dateFormat = 'MM/DD/YY';
    
    var getUrlBase = function () {
        var url = Security.getIsNoAuth() ? urlBase + 'noauth/' : urlBase;
        return url + 'api/';
    }

    return {
        getUrlBase: getUrlBase,
        dateFormat: dateFormat
    };
})();
