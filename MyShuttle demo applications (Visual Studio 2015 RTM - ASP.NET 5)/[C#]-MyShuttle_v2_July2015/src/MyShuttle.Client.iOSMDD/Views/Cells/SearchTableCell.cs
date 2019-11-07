using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace MyShuttle.Client.iOS
{
    public partial class SearchTableCell : MvxTableViewCell
    {
        internal static readonly NSString Identifier = new NSString("SearchTableCell");

        private const string BindingText = @"
VehiclePictureBytes InMemoryImage(Picture);
VehicleTitleText Make + ' ' + Model;
DistanceText DistanceToString(DistanceFromGivenPosition);
PriceText PriceToString(Rate, true) + ' $/mi';
FreeMarkerHidden VehicleStatusToBool(VehicleStatus);
RatingStar1Hidden RatingToBool(RatingAvg, 1);
RatingStar2Hidden RatingToBool(RatingAvg, 2);
RatingStar3Hidden RatingToBool(RatingAvg, 3);
RatingStar4Hidden RatingToBool(RatingAvg, 4);
RatingStar5Hidden RatingToBool(RatingAvg, 5)";

        public UIImage VehiclePictureBytes
        {
            get { return this.VehiclePicture.Image; }
            set { this.VehiclePicture.Image = value; }
        }

        public string VehicleTitleText
        {
            get { return this.VehicleTitle.Text; }
            set { this.VehicleTitle.Text = value; }
        }

        public string DistanceText
        {
            get { return this.Distance.Text; }
            set { this.Distance.Text = value; }
        }

        public string PriceText
        {
            get { return this.Price.Text; }
            set { this.Price.Text = value; }
        }

        public bool FreeMarkerHidden
        {
            get { return this.FreeMarker.Hidden; }
            set { this.FreeMarker.Hidden = value; }
        }

        public bool RatingStar1Hidden
        {
            get { return this.RatingStar1.Hidden; }
            set { this.RatingStar1.Hidden = value; }
        }

        public bool RatingStar2Hidden
        {
            get { return this.RatingStar2.Hidden; }
            set { this.RatingStar2.Hidden = value; }
        }

        public bool RatingStar3Hidden
        {
            get { return this.RatingStar3.Hidden; }
            set { this.RatingStar3.Hidden = value; }
        }

        public bool RatingStar4Hidden
        {
            get { return this.RatingStar4.Hidden; }
            set { this.RatingStar4.Hidden = value; }
        }

        public bool RatingStar5Hidden
        {
            get { return this.RatingStar5.Hidden; }
            set { this.RatingStar5.Hidden = value; }
        }

        public SearchTableCell(IntPtr handle)
            : base(BindingText, handle)
        {
            this.SelectedBackgroundView = new UIView { BackgroundColor = Colors.CellBackgroundLightGray };
        }
    }
}
