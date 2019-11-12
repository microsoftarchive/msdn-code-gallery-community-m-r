using ReversiGameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

namespace ReversiGameComponentCS
{
    /// <summary>
    /// Represents the game logic, including the game state, move validation, and AI.
    /// </summary>
    /// <remarks>
    /// The Game class is a WinRT component so that it can be used by different 
    /// kinds of client apps or replaced with alternate implementations 
    /// (such as a C++ implementation with improved performance). 
    /// </remarks>
    public sealed class Game : IGame
    {
        #region setup

        /// <summary>
        /// Initializes a new instance of the Game class, using an 8x8 board.
        /// </summary>
        /// <remarks>
        /// This constructor is required for serialization.
        /// </remarks>
        public Game() : this(8, 8) { }

        /// <summary>
        /// Initializes a new instance of the Game class using the specified
        /// board size.
        /// </summary>
        /// <param name="rowCount">The number of rows on the board.</param>
        /// <param name="columnCount">The number of columns on the board.</param>
        public Game(int rowCount, int columnCount)
        {
            if (rowCount < 2) throw new ArgumentException(
                    "must be 2 or greater.", "rowCount");
            if (columnCount < 2) throw new ArgumentException(
                    "must be 2 or greater.", "columnCount");

            RowCount = rowCount;
            ColumnCount = columnCount;
            Moves = new List<ISpace>();
            MoveStack = new List<ISpace>();
            Board = new State[RowCount][];
            SetUpEmptyBoard();
        }

        /// <summary>
        /// Populates the Board array and sets up the initial board state.
        /// </summary>
        private void SetUpEmptyBoard()
        {
            foreach (var row in Enumerable.Range(0, RowCount))
            {
                Board[row] = new State[ColumnCount];
            }

            // Set up the default initial board state.
            Board[RowCount / 2 - 1][ColumnCount / 2 - 1] = State.One;
            Board[RowCount / 2][ColumnCount / 2] = State.One;
            Board[RowCount / 2 - 1][ColumnCount / 2] = State.Two;
            Board[RowCount / 2][ColumnCount / 2 - 1] = State.Two;

            // Uncomment and tweak one of the following to perform simple manual testing
            // with specific board states. For this to work correctly, you must 
            // start a game with the corresponding board size. 

            // Demonstrates the effect of a forced pass and a winning move.
            //LoadSerializedBoardState(
            //    "11111111" +
            //    "11111111" +
            //    "11111111" +
            //    "11102111" +
            //    "11111111" +
            //    "11201111" +
            //    "11111111" +
            //    "11111111");

            // Demonstrates the effect of a forced pass and a winning move
            // on a large board.
            //LoadSerializedBoardState(
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111120111111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111201111" +
            //    "11111111111111" +
            //    "11111111111111" +
            //    "11111111111111");
        }

        #endregion setup

        #region game state

        /// <summary>
        /// Gets or sets the number of rows in the game.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the number of columns in the game.
        /// </summary>
        public int ColumnCount { get; set; }

        /// <summary>
        /// Gets or sets the values that represent the states of the board spaces.
        /// </summary>
        public IList<IList<State>> Board { get; set; }

        /// <summary>
        /// Gets or sets a list of the moves that have been made in the game.
        /// </summary>
        /// <remarks>
        /// The Moves list contains only moves including and prior to the current move.
        /// Moves that have been undone are included in the MoveStack list.
        /// </remarks>
        public IList<ISpace> Moves 
        {
            get { return _moves; }
            set { _moves = (value as List<ISpace>) ?? new List<ISpace>(value); }
        }

        /// <summary>
        /// Gets or sets a copy of the Moves list that includes moves that have been undone. 
        /// </summary>
        /// <remarks>
        /// The MoveStack enables forward and backward navigation through the Moves list.
        /// </remarks>
        public IList<ISpace> MoveStack 
        {
            get { return _moveStack; }
            set { _moveStack = (value as List<ISpace>) ?? new List<ISpace>(value); }
        }

        /// <summary>
        /// Gets a value that indicates the current player.
        /// </summary>
        public State CurrentPlayer
        {
            get { return (Moves.Count % 2 == 0 ? State.One : State.Two); }
        }

        /// <summary>
        /// Gets a value that indicates the opponent of the current player.
        /// </summary>
        public State CurrentOpponent
        {
            get { return (Moves.Count % 2 == 0 ? State.Two : State.One); }
        }

