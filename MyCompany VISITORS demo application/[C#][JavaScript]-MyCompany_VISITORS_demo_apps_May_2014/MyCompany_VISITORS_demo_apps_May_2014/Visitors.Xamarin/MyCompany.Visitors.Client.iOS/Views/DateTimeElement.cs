using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace MyCompany.Visitors.Client
{
	public class DateTimeElement : Element, IElementSizing
	{
		public DateTime DateValue;
		public UIDatePicker datePicker;
		public UILabel label;
		protected internal NSDateFormatter fmt = new NSDateFormatter()
		{
			DateStyle = NSDateFormatterStyle.Short
		};

		public DateTimeElement(string caption, DateTime date)
			: base(caption)
		{
			DateValue = date;
		}

		public override UITableViewCell GetCell(UITableView tv)
		{
			RectangleF frame;
			if (datePicker == null)
			{
				label = new UILabel
				{
					Text = Caption
				};
				label.SizeToFit();
				frame = label.Frame;
				frame.X = 15;
				frame.Y = 5;
				label.Frame = frame;

				datePicker = CreatePicker();
				
			}
			if(datePicker.Date != DateValue)
				datePicker.Date = DateValue;

			frame = datePicker.Frame;
			frame.Y = frame.X = 0;
			datePicker.Frame = frame;
			var cell = tv.DequeueReusableCell("datePicker") ?? new UITableViewCell(UITableViewCellStyle.Default, "datePicker") { Accessory = UITableViewCellAccessory.None };
			cell.ContentView.Add(label);
			if(cell.ContentView != datePicker.Superview)
				cell.ContentView.Add(datePicker);
			
			return cell;
		}


		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (fmt != null)
				{
					fmt.Dispose();
					fmt = null;
				}
				if (datePicker != null)
				{
					datePicker.Dispose();
					datePicker = null;
				}
			}
		}

	
		public virtual UIDatePicker CreatePicker()
		{
			var picker = new UIDatePicker(RectangleF.Empty)
			{
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
				Mode = UIDatePickerMode.DateAndTime,
				Date = DateValue
			};
			picker.ValueChanged += (sender, args) =>
			{
				var dp = sender as UIDatePicker;
				if (dp.Date != DateValue)
					DateValue = dp.Date;
			};
			return picker;
		}


		public float GetHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 216;
		}
	}
}