define(['services/dataservice', 'config', 'services/logger', 'viewmodels/base'], function (dataservice, config, logger, base) {
    var initialized = false,
        map,
        maxImagesPerPushpin = 5,
        travelDistribution = ko.observable(),
        maxCityPercentage = 0,
        locs = [],
        infobox,
        dataLayer,
        isNoAuth = config.isNoAuth;

    travelDistribution.subscribe(function (distribution) {
        createPushpins(distribution);
    });

    var vm = {
        activate: activate,
        viewAttached: viewAttached,
        isNoAuth: isNoAuth
    };

    return vm;

    function activate() {
        base.showLoading();
        if (initialized) return;

        initialized = true;
    }

    function viewAttached(view) {
        if (!isNoAuth)
            getMap();

        base.hideLoading();
        return true;
    }

    function getMap() {
        var options = {
            mapType: Microsoft.Maps.MapTypeId.road,
            zoom: 4,
            showDashboard: true,
            defaultLongitude: -92,
            defaultLatitude: 44,
        }

        map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
            {
                credentials: config.bingCredentialsKey,
                mapTypeId: options.mapType,
                zoom: options.zoom,
                center: new Microsoft.Maps.Location(options.defaultLatitude, options.defaultLongitude),
                showDashboard: options.showDashboard
            });


        infobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0), { visible: false, offset: new Microsoft.Maps.Point(0, 20) });
        Microsoft.Maps.Events.addHandler(infobox, 'click', hideInfobox);

        map.entities.push(infobox);

        getTravelDistribution();
    }

    function createPinFromCoordinates(coordinates, cityDistribution, callback) {
        var location = new Microsoft.Maps.Location(coordinates[0], coordinates[1]);
        var size = Math.max(cityDistribution.percent() * 100 / maxCityPercentage, 20);
        var anchorPoint = new Microsoft.Maps.Point(size / 2, size / 2)
        var pin = new Microsoft.Maps.Pushpin(location, {
            anchor: anchorPoint,
            height: size,
            width: size,
            htmlContent: "<div class='pushpin' style='border-radius: " + size / 2 + "px; height: " + size + "px; width: " + size + "px'></div>"
        });

        Microsoft.Maps.Events.addHandler(pin, 'click', function () { showInfoBox(pin, cityDistribution); });
        map.entities.push(pin);

        locs.push(location);
        var bestview = Microsoft.Maps.LocationRect.fromLocations(locs);
        map.setView({ bounds: bestview });
    };

    function showInfoBox(pin, cityDistribution) {
        infobox.setLocation(pin.getLocation());
        var pixelLocation = map.tryLocationToPixel(pin.getLocation());
        var xOffset = -230;
        var upDown = "down";
        var leftRight = "right";
        if (pixelLocation.x < -280) {
            xOffset = 0;
            leftRight = "left";
        }

        var yOffset = 20;
        if (pixelLocation.y < -140) {
            yOffset = -150;
            upDown = "up";
        }

        var arrowStyle = "arrow-" + upDown + "-" + leftRight;
        infoBoxHtml = '<div class="infoboxText">' +
            '<div><div class="header-item">total</div><div class="header-item">last month</div><div class="header-item">last year</div></div>' +
            '<div><div class="data-item">' + cityDistribution.percent() + '<span class="middle-font-size">%<span></div><div class="data-item">' + cityDistribution.monthCount() + '</div><div class="data-item">' + cityDistribution.yearCount() + '</div></div>' +
            '<div>';

        for (var i = 0; i < maxImagesPerPushpin; i++)
            infoBoxHtml += '<div style="' + getBackgroundImage(cityDistribution, i) + '" class="photo"></div>';

        infoBoxHtml += '</div></div><div class="' + arrowStyle + '" ></div>';

        infobox.setHtmlContent(infoBoxHtml);
        infobox.setOptions({ visible: true, offset: new Microsoft.Maps.Point(xOffset, yOffset) });
        $(".infoboxText").parent().parent().css("z-index", 10);
    }

    function hideInfobox() {
        infobox.setOptions({ visible: false });
    }

    function getTravelDistribution() {
        return dataservice.getTeamTravelDistribution(maxImagesPerPushpin).then(function (distribution) {
            travelDistribution(distribution);
        });

    }

    function getBackgroundImage(cityDistribution, index) {
        if (cityDistribution && cityDistribution.employeesPictures && cityDistribution.employeesPictures().length > index) {
            logger.log('City ' + cityDistribution.city() + " picture " + index + " added");
            return "background-image: url(data:image/jpg;base64," + cityDistribution.employeesPictures()[index].Content() + ");";
        }

        return "";
    }

    function createPushpins(distribution) {
        if (distribution && distribution.length > 0) {
            distribution.sort(function (a, b) { return b.percent() - a.percent() });
            maxCityPercentage = distribution[0].percent();
            for (var i = 0; i < distribution.length; i++) {
                addPinAsync(distribution[i]);
            }
        }
    }

    function addPinAsync(cityDistribution) {

        var widgetInstance = this;

        // construct a request to the REST geocode service using the widget's
        // optional parameters
        var geocodeRequest = "https://dev.virtualearth.net/REST/v1/Locations/?" +
                             "locality=" + cityDistribution.city() +
                             "&maxResults=1&key=" + config.bingCredentialsKey;

        // make the ajax request to the Bing Maps geocode REST service
        $.ajax({
            url: geocodeRequest,
            dataType: 'jsonp',
            async: true,
            jsonp: 'jsonp',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                logger.log('Error getting data. ' + textStatus);
            },
            success: function (result) {
                var coordinates = null;

                if (result && result.resourceSets && (result.resourceSets.length > 0) &&
                    result.resourceSets[0].resources && (result.resourceSets[0].resources.length > 0)) {

                    // create a location based on the geocoded coordinates
                    coordinates = result.resourceSets[0].resources[0].point.coordinates;

                    createPinFromCoordinates(coordinates, cityDistribution, function () { alert(cityDistribution.city()); });
                }
            }
        });
    }
});