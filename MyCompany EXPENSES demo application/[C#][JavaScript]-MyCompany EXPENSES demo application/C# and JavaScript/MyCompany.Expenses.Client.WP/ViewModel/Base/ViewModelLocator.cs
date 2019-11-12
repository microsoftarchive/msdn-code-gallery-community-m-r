namespace MyCompany.Expenses.Client.WP.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using MyCompany.Expenses.Client.WP.Services.Location;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.Services.Photo;
    using MyCompany.Expenses.Client.WP.Services.Tile;
    using MyCompany.Expenses.Client.WP.Settings;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register<IPhotoService, PhotoService>(true);
            SimpleIoc.Default.Register<INavigationService, NavigationService>(true);
            SimpleIoc.Default.Register<ILocationService, LocationService>();
            SimpleIoc.Default.Register<ITileService, TileService>();
            SimpleIoc.Default.Register<MyCompany.Expenses.Client.WP.Services.Notification.INotificationService,
                                       MyCompany.Expenses.Client.WP.Services.Notification.NotificationService>();
            SimpleIoc.Default.Register<IMyCompanyClient>(() =>
            {
                if (AppSettings.TestMode)
                {
                    string api = string.Format("{0}noauth/", AppSettings.ApiUri.ToString());
                    string token = "TestToken";
                    return new MyCompanyClient(api, token);
                }
                if (ViewModelBase.IsInDesignModeStatic)
                    return null;

                return new MyCompanyClient(AppSettings.ApiUri.ToString(), AppSettings.SecurityToken);
            });

            SimpleIoc.Default.Register<VMMain>();
            SimpleIoc.Default.Register<VMUserHistory>();
            SimpleIoc.Default.Register<VMAddExpenseMenu>();
            SimpleIoc.Default.Register<VMTeamHistory>();
            SimpleIoc.Default.Register<VMTeamPendings>();
            SimpleIoc.Default.Register<VMAddExpense>();
            SimpleIoc.Default.Register<VMExpenseDetail>();
            SimpleIoc.Default.Register<VMExpenseRoute>();
            SimpleIoc.Default.Register<VMSettings>();
            SimpleIoc.Default.Register<VMTrackRoute>();
        }

        /// <summary>
        /// Main viewmodel.
        /// </summary>
        public VMMain Main
        {
            get { return ServiceLocator.Current.GetInstance<VMMain>(); }
        }

        /// <summary>
        /// User history viewmodel.
        /// </summary>
        public VMUserHistory UserHistory
        {
            get { return ServiceLocator.Current.GetInstance<VMUserHistory>(); }
        }

        /// <summary>
        /// Add expense menu viewmodel.
        /// </summary>
        public VMAddExpenseMenu AddExpenseMenu
        {
            get { return ServiceLocator.Current.GetInstance<VMAddExpenseMenu>(); }
        }

        /// <summary>
        /// Team history viewmodel.
        /// </summary>
        public VMTeamHistory TeamHistory
        {
            get { return ServiceLocator.Current.GetInstance<VMTeamHistory>(); }
        }

        /// <summary>
        /// Team pendings viewmodel.
        /// </summary>
        public VMTeamPendings TeamPendings
        {
            get { return ServiceLocator.Current.GetInstance<VMTeamPendings>(); }
        }

        /// <summary>
        /// Add expense viewmodel.
        /// </summary>
        public VMAddExpense AddExpense
        {
            get { return ServiceLocator.Current.GetInstance<VMAddExpense>(); }
        }

        /// <summary>
        /// Expense detail viewmodel.
        /// </summary>
        public VMExpenseDetail ExpenseDetail
        {
            get { return ServiceLocator.Current.GetInstance<VMExpenseDetail>(); }
        }

        /// <summary>
        /// Expense route viewmodel.
        /// </summary>
        public VMExpenseRoute ExpenseRoute
        {
            get { return ServiceLocator.Current.GetInstance<VMExpenseRoute>(); }
        }

        /// <summary>
        /// Settings viewmodel
        /// </summary>
        public VMSettings Settings
        {
            get { return ServiceLocator.Current.GetInstance<VMSettings>(); }
        }

        /// <summary>
        /// Traclroute viewmodel.
        /// </summary>
        public VMTrackRoute TrackRoute
        {
            get { return ServiceLocator.Current.GetInstance<VMTrackRoute>(); }
        }
    }
}