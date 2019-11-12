using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MyShuttle.Client.Core.DocumentResponse;
using System.Collections;
using System.Collections.Specialized;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.WeakSubscription;
using MyShuttle.Client.Core.Model;
using System.Windows.Input;

namespace MyShuttle.Client.iOS.Views.Annotations
{
    public class AnnotationManager
    {
        private MKMapView _mapView;

        private Dictionary<object, MKAnnotation> _annotations = new Dictionary<object, MKAnnotation>();
        
        private ICommand showDetailsCommand;

        // MvxSetToNullAfterBinding isn't strictly needed any more 
        // - but it's nice to have for when binding is torn down
        [MvxSetToNullAfterBinding]
        public Location CurrentLocation
        {
            get { throw new InvalidOperationException(); }

            set 
            {
                if (value == null)
                {
                    return;
                }

                var annotation = new UserAnnotation(value);

                _mapView.AddAnnotation(annotation);
            }
        }

        [MvxSetToNullAfterBinding]
        public Vehicle SingleVehicle
        {
            get { throw new NotImplementedException(); }

            set
            {
                if (value == null)
                {
                    return;
                }

                var annotation = new VehicleAnnotation(value);

                _mapView.AddAnnotation(annotation);
            }
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable<Vehicle> ItemsSource
        {
            get { throw new InvalidOperationException(); }

            set 
            { 
                if (value == null)
                {
                    return;
                }

                this.RemoveCurrentAnnotations();
                this.AddAnnotations(value);
            }
        }

        public ICommand ShowDetailsCommand 
        {
            get
            {
                return this.showDetailsCommand;
            }

            set
            {
                this.showDetailsCommand = value;
            }
        }

        public AnnotationManager(MKMapView mapView)
        {
            if (mapView == null)
            {
                throw new ArgumentNullException("mapView");
            }

            _mapView = mapView;
        }

        private void AddAnnotations(IEnumerable items)
        {
            foreach (var item in items)
            {
                this.AddAnnotation(item);
            }
        }

        private void AddAnnotation(object item)
        {
            if (item is Vehicle)
            {
                var vehicle = item as Vehicle;
                var annotation = new VehicleAnnotation(vehicle);

                if (this.ShowDetailsCommand != null)
                {
                    annotation.ShowDetailCommand = this.ShowDetailsCommand;
                    annotation.VehicleId = vehicle.VehicleId;
                }

                _annotations.Add(vehicle, annotation);

                _mapView.AddAnnotation(annotation);
            }
        }

        private void RemoveCurrentAnnotations()
        {
            foreach (var annotation in this._annotations.Values)
            {
                this._mapView.RemoveAnnotation(annotation);
            }

            this._annotations.Clear();
        }
    }
}