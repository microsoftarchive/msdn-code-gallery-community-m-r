using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace MyShuttle.Client.iOS
{
    public class SearchTableViewSource : MvxTableViewSource
    {
        public SearchTableViewSource(UITableView tableView)
            : base(tableView)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var returnCell = (SearchTableCell)tableView.DequeueReusableCell(SearchTableCell.Identifier);

            return returnCell;
        }
    }
}