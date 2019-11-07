using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyShuttle.Client.Core.Model;

namespace MyShuttle.Client.iOS.Views.Annotations
{
    public class UserAnnotation : BaseAnnotation<Location>
    {
        public override string AnnotationId
        {
            get
            {
                return "UserAnnotation";
            }
        }

        public UserAnnotation(Location location)
            : base(location)
        { }

        protected override MonoTouch.CoreLocation.CLLocationCoordinate2D GetLocationCoordinate2D(Location item)
        {
            return new MonoTouch.CoreLocation.CLLocationCoordinate2D(item.Latitude, item.Longitude);
        }
    }
}