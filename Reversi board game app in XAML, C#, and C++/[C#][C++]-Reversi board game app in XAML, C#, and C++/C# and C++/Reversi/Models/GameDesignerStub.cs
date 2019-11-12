using ReversiGameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Reversi.Models
{
    /// <summary>
    /// Provides a concrete IGame implementation for use by the designer data.
    /// </summary>
    public sealed class GameDesignerStub : IGame
    {
        public IList<IList<State>> Board { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public IList<ISpace> Moves { get; set; }
        public IList<ISpace> MoveStack { get; set; }
        public State CurrentPlayer { get { return State.One; } }
        public State CurrentOpponent { get { return State.Two; } }
        public State Winner { get { return State.None; } }
        public IScore GetScore() { throw new NotImplementedException(); }
        public State GetSpaceState(int row, int column) { throw new NotImplementedException(); }
        public bool IsValidMove(ISpace move) { throw new NotImplementedException(); }
        public bool IsValidMove(int row, int column) { throw new NotImplementedException(); }
        public bool IsPassValid() { throw new NotImplementedException(); }
        public bool IsGameOver() { throw new NotImplementedException(); }
        public IAsyncOperation<IList<ISpace>> MoveAsync(ISpace move) { throw new NotImplementedException(); }
        public IAsyncOperation<IList<ISpace>> MoveAsync(int row, int column) { throw new NotImplementedException(); }
        public IAsyncAction MoveAsync(string moves) { throw new NotImplementedException(); }
        public IAsyncAction AiMoveAsync(int searchDepth) { throw new NotImplementedException(); }
        public IAsyncOperation<ISpace> GetBestMoveAsync(int searchDepth) { throw new NotImplementedException(); }
        public IAsyncOperation<IList<ISpace>> PassAsync() { throw new NotImplementedException(); }
        public IAsyncOperation<IList<ISpace>> RedoAsync() { throw new NotImplementedException(); }
        public IAsyncOperation<IList<ISpace>> UndoAsync() { throw new NotImplementedException(); }
        public void LoadSerializedBoardState(string state) { throw new NotImplementedException(); }
    }
}
