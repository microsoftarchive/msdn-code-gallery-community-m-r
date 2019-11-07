using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS.Views;
using MyCompany.Visitors.Client.Model;

namespace MyCompany.Visitors.Client.iOS.ViewController
{
	class VisitsViewController : UICollectionViewController
	{
		readonly Source source;
		public Action<VMVisit> Selected { get; set; } 
		int columns = 1;

		protected UILabel titleLabel;
		protected BadgeView topBadge;

		public VisitsViewController()
			: base(new UICollectionViewFlowLayout())
		{
			CollectionView.RegisterClassForCell(typeof (Source.VisitCell), Source.VisitCell.Identifier);
			CollectionView.Source = source = new Source();
			CollectionView.BackgroundColor = View.BackgroundColor = UIColor.Clear;
			titleLabel = new UILabel { Text = "Next Visit".ToUpper(), Font = UIFont.FromName("HelveticaNeue", 13) };
			titleLabel.SizeToFit();
			View.AddSubview(titleLabel);

			topBadge = new BadgeView {Font = UIFont.FromName("HelveticaNeue", 8) };

			topBadge.TextColor = UIColor.White;
			topBadge.BackgroundColor = UIColor.FromRGB(196, 48, 81);

			View.AddSubview(topBadge);
		}

		public override string Title
		{
			get { return base.Title; }
			set
			{
				base.Title = value;
				titleLabel.Text = value.ToUpper();
				titleLabel.SizeToFit();
			}
		}

		public override void ViewDidLayoutSubviews()
		{
			const float topHeight = 115f;
			base.ViewDidLayoutSubviews();
			var frame = titleLabel.Frame;
			frame.X = 30;
			frame.Y = (topHeight - frame.Height) / 2;
			titleLabel.Frame = frame;

			float x = frame.Right + 10;
			frame = topBadge.Frame;
			frame.Y = (topHeight - frame.Height) / 2;
			frame.X = x;
			topBadge.Frame = frame;
		}

		public ObservableCollection<VMVisit> Visits
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
				topBadge.BadgeNumber = value.Count;
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
					topBadge.BadgeNumber = source.Visits.Count;
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
			base.ViewWillAppear(animated);
			//CollectionView.ContentInset = new UIEdgeInsets(0,10, 0, 10);
			SetLayout();
			source.Parent = this;
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
			CollectionView.SetCollectionViewLayout(new UICollectionViewFlowLayout
			{
				ItemSize = new SizeF(205, 250),
				//MinimumInteritemSpacing = 20,
				ScrollDirection = UICollectionViewScrollDirection.Horizontal
			}, true);
		}

		class Source : UICollectionViewSource
		{
			public VisitsViewController Parent;
			public ObservableCollection<VMVisit> Visits { get; set; }

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
				cell.Tapped = visit =>
				{
					if (Parent.Selected != null)
						Parent.Selected(visit);
				};
				cell.Visit = Visits[indexPath.Row];

				return cell;
			}

			public class VisitCell : UICollectionViewCell
			{
				public static readonly NSString Identifier = (NSString) "VisitCell";
				readonly TallVisitView tallVisitView;
				public Action<VMVisit> Tapped { get; set; }
				[Export("initWithFrame:")]
				public VisitCell(RectangleF frame)
					: base(frame)
				{
					tallVisitView = new TallVisitView();
					tallVisitView.TouchUpInside += (sender, args) =>
					{
						if (Tapped != null)
							Tapped(Visit);
					};
					tallVisitView.AddParallax(-20, 20);
					ContentView.AddSubview(tallVisitView);
				}


				public VMVisit Visit
				{
					get { return tallVisitView.Visit; }
					set { tallVisitView.Visit = value; }
				}

				public override void LayoutSubviews()
				{
					base.LayoutSubviews();
					tallVisitView.Frame = ContentView.Bounds;
				}
			}
		}
	}
}