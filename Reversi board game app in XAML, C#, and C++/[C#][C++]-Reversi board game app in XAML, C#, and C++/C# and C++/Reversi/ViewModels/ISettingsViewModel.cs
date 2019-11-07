using System;
using System.ComponentModel;

namespace Reversi.ViewModels
{
    /// <summary>
    /// Defines the public members required by Reversi settings view 
    /// model implementations. 
    /// </summary>
    /// <remarks>
    /// This interface enables you to decouple the view model 
    /// consumers (such as unit tests and other view models) from 
    /// the specific view model implementations, and to substitute 
    /// alternate implementations (such as Mock implementations). 
    /// </remarks>
    public interface ISettingsViewModel
    {
        int BoardSizeIndex { get; set; }
        IGameViewModel GameViewModel { get; set; }
        bool IsClockShowing { get; set; }
        bool IsLastMoveIndicatorShowing { get; set; }
        bool IsShowingValidMoves { get; set; }
        void NewGame();
        string NewGameBoardSizeText { get; }
        string NewGameOpponentsText { get; }
        int PlayerOneAiSearchDepthSetting { get; set; }
        bool PlayerOneIsAiSetting { get; set; }
        int PlayerTwoAiSearchDepthSetting { get; set; }
        bool PlayerTwoIsAiSetting { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
