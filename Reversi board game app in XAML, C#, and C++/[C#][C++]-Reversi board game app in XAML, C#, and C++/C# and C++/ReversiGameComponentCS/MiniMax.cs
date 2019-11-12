using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ReversiGameComponentCS
{
    /// <summary>
    /// Provides a generalized MiniMax algorithm.
    /// </summary>
    public class MiniMax
    {
        /// <summary>
        /// Gets or sets a function that gets the valid moves 
        /// for a given game board and player.
        /// </summary>
        /// <remarks>
        /// The function parameters represent the board, the player
        /// (where true is player one and false is player two), and the
        /// returned list of valid moves. 
        /// </remarks>
        public Func<object, bool, IEnumerable<object>> GetValidMoves { get; set; }

        /// <summary>
        /// Gets or sets a function that performs a move on a given 
        /// board for a given player, returning an updated board.
        /// </summary>
        /// <remarks>
        /// The function parameters represent the board, the player
        /// (where true is player one and false is player two), the move,
        /// and the returned board. 
        /// </remarks>
        public Func<object, bool, object, object> Move { get; set; }

        /// <summary>
        /// Gets or sets a function that determines whether 
        /// the given board represents a completed game. 
        /// </summary>
        /// <remarks>
        /// The function parameters represent the board and the return value.
        /// </remarks>
        public Func<object, bool> IsGameOver { get; set; }

        /// <summary>
        /// Gets or sets a function that determines the value of
        /// the given board from the perspective of the given player.
        /// </summary>
        /// <remarks>
        /// The function parameters represent the board, the player, 
        /// and the return value.
        /// </remarks>
        public Func<object, bool, int> GetBoardValue { get; set; }

        /// <summary>
        /// Gets the best move for the given board by searching
        /// the game tree to the given depth.
        /// </summary>
        /// <param name="board">The board to check.</param>
        /// <param name="player">The player to evaluate the moves for.</param>
        /// <param name="searchDepth">The game-tree depth to search.</param>
        /// <param name="cancellationToken">An optional token used for cancelling the search.</param>
        /// <returns>The best move (or one of the best moves, if multiple moves tie for best).</returns>
        public Object GetBestMove(Object board, bool player,
            int searchDepth, CancellationToken? cancellationToken)
        {   
            var moveEvaluations = GetMoveEvaluations(
                board, player, searchDepth, cancellationToken).ToArray();
            var bestValue = moveEvaluations.Max(eval => eval.Item2);
            var bestMoves =
                (from moveEvaluation in moveEvaluations
                 where moveEvaluation.Item2 == bestValue
                 select moveEvaluation.Item1).ToArray();

            // If there are multiple, equivalent best moves, 
            // select one at random. 
            return bestMoves[_random.Next(bestMoves.Length)];
        }
        private static readonly Random _random = new Random();

        /// <summary>
        /// Gets a list of move and value pairs for the given board by searching
        /// the game tree to the given depth.
        /// </summary>
        /// <param name="board">The board to check.</param>
        /// <param name="player">The player to evaluate the moves for.</param>
        /// <param name="searchDepth">The game-tree depth to search.</param>
        /// <param name="cancellationToken">An optional token used for cancelling the search.</param>
        /// <param name="maxValue">The best value found in previous iterations.</param>
        /// <returns>The list of move and value pairs.</returns>
        private IEnumerable<Tuple<Object, int>> GetMoveEvaluations(
            Object board, bool player, int searchDepth,
            CancellationToken? cancellationToken, int maxValue = Int32.MaxValue)
        {
            // Must use negative MaxValue instead of MinValue
            // because MinValue * -1 > MaxValue.
            var maxSoFar = -Int32.MaxValue;
            var validMoveSpaces = GetValidMoves(board, player).ToArray();

            Func<object, Tuple<object, int>> getEval = move => Tuple.Create(move,
                GetMoveValue(board, player, move, searchDepth, -maxSoFar, cancellationToken));

            // Pass is the only valid move.
            if (!validMoveSpaces.Any()) yield return getEval(null);
            else
            {
                var evaluations = validMoveSpaces.Select(move => getEval(move));
                foreach (var evaluation in evaluations)
                {
                    // Prune the list so that it includes only the best moves
                    // discovered so far. 

                    // The current result is over the maximum value.
                    // This means that the opponent has at least one
                    // move that will result in a better outcome by avoiding 
                    // this branch completely. Therefore, there is no 
                    // point in searching it further, so stop searching
                    // and yield the evaluation. (This is known as 
                    // "alpha-beta pruning".)
                    if (evaluation.Item2 > maxValue)
                    {
                        yield return evaluation;
                        break;
                    }

                    // The current result is worse than one the current player
                    // has already found, so skip it.
                    if (evaluation.Item2 < maxSoFar) continue;

                    maxSoFar = Math.Max(evaluation.Item2, maxSoFar);
                    yield return evaluation;
                }
            }
        }

        /// <summary>
        /// Gets the value of the given move on the given board for the
        /// given player as heuristically determined by searching to the
        /// given depth in the game tree.
        /// </summary>
        /// <param name="board">The board to check.</param>
        /// <param name="player">The player to evaluate the move for.</param>
        /// <param name="move">The move to check.</param>
        /// <param name="searchDepth">The game-tree depth to search.</param>
        /// <param name="maxValue">The best value found in previous iterations.</param>
        /// <param name="cancellationToken">An optional token used for cancelling the search.</param>
        /// <returns>The value of the move.</returns>
        private int GetMoveValue(Object board, bool player, Object move, 
            int searchDepth, int maxValue, CancellationToken? cancellationToken)
        {
            if (cancellationToken.HasValue)
                cancellationToken.Value.ThrowIfCancellationRequested();

            // GetMoveValue is only called with a cloned board that was created in GetMoveEvaluations.
            // The changes that Move makes are to that copy, not to the Board property value.
            // Therefore, the return value changedSpaces is ignored because we
            // haven't made a move on the game board yet and changedSpaces is only
            // relevant in that context.
            board = Move(board, player, move);

            // Decrement the searchDepth then check whether the search is over.
            if (--searchDepth == 0 || IsGameOver(board))
                return GetBoardValue(board, player);

            // Return the opposite of the opponent's best move value.
            var opponent = !player;
            var moveEvaluations = GetMoveEvaluations(
                board, opponent, searchDepth, cancellationToken, maxValue);
            return -moveEvaluations.Max(eval => eval.Item2);
        }
    }
}
