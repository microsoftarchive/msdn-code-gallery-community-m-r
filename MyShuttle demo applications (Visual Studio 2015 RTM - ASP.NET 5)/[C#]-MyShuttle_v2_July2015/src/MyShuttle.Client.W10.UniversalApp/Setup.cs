using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.W10.UniversalApp.Infrastructure;
using MyShuttle.Client.W10.UniversalApp.ViewModels;
using MyShuttle.Client.W10.UniversalApp.Views;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace MyShuttle.Client.W10.UniversalApp
{
    public class Setup : MvxWindowsSetup
    {
        protected override void InitializeFirstChance()
        {
            Mvx.RegisterType<IApplicationDataRepository, ApplicationDataRepository>();

            base.InitializeFirstChance();
        }

        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new UAP.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeViewLookup()
        {
            var mainViewModelLookup = new Dictionary<Type, Type>
                {
                    {typeof(MainPageViewModel), typeof(MainPage)},
                    {typeof(ViewModels.VehicleDetailViewModel), typeof(VehicleDetailPage)},
                    {typeof(VehicleByDistancePageViewModel), typeof(VehicleByDistancePage)},
                    {typeof(VehicleByPricePageViewModel), typeof(VehicleByPricePage)},
                    {typeof(MapPageViewModel), typeof(MapPage)},
                    {typeof(MyRidesPageViewModel), typeof(MyRidesPage)}
                };

            var container = Mvx.Resolve<IMvxViewsContainer>();
            container.AddAll(mainViewModelLookup);
        }
    }
}