var MyShuttle = MyShuttle || {};
MyShuttle.Pages = MyShuttle.Pages || {};
MyShuttle.Pages.Home = MyShuttle.Pages.Home || {};

MyShuttle.Pages.Home.Index = function(){
    'use strict';

    var initMap = function(isInExtraSmallScreenWidth){
            var pinLocation = new Microsoft.Maps.Location(MyShuttle.Config.companyLocation.Latitude, MyShuttle.Config.companyLocation.Longitude),
                centerLocation,
                latitudeDeviation;

            if (isInExtraSmallScreenWidth) {
                latitudeDeviation = 0.0020;
            } else{
                latitudeDeviation = -0.0050;
            }

            centerLocation = new Microsoft.Maps.Location(MyShuttle.Config.companyLocation.Latitude + latitudeDeviation, MyShuttle.Config.companyLocation.Longitude);

            var mapOptions = {
                    zoom: 15,
                    center: centerLocation,
                    showScalebar: false,
                    enableSearchLogo: false,
                    showMapTypeSelector: false,
                    showDashboard: false,
                    credentials: MyShuttle.Config.bingMapsKey
                },
                pinOptions = {
                    icon: 'Content/Images/pinmap.png',
                    width: 50,
                    height: 70
                },
                infoBoxOptions = {
                    visible: true,
                    offset: new Microsoft.Maps.Point(-100,85),
                    htmlContent: '<div class="map-infoBox">' + MyShuttle.Config.infoBoxCompanyAddress + '</div>'
                };

            var map = new Microsoft.Maps.Map(document.getElementById('mapDiv'), mapOptions);
            var pin = new Microsoft.Maps.Pushpin(pinLocation, pinOptions);
            var pinInfoBox = new Microsoft.Maps.Infobox(pin.getLocation(), infoBoxOptions);

            // This hack avoids the map zoom when user scrolls into the page.
            Microsoft.Maps.Events.addHandler(map, 'mousewheel', function(e) {
                e.handled = true;
                return true;
            });

            map.entities.push(pin);
            map.entities.push(pinInfoBox);
        },
        initNumbersAnimations = function(){
            var countUpOptions = {
                useEasing: true,
                useGrouping: true,
                separator: ',',
                decimal: '.',
                prefix: '',
                suffix: ''
            };

            // Init the product stats count up animations.
            var clientsStatsCount = new countUp('clients-stats', 0, 5210, 0, 2.5, countUpOptions);
            var vehiclesStatsCount = new countUp('vehicles-stats', 0, 630, 0, 2.5, countUpOptions);
            var driversStatsCount = new countUp('drivers-stats', 0, 870, 0, 2.5, countUpOptions);
            var ridesStatsCount = new countUp('rides-stats', 0, 95120, 0, 2.5, countUpOptions);

            $('#product-stats').waypoint(function() {
                clientsStatsCount.start();
                vehiclesStatsCount.start();
                driversStatsCount.start();
                ridesStatsCount.start();
            }, { offset: 'bottom-in-view' });
        },
        initAnimations = function () {
            new WOW().init();
        };

    var initialize = function(){
        MyShuttle.Pages.Home.Common.initRatingControls();
        if (document.documentElement.clientWidth < MyShuttle.Config.smallScreenMinWidth) {
            initMap(true);
        } else{
            initMap(false);            
            initAnimations();
            initNumbersAnimations();
        }
    };

    return {
        initialize : initialize
    }
}();

MyShuttle.Pages.Home.Index.initialize();