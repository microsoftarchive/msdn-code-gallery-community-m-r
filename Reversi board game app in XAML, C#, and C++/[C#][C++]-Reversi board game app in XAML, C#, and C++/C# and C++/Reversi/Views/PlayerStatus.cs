using Reversi.ViewModels;
using ReversiGameModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Reversi.Views
{
    /// <summary>
    /// The UI for the score board and clock.
    /// </summary>
    public class PlayerStatus : Control
    {
        /// <summary>
        /// Initializes a new instance of the PlayerStatus control.
        /// </summary>
        public PlayerStatus()
        {
            DefaultStyleKey = typeof(PlayerStatus);
            SetBinding(CurrentPlayerProperty, new Binding { Path = new PropertyPath("CurrentPlayer") });
            SetBinding(IsClockShowingProperty, new Binding { Path = new PropertyPath("Settings.IsClockShowing") });
            SetBinding(IsGameOverProperty, new Binding { Path = new PropertyPath("IsGameOver") });
            SetBinding(WinnerProperty, new Binding { Path = new PropertyPath("Winner") });
        }

        /// <summary>
        /// Updates the visual state of the control based on the initial binding values.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdatePlayerState(false);
            UpdateClockState(false);
            UpdateGameOverState(false);
        }

        /// <summary>
        /// Gets or sets the current player.
        /// </summary>
        public State CurrentPlayer
        {
            get { return (State)GetValue(CurrentPlayerProperty); }
            set { SetValue(CurrentPlayerProperty, value); }
        }

        /// <summary>
        /// Identifier for the CurrentPlayer dependency property.
        /// </summary>
        public static readonly DependencyProperty CurrentPlayerProperty =
            DependencyProperty.Register("CurrentPlayer", typeof(State), typeof(PlayerStatus),
            new PropertyMetadata(State.One, CurrentPlayerChanged));

        /// <summary>
        /// Updates the visual state of the control to match the changed CurrentPlayer property.
        /// </summary>
        /// <param name="d">The source of the property change.</param>
        /// <param name="e">Details about the property change.</param>
        private static void CurrentPlayerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PlayerStatus).UpdatePlayerState(true);
        }

        /// <summary>
        /// Updates the visual state of the player glyphs, optionally using animated transitions.
        /// </summary>
        /// <param name="useTransitions">true to use transitions; otherwise, false.</param>
        private void UpdatePlayerState(bool useTransitions)
        {
            switch (CurrentPlayer)
            {
                case State.None: 
                    GoToState("TiedGameOver", useTransitions); 
                    PlayerOneState = BoardSpaceState.PlayerOne;
                    PlayerTwoState = BoardSpaceState.PlayerTwo;                    
                    break;
                case State.One: 
                    GoToState("PlayerOneTurn", useTransitions); 
                    PlayerOneState = BoardSpaceState.PlayerOne;
                    PlayerTwoState = BoardSpaceState.PlayerTwoHint;
                    break;
                case State.Two: 
                    GoToState("PlayerTwoTurn", useTransitions); 
                    PlayerOneState = BoardSpaceState.PlayerOneHint;
                    PlayerTwoState = BoardSpaceState.PlayerTwo;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the visual state of the player one glyph. 
        /// </summary>
        public BoardSpaceState PlayerOneState
        {
            get { return (BoardSpaceState)GetValue(PlayerOneStateProperty); }
            set { SetValue(PlayerOneStateProperty, value); }
        }

        /// <summary>
        /// Identifier for the PlayerOneState dependency property.
        /// </summary>
        public static readonly DependencyProperty PlayerOneStateProperty =
            DependencyProperty.Register("PlayerOneState", typeof(BoardSpaceState),
            typeof(PlayerStatus), new PropertyMetadata(BoardSpaceState.PlayerOne));

        /// <summary>
        /// Gets or sets the visual state of the player two glyph.
        /// </summary>
        public BoardSpaceState PlayerTwoState
        {
            get { return (BoardSpaceState)GetValue(PlayerTwoStateProperty); }
            set { SetValue(PlayerTwoStateProperty, value); }
        }

        /// <summary>
        /// Identifier for the PlayerTwoState dependency property.
        /// </summary>
        public static readonly DependencyProperty PlayerTwoStateProperty =
            DependencyProperty.Register("PlayerTwoState", typeof(BoardSpaceState),
            typeof(PlayerStatus), new PropertyMetadata(BoardSpaceState.PlayerTwo));

        /// <summary>
        /// Gets or sets a value that indicates whether the clock is displayed.
        /// </summary>
        public bool IsClockShowing
        {
            get { return (bool)GetValue(IsClockShowingProperty); }
            set { SetValue(IsClockShowingProperty, value); }
        }

        /// <summary>
        /// Identifier for the IsClockShowing dependency property.
        /// </summary>
        public static readonly DependencyProperty IsClockShowingProperty =
            DependencyProperty.Register("IsClockShowing", typeof(bool),
            typeof(PlayerStatus), new PropertyMetadata(true, IsClockShowingChanged));

        /// <summary>
        /// Updates the visual state of the control to match the changed IsClockShowing property.
        /// </summary>
        /// <param name="d">The source of the property change.</param>
        /// <param name="e">Details about the property change.</param>
        private static void IsClockShowingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PlayerStatus).UpdateClockState(true);
        }

        /// <summary>
        /// Updates the visual state of the clock, optionally using animated transitions.
        /// </summary>
        /// <param name="useTransitions">true to use transitions; otherwise, false.</param>
        private void UpdateClockState(bool useTransitions)
        {
            GoToState(IsClockShowing ? "ClockShowing" : "ClockHidden", useTransitions);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the game is over.
        /// </summary>
        public bool IsGameOver
        {
            get { return (bool)GetValue(IsGameOverProperty); }
            set { SetValue(IsGameOverProperty, value); }
        }

        /// <summary>
        /// Identifies the IsGameOver dependency property.
        /// </summary>
        public static readonly DependencyProperty IsGameOverProperty =
            DependencyProperty.Register("IsGameOver", typeof(bool),
            typeof(PlayerStatus), new PropertyMetadata(false, IsGameOverChanged));

        /// <summary>
        /// Updates the visual state of the control to match the changed IsGameOver property.
        /// </summary>
        /// <param name="d">The source of the property change.</param>
        /// <param name="e">Details about the property change.</param>
        private static void IsGameOverChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            (d as PlayerStatus).UpdateGameOverState(true);
        }

        /// <summary>
        /// Updates the visual state of the game over indicator, optionally using animated transitions.
        /// </summary>
        /// <param name="useTransitions">true to use transitions; otherwise, false.</param>
        private void UpdateGameOverState(bool useTransitions)
        {
            if (!IsGameOver)
            {
                GoToState("GameNotOver", useTransitions);
                return;
            }

            switch (Winner)
            {
                case State.None: GoToState("Tied", useTransitions); break;
                case State.One: GoToState("PlayerOneWinner", useTransitions); break;
                case State.Two: GoToState("PlayerTwoWinner", useTransitions); break;
            }
        }

        /// <summary>
        /// Gets or sets the winner of the game.
        /// </summary>
        private State Winner
        {
            get { return (State)GetValue(WinnerProperty); }
            set { SetValue(WinnerProperty, value); }
        }

        /// <summary>
        /// Identifies the Winner dependency property.
        /// </summary>
        private static readonly DependencyProperty WinnerProperty =
            DependencyProperty.Register("Winner", typeof(State),
            typeof(PlayerStatus), new PropertyMetadata(State.Two));

        /// <summary>
        /// Goes to the specified state, optionally using animated transitions. 
        /// </summary>
        /// <param name="state">The state to go to.</param>
        /// <param name="useTransitions">true to use animated transitions; otherwise, false.</param>
        private void GoToState(string state, bool useTransitions)
        {
            VisualStateManager.GoToState(this, state, useTransitions);
        }

    }

}
