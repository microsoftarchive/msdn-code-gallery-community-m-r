using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Reversi.Models;
using ReversiGameModel;
using System;
using System.Threading.Tasks;

namespace ReversiGameComponentTests
{
    [TestClass]
    public class BasicFunctionalityTests
    {
        [TestMethod]
        [TestCategory("basics")]
        public void GameBoardHasCorrectInitialState()
        {
            var game = GameFactory.GetGame();
            Assert.AreEqual(State.One, game.Board[3][3]);
            Assert.AreEqual(State.One, game.Board[4][4]);
            Assert.AreEqual(State.Two, game.Board[3][4]);
            Assert.AreEqual(State.Two, game.Board[4][3]);

            var game2 = GameFactory.GetGame(32, 32);
            Assert.AreEqual(State.One, game2.Board[15][15]);
            Assert.AreEqual(State.One, game2.Board[16][16]);
            Assert.AreEqual(State.Two, game2.Board[15][16]);
            Assert.AreEqual(State.Two, game2.Board[16][15]);
        }

        [TestMethod]
        [TestCategory("basics")]
        public void CurrentPlayerReflectsNumberOfMoves()
        {
            var game = GameFactory.GetGame();

            Assert.AreEqual(0, game.Moves.Count);
            Assert.AreEqual(State.One, game.CurrentPlayer);

            var move = new Space(3, 5);
            Assert.IsTrue(game.IsValidMove(move));
            game.MoveAsync(move).AsTask().Wait();

            Assert.AreEqual(1, game.Moves.Count);
            Assert.AreEqual(State.Two, game.CurrentPlayer);
        }

        [TestMethod]
        [TestCategory("basics")]
        public void PassesAreAddedToMovesList()
        {
            var game = GameFactory.GetGame();
            Assert.IsFalse(game.IsPassValid());
            game.PassAsync().AsTask().Wait();
            Assert.AreEqual(1, game.Moves.Count);
            game.PassAsync().AsTask().Wait();
            Assert.AreEqual(2, game.Moves.Count);
        }

        [TestMethod]
        [TestCategory("basics")]
        public void GameIsOverOnlyWhenNeitherPlayerCanMoveOrPass()
        {
            var game1 = GameFactory.GetGame(2, 2);
            Assert.IsTrue(game1.IsGameOver());

            var game2 = GameFactory.GetGame(2, 4);
            game2.LoadSerializedBoardState(
                "0122" +
                "1212");
            Assert.AreEqual(0, game2.Moves.Count);
            Assert.IsTrue(game2.IsPassValid());
            game2.PassAsync().AsTask().Wait();
            Assert.AreEqual(1, game2.Moves.Count);
            Assert.AreEqual(null, game2.Moves[0]);
            Assert.IsFalse(game2.IsGameOver());
            Assert.IsFalse(game2.IsPassValid());
            game2.MoveAsync(0, 0).AsTask().Wait();
            Assert.AreEqual(2, game2.Moves.Count);
            Assert.AreEqual(new Space(0, 0), game2.Moves[1]);
            Assert.IsFalse(game2.IsPassValid());
            Assert.IsTrue(game2.IsGameOver());
        }

        [TestMethod]
        [TestCategory("basics")]
        public void GameHasWinnerOnlyWhenItIsOver()
        {
            var game = GameFactory.GetGame(4, 4);
            game.MoveAsync("13,01,00,03,02,23,33").AsTask().Wait();
            Assert.AreEqual(7, game.Moves.Count);
            var score = game.GetScore();
            Assert.AreEqual(7, score.PlayerOne);
            Assert.AreEqual(4, score.PlayerTwo);
            Assert.IsFalse(game.IsGameOver());
            Assert.AreEqual(State.None, game.Winner);
            game.MoveAsync("31,32,10,30,--,20").AsTask().Wait();
            Assert.AreEqual(13, game.Moves.Count);
            score = game.GetScore();
            Assert.AreEqual(12, score.PlayerOne);
            Assert.AreEqual(4, score.PlayerTwo);
            Assert.IsTrue(game.IsGameOver());
            Assert.AreEqual(State.One, game.Winner);
            Assert.IsFalse(game.IsValidMove(null));
        }

