var MyShuttle = MyShuttle || {};
MyShuttle.Pages = MyShuttle.Pages || {};
MyShuttle.Pages.CarrierList = MyShuttle.Pages.CarrierList || {};

MyShuttle.Pages.CarrierList.Index = function () {
    'use strict';

    var initHandlers = function () {
        $('#registerExpand').click(expandForm);
        $('#searchBtn').click(search);
        $('#searchInput').keypress(testSearch);
    },
    initialize = function () {
        MyShuttle.Pages.Home.Common.initRatingControls();
        initHandlers();
    },
    expandForm =function(ev) {
        $(".register-carrier-form").collapse('show');
        $(this).toggleClass('hide');
    },
    testSearch = function (ev) {
        if (ev.which == 13) { search(ev); }
    },
    search = function(ev) {
        $("#searchForm").submit();
    };

    return {
        initialize: initialize
    }
}();

MyShuttle.Pages.CarrierList.Index.initialize();