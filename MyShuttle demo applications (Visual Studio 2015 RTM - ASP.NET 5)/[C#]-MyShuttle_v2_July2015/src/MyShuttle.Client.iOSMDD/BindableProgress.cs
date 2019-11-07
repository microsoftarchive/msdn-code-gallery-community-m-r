using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MBProgressHUD;

namespace MyShuttle.Client.iOS
{
    public class BindableProgress
    {
        private MTMBProgressHUD _progress;
        private UIView _parent;

        public BindableProgress(UIView parent)
        {
            _parent = parent;
        }

        public bool Visible
        {
            get { return _progress != null; }
            set
            {
                if (Visible == value)
                    return;

                if (value)
                {
                    _progress = new MTMBProgressHUD(_parent)
                    {
                        LabelText = "Waiting...",
                        RemoveFromSuperViewOnHide = true
                    };
                    _parent.AddSubview(_progress);
                    _progress.Show(true);
                }
                else
                {
                    _progress.Hide(true);
                    _progress = null;
                }
            }
        }
    }
}