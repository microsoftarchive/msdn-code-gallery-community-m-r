using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.Droid.Infrastructure;
using PerpetualEngine.Storage;

namespace MyShuttle.Client.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
            SimpleStorage.SetContext(applicationContext);
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.RegisterType<IApplicationDataRepository, ApplicationDataRepository>();

            base.InitializeFirstChance();
        }
    }
}