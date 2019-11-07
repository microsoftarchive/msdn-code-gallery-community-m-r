using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.iOS.Views.Annotations
{
    public abstract class BaseAnnotation<T> : MKAnnotation
        where T : class
    {
        private CLLocationCoordinate2D coordinate;

        protected string title;
        
        private string subtitle;

        public abstract string AnnotationId { get; }

        public override CLLocationCoordinate2D Coordinate
        {
            get { return this.coordinate; }
            set { throw new NotImplementedException(); }
        }

        public override string Title { get { return this.title; } }

        public override string Subtitle { get { return this.subtitle; } }

        public BaseAnnotation(T item, string title = "", string subtitle = "")
            : base()
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.title = title;
            this.subtitle = subtitle;

            this.coordinate = this.GetLocationCoordinate2D(item);
        }

        protected abstract CLLocationCoordinate2D GetLocationCoordinate2D(T item);
    }
}