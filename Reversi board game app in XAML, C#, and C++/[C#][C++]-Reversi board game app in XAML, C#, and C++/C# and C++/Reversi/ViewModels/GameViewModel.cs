using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Reversi.Common;
using ReversiGameModel;
using Reversi.Models;

namespace Reversi.ViewModels
{
    /// <summary>
    /// Encapsulates the non-visual UI code for a game.
    /// </summary>
    public class GameViewModel : BindableBase, IDisposable, IGameViewModel
    {
        #region setup

        /// <summary>
        /// Initializes a new instance of the GameViewModel class using default settings.
        /// </summary>
        /// <remarks>
        /// This constructor is required to instantiate this class from XAML.
        /// </remarks>
        public GameViewModel() { }

        /// <summary>
        /// Initializes a new instance of the GameViewModel class using the specified settings. 
        /// </summary>
        public GameViewModel(IClockViewModel clock, IGame game, 
            Player playerOne = Player.Human, Player playerTwo = Player.Human)
        {
            Clock = clock;
            Game = game;
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            Start();
        }

        #endregion setup

        #region public properties and event

        /// <summary>
        /// Gets a value that indicates the visual state of the specified space.
        /// </summary>
        /// <param name="index">The space as a string in "row,column" format.</param>
        /// <returns>A BoardSpaceState value indicating the name of a state in the 
        /// PieceStates visual state group in the BoardSpace control template.</returns>
        /// <remarks>This property is bound to the BoardSpace instances of the Board
        /// user control using procedural code in the Board.OnLoaded method. The 
        /// property path for this property is String.Format("[{0},{1}]", row, column)
        /// for each space on the board. 
        /// </remarks>
        public BoardSpaceState this[String index] { get { return GetBoardState(index); } }

        /// <summary>
        /// Gets the command for performing a move.
        /// </summary>
        public DelegateCommand<ISpace> MoveCommand 
        { 
            get 
            { 
                return _moveCommand ?? (_moveCommand = 
                    DelegateCommand<ISpace>.FromAsyncHandler(MoveAsync, CanMove));
            } 
        }

        /// <summary>
        /// Gets the command for undoing the previous move.
        /// </summary>
        public DelegateCommand UndoCommand 
        { 
            get 
            { 
                return _undoCommand ?? (_undoCommand = 
                    DelegateCommand.FromAsyncHandler(UndoAsync, CanUndo));
            } 
        }

        /// <summary>
        /// Gets the command for redoing the next move. 
        /// </summary>
        public DelegateCommand RedoCommand 
        { 
            get 
            {
                return _redoCommand ?? (_redoCommand = 
                    DelegateCommand.FromAsyncHandler(RedoAsync, CanRedo));
            } 
        }

        /// <summary>
        /// Occurs when the current move in the game is a forced pass.
        /// </summary>
        public event EventHandler ForcedPass;

        /// <summary>
        /// Gets the number of rows in the current game.
        /// </summary>
        public int RowCount { get { return Game.RowCount; } }

        /// <summary>
        /// Gets the number of columns in the current game.
        /// </summary>
        public int ColumnCount { get { return Game.ColumnCount; } }

        /// <summary>
        /// Gets or sets the current Game instance.
        /// </summary>
        public IGame Game { get; set; }