        public State GetSpaceState(int row, int column)
        {
            return Board[row][column];
        }

        #endregion game state

        #region score, game over, and winner

        /// <summary>
        /// Gets the current score.
        /// </summary>
        /// <returns>The current score.</returns>
        public IScore GetScore()
        {
            return GetScore(Board);
        }

        /// <summary>
        /// Gets the score of the specified game board.
        /// </summary>
        /// <param name="board">The board to score.</param>
        /// <returns>The score.</returns>
        private static IScore GetScore(IList<IList<State>> board)
        {
            var playerOneScore = 0;
            var playerTwoScore = 0;

            foreach (var space in board.SelectMany(column => column))
            {
                switch (space)
                {
                    case State.One: playerOneScore++; break;
                    case State.Two: playerTwoScore++; break;
                }
            }

            return new Score
            {
                PlayerOne = playerOneScore,
                PlayerTwo = playerTwoScore
            };
        }

        /// <summary>
        /// Gets a value that indicates whether the game is over.
        /// </summary>
        /// <returns>True if the game is over; otherwise, false.</returns>
        /// <remarks>
        /// This is a method instead of a property to accommodate the overload.
        /// </remarks>
        public bool IsGameOver()
        {
            return IsGameOver(Board);
        }

        /// <summary>
        /// Gets a value that indicates whether the specified board represents a completed game.
        /// </summary>
        /// <param name="board">The game board</param>
        /// <returns>true if the game is complete; otherwise, false.</returns>
        private static bool IsGameOver(object board)
        {
            var boardList = (IList<IList<State>>)board;
            return
                IsBoardFilled(boardList) ||
                // There are no valid moves.
                (!GetValidMoveSpaces(boardList, true).Any() &&
                !GetValidMoveSpaces(boardList, false).Any());
        }

        /// <summary>
        /// Gets a value that indicates whether specified board has pieces in all of its spaces.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <returns>true if the board is filled; otherwise, false.</returns>
        private static bool IsBoardFilled(IList<IList<State>> board)
        {
            var score = GetScore(board);
            return score.PlayerOne + score.PlayerTwo ==
                board.Count * board[0].Count;
        }

        /// <summary>
        /// Gets a value that indicates the winner of the game.
        /// </summary>
        public State Winner
        {
            get { return GetWinner(Board); }
        }

        /// <summary>
        /// Gets a value that indicates the winner of the game represented by the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <returns>The winner.</returns>
        private static State GetWinner(IList<IList<State>> board)
        {
            var score = GetScore(board);
            if (!IsGameOver(board) || score.PlayerOne == score.PlayerTwo) return State.None;
            return (score.PlayerOne > score.PlayerTwo ? State.One : State.Two);
        }

        #endregion score, game over, and winner

        #region move

        /// <summary>
        /// Passes the current move.
        /// </summary>
        /// <returns>An empty list, indicating that there are no spaces affected by the move.</returns>
        public IAsyncOperation<IList<ISpace>> PassAsync()
        {
            return MoveAsync((ISpace)null);
        }

        /// <summary>
        /// Performs a move at the specified board position.
        /// </summary>
        /// <param name="row">The row of the move space.</param>
        /// <param name="column">The column of the move space.</param>
        /// <returns>A list of the spaces affected by the move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        public IAsyncOperation<IList<ISpace>> MoveAsync(int row, int column)
        {
            return MoveAsync(new Space(row, column));
        }

        /// <summary>
        /// Performs a move at the specified board space.
        /// </summary>
        /// <param name="move">The space of the move.</param>
        /// <returns>A list of the spaces affected by the move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        [Windows.Foundation.Metadata.DefaultOverload()]
        public IAsyncOperation<IList<ISpace>> MoveAsync(ISpace move)
        {
            // Use a lock to prevent the ResetAsync method from modifying the game 
            // state at the same time that a different thread is in this method.
            lock (_lockObject)
            {
                return AsyncInfo.Run(cancellationToken => Task.Run(() =>
                {
                    if (cancellationToken.IsCancellationRequested) return null;
                    var changedSpaces = Move(move);
                    SyncMoveStack(move);
                    return changedSpaces;
                }, cancellationToken));
            }
        }

