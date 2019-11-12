using ReversiGameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;

namespace Reversi.Models
{
    /// <summary>
    /// Provides a CLR-serializable wrapper for the C++ game component. 
    /// </summary>
    [DataContract]
    public sealed class CLRSerializableCPPGame : IGame
    {
        private ReversiGameComponentCPP.ReversiGameWrapper _gameComponent;
        public ReversiGameComponentCPP.ReversiGameWrapper GameComponent
        {
            get { return _gameComponent = _gameComponent ?? new ReversiGameComponentCPP.ReversiGameWrapper(); }
            set { _gameComponent = value; }
        }

        public CLRSerializableCPPGame()
        {
            _gameComponent = new ReversiGameComponentCPP.ReversiGameWrapper();
        }

        public CLRSerializableCPPGame(int rowCount, int columnCount)
        {
            _gameComponent = new ReversiGameComponentCPP.ReversiGameWrapper(rowCount, columnCount);
        }

        [DataMember]
        public IList<IList<State>> Board
        {
            get
            {
                return (
                    from row in GameComponent.Board
                    select row.ToList<State>())
                    .ToList<IList<State>>();
            }
            set { GameComponent.Board = value; }
        }

        [DataMember]
        public IList<ISpace> Moves
        {
            get { return GameComponent.Moves.ToList<ISpace>(); }
            set { GameComponent.Moves = (value as List<ISpace>) ?? new List<ISpace>(value); }
        }

        [DataMember]
        public IList<ISpace> MoveStack
        {
            get { return GameComponent.MoveStack.ToList<ISpace>(); }
            set { GameComponent.MoveStack = (value as List<ISpace>) ?? new List<ISpace>(value); }
        }

        [DataMember]
        public int RowCount
        {
            get { return GameComponent.RowCount; }
            set { GameComponent.RowCount = value; }
        }

        [DataMember]
        public int ColumnCount
        {
            get { return GameComponent.ColumnCount; }
            set { GameComponent.ColumnCount = value; }
        }

        public State CurrentPlayer
        {
            get { return (State)GameComponent.CurrentPlayer; }
        }

        public State CurrentOpponent
        {
            get { return (State)GameComponent.CurrentOpponent; }
        }

        public State Winner
        {
            get { return (State)GameComponent.Winner; }
        }

        public IScore GetScore()
        {
            return GameComponent.GetScore();
        }

        public bool IsValidMove(ISpace move)
        {
            return GameComponent.IsValidMove(move);
        }

        public State GetSpaceState(int row, int column)
        {
            return (State)GameComponent.GetSpaceState(row, column);
        }

        public bool IsValidMove(int row, int column)
        {
            return GameComponent.IsValidMove(row, column);
        }

        public bool IsPassValid()
        {
            return GameComponent.IsPassValid();
        }

        public bool IsGameOver()
        {
            return GameComponent.IsGameOver();
        }

        public void LoadSerializedBoardState(string state)
        {
            GameComponent.LoadSerializedBoardState(state);
        }

        public sealed override string ToString()
        {
            return GameComponent.ToString();
        }

        public IAsyncOperation<ISpace> GetBestMoveAsync(int searchDepth)
        {
            return GameComponent.GetBestMoveAsync(searchDepth);
        }

        public IAsyncAction AiMoveAsync(int searchDepth)
        {
            return GameComponent.AiMoveAsync(searchDepth);
        }

        public IAsyncAction MoveAsync(string moves)
        {
            return GameComponent.MoveAsync(moves);
        }

        public IAsyncOperation<IList<ISpace>> MoveAsync(ISpace move)
        {
            return Call(GameComponent.MoveAsync(move));
        }

        public IAsyncOperation<IList<ISpace>> MoveAsync(int row, int column)
        {
            return Call(GameComponent.MoveAsync(row, column));
        }

        public IAsyncOperation<IList<ISpace>> PassAsync()
        {
            return Call(GameComponent.PassAsync());
        }

        public IAsyncOperation<IList<ISpace>> RedoAsync()
        {
            return Call(GameComponent.RedoAsync());
        }

        public IAsyncOperation<IList<ISpace>> UndoAsync()
        {
            return Call(GameComponent.UndoAsync());
        }

        public IAsyncOperation<IList<ISpace>> Call(IAsyncOperation<IList<ISpace>> operation)
        {
            return AsyncInfo.Run(async cancellationToken =>
            {
                var affectedSpaces = await operation.AsTask(cancellationToken);
                return (IList<ISpace>)(
                    from space in affectedSpaces
                    select new Space(space.Row, space.Column))
                    .ToList<ISpace>();
            });
        }

    }
}
