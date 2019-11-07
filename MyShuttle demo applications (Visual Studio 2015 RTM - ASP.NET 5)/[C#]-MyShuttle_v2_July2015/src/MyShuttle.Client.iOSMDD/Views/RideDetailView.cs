using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MyShuttle.Client.Core.Model.Enums;
using MyShuttle.Client.iOS.CustomControls;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MyShuttle.Client.iOS.Services;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using MonoTouch.QuickLook;

namespace MyShuttle.Client.iOS
{
	partial class RideDetailView : MvxViewController
	{
        private UIBarButtonItem downloadInvoiceButton;

        private UIBarButtonItem saveButton;
        
        private UIRatingControl ratingControl;
        
        private BindableProgress bindableProgress;

        protected RideDetailViewModel RideDetailViewModel
        {
            get
            {
                return base.ViewModel as RideDetailViewModel;
            }
        }

		public RideDetailView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.CustomizeNavigationBar();

            this.CustomizeLayout();

            this.SetUpBindings();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationItem.SetRightBarButtonItem(this.downloadInvoiceButton, true);
        }

        public override void ViewWillDisappear(bool animated)
        {
            this.NavigationItem.RightBarButtonItem = null;

            base.ViewWillDisappear(animated);
        }

        private void CustomizeNavigationBar()
        {
            this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(
                UIImage.FromFile("back.png"),
                UIBarButtonItemStyle.Plain,
                (sender, args) => this.NavigationController.PopViewControllerAnimated(true));
        }

        private void CustomizeLayout()
        {
            this.AddRatingControl();

            this.downloadInvoiceButton = new UIBarButtonItem("Invoice", UIBarButtonItemStyle.Plain, 
                this.DownloadAndShowInvoiceAsync);
            this.NavigationItem.SetRightBarButtonItem(this.downloadInvoiceButton, true);

            this.saveButton = new UIBarButtonItem(UIBarButtonSystemItem.Save, this.ExecuteSaveCommand);

            this.SegmentedControl.ValueChanged += this.ShowOrHideSubviews;

            this.bindableProgress = new BindableProgress(View);

            this.View.AddGestureRecognizer(new UITapGestureRecognizer(_ =>
                this.CommentTextBox.ResignFirstResponder()));

            this.CustomizeCommentTextBox();
        }

        private void AddRatingControl()
        {
            var separation = 20;

            this.ratingControl = new UIRatingControl(new RectangleF(
                this.RateLabel.Frame.X + this.RateLabel.Frame.Width + separation,
                this.RateLabel.Frame.Y,
                200,
                this.RateLabel.Frame.Height));
            this.ratingControl.TranslatesAutoresizingMaskIntoConstraints = false;

            this.CommentsView.Add(this.ratingControl);

            this.View.AddConstraints(new NSLayoutConstraint[]
                {
                    NSLayoutConstraint.Create(this.ratingControl, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 
                        null, NSLayoutAttribute.NoAttribute, 0, 200),
                    NSLayoutConstraint.Create(this.ratingControl, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 
                        null, NSLayoutAttribute.NoAttribute, 0, 100),
                    NSLayoutConstraint.Create(this.ratingControl, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, 
                        this.RateLabel, NSLayoutAttribute.Trailing, 1, separation),
                    NSLayoutConstraint.Create(this.ratingControl, NSLayoutAttribute.Top, NSLayoutRelation.Equal, 
                        this.RateLabel, NSLayoutAttribute.Top, 1, 0)
                });
        }

        private void CustomizeCommentTextBox()
        {
            this.CommentTextBox.Changed += (sender, args) =>
                {
                    this.CommentTextBox.SizeToFit();

                    this.CommentsView.SetNeedsLayout();
                    this.CommentsView.LayoutIfNeeded();
                };

            this.CommentTextBox.Layer.CornerRadius = 3.5f;

            this.CommentTextBox.Layer.MasksToBounds = false;
            this.CommentTextBox.Layer.ShadowOffset = new SizeF(0, 2);
            this.CommentTextBox.Layer.ShadowRadius = 0;
            this.CommentTextBox.Layer.ShadowOpacity = 0.15f;

            // Inset = padding
            var defaultInset = 11.5f;
            this.CommentTextBox.TextContainerInset = new UIEdgeInsets(defaultInset, defaultInset, defaultInset, defaultInset);
        }

        private void ShowOrHideSubviews(object sender, EventArgs e)
        {
            if (this.RideDetailViewModel.ShowResumeCommand == null ||
                    this.RideDetailViewModel.ShowRateCommand == null)
            {
                return;
            }

            if (this.SegmentedControl.SelectedSegment == 0)
            {
                this.RideDetailViewModel.ShowResumeCommand.Execute(null);

                this.NavigationItem.SetRightBarButtonItem(this.downloadInvoiceButton, true);
            }
            else
            {
                this.RideDetailViewModel.ShowRateCommand.Execute(null);

                this.NavigationItem.SetRightBarButtonItem(this.saveButton, true);
            }
        }

        private void ExecuteSaveCommand(object sender, EventArgs e)
        {
            if (this.RideDetailViewModel.SaveRideRateCommand != null)
            {
                this.RideDetailViewModel.SaveRideRateCommand.Execute(null);
            }
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<RideDetailView, RideDetailViewModel>();

            set.Bind(this.bindableProgress)
                .For(progress => progress.Visible)
                .To(vm => vm.IsLoadingRide);

            set.Bind(this.ResumeView)
                .For(view => view.Hidden)
                .To(vm => vm.RideDetailWorkflowState)
                .WithConversion("EnumEqualityToBoolean", RideDetailWorkflow.Resume);
            set.Bind(this.CommentsView)
                .For(view => view.Hidden)
                .To(vm => vm.RideDetailWorkflowState)
                .WithConversion("EnumEqualityToBoolean", RideDetailWorkflow.Rate);

            set.Bind(this.DateLabel)
                .To(vm => vm.Ride.StartDateTime)
                .WithConversion("DateTimeToFormattedDate");
            set.Bind(this.VehicleImage)
                .To(vm => vm.Ride.Vehicle.Picture)
                .WithConversion("InMemoryImage");
            set.Bind(this.MakerLabel)
                .To(vm => vm.Ride.Vehicle.Make);
            set.Bind(this.ModelLabel)
                .To(vm => vm.Ride.Vehicle.Model);
            set.Bind(this.DriverLabel)
                .To(vm => vm.Ride.Driver.Name);
            set.Bind(this.FromLabel)
                .To(vm => vm.Ride.StartAddress);
            set.Bind(this.ToLabel)
                .To(vm => vm.Ride.EndAddress);
            set.Bind(this.StartLabel)
                .To(vm => vm.Ride.StartDateTime)
                .WithConversion("DateTimeToTime");
            set.Bind(this.EndLabel)
                .To(vm => vm.Ride.EndDateTime)
                .WithConversion("DateTimeToTime");
            set.Bind(this.DistanceLabel)
                .To(vm => vm.Ride.Distance)
                .WithConversion("DistanceToString")
                .WithFallback("-");
            set.Bind(this.CostLabel)
                .To(vm => vm.Ride.Vehicle.Rate)
                .WithConversion("PriceToString", false)
                .WithFallback("-");

            set.Bind(this.CommentTextBox)
                .To(vm => vm.Ride.Comments);
            set.Bind(this.ratingControl)
                .For(control => control.Rating)
                .To(vm => vm.Ride.Rating);

            set.Apply();
        }

        private async void DownloadAndShowInvoiceAsync(object sender, EventArgs e)
        {
            this.RideDetailViewModel.IsLoadingRide = true;

            string invoiceFilePath = null;

            try
            {
                invoiceFilePath = await InvoiceService.DownloadInvoiceAsync(
                    this.RideDetailViewModel.Ride.EmployeeId, this);
            }
            catch (Exception)
            { }
            finally
            {
                this.RideDetailViewModel.IsLoadingRide = false;
            }

            if (invoiceFilePath == null)
            {
                return;
            }

            var pdfPreviewController = new QLPreviewController
            {
                DataSource = new PDFPreviewControllerDataSource(invoiceFilePath, Path.GetFileName(invoiceFilePath))
            };

            await this.NavigationController.PresentViewControllerAsync(pdfPreviewController, true);
        }
	}

    public class PDFItem : QLPreviewItem
    {
        string title;
        string uri;

        public PDFItem(string title, string uri)
        {
            this.title = title;
            this.uri = uri;
        }

        public override string ItemTitle
        {
            get { return title; }
        }

        public override NSUrl ItemUrl
        {
            get { return NSUrl.FromFilename(uri); }
        }
    }

    public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
    {
        string url = "";
        string filename = "";

        public PDFPreviewControllerDataSource(string url, string filename)
        {
            this.url = url;
            this.filename = filename;
        }

        public override QLPreviewItem GetPreviewItem(QLPreviewController controller, int index)
        {
            return new PDFItem(filename, url);
        }

        public override int PreviewItemCount(QLPreviewController controller)
        {
            return 1;
        }
    }
}
