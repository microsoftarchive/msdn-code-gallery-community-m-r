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
	class VisitorPickerViewController: UICollectionViewController
	{
		public Action<Visitor> Selected { get; set; }

		public Action CreateNew { get; set; } 
		readonly Source source;

		int columns = 1;


		public VisitorPickerViewController()
			: base(new UICollectionViewFlowLayout())
		{

			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add,(sender,args)=>
			{
				//if (CreateNew != null) CreateNew();
				var visitVc = new EditVisitorViewController()
				{
					Visitor = new VMVisitor()
					{
						Visitor = new Visitor(),
					},
					Selected = (visit) =>
					{
						if (Selected != null)
							Selected(visit);
					}
				};
				this.NavigationController.PushViewController(visitVc,true);
			});
			CollectionView.RegisterClassForCell(typeof(Source.VisitCell), Source.VisitCell.Identifier);
			CollectionView.Source = source = new Source();
			CollectionView.BackgroundColor = View.BackgroundColor = UIColor.FromRGB(239, 239, 244);
			CollectionView.ContentInset = new UIEdgeInsets(10,20,0,20);
			Title = "Who is visiting?";
		}


		public ObservableCollection<VMVisitor> Visits
		{
			get { return source.Visits; }
			set
			{
				try
				{
					if (source.Visits != null)
						source.Visits.CollectionChanged -= VisitsOnCollectionChanged;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				source.Visits = value;
				source.Visits.CollectionChanged += VisitsOnCollectionChanged;
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
			//CollectionView.ContentInset = new UIEdgeInsets(0,10, 0, 10);
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
				//MinimumInteritemSpacing = 20,
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			}, true);
		}

		class Source : UICollectionViewSource
		{
			public VisitorPickerViewController Parent;
			public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
			{
				Parent.Selected(Visits[indexPath.Row].Visitor);
			}

			public ObservableCollection<VMVisitor> Visits { get; set; }

			public override int GetItemsCount(UICollectionView collectionView, int section)
			{
				return Visits == null ? 0 : Visits.Count;
			}

			public override int NumberOfSections(UICollectionView collectionView)
			{
				return 1;
			}

			public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
			{
				var cell = collectionView.DequeueReusableCell(VisitCell.Identifier, indexPath) as VisitCell;

				cell.Visit = Visits[indexPath.Row];

				return cell;
			}

			public class VisitCell : UICollectionViewCell
			{
				public static readonly NSString Identifier = (NSString) "VisitCell";
				readonly SmallVisitorView visitorView;

				[Export("initWithFrame:")]
				public VisitCell(RectangleF frame)
					: base(frame)
				{
					//this.BackgroundColor = UIColor.Red;
					visitorView = new SmallVisitorView();
					visitorView.AddParallax(-20, 20);
					ContentView.AddSubview(visitorView);
				}


				public VMVisitor Visit
				{ 
					get { return visitorView.Visitor; }
					set { visitorView.Visitor = value; }
				}

				public override void LayoutSubviews()
				{
					base.LayoutSubviews();
					visitorView.Frame = ContentView.Bounds;
				}
			}
		}
	}
}