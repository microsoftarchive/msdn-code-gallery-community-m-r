//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PlayReadyUAP.Data;

using System.Threading;
using System.Threading.Tasks;

//Interacting with the XAML elements needs to be done on a separate thread
using Windows.UI.Core;
using Windows.UI;

//Simplifies adding PlayReady API calls
using Windows.Media.Protection.PlayReady;

//Simplifies call to the MediaProtectionManager
using Windows.Media.Protection;
using Windows.Media;

using PlayReadyUAP;

// The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace PlayReadyUAP
{
    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class ItemPage : Page
    {
        #region Global Variables

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private static CoreDispatcher _dispatcher;

        private Playback [] playbackContents;

        string[] itemIds = null;

        public bool bMeteringOrSecureStopButtonClicked = false;
        Button btnMeteringOrSecureStopButtonClicked = null;

        MediaElement mediaElement = null;
        private Windows.Storage.ApplicationDataContainer localSettings = null;
        private uint MediaCount = 1;
        private const uint timeBuffer = 20000; // 20 seconds

        #endregion

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public ItemPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            GlobalData.g_itemPage = this;
            _dispatcher = Window.Current.Dispatcher;
            mediaElement = myME;

            SettingPage.DiableOptOutHWDRM = true;
        }

        private void OnKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            if (e != null)
            {
                if (e.Key == Windows.System.VirtualKey.H)
                {
                    txtMessages.Visibility = Visibility.Collapsed;
                    btnShowLog.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnShowLog_Click(object sender, RoutedEventArgs e)
        {
            if (txtMessages.Visibility == Visibility.Collapsed)
            {
                btnShowLog.Content = "Hide Log";
                txtMessages.Visibility = Visibility.Visible;

                // Set opacity to 0.8 so the log is readable and video is still visible
                txtMessages.Opacity = 0.8;
            }
            else
            {
                btnShowLog.Content = "Show Log";
                txtMessages.Visibility = Visibility.Collapsed;
            }
        }

        private void btnMeteringOrSecureStop_Click(object sender, RoutedEventArgs e)
        {
            btnMeteringOrSecureStopButtonClicked = (Button)sender;
            MediaElement me = btnMeteringOrSecureStopButtonClicked.Name == "btnMeteringOrSecureStop1" ? myME : (btnMeteringOrSecureStopButtonClicked.Name == "btnMeteringOrSecureStop2" ? myME2 : myME3);

            SampleDataItem item = (SampleDataItem)btnMeteringOrSecureStopButtonClicked.DataContext;
            bMeteringOrSecureStopButtonClicked = true;
            PRUtilities prUtilities = new PRUtilities();

            if (item.MeteringCertFile != "null")
            {
                prUtilities.Test_ReportMeteringData(item.MeteringCertFile,
                                                    item.LaurlWithRights,
                                                    item.CustomData,
                                                    "null",
                                                    "null",
                                                    "1");
            }

            if( item.SecureStopCertFile != "null")
            {
                me.Stop();
                prUtilities.Test_ReportSecureStopData(item.SecureStopCertFile,
                                    item.LaurlWithRights,
                                    item.CustomData,
                                    "null",
                                    "null");
            }

        }

        private void ConfigLayout( int playerIndex, SampleDataItem item )
        {
            TextBlock tbStatus = (TextBlock)FindName("tb_status" + (playerIndex + 1).ToString());
            Button btnMeteringOrSecureStop = (Button)FindName("btnMeteringOrSecureStop" + (playerIndex + 1).ToString());
            TextBlock tbssNote = (TextBlock)FindName("tb_ssNote" + (playerIndex + 1).ToString());

            // Secure stop button is disabled until playback ends
            btnMeteringOrSecureStop.IsEnabled = false;

            tbStatus.DataContext = item;
            btnMeteringOrSecureStop.DataContext = item;

            if (item.MeteringCertFile != "null")
            {
                btnMeteringOrSecureStop.Visibility = Visibility.Visible;
                tbssNote.Visibility = Visibility.Visible;

                //Metering doesn't support in memory licenses
                item.LaurlWithRights = item.LaurlWithRights.Replace("&UseSimpleNonPersistentLicense=1", "");
                item.LaurlWithRights += "&UseMetering=1";
            }
            else
            {
                btnMeteringOrSecureStop.Visibility = Visibility.Collapsed;
                tbssNote.Visibility = Visibility.Collapsed;
            }

            if (item.SecureStopCertFile != "null")
            {
                btnMeteringOrSecureStop.Visibility = Visibility.Visible;
                btnMeteringOrSecureStop.Content = "Send Secure Stop";

                tbssNote.Visibility = Visibility.Visible;

                item.LaurlWithRights += "&SecureStop=1";
            }

            double dbHeight = Window.Current.Bounds.Height;
            double dbWidth = Window.Current.Bounds.Width;
            double dbHeightRatio = 0.7;
            double dbWidthRatio = 0.95;

            myME.Height = dbHeight * dbHeightRatio;
            myME.Width = dbWidth * dbWidthRatio;
            myME2.Height = dbHeight * dbHeightRatio;
            myME2.Width = dbWidth * dbWidthRatio;
            myME3.Height = dbHeight * dbHeightRatio;
            myME3.Width = dbWidth * dbWidthRatio;

            secondMESection.Visibility = MediaCount >= 2 ? Visibility.Visible : Visibility.Collapsed;
            thirdMESection.Visibility = MediaCount >= 3 ? Visibility.Visible : Visibility.Collapsed;

            svPlayers.Height = dbHeight * 0.95;

            txtMessages.Height = dbHeight * dbHeightRatio;
            txtMessages.Width = dbWidth * dbWidthRatio;
        }

        private void ProvactiveActionFinished(bool bResult, object resultContext)
        {
            SampleDataItem item = (SampleDataItem)resultContext;
            if (bResult)
            {
                TestLogger.LogImportantMessage("ActionFinished successfully");
            }
            else
            {
                TestLogger.LogError("ActionFinished with error!!!");
                return;
            }

            if(item.IsRootLicense == false &&
               item.IsLDL == false && 
               playbackContents != null)
            {
                playbackContents[item.PlayerIndex].Play(item.Content);
            }
        }

        private void ProactiveLA(LAAndReportResult f_licenseAcquisition,
                                 SampleDataItem f_item, 
                                 Playback f_playMedia, 
                                 String f_Laurl,
                                 bool f_useFirstPlayExpiration,
                                 bool f_rootLicense)
        {
            PRUtilities prUtilities = new PRUtilities();
            String laurl = f_Laurl;

            ServiceRequestConfigData LArequestConfigData = new ServiceRequestConfigData();

            
            if (f_rootLicense &&
                f_item.UplinkKey != "null")
            //pre-acquire a root license
            {
                Guid[] GuidRootKids = new Guid[1];
                GuidRootKids[0] = prUtilities.ActionParamConvertToGuid(f_item.UplinkKey);
                LArequestConfigData.KeyIds = GuidRootKids;

                //chained Root License
                laurl = laurl + "&UseRootLicense=1";
            }
            else if (!(f_item.Kid.ToString().Equals("null")))
            //if kids is not null, proactively acquire simple or leaf license
            {
                if (f_item.UplinkKey != "null")
                {
                    //chained Leaf License
                    Guid uplinkKeyGUID = prUtilities.ActionParamConvertToGuid(f_item.UplinkKey);
                    LogMessage("uplinkKeyGUID=" + uplinkKeyGUID);
                    string base64UplinkKid = prUtilities.GuidToBase64(uplinkKeyGUID);
                    LogMessage("base64UplinkKid=" + base64UplinkKid);
                    {
                        laurl = laurl + "&UseChainLicense=1" + "&UplinkKey=" + base64UplinkKid;
                    }
                }

                int count = 100;
                int i = 0;
                Guid[] guidKids;

                string[] keyIds = f_item.Kid.Split(',');
                int len = keyIds.Length;

                if (f_useFirstPlayExpiration &&
                    f_item.FirstPlayExpiration != "null")
                //realtime expiration
                {
                    laurl = laurl + "&RealTimeExpiration=1" + "&FirstPlayExpiration=" + f_item.FirstPlayExpiration;

                    guidKids = new Guid[count];

                    for (; i < count / 2; i++)
                    {
                        guidKids[i] = Guid.NewGuid();
                    }

                    for (int j = 0; j < len; j++)
                    {
                        guidKids[i++] = prUtilities.ActionParamConvertToGuid(keyIds[j]);
                    }

                    for (; i < count; i++)
                    {
                        guidKids[i] = Guid.NewGuid();
                    }
                }
                else
                {
                    guidKids = new Guid[len];
                    for (int j = 0; j < len; j++)
                    {
                        guidKids[j] = prUtilities.ActionParamConvertToGuid(keyIds[j]);
                    }
                }

                LArequestConfigData.KeyIds = guidKids;
            }
                              
            LArequestConfigData.EncryptionAlgorithm = prUtilities.ActionParamConvertToPlayReadyEncryptionAlgorithm(f_item.EncryptionAlgorithm);
            LArequestConfigData.Uri = prUtilities.ActionParamConvertToUri(laurl);
            LArequestConfigData.DomainServiceId = prUtilities.ActionParamConvertToGuid(f_item.DomainId);
            LArequestConfigData.ChallengeCustomData = prUtilities.ActionParamConvertToString(f_item.CustomData);

            f_licenseAcquisition.RequestConfigData = LArequestConfigData;

            if (f_Laurl.Contains("UseSimpleNonPersistentLicense=1"))
            {
                //To acquire in memory license proactively that will later be used for playback, we need to create the media session before hand.
                //Afterward, we need to tie the license session to the playback session. See configMediaProtectionManager for more detail.
                f_licenseAcquisition.Persistent = false;
                f_licenseAcquisition.configMediaProtectionManager(f_playMedia.mediaProtectionManager);
            }
            else
            {
                f_licenseAcquisition.Persistent = true;
            }

            f_licenseAcquisition.AcquireLicenseProactively();
            
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            itemIds = (String[])e.NavigationParameter;

            MediaCount = (uint)itemIds.Length;

            playbackContents = new Playback[MediaCount];
            playbackContents[0] = new Playback(myME);
            if(MediaCount > 1 )
            {
                playbackContents[1] = new Playback(myME2);
                if (MediaCount > 2)
                {
                    playbackContents[2] = new Playback(myME3);
                }
            }

            for ( uint playerIndex = 0; playerIndex < itemIds.Length; playerIndex++ )
            {
                PRUtilities prUtilities = new PRUtilities();
                SampleDataItem item = await SampleDataSource.GetItemAsync(itemIds[ playerIndex ]);
                this.DefaultViewModel["Item" + (playerIndex + 1).ToString()] = item;

                item.PlayerIndex = playerIndex;
                item.IsLDL = false;

                this.localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (item.Laurl.Contains("http://playready.directtaps.net"))
                {
                    if (this.localSettings.Values.ContainsKey("CustomRightsURL"))
                    {
                        item.LaurlWithRights = item.Laurl + this.localSettings.Values["CustomRightsURL"];
                    }
                    else
                    {
                        item.LaurlWithRights = item.Laurl + "&UseSimpleNonPersistentLicense=1";
                    }
                }
                else
                {
                    item.LaurlWithRights = item.Laurl;
                }

                ConfigLayout((int)playerIndex, item);

                if (!(item.Kid.ToString().Equals("null")))
                {
                    //proactive LA
                    LAAndReportResult licenseAcquisition = new LAAndReportResult(new ReportResultDelegate(ProvactiveActionFinished), item);

                    bool isFirstPlayExpirationUsed = false;
                    item.IsRootLicense = false;

                    if (item.UplinkKey != "null")
                    {
                        LogMessage("pre-acquire a non-expired root license");
                        item.IsRootLicense = true;
                        ProactiveLA(licenseAcquisition, item, playbackContents[playerIndex], item.LaurlWithRights, isFirstPlayExpirationUsed, item.IsRootLicense);

                    }

                    item.IsRootLicense = false;
                    if (item.FirstPlayExpiration != "null")
                    {
                        LogMessage("pre-acquire a SDL(short duration license) first");
                        isFirstPlayExpirationUsed = true;
                    }
                    else
                    {
                        LogMessage("pre-acquire a non-expiration license");
                    }

                    ProactiveLA(licenseAcquisition, item, playbackContents[playerIndex], item.LaurlWithRights, isFirstPlayExpirationUsed, item.IsRootLicense);

                    if (isFirstPlayExpirationUsed &&
                        item.FirstPlayExpiration != "null")
                    {
                        isFirstPlayExpirationUsed = false;
                        uint? uintSec = prUtilities.ActionParamConvertToUint(item.FirstPlayExpiration);
                        if (uintSec != null)
                        {
                            uint waitDuration = (uint)uintSec * 1000 - timeBuffer;
                            if (waitDuration < 0)
                                waitDuration = 0;
                            LogMessage("wait for " + waitDuration + " ms");
                            await Task.Delay((int)waitDuration);
                        }
                        else
                        {
                            LogError("invalid FirstPlayExpiration value");
                            break;
                        }

                        LogMessage("acquire a LDL(long duration license) before SDL expired");
                        item.IsLDL = true;
                        ProactiveLA(licenseAcquisition, item, playbackContents[playerIndex], item.LaurlWithRights, isFirstPlayExpirationUsed, item.IsRootLicense);
                    }
                }
                else
                {
                    //reactive LA
                    ServiceRequestConfigData requestConfigData = new ServiceRequestConfigData();

                    if (item.Laurl != "null")
                    {
                        requestConfigData.Uri = new Uri(item.LaurlWithRights);
                    }

                    if (item.DomainUrl != "null")
                    {
                        requestConfigData.DomainUri = new Uri(item.DomainUrl);
                    }

                    if (item.CustomData != "null")
                    {
                        requestConfigData.ChallengeCustomData = item.CustomData;
                    }

                    playbackContents[playerIndex].RequestConfigData = requestConfigData;
                    playbackContents[playerIndex].Play(item.Content);
                }
            }
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region LogMessage
        private void log(string msg) { TestLogger.LogMessage(msg); }

        public async void LogMeteringdata(String countValue)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                btnMeteringOrSecureStopButtonClicked.Content = countValue;
            });
        }
        public async void ClearMessage()
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtMessages.Text = "Press 'H' to hide log" + Environment.NewLine;
            });
        }

        public async void LogMessage(String msg)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtMessages.Text += Environment.NewLine + msg;
            });
        }

        public async void LogError(String msg)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                SolidColorBrush scb = new SolidColorBrush(Colors.Red);
                txtMessages.Foreground = scb;
                txtMessages.Text += Environment.NewLine + msg;
            });
        }

        #endregion

        #region MediaStatusUpdate
        private void updateStatus(string status, object sender)
        {
            MediaElement me = (MediaElement)sender;
            if (me.Name == "myME")
            {
                tb_status1.Text = status;
            }
            else if(me.Name == "myME2")
            {
                tb_status2.Text = status;
            }
            else
            {
                tb_status3.Text = status;
            }
        }

        private void MyME_MediaOpened(object sender, RoutedEventArgs e)
        {
            updateStatus("MediaOpened", sender);
            LogMessage("MediaOpened");
        }

        private void MyME_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            updateStatus("Video CurrentStateChanged: " + myME.CurrentState.ToString(), sender);
            LogMessage("Video CurrentStateChanged: " + myME.CurrentState.ToString());
        }

        private void myME_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            updateStatus("MediaFailed: " + e.ErrorMessage, sender);
            LogMessage("MediaFailed: " + e.ErrorMessage);
        }

        private async void myME_MediaEnded(object sender, RoutedEventArgs e)
        {
            Button btnSecureStop;
            SampleDataItem item;

            if (((MediaElement)sender).Name == "myME")
            {
                btnSecureStop = btnMeteringOrSecureStop1;
                item = await SampleDataSource.GetItemAsync(itemIds[0]);
            }
            else if (((MediaElement)sender).Name == "myME2")
            {
                btnSecureStop = btnMeteringOrSecureStop2;
                item = await SampleDataSource.GetItemAsync(itemIds[1]);
            }
            else
            {
                btnSecureStop = btnMeteringOrSecureStop3;
                item = await SampleDataSource.GetItemAsync(itemIds[2]);
            }

            if (item.SecureStopCertFile != "null")
            {
                btnSecureStop.IsEnabled = true;
            }

            updateStatus("MediaEnded", sender);
            LogMessage("MediaEnded");
        }

        #endregion
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211: Non-constant fields should not be visible", Justification = "this is application code")]
    sealed public class GlobalData
    {
        public static ItemPage g_itemPage = null;
        private GlobalData() { }
    }

    #region TestLogger
    sealed public class TestLogger
    {
        private TestLogger() { }
        public static void ClearMessage()
        {
            if (GlobalData.g_itemPage != null)
            {
                GlobalData.g_itemPage.ClearMessage();
            }
        }

        public static void LogMessage(String message)
        {
            if (GlobalData.g_itemPage != null)
            {
                GlobalData.g_itemPage.LogMessage(message);
            }
        }

        public static void LogImportantMessage(String message)
        {
            if (GlobalData.g_itemPage != null)
            {
                GlobalData.g_itemPage.LogMessage("*****" + message + "*****");
            }
        }

        public static void LogError(String message)
        {
            if (GlobalData.g_itemPage != null)
            {
                GlobalData.g_itemPage.LogError(message);
            }
        }

        public static void LogMeteringData(String message)
        {
            if (GlobalData.g_itemPage != null)
            {
                GlobalData.g_itemPage.LogMeteringdata(message);
            }
        }
    }

    #endregion
}
