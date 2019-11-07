/*******************************************************************************
* Author: Alastair Aitchison
* Website: http://alastaira.wordpress.com
* Date: 15th April 2011
* 
* Description: 
* This JavaScript file provides an algorithm that can be used to add a heatmap
* overlay on a Bing Maps v7 control. The intensity and temperature palette
* of the heatmap are designed to be easily customisable.
*
* Requirements:
* The heatmap layer itself is created dynamically on the client-side using
* the HTML5 <canvas> element, and therefore requires a browser that supports
* this element. It has been tested on IE9, Firefox 3.6/4 and 
* Chrome 10 browsers. If you can confirm whether it works on other browsers or
* not, I'd love to hear from you!

* Usage:
* The HeatMapLayer constructor requires:
* - A reference to a map object
* - An array or Microsoft.Maps.Location items
* - Optional parameters to customise the appearance of the layer
*  (Radius,, Unit, Intensity, and ColourGradient), and a callback function
*
*/

var HeatMapLayer = function (map, locations, options) {

    /* Private Properties */
    var _map = map,
      _canvas,
      _temperaturemap,
      _locations = [],
      _viewchangestarthandler,
      _viewchangeendhandler;

    // Set default options
    var _options = {
        // Opacity at the centre of each heat point
        intensity: 0.5,

        // Affected radius of each heat point
        radius: 1000,

        // Whether the radius is an absolute pixel value or meters
        unit: 'meters',

        // Colour temperature gradient of the map
        colourgradient: {
            "0.00": 'rgba(255,0,255,20)',  // Magenta
            "0.25": 'rgba(0,0,255,40)',    // Blue
            "0.50": 'rgba(0,255,0,80)',    // Green
            "0.75": 'rgba(255,255,0,120)', // Yellow
            "1.00": 'rgba(255,0,0,150)'    // Red
        },

        // Callback function to be fired after heatmap layer has been redrawn 
        callback: null
    };

    /* Private Methods */
    function _init() {
        var _mapDiv = _map.getRootElement();

        if (_mapDiv.childNodes.length >= 3 && _mapDiv.childNodes[2].childNodes.length >= 2) {
            // Create the canvas element
            _canvas = document.createElement('canvas');
            _canvas.style.position = 'relative';

            var container = document.createElement('div');
            container.style.position = 'absolute';
            container.style.left = '0px';
            container.style.top = '0px';
            container.appendChild(_canvas);

            _mapDiv.childNodes[2].childNodes[1].appendChild(container);

            // Override defaults with any options passed in the constructor
            _setOptions(options);

            // Load array of location data
            _setPoints(locations);

            // Create a colour gradient from the suppied colourstops
            _temperaturemap = _createColourGradient(_options.colourgradient);

            // Wire up the event handler to redraw heatmap canvas
            _viewchangestarthandler = Microsoft.Maps.Events.addHandler(_map, 'viewchangestart', _clearHeatMap);
            _viewchangeendhandler = Microsoft.Maps.Events.addHandler(_map, 'viewchangeend', _createHeatMap);

            _createHeatMap();

            delete _init;
        } else {
            setTimeout(_init, 100);
        }
    }

    // Resets the heat map
    function _clearHeatMap() {
        var ctx = _canvas.getContext("2d");
        ctx.clearRect(0, 0, _canvas.width, _canvas.height);
    }

    // Creates a colour gradient from supplied colour stops on initialisation
    function _createColourGradient(colourstops) {
        var ctx = document.createElement('canvas').getContext('2d');
        var grd = ctx.createLinearGradient(0, 0, 256, 0);
        for (var c in colourstops) {
            grd.addColorStop(c, colourstops[c]);
        }
        ctx.fillStyle = grd;
        ctx.fillRect(0, 0, 256, 1);
        return ctx.getImageData(0, 0, 256, 1).data;
    }

    // Applies a colour gradient to the intensity map
    function _colouriseHeatMap() {
        var ctx = _canvas.getContext("2d");
        var dat = ctx.getImageData(0, 0, _canvas.width, _canvas.height);
        var pix = dat.data; // pix is a CanvasPixelArray containing height x width x 4 bytes of data (RGBA)
        for (var p = 0, len = pix.length; p < len; ) {
            var a = pix[p + 3] * 4; // get the alpha of this pixel
            if (a != 0) { // If there is any data to plot
                pix[p] = _temperaturemap[a]; // set the red value of the gradient that corresponds to this alpha
                pix[p + 1] = _temperaturemap[a + 1]; //set the green value based on alpha
                pix[p + 2] = _temperaturemap[a + 2]; //set the blue value based on alpha
            }
            p += 4; // Move on to the next pixel
        }
        ctx.putImageData(dat, 0, 0);
    }

    // Sets any options passed in
    function _setOptions(options) {
        for (attrname in options) {
            _options[attrname] = options[attrname];
        }
    }

    // Sets the heatmap points from an array of Microsoft.Maps.Locations  
    function _setPoints(locations) {
        _locations = locations;
    }

    // Main method to draw the heatmap
    function _createHeatMap() {
        // Ensure the canvas matches the current dimensions of the map
        // This also has the effect of resetting the canvas
        _canvas.height = _map.getHeight();
        _canvas.width = _map.getWidth();

        _canvas.style.top = -_canvas.height / 2 + 'px';
        _canvas.style.left = -_canvas.width / 2 + 'px';

        // Calculate the pixel radius of each heatpoint at the current map zoom
        if (_options.unit == "pixels") {
            radiusInPixel = _options.radius;
        } else {
            radiusInPixel = _options.radius / _map.getMetersPerPixel();
        }

        var ctx = _canvas.getContext("2d");

        // Convert lat/long to pixel location
        var pixlocs = _map.tryLocationToPixel(_locations, Microsoft.Maps.PixelReference.control);
        var shadow = 'rgba(0, 0, 0, ' + _options.intensity + ')';
        var mapWidth = 256 * Math.pow(2, _map.getZoom());

        // Create the Intensity Map by looping through each location
        for (var i = 0, len = pixlocs.length; i < len; i++) {
            var x = pixlocs[i].x;
            var y = pixlocs[i].y;

            if (x < 0) {
                x += mapWidth * Math.ceil(Math.abs(x / mapWidth));
            }

            // Create radial gradient centred on this point
            var grd = ctx.createRadialGradient(x, y, 0, x, y, radiusInPixel);
            grd.addColorStop(0.0, shadow);
            grd.addColorStop(1.0, 'transparent');

            // Draw the heatpoint onto the canvas
            ctx.fillStyle = grd;
            ctx.fillRect(x - radiusInPixel, y - radiusInPixel, 2 * radiusInPixel, 2 * radiusInPixel);
        }

        // Apply the specified colour gradient to the intensity map
        _colouriseHeatMap();

        // Call the callback function, if specified
        if (_options.callback) {
            _options.callback();
        }
    }

    /* Public Methods */

    this.Show = function () {
        if (_canvas) {
            _canvas.style.display = '';
        }
    };

    this.Hide = function () {
        if (_canvas) {
            _canvas.style.display = 'none';
        }
    };

    // Sets options for intensity, radius, colourgradient etc.
    this.SetOptions = function (options) {
        _setOptions(options);
    }

    // Sets an array of Microsoft.Maps.Locations from which the heatmap is created
    this.SetPoints = function (locations) {
        // Reset the existing heatmap layer
        _clearHeatMap();
        // Pass in the new set of locations
        _setPoints(locations);
        // Recreate the layer
        _createHeatMap();
    }

    // Removes the heatmap layer from the DOM
    this.Remove = function () {
        _canvas.parentNode.parentNode.removeChild(_canvas.parentNode);

        if (_viewchangestarthandler) { Microsoft.Maps.Events.removeHandler(_viewchangestarthandler); }
        if (_viewchangeendhandler) { Microsoft.Maps.Events.removeHandler(_viewchangeendhandler); }

        _locations = null;
        _temperaturemap = null;
        _canvas = null;
        _options = null;
        _viewchangestarthandler = null;
        _viewchangeendhandler = null;
    }

    // Call the initialisation routine
    _init();
};

// Call the Module Loaded method
Microsoft.Maps.moduleLoaded('HeatMapModule');