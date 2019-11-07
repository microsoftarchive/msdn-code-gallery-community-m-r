using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace ReversiGameModel
{
    public interface IGame
    {
        IList<IList<State>> Board { get; set; }
        int RowCount { get; set; }
        int ColumnCount { get; set; }
        IList<ISpace> Moves { get; set; }
        IList<ISpace> MoveStack { get; set; }

        State CurrentPlayer { get; }
        State CurrentOpponent { get; }
        State Winner { get; }
        IScore GetScore();

        State GetSpaceState(int row, int column);
        bool IsValidMove(ISpace move);
        bool IsValidMove(int row, int column);
        bool IsPassValid();
        bool IsGameOver();

        [Windows.Foundation.Metadata.DefaultOverload()]
        IAsyncOperation<IList<ISpace>> MoveAsync(ISpace move);
        IAsyncOperation<IList<ISpace>> MoveAsync(int row, int column);
        IAsyncAction MoveAsync(string moves);
        IAsyncAction AiMoveAsync(int searchDepth);
        IAsyncOperation<ISpace> GetBestMoveAsync(int searchDepth);
        
        IAsyncOperation<IList<ISpace>> PassAsync();
        IAsyncOperation<IList<ISpace>> RedoAsync();
        IAsyncOperation<IList<ISpace>> UndoAsync();

        string ToString();
        void LoadSerializedBoardState(string state);
    }
}
