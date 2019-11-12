///
/// Copyright (c) Microsoft Corporation. All rights reserved.
/// 

using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MSDN_Voice_Search.Resources;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Windows.Foundation;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.VoiceCommands;

namespace MSDN_Voice_Search
{
    public partial class MainPage : PhoneApplicationPage
    {
        // This enum and these members are used for state management for the various buttons and input
        private enum SearchState
        {
            ReadyForInput,
            ListeningForInput,
            TypingInput,
            Browsing
        };
        private SearchState CurrentSearchState;
        private bool BrowserAbandoned;

        // State maintenance of the Speech Recognizer
        private SpeechRecognizer Recognizer;
        private AsyncOperationCompletedHandler<SpeechRecognitionResult> recoCompletedAction;
        private IAsyncOperation<SpeechRecognitionResult> CurrentRecognizerOperation;

        // State maintenance of the Speech Synthesizer
        private SpeechSynthesizer Synthesizer;
        private IAsyncAction CurrentSynthesizerAction;

        /// <summary>
        /// Page constructor. Initializes recognition and synthesis components to prepare them for use.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            InitializeRecognizer();
            this.Synthesizer = new SpeechSynthesizer();
        }

        #region Page and State Management

        /// <summary>
        /// Triggered when the app is launched or reactivated, both with and without Voice Commands.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // We're only adding special behavior for "new" navigations -- Voice Commands will always trigger "new"
            // navigations, as well.
            if (e.NavigationMode == NavigationMode.New)
            {
                // First, we'll look for the special "voiceCommandName" key in the QueryString arguments. If it's
                // present, the application was activated with Voice Commands, and we'll take action based on the name
                // of the command that was triggered.
                string voiceCommandName;

                if (NavigationContext.QueryString.TryGetValue("voiceCommandName", out voiceCommandName))
                {
                    HandleVoiceCommand(voiceCommandName);
                }
                else
                {
                    // If we just freshly launched this app without a Voice Command, asynchronously try to install the 
                    // Voice Commands.
                    // If the commands are already installed, no action will be taken--there's no need to check.
                    Task.Run(() => InstallVoiceCommands());

                    // Just for fun, we'll also animate the home page buttons
                    FadeInfoButtons(true);
                }
            }
            else
            {
                // If we're returning to the page (e.g. from suspending in the middle of anything), restore our state
                // back to acceptance of input.
                SetSearchState(SearchState.ReadyForInput);
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Sets the current state associated with the search bar and button and performs the needed UI modifications
        /// associated with the new state
        /// </summary>
        /// <param name="newState"> the new state being selected </param>
        private void SetSearchState(SearchState newState)
        {
            this.CurrentSearchState = newState;

            // Hide all of the possible button elements for the microphone icon; we'll restore the one we want momentarily.
            this.SpeechActionButtonMicrophone.Opacity = 0;
            this.SpeechActionButtonGoBackingRect.Opacity = 0;
            this.SpeechActionButtonGo.Opacity = 0;
            this.SpeechActionButtonStop.Opacity = 0;
            this.SpeechActionButtonStopBorder.Opacity = 0;

            // Preemptively restore the absolute width of the search text box; we'll resize it if needed.
            this.SearchTextBox.Width = App.Current.Host.Content.ActualWidth - 66;

            switch (newState)
            {
                case SearchState.ReadyForInput:
                    FadeInfoButtons(true);
                    this.SearchTextBox.FontStyle = FontStyles.Normal;
                    this.SearchTextBox.IsEnabled = true;
                    this.SearchProgressBar.Visibility = Visibility.Collapsed;
                    this.SpeechActionButtonMicrophone.Opacity = 1;

                    if (this.BrowserAbandoned || this.SearchTextBox.Text == "")
                    {
                        RestoreDefaultSearchText();
                    }

                    break;
                case SearchState.ListeningForInput:
                    FadeInfoButtons(false);
                    this.SearchTextBox.Text = AppResources.ListeningText;
                    this.SearchTextBox.FontStyle = FontStyles.Italic;
                    this.SearchTextBox.IsEnabled = false;
                    this.SpeechActionButtonStop.Opacity = 1;
                    this.SpeechActionButtonStopBorder.Opacity = 1;
                    break;
                case SearchState.TypingInput:
                    if (this.SearchTextBox.Text == AppResources.DefaultSearchBoxText)
                    {
                        this.SearchTextBox.Text = "";
                    }
                    this.SearchTextBox.Foreground = new SolidColorBrush(Colors.Black);
                    this.SearchTextBox.Width = App.Current.Host.Content.ActualWidth + 17;
                    break;
                case SearchState.Browsing:
                    this.SearchTextBox.FontStyle = FontStyles.Normal;
                    this.SearchTextBox.IsEnabled = false;
                    this.SearchProgressBar.Visibility = Visibility.Visible;
                    this.SpeechActionButtonGoBackingRect.Opacity = 1;
                    this.SpeechActionButtonGo.Opacity = 1;
                    break;
            }
        }
        #endregion

        #region Voice Commands Installation and Processing

        /// <summary>
        /// Installs the Voice Command Definition (VCD) file associated with the application.
        /// Based on OS version, installs a separate document based on version 1.0 of the schema or version 1.1.
        /// </summary>
        private async void InstallVoiceCommands()
        {
            const string wp80vcdPath = "ms-appx:///VoiceCommandDefinition_8.0.xml";
            const string wp81vcdPath = "ms-appx:///VoiceCommandDefinition_8.1.xml";

            try
            {
                bool using81orAbove = ((Environment.OSVersion.Version.Major >= 8)
                    && (Environment.OSVersion.Version.Minor >= 10));

                Uri vcdUri = new Uri(using81orAbove ? wp81vcdPath : wp80vcdPath);

                await VoiceCommandService.InstallCommandSetsFromFileAsync(vcdUri);
            }
            catch (Exception vcdEx)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(String.Format(
                        AppResources.VoiceCommandInstallErrorTemplate, vcdEx.HResult, vcdEx.Message));
                });
            }
        }

        /// <summary>
        /// Takes specific action for a retrieved VoiceCommand name.
        /// </summary>
        /// <param name="voiceCommandName"> the command name triggered to activate the application </param>
        private void HandleVoiceCommand(string voiceCommandName)
        {
            // Voice Commands can be typed into Cortana; when this happens, "voiceCommandMode" is populated with the
            // "textInput" value. In these cases, we'll want to behave a little differently by not speaking back.
            bool typedVoiceCommand = (NavigationContext.QueryString.ContainsKey("commandMode") 
                && (NavigationContext.QueryString["commandMode"] == "text"));

            string phraseTopicContents = null;
            bool doSearch = false;

            switch (voiceCommandName)
            {
                case "MSDNNaturalLanguage":
                    if (NavigationContext.QueryString.TryGetValue("naturalLanguage", out phraseTopicContents)
                        && !String.IsNullOrEmpty(phraseTopicContents))
                    {
                        // We'll try to process the input as a natural language query; if we're successful, we won't
                        // fall back into searching, since the query will have already been handled.
                        doSearch = TryHandleNlQuery(phraseTopicContents, typedVoiceCommand);
                    }
                    break;
                case "MSDNSearch":
                    // The user explicitly asked to search, so we'll attempt to retrieve the query.
                    NavigationContext.QueryString.TryGetValue("dictatedSearchTerms", out phraseTopicContents);
                    doSearch = true;
                    break;
            }

            if (doSearch)
            {
                HandleSearchQuery(phraseTopicContents, typedVoiceCommand);
            }
        }

        /// <summary>
        /// Processes a given query string for searching. If the string is syntactically meaningful (not empty or
        /// '...', the latter of which indicates a resolution error likely stemming from network unavailability),
        /// a search will be executed, either with or without spoken feedback depending on preference. Without a
        /// syntactically meaningful query, the user will be prompted for a query if silence isn't requested.
        /// </summary>
        /// <param name="searchQuery"> the search query to attempt action on </param>
        /// <param name="actSilently"> whether or not to only take actions without audio feedback </param>
        private void HandleSearchQuery(string searchQuery, bool actSilently)
        {
            if (!String.IsNullOrEmpty(searchQuery) && (searchQuery != "..."))
            {
                FadeInfoButtons(false);
                StartSearchQueryNavigation(searchQuery, !actSilently);
            }
            else if (!actSilently)
            {
                this.CurrentSynthesizerAction = this.Synthesizer.SpeakSsmlAsync(
                    AppResources.SpokenEmptyInvocationQuery);
                this.CurrentSynthesizerAction.Completed = new AsyncActionCompletedHandler(
                    (operation, asyncStatus) =>
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            this.CurrentSynthesizerAction = null;
                            StartListening();
                        });
                    });
            }
            else
            {
                // If there's no query text available and we can't speak to prompt the user, there's nothing to do.
            }
        }

        /// <summary>
        /// Given a query string from the user, attempts rudimentary "natural language" string processing in an
        /// effort to derive a specific, curated action from the text; if a match is found, that curated action
        /// is taken. If not, the string is unhandled and should be handled elsewhere.
        /// </summary>
        /// <param name="query"> the query to attempt processing and action upon </param>
        /// <param name="actSilently"> whether or not to only take actions without audio feedback </param>
        /// <returns></returns>
        private bool TryHandleNlQuery(string query, bool actSilently)
        {
            // There are a variety of ways to say things like "I want to go to Windows Phone Dev Center"; let's load
            // some alternatives for the key components of this.
            string[] intentMarkers = AppResources.NaturalLanguageCommandIntentMarkers.Split(new char[] { ';' });
            string[] wpDevCenterNames = AppResources.WPDevCenterNames.Split(new char[] { ';' });

            int intentIndex = -1;
            int destinationIndex = -1;

            Uri destinationUri = null;
            string confirmationTts = null;

            // First we'll try to find a match for the "intent marker," e.g. "go to"
            foreach (string marker in intentMarkers)
            {
                intentIndex = query.IndexOf(marker, StringComparison.InvariantCultureIgnoreCase);
                if (intentIndex >= 0)
                {
                    break;
                }
            }

            if (intentIndex >= 0)
            {
                // Now we'll try to figure out a destination--if it comes after the intent marker in the string, we'll
                // store the destination and spoken feedback.
                foreach (string wpDevCenterName in wpDevCenterNames)
                {
                    destinationIndex = query.IndexOf(wpDevCenterName, StringComparison.InvariantCultureIgnoreCase);
                    if (destinationIndex > intentIndex)
                    {
                        destinationUri = new Uri(AppResources.WPDevCenterURL);
                        confirmationTts = AppResources.SpokenWPDevCenterSsml;
                        break;
                    }
                }
            }

            // If we found a destination to go to, we'll go there and--if allowed--provide the corresponding spoken
            // feedback.
            if (destinationUri != null)
            {
                if (!actSilently && (confirmationTts != null))
                {
                    StartSpeakingSsml(confirmationTts);
                }

                StartBrowserNavigation(destinationUri);
            }

            // If we found a destination, we handled the query. Otherwise, it hasn't been handled yet.
            return (destinationUri == null); ;
        }

        #endregion

        private void StartSpeaking(string str) { }

        #region Button and Action Handling

        /// <summary>
        /// Handler triggered when the multipurpose "action" button for speaking, canceling, or expediting is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpeechActionButtonTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            switch (this.CurrentSearchState)
            {
                case SearchState.ReadyForInput:
                    StartListening();
                    break;
                case SearchState.ListeningForInput:
                    // The input bar is currently disabled and we may have an outstanding speech operation.
                    // There's unfortunately no way to manually endpoint (tell the recognizer "Hey, we're done!" and so
                    // we'll just cancel here.
                    if (this.CurrentRecognizerOperation != null)
                    {
                        this.CurrentRecognizerOperation.Cancel();
                        PlaySound("Assets/CancelledEarcon.wav");
                    }
                    else
                    {
                        SetSearchState(SearchState.ReadyForInput);
                    }
                    break;
                case SearchState.Browsing:
                    // The input box is disabled but we have no outstanding speech operation--that means we're using the
                    // browser to navigate. In this case, we'll just preemptively pop up the still-loading page.
                    ToggleBrowserVisibility(true);
                    break;
            }
        }

        /// <summary>
        /// Fades the front-page information buttons in or out using the XAML-defined animation.
        /// </summary>
        /// <param name="fadeIn"> true if buttons being faded "in" into view; false if fading out </param>
        private void FadeInfoButtons(bool fadeIn)
        {
            if (fadeIn)
            {
                this.CortanaInfoButtonFadeInAnimation.Begin();
                this.SourceCodeInfoButtonFadeInAnimation.Begin();
                this.BlogInfoButtonFadeInAnimation.Begin();
            }
            else
            {
                this.CortanaInfoButtonFadeOutAnimation.Begin();
                this.SourceCodeInfoButtonFadeOutAnimation.Begin();
                this.BlogInfoButtonFadeOutAnimation.Begin();
            }
        }

        /// <summary>
        /// The handler invoked when one of the front-page information buttons is tapped.
        /// </summary>
        /// <param name="sender"> As an object, the button that was tapped </param>
        /// <param name="e"></param>
        private void OnInfoButtonTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var sourceButton = sender as Button;

            if (sourceButton != null && (this.CurrentSearchState == SearchState.ReadyForInput))
            {
                // Fade all the buttons; we'll abort on the one we're using.
                FadeInfoButtons(false);

                if (sourceButton == CortanaInfoButton)
                {
                    this.CortanaInfoButtonFadeOutAnimation.Stop();
                    StartBrowserNavigation(new Uri(AppResources.CortanaInfoUri));
                }
                else if (sourceButton == this.SourceCodeInfoButton)
                {
                    this.SourceCodeInfoButtonFadeOutAnimation.Stop();
                    StartBrowserNavigation(new Uri(AppResources.SourceCodeInfoUri));
                }
                else if (sourceButton == this.BlogInfoButton)
                {
                    this.BlogInfoButtonFadeOutAnimation.Stop();
                    StartBrowserNavigation(new Uri(AppResources.BlogInfoUri));
                }
            }
        }

        /// <summary>
        /// The handler invoked when the hardware back key is pressed. Actions should be taken as follows:
        ///   1. When the browser is active and a back stack is available, 'back' should navigate the browser back;
        ///   2. When the browser has exhausted its back stack, it should be dismissed and the home page should show;
        ///   3. When the home page is unobscured, we should exit the application.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            // The back button should *always* cancel any TTS in progress.
            if (this.CurrentSynthesizerAction != null)
            {
                this.CurrentSynthesizerAction.Cancel();
            }

            // We should also always hide the progress bar, since we're trying to cancel anything pending
            this.SearchProgressBar.Visibility = Visibility.Collapsed;

            switch (this.CurrentSearchState)
            {
                case SearchState.Browsing:
                    e.Cancel = true;

                    if (this.BrowserControl.CanGoBack)
                    {
                        this.BrowserControl.GoBack();
                    }
                    else
                    {
                        ToggleBrowserVisibility(false);
                    }

                    SetSearchState(SearchState.ReadyForInput);

                    break;
                case SearchState.ListeningForInput:
                    e.Cancel = true;
                    this.BrowserAbandoned = true;

                    if (this.CurrentRecognizerOperation != null)
                    {
                        this.CurrentRecognizerOperation.Cancel();
                        PlaySound("Assets/CancelledEarcon.wav");
                    }
                    else
                    {
                        SetSearchState(SearchState.ReadyForInput);
                    }

                    break;
                case SearchState.ReadyForInput:
                    if (this.BrowserControl.Height > 0 && !this.BrowserAbandoned)
                    {
                        e.Cancel = true;
                        RestoreDefaultSearchText();

                        if (this.BrowserControl.CanGoBack)
                        {
                            this.BrowserControl.GoBack();
                        }
                        else
                        {
                            ToggleBrowserVisibility(false);
                        }
                    }

                    break;
            }

            base.OnBackKeyPress(e);
        }

        #endregion

        #region Speech Recognizer Management

        /// <summary>
        /// Initializes the Speech Recognizer object and its completion handler, used for subsequent reco operations.
        /// </summary>
        private async void InitializeRecognizer()
        {
            this.Recognizer = new SpeechRecognizer();
            this.Recognizer.Grammars.AddGrammarFromPredefinedType("search", SpeechPredefinedGrammar.WebSearch);
            await this.Recognizer.PreloadGrammarsAsync();

            recoCompletedAction = new AsyncOperationCompletedHandler<SpeechRecognitionResult>((operation, asyncStatus) =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    this.CurrentRecognizerOperation = null;
                    bool recognitionSuccessful = false;

                    switch (asyncStatus)
                    {
                        case AsyncStatus.Completed:
                            SpeechRecognitionResult result = operation.GetResults();
                            if (!String.IsNullOrEmpty(result.Text))
                            {
                                recognitionSuccessful = true;
                                StartSearchQueryNavigation(result.Text, true);
                            }
                            break;
                        case AsyncStatus.Error:
                            MessageBox.Show(String.Format(
                                AppResources.SpeechRecognitionErrorTemplate,
                                operation.ErrorCode.HResult,
                                operation.ErrorCode.Message));
                            break;
                        default:
                            break;
                    }

                    if (!recognitionSuccessful)
                    {
                        // For errors and cancellations, we'll revert back to the starting state
                        RestoreDefaultSearchText();
                        SetSearchState(SearchState.ReadyForInput);
                    }
                });
            });
        }

        /// <summary>
        /// Generates a new recognition operation from the speech recognizer, hooks up its completion handler, and
        /// updates state as needed for the duration of the recognition operation. Also checks any errors for
        /// known user-actionable steps, like accepting the privacy policy before using in-app recognition.
        /// </summary>
        private void StartListening()
        {
            try
            {
                // Start listening to the user and set up the completion handler for when the result
                this.CurrentRecognizerOperation = this.Recognizer.RecognizeAsync();
                this.CurrentRecognizerOperation.Completed = recoCompletedAction;
                SetSearchState(SearchState.ListeningForInput);
                PlaySound("Assets/ListeningEarcon.wav");
            }
            catch (Exception recoException)
            {
                const int privacyPolicyHResult = unchecked((int)0x80045509);

                if (recoException.HResult == privacyPolicyHResult)
                {
                    MessageBox.Show(AppResources.SpeechPrivacyPolicyError);
                }
                else
                {
                    PlaySound("Assets/CancelledEarcon.wav");
                    Debug.WriteLine(String.Format(
                        AppResources.SpeechRecognitionErrorTemplate, recoException.HResult, recoException.Message));
                }

                recoCompletedAction.Invoke(null, AsyncStatus.Error);
            }
        }

        #endregion

        #region Speech Synthesis and Audio Output

        /// <summary>
        /// Initiates synthesis of a speech synthesis markup language (SSML) document, which allows for finer and more
        /// robust control than plain text.
        /// </summary>
        /// <param name="ssmlToSpeak"> The body fo the SSML document to be spoken </param>
        private void StartSpeakingSsml(string ssmlToSpeak)
        {
            // Begin speaking using our synthesizer, wiring the completion event to stop tracking the action when it
            // finishes.
            this.CurrentSynthesizerAction = this.Synthesizer.SpeakSsmlAsync(ssmlToSpeak);
            this.CurrentSynthesizerAction.Completed = new AsyncActionCompletedHandler(
                (operation, asyncStatus) =>
                {
                    Dispatcher.BeginInvoke(() => { this.CurrentSynthesizerAction = null; });
                });
        }

        /// <summary>
        /// Uses the XNA framework to play a given sound effect
        /// </summary>
        /// <param name="path"> the relative path of the sound being played </param>
        private void PlaySound(string path)
        {
            var stream = TitleContainer.OpenStream(path);
            var effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            effect.Play();
        }

        #endregion

        #region Browser Control Use

        /// <summary>
        /// Toggles whether the main content area of the application is showing the title and front page buttons or the
        /// browser control.
        /// </summary>
        /// <param name="browserVisible"> Whether or not the browser is visible </param>
        private void ToggleBrowserVisibility(bool browserBecomingVisible)
        {
            this.BrowserAbandoned = !browserBecomingVisible;

            if (browserBecomingVisible)
            {
                this.BrowserControlSlideInAnimation.To = this.ContentGrid.ActualHeight;
                this.BrowserControlSlideInStoryboard.Begin();
                this.TitleText.Visibility = Visibility.Collapsed;
                this.ButtonPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.BrowserControlSlideOutAnimation.From = this.BrowserControl.ActualHeight;
                this.BrowserControlSlideOutStoryboard.Begin();
                this.TitleText.Visibility = Visibility.Visible;
                this.ButtonPanel.Visibility = Visibility.Visible;
                RestoreDefaultSearchText();
                FadeInfoButtons(true);
            }
        }

        /// <summary>
        /// Handler triggered when the page-top logo is hit; returns the user to the front page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLogoContainerTapped(object sender, GestureEventArgs e)
        {
            ToggleBrowserVisibility(false);
            SetSearchState(SearchState.ReadyForInput);
        }

        /// <summary>
        /// Begins a browser search operation with or without spoken or audio output to the user
        /// </summary>
        /// <param name="query"> The query being search for </param>
        /// <param name="speakQuery"> whether or not the query should be spoken back to the user </param>
        private void StartSearchQueryNavigation(string query, bool speakQuery)
        {
            if (speakQuery)
            {
                // We'll be speaking the query string within an SSML template, so it needs to be appropriately encoded
                string htmlEncodedQuery = HttpUtility.HtmlEncode(query);
                StartSpeakingSsml(String.Format(AppResources.SpokenSearchSsmlTemplate, htmlEncodedQuery));
            }

            // UrlEncode retains extra '+' that aren't needed; we'll remove them
            string urlEncodedQuery = HttpUtility.UrlEncode(query).Replace('+', ' ');

            Uri queryUri = new Uri(String.Format(AppResources.MSDNSearchQueryBase, urlEncodedQuery));

            Dispatcher.BeginInvoke(() =>
            {
                this.SearchTextBox.Text = query;
                this.SearchTextBox.Foreground = new SolidColorBrush(Colors.Black);
                StartBrowserNavigation(queryUri);
            });
        }

        /// <summary>
        /// Initiates a browser navigation to the specified Uri and updates the UI state accordingly.
        /// </summary>
        /// <param name="addressUri"></param>
        private void StartBrowserNavigation(Uri addressUri)
        {
            SetSearchState(SearchState.Browsing);
            this.BrowserAbandoned = false;
            this.BrowserControl.Navigate(addressUri);
        }

        /// <summary>
        /// When the browser control finishes loading, we want to ensure that it is visible and then reset our state
        /// back to input preparedness.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnBrowserLoadCompleted(object sender, NavigationEventArgs e)
        {
            if (!this.BrowserAbandoned && this.BrowserControl.Height == 0)
            {
                // Only pop the browser up if the browser is wanted and not yet visible
                ToggleBrowserVisibility(true);
            }

            SetSearchState(SearchState.ReadyForInput);
        }

        /// <summary>
        /// Each time the browser initiates a new navigation action (whether by a link tap, redirect, or initial
        /// navigation), we update the state to ensure that we still have the right UI in place.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBrowserNavigating(object sender, NavigatingEventArgs e)
        {
            if (!this.BrowserAbandoned)
            {
                SetSearchState(SearchState.Browsing);
            }
        }

        #endregion

        #region Textual Input Support

        /// <summary>
        /// Restores the default search text to the search bar and colors it appropriately.
        /// </summary>
        private void RestoreDefaultSearchText()
        {
            this.SearchTextBox.Text = AppResources.DefaultSearchBoxText;
            this.SearchTextBox.Foreground = new SolidColorBrush(Colors.Gray);
        }

        /// <summary>
        /// Handles the loss of focus from the Text Input box by restoring the appropriate UI state and resetting the
        /// text as needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextInputLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.CurrentSearchState == SearchState.TypingInput)
            {
                SetSearchState(SearchState.ReadyForInput);
            }
        }

        /// <summary>
        /// Handles the gain of focus for the Text Input Box by hiding and resizing the appropriate UI elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextInputGainedFocus(object sender, RoutedEventArgs e)
        {
            SetSearchState(SearchState.TypingInput);
        }

        /// <summary>
        /// Handler for key input in the Text Box, used to capture the 'enter' event that completes the input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextInputKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (this.SearchTextBox.Text.Length > 0))
            {
                FadeInfoButtons(false);
                StartSearchQueryNavigation(this.SearchTextBox.Text, false);
            }
        }

        #endregion
    }
}