using Android.App;
using Xamarin.Forms.Platform.Android;
using DevEnvExeLogin;
using Xamarin.Forms;
using DevEnvExeLogin.Droid.PageRender;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]
namespace DevEnvExeLogin.Droid.PageRender
{
    public class LoginRenderer : PageRenderer
    {
        bool showLogin = true;
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            
            //Get and Assign ProviderName from ProviderLoginPage
            var loginPage = Element as ProviderLoginPage;
            string providername = loginPage.ProviderName;

            var activity = this.Context as Activity;
            if (showLogin && OAuthConfig.User == null)
            {
                showLogin = false;
               
                //Create OauthProviderSetting class with Oauth Implementation .Refer Step 6
                OAuthProviderSetting oauth = new OAuthProviderSetting();

                if (providername == "Twitter")
                {
                    var auth = oauth.LoginWithTwitter();
                   // After Twitter  login completed 
                    auth.Completed += (sender, eventArgs) =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {
                            OAuthConfig.User = new UserDetails();
                            // Get and Save User Details 
                            OAuthConfig.User.Token = eventArgs.Account.Properties["oauth_token"];
                            OAuthConfig.User.TokenSecret = eventArgs.Account.Properties["oauth_token_secret"];
                            OAuthConfig.User.TwitterId = eventArgs.Account.Properties["user_id"];
                            OAuthConfig.User.ScreenName = eventArgs.Account.Properties["screen_name"];

                            OAuthConfig.SuccessfulLoginAction.Invoke();
                        }
                        else
                        {
                            // The user cancelled
                        }
                    };


                    activity.StartActivity(auth.GetUI(activity));
                }
                else
                {
                    var auth = oauth.LoginWithProvider(providername);

                    // After facebook,google and all identity provider login completed 
                    auth.Completed += (sender, eventArgs) =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {
                            OAuthConfig.User = new UserDetails();
                            // Get and Save User Details 
                            OAuthConfig.User.Token = eventArgs.Account.Properties["oauth_token"];
                            OAuthConfig.User.TokenSecret = eventArgs.Account.Properties["oauth_token_secret"];
                            OAuthConfig.User.TwitterId = eventArgs.Account.Properties["user_id"];
                            OAuthConfig.User.ScreenName = eventArgs.Account.Properties["screen_name"];

                            OAuthConfig.SuccessfulLoginAction.Invoke();
                        }
                        else
                        {
                            // The user cancelled
                        }
                    };


                    activity.StartActivity(auth.GetUI(activity));
                }
            }
                }
        }
    }