using System;
using System.ComponentModel;

namespace Reversi.ViewModels
{
    /// <summary>
    /// Defines the public members required by Reversi game view 
    /// model implementations. 
    /// </summary>
    /// <remarks>
    /// This interface enables you to decouple the view model 
    /// consumers (such as unit tests and other view models) from 
    /// the specific view model implementations, and to substitute 
    /// alternate implementations (such as Mock implementations). 
    /// </remarks>
    public interface IGameViewModel
    {
        string BoardSizeText { get; }
        IClockViewModel Clock { get; set; }
        int ColumnCount { get; }
        ReversiGameModel.State CurrentPlayer { get; set; }
        int CurrentPlayerAiSearchDepth { get; }
        void Dispose();
        event EventHandler ForcedPass;
        ReversiGameModel.IGame Game { get; set; }
        string GameOverText { get; }
        bool IsCurrentPlayerAi { get; }
        bool IsGameAiVersusAi { get; }
        bool IsGameHumanVersusHuman { get; }
        bool IsGameOver { get; }
        bool IsPlayerOneAi { get; }
        bool IsPlayerTwoAi { get; }
        System.Collections.Generic.IList<ReversiGameModel.ISpace> LastMoveAffectedSpaces { get; set; }
        Reversi.Common.DelegateCommand<ReversiGameModel.ISpace> MoveCommand { get; }
        string OpponentsText { get; }
        Player PlayerOne { get; set; }
        int PlayerOneAiSearchDepth { get; }
        Player PlayerTwo { get; set; }
        int PlayerTwoAiSearchDepth { get; }
        Reversi.Common.DelegateCommand RedoCommand { get; }
        int RowCount { get; }
        ReversiGameModel.IScore Score { get; set; }
        int ScoreFontSize { get; }
        double ScoreTranslateY { get; }
        void Start();
        void Stop();
        BoardSpaceState this[string index] { get; }
        Reversi.Common.DelegateCommand UndoCommand { get; }
        void UpdateBoard();
        ReversiGameModel.State Winner { get; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
