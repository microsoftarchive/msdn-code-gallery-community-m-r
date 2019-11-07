using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.Model;
using MyCompany.Visitors.Client.ViewModels;


namespace MyCompany.Visitors.Client
{
	class VisitDetailsView : UIView
	{
		public UIViewController Parent
		{
			get { return parent; }
			set
			{
				parent = value;
				if(navController != null)
					parent.AddChildViewController(navController);
			}
		}

		static RectangleF baseRectangleF = new RectangleF(0, 0, 600, 640);
		UINavigationController navController;
		VisitDetailsViewController mainView;
		public Action Close { get; set; }

		public VMVisit Visit
		{
			get { return visit; }
			set
			{
				visit = value;
				mainView.Visit = value;
			}
		}

		public VMMainPage MainPage
		{
			get { return mainPage; }
			set
			{
				mainPage = value; 
				mainPage.SubscribeToProperty("Visitors", () =>
				{
					if (visitorPicker != null)
						visitorPicker.Visits = mainPage.Visitors;
				});
			}
		}

		public VisitDetailsView()
			: base(baseRectangleF)
		{
			navController = new UINavigationController(mainView = new VisitDetailsViewController
			{
				Close =  ()=>Close(),
				Save = ()=> { 
					MainPage.Save(Visit);
					Close();
				},
				PickVisitor = ShowVisitorPicker,
				PickEmployee = ShowEmployeePicker,
			});
			this.Layer.BorderColor = UIColor.LightGray.CGColor;
			this.Layer.BorderWidth = .5f;
			this.AddSubview(navController.View);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			if (navController.View.Frame != Bounds)
			{
				navController.View.Frame = mainView.View.Frame = Bounds;
				navController.ViewDidAppear(true);
			}
		}

		VMVisit visit;
		VMMainPage mainPage;
		VisitorPickerViewController visitorPicker;
		EmployeePickerViewController employeePicker;
		UIViewController parent;

		void ShowVisitorPicker()
		{
			visitorPicker = new VisitorPickerViewController
			{
				Visits = MainPage.Visitors,
				Selected = async (visitor) =>
				{
					Visit.Visitor = visitor;
					navController.PopToRootViewController(true);
					visitorPicker = null;
				},
				CreateNew = ShowNewVisitor
			};
			navController.PushViewController(visitorPicker,true);
			

		}
		void ShowEmployeePicker()
		{
			employeePicker = new EmployeePickerViewController
			{
				Employees = MainPage.Employees,
				Selected = async (employee) =>
				{
					Visit.Employee = employee;
					navController.PopToRootViewController(true);
					employeePicker = null;
				},
				CreateNew = ShowNewVisitor
			};
			navController.PushViewController(employeePicker, true);


		}


		
		void ShowNewVisitor()
		{
			
		}

	}
}