using MyEvents.Client.Organizer.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;

namespace MyEvents.Client.Organizer.Services.Twitter
{
    // Written by John Michael Hauck, Bridgman, Michigan - no rights reserved, no responsibility assumed or accepted.
    //
    // To add the ability to tweet from your application, let's say its called MyTwitApp:
    //
    // 1. Create your own personal twitter account if you don't have one, at twitter.com.  Now log out of twitter.
    // 2. Create your application's own twitter account, preferably with the name MyTwitApp (or whatever your app is called), at twitter.com
    //    a. Navigate to dev.twitter.com, select "Create an app", call it "MyTwitApp" (or whatever your app is called)
    //    b. Make sure to specify (*) read, write and access direct messages.
    //    c. Make sure to specify a callback url of http://MyTwitApp.com (or whatever your app is called), even if it is not a real web site.
    // 3. From the Details page of your dev.twitter.com's MyTwitApp (or whatever your app is called):
    //    a. Ensure the "Access level" is "Read, write, and direct messages"
    //    b. Copy the "Consumer key" and "Consumer secret" values to the constants below.
    //    c. Ensure the "Request token URL", "Authorize URL", and "Access token URL" are as specified in the constants below.
    //    d. Copy the Callback URL to the contant below, which should be http://MyTwitApp.com (or whatever your app is called)
    // 4. There is an option on the Details page called "Create my access token". Found it? Great! Ignore it.
    // 5. From your application:
    //    a. Add a call to await GainAccessToTwitter()   <- yeah, you need to make your method "async"
    //    b. Run your app so that it makes the call to GainAccessToTwitter()
    //    c. When confronted with the twitter signin screen that magically appears from within your application, log in to your personal twitter account and click the "authorize button" (you might need to scroll down)
    // 6. From a web browser:
    //    a. Log in to twitter using your personal account.
    //    b. Navigate to https://twitter.com/settings/applications to see if your application is listed.
    //    c. If it is listed, great!  If not, I dunno what to tell you.
    // 7. From your application, call await Tweet("test"); <- yeah, you need to make your method "async"
    // 8. Go back to the web browser and see if you tweeted "test".  If it is there, great!  If not, I dunno what to tell you.

    public class TwitterRt : TwitterRtBindableBase
    {
        const string _requestTokenUrl = "https://api.twitter.com/oauth/request_token";
        const string _authorizeUrl = "https://api.twitter.com/oauth/authorize";
        const string _accessTokenUrl = "https://api.twitter.com/oauth/access_token";
        const string _signatureMethod = "HMAC-SHA1";
        const string _oauthVersion = "1.0";
        const string _updateStatusUrl = "https://api.twitter.com/1/statuses/update.json";
        const string _getTimelineUrl = "https://api.twitter.com/1/statuses/home_timeline.json";

        string _consumerKey;
        string _consumerSecret;
        string _callbackUrl;
        string _eventName;

