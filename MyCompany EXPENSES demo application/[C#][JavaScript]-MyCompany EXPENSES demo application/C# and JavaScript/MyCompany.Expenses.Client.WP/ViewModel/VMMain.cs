namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.Services.Notification;
    using MyCompany.Expenses.Client.WP.Settings;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System.Threading.Tasks;

    /// <summary>
    /// Main page ViewModel
    /// </summary>
    public class VMMain : VMBase
    {
        private readonly IMyCompanyClient myCompanyClient;
        private readonly INotificationService notificationService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public VMMain(IMyCompanyClient myCompanyClient, INotificationService notificationService)
        {
            this.myCompanyClient = myCompanyClient;
            this.notificationService = notificationService;
            MessengerInstance.Register<ReloadMainViewMessage>(this, ReloadNeeded);
        }

        private async void ReloadNeeded(ReloadMainViewMessage msg)
        {
            notificationService.Subscribe();
            await AppSettings.GetLoggedUserInformation(this.myCompanyClient);
        }

        /// <summary>
        /// Dispose the viewmodel and unregister events or messengers
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            MessengerInstance.Unregister<ReloadMainViewMessage>(this);
        }
    }
}