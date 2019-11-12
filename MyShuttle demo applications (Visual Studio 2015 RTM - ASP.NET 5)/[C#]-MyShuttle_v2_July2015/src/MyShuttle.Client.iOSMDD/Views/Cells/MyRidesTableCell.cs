using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.iOS
{
	public partial class MyRidesTableCell : MvxTableViewCell
	{
        internal static readonly NSString Identifier = new NSString("MyRidesTableCell");

        private const string BindingText = @"DateText DateTimeToFormattedDate(StartDateTime);" +
            "AddressText StartAddress + ' to ' + EndAddress";

        public string DateText
        {
            get { return this.DateLabel.Text; }
            set { this.DateLabel.Text = value; }
        }

        public string AddressText
        {
            get { return this.AddressLabel.Text; }
            set { this.AddressLabel.Text = value; }
        }

		public MyRidesTableCell (IntPtr handle) : base (BindingText, handle)
		{
            this.SelectedBackgroundView = new UIView { BackgroundColor = Colors.CellBackgroundLightGray };
		}
	}
}
