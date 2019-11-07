using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.UniversalApp.Infrastructure;
using Windows.UI.Xaml.Controls;

namespace MyShuttle.Client.UniversalApp
{
    public class Setup : MvxWindowsSetup
    {
        protected override void InitializeFirstChance()
        {
            Mvx.RegisterType<IApplicationDataRepository, ApplicationDataRepository>();
            base.InitializeFirstChance();
        }

        public Setup(Frame rootFrame)
            : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}