using System;
using System.Diagnostics;
using Reversi.Common;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using ReversiGameModel;
using System.Threading.Tasks;

namespace Reversi.ViewModels
{
    /// <summary>
    /// Encapsulates the game clock code, including clock-related commands. 
    /// </summary>
    public class ClockViewModel : BindableBase, IClockViewModel
    {
        #region setup

        /// <summary>
        /// Initializes a new instance of the ClockViewModel class.
        /// </summary>
        /// <remarks>
        /// This constructor is required to instantiate this class from XAML.
        /// </remarks>
        public ClockViewModel() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the ClockViewModel class and sets the
        /// GameViewModel property to the specified view model. 
        /// </summary>
        public ClockViewModel(IGameViewModel gameViewModel)
        {
            GameViewModel = gameViewModel;
            PlayerOneMoveTimeStopwatch = new Stopwatch();
            PlayerOneTotalTimeStopwatch = new Stopwatch();
            PlayerTwoMoveTimeStopwatch = new Stopwatch();
            PlayerTwoTotalTimeStopwatch = new Stopwatch();
            Timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
            Timer.Tick += (sender, e) => UpdateClockView();
            if (gameViewModel != null) Start();
        }

        #endregion setup

        #region public properties

        /// <summary>
        /// Gets or sets a reference to the current game view-model.
        /// </summary>
        public IGameViewModel GameViewModel
        {
            get { return _gameViewModel; }
            set
            {
                if (value == null || value == _gameViewModel) return;
                if (_gameViewModel != null) _gameViewModel.PropertyChanged -= GameViewModel_PropertyChanged;
                _gameViewModel = value;
                _gameViewModel.PropertyChanged += GameViewModel_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets the command for starting or restarting the clock.
        /// </summary>
        public DelegateCommand PlayCommand 
        { 
            get 
            {
                return _playCommand ?? (_playCommand =
                    new DelegateCommand(Start, () => IsPaused));
            } 
        }

        /// <summary>
        /// Gets the command for pausing the clock.
        /// </summary>
        public DelegateCommand PauseCommand 
        { 
            get 
            {
                return _pauseCommand ?? (_pauseCommand =
                    DelegateCommand.FromAsyncHandler(Pause, () => !IsPaused));
            } 
        }

        /// <summary>
        /// Gets or sets the time span of player one's current or last move. 
        /// </summary>
        public TimeSpan PlayerOneMoveTimeSpan
        {
            get
            {
                var timeSpan = PlayerOneMoveTimeStopwatch.Elapsed + _playerOneMoveTimeSpan;
                if (_playerOneMoveTimeSpan.Ticks > 0) _playerOneMoveTimeSpan = new TimeSpan();
                return timeSpan;
            }
            set { _playerOneMoveTimeSpan = value; }
        }

        /// <summary>
        /// Gets a formatted string representing the time spent on player one's current or last move. 
        /// </summary>
        public String PlayerOneMoveTime
        {
            get { return Format(PlayerOneMoveTimeSpan); }
        }

        /// <summary>
        /// Gets or sets the total time span of all of player one's moves. 
        /// </summary>
        public TimeSpan PlayerOneTotalTimeSpan
        {
            get { return PlayerOneTotalTimeStopwatch.Elapsed + _playerOneTotalTimeSpan; }
            set { _playerOneTotalTimeSpan = value; }
        }

        /// <summary>
        /// Gets a formatted string representing the total time spent on all of player one's moves. 
        /// </summary>
        public String PlayerOneTotalTime
        {
            get { return Format(PlayerOneTotalTimeSpan); }
        }

        /// <summary>
        /// Gets or sets the time span of player two's current or last move. 
        /// </summary>
        public TimeSpan PlayerTwoMoveTimeSpan
        {
            get
            {
                var timeSpan = PlayerTwoMoveTimeStopwatch.Elapsed +
                    _playerTwoMoveTimeSpan;
                if (_playerTwoMoveTimeSpan.Ticks > 0)
                    _playerTwoMoveTimeSpan = new TimeSpan();
                return timeSpan;
            }
            set { _playerTwoMoveTimeSpan = value; }
        }

        /// <summary>
        /// Gets a formatted string representing the time spent on player two's current or last move. 
        /// </summary>
        public String PlayerTwoMoveTime
        {
            get { return Format(PlayerTwoMoveTimeSpan); }
        }

        /// <summary>
        /// Gets or sets the total time span of all of player two's moves. 
        /// </summary>
        public TimeSpan PlayerTwoTotalTimeSpan
        {
            get { return PlayerTwoTotalTimeStopwatch.Elapsed + _playerTwoTotalTimeSpan; }
            set { _playerTwoTotalTimeSpan = value; }
        }

        /// <summary>
        /// Gets a formatted string representing the total time spent on all of player two's moves. 
        /// </summary>
        public String PlayerTwoTotalTime
        {
            get { return Format(PlayerTwoTotalTimeSpan); }
        }

        /// <summary>
        /// Gets a value that indicates whether the clock is paused.
        /// </summary>
        public bool IsPaused
        {
            get { return !Timer.IsEnabled; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the game board shows its paused display.
        /// </summary>
        public bool IsShowingPauseDisplay
        {
            get { return !Timer.IsEnabled && !GameViewModel.IsGameOver; }
            set
            {
                if (Timer == null || Timer.IsEnabled != value) return;
                if (value) Stop(); else Start();
            }
        }

        /// <summary>
        /// Gets a value that indicates the opacity of the paused display.
        /// </summary>
        /// <remarks>
        /// Indicates partial opacity when pausing AI vs. AI games, 
        /// and full opacity when any humans are playing. This prevents
        /// off-the-clock examination of the board (that is, cheating). 
        /// </remarks>
        public double PauseDisplayOpacity
        {
            get
            {
                if (GameViewModel == null) return 0;
                return GameViewModel.IsGameAiVersusAi ? 0.2 : 1;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the pause button is visibile.
        /// </summary>
        /// <remarks>
        /// The pause button is visible when the clock is running or the game 
        /// is over; otherwise, the play button is visible. When the game is 
        /// over, the pause button appears in the disabled state. Showing
        /// the disabled pause button instead of hiding both pause and play
        /// enables the undo and redo buttons to retain their positions. 
        /// </remarks>
        public bool IsPauseButtonVisible
        {
            get 
            { 
                return Windows.ApplicationModel.DesignMode.DesignModeEnabled ? false :
                    (!IsPaused && !_pauseQueued) || GameViewModel.IsGameOver; 
            }
        }

        #endregion public properties

        #region start, stop, and switch methods

        /// <summary>
        /// Starts or resumes the clock.
        /// </summary>
        public void Start()
        {
            if (Timer.IsEnabled || GameViewModel.IsGameOver)
            {
                if (_pauseQueued) _pauseQueued = false;
                return;
            }
            Timer.Start();
            if (GameViewModel.IsGameAiVersusAi) GameViewModel.Start();
            UpdateViews();
            if (GameViewModel.CurrentPlayer == State.One)
            {
                PlayerOneMoveTimeStopwatch.Start();
                PlayerOneTotalTimeStopwatch.Start();
            }
            else
            {
                PlayerTwoMoveTimeStopwatch.Start();
                PlayerTwoTotalTimeStopwatch.Start();
            }
        }

        /// <summary>
        /// Stops the clock, or queues a pause if it is the AI's move
        /// in an AI vs. Human game.
        /// </summary>
        public void Stop()
        {
            if (!Timer.IsEnabled) return;

            // If it is the computer's turn in an AI vs. Human game, queue the 
            // pause so that it will take effect when it is the human's turn. 
            if (!GameViewModel.IsGameOver && !GameViewModel.IsGameAiVersusAi && GameViewModel.IsCurrentPlayerAi)
            {
                // Pause (in the Switch method) when the computer finishes its move.
                _pauseQueued = true;
                return;
            }

            // If it is an AI vs. AI game, halt it. If any players are human,
            // then the game will pause automatically when it is a human's turn.
            if (GameViewModel.IsGameAiVersusAi) GameViewModel.Stop();

            Timer.Stop();
            UpdateViews();
            PlayerOneMoveTimeStopwatch.Stop();
            PlayerOneTotalTimeStopwatch.Stop();
            PlayerTwoMoveTimeStopwatch.Stop();
            PlayerTwoTotalTimeStopwatch.Stop();
        }

        /// <summary>
        /// Pauses the clock.
        /// </summary>
        /// <returns></returns>
        private async Task Pause()
        {
            Stop();

            if (!GameViewModel.IsCurrentPlayerAi || 
                GameViewModel.IsGameAiVersusAi || 
                _isPauseWarningDisabled) return;

            var message = new Windows.UI.Popups.MessageDialog("The game will pause when it's your turn.");
            message.Commands.Add(new Windows.UI.Popups.UICommand("Close"));
            message.Commands.Add(new Windows.UI.Popups.UICommand("Stop showing this message"));
            var result = await message.ShowAsync();
            if (result.Label != "Close") _isPauseWarningDisabled = true;
        }

        /// <summary>
        /// Called when a player makes a move, stopping that player's
        /// clock and starting the opponent's clock (or stopping both
        /// player's clocks if the game has been paused). 
        /// </summary>
        private void Switch()
        {
            // If the game is not the main view, and it is now the human's turn 
            // in a human vs. AI game, then pause the game. 
            if (!Window.Current.Visible && 
                !GameViewModel.IsCurrentPlayerAi && !GameViewModel.IsGameHumanVersusHuman) Stop();

            // Retain the pause queue state only if it is the computer's turn. This is 
            // necessary because the pause can occur after the human's turn but
            // before the GameViewModel.CurrentPlayer changes (which calls this method). 
            if (_pauseQueued && !GameViewModel.IsCurrentPlayerAi)
            {
                Stop();
                _pauseQueued = false;
            }
            if (GameViewModel.CurrentPlayer == State.One)
            {
                PlayerTwoMoveTimeStopwatch.Stop();
                PlayerTwoTotalTimeStopwatch.Stop();
                if (Timer.IsEnabled)
                {
                    PlayerOneMoveTimeStopwatch.Restart();
                    PlayerOneTotalTimeStopwatch.Start();
                }
                else PlayerOneMoveTimeStopwatch.Reset();
            }
            else
            {
                PlayerOneMoveTimeStopwatch.Stop();
                PlayerOneTotalTimeStopwatch.Stop();
                if (Timer.IsEnabled)
                {
                    PlayerTwoMoveTimeStopwatch.Restart();
                    PlayerTwoTotalTimeStopwatch.Start();
                }
                else PlayerTwoMoveTimeStopwatch.Reset();
            }
        }

        #endregion start, stop, and switch methods

        #region miscellaneous

        /// <summary>
        /// Responds to property changes in the game view model, switching or pausing the clock as 
        /// appropriate when the game turn changes or the game is over. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void GameViewModel_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPlayer") && GameViewModel.Game != null) Switch();
            else if (e.PropertyName.Equals("IsGameOver"))
            {
                if (_gameViewModel.IsGameOver) Stop();
                else if (!_gameViewModel.IsGameAiVersusAi) Start();
                OnPropertyChanged("IsPaused");
                OnPropertyChanged("IsPauseButtonVisible");
            }
        }

        /// <summary>
        /// Updates UI bound to play/pause properties. 
        /// </summary>
        private void UpdateViews()
        {
            OnPropertyChanged("IsPaused");
            OnPropertyChanged("IsShowingPauseDisplay");
            OnPropertyChanged("IsPauseButtonVisible");
            UpdateClockView();
            PlayCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
            GameViewModel.MoveCommand.RaiseCanExecuteChanged();
            GameViewModel.UndoCommand.RaiseCanExecuteChanged();
            GameViewModel.RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Updates UI bound to the Time properties.
        /// </summary>
        private void UpdateClockView()
        {
            OnPropertyChanged("PlayerOneMoveTime");
            OnPropertyChanged("PlayerOneTotalTime");
            OnPropertyChanged("PlayerTwoMoveTime");
            OnPropertyChanged("PlayerTwoTotalTime");
        }

        /// <summary>
        /// Formats the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to format.</param>
        /// <returns>A formatted string representing the specified time span.</returns>
        private static string Format(TimeSpan timeSpan)
        {
            return timeSpan.ToString(timeSpan.Hours > 0 ? "h':'mm':'ss" : "mm':'ss");
        }

        // Fields and simple properties. 
        private IGameViewModel _gameViewModel;
        private DelegateCommand _playCommand;
        private DelegateCommand _pauseCommand;
        private TimeSpan _playerOneMoveTimeSpan;
        private TimeSpan _playerOneTotalTimeSpan;
        private TimeSpan _playerTwoMoveTimeSpan;
        private TimeSpan _playerTwoTotalTimeSpan;
        private Stopwatch PlayerOneMoveTimeStopwatch { get; set; }
        private Stopwatch PlayerOneTotalTimeStopwatch { get; set; }
        private Stopwatch PlayerTwoMoveTimeStopwatch { get; set; }
        private Stopwatch PlayerTwoTotalTimeStopwatch { get; set; }
        private DispatcherTimer Timer { get; set; }
        private bool _isPauseWarningDisabled;
        private bool _pauseQueued;

        #endregion miscellaneous
    }
}
