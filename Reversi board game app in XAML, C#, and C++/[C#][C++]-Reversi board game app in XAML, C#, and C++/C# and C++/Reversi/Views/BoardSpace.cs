using Reversi.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Reversi.Views
{
    /// <summary>
    /// Represents the UI for a single space on the game board. 
    /// </summary>
    public class BoardSpace : Button
    {
        /// <summary>
        /// Initializes a new instance of the BoardSpace class.
        /// </summary>
        public BoardSpace()
        {
            DefaultStyleKey = typeof(BoardSpace);
        }

        /// <summary>
        /// Updates the visual state of the space based on the initial binding values.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateSpaceState(false);
        }

        /// <summary>
        /// Gets or sets the visual state of the space.
        /// </summary>
        public BoardSpaceState SpaceState
        {
            get { return (BoardSpaceState)GetValue(SpaceStateProperty); }
            set { SetValue(SpaceStateProperty, value); }
        }

        /// <summary>
        /// Identifier for the SpaceState dependency property.
        /// </summary>
        public static readonly DependencyProperty SpaceStateProperty =
            DependencyProperty.Register("SpaceState",
            typeof(BoardSpaceState), typeof(BoardSpace),
            new PropertyMetadata(BoardSpaceState.None, SpaceStateChanged));

        /// <summary>
        /// Updates the visual state of the space to match the changed SpaceState value.
        /// </summary>
        /// <param name="d">The source of the property change.</param>
        /// <param name="e">Details about the property change.</param>
        private static void SpaceStateChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            (d as BoardSpace).UpdateSpaceState(true);
        }

        /// <summary>
        /// Updates the visual state of the space, optionally using animated transitions.
        /// </summary>
        /// <param name="useTransitions">true to use transitions; otherwise, false.</param>
        private void UpdateSpaceState(bool useTransitions)
        {
            string pieceState;
            string lastMoveState;
            var state = SpaceState;
            if (state.ToString().Contains("New"))
            {
                pieceState = state.ToString().Substring(0, 9);
                lastMoveState = state.ToString().Substring(9);
            }
            else
            {
                pieceState = state.ToString();
                lastMoveState = "NoNewIndicator";
            }
            VisualStateManager.GoToState(this, pieceState, useTransitions);
            VisualStateManager.GoToState(this, lastMoveState, useTransitions);
        }
    }
}
