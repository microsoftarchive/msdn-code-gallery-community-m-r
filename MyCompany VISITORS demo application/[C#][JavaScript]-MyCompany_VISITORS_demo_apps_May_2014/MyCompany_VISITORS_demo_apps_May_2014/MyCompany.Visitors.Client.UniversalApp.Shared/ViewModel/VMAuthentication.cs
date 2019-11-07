namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    using GalaSoft.MvvmLight;
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using System;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Windows.Security.Authentication.Web;
using System.Threading.Tasks;

    /// <summary>
    /// Authentication page viewmodel.
    /// </summary>
    public class VMAuthentication : ViewModelBase
    {
        private readonly INavigationService navService;
        private readonly IMyCompanyClient myCompanyClient;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMAuthentication(INavigationService navService, IMyCompanyClient myCompanyClient)
        {
            this.navService = navService;
            this.myCompanyClient = myCompanyClient;
        }

        /// <summary>
        /// Authenticate an user and navigate to mainpage if success.
        /// </summary>
        public async void AuthenticateUser()
        {                       
#if WINDOWS_APP
            var authenticationContext = new AuthenticationContext(AppSettings.AuthenticationUri);

            AuthenticationResult authResult = await authenticationContext.AcquireTokenAsync(
                                                                AppSettings.ApiUri.ToString(),
                                                                AppSettings.ClientId,
                                                                new Uri(AppSettings.ReplyUri),
                                                                PromptBehavior.Always,
                                                                string.Empty);

            if (authResult.ExpiresOn < DateTime.UtcNow)
            {
                authResult = await authenticationContext.AcquireTokenByRefreshTokenAsync(authResult.RefreshToken, AppSettings.ClientId);
            }


            if (authResult.Status == AuthenticationStatus.Succeeded)
            {
                myCompanyClient.RefreshToken(authResult.AccessToken);
                AppSettings.SecurityToken = String.Format("Bearer {0}", authResult.AccessToken);
                AppSettings.SecurityTokenExpirationDateTime = authResult.ExpiresOn;
                this.navService.NavigateToMainPage();
            }
#endif
            
        }
    }
}
