var MyShuttle = MyShuttle || {};
MyShuttle.Pages = MyShuttle.Pages || {};
MyShuttle.Pages.Home = MyShuttle.Pages.Home || {};

MyShuttle.Pages.Home.Common = function(){
    var initSmoothScroll = function(){
            smoothScroll.init({
                speed: 1500,
                easing: 'easeInOutQuart',
                offset: 0,
                updateURL: true
            });
        },
        initScrollSpy = function(){
            $('body').scrollspy({
                target: '#ms-navbar'
            });
        },
    initialize = function(){
        initSmoothScroll();
        initScrollSpy();
        initRatingControls();
    },
    initRatingControls = function () {
        $('.customer-rating').raty({
            starOff: 'Content/Images/starOFF.png',
            starOn: 'Content/Images/starON.png',
            readOnly: true,
            hints: [1, 2, 3, 4, 5],
            score: function () {
                return $(this).attr('data-score');
            }
        });
    };

    return {
        initialize: initialize,
        initRatingControls: initRatingControls
    }
}();

MyShuttle.Pages.Home.Common.initialize();

