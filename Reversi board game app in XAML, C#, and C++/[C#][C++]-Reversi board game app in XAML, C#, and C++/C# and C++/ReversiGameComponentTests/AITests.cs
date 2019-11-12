using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Linq;
using System.Threading.Tasks;
using ReversiGameModel;
using Reversi.Models;

namespace ReversiGameComponentTests
{
    [TestClass]
    public class AiTests
    {
        [TestMethod]
        [TestCategory("ai")]
        public void AiMakesOnlyPossibleMove()
        {
            var game = GameFactory.GetGame(2, 4);
            game.MoveAsync("03").AsTask().Wait();
            Assert.AreEqual(1, game.Moves.Count);
            Assert.AreEqual(State.None, game.Board[1][3]);
            game.AiMoveAsync(1).AsTask().Wait();
            Assert.AreEqual(2, game.Moves.Count);
            Assert.AreEqual(State.Two, game.Board[1][3]);
        }

        [TestMethod]
        [TestCategory("ai")]
        public void AiMakesBestMoveWithNoDepthSearch()
        {
            var game1 = GameFactory.GetGame(4, 4);
            game1.LoadSerializedBoardState(
                "0000" +
                "0221" +
                "0020" +
                "0000");
            Assert.AreEqual(State.None, game1.Board[1][0]);
            game1.AiMoveAsync(1).AsTask().Wait();
            Assert.AreEqual(State.One, game1.Board[1][0]);

            var game2 = GameFactory.GetGame(5, 4);
            game2.LoadSerializedBoardState(
                "0000" +
                "0100" +
                "2222" +
                "0221" +
                "2000");
            Assert.AreEqual(State.None, game2.Board[1][3]);
            game2.AiMoveAsync(1).AsTask().Wait();

            // Confirm that the only bad move hasn't been made. 
            Assert.AreEqual(State.None, game2.Board[1][3]);
        }

        [TestMethod]
        [TestCategory("ai")]
        public void AiMakesBestMoveWithDepthSearch()
        {
            // Depth 1 will make the move that captures two opponent pieces.
            var game1 = GameFactory.GetGame(4, 6);
            game1.LoadSerializedBoardState(
                "000000" +
                "012202" +
                "002000" +
                "000000");
            Assert.AreEqual(State.None, game1.Board[1][4]);
            game1.AiMoveAsync(1).AsTask().Wait();
            Assert.AreEqual(State.One, game1.Board[1][4]);

            // Depth 2 will make the move that captures one opponent piece.
            var game2 = GameFactory.GetGame(4, 6);
            game2.LoadSerializedBoardState(
                "000000" +
                "012202" +
                "002000" +
                "000000");
            Assert.AreEqual(State.None, game2.Board[1][4]);
            game2.AiMoveAsync(2).AsTask().Wait();
            Assert.AreEqual(State.None, game2.Board[1][4]);

            IGame game3 = null;
            
            // Given the following initial board state, AI should
            // alternate between playing and not playing space 1,7 
            // as the search depth increases.
            Action reset = () => 
            {
                game3 = GameFactory.GetGame(6, 10);
                game3.LoadSerializedBoardState(
                    "0000000000" +
                    "1121222000" +
                    "0000000000" +
                    "1200000012" +
                    "0000000000" +
                    "1200000012");
            };
            Action<State> assertSpace17Is = state =>
                Assert.AreEqual(state, game3.Board[1][7]);
            
            reset();
            game3.AiMoveAsync(1).AsTask().Wait();
            assertSpace17Is(State.One);
            
            reset();
            game3.AiMoveAsync(2).AsTask().Wait();
            assertSpace17Is(State.None);
            
            // With depth 3, move 1,7 can happen first or third, so perform 
            // actual moves and then check space 1,7. Note that AI at each 
            // move should search no further than on previous moves.
            reset();
            game3.AiMoveAsync(3).AsTask().Wait();
            game3.AiMoveAsync(2).AsTask().Wait();
            game3.AiMoveAsync(1).AsTask().Wait();
            assertSpace17Is(State.One);

            reset();
            game3.AiMoveAsync(4).AsTask().Wait();
            assertSpace17Is(State.None);
        }

