using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;
using System.Drawing;
using MyShuttle.Client.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MyShuttle.Client.iOS.Services;
using Cirrious.CrossCore;
using Chance.MvvmCross.Plugins.UserInteraction;

namespace MyShuttle.Client.iOS
{
	partial class SettingsView : MvxViewController
	{
        protected SettingsViewModel SettingsViewModel
        {
            get
            {
                return this.ViewModel as SettingsViewModel;
            }
        }

		public SettingsView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.CustomizeLayout();

            this.SetUpBindings();
        }

        private void CustomizeLayout()
        {
            this.UrlTextField.Layer.CornerRadius = 2.5f;

            var paddingView = new UIView(new RectangleF(0, 0, 11.5f, this.UrlTextField.Frame.Height));
            this.UrlTextField.LeftView = paddingView;
            this.UrlTextField.LeftViewMode = UITextFieldViewMode.Always;

            // Close/dismiss the keyboard when pressing <Intro>
            this.UrlTextField.ShouldReturn += (textField) =>
                {
                    textField.ResignFirstResponder();

                    return true;
                };
        }

        private void SetUpBindings()
        {
            var set = this.CreateBindingSet<SettingsView, SettingsViewModel>();

            set.Bind(this.UrlTextField)
                .To(vm => vm.Url)
                .TwoWay();

            set.Bind(this.SaveButton)
                .To(vm => vm.SaveSettingsCommand);

            set.Bind(this.CancelButton)
                .To(vm => vm.CancelSettingsCommand);

            set.Apply();
        }

        partial void SignOutOfOffice365(UIButton sender)
        {
            InvoiceService.SignOut(this);

            var userInteractionService = Mvx.Resolve<IUserInteraction>();

            if (userInteractionService != null)
            {
                userInteractionService.Alert("Sign out successful");
            }
        }
	}
}
