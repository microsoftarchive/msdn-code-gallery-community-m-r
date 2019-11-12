using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS.ViewController;
using MyCompany.Visitors.Client.iOS.Views;
using MyCompany.Visitors.Client.Model;
using MyCompany.Visitors.Client.ViewModels;

namespace MyCompany.Visitors.Client.iOS.ViewControllers
{
	class MainViewController : UIViewController
	{
		MainView mainView;

		public MainViewController()
		{
			View = mainView = new MainView(this)
			{
				ViewModel = new VMMainPage()
			};
			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) => ShowVisitDetails(new VMVisit(new Visit(){VisitDateTime = DateTime.Now}, 0)));
		}

		public void ShowVisitDetails(VMVisit visit)
		{
			if (visit == null)
				return;
			var popupViewController = new PopupViewController {View = {Frame = View.Bounds}};
			//popupViewController.ContentView = new UIView(new RectangleF(0,0,200,200)){BackgroundColor = UIColor.Red};
			popupViewController.ContentView = new VisitDetailsView
			{
				Parent = popupViewController,
				Visit = visit,
				MainPage = mainView.ViewModel,
				Close = ()=> popupViewController.Hide()
			};
			popupViewController.Show(this.NavigationController);
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			if (NavigationController == null)
				return;
			NavigationController.NavigationBar.SetBackgroundImage(new UIImage(),UIBarMetrics.Default );
			NavigationController.NavigationBar.Translucent = true;
			NavigationController.NavigationBar.ShadowImage = new UIImage();
			NavigationController.NavigationBar.TintColor = UIColor.FromRGB(196, 48, 81);
			mainView.Parent = this;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			mainView.Parent = null;
		}

		public class MainView : UIView
		{
			public MainViewController Parent { get; set; }
			const float topHeight = 240;
			readonly UIImageView backgroundImageView;
			readonly VisitView nextVisitView;
			readonly VisitFlipper otherVisitFlipper;
			readonly VisitsViewController todayVisitsViewController;
			UIImageView logoImageView;
			VMMainPage viewModel;

			public MainView(MainViewController parent)
			{
				BackgroundColor = UIColor.White;
				backgroundImageView = new UIImageView(UIImage.FromBundle("background"));
				AddSubview(backgroundImageView);

				logoImageView = new UIImageView(new RectangleF(20, 40, 150, 45))
				{
					Image = UIImage.FromBundle("logo_mainhub"),
					ContentMode = UIViewContentMode.ScaleAspectFit,
				};
				AddSubview(logoImageView);

				nextVisitView = new NextVisitView();
				nextVisitView.TouchUpInside += (sender, args) => Parent.ShowVisitDetails(nextVisitView.Visit);
				AddSubview(nextVisitView);

				otherVisitFlipper = new VisitFlipper {Selected = visit => Parent.ShowVisitDetails(visit)};
				AddSubview(otherVisitFlipper);

				todayVisitsViewController = new VisitsViewController
				{
					Title = "Today's Visits",
					Selected = visit => Parent.ShowVisitDetails(visit)
				};
				AddSubview(todayVisitsViewController.View);
				parent.AddChildViewController(todayVisitsViewController);
			}

			public VMMainPage ViewModel
			{
				get { return viewModel; }
				set
				{
					viewModel = value;
					updateBindings();
				}
			}

			void updateBindings()
			{
				viewModel.SubscribeToProperty("NextVisit", () =>
				{
					if (nextVisitView.Visit == viewModel.NextVisit)
						return;
					BeginInvokeOnMainThread(() => Transition(nextVisitView, .3, UIViewAnimationOptions.TransitionFlipFromLeft,
						() => nextVisitView.Visit = viewModel.NextVisit, null));
				});
				viewModel.SubscribeToProperty("TodayVisits", () => todayVisitsViewController.Visits = viewModel.TodayVisits);
				viewModel.SubscribeToProperty("OtherVisits", () => otherVisitFlipper.Visits = viewModel.OtherVisits);
				viewModel.PropertyChanged += ViewModelOnPropertyChanged;
				viewModel.InitializeData();
			}

			void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
			{
				Console.WriteLine(propertyChangedEventArgs.PropertyName);
			}

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();
				backgroundImageView.Frame = Bounds;

				nextVisitView.Frame = new RectangleF(0, 111, 632, topHeight);
				float width = Bounds.Width - nextVisitView.Frame.Right;
				otherVisitFlipper.Frame = new RectangleF(nextVisitView.Frame.Right, 111, width, topHeight);

				RectangleF frame = Bounds;
				frame.Y = nextVisitView.Frame.Bottom;
				frame.Height -= frame.Y;
				todayVisitsViewController.View.Frame = frame;
			}

			class VisitFlipper : UIView
			{
				readonly OtherVisitView view;
				NSTimer timer;
				ObservableCollection<VMVisit> visits;
				public Action<VMVisit> Selected { get; set; }
				public VisitFlipper()
				{
					view = new OtherVisitView();
					view.TouchUpInside += (sender, args) =>
					{
						if (Selected != null)
							Selected(view.Visit);
					};
					AddSubview(view);
				}

				public ObservableCollection<VMVisit> Visits
				{
					get { return visits; }
					set
					{
						visits = value;
						view.VisitCount = visits.Count;
						startFlipping();
					}
				}

				void startFlipping()
				{
					if (visits.Count == 1)
						view.Visit = visits.FirstOrDefault();

					else if (visits.Count == 0 || timer != null)
						return;
					view.Visit = visits.FirstOrDefault();
					timer = NSTimer.CreateRepeatingScheduledTimer(5, next);
				}

				void next()
				{
					int currentIndex = view.Visit == null ? -1 : visits.IndexOf(view.Visit);
					currentIndex ++;
					if (currentIndex >= visits.Count)
						currentIndex = 0;
					view.Visit = visits[currentIndex];
				}

				public override void LayoutSubviews()
				{
					base.LayoutSubviews();
					view.Frame = Bounds;
				}
			}
		}
	}
}