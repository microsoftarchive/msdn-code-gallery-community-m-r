using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace MApp
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            Title = "Map";

            var map = new Map
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            var mapPosition = new Position(10.020921, 76.337919);
            map.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    mapPosition,
                    Distance.FromMiles(3)
                ));

            var mapPin = new Pin
            {
                Type = PinType.Place,
                Position = mapPosition,
                Label = "Mazsoft Technologies",
                Address = "Kakkanad - Kerala"
            };
            map.Pins.Add(mapPin);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    map
                }
            };
        }
    }
}