        [TestMethod]
        [TestCategory("ai")]
        public void AiPassesAppropriately()
        {
            var game = GameFactory.GetGame(2, 4);
            game.LoadSerializedBoardState(
                "0122" +
                "1212");
            game.AiMoveAsync(1).AsTask().Wait();
            Assert.IsFalse(game.IsGameOver());
            Assert.AreEqual(1, game.Moves.Count);
            Assert.AreEqual(null, game.Moves[0]);

            game.AiMoveAsync(1).AsTask().Wait();
            Assert.IsTrue(game.IsGameOver());
            Assert.AreEqual(2, game.Moves.Count);
            Assert.AreEqual(new Space(0, 0), game.Moves[1]);
        }

        [TestMethod]
        [TestCategory("ai")]
        public void DepthSearchIncludesBranchesWithPasses()
        {
            IGame game = null;
            Action reset = () => 
            {
                game = GameFactory.GetGame(4, 10);
                game.LoadSerializedBoardState(
                    "0000000000" +
                    "1121222000" +
                    "0000000000" +
                    "0000210000");
            };
            Action<State> assertSpace17Is = state =>
                Assert.AreEqual(state, game.Board[1][7]);
            Action<int, int> assertScoreIs = (one, two) => 
            {
                var score = game.GetScore();
                Assert.AreEqual(one, score.PlayerOne);
                Assert.AreEqual(two, score.PlayerTwo);
            };

            // For each search depth, perform the first move and then confirm 
            // that the correct one was made. Then, perform the subsequent 
            // moves, with the AI searching no further than on previous moves. 
            // Finally, check that the score is as expected. This confirms that
            // the branches are fully analyzed, including the ones with passes. 

            reset();
            game.AiMoveAsync(1).AsTask().Wait();
            assertSpace17Is(State.One);
            assertScoreIs(8, 2);

            reset();
            game.AiMoveAsync(2).AsTask().Wait();
            assertSpace17Is(State.None);
            game.AiMoveAsync(1).AsTask().Wait();
            assertScoreIs(6, 4);

            reset();
            game.AiMoveAsync(3).AsTask().Wait();
            assertSpace17Is(State.None);
            game.AiMoveAsync(2).AsTask().Wait();
            game.AiMoveAsync(1).AsTask().Wait();
            assertScoreIs(10, 1);

            reset();
            game.AiMoveAsync(4).AsTask().Wait();
            assertSpace17Is(State.None);
            game.AiMoveAsync(3).AsTask().Wait();
            game.AiMoveAsync(2).AsTask().Wait();
            game.AiMoveAsync(1).AsTask().Wait();
            assertScoreIs(5, 7);

            reset();
            game.AiMoveAsync(5).AsTask().Wait();
            assertSpace17Is(State.None);
            game.AiMoveAsync(4).AsTask().Wait();
            game.AiMoveAsync(3).AsTask().Wait();
            game.AiMoveAsync(2).AsTask().Wait();
            game.AiMoveAsync(1).AsTask().Wait();
            assertScoreIs(13, 0);
        }

        [TestMethod]
        [TestCategory("ai")]
        public void AiHandlesEndOfGameAppropriatelyInDepthSearch()
        {
            var game = GameFactory.GetGame(2, 4);
            game.LoadSerializedBoardState(
                "1220" +
                "1212");
            game.AiMoveAsync(2).AsTask().Wait();
            Assert.IsTrue(game.IsGameOver());
        }

        // This test attempts to confirm that the corner heuristics are properly
        // accounted for. However, the AI doesn't always select the corner move 
        // when you would expect it to, so more testing might be called for. In
        // some cases, the AI may neglect a corner move because lookahead reveals
        // that it will be able to make that move soon regardless. However, that
        // doesn't always happen. So the question remains: does the corner 
        // heuristic need further tweaking?
        [TestMethod]
        [TestCategory("ai")]
        public void AiAccountsForCornersCorrectly()
        {
            // Use a non-corner case as an experimental control group.
            // On this board, 1,5 is the best move with depth 3,
            // since it forces a response at 2,5 which allows 2,6 
            // and a score of 9 to 2 after 3 moves. 
            var game1 = GameFactory.GetGame(4, 8);
            game1.LoadSerializedBoardState(
                "00000000" +
                "00200000" +
                "12122000" +
                "00110000");

            var moveDepth1Task = game1.GetBestMoveAsync(1).AsTask<ISpace>();
            moveDepth1Task.Wait();
            Assert.AreEqual(new Space(2, 5), moveDepth1Task.Result);

            var moveDepth3Task = game1.GetBestMoveAsync(3).AsTask<ISpace>();
            moveDepth3Task.Wait();
            Assert.AreEqual(new Space(1, 5), moveDepth3Task.Result);

            var moveDepth4Task = game1.GetBestMoveAsync(4).AsTask<ISpace>();
            moveDepth4Task.Wait();
            Assert.AreEqual(new Space(0, 2), moveDepth4Task.Result);

            game1.AiMoveAsync(3).AsTask().Wait();
            Assert.AreEqual(State.One, game1.Board[1][5]);
            game1.AiMoveAsync(3).AsTask().Wait();
            Assert.AreEqual(State.Two, game1.Board[2][5]);

            var game2 = GameFactory.GetGame(4, 8);
            game2.LoadSerializedBoardState(
                "00010000" +
                "00022121" +
                "00000020" +
                "00000200");
            game2.AiMoveAsync(3).AsTask().Wait();
            Assert.AreEqual(State.One, game2.Board[3][7]);
            game2.AiMoveAsync(3).AsTask().Wait();
            Assert.AreEqual(State.Two, game2.Board[3][6]);

            // test case where AI has to avoid letting opponent get a corner.
            var game3 = GameFactory.GetGame(4, 8);
            game3.LoadSerializedBoardState(
                "00000121" +
                "00010200" +
                "00022110" +
                "00000000");
            game3.AiMoveAsync(3).AsTask().Wait();
            Assert.AreEqual(State.One, game3.Board[0][4]);
            game3.AiMoveAsync(2).AsTask().Wait();
            Assert.AreEqual(State.Two, game3.Board[0][3]);
        }