        public TwitterRt(string eventName, string consumerKey, string consumerSecret, string callbackUrl)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _callbackUrl = callbackUrl;
            _eventName = eventName;

            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                LoadSettings();
            }
        }

        Boolean _accessGranted;
        public Boolean AccessGranted
        {
            get { return _accessGranted; }
            private set { SetProperty<Boolean>(ref _accessGranted, value); }
        }

        Boolean _isTweeting;
        public Boolean IsTweeting
        {
            get { return _isTweeting; }
            private set { SetProperty<Boolean>(ref _isTweeting, value); }
        }

        string _oauthToken;
        public string OauthToken
        {
            get { return _oauthToken; }
            private set { SetProperty<string>(ref _oauthToken, value); }
        }

        string _oauthTokenSecret;
        public string OauthTokenSecret  // I would not worry about keeping this secret, as it is in the user's password protected storage.
        {
            get { return _oauthTokenSecret; }
            private set { SetProperty<string>(ref _oauthTokenSecret, value); }
        }

        string _userID;
        public string UserID
        {
            get { return _userID; }
            private set { SetProperty<string>(ref _userID, value); }
        }

        string _screenName;
        public string ScreenName
        {
            get { return _screenName; }
            private set { SetProperty<string>(ref _screenName, value); }
        }

        string _status;
        public string Status
        {
            get { return _status; }
            private set { SetProperty<string>(ref _status, value); }
        }

        // See https://dev.twitter.com/docs/auth/implementing-sign-twitter
        public async Task<Boolean> GainAccessToTwitter()
        {
            ResetSettings();
            Status = "Authorizing";

            var step1 = await Step1();
            if (step1.Status != TwitterRtPostResults.EStatus.Success || step1.Dictionary.ContainsKey("oauth_token") == false)
            {
                Status = String.IsNullOrEmpty(step1.Description) ? "Step 1 failed" : step1.Description;
                SaveSettings();
                return false;
            }

            var step2 = await Step2(step1.Dictionary["oauth_token"]);
            if (step2.Status != TwitterRtPostResults.EStatus.Success || step2.Dictionary.ContainsKey("oauth_token") == false || step2.Dictionary.ContainsKey("oauth_verifier") == false)
            {
                if (step2.Status == TwitterRtPostResults.EStatus.Canceled)
                {
                    LoadSettings();
                }
                else if (step2.Dictionary != null && step2.Dictionary.ContainsKey("denied"))
                {
                    Status = "Denied by user";
                    SaveSettings();
                }
                else
                {
                    Status = String.IsNullOrEmpty(step2.Description) ? "Step 2 failed" : step2.Description;
                    SaveSettings();
                }
                return false;
            }

            var step3 = await Step3(step2.Dictionary["oauth_token"], step2.Dictionary["oauth_verifier"]);
            if (step3.Status != TwitterRtPostResults.EStatus.Success || step3.Dictionary.ContainsKey("oauth_token") == false || step3.Dictionary.ContainsKey("oauth_token_secret") == false || step3.Dictionary.ContainsKey("user_id") == false || step3.Dictionary.ContainsKey("screen_name") == false)
            {
                Status = String.IsNullOrEmpty(step3.Description) ? "Step 3 failed" : step3.Description;
                SaveSettings();
                return false;
            }

            OauthToken = step3.Dictionary["oauth_token"];
            OauthTokenSecret = step3.Dictionary["oauth_token_secret"];
            UserID = step3.Dictionary["user_id"];
            ScreenName = step3.Dictionary["screen_name"];
            AccessGranted = true;
            Status = "Access granted";

            SaveSettings();
            return true;
        }

        // See https://dev.twitter.com/docs/auth/authorizing-request
        public async Task<Boolean> UpdateStatus(String status) // a.k.a. tweet
        {
            IsTweeting = true;
            Status = "Tweeting";
            var header = new TwitterRtDictionary();
            header.Add("oauth_consumer_key", _consumerKey);
            header.Add("oauth_nonce", GenerateNonce());
            header.Add("oauth_signature_method", _signatureMethod);
            header.Add("oauth_timestamp", GenerateSinceEpoch());
            header.Add("oauth_token", OauthToken);
            header.Add("oauth_version", _oauthVersion);
            var request = new TwitterRtDictionary();
            request.Add("status", Uri.EscapeDataString(status));
            var response = await PostData(_updateStatusUrl, header, request);
            IsTweeting = false;

            if (response.Status == TwitterRtPostResults.EStatus.Success)
            {
                Status = status;
                return true;
            }
            else
            {
                Status = response.Description;
                return false;
            }
        }

        public async Task<string> GetTimeline()
        {
            Status = "Getting timeline";
            var header = new TwitterRtDictionary();
            header.Add("oauth_consumer_key", _consumerKey);
            header.Add("oauth_nonce", GenerateNonce());
            header.Add("oauth_signature_method", _signatureMethod);
            header.Add("oauth_timestamp", GenerateSinceEpoch());
            header.Add("oauth_token", OauthToken);
            header.Add("oauth_version", _oauthVersion);
            var request = new TwitterRtDictionary();
            var response = await GetData(_getTimelineUrl, header, request);
            
            if (response.Dictionary != null)
            {
                return response.Dictionary["timeline"];         
            }

            return string.Empty;
        }

        String GenerateSinceEpoch()
        {
            return Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString(); // http://social.msdn.microsoft.com/Forums/en-US/winappswithcsharp/thread/4d9a098d-a1de-4c3c-b3ce-20e0132e1f12
        }

        String GenerateNonce()
        {
            return _rand.Next(Int32.MaxValue).ToString();
        }

        // Step 1: Obtaining a request token
        async Task<TwitterRtPostResults> Step1()
        {
            var header = new TwitterRtDictionary();
            header.Add("oauth_callback", Uri.EscapeDataString(_callbackUrl));
            header.Add("oauth_consumer_key", _consumerKey);
            header.Add("oauth_nonce", GenerateNonce());
            header.Add("oauth_signature_method", _signatureMethod);
            header.Add("oauth_timestamp", GenerateSinceEpoch());
            header.Add("oauth_version", _oauthVersion);
            return await PostData(_requestTokenUrl, header); // should contain oauth_token, oauth_token_secret, and oauth_callback_confirmed
        }

        // Step 2: Redirecting the user
        async Task<TwitterRtPostResults> Step2(String oauthToken)
        {
            try
            {
                var url = _authorizeUrl + "?oauth_token=" + oauthToken;
                var war = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(url), new Uri(_callbackUrl));
                switch (war.ResponseStatus)
                {
                    case WebAuthenticationStatus.Success:
                        return new TwitterRtPostResults
                        {
                            Status = TwitterRtPostResults.EStatus.Success,
                            Dictionary = new TwitterRtDictionary(war.ResponseData) // should contain oauth_token and oauth_verifier
                        };

                    case WebAuthenticationStatus.UserCancel:
                        return new TwitterRtPostResults
                        {
                            Status = TwitterRtPostResults.EStatus.Canceled,
                        };

                    default:
                    case WebAuthenticationStatus.ErrorHttp:
                        return new TwitterRtPostResults
                        {
                            Status = TwitterRtPostResults.EStatus.Error,
                            Description = war.ResponseErrorDetail.ToString()
                        };
                }
            }
            catch (Exception e)
            {
                return new TwitterRtPostResults
                {
                    Status = TwitterRtPostResults.EStatus.Error,
                    Description = e.Message
                };
            }
        }

        // Step 3: Converting the request token to an access token
        async Task<TwitterRtPostResults> Step3(String oauthToken, String oauthVerifier)
        {
            var header = new TwitterRtDictionary();
            header.Add("oauth_consumer_key", _consumerKey);
            header.Add("oauth_nonce", GenerateNonce());
            header.Add("oauth_signature_method", _signatureMethod);
            header.Add("oauth_timestamp", GenerateSinceEpoch());
            header.Add("oauth_token", oauthToken);
            header.Add("oauth_version", _oauthVersion);
            var request = new TwitterRtDictionary();
            request.Add("oauth_verifier", Uri.EscapeDataString(oauthVerifier));
            return await PostData(_accessTokenUrl, header, request);  // should contain oauth_token, oauth_token_secret, user_id, and screen_name
        }

        async Task<TwitterRtPostResults> GetData(String url, TwitterRtDictionary headerDictionary, TwitterRtDictionary requestDictionary = null)
        {
            // See https://dev.twitter.com/docs/auth/creating-signature
            var combinedDictionaries = new TwitterRtDictionary(headerDictionary);
            combinedDictionaries.Add(requestDictionary);
            var signatureBase = "GET&" + Uri.EscapeDataString(url) + "&" + Uri.EscapeDataString(combinedDictionaries.ToStringA());
            var keyMaterial = CryptographicBuffer.ConvertStringToBinary(_consumerSecret + "&" + OauthTokenSecret, BinaryStringEncoding.Utf8);
            var algorithm = MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");
            var key = algorithm.CreateKey(keyMaterial);
            var dataToBeSigned = CryptographicBuffer.ConvertStringToBinary(signatureBase, BinaryStringEncoding.Utf8);
            var signatureBuffer = CryptographicEngine.Sign(key, dataToBeSigned);
            var signature = CryptographicBuffer.EncodeToBase64String(signatureBuffer);
            var headers = "OAuth " + headerDictionary.ToStringQ() + ", oauth_signature=\"" + Uri.EscapeDataString(signature) + "\"";
            return await GetData(url, headers, (requestDictionary == null) ? String.Empty : requestDictionary.ToString());
        }

        async Task<TwitterRtPostResults> GetData(String url, String headers, String requestData = null)
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "GET";
                Request.Headers["Authorization"] = headers;

                if (!String.IsNullOrEmpty(requestData))
                {
                    using (StreamWriter RequestDataStream = new StreamWriter(await Request.GetRequestStreamAsync()))
                    {
                        await RequestDataStream.WriteAsync(requestData);
                    }
                }

                HttpWebResponse Response = (HttpWebResponse)await Request.GetResponseAsync();

                if (Response.StatusCode != HttpStatusCode.OK)
                {
                    return new TwitterRtPostResults
                    {
                        Status = TwitterRtPostResults.EStatus.Error,
                        Description = Response.StatusDescription
                    };
                }

                using (StreamReader ResponseDataStream = new StreamReader(Response.GetResponseStream()))
                {
                    var response = await ResponseDataStream.ReadToEndAsync();
                    var returnResult = new TwitterRtPostResults
                    {
                        Status = TwitterRtPostResults.EStatus.Success,
                        Dictionary = new TwitterRtDictionary()
                    };

                    returnResult.Dictionary.Add("timeline", response);

                    return returnResult;
                }
            }
            catch (Exception e)
            {
                return new TwitterRtPostResults
                {
                    Status = TwitterRtPostResults.EStatus.Error,
                    Description = e.Message,
                };
            }
        }

        async Task<TwitterRtPostResults> PostData(String url, TwitterRtDictionary headerDictionary, TwitterRtDictionary requestDictionary = null)
        {
            // See https://dev.twitter.com/docs/auth/creating-signature
            var combinedDictionaries = new TwitterRtDictionary(headerDictionary);
            combinedDictionaries.Add(requestDictionary);
            var signatureBase = "POST&" + Uri.EscapeDataString(url) + "&" + Uri.EscapeDataString(combinedDictionaries.ToStringA());
            var keyMaterial = CryptographicBuffer.ConvertStringToBinary(_consumerSecret + "&" + OauthTokenSecret, BinaryStringEncoding.Utf8);
            var algorithm = MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");
            var key = algorithm.CreateKey(keyMaterial);
            var dataToBeSigned = CryptographicBuffer.ConvertStringToBinary(signatureBase, BinaryStringEncoding.Utf8);
            var signatureBuffer = CryptographicEngine.Sign(key, dataToBeSigned);
            var signature = CryptographicBuffer.EncodeToBase64String(signatureBuffer);
            var headers = "OAuth " + headerDictionary.ToStringQ() + ", oauth_signature=\"" + Uri.EscapeDataString(signature) + "\"";
            return await PostData(url, headers, (requestDictionary == null) ? String.Empty : requestDictionary.ToString());
        }

        async Task<TwitterRtPostResults> PostData(String url, String headers, String requestData = null)
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                Request.Headers["Authorization"] = headers;

                if (!String.IsNullOrEmpty(requestData))
                {
                    using (StreamWriter RequestDataStream = new StreamWriter(await Request.GetRequestStreamAsync()))
                    {
                        await RequestDataStream.WriteAsync(requestData);
                    }
                }

                HttpWebResponse Response = (HttpWebResponse)await Request.GetResponseAsync();

                if (Response.StatusCode != HttpStatusCode.OK)
                {
                    return new TwitterRtPostResults
                    {
                        Status = TwitterRtPostResults.EStatus.Error,
                        Description = Response.StatusDescription
                    };
                }

                using (StreamReader ResponseDataStream = new StreamReader(Response.GetResponseStream()))
                {
                    var response = await ResponseDataStream.ReadToEndAsync();
                    return new TwitterRtPostResults
                    {
                        Status = TwitterRtPostResults.EStatus.Success,
                        Dictionary = new TwitterRtDictionary(response)
                    };
                }
            }
            catch (Exception e)
            {
                return new TwitterRtPostResults
                {
                    Status = TwitterRtPostResults.EStatus.Error,
                    Description = e.Message,
                };
            }
        }

        void LoadSettings()
        {
            AccessGranted = LoadSetting(string.Format("{0}_AccessGranted", _eventName), false);
            OauthToken = LoadSetting(string.Format("{0}_OauthToken", _eventName), String.Empty);
            OauthTokenSecret = LoadSetting(string.Format("{0}_OauthTokenSecret", _eventName), String.Empty);
            UserID = LoadSetting(string.Format("{0}_UserID", _eventName), String.Empty);
            ScreenName = LoadSetting(string.Format("{0}_ScreenName", _eventName), String.Empty);

            if (AccessGranted)
            {
                Status = "Access granted";
            }
            else
            {
                Status = "Unauthorized";
            }
        }

        void SaveSettings()
        {
            SaveSetting(string.Format("{0}_AccessGranted", _eventName), AccessGranted.ToString());
            SaveSetting(string.Format("{0}_OauthToken", _eventName), OauthToken);
            SaveSetting(string.Format("{0}_OauthTokenSecret", _eventName), OauthTokenSecret);
            SaveSetting(string.Format("{0}_UserID", _eventName), UserID);
            SaveSetting(string.Format("{0}_ScreenName", _eventName), ScreenName);
        }

        void ResetSettings()
        {
            AccessGranted = false;
            OauthToken = String.Empty;
            OauthTokenSecret = String.Empty;
            UserID = String.Empty;
            ScreenName = String.Empty;
            IsTweeting = false;
            Status = "Disconnected";
        }

        Boolean LoadSetting(String name, Boolean defaultValue)
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (settings.Values.ContainsKey("Twitter" + name))
            {
                Boolean value;
                if (Boolean.TryParse(settings.Values["Twitter" + name].ToString(), out value))
                {
                    return value;
                }
            }
            return defaultValue;
        }

        String LoadSetting(String name, String defaultValue)
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (settings.Values.ContainsKey("Twitter" + name))
            {
                return settings.Values["Twitter" + name].ToString();
            }
            return defaultValue;
        }

        void SaveSetting(String key, String value)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["Twitter" + key] = value;
        }

        Random _rand = new Random((int)DateTime.Now.Ticks);
    }

    public class TwitterRtDictionary : SortedDictionary<String, String>
    {
        public TwitterRtDictionary()
        {
        }

        public TwitterRtDictionary(string response)
        {
            var qSplit = response.Split('?');
            foreach (var kvp in qSplit[qSplit.Length - 1].Split('&'))
            {
                var kvpSplit = kvp.Split('=');
                if (kvpSplit.Length == 2)
                {
                    Add(kvpSplit[0], kvpSplit[1]);
                }
            }
        }

        public TwitterRtDictionary(TwitterRtDictionary src)
        {
            Add(src);
        }

        public void Add(TwitterRtDictionary src)
        {
            if (src != null)
            {
                foreach (var kvp in src)
                {
                    Add(kvp.Key, kvp.Value);
                }
            }
        }

        public String ToStringA()
        {
            String retVal = String.Empty;
            foreach (var kvp in this)
            {
                retVal += ((retVal.Length > 0) ? "&" : "") + kvp.Key + "=" + kvp.Value;
            }
            return retVal;
        }

        public String ToStringQ()
        {
            String retVal = String.Empty;
            foreach (var kvp in this)
            {
                retVal += ((retVal.Length > 0) ? ", " : "") + kvp.Key + "=" + "\"" + kvp.Value + "\"";
            }
            return retVal;
        }

        public override String ToString()
        {
            String retVal = String.Empty;
            foreach (var kvp in this)
            {
                retVal += ((retVal.Length > 0) ? ", " : "") + kvp.Key + "=" + kvp.Value;
            }
            return retVal;
        }
    }

    public class TwitterRtPostResults
    {
        public enum EStatus
        {
            Success = 0,
            Canceled = 1,
            Error = 2,
        }

        public EStatus Status { get; set; }
        public String Description { get; set; }
        public TwitterRtDictionary Dictionary { get; set; }
    }
}

