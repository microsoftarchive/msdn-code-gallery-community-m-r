using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Reversi.ViewModels;
using ReversiViewModelTests.Mocks;
using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace ReversiViewModelTests
{
    [TestClass]
    public class ClockViewModelTests
    {
        MockGameViewModel mockGameViewModel;
        ClockViewModel clockViewModel;

        [TestInitialize]
        public void InitializeTest()
        {
            ExecuteOnUIThread(() =>
            {
                mockGameViewModel = new MockGameViewModel();
                clockViewModel = new ClockViewModel(mockGameViewModel);
            });
        }

        private void ExecuteOnUIThread(DispatchedHandler action)
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, action).AsTask().Wait();
        }

        private void ExecuteOnUIThreadThenAssertAreEqual<T>(T expected, Func<T> function)
        {
            T actual = default(T);
            ExecuteOnUIThread(() => actual = function());
            Assert.AreEqual<T>(expected, actual);
        }

        [TestMethod]
        [TestCategory("ClockViewModel")]
        public void StartAndStopMethodsStartAndStopTheClock()
        {
            // Confirm that the ClockViewModel constructor 
            // starts the clock automatically. 
            ExecuteOnUIThreadThenAssertAreEqual(false, () => 
                clockViewModel.IsPaused);

            ExecuteOnUIThreadThenAssertAreEqual(true, () =>
            {
                clockViewModel.Stop();
                return clockViewModel.IsPaused;
            });

            ExecuteOnUIThreadThenAssertAreEqual(false, () =>
            {
                clockViewModel.Start();
                return clockViewModel.IsPaused;
            });
        }

        [TestMethod]
        [TestCategory("ClockViewModel")]
        public void PauseCommandCanExecuteOnlyIfClockIsRunning()
        {
            ExecuteOnUIThreadThenAssertAreEqual(true, () =>
            {
                return clockViewModel.PauseCommand.CanExecute();
            });
            
            ExecuteOnUIThreadThenAssertAreEqual(false, () =>
            {
                clockViewModel.Stop();
                return clockViewModel.PauseCommand.CanExecute();
            });

            ExecuteOnUIThreadThenAssertAreEqual(true, () =>
            {
                clockViewModel.Start();
                return clockViewModel.PauseCommand.CanExecute();
            });
        }

        [TestMethod]
        [TestCategory("ClockViewModel")]
        public void PauseCommandExecutionStopsClock()
        {
            ExecuteOnUIThreadThenAssertAreEqual(false, () => 
                clockViewModel.IsPaused);

            ExecuteOnUIThreadThenAssertAreEqual(true, () =>
            {
                clockViewModel.PauseCommand.Execute().Wait();
                return clockViewModel.IsPaused;
            });
        }

        [TestMethod]
        [TestCategory("ClockViewModel")]
        public void PlayCommandCanExecuteOnlyIfClockIsPaused()
        {
            ExecuteOnUIThreadThenAssertAreEqual(false, 
                () => clockViewModel.PlayCommand.CanExecute());

            ExecuteOnUIThreadThenAssertAreEqual(true, () =>
            {
                clockViewModel.Stop();
                return clockViewModel.PlayCommand.CanExecute();
            });

            ExecuteOnUIThreadThenAssertAreEqual(false, () =>
            {
                clockViewModel.Start();
                return clockViewModel.PlayCommand.CanExecute();
            });
        }

        [TestMethod]
        [TestCategory("ClockViewModel")]
        public void PlayCommandExecutionStartsClock()
        {
            ExecuteOnUIThreadThenAssertAreEqual(false, () => 
                clockViewModel.IsPaused);

            ExecuteOnUIThreadThenAssertAreEqual(true, () =>
            {
                clockViewModel.Stop();
                return clockViewModel.IsPaused;
            });

            ExecuteOnUIThreadThenAssertAreEqual(false, () =>
            {
                clockViewModel.PlayCommand.Execute().Wait();
                return clockViewModel.IsPaused;
            });
        }

        [TestMethod]
        [TestCategory("ClockViewModel")]
        public void GameOverChangeStopsOrStartsClock()
        {
            ExecuteOnUIThreadThenAssertAreEqual(false, () => 
                clockViewModel.IsPaused);

            ExecuteOnUIThreadThenAssertAreEqual(true, () => 
            {
                mockGameViewModel.IsGameOver = true;
                mockGameViewModel.RaisePropertyChanged("IsGameOver");
                return clockViewModel.IsPaused;
            });

            ExecuteOnUIThreadThenAssertAreEqual(false, () => 
            {
                mockGameViewModel.IsGameOver = false;
                mockGameViewModel.RaisePropertyChanged("IsGameOver");
                return clockViewModel.IsPaused;
            });

            ExecuteOnUIThreadThenAssertAreEqual(true, () => 
            {
                mockGameViewModel.IsGameOver = true;
                mockGameViewModel.RaisePropertyChanged("IsGameOver");
                return clockViewModel.IsPaused;
            });
        }
    }
}
