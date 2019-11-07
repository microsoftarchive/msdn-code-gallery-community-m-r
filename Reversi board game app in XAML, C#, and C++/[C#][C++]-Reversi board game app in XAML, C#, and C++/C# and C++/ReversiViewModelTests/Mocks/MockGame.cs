using ReversiGameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace ReversiViewModelTests.Mocks
{
    public sealed class MockGame : IGame
    {
        public MockGame()
        {
            CurrentPlayer = State.One;
            CurrentOpponent = State.Two;
            Winner = State.None;
            IsValidMoveDelegate = move => false;
            MoveAsyncDelegate = move => TaskFromListResult;
            GetBestMoveAsyncDelegate = searchDepth => TaskFromSpaceResult; 
            IsGameOverDelegate = () => false; 
            GetScoreDelegate = () => new Score(0, 0);
            UndoAsyncDelegate = () => TaskFromListResult;
            RedoAsyncDelegate = () => TaskFromListResult;
            PassAsyncDelegate = () => TaskFromListResult;
            GetSpaceStateDelegate = (row, column) => State.None; 
            IsPassValidDelegate = () => false;
        }

        public readonly Task<IList<ISpace>> TaskFromListResult = Task.FromResult<IList<ISpace>>(new List<ISpace>());
        public readonly Task<ISpace> TaskFromSpaceResult = Task.FromResult<ISpace>(new Space(0, 0));

        public Func<ISpace, bool> IsValidMoveDelegate { get; set; }
        public Func<ISpace, Task<IList<ISpace>>> MoveAsyncDelegate { get; set; }
        public Func<int, Task<ISpace>> GetBestMoveAsyncDelegate { get; set; }
        public Func<bool> IsGameOverDelegate { get; set; }
        public Func<IScore> GetScoreDelegate { get; set; }
        public Func<Task<IList<ISpace>>> UndoAsyncDelegate { get; set; }
        public Func<Task<IList<ISpace>>> RedoAsyncDelegate { get; set; }
        public Func<Task<IList<ISpace>>> PassAsyncDelegate { get; set; }
        public Func<int, int, State> GetSpaceStateDelegate { get; set; }
        public Func<bool> IsPassValidDelegate { get; set; }

        // IGame implementation:
        public IList<IList<State>> Board { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public IList<ISpace> Moves { get; set; }
        public IList<ISpace> MoveStack { get; set; }
        public State CurrentPlayer { get; set; }
        public State CurrentOpponent { get; set; }
        public State Winner { get; set; }
        public IScore GetScore() { return GetScoreDelegate(); }
        public State GetSpaceState(int row, int column) { return GetSpaceStateDelegate(row, column); }
        public bool IsValidMove(ISpace move) { return IsValidMoveDelegate(move); }
        public bool IsValidMove(int row, int column) { return IsValidMoveDelegate(new Space(row, column)); }
        public bool IsPassValid() { return IsPassValidDelegate(); }
        public bool IsGameOver() { return IsGameOverDelegate(); }
        public IAsyncOperation<IList<ISpace>> MoveAsync(ISpace move) { return AsyncInfo.Run(async _ => await MoveAsyncDelegate(move)); }
        public IAsyncOperation<IList<ISpace>> MoveAsync(int row, int column) { return MoveAsync(new Space(row, column)); }
        public IAsyncAction MoveAsync(string moves) { throw new NotImplementedException(); }
        public IAsyncAction AiMoveAsync(int searchDepth) { throw new NotImplementedException(); }
        public IAsyncOperation<ISpace> GetBestMoveAsync(int searchDepth) { return AsyncInfo.Run(async _ => await GetBestMoveAsyncDelegate(searchDepth)); }
        public IAsyncOperation<IList<ISpace>> PassAsync() { return AsyncInfo.Run(async _ => await PassAsyncDelegate()); }
        public IAsyncOperation<IList<ISpace>> RedoAsync() { return AsyncInfo.Run(async _ => await RedoAsyncDelegate()); }
        public IAsyncOperation<IList<ISpace>> UndoAsync() { return AsyncInfo.Run(async _ => await UndoAsyncDelegate()); }
        public void LoadSerializedBoardState(string state) { throw new NotImplementedException(); }
    }
}
