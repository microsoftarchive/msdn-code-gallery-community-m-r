using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS.ViewController;
using MyCompany.Visitors.Client.iOS.Views;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client
{
	class EmployeePickerViewController: UICollectionViewController
	{
		public Action<Employee> Selected { get; set; }

		public Action CreateNew { get; set; } 
		readonly Source source;

		int columns = 1;


		public EmployeePickerViewController()
			: base(new UICollectionViewFlowLayout())
		{

			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add,(sender,args)=>
			{
				if (CreateNew != null) CreateNew();
			});
			CollectionView.RegisterClassForCell(typeof(Source.VisitCell), Source.VisitCell.Identifier);
			CollectionView.Source = source = new Source();
			CollectionView.BackgroundColor = View.BackgroundColor = UIColor.FromRGB(239, 239, 244);
			CollectionView.ContentInset = new UIEdgeInsets(10,20,0,20);
			Title = "Who is visiting?";
		}


		public ObservableCollection<VMEmployee> Employees
		{
			get { return source.Employees; }
			set
			{
				try
				{
					if (source.Employees != null)
						source.Employees.CollectionChanged -= VisitsOnCollectionChanged;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				source.Employees = value;
				source.Employees.CollectionChanged += VisitsOnCollectionChanged;
				CollectionView.ReloadData();
			}
		}

		public int Columns
		{
			get { return columns; }
			set
			{
				if (columns == value)
					return;
				columns = value;
				SetLayout();
			}
		}

		void VisitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add || args.Action == NotifyCollectionChangedAction.Remove)
				BeginInvokeOnMainThread(() =>
				{
					if (args.NewItems != null)
					{
						var indexPaths = new List<NSIndexPath>();
						for (int i = 0; i < args.NewItems.Count; i++)
							indexPaths.Add(NSIndexPath.FromRowSection(args.NewStartingIndex + i, 0));
						CollectionView.InsertItems(indexPaths.ToArray());
					}

					if (args.OldItems != null)
					{
						var indexPaths = new List<NSIndexPath>();
						for (int i = 0; i < args.OldItems.Count; i++)
							indexPaths.Add(NSIndexPath.FromRowSection(args.OldStartingIndex + i, 0));
						CollectionView.DeleteItems(indexPaths.ToArray());
					}
				});
		}

		public override void ViewWillAppear(bool animated)
		{

			source.Parent = this;
			base.ViewWillAppear(animated);
			SetLayout();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			source.Parent = null;
		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);
			SetLayout();
		}

		void SetLayout()
		{
			var width = View.Bounds.Width/3 - 45;
			CollectionView.SetCollectionViewLayout(new UICollectionViewFlowLayout
			{
				ItemSize = new SizeF(width, width * 1.2f),
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			}, true);
		}

		class Source : UICollectionViewSource
		{
			public EmployeePickerViewController Parent;
			public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
			{
				Parent.Selected(Employees[indexPath.Row].Employee);
			}

			public ObservableCollection<VMEmployee> Employees { get; set; }

			public override int GetItemsCount(UICollectionView collectionView, int section)
			{
				return Employees == null ? 0 : Employees.Count;
			}

			public override int NumberOfSections(UICollectionView collectionView)
			{
				return 1;
			}

			public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
			{
				var cell = collectionView.DequeueReusableCell(VisitCell.Identifier, indexPath) as VisitCell;

				cell.Visit = Employees[indexPath.Row];

				return cell;
			}

			public class VisitCell : UICollectionViewCell
			{
				public static readonly NSString Identifier = (NSString) "VisitCell";
				readonly SmallEmployeeView EmployeeView;

				[Export("initWithFrame:")]
				public VisitCell(RectangleF frame)
					: base(frame)
				{
					EmployeeView = new SmallEmployeeView();
					EmployeeView.AddParallax(-20, 20);
					ContentView.AddSubview(EmployeeView);
				}


				public VMEmployee Visit
				{
					get { return EmployeeView.Employee; }
					set { EmployeeView.Employee = value; }
				}

				public override void LayoutSubviews()
				{
					base.LayoutSubviews();
					EmployeeView.Frame = ContentView.Bounds;
				}
			}
		}
	}
}