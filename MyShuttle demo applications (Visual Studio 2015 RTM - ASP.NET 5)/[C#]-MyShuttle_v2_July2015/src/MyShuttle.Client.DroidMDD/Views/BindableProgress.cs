using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;

namespace MyShuttle.Client.Droid.Views
{
    public class BindableProgress
    {
        private readonly Context _context;

        public BindableProgress(Context context)
        {
            _context = context;
        }

        public bool Visible
        {
            get 
            {
                if (AndHUD.Shared.CurrentDialog == null)
                {
                    return false;
                }

                return AndHUD.Shared.CurrentDialog.IsShowing; 
            }

            set
            {
                if (value == Visible)
                    return;

                if (value)
                {
                    AndHUD.Shared.Show(this._context);
                }
                else
                {
                    AndHUD.Shared.Dismiss(this._context);
                }
            }
        }
    }
}