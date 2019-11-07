using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.UniversalApp.Infrastructure;
using MyShuttle.Client.UniversalApp.ViewModels;
using MyShuttle.Client.UniversalApp.Views;
using MyShuttle.Client.UniversalApp.Views.Partials;
using System;
using System.Collections.Generic;
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
            return new WindowsStore.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
        protected override void InitializeViewLookup()
        {
            var mainViewModelLookup = new Dictionary<Type, Type>
                {
                    {typeof(Core.ViewModels.MainViewModel), typeof(Main)}
                };

            var myRidesViewModelLookup = new Dictionary<Type, Type>
                {
                    {typeof(MyRidesViewModel), typeof(MyRides)}
                };
            var companyRidesViewModelLookup = new Dictionary<Type, Type>
                {
                    {typeof(CompanyRidesViewModel), typeof(CompanyRides)}
                };


            var rideViewModelLookup = new Dictionary<Type, Type>
                {
                    {typeof(RideDetailViewModel), typeof(RideDetailPage)}
                };

            var container = Mvx.Resolve<IMvxViewsContainer>();
            container.AddAll(mainViewModelLookup);
            container.AddAll(rideViewModelLookup);
            container.AddAll(myRidesViewModelLookup);
            container.AddAll(companyRidesViewModelLookup);
        }

    }
}