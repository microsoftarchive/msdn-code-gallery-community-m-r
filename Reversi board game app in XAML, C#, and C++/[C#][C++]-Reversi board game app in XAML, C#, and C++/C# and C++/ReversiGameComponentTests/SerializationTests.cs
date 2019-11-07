using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Reversi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiGameComponentTests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        [TestCategory("serialization")]
        public void CanParseAndPerformAStringOfMoves()
        {
            var game = GameFactory.GetGame();
            game.MoveAsync("35,45,53,42,55,46,54,--").AsTask().Wait();
            Assert.AreEqual(8, game.Moves.Count);
            var score = game.GetScore();
            Assert.AreEqual(7, score.PlayerOne);
            Assert.AreEqual(4, score.PlayerTwo);
        }

        [TestMethod]
        [TestCategory("serialization")]
        public void CanSerializeAndDeserializeBoardState()
        {
            var game1 = GameFactory.GetGame(4, 4);
            game1.MoveAsync("13,01,00,03,02,23,33,31,32,10,30,--,20").AsTask().Wait();
            var state = game1.ToString();
            var game2 = GameFactory.GetGame(4, 4);
            game2.LoadSerializedBoardState(state);
            foreach (var row in Enumerable.Range(0, 4))
            {
                foreach (var column in Enumerable.Range(0, 4))
                {
                    if (game1.Board[row][column] != game2.Board[row][column])
                        Assert.Fail(String.Format(
                            "Mismatch: row = {0}, column = {1}", row, column));
                }
            }
        }
    }
}