        /// <summary>
        /// Performs a move.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns>A list of the spaces affected by the move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        private IList<ISpace> Move(ISpace move)
        {
            var changedSpaces = new List<ISpace>();

            // If move is a pass, return an empty list. 
            if (move != null)
            {
                var newBoard = (IList<IList<State>>)Move(
                    Board, CurrentPlayer == State.One, move);

                for (int row = 0; row < newBoard.Count; ++row)
                    for (int column = 0; column < newBoard[row].Count; ++column)
                        if (newBoard[row][column] != Board[row][column])
                            changedSpaces.Add(new Space(row, column));
                changedSpaces.Remove(move);
                changedSpaces.Insert(0, move);
                Board = newBoard;
            }

            Moves.Add(move);
            return changedSpaces;
        }

        /// <summary>
        /// Synchronizes the Moves and MoveStack lists if the specified move 
        /// is not already in both lists.
        /// </summary>
        /// <param name="move">The move to check.</param>
        private void SyncMoveStack(ISpace move)
        {
            // If the move diverges from the move stack, synchronize
            // the Moves and MoveStack lists.  
            if (Moves.Count > MoveStack.Count)
            {
                // The move goes beyond the MoveStack, so add 
                // the move to the MoveStack.
                MoveStack.Add(move);
            }
            else
            {
                var stackMove = MoveStack[Moves.Count - 1];
                if ((stackMove == null && stackMove != move) ||
                    (stackMove != null && !stackMove.Equals(move)))
                {
                    // The move is different from same position in
                    // the MoveStack, so reset the MoveStack to match
                    // the Moves list. 
                    MoveStack = new List<ISpace>(Moves);
                }
            }
        }

        /// <summary>
        /// Performs the specified move on the specified board
        /// on behalf of the specified player. 
        /// </summary>
        /// <param name="board">The board to perform the move on.</param>
        /// <param name="player">true if the player is player one; otherwise, false.</param>
        /// <param name="move">The move to perform.</param>
        /// <returns>A new board that reflects the move.</returns>
        private static object Move(object board, bool player, object move)
        {
            if (move == null) return board;

            var boardList = (IList<IList<State>>)board;
            var newBoardList = CloneBoard(boardList);

            var moveSpace = (ISpace)move;
            var playerState = player ? State.One : State.Two;
            var opponentState = player ? State.Two : State.One;

            // Place a piece at the move space.
            newBoardList[moveSpace.Row][moveSpace.Column] = playerState;

            // Flip any captured opponent pieces. 
            foreach (var direction in Directions)
            {
                if (!DoesMoveHaveCaptureInDirection(newBoardList, playerState, moveSpace, direction)) continue;
                var nextRow = moveSpace.Row;
                var nextCol = moveSpace.Column;
                while (IsSpaceOnBoard(newBoardList, new Space(
                    nextRow += direction.Row, nextCol += direction.Column)) &&
                    (newBoardList[nextRow][nextCol] == opponentState))
                {
                    newBoardList[nextRow][nextCol] = playerState;
                }
            }

            return newBoardList;
        }

        /// <summary>
        /// Performs the moves indicated by the specified string.
        /// </summary>
        /// <param name="moves">The moves to perform, in "rc,rc,rc,..." format, 
        /// where each "rc" is the row and column of a move.</param>
        /// <remarks>
        /// This method supports testing specific board states that result from 
        /// a series of moves. However, it does not support 10x10 or larger boards.
        /// </remarks>
        public IAsyncAction MoveAsync(string moves)
        {
            return Task.Run(async () =>
            {
                foreach (var move in moves.Split(','))
                {
                    if (move.Equals("--")) await PassAsync();
                    else
                    {
                        var row = Int32.Parse(move[0].ToString());
                        var column = Int32.Parse(move[1].ToString());
                        await MoveAsync(row, column);
                    }
                }
            }).AsAsyncAction();
        }

        /// <summary>
        /// Performs the moves indicated by the specified list.
        /// </summary>
        /// <param name="moves">The moves to perform.</param>
        /// <returns>An operation that returns a list of the spaces affected by the move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        private IAsyncOperation<IList<ISpace>> MoveAsync(IEnumerable<ISpace> moves)
        {
            return Task.Run(async () =>
            {
                var movesList = moves.ToList();

                // If there are no moves (for example, after undoing all moves),
                // then return an empty list. 
                if (movesList.Count == 0) return new List<ISpace>();

                // Perform each move and return the affected spaces only for the last move.
                foreach (var move in movesList.Take(movesList.Count - 1)) await MoveAsync(move);
                return await MoveAsync(movesList.Last());
            }).AsAsyncOperation();
        }

