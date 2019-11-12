using Reversi.ViewModels;
using ReversiGameModel;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Reversi.Views
{
    /// <summary>
    /// Represents the UI for the game board. 
    /// </summary>
    public sealed partial class Board
    {
        /// <summary>
        /// Initializes a new instance of the Board class.
        /// </summary>
        public Board()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        /// <summary>
        /// Adds rows and columns to the grid based on the RowCount and 
        /// ColumnCount property values, and then populates the grid with 
        /// BoardSpace instances bound to the default or "Item" property
        /// of the data context.
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_gameViewModel != null) return;

            var rows = Enumerable.Range(0, RowCount).ToArray();
            var columns = Enumerable.Range(0, ColumnCount).ToArray();

            foreach (var row in rows) BoardGrid.RowDefinitions.Add(new RowDefinition());
            foreach (var column in columns) BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());

            foreach (var row in rows)
            {
                foreach (var column in columns)
                {
                    var boardSpace = new BoardSpace();
                    
                    // Bind the BoardSpace.SpaceState property to the GameViewModel string indexer 
                    // (the "this[String]" property), creating an index from the row and column.
                    boardSpace.SetBinding(BoardSpace.SpaceStateProperty,
                        new Binding { Path = new PropertyPath(String.Format("[{0},{1}]", row, column)) });

                    // Bind the BoardSpace.Command property to the GameViewModel.MoveCommand property,
                    // adding a Space object as the command parameter to represent the row and column. 
                    boardSpace.SetBinding(BoardSpace.CommandProperty,
                        new Binding { Path = new PropertyPath("MoveCommand") });
                    boardSpace.CommandParameter = new Space(row, column);

                    // Add the BoardSpace to the BoardGrid at the given position.
                    Grid.SetRow(boardSpace, row);
                    Grid.SetColumn(boardSpace, column);
                    BoardGrid.Children.Add(boardSpace);
                }
            }

            // Handle various GameViewModel events to trigger animations and visual state changes.
            _gameViewModel = DataContext as GameViewModel;
            if (_gameViewModel == null) return;
            _gameViewModel.ForcedPass += (s2, e2) =>
            {
                PassStoryboard.Begin();
                PassStoryboard.Completed += (s3, e3) => PassStoryboard.Stop();
            };
            _gameViewModel.PropertyChanged += (s2, e2) =>
            {
                if (e2.PropertyName.Equals("IsGameOver")) UpdateState();
            };
            _gameViewModel.Clock.PropertyChanged += (s2, e2) =>
            {
                if (e2.PropertyName.Equals("IsShowingPauseDisplay")) UpdateState();
            };
            UpdateState();
        }

        /// <summary>
        /// Updates the visual state of the game board.
        /// </summary>
        private void UpdateState()
        {
            if (_gameViewModel.Clock.IsShowingPauseDisplay)
                VisualStateManager.GoToState(this, "Paused", true);
            else if (_gameViewModel.IsGameOver)
                VisualStateManager.GoToState(this, "GameOver", true);
            else VisualStateManager.GoToState(this, "Default", true);
        }

        /// <summary>
        /// Gets or sets the number of rows on the board.
        /// </summary>
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }
        
        /// <summary>
        /// Identifier for the RowCount dependency property.
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", 
            typeof(int), typeof(Board), new PropertyMetadata(8));

        /// <summary>
        /// Gets or sets the number of columns on the board.
        /// </summary>
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        /// <summary>
        /// Identifier for the ColumnCount dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", 
            typeof(int), typeof(Board), new PropertyMetadata(8));

        /// <summary>
        /// Resumes the paused game clock.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void StartClock(object sender, RoutedEventArgs e)
        {
            _gameViewModel.Clock.Start();
        }

        // A reference to the current game.
        private GameViewModel _gameViewModel;
    }
}
