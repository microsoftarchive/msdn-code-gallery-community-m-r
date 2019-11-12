using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using MyShuttle.Client.Core.DocumentResponse;
using System.Windows.Input;
using MyShuttle.Client.Core.Converters;

namespace MyShuttle.Client.iOS.Views.Annotations
{
    public class VehicleAnnotation : BaseAnnotation<Vehicle>
    {
        public override string AnnotationId
        {
            get
            {
                return "VehicleAnnotation";
            }
        }

        public bool IsFree { get; set; }

        public byte[] BrandImage { get; set; }

        public ICommand ShowDetailCommand { get; set; }

        public int VehicleId { get; set; }

        public VehicleAnnotation(Vehicle vehicle)
            : base(vehicle, " ", vehicle.Type.ToString())
        {
            this.title = (string)new MyShuttle.Client.Core.Converters.PriceToStringConverter().Convert(vehicle.Rate, null, true, null);
            this.title += " $/mi";

            this.IsFree = vehicle.VehicleStatus == VehicleStatus.Free;

            if (vehicle.Carrier != null)
            {
                this.BrandImage = vehicle.Carrier.Picture;
            }
        }

        protected override CLLocationCoordinate2D GetLocationCoordinate2D(Vehicle item)
        {
            return new CLLocationCoordinate2D(item.Latitude, item.Longitude);
        }
    }
}