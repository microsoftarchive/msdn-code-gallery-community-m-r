var map;
var zipCodeControl;
var latitudControl;
var longitudControl;

(function ($) {

    $.fn.bingMap = function (addressElement, cityElement, zipCodeElement, latitudElement, longitudElement) {
        var self = this;
        var settings;
        if (myEvents.app) {
            settings = myEvents.app.settings
        } else {
            settings = MyEvents.Config
        }

        if ((settings.offLineMode && settings.offLineMode()) || settings.isOffline) {
            loadOfflineMap();
            return;
        }

        zipCodeControl = zipCodeElement;
        latitudControl = latitudElement;
        longitudControl = longitudElement;

        loadMap();

        function loadMap() {
            map = new Microsoft.Maps.Map(
                self[0],
                {
                    credentials: settings.bingMapsKey,
                    mapTypeId: Microsoft.Maps.MapTypeId.road
                });

            function findLocation() {

                var city = cityElement.val() || '';
                var address = addressElement.val() || '';
                var location = address + ' ' + city;

                map.getCredentials(function (credentials) {
                    var geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURI(location) + "&output=json&jsonp=geocodeCallback&key=" + credentials;
                    callRestService(geocodeRequest);
                });
            }

            function callRestService(request) {
                var script = document.createElement("script");
                script.setAttribute("type", "text/javascript");
                script.setAttribute("src", request);
                document.body.appendChild(script);
            }

            cityElement.blur(findLocation);
            addressElement.blur(findLocation);

            if (cityElement.val() && addressElement.val())
                findLocation();
        }

        function loadOfflineMap() {
            var $self = $(self);
            var imgSrc = settings.imagesPath + "offline-map-big.png";
            var mapImg = $('<img src="' + imgSrc + '" class="offline-map" />')
            $self.append(mapImg);
        }

        return this;
    };

    $.fn.showLocationInBingMap = function (latitude, longitude) {
        var self = this;

        var settings;
        if (myEvents.app) {
            settings = myEvents.app.settings
        } else {
            settings = MyEvents.Config
        }

        if ((settings.offLineMode && settings.offLineMode()) || settings.isOffline) {
            loadOfflineMap();
            return;
        }

        var location = new Microsoft.Maps.Location(latitude, longitude);

        map = new Microsoft.Maps.Map(
            self[0],
            {
                credentials: settings.bingMapsKey,
                mapTypeId: Microsoft.Maps.MapTypeId.road,
                center: location,
                zoom: 15 // http://msdn.microsoft.com/en-us/library/aa940990.aspx
            });

        // Add a pushpin at the found location
        var pushpin = new Microsoft.Maps.Pushpin(location);
        map.entities.clear();
        map.entities.push(pushpin);

        function loadOfflineMap() {
            var $self = $(self);
            var imgSrc = settings.imagesPath + "offline-map.png";
            var mapImg = $('<img src="' + imgSrc + '" class="offline-map" />')
            $self.append(mapImg);
        }

        return this;
    };

})(jQuery);

function geocodeCallback(result) {
    if (result &&
        result.resourceSets &&
        result.resourceSets.length > 0 &&
        result.resourceSets[0].resources &&
        result.resourceSets[0].resources.length > 0) {
        // Set the map view using the returned bounding box
        var bbox = result.resourceSets[0].resources[0].bbox;
        var viewBoundaries = Microsoft.Maps.LocationRect.fromLocations(new Microsoft.Maps.Location(bbox[0], bbox[1]), new Microsoft.Maps.Location(bbox[2], bbox[3]));
        map.setView({ bounds: viewBoundaries });

        // Add a pushpin at the found location
        var location = new Microsoft.Maps.Location(result.resourceSets[0].resources[0].point.coordinates[0], result.resourceSets[0].resources[0].point.coordinates[1]);
        var pushpin = new Microsoft.Maps.Pushpin(location);
        map.entities.clear();
        map.entities.push(pushpin);

        latitudControl.val(location.latitude);
        longitudControl.val(location.longitude);

        var address = result.resourceSets[0].resources[0].address;
        zipCodeControl.val(address && address.postalCode);
    }
}