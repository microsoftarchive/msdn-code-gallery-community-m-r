using System;
using System.ComponentModel;

namespace Reversi.ViewModels
{
    /// <summary>
    /// Defines the public members required by Reversi clock view 
    /// model implementations. 
    /// </summary>
    /// <remarks>
    /// This interface enables you to decouple the view model 
    /// consumers (such as unit tests and other view models) from 
    /// the specific view model implementations, and to substitute 
    /// alternate implementations (such as Mock implementations). 
    /// </remarks>
    public interface IClockViewModel
    {
        IGameViewModel GameViewModel { get; set; }
        bool IsPauseButtonVisible { get; }
        bool IsPaused { get; }
        bool IsShowingPauseDisplay { get; set; }
        Reversi.Common.DelegateCommand PauseCommand { get; }
        double PauseDisplayOpacity { get; }
        Reversi.Common.DelegateCommand PlayCommand { get; }
        string PlayerOneMoveTime { get; }
        TimeSpan PlayerOneMoveTimeSpan { get; set; }
        string PlayerOneTotalTime { get; }
        TimeSpan PlayerOneTotalTimeSpan { get; set; }
        string PlayerTwoMoveTime { get; }
        TimeSpan PlayerTwoMoveTimeSpan { get; set; }
        string PlayerTwoTotalTime { get; }
        TimeSpan PlayerTwoTotalTimeSpan { get; set; }
        void Start();
        void Stop();
        event PropertyChangedEventHandler PropertyChanged;
    }
}
