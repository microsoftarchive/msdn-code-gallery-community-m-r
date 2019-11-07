using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Reversi.ViewModels;
using ReversiGameModel;
using ReversiViewModelTests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReversiViewModelTests
{
    [TestClass]
    public class GameViewModelTests
    {
        [TestMethod]
        [TestCategory("GameViewModel")]
        public void MoveCommandCanExecuteOnlyForLegalHumanMove()
        {
            var mockClockViewModel = new MockClockViewModel();
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(mockClockViewModel, mockGame);

            Action<bool> assertMoveCanExecute = expected => Assert.AreEqual<bool>(
                expected, gameViewModel.MoveCommand.CanExecute(new Space(0, 0)));

            mockGame.IsValidMoveDelegate = move => false;
            assertMoveCanExecute(false);

            mockGame.IsValidMoveDelegate = move => true;
            assertMoveCanExecute(true);

            mockClockViewModel.IsPaused = true;
            assertMoveCanExecute(false);

            mockClockViewModel.IsPaused = false;
            assertMoveCanExecute(true);

            gameViewModel.PlayerOne = Player.Ai1;
            assertMoveCanExecute(false);
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void MoveCommandExecutionPerformsMove()
        {
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(null, mockGame);
            var testMove = new Space(2, 3);
            bool moveAsyncCalled = false;

            mockGame.MoveAsyncDelegate = move =>
            {
                moveAsyncCalled = true;
                Assert.AreEqual<Space>(testMove, (Space)move);
                return Task.FromResult<IList<ISpace>>(
                    new List<ISpace>(new ISpace[] { move }));
            };

            gameViewModel.MoveCommand.Execute(testMove).Wait();

            Assert.IsTrue(moveAsyncCalled);
            Assert.AreEqual<Space>(testMove, (Space)gameViewModel.LastMoveAffectedSpaces[0]);
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void UndoCommandCanExecuteOnlyAfterFirstMoveExceptInPausedNonAiVsAiGamesUnlessGameIsOver()
        {
            var mockClockViewModel = new MockClockViewModel();
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(mockClockViewModel, mockGame);
            var undoCommand = gameViewModel.UndoCommand;

            // Undo cannot execute if there are no earlier moves.

            mockGame.Moves = new List<ISpace>();
            Assert.IsFalse(undoCommand.CanExecute());

            mockGame.Moves.Add(new Space(0, 0));
            Assert.IsTrue(undoCommand.CanExecute());

            // Undo cannot execute if clock is stopped except
            // when game is over and in AI vs. AI games.

            mockClockViewModel.IsPaused = true;
            mockGame.IsGameOverDelegate = () => true;
            gameViewModel.SyncModelProperties();
            Assert.IsTrue(undoCommand.CanExecute());

            mockGame.IsGameOverDelegate = () => false;
            gameViewModel.SyncModelProperties();
            Assert.IsFalse(undoCommand.CanExecute());

            gameViewModel.PlayerOne = Player.Ai1;
            Assert.IsFalse(gameViewModel.IsGameAiVersusAi);
            Assert.IsFalse(undoCommand.CanExecute());

            gameViewModel.PlayerTwo = Player.Ai1;
            Assert.IsTrue(gameViewModel.IsGameAiVersusAi);
            Assert.IsTrue(undoCommand.CanExecute());
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void RedoCommandCanExecuteOnlyWhenThereAreUndoneMovesExceptOnPausedHumanMoveOrUnpausedAiVsAiMove()
        {
            var mockClockViewModel = new MockClockViewModel();
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(mockClockViewModel, mockGame);
            var redoCommand = gameViewModel.RedoCommand;

            // Redo cannot execute if there are no previously-undone moves.

            mockGame.Moves = new List<ISpace>();
            mockGame.MoveStack = new List<ISpace>();
            Assert.IsFalse(redoCommand.CanExecute());

            mockGame.MoveStack.Add(new Space(0, 0));
            Assert.IsTrue(redoCommand.CanExecute());

            // Redo cannot execute if a human move is paused or if an AI vs. AI game is not paused. 

            mockClockViewModel.IsPaused = true;
            Assert.IsFalse(gameViewModel.IsGameAiVersusAi);
            Assert.IsFalse(redoCommand.CanExecute());

            gameViewModel.PlayerOne = Player.Ai1;
            gameViewModel.PlayerTwo = Player.Ai1;
            Assert.IsTrue(gameViewModel.IsGameAiVersusAi);
            Assert.IsTrue(redoCommand.CanExecute());

            mockClockViewModel.IsPaused = false;
            Assert.IsFalse(redoCommand.CanExecute());
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void UndoCommandExecutionPerformsUndo()
        {
            var mockClockViewModel = new MockClockViewModel();
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(mockClockViewModel, mockGame);
            var testMove = new Space(2, 3);
            bool undoAsyncCalled = false;

            mockGame.UndoAsyncDelegate = () =>
            {
                undoAsyncCalled = true;
                return Task.FromResult<IList<ISpace>>(
                    new List<ISpace>(new ISpace[] { testMove }));
            };

            gameViewModel.UndoCommand.Execute().Wait();

            Assert.IsTrue(undoAsyncCalled);
            Assert.AreEqual<Space>(testMove, (Space)gameViewModel.LastMoveAffectedSpaces[0]);
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void RedoCommandExecutionPerformsRedo()
        {
            var mockClockViewModel = new MockClockViewModel();
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(mockClockViewModel, mockGame);
            var testMove = new Space(2, 3);
            bool redoAsyncCalled = false;

            mockGame.RedoAsyncDelegate = () =>
            {
                redoAsyncCalled = true;
                return Task.FromResult<IList<ISpace>>(
                    new List<ISpace>(new ISpace[] { testMove }));
            };

            gameViewModel.RedoCommand.Execute().Wait();

            Assert.IsTrue(redoAsyncCalled);
            Assert.AreEqual<Space>(testMove, (Space)gameViewModel.LastMoveAffectedSpaces[0]);
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void UndoAndRedoCommandsCanExecuteOnlyIfUndoOrRedoIsNotInProgress()
        {
            var mockClockViewModel = new MockClockViewModel();
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(mockClockViewModel, mockGame);
            bool undoAsyncCalled = false;
            bool redoAsyncCalled = false;

            // Set up conditions so that both undo and redo are possible.
            mockGame.Moves = new List<ISpace>(new Space[] { new Space(0, 0) });
            mockGame.MoveStack = new List<ISpace>(new Space[] { new Space(0, 0), new Space(0, 0) });

            mockGame.UndoAsyncDelegate = () =>
            {
                undoAsyncCalled = true;
                Assert.IsFalse(gameViewModel.UndoCommand.CanExecute());
                Assert.IsFalse(gameViewModel.RedoCommand.CanExecute());
                return mockGame.TaskFromListResult;
            };
            gameViewModel.UndoCommand.Execute().Wait();
            Assert.IsTrue(undoAsyncCalled);
            Assert.IsTrue(gameViewModel.UndoCommand.CanExecute());
            Assert.IsTrue(gameViewModel.RedoCommand.CanExecute());

            mockGame.RedoAsyncDelegate = () =>
            {
                redoAsyncCalled = true;
                Assert.IsFalse(gameViewModel.UndoCommand.CanExecute());
                Assert.IsFalse(gameViewModel.RedoCommand.CanExecute());
                return mockGame.TaskFromListResult;
            };
            gameViewModel.RedoCommand.Execute().Wait();

            Assert.IsTrue(redoAsyncCalled);
            Assert.IsTrue(gameViewModel.UndoCommand.CanExecute());
            Assert.IsTrue(gameViewModel.RedoCommand.CanExecute());
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void PassMovesOccurAutomatically()
        {
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(null, mockGame);
            bool moveAsyncCalled = false;
            bool passAsyncCalled = false;
            bool passEventOccurred = false;

            mockGame.IsValidMoveDelegate = move => move != null;
            mockGame.MoveAsyncDelegate = move =>
            {
                moveAsyncCalled = true;
                mockGame.IsValidMoveDelegate = move_ => move_ == null;
                return mockGame.TaskFromListResult;
            };
            mockGame.PassAsyncDelegate = () =>
            {
                passAsyncCalled = true;
                mockGame.IsValidMoveDelegate = move => false;
                return mockGame.TaskFromListResult;
            };
            gameViewModel.ForcedPass += (s, e) => passEventOccurred = true;

            gameViewModel.MoveCommand.Execute(new Space(0, 0)).Wait();

            Assert.IsTrue(moveAsyncCalled);
            Assert.IsTrue(passEventOccurred);
            Assert.IsTrue(passAsyncCalled);
        }

        [TestMethod]
        [TestCategory("GameViewModel")]
        public void AiMovesOccurAutomatically()
        {
            var mockGame = new MockGame();
            var gameViewModel = new GameViewModel(null, mockGame);
            bool moveAsyncCalledForHuman = false;
            bool moveAsyncCalledForAI = false;
            bool getBestMoveAsyncCalled = false;

            gameViewModel.PlayerTwo = Player.Ai1;

            mockGame.IsValidMoveDelegate = move => move != null;
            mockGame.MoveAsyncDelegate = move =>
            {
                if (mockGame.CurrentPlayer == State.One)
                {
                    moveAsyncCalledForHuman = true;
                    mockGame.CurrentPlayer = State.Two;
                }
                else
                {
                    moveAsyncCalledForAI = true;
                    mockGame.CurrentPlayer = State.One;
                }
                return mockGame.TaskFromListResult;
            };
            mockGame.GetBestMoveAsyncDelegate = searchDepth =>
            {
                getBestMoveAsyncCalled = true;
                return mockGame.TaskFromSpaceResult;
            };

            gameViewModel.MoveCommand.Execute(new Space(0, 0)).Wait();

            Assert.IsTrue(moveAsyncCalledForHuman);
            Assert.IsTrue(getBestMoveAsyncCalled);
            Assert.IsTrue(moveAsyncCalledForAI);
        }
    }
}