        #endregion move

        #region undo and redo

        /// <summary>
        /// Returns the game to the state it was in after the specified number of turns.
        /// </summary>
        /// <param name="turnCount">The number of turns.</param>
        /// <returns>A list of the spaces affected by the last move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        private IAsyncOperation<IList<ISpace>> ResetAsync(int turnCount)
        {
            if (turnCount < 0 || turnCount > MoveStack.Count)
                throw new ArgumentException("must be 0 or greater and " +
                    "no higher than MoveStack.Count.", "turnCount");

            if (turnCount == Moves.Count)
                throw new ArgumentException("must be different than " +
                    "Moves.Count.", "turnCount");

            // Use a lock to prevent this method from modifying the game state at 
            // the same time a different thread is in the locked MoveAsync method.
            lock (_lockObject)
            {
                IList<ISpace> newMoves;
                if (turnCount < Moves.Count)
                {
                    // Backward navigation resets the board to its initial state in 
                    // preparation for replaying all moves up to the indicated move.
                    SetUpEmptyBoard();
                    newMoves = Moves.Take(turnCount).ToList();
                    Moves.Clear();
                }
                else
                {
                    // Forward navigation retrieves previously-undone moves in 
                    // preparation for their replay from the current board state.
                    newMoves = MoveStack.Skip(Moves.Count)
                        .Take(turnCount - Moves.Count).ToList();
                }
                return MoveAsync(newMoves);
            }
        }

        /// <summary>
        /// Returns the game to the state it was in after the last move. 
        /// </summary>
        /// <returns>A list of the spaces affected by the move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        public IAsyncOperation<IList<ISpace>> UndoAsync()
        {
            return ResetAsync(Moves.Count - 1);
        }

        /// <summary>
        /// Returns the game to the state it was in after the next move. 
        /// </summary>
        /// <returns>A list of the spaces affected by the move, starting with the space 
        /// of the move and including the spaces of all the pieces captured by the move.</returns>
        public IAsyncOperation<IList<ISpace>> RedoAsync()
        {
            return ResetAsync(Moves.Count + 1);
        }

        #endregion undo and redo

        #region AI

        /// <summary>
        /// Performs an AI move using the specified search depth.
        /// </summary>
        /// <param name="searchDepth">The AI search depth to use.</param>
        /// <remarks>This is a convenience method for use by unit tests, so
        /// it does not have a return value and does not support cancellation. 
        /// There is no version of AiMoveAsync that is used the same way as MoveAsync
        /// because with shallow search depths, there would be no time for cancellation,
        /// making it impossible to undo a move that comes before an AI move. 
        /// Instead, client apps should call GetBestMoveAsync and then, after an 
        /// appropriate delay, pass the return value to MoveAsync. For more info,
        /// see the GameViewModel.AiMoveAsync method.</remarks>
        public IAsyncAction AiMoveAsync(int searchDepth)
        {
            return Task.Run(async () => 
            {
                // If it is the AI's turn and we're not at the end of the move stack,
                // just use the next move in the stack. This is necessary to preserve
                // the forward stack, but it also prevents the AI from having to search again. 
                var bestMove = Moves.Count < MoveStack.Count ? 
                    MoveStack[Moves.Count] : await GetBestMoveAsync(searchDepth);
                await MoveAsync(bestMove);
            }).AsAsyncAction();
        }

        /// <summary>
        /// Gets the best move that the AI can find for the specified search depth.
        /// </summary>
        /// <param name="searchDepth">The AI search depth to use.</param>
        /// <returns>An operation that returns the space of the move.</returns>
        public IAsyncOperation<ISpace> GetBestMoveAsync(int searchDepth)
        {
            if (searchDepth < 1) throw new ArgumentException(
                "must be 1 or greater.", "searchDepth");

            return AsyncInfo.Run(cancellationToken => Task.Run(() => 
            {
                return (ISpace)reversiAI.GetBestMove(Board, 
                    CurrentPlayer == State.One, searchDepth, cancellationToken);
            }, cancellationToken));
        }

        /// <summary>
        /// A MiniMax object initialized with Reversi game rules. 
        /// </summary>
        private MiniMax reversiAI = new MiniMax()
        {
            GetValidMoves = GetValidMoveSpaces, 
            Move = Move,
            IsGameOver = IsGameOver, 
            GetBoardValue = GetBoardValue
        };

        /// <summary>
        /// Gets the value of the specified game board.
        /// </summary>
        /// <param name="board">The game board to assess.</param>
        /// <param name="playerState">The player for whom to assess the game board.</param>
        /// <returns>The value of the game board from the perspective of the specified player.</returns>
        /// <remarks>
        /// This method encapsulates the Reversi-specific heuristics, and would be replaced 
        /// for other games. The AI code higher in the call stack is generic and can be used 
        /// as the basis of an AI implementation for any two-player, zero-sum game. 
        /// </remarks>
        private static int GetBoardValue(object board, bool player)
        {
            var playerState = player ? State.One : State.Two;
            var boardList = (IList<IList<State>>)board;
            var scoreValue = GetScoreDifference(boardList, playerState);
            var cornersValue = GetCornersValue(boardList, playerState);
            var winValue = GetWinValue(boardList, playerState);
            return scoreValue + cornersValue + winValue;
        }

        /// <summary>
        /// Gets the difference in score for the specified game board.
        /// </summary>
        /// <param name="board">The game board to assess.</param>
        /// <param name="player">The player for whom to assess the game board.</param>
        /// <returns>The score difference of the game board from the perspective of the specified player;
        /// that is, a positive value if the player is winning, a negative value if the player is losing.</returns>
        private static int GetScoreDifference(IList<IList<State>> board, State player)
        {
            var score = GetScore(board);
            return player == State.One ?
                score.PlayerOne - score.PlayerTwo :
                score.PlayerTwo - score.PlayerOne;
        }

        /// <summary>
        /// Gets the value contributed by the corner positions of the specified board.
        /// </summary>
        /// <param name="board">The game board to assess.</param>
        /// <param name="player">The player for whom to assess the game board.</param>
        /// <returns>The value of the corner positions from the perspective of the specified player;
        /// that is, a positive value for the corners owned by the player, and a negative value for
        /// the corners owned by the player's opponent.</returns>
        public static int GetCornersValue(IList<IList<State>> board, State player)
        {
            var rowCount = board.Count;
            var columnCount = board[0].Count;

            // Use a corner value a little higher than the maximum possible 
            // move value. This is just an arbitrary guess at the actual value.
            // This value means that a series of high-value moves not including 
            // the corner could produce a value higher than a corner move. 
            var shorter = Math.Min(rowCount, columnCount);
            var longer = Math.Max(rowCount, columnCount);
            var cornerValue = shorter * 2 + longer;

            State[] cornerStates = { board[0][0], board[0][columnCount - 1],
                board[rowCount - 1][0], board[rowCount - 1][columnCount - 1] };

            return cornerStates.Where(state => state != State.None)
                .Sum(state => (cornerValue * (state == player ? 1 : -1)));
        }

        /// <summary>
        /// Gets the value of a winning game board.
        /// </summary>
        /// <param name="board">The game board to assess.</param>
        /// <param name="player">The player for whom to assess the game board.</param>
        /// <returns>The value of a win from the perspective of the specified player;
        /// that is, a positive value if the specified player has won, and a 
        /// negative value if the player's opponent has won. </returns>
        private static int GetWinValue(IList<IList<State>> board, State player)
        {
            if (!IsGameOver(board)) return 0;
            return Int32.MaxValue / 2 * (GetWinner(board) == player ? 1 : -1);
        }

        #endregion AI

        #region move validation

        /// <summary>
        /// Gets a value that indicates whether a move to the specified board position is legal.
        /// </summary>
        /// <param name="row">The row of the move space.</param>
        /// <param name="column">The column of the move space.</param>
        /// <returns>True if the move is legal; otherwise, false.</returns>
        public bool IsValidMove(int row, int column)
        {
            return IsValidMove(new Space(row, column));
        }

        /// <summary>
        /// Gets a value that indicates whether a move is legal.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns>True if the move is legal; otherwise, false.</returns>
        public bool IsValidMove(ISpace move)
        {
            return move != null ?
                IsValidMoveSpace(Board, CurrentPlayer, move) :
                IsPassValid(Board, CurrentPlayer);
        }

        /// <summary>
        /// Gets a value that indicates whether a pass move is legal in the current game.
        /// </summary>
        /// <returns>True if a pass move is legal; otherwise, false.</returns>
        public bool IsPassValid()
        {
            return IsPassValid(Board, CurrentPlayer);
        }

        /// <summary>
        /// Gets a value that indicates whether a pass move is legal for the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="player">The player whose turn it is for the specified board.</param>
        /// <returns>True if a pass move is legal for the given board; otherwise, false.</returns>
        private bool IsPassValid(IList<IList<State>> board, State player)
        {
            return
                !IsGameOver(board) &&
                !GetValidMoveSpaces(board, player == State.One).Any();
        }

        /// <summary>
        /// Gets a list of the legal moves available for the specified game board and player.
        /// </summary>
        /// <param name="board">The game board to assess.</param>
        /// <param name="player">The player whose turn it is for the specified board.</param>
        /// <returns>The list of legal moves.</returns>
        private static IEnumerable<object> GetValidMoveSpaces(
            object board, bool player)
        {
            var boardList = (IList<IList<State>>)board;
            return GetAllMoveSpaces(boardList.Count, boardList[0].Count).Where(move => 
                IsValidMoveSpace(boardList, player ? State.One : State.Two, move));
        }

        /// <summary>
        /// Gets a value that indicates whether the specified move is a legal for the given player and game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="player">The player whose turn it is for the specified board.</param>
        /// <param name="move">The move to assess.</param>
        /// <returns>True if the move is legal; otherwise, false.</returns>
        private static bool IsValidMoveSpace(
            IList<IList<State>> board, State player, ISpace move)
        {
            return
                IsSpaceOnBoard(board, move) &&
                IsSpaceUnoccupied(board, move) &&
                DoesMoveHaveCapture(board, player, move);
        }

        /// <summary>
        /// Gets a value that indicates whether a space is within the bounds of the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="space">The space to assess.</param>
        /// <returns>True if the space is within the bounds of the game board; otherwise, false.</returns>
        private static bool IsSpaceOnBoard(
            IList<IList<State>> board, ISpace space)
        {
            return space.Row >= 0 && space.Row < board.Count &&
                space.Column >= 0 && space.Column < board[0].Count;
        }

        /// <summary>
        /// Gets a value that indicates whether a space is empty on the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="space">The space to assess.</param>
        /// <returns>True if the space is empty; otherwise, false.</returns>
        private static bool IsSpaceUnoccupied(
            IList<IList<State>> board, ISpace space)
        {
            return board[space.Row][space.Column] == State.None;
        }

        /// <summary>
        /// Gets a value that indicates whether a move has any captures on the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="player">The player whose turn it is for the specified board.</param>
        /// <param name="move">The move to assess.</param>
        /// <returns>True if the move has captures; otherwise, false.</returns>
        private static bool DoesMoveHaveCapture(
            IList<IList<State>> board, State player, ISpace move)
        {
            return Directions.Any(direction =>
                DoesMoveHaveCaptureInDirection(board, player, move, direction));
        }

        /// <summary>
        /// Gets a value that indicates whether a move has any captures in a particular direction 
        /// on the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="player">The player whose turn it is for the specified board.</param>
        /// <param name="move">The move to assess.</param>
        /// <param name="direction">The direction to assess (as a space with a relative position).</param>
        /// <returns>True if the move has any captures in the given direction; otherwise, false.</returns>
        private static bool DoesMoveHaveCaptureInDirection(
            IList<IList<State>> board, State player,
            ISpace move, ISpace direction)
        {
            bool isFirstState = true;

            // Check the spaces in the given direction looking for a capture 
            // or for conditions indicating that no capture is possible. 
            // Halting the iteration as early as possible avoids the full 
            // enumeration of GetStatesInDirection, saving some time. 
            foreach (var state in GetStatesInDirection(board, move, direction))
            {
                // Found an empty space, so there is no capture. 
                if (state == State.None) return false;

                // Found a space with the player's piece. If it's the first space,
                // then there is no capture; otherwise, there is a capture. 
                if (state == player) return !isFirstState;

                isFirstState = false;
            }

            // All the spaces in the direction are occupied by the opponent's pieces. 
            return false;
        }

        /// <summary>
        /// Gets a list of the states of spaces in a given direction from a particular space for 
        /// the specified game board.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="start">The space.</param>
        /// <param name="direction">The direction to assess (as a space with a relative position).</param>
        /// <returns>A list of space states for spaces in the specified direction from the given start space.</returns>
        private static IEnumerable<State> GetStatesInDirection(
            IList<IList<State>> board, ISpace start, ISpace direction)
        {
            var nextRow = start.Row;
            var nextCol = start.Column;
            while ((nextRow += direction.Row) >= 0 &&
                nextRow < board.Count &&
                (nextCol += direction.Column) >= 0 &&
                nextCol < board[0].Count)
                yield return board[nextRow][nextCol];
        }

        /// <summary>
        /// Gets a list of all the spaces for a game board with the specified dimensions.
        /// </summary>
        /// <param name="rowCount">The number of rows.</param>
        /// <param name="columnCount">The number of columns.</param>
        /// <returns>The list of spaces.</returns>
        /// <remarks>
        /// This method creates lists for particular board sizes on demand, then
        /// reuses the lists as needed.
        /// </remarks>
        private static IEnumerable<ISpace> GetAllMoveSpaces(int rowCount, int columnCount)
        {
            var boardDimensions = Tuple.Create(rowCount, columnCount);
            return AllSpaces.ContainsKey(boardDimensions) ?
                AllSpaces[boardDimensions] : AllSpaces[boardDimensions] =
                    from row in Enumerable.Range(0, rowCount)
                    from col in Enumerable.Range(0, columnCount)
                    select new Space(row, col);
        }

        #endregion move validation

        #region miscellaneous

        /// <summary>
        /// Gets a copy of the specified game board.
        /// </summary>
        /// <param name="board">The game board to copy.</param>
        /// <returns>The copy of the game board.</returns>
        private static IList<IList<State>> CloneBoard(IList<IList<State>> board)
        {
            var boardClone = new State[board.Count][];
            foreach (var row in Enumerable.Range(0, board.Count))
            {
                boardClone[row] = new State[board[row].Count];
                board[row].CopyTo(boardClone[row], 0);
            }
            return boardClone;
        }

        /// <summary>
        /// Initializes the Board property using the specified state string.
        /// </summary>
        /// <param name="state">A string that represents a board state.</param>
        /// <remarks>
        /// This method is used by unit tests to confirm various behaviors for specific board states.
        /// The state string includes one number for each space on the board, 
        /// with 0 representing an empty space, and 1 or 2 representing player 1 or 2.
        /// </remarks>
        public void LoadSerializedBoardState(string state)
        {
            foreach (var row in Enumerable.Range(0, RowCount))
            {
                foreach (var column in Enumerable.Range(0, ColumnCount))
                {
                    Board[row][column] = (State)Int32.Parse(
                        state[row * ColumnCount + column].ToString());
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the current game board.
        /// </summary>
        /// <returns>A string that represents the board state.</returns>
        /// <remarks>
        /// The return value includes one number for each space on the board, 
        /// with 0 representing an empty space, and 1 or 2 representing player 1 or 2.
        /// </remarks>
        public sealed override string ToString()
        {
            var state = new StringBuilder(RowCount * ColumnCount);
            foreach (var row in Enumerable.Range(0, RowCount))
            {
                foreach (var column in Enumerable.Range(0, ColumnCount))
                {
                    state.Append(((int)Board[row][column]).ToString());
                }
            }
            return state.ToString();
        }

        /// <summary>
        /// A dictionary that holds lists of spaces for different board sizes.
        /// </summary>
        private static readonly Dictionary<Tuple<int, int>, IEnumerable<ISpace>>
            AllSpaces = new Dictionary<Tuple<int, int>, IEnumerable<ISpace>>();

        /// <summary>
        /// A list of spaces with relative positions that represent the eight 
        /// directions (horizontal, vertical, and diagonal) from a given space.
        /// </summary>
        private static readonly Space[] Directions = new[] {
            new Space(-1, -1), new Space(-1, 0), 
            new Space(-1, 1), new Space(0, -1), 
            new Space(0, 1), new Space(1, -1), 
            new Space(1, 0), new Space(1, 1)
        };

        // Miscellaneous fields. 
        private static readonly Random _random = new Random();
        private static readonly object _lockObject = new object();
        private List<ISpace> _moves;
        private List<ISpace> _moveStack;

        #endregion miscellaneous
    }
}
