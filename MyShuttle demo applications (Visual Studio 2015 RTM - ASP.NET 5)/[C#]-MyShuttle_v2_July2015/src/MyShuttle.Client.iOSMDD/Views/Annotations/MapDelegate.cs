using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using System.Drawing;
using Cirrious.MvvmCross.Plugins.PictureChooser.Touch;

namespace MyShuttle.Client.iOS.Views.Annotations
{
    class MapDelegate : MKMapViewDelegate
    {
        private bool customizeLayout;

        // Needed to avoid SIGSEGV when cliking on detail button
        private Dictionary<object, MKAnnotationView> cachedAnnotations = new Dictionary<object, MKAnnotationView>();

        public MapDelegate(bool customizeLayout = false)
            : base()
        {
            this.customizeLayout = customizeLayout;
        }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, NSObject annotation)
        {
            if (annotation is MKUserLocation)
            {
                return null;
            }

            MKAnnotationView annotationView = null;

            if (annotation is VehicleAnnotation)
            {
                // TODO: Reuse pushpin
                // show conference annotation
                //annotationView = mapView.DequeueReusableAnnotation(annotationId);

                //if (annotationView == null)
                //    annotationView = new MKAnnotationView(annotation, annotationId);

                var vehicleAnnotation = annotation as VehicleAnnotation;

                var imageFilePath = GetImageFilePath(vehicleAnnotation);

                annotationView = CreateAnnotationView(annotation, vehicleAnnotation.AnnotationId, imageFilePath);

                this.cachedAnnotations.Add(vehicleAnnotation, annotationView);

                if (this.customizeLayout)
                {
                    CustomizeCallout(annotationView, vehicleAnnotation.BrandImage);
                }

                var gesture = new UITapGestureRecognizer(_ =>
                    {
                        if (vehicleAnnotation.ShowDetailCommand == null)
                        {
                            return;
                        }

                        vehicleAnnotation.ShowDetailCommand.Execute(vehicleAnnotation.VehicleId);
                    });

                annotationView.AddGestureRecognizer(gesture);
            }
            else if (annotation is UserAnnotation)
            {
                var userAnnotation = annotation as UserAnnotation;

                annotationView = CreateAnnotationView(annotation, userAnnotation.AnnotationId, "mypositionpushpin.png");
                annotationView.CenterOffset = new PointF(0, -annotationView.Image.Size.Height / 2);
            }

            return annotationView;
        }

        public override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            UpdateAnnotationImageIfIsVehicle(view, "car-selected.png");
        }

        public override void DidDeselectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            UpdateAnnotationImageIfIsVehicle(view);
        }

        private void UpdateAnnotationImageIfIsVehicle(MKAnnotationView view, string imageFilePath = null)
        {
            var correspondingKayValuePair = this.cachedAnnotations.FirstOrDefault(pair => pair.Value == view);
            var correspondingVehicleAnnotation = correspondingKayValuePair.Key as VehicleAnnotation;

            if (correspondingVehicleAnnotation == null)
            {
                return;
            }

            var actualImageFilePath = imageFilePath ?? this.GetImageFilePath(correspondingVehicleAnnotation);

            view.Image = UIImage.FromFile(actualImageFilePath);
        }

        private string GetImageFilePath(VehicleAnnotation vehicleAnnotation)
        {
            var imageFilename = vehicleAnnotation.IsFree ?
                "car-available" :
                "car-occupied";
            var imageFilePath = imageFilename + ".png";

            return imageFilePath;
        }

        private void CustomizeCallout(MKAnnotationView annotationView, byte[] brandImage)
        {
            //var detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);
            //detailButton.TouchUpInside += (s, e) => { };
            //annotationView.RightCalloutAccessoryView = detailButton;

            var actualBrandImage = (UIImage)new MvxInMemoryImageValueConverter().Convert(brandImage, null, null, null);

            annotationView.LeftCalloutAccessoryView = new UIImageView(actualBrandImage) 
            { 
                Frame = new RectangleF(0, 0, 35, 35),
                ContentMode = UIViewContentMode.ScaleAspectFit
            };

            //var offset = annotationView.CalloutOffset;
            //offset.Y -= 2.5f;
            //annotationView.CalloutOffset = offset;

            annotationView.CanShowCallout = true;
        }

        private MKAnnotationView CreateAnnotationView(NSObject annotation, string annotationId, string imageFilePath)
        {
            var annotationView = new MKAnnotationView(annotation, annotationId)
            {
                Image = UIImage.FromFile(imageFilePath),
                CanShowCallout = false
            };
            
            return annotationView;
        }
    }
}