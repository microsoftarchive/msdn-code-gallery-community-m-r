using Reversi.Common;
using Reversi.ViewModels;
using ReversiGameModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReversiViewModelTests.Mocks
{
    public class MockGameViewModel : IGameViewModel
    {
        public MockGameViewModel()
        {
            StartDelegate = () => { };
            StopDelegate = () => { };
            UpdateBoardDelegate = () => { };
            MoveCommand = new DelegateCommand<ISpace>(space => { });
            UndoCommand = new DelegateCommand(() => { });
            RedoCommand = new DelegateCommand(() => { });
        }

        public Action StartDelegate { get; set; }
        public Action StopDelegate { get; set; }
        public Action UpdateBoardDelegate { get; set; }

        public void RaisePropertyChanged(String propertyName)
        {
            var handlers = PropertyChanged;
            if (handlers != null)
            {
                handlers(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IClockViewModel Clock { get; set; }
        public IGame Game { get; set; }
        public BoardSpaceState this[string index] { get { return BoardSpaceState.None; } }
        public IList<ISpace> LastMoveAffectedSpaces { get; set; }
        public DelegateCommand<ISpace> MoveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }
        public DelegateCommand RedoCommand { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int CurrentPlayerAiSearchDepth { get; set;}
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public State CurrentPlayer { get; set; }
        public State Winner { get; set; }
        public IScore Score { get; set; }
        public bool IsCurrentPlayerAi { get; set; }
        public bool IsGameAiVersusAi { get; set; }
        public bool IsGameHumanVersusHuman { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsPlayerOneAi { get; set; }
        public bool IsPlayerTwoAi { get; set; }
        public int PlayerOneAiSearchDepth { get; set; }
        public int PlayerTwoAiSearchDepth { get; set; }
        public int ScoreFontSize { get; set; }
        public double ScoreTranslateY { get; set; }
        public string BoardSizeText { get; set; }
        public string OpponentsText { get; set; }
        public string GameOverText { get; set; }
        public void Start() { StartDelegate(); }
        public void Stop() { StopDelegate(); }
        public void UpdateBoard() { UpdateBoardDelegate(); }
        public void Dispose() { throw new NotImplementedException(); }
        #pragma warning disable 0067
        public event EventHandler ForcedPass;
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 0067
    }
}