        // This test demonstrates a way to use test methods to debug.
        // I was seeing a mysterious and rare, hard-to-replicate exception. 
        // I waited until the exception happened again, and then I captured 
        // the board state and the move that caused the exception. Then, 
        // I replicated the state and move in this test, and was able to 
        // reproduce the exception consistently (causing the test to fail).
        // At that point, I could try various code changes to see if they 
        // caused the test to pass. It turned out that I had neglected the
        // fact that Int32.MinValue != -Int32.MaxValue in one place. 
        [TestMethod]
        [TestCategory("ai")]
        public void MysteryProblem()
        {
            var game = GameFactory.GetGame();
            game.LoadSerializedBoardState(
                "01111111" +
                "12112222" +
                "12211221" +
                "12121221" +
                "12212121" +
                "12221211" +
                "10222220" +
                "10111200");
            game.MoveAsync(6, 1).AsTask().Wait();
            game.AiMoveAsync(5).AsTask().Wait();
            // threw an exception until I changed:
            //return Int32.MinValue * (GetWinner(board) == player ? -1 : 1);
            // to
            //return Int32.MaxValue * (GetWinner(board) == player ? 1 : -1);
        }

        // This test demonstrates a way to test for indeterminate outcomes. 
        // This is a time-consuming test; uncomment the following line to run it.
        //[TestMethod]
        public void MoveIsRandomWhenBestMovesAreEquivalent()
        {
            var test1 = 0;
            var test2 = 0;
            var test3 = 0;
            foreach (var i in Enumerable.Range(0, 1000))
            {
                var game = GameFactory.GetGame(6, 6);
                game.MoveAsync("24").AsTask().Wait();
                game.AiMoveAsync(1).AsTask().Wait();
                if (game.Board[1][2] == State.Two) test1++;
                if (game.Board[1][4] == State.Two) test2++;
                if (game.Board[3][4] == State.Two) test3++;
            }
            Assert.IsTrue(test1 > 0 && test2 > 0 && test3 > 0);
        }

        // This is a time-consuming test; use the Ignore attribute to exclude it. 
        // NOTE: For accurate results, be sure to run this test on a Release build.
        [Ignore]
        [TestMethod]
        [TestCategory("ai")]
        [Timeout(20000)]
        public void MoveTimeIsAdequate()
        {
            var game = GameFactory.GetGame();
            game.LoadSerializedBoardState(
                "00000000" +
                "00000000" +
                "00211200" +
                "00121200" +
                "00212100" +
                "00221200" +
                "00000000" +
                "00000000");
            game.GetBestMoveAsync(6).AsTask().Wait();
        }

        [TestCategory("ai")]
        [TestMethod]
        public void AiTakesAvailableWin()
        {
            var game = GameFactory.GetGame();
            game.LoadSerializedBoardState(
                "00000000" +
                "00002000" +
                "00012100" +
                "00012100" +
                "00012100" +
                "00011000" +
                "00010000" +
                "00000000");
            var task = game.GetBestMoveAsync(6).AsTask<ISpace>();
            task.Wait();
            Assert.AreEqual(new Space(0, 4), task.Result);
        }

    }

}
