using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.CoreGraphics;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS.Views;
using MyCompany.Visitors.Client.Model;


namespace MyCompany.Visitors.Client
{
	class VisitDetailsViewController : UIViewController
	{
		public Action Close { get; set; }
		public Action Save { get; set; } 
		public Action PickVisitor { get; set; }
		public Action PickEmployee{ get; set; }

		public VMVisit Visit
		{
			get { return view.Visit; }
			set
			{
				view.Visit = value;
				Title = view.Visit.VisitId == 0 ? "Create a new visit" : "Edit visit";
			}
		}

		public VisitDetailsViewController()
		{

			View = view = new VisitDetailsView(this);
			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Save,(sender,args)=>
			{
				view.UpdateVisitData();
				if (Save != null) Save();
			});
			this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (sender, args) =>
			{
				if (Close != null) Close();
			});
		}

		VisitDetailsView view;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			view.Parent = this;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			view.Parent = null;
		}

		class VisitDetailsView : UIView
		{
			const string font1 = "HelveticaNeue";
			const string font2 = "HelveticaNeue-Light";
			public VisitDetailsViewController Parent { get; set; }

			UIButton addVisitor;
			UIButton addEmployee;
			UIButton editButton;
			UILabel visitorLabel;
			UILabel employeeLabel;
			VMVisit visit;
			DialogViewController datadvc;
			DateTimeElement date;
			EntryElement comment;
			BoolElement vehicle;
			EntryElement licensePlate;
			Section section;
			public VisitDetailsView(VisitDetailsViewController parent)
			{
				Parent = parent;
				BackgroundColor = UIColor.FromRGB(239,239,244);

				addVisitor = new UIButton
				{
					Frame = new RectangleF(0, 0, 150, 150),
					TintColor = UIColor.White,
					Layer =
					{
						CornerRadius = 75,
						MasksToBounds = true,
					}
				};
				addVisitor.SetTitle("Add a visitor", UIControlState.Normal);
				addVisitor.ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;;
				addVisitor.SetImage(Theme.UserImageDefaultLight.Value,UIControlState.Normal);
				addVisitor.TouchUpInside += (sender, args) => { if (Parent.PickVisitor != null) Parent.PickVisitor(); };
				AddSubview(addVisitor);

				addEmployee = new UIButton
				{
					Frame = new RectangleF(0, 0, 150, 150),
					TintColor = UIColor.White,
					Layer =
					{
						CornerRadius = 75,
						MasksToBounds = true,
					}
				};
				addEmployee.SetTitle("Add an employee", UIControlState.Normal);
				addEmployee.ImageView.ContentMode = UIViewContentMode.ScaleAspectFill; ;
				addEmployee.SetImage(Theme.UserImageDefaultLight.Value, UIControlState.Normal);
				addEmployee.TouchUpInside += (sender, args) => { if (Parent.PickEmployee != null) Parent.PickEmployee(); };
				AddSubview(addEmployee);

				editButton = new UIButton(new RectangleF(0,0,40,40));
				editButton.SetBackgroundImage(UIImage.FromBundle("edit"),UIControlState.Normal );
				editButton.TouchUpInside += (sender, args) =>
				{
					var vc = new EditVisitorViewController
					{
						Visitor = new VMVisitor{Visitor = visit.Visitor}
					};
					this.Parent.NavigationController.PushViewController(vc,true);
				};

				visitorLabel = new UILabel { Text = "Visitor", Font = UIFont.FromName(font2, 30), TextAlignment = UITextAlignment.Center, AdjustsFontSizeToFitWidth = true,};

				visitorLabel.SizeToFit();
				AddSubview(visitorLabel);

				employeeLabel = new UILabel { Text = "Employee", Font = UIFont.FromName(font2, 30), TextAlignment = UITextAlignment.Center, AdjustsFontSizeToFitWidth = true,};
				employeeLabel.SizeToFit();
				AddSubview(employeeLabel);

				date = new DateTimeElement("Date", DateTime.Now);
				comment = new EntryElement("Reason: ", "Reason", "");
				comment.Changed += (sender, args) =>
				{
					Console.WriteLine("Comment");
				};
				vehicle = new BooleanElement("Vehicle",false);
				licensePlate = new EntryElement("Lic Plate: ", "License Plate", "");
				licensePlate.Changed += (sender, args) =>
				{
					Console.WriteLine("licensePlate");
				};
				vehicle.ValueChanged += (sender, args) =>
				{
					if (vehicle.Value)
					{
						if (!section.Elements.Contains(licensePlate))
							section.Add(licensePlate);
						datadvc.ReloadData();
					}
					else
					{
						licensePlate.FetchValue();
						section.Remove(licensePlate);
					}
				};


				datadvc = new DialogViewController(new RootElement("visit")
				{
					(section = new Section
					{
						date,
						comment,
						vehicle,
						licensePlate
					})
				});
				datadvc.TableView.SectionHeaderHeight = 0;
				datadvc.TableView.TableHeaderView = null;
				datadvc.View.BackgroundColor = UIColor.White;
				datadvc.View.Layer.CornerRadius = 5f;
				var height = Enumerable.Range(0, datadvc.TableView.Source.RowsInSection(datadvc.TableView,0)).Sum(x => datadvc.TableView.Source.GetHeightForRow(datadvc.TableView, NSIndexPath.FromRowSection(x, 0)));
				datadvc.View.Frame = new RectangleF(0,0,100,height);
				AddSubview(datadvc.View);
				this.Parent.AddChildViewController(datadvc);


			}

			public void UpdateVisitData()
			{
				visit.VisitDate = date.DateValue;
				visit.Comment = comment.Value;
				visit.HasCar = vehicle.Value;
				visit.Plate = licensePlate.Value;
			}

			public VMVisit Visit
			{
				get { return visit; }
				set
				{
					visit = value;
					setupVisitor();
					setupEmployee();
					setupVisit();
					visit.SubscribeToProperty("Visitor", setupVisitor);
					visit.SubscribeToProperty("Employee", setupEmployee);

				}
			}

			void setupVisitor()
			{
				if (visit.Visitor != null && visit.Visitor.VisitorId > 0)
				{

					this.AddSubview(editButton);
					VisitorImage = visit.VisitorPhoto.ToImage() ?? Theme.UserImageDefaultLight.Value;
					addVisitor.SetTitle("", UIControlState.Normal);
					visitorLabel.Text = visit.VisitorName;
				}
				else
				{
					addVisitor.SetTitle("Add an visitor", UIControlState.Normal);
					visitorLabel.Text = "";
					VisitorImage = Theme.UserImageDefaultLight.Value;
					editButton.RemoveFromSuperview();
				}
			}

			void setupVisit()
			{
				date.DateValue = visit.VisitDate;
				comment.Value = visit.Comment;
				vehicle.Value = visit.HasCar;
				licensePlate.Value = visit.Plate;
				if (visit.HasCar)
				{
					if(!section.Elements.Contains(licensePlate))
						section.Add(licensePlate);
				}
				else
					section.Remove(licensePlate);
			}

			void setupEmployee()
			{
				if (visit.Employee != null)
				{
					addEmployee.SetTitle("", UIControlState.Normal);
					employeeLabel.Text = visit.EmployeeName;
					EmployeeImage = visit.EmployeePhoto.ToImage() ?? Theme.UserImageDefaultLight.Value;

				}
				else
				{
					addEmployee.SetTitle("Add an employee", UIControlState.Normal);
					employeeLabel.Text = "";
					EmployeeImage = Theme.UserImageDefaultLight.Value;
				}
			}
			public UIImage VisitorImage
			{
				get { return addVisitor.CurrentBackgroundImage; }
				set
				{
					UIView.Transition(addVisitor, .3, UIViewAnimationOptions.TransitionFlipFromLeft,
						() => addVisitor.SetImage(value, UIControlState.Normal)
						, null);
				}
			}

			public UIImage EmployeeImage
			{
				get { return addEmployee.CurrentBackgroundImage; }
				set
				{
					UIView.Transition(addEmployee, .3, UIViewAnimationOptions.TransitionFlipFromLeft,
						() => addEmployee.SetImage(value, UIControlState.Normal)
						, null);
				}
			}

			public override void LayoutSubviews()
			{
				const float topPadding = 54f;
				const float tableWidth = 485f;
				base.LayoutSubviews();
				var midX = Bounds.GetMidX();

				var frame = addVisitor.Frame;
				frame.Y = topPadding;
				frame.X = midX - (frame.Width + 10);
				addVisitor.Frame = frame;

				var center = addVisitor.Center;
				center.Y = frame.Bottom;
				editButton.Center = center;

				frame.X = midX + 10;
				addEmployee.Frame = frame;

				var h = visitorLabel.Frame.Height;
				frame = addVisitor.Frame;
				frame.Y = frame.Bottom;
				frame.Height = h;
				visitorLabel.Frame = frame;

				h = employeeLabel.Frame.Height;
				frame.X = addEmployee.Frame.X;
				frame.Height = h;
				employeeLabel.Frame = frame;

				var y = frame.Bottom + 10;
				var x = midX - tableWidth/2;
				h = Bounds.Height - y - 10;

				datadvc.View.Frame = new RectangleF(x, y, tableWidth, h);
			}
		}
	}
}