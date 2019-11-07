using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.CrossCore;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.iOS.Infrastructure;
using Cirrious.MvvmCross.Touch.Views.Presenters;

namespace MyShuttle.Client.iOS
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp ()
		{
			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxTouchViewsContainer CreateTouchViewsContainer()
        {
            return new StoryboardBasedContainer();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.RegisterType<IApplicationDataRepository, ApplicationDataRepository>();

            base.InitializeFirstChance();
        }

        protected override IMvxTouchViewPresenter CreatePresenter()
        {
            return new CustomPresenter(this.ApplicationDelegate, this.Window);
        }
	}
}