        [TestMethod]
        [TestCategory("basics")]
        public void UndoAndRedoWorkCorrectly()
        {
            var game = GameFactory.GetGame(4, 4);
            game.MoveAsync("13,01,00,03,02,23").AsTask().Wait();
            Assert.AreEqual(6, game.Moves.Count);

            // Confirm that undoing or redoing a move restores the board
            // to its previous or next state.
            var boardState1 = game.ToString();
            game.MoveAsync(3, 3).AsTask().Wait();
            var boardState2 = game.ToString();
            Assert.AreEqual(7, game.Moves.Count);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(6, game.Moves.Count);
            Assert.AreEqual(boardState1, game.ToString());
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(7, game.Moves.Count);
            Assert.AreEqual(boardState2, game.ToString());

            // Undo several times, confirming the move count
            // and the last move after the undo. 
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(6, game.Moves.Count);
            Assert.AreEqual(new Space(2, 3), game.Moves[game.Moves.Count - 1]);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(5, game.Moves.Count);
            Assert.AreEqual(new Space(0, 2), game.Moves[game.Moves.Count - 1]);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(4, game.Moves.Count);
            Assert.AreEqual(new Space(0, 3), game.Moves[game.Moves.Count - 1]);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(3, game.Moves.Count);
            Assert.AreEqual(new Space(0, 0), game.Moves[game.Moves.Count - 1]);

            // Now redo a few times and confirm that the 
            // moves list is restored.
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(4, game.Moves.Count);
            Assert.AreEqual(new Space(0, 3), game.Moves[game.Moves.Count - 1]);
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(5, game.Moves.Count);
            Assert.AreEqual(new Space(0, 2), game.Moves[game.Moves.Count - 1]);

            // Now undo and make a diverging move, then
            // confirm that the move stack is reset.
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(4, game.Moves.Count);
            Assert.AreEqual(new Space(0, 3), game.Moves[game.Moves.Count - 1]);
            Assert.AreEqual(7, game.MoveStack.Count);
            game.MoveAsync(2, 0).AsTask().Wait();
            Assert.AreEqual(5, game.MoveStack.Count);
            Assert.AreEqual(5, game.Moves.Count);
            Assert.AreEqual(new Space(2, 0), game.Moves[game.Moves.Count - 1]);

            // Confirm that the reset move stack works
            // as normal, and undo/redo still works. 
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(4, game.Moves.Count);
            Assert.AreEqual(new Space(0, 3),
                game.Moves[game.Moves.Count - 1]);
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(5, game.Moves.Count);
            Assert.AreEqual(new Space(2, 0),
                game.Moves[game.Moves.Count - 1]);

            // Now back up three moves and confirm that making
            // a new move identical to a redo move is the same
            // as performing a redo, but making a different move
            // resets the move stack. 
            game.UndoAsync().AsTask().Wait();
            game.UndoAsync().AsTask().Wait();
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(2, game.Moves.Count);
            Assert.AreEqual(new Space(0, 1),
                game.Moves[game.Moves.Count - 1]);
            Assert.AreEqual(5, game.MoveStack.Count);
            game.MoveAsync(0, 0).AsTask().Wait();
            Assert.AreEqual(3, game.Moves.Count);
            Assert.AreEqual(5, game.MoveStack.Count);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(2, game.Moves.Count);
            game.MoveAsync(0, 3).AsTask().Wait();
            Assert.AreEqual(3, game.Moves.Count);
            Assert.AreEqual(3, game.MoveStack.Count);
        }

        [TestMethod]
        [TestCategory("basics")]
        public void UndoAndRedoWorkWithPassMoves()
        {
            var game = GameFactory.GetGame(4, 4);
            game.PassAsync().AsTask().Wait();
            Assert.AreEqual(1, game.Moves.Count);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(0, game.Moves.Count);
            game.PassAsync().AsTask().Wait();
            Assert.AreEqual(1, game.Moves.Count);
            game.PassAsync().AsTask().Wait();
            Assert.AreEqual(2, game.Moves.Count);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(1, game.Moves.Count);
            game.UndoAsync().AsTask().Wait();
            Assert.AreEqual(0, game.Moves.Count);
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(1, game.Moves.Count);
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(2, game.Moves.Count);
        }

        [TestMethod]
        [TestCategory("basics")]
        public void UndoAndRedoWorkCorrectlyWithGameEnd()
        {
            // this should be a view model (or automation) test since the issue is in the UI

            // need to test that you can undo from the end (game over),
            // and after doing so, you can redo back to the end.

            var game = GameFactory.GetGame(4, 4);
            game.MoveAsync("13,01,00,03,02,23,33,31,32,10,30,--,20").AsTask().Wait();
            Assert.IsTrue(game.IsGameOver());
            Assert.AreEqual(13, game.Moves.Count);
            game.UndoAsync().AsTask().Wait();
            Assert.IsFalse(game.IsGameOver());
            Assert.AreEqual(12, game.Moves.Count);
            game.RedoAsync().AsTask().Wait();
            Assert.IsTrue(game.IsGameOver());
            Assert.AreEqual(13, game.Moves.Count);
            game.UndoAsync().AsTask().Wait();
            game.UndoAsync().AsTask().Wait();
            Assert.IsFalse(game.IsGameOver());
            Assert.AreEqual(11, game.Moves.Count);
            Assert.IsTrue(game.IsPassValid());
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(12, game.Moves.Count);
            Assert.IsFalse(game.IsGameOver());
            game.RedoAsync().AsTask().Wait();
            Assert.AreEqual(13, game.Moves.Count);
            Assert.IsTrue(game.IsGameOver());
        }

        [TestMethod]
        [TestCategory("basics")]
        public void PerformingAMoveReturnsAffectedSpaces()
        {
            var game = GameFactory.GetGame(4, 4);
            var moveTask = game.MoveAsync(1, 3).AsTask<System.Collections.Generic.IList<ISpace>>();
            moveTask.Wait();
            var affectedSpaces = moveTask.Result;
            Assert.AreEqual(2, affectedSpaces.Count);
            Assert.AreEqual(new Space(1, 3), affectedSpaces[0]);
            Assert.AreEqual(new Space(1, 2), affectedSpaces[1]);
        }

    }
}
