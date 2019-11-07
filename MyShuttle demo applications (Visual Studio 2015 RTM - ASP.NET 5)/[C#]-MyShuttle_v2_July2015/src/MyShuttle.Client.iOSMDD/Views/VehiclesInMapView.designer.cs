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
	[Register ("VehiclesInMapView")]
	partial class VehiclesInMapView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.MapKit.MKMapView Map { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView SelectedVehicleTable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Map != null) {
				Map.Dispose ();
				Map = null;
			}
			if (SelectedVehicleTable != null) {
				SelectedVehicleTable.Dispose ();
				SelectedVehicleTable = null;
			}
		}
	}
}
