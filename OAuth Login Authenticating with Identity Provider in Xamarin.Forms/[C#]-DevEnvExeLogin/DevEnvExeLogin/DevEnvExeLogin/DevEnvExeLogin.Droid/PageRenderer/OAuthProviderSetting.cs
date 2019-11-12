using System;
using Xamarin.Auth;

namespace DevEnvExeLogin
{
    public class OAuthProviderSetting
    {

        public enum OauthIdentityProvider
        {
            GOOGLE,
            FACEBOOK,
            TWITTER,
            MICROSOFT,
            LINKEDIN,
            GITHUB,
            FLICKER,
            YAHOO,
            DROPBOX
        }

        public OAuth1Authenticator LoginWithTwitter()
        {
            OAuth1Authenticator Twitterauth = null;
            try
            {
                Twitterauth = new OAuth1Authenticator(
                           consumerKey: "*****",    // For Twitter login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                           consumerSecret: "****",  // For Twitter login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                           requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"), // These values do not need changing
                           authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"), // These values do not need changing
                           accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"), // These values do not need changing
                           callbackUrl: new Uri("http://www.devenvexe.com")    // Set this property to the location the user will be redirected too after successfully authenticating
                       );
            }
            catch(Exception ex)
            {

            }
            return Twitterauth;
        }
        public OAuth2Authenticator LoginWithProvider(string Provider)
        {
            OAuth2Authenticator auth = null;
            switch (Provider)
            {
                case "Google":
                    {
                        auth = new OAuth2Authenticator(
                                    // For Google login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                                    "ClientId",
                                   "ClientSecret",
                                    // Below values do not need changing
                                    "https://www.googleapis.com/auth/userinfo.email",
                                    new Uri("https://accounts.google.com/o/oauth2/auth"),
                                    new Uri("http://www.devenvexe.com"),// Set this property to the location the user will be redirected too after successfully authenticating
                                    new Uri("https://accounts.google.com/o/oauth2/token")
                                    );

                        break;
                    }
                case "FaceBook":
                    {
                        auth = new OAuth2Authenticator(
                 clientId: "MyAppId",  // For Facebook login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                 scope: "",
                 authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), // These values do not need changing
                 redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")// These values do not need changing
             );
                        break;
                    }
                case "MICROSOFT":
                    {
                        auth = new OAuth2Authenticator(
                          clientId: "MY ID", // For Micrsoft login, for configure refer http://www.c-sharpcorner.com/article/register-identity-provider-for-new-oauth-application/
                          scope: "bingads.manage",
                          authorizeUrl: new Uri("https://login.live.com/oauth20_authorize.srf?client_id=myid&scope=bingads.manage&response_type=token&redirect_uri=https://login.live.com/oauth20_desktop.srf"),
                          redirectUrl: new Uri("https://adult-wicareerpathways-dev.azurewebsites.net/Account/ExternalLoginCallback")
                          );
                        break;
                    }
                case "LinkedIn":
                    {
                        auth = new OAuth2Authenticator(
             clientId: "**",// For LinkedIN login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
             clientSecret: "**",
             scope: "",
             authorizeUrl: new Uri("https://www.linkedin.com/uas/oauth2/authorization"),
             redirectUrl: new Uri("http://devenvexe.com/"),
             accessTokenUrl: new Uri("https://www.linkedin.com/uas/oauth2/accessToken")

         );

                        break;
                    }
                case "Github":
                    {
                        auth = new OAuth2Authenticator(
                                // For GITHUB login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                                "ClientId",
                               "ClientSecret",
                                // Below values do not need changing
                                "",
                                new Uri("https://github.com/login/oauth/authorize"),
                                new Uri("http://www.devenvexe.com"),// Set this property to the location the user will be redirected too after successfully authenticating
                                new Uri("https://github.com/login/oauth/access_token")
                                );

                        break;

                    }
                case "Flicker":
                    {
                        auth = new OAuth2Authenticator(
                                // For Flicker login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                                "ClientId",
                               "ClientSecret",
                                // Below values do not need changing
                                "",
                                new Uri("https://www.flickr.com/services/oauth/request_token"),
                                new Uri("http://www.devenvexe.com"),// Set this property to the location the user will be redirected too after successfully authenticating
                                new Uri("http://www.flickr.com/services/oauth/access_token")
                                );
                        break;
                    }
                case "Yahoo":
                    {
                        auth = new OAuth2Authenticator(
                                // For Yahoo login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                                "ClientId",
                               "ClientSecret",
                                // Below values do not need changing
                                "",
                                new Uri("https://api.login.yahoo.com/oauth2/request_auth"),
                                new Uri("http://www.devenvexe.com"),// Set this property to the location the user will be redirected too after successfully authenticating
                                new Uri("https://api.login.yahoo.com/oauth2/get_token")
                                );
                        break;
                    }
                case "DropBox":
                    {
                        auth = new OAuth2Authenticator(
                               // For DROPBOX login, for configure refer https://code.msdn.microsoft.com/Register-Identity-Provider-41955544
                               "ClientId",
                              "ClientSecret",
                               // Below values do not need changing
                               "",
                               new Uri("https://www.dropbox.com/1/oauth2/authorize"),
                               new Uri("http://www.devenvexe.com"),// Set this property to the location the user will be redirected too after successfully authenticating
                               new Uri("https://api.dropboxapi.com/1/oauth2/token")
                               );
                        break;

                    }

            }
            return auth;

        }


    }
}

