using Reversi.Common;
using Reversi.ViewModels;
using System;
using System.ComponentModel;

namespace ReversiViewModelTests.Mocks
{
    public class MockClockViewModel : IClockViewModel
    {
        public Action StartDelegate { get; set; }
        public Action StopDelegate { get; set; }

        public IGameViewModel GameViewModel { get; set; }
        public DelegateCommand PlayCommand { get; set; }
        public DelegateCommand PauseCommand { get; set; }
        public bool IsPaused { get; set; }
        public bool IsShowingPauseDisplay { get; set; }
        public bool IsPauseButtonVisible { get; set; }
        public double PauseDisplayOpacity { get; set; }
        public string PlayerOneMoveTime { get; set; }
        public TimeSpan PlayerOneMoveTimeSpan { get; set; }
        public string PlayerOneTotalTime { get; set; }
        public TimeSpan PlayerOneTotalTimeSpan { get; set; }
        public string PlayerTwoMoveTime { get; set; }
        public TimeSpan PlayerTwoMoveTimeSpan { get; set; }
        public string PlayerTwoTotalTime { get; set; }
        public TimeSpan PlayerTwoTotalTimeSpan { get; set; }
        public void Start() { StartDelegate(); }
        public void Stop() { StopDelegate(); }
        #pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 0067
    }
}
