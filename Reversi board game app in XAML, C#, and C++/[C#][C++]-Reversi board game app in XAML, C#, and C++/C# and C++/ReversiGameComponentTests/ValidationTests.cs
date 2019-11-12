using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Reversi.Models;
using ReversiGameModel;
using System;
using System.Threading.Tasks;

namespace ReversiGameComponentTests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        [TestCategory("validation")]
        public void AMoveOutsideTheBoardFailsValidation()
        {
            var game = GameFactory.GetGame();

            // Set up a board situation that would result in a legal move
            // if the board were large enough to accommodate the move.
            game.Board[6][6] = game.CurrentPlayer;
            game.Board[7][7] = game.CurrentOpponent;

            // Confirm that the validation routine returns false.
            Assert.IsFalse(game.IsValidMove(8, 8));
        }

        [TestMethod]
        [TestCategory("validation")]
        public void AMoveOnAnOccupiedSpaceFailsValidation()
        {
            var game = GameFactory.GetGame(4, 4);
            Assert.AreEqual(State.One, game.Board[1][1]);
            Assert.IsFalse(game.IsValidMove(1, 1));

            // Confirm that a move on an occupied space that would capture
            // is also invalid.
            game.MoveAsync("13,23,31").AsTask().Wait();
            Assert.AreEqual(State.One, game.Board[2][1]);
            Assert.IsFalse(game.IsValidMove(2, 1));
        }

        [TestMethod]
        [TestCategory("validation")]
        public void AValidMoveFlipsOpponentPieces()
        {
            var game = GameFactory.GetGame();
            Assert.AreEqual(State.One, game.CurrentPlayer);
            Assert.AreEqual(State.Two, game.Board[3][4]);

            // Test a few illegal moves.
            Assert.IsFalse(game.IsValidMove(0, 0));
            Assert.IsFalse(game.IsValidMove(3, 2));

            // Test legal move.
            var move = new Space(3, 5);
            Assert.IsTrue(game.IsValidMove(move));
            game.MoveAsync(move).AsTask().Wait();
            Assert.AreEqual(State.Two, game.CurrentPlayer);
            Assert.AreEqual(State.One, game.Board[3][4]);
        }

        [TestMethod]
        [TestCategory("validation")]
        public void PassIsValidOnlyWhenNoMovesAvailableAndGameIsNotOver()
        {
            var game1 = GameFactory.GetGame(2, 2);
            Assert.IsFalse(game1.IsPassValid());

            var game2 = GameFactory.GetGame(2, 4);
            game2.LoadSerializedBoardState(
                "0122" +
                "1212");
            Assert.IsTrue(game2.IsPassValid());
            Assert.IsFalse(game2.IsGameOver());
            game2.PassAsync().AsTask().Wait();
            Assert.IsFalse(game2.IsPassValid());
            Assert.IsFalse(game2.IsGameOver());
            game2.MoveAsync(0, 0).AsTask().Wait();
            Assert.IsFalse(game2.IsPassValid());
            Assert.IsTrue(game2.IsGameOver());
        }
    }
}
