using Reversi.Common;
using Reversi.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Reversi.ViewModels
{
    /// <summary>
    /// Encapsulates the game settings.
    /// </summary>
    public class SettingsViewModel : BindableBase, ISettingsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the SettingsViewModel class.
        /// </summary>
        /// <remarks>
        /// This constructor is required to instantiate this class from XAML.
        /// </remarks>
        public SettingsViewModel() { }

        /// <summary>
        /// Gets or sets a reference to the current game.
        /// </summary>
        public IGameViewModel GameViewModel 
        {
            get { return _gameViewModel; }
            set { _gameViewModel = value; OnPropertyChanged(); }
        }
        private IGameViewModel _gameViewModel;

        /// <summary>
        /// Gets or sets a value that indicates whether the legal moves are indicated on the game board.
        /// </summary>
        public bool IsShowingValidMoves
        {
            get { return GetSetting(true); }
            set { SaveSetting(value); UpdateBoard(); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the clock is displayed on the game page. 
        /// </summary>
        public bool IsClockShowing
        {
            get { return GetSetting(true); }
            set { SaveSetting(value); OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the previous move is indicated on the board.
        /// </summary>
        public bool IsLastMoveIndicatorShowing
        {
            get { return GetSetting(true); }
            set { SaveSetting(value); UpdateBoard(); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether player one is an AI player.
        /// </summary>
        public bool PlayerOneIsAiSetting 
        {
            get { return GetSetting(false); }
            set { SaveSetting(value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether player two is an AI player.
        /// </summary>
        public bool PlayerTwoIsAiSetting 
        {
            get { return GetSetting(true); }
            set { SaveSetting(value); }
        }

        /// <summary>
        /// Gets or sets the search depth for player one if it is an AI player.
        /// </summary>
        public int PlayerOneAiSearchDepthSetting 
        {
            get { return GetSetting(1); }
            set { SaveSetting(value); }
        }

        /// <summary>
        /// Gets or sets the search depth for player two if it is an AI player. 
        /// </summary>
        public int PlayerTwoAiSearchDepthSetting 
        {
            get { return GetSetting(1); }
            set { SaveSetting(value); }
        }

        /// <summary>
        /// Gets or sets the index of the option in the board sizes drop-down box.
        /// </summary>
        public int BoardSizeIndex
        {
            get { return GetSetting<int>(1); }
            set { SaveSetting(value); }
        }

        /// <summary>
        /// Gets the number of spaces along one edge of a square board.
        /// </summary>
        private int BoardEdgeSize
        {
            get { return (BoardSizeIndex + 3) * 2; }
        }

        /// <summary>
        /// Gets a description of the opponents that a new game would use.
        /// </summary>
        public string NewGameOpponentsText
        {
            get 
            {
                var playerOne = !PlayerOneIsAiSetting ? "Human" : "Computer level " + PlayerOneAiSearchDepthSetting;
                var playerTwo = !PlayerTwoIsAiSetting ? "Human" : "Computer level " + PlayerTwoAiSearchDepthSetting;
                return String.Format("{0} vs. {1}", playerOne, playerTwo);
            }
        }

        /// <summary>
        /// Gets a description of the board size that a new game would use. 
        /// </summary>
        public string NewGameBoardSizeText
        {
            get { return String.Format("{0} by {0} board", BoardEdgeSize); }
        }

        #region miscellaneous

        /// <summary>
        /// Gets a value from the roaming settings.
        /// </summary>
        /// <typeparam name="T">The type of the value expected.</typeparam>
        /// <param name="defaultValue">The default value to return if the setting does not exist.</param>
        /// <param name="setting">The name of the setting to retrieve; the default is the name of the calling property.</param>
        /// <returns>The value.</returns>
        private T GetSetting<T>(T defaultValue, [CallerMemberName] String setting = null)
        {
            return (T)(ApplicationData.Current.RoamingSettings.Values[setting] ?? defaultValue);
        }

        /// <summary>
        /// Saves a value to the roaming settings.
        /// </summary>
        /// <typeparam name="T">The type of the value to save.</typeparam>
        /// <param name="value">The value to save.</param>
        /// <param name="setting">The name of the setting to save the value under; the default is the name of the calling property.</param>
        private void SaveSetting<T>(T value, [CallerMemberName] String setting = null)
        {
            ApplicationData.Current.RoamingSettings.Values[setting] = value;
            UpdateStatus();
        }

        /// <summary>
        /// Updates UI bound to the Text properties.
        /// </summary>
        private void UpdateStatus()
        {
            OnPropertyChanged("NewGameOpponentsText");
            OnPropertyChanged("NewGameBoardSizeText");
        }

        /// <summary>
        /// Updates UI bound to any of the properties.
        /// </summary>
        public void UpdateSettings()
        {
            OnPropertyChanged("IsLastMoveIndicatorShowing");
            OnPropertyChanged("IsShowingValidMoves");
            OnPropertyChanged("IsClockShowing");
            OnPropertyChanged("PlayerOneIsAiSetting");
            OnPropertyChanged("PlayerTwoIsAiSetting");
            OnPropertyChanged("PlayerOneAiSearchDepthSetting");
            OnPropertyChanged("PlayerTwoAiSearchDepthSetting");
            OnPropertyChanged("BoardSizeIndex");
            OnPropertyChanged("NewGameOpponentsText");
            OnPropertyChanged("NewGameBoardSizeText");
        }

        /// <summary>
        /// Updates the game board UI.
        /// </summary>
        private void UpdateBoard()
        {
            if (GameViewModel != null) GameViewModel.UpdateBoard();
        }

        /// <summary>
        /// Creates a new game using the current new game settings.
        /// </summary>
        public void NewGame()
        {
            var playerOne = PlayerOneIsAiSetting ? (Player)PlayerOneAiSearchDepthSetting : Player.Human;
            var playerTwo = PlayerTwoIsAiSetting ? (Player)PlayerTwoAiSearchDepthSetting : Player.Human;
            var clockViewModel = new ClockViewModel(null);
            GameViewModel = new GameViewModel(clockViewModel, 
                GameFactory.GetGame(BoardEdgeSize), playerOne, playerTwo);
            clockViewModel.GameViewModel = GameViewModel;
        }

        #endregion miscellaneous
    }
}