        /// <summary>
        /// Gets or sets a reference to the clock view-model.
        /// </summary>
        public IClockViewModel Clock { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether player one is a human or an AI of a particular level.
        /// </summary>
        public Player PlayerOne { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether player two is a human or an AI of a particular level.
        /// </summary>
        public Player PlayerTwo { get; set; }

        /// <summary>
        /// Gets or sets a list of the spaces affected by the last move, starting with
        /// the space of the move and followed by the spaces of all the captured pieces. 
        /// </summary>
        public IList<ISpace> LastMoveAffectedSpaces { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the current player is an AI player.
        /// </summary>
        public bool IsCurrentPlayerAi
        {
            get
            {
                // Use the Game.CurrentPlayer property because the 
                // GameViewModel.CurrentPlayer property is not updated until 
                // after the effects of the last move take place.
                return Game.CurrentPlayer == State.One ? IsPlayerOneAi : IsPlayerTwoAi;
            }
        }

        /// <summary>
        /// Gets the AI search depth of the current player, or 0 if the player is human. 
        /// </summary>
        public int CurrentPlayerAiSearchDepth
        {
            get { return (CurrentPlayer == State.One ? (int)PlayerOne : (int)PlayerTwo); }
        }

        /// <summary>
        /// Gets a value that indicates whether player one is an AI player.
        /// </summary>
        public bool IsPlayerOneAi { get { return (int)PlayerOne > 0; } }

        /// <summary>
        /// Gets the AI search depth of player one, or 0 if the player is human. 
        /// </summary>
        public int PlayerOneAiSearchDepth { get { return (int)PlayerOne; } }

        /// <summary>
        /// Gets a value that indicates whether player two is an AI player.
        /// </summary>
        public bool IsPlayerTwoAi { get { return (int)PlayerTwo > 0; } }

        /// <summary>
        /// Gets the AI search depth of player two, or 0 if the player is human. 
        /// </summary>
        public int PlayerTwoAiSearchDepth { get { return (int)PlayerTwo; } }

        /// <summary>
        /// Gets a value that indicates whether the game is an AI vs. AI game.
        /// </summary>
        public bool IsGameAiVersusAi { get { return (int)PlayerOne > 0 && (int)PlayerTwo > 0; } }

        /// <summary>
        /// Gets a value that indicates whether the game is a human vs. human game.
        /// </summary>
        public bool IsGameHumanVersusHuman { get { return (int)PlayerOne == 0 && (int)PlayerTwo == 0; } }

        /// <summary>
        /// Gets or sets the current player. 
        /// </summary>
        public State CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                SetProperty(ref _currentPlayer, value);
                OnPropertyChanged("IsCurrentPlayerAi");
                OnPropertyChanged("IsPlayerOneAi");
                OnPropertyChanged("IsPlayerTwoAi");
                OnPropertyChanged("CurrentPlayerAiSearchDepth");
            }
        }

        /// <summary>
        /// Gets the current score.
        /// </summary>
        public IScore Score
        {
            get { return _score; }
            set { SetProperty(ref _score, value); }
        }

        /// <summary>
        /// Gets the font size needed for correct display of the current score.
        /// </summary>
        public int ScoreFontSize { get { return (Score.PlayerOne > 99 || Score.PlayerTwo > 99) ? 36 : 56; } }

        /// <summary>
        /// Gets the vertical offset required for correct display of the current score.
        /// </summary>
        public double ScoreTranslateY { get { return (Score.PlayerOne > 99 || Score.PlayerTwo > 99) ? -6 : 0; } }

        /// <summary>
        /// Gets or sets a value that indicates whether the game is over.
        /// </summary>
        public bool IsGameOver
        {
            get { return _isGameOver; }
            private set
            {
                if (value)
                {
                    OnPropertyChanged("GameOverText");
                    OnPropertyChanged("Winner");
                }
                SetProperty(ref _isGameOver, value);
            }
        }

        /// <summary>
        /// Gets the winner of the game.
        /// </summary>
        public State Winner { get { return Game.Winner; } }

        /// <summary>
        /// Gets a message that indicates the winner or tie state of the game.
        /// </summary>
        public string GameOverText
        {
            get
            {
                switch (Winner)
                {
                    case State.One: return "Red wins!";
                    case State.Two: return "Blue wins!";
                    default: return "Tie game!";
                }
            }
        }

        /// <summary>
        /// Gets a description of the opponents in the game.
        /// </summary>
        public string OpponentsText
        {
            get
            {
                var playerOne = (int)PlayerOne == 0 ? "Human" : "Computer level " + (int)PlayerOne;
                var playerTwo = (int)PlayerTwo == 0 ? "Human" : "Computer level " + (int)PlayerTwo;
                return String.Format("{0} vs. {1}", playerOne, playerTwo);
            }
        }

        /// <summary>
        /// Gets a description of the board size of the game.
        /// </summary>
        public string BoardSizeText { get { return String.Format("{0} by {1} board", RowCount, ColumnCount); } }

        #endregion public event and properties

        #region private move/undo/redo-related methods

        /// <summary>
        /// Indicates whether the move command can execute.
        /// </summary>
        /// <returns>true when move can execute; otherwise, false.</returns>
        private bool CanMove(ISpace space)
        {
            return !Clock.IsPaused && IsValidMove(space) && !IsCurrentPlayerAi;
        }

        /// <summary>
        /// Performs the specified move.
        /// </summary>
        /// <param name="move">The move to perform.</param>
        /// <returns>A task for awaiting the method.</returns>
        private async Task MoveAsync(ISpace move)
        {
            var cancellationToken = GetNewCancellationToken();
            LastMoveAffectedSpaces = await Game.MoveAsync(move).AsTask(cancellationToken);
            if (cancellationToken.IsCancellationRequested) return;
            await OnMoveCompletedAsync(cancellationToken);
        }

        /// <summary>
        /// Performs an AI move, continuing after a minimum delay. 
        /// </summary>
        /// <returns>A task for awaiting the method.</returns>
        /// <remarks>
        /// The delay mimics a human minimum response time and also supports undo/redo 
        /// navigation through the AI's turns, giving enough time for the user 
        /// to press the navigation button again before the AI can make its move.         
        /// </remarks>
        private async Task AiMoveAsync()
        {
            var cancellationToken = GetNewCancellationToken();

            // Unlike the MoveAsync method, the AiMoveAsync method requires a try/catch 
            // block for cancellation. This is because the AI search checks for 
            // cancellation deep within a recursive, iterative search process
            // that is easiest to halt by throwing an exception. 
            try
            {
                // The WhenAll method call enables the delay and the AI search to 
                // occur concurrently. However, in order to retrieve the return 
                // value of the first task, both tasks must have the same signature,
                // thus requiring the delay task to have a (meaningless) return value.  
                var results = await Task.WhenAll(
                    Game.GetBestMoveAsync(CurrentPlayerAiSearchDepth).AsTask(cancellationToken),
                    Task.Run(async () =>
                    {
                        await DelayAsync(MinimumTurnLength, cancellationToken);
                        return (ISpace)null;
                    })
                );

                // Perform the AI move only after both the search and the minimum delay have passed.
                LastMoveAffectedSpaces = await Game.MoveAsync(results[0]).AsTask(cancellationToken);
                if (cancellationToken.IsCancellationRequested) return;

                await OnMoveCompletedAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                System.Diagnostics.Debug.WriteLine("cancelled with exception");
            }
        }

        /// <summary>
        /// Passes the current move.
        /// </summary>
        /// <returns>A task for awaiting the method.</returns>
        private async Task PassAsync()
        {
            var cancellationToken = GetNewCancellationToken();

            // Delay long enough to show the effect of the move and provide 
            // an opportunity to undo before displaying the pass indicator.
            await DelayAsync(MinimumTurnLength);

            if (cancellationToken.IsCancellationRequested) return;

            // Raise the ForcedPass event in order to display the pass indicator.
            var passEvent = ForcedPass;
            if (passEvent != null) passEvent(this, EventArgs.Empty);

            // Perform the move, but do not clear the LastMoveAffectedSpaces list.
            // LastMoveAffectedSpaces remains the same so that the info is not
            // lost as a result of an automatic pass when the user isn't looking.
            // The user can tell that a pass occurred because of the PlayerStatus
            // control, and also because the last-move and valid-move indicators 
            // (if enabled) are for the same player. 
            await Game.PassAsync().AsTask(cancellationToken);

            // Delay long enough to display the pass move indicator 
            // before performing the next move.
            await DelayAsync(MinimumTurnLength);

            UpdateView();
            if (!cancellationToken.IsCancellationRequested) await NextMoveAsync();
        }

        /// <summary>
        /// Displays a move with a short delay before displaying the captures 
        /// resulting from the move and proceeding to the next move.
        /// </summary>
        /// <param name="cancellationToken">A token that enables the cancellation of
        /// an automatic next move so that the game can be reset to a different turn.</param>
        /// <returns>A task for awaiting the method.</returns>
        private async Task OnMoveCompletedAsync(CancellationToken cancellationToken)
        {
            if (LastMoveAffectedSpaces.Count > 0)
            {
                // Update the board space where a new piece was added. This space
                // corresponds to the first entry (index 0) of LastMoveAffectedSpaces.
                // The "Item" property refers to the string indexer or "this" property,
                // which expects an index in "row,column" format. 
                OnPropertyChanged(String.Format("Item[{0},{1}]",
                    LastMoveAffectedSpaces[0].Row, LastMoveAffectedSpaces[0].Column));
                await DelayAsync(MinimumStepLength);
            }

            UpdateView();

            if (cancellationToken.IsCancellationRequested) return;

            var window = Windows.UI.Xaml.Window.Current;
            if (window != null && !window.Visible && !IsCurrentPlayerAi)
            {
                Toast.Show("It's your turn!");
            }

            await NextMoveAsync();
        }

        /// <summary>
        /// Synchronizes the game view model with the game and then performs any
        /// automatic next move as appropriate (pass or AI move). 
        /// </summary>
        /// <returns>A task for awaiting the method.</returns>
        private async Task NextMoveAsync()
        {
            if ((IsGameAiVersusAi && Clock.IsPaused) || IsGameOver) return;
            if (IsValidMove(null)) await PassAsync();
            else if (IsCurrentPlayerAi) await AiMoveAsync();
        }

        /// <summary>
        /// Indicates whether the specified move is legal.
        /// </summary>
        /// <param name="move">The move to assess.</param>
        /// <returns>true if the move is legal; otherwise, false.</returns>
        private bool IsValidMove(ISpace move) { return Game.IsValidMove(move); }

        /// <summary>
        /// Indicates whether the undo command can execute.
        /// </summary>
        /// <returns>true when undo can execute; otherwise, false.</returns>
        private bool CanUndo()
        {
            return
                !UndoOrRedoInProgress &&

                // Confirm that there are moves to undo.
                Game.Moves.Count > 0 &&

                // Confirm that the clock is running, the game is over,
                // or it's an AI vs. AI game (in which case it doesn't 
                // matter whether the clock is running). 
                (IsGameAiVersusAi || !Clock.IsPaused || IsGameOver);
        }

        /// <summary>
        /// Undoes the last move, if there is one.
        /// </summary>
        private async Task UndoAsync()
        {
            UndoOrRedoInProgress = true;

            if (IsGameAiVersusAi) Clock.Stop();
            Stop();

            // The WhenAll method call enables the undo and the minimum delay to 
            // occur concurrently. However, in order to retrieve the return 
            // value of the first task, both tasks must have the same signature,
            // thus requiring the delay task to have a (meaningless) return value.  
            var results = await Task.WhenAll(
                Game.UndoAsync().AsTask(),
                Task.Run(async () =>
                {
                    await DelayAsync(MinimumStepLength);
                    return (IList<ISpace>)null;
                })
            );
            LastMoveAffectedSpaces = results[0];

            UpdateView();

            UndoOrRedoInProgress = false;

            await NextMoveAsync();
        }

        /// <summary>
        /// Indicates whether the redo command can execute.
        /// </summary>
        /// <returns>true when redo can execute; otherwise, false.</returns>
        private bool CanRedo()
        {
            return
                !UndoOrRedoInProgress &&

                // Confirm that there are moves to redo.
                Game.Moves.Count < Game.MoveStack.Count &&

                // Confirm that it's an AI vs. AI game with the clock paused
                // or a game with at least one person and the clock running.
                // (Redo is irrelevant in a non-paused AI vs. AI game because the
                // AI will replay undone moves as fast as the redo command would.)
                (IsGameAiVersusAi ^ !Clock.IsPaused);
        }

        /// <summary>
        /// Performs the next move in the move stack, if there is one.
        /// </summary>
        private async Task RedoAsync()
        {
            UndoOrRedoInProgress = true;

            Stop();

            // The WhenAll method call enables the redo and the minimum delay to 
            // occur concurrently. However, in order to retrieve the return 
            // value of the first task, both tasks must have the same signature,
            // thus requiring the delay task to have a (meaningless) return value.  
            var results = await Task.WhenAll(
                Game.RedoAsync().AsTask(),
                Task.Run(async () =>
                {
                    await DelayAsync(MinimumStepLength);
                    return (IList<ISpace>)null;
                })
            );
            LastMoveAffectedSpaces = results[0];

            UpdateView();

            UndoOrRedoInProgress = false;

            await NextMoveAsync();
        }

        #endregion private move/undo/redo-related methods

        #region miscellaneous

        public BoardSpaceState GetBoardState(String index)
        {
            var settings = (App.Current as App).SettingsViewModel;
            var coordinates = index.Split(',');
            var row = Int32.Parse(coordinates[0]);
            var column = Int32.Parse(coordinates[1]);
            var space = new Space(row, column);
            var spaceState = Game.GetSpaceState(row, column);
            var isPlayerOneState = (spaceState == State.One);
            var boardSpaceState = BoardSpaceState.None;
            if (spaceState != State.None)
            {
                if (settings.IsLastMoveIndicatorShowing &&
                    LastMoveAffectedSpaces != null && LastMoveAffectedSpaces.Count > 0)
                {
                    if (LastMoveAffectedSpaces[0].Equals(space))
                    {
                        boardSpaceState = isPlayerOneState ?
                            BoardSpaceState.PlayerOneNewPiece : BoardSpaceState.PlayerTwoNewPiece;
                    }
                    else if (LastMoveAffectedSpaces.Contains(space))
                    {
                        boardSpaceState = isPlayerOneState ?
                            BoardSpaceState.PlayerOneNewCapture : BoardSpaceState.PlayerTwoNewCapture;
                    }
                    else boardSpaceState = isPlayerOneState ?
                        BoardSpaceState.PlayerOne : BoardSpaceState.PlayerTwo;
                }
                else boardSpaceState = isPlayerOneState ?
                    BoardSpaceState.PlayerOne : BoardSpaceState.PlayerTwo;
            }
            else if (settings.IsShowingValidMoves && Game.IsValidMove(row, column))
            {
                boardSpaceState = Game.CurrentPlayer == State.One ?
                    BoardSpaceState.PlayerOneHint : BoardSpaceState.PlayerTwoHint;
            }
            return boardSpaceState;
        }

        /// <summary>
        /// Starts or resumes the game. 
        /// </summary>
        public void Start() 
        {
            UpdateView();

            // Call NextMoveAsync asynchronously without awaiting its return
            // (and disable the compiler warning about that).
            #pragma warning disable 4014
            NextMoveAsync();
            #pragma warning restore 4014
        }

        /// <summary>
        /// Cancels the current AI move, halting the progress of the game.
        /// </summary>
        public void Stop() { if (_canceller != null) _canceller.Cancel(); }

        /// <summary>
        /// Updates UI bound to the view model properties.
        /// </summary>
        private void UpdateView()
        {
            SyncModelProperties();
            UpdateBoard();
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
            MoveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Synchronizes the game view model with the game.
        /// </summary>
        /// <remarks>
        /// The view's rendering of the game is often delayed to account for animations.
        /// This method is public to enable unit tests to force the view model to reflect
        /// the true state of the underlying model. 
        /// </remarks>
        public void SyncModelProperties()
        {
            IsGameOver = Game.IsGameOver();
            CurrentPlayer = IsGameOver ? Winner : Game.CurrentPlayer;
            Score = Game.GetScore();
        }

        /// <summary>
        /// Updates the UI for each board space.
        /// </summary>
        public void UpdateBoard() { OnPropertyChanged("Item[]"); }

        /// <summary>
        /// Resets the CancellationTokenSource and gets a new token.
        /// </summary>
        private CancellationToken GetNewCancellationToken()
        {
            if (_canceller != null) _canceller.Dispose();
            _canceller = new CancellationTokenSource();
            return _canceller.Token;
        }

        /// <summary>
        /// Indicates whether the code is running in an app instance
        /// as opposed to a unit test. 
        /// </summary>
        private readonly bool isWindowPresent = Windows.UI.Xaml.Window.Current != null;

        /// <summary>
        /// Pause for the specified number of milliseconds, optionally passing in
        /// a token to cancel the pause. 
        /// </summary>
        /// <param name="milliseconds">The delay.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task for awaiting the method.</returns>
        private async Task DelayAsync(int milliseconds, Nullable<CancellationToken> cancellationToken = null)
        {
            // Delay only in the running app (not in unit tests). 
            if (isWindowPresent)
            {
                if (!cancellationToken.HasValue) await Task.Delay(milliseconds);
                else await Task.Delay(milliseconds, cancellationToken.Value);
            }
        }

        /// <summary>
        /// Releases resources used by the class.
        /// </summary>
        ~GameViewModel() { Dispose(false); }

        /// <summary>
        /// Releases resources used by this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by this class.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; 
        /// false to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _canceller.Dispose();
            _disposed = true;
        }

        // A cancellation token source so that AI progress can be halted by the 
        // UndoAsync, RedoAsync, and Stop methods. This is an IDisposable class, so 
        // it needs the _disposed field, destructor, and Dispose methods for cleanup. 
        private CancellationTokenSource _canceller;
        private bool _disposed;

        // A few standard delays (in milliseconds) that enhance the perception 
        // of sequences of game events. These delays work in concert
        // with animation durations defined in Themes\Generic.xaml
        private const int MinimumStepLength = 400;
        private const int MinimumTurnLength = 1000;

        // A flag to prevent undo and redo from occurring too frequently. 
        // Because each undo and redo causes timed animation effects, 
        // these commands must be disabled while the animation is in effect. 
        // Otherwise, it is possible to perform multiple undo or redo actions 
        // in a short period, creating cumulative animation artifacts and 
        // eventually crashing the app. 
        private bool _undoOrRedoInProgress;
        private bool UndoOrRedoInProgress
        {
            get { return _undoOrRedoInProgress; }
            set
            {
                _undoOrRedoInProgress = value;
                UndoCommand.RaiseCanExecuteChanged();
                RedoCommand.RaiseCanExecuteChanged();
            }
        }

        // Backing fields for property values. 
        private DelegateCommand<ISpace> _moveCommand;
        private DelegateCommand _undoCommand;
        private DelegateCommand _redoCommand;
        private State _currentPlayer = State.One;
        private IScore _score;
        private bool _isGameOver;

        #endregion miscellaneous
    }

}
