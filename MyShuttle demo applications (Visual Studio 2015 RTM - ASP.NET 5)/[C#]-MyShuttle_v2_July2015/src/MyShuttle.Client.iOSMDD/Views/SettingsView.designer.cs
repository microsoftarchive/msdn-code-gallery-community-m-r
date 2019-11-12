// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace MyShuttle.Client.iOS
{
	[Register ("SettingsView")]
	partial class SettingsView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CancelButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SaveButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField UrlTextField { get; set; }

		[Action ("SignOutOfOffice365:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void SignOutOfOffice365 (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
			if (UrlTextField != null) {
				UrlTextField.Dispose ();
				UrlTextField = null;
			}
		}
	}
}
