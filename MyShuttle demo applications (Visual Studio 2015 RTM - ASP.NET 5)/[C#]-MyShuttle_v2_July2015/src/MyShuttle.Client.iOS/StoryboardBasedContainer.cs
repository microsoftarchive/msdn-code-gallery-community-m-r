using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;

namespace MyShuttle.Client.iOS
{
    public class StoryboardBasedContainer : MvxTouchViewsContainer
    {
        protected override IMvxTouchView CreateViewOfType(Type viewType, MvxViewModelRequest request)
        {
            return (IMvxTouchView)UIStoryboard.FromName("Storyboard", null)
                .InstantiateViewController(viewType.Name);
        }
    }
}