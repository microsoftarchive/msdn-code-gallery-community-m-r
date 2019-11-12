using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

using Tetris.Helpers;
using Tetris.Constants;
using Tetris.Game;

namespace Tetris.Presentation
{
    /// <summary>
    /// Window class responsible for presentation and processing uset input
    /// </summary>
    public class MainWindow : Window
    {
        #region Private fields

        /// <summary>
        /// Game controller
        /// </summary>
        private Game.GameController controller = new Game.GameController();

        /// <summary>
        /// Input controll manager
        /// </summary>
        private ControlsManager controlsManager = new ControlsManager();

        /// <summary>
        /// Helper timer
        /// </summary>
        private DispatcherTimer refreshTimer;

        /// <summary>
        /// Variable for tick counts
        /// </summary>
        private byte ticks;

        #endregion Private fields

        #region Public methods

        /// <summary>
        /// Public constuctor
        /// </summary>
        public MainWindow()
        {
            refreshTimer = new DispatcherTimer(this.Dispatcher);
            refreshTimer.Interval = new TimeSpan(0, 0, 0, 0, TimeConsts.RefreshRateMs);
            refreshTimer.Tick += new EventHandler(TimerTick);
        }

        #endregion Public methods

        #region Event Hanlers

        /// <summary>
        /// Timer event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            ++ticks;
            if (ticks == TimeConsts.RefreshRateTicks)
            {
                if (controller.GameBoard.UpdateBoard)
                {
                    controller.GameBoard.SwapBuffer();
                }
                if (controller.GameBoard.UpdateBoard || controller.GameInfo.UpdateInfo)
                {
                    Invalidate();
                }
                ticks = 0;
            }
            controller.GameTick(controlsManager);
        }
      
        /// <summary>
        /// Render event handler
        /// </summary>
        /// <param name="dc"></param>
        public override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            switch (controller.GameState.CurrentState)
            {
                case Game.GameState.State.INIT:
                    DrawGameInit(dc);
                    break;
                case Game.GameState.State.GAME_INIT:
                    DrawGameStartup(dc);
                    controller.StartGame();
                    refreshTimer.Start();
                    break;
                case Game.GameState.State.GAME_PROGRESS:
                    DrawGameStartup(dc);
                    DrawGameBoard(dc);
                    DrawGameInfo(dc);
                    controller.GameBoard.BoardUpdated();
                    controller.GameInfo.InfoUpdated();
                    break;
                case Game.GameState.State.GAME_OVER:
                    controller.EndGame();
                    refreshTimer.Stop();
                    ticks = 0;
                    DrawGameEnd(dc);
                    DrawFinalScore(dc);
                    controlsManager.TouchHandled();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Touch event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            
            switch (controller.GameState.CurrentState)
            {
                case Game.GameState.State.INIT:
                    controller.GameInit();
                    break;
                case Game.GameState.State.GAME_PROGRESS:
                    controlsManager.HandleTouch(e, (UIElement)this);
                    break;
                case Game.GameState.State.GAME_OVER:
                    controller.RestartGame();
                    break;
                default:
                    break;
            }

            if (controller.GameState.CurrentState != Game.GameState.State.GAME_PROGRESS)
                Invalidate();
            e.Handled = true;
        }

        #endregion Event Hanlers

        #region Private methods

        /// <summary>
        /// Drawing GameInit Bitmap
        /// </summary>
        /// <param name="dc"></param>
        private void DrawGameInit(DrawingContext dc)
        {
            dc.DrawImage(ResourcesManager.GameInitImg, 0, 0);
        }

        /// <summary>
        /// Drawing GameEndImg Bitmap
        /// </summary>
        /// <param name="dc"></param>
        private void DrawGameEnd(DrawingContext dc)
        {
            dc.DrawImage(ResourcesManager.GameEndImg, 0, 0);
        }

        /// <summary>
        /// Drawing GameProgressImg Bitmap
        /// </summary>
        /// <param name="dc"></param>
        private void DrawGameStartup(DrawingContext dc)
        {
            dc.DrawImage(ResourcesManager.GameProgressImg, 0, 0);
        }

        /// <summary>
        /// Drawing final score on game over screen
        /// </summary>
        /// <param name="dc"></param>
        private void DrawFinalScore(DrawingContext dc)
        {
            dc.DrawText(controller.GameInfo.Score.ToString(),
                ResourcesManager.GameFont, Color.Black,
                WindowConsts.xFinalScore, WindowConsts.yFinalScore);
        }

        /// <summary>
        /// Drawing game info fields
        /// </summary>
        /// <param name="dc"></param>
        private void DrawGameInfo(DrawingContext dc)
        {
            int nextBrick = controller.GameInfo.NextBrick;

            for (int i = 0; i < BrickConsts.BrickSize; i++)
            {
                for (int j = 0; j < BrickConsts.BrickSize; j++)
                {
                    dc.DrawRectangle(
                        BitHelper.GetBitBool(nextBrick, i * BrickConsts.BrickSize + j) ?
                        WindowConsts.FilledFieldBrush : WindowConsts.EmptyFieldBrush
                        , WindowConsts.StandardPen,
                        WindowConsts.xInfoBeginPoint + j * WindowConsts.BrickInfoOffset,
                        WindowConsts.yInfoBeginPoint + i * WindowConsts.BrickInfoOffset,
                        WindowConsts.BrickInfoSize, WindowConsts.BrickInfoSize);
                }
            }

            dc.DrawText(controller.GameInfo.Score.ToString(),
                ResourcesManager.GameFont, Color.Black, 
                WindowConsts.xInfoScorePoint, WindowConsts.yInfoScorePoint);
            dc.DrawText(controller.GameInfo.Level.ToString(),
                ResourcesManager.GameFont, Color.Black, 
                WindowConsts.xInfoLevelPoint, WindowConsts.yInfoLevelPoint);
        }

        /// <summary>
        /// Drawing game board fields
        /// </summary>
        /// <param name="dc"></param>
        private void DrawGameBoard(DrawingContext dc)
        {
            for (int i = 0; i < BoardConsts.BoardHeight; i++)
            {
                for (int j = 0; j < BoardConsts.BoardWidth; j++)
                {
                    GameBoard.FieldState fs = controller.GameBoard.BoardDataDisplay[i*BoardConsts.BoardWidth+j];
                    dc.DrawRectangle(fs == GameBoard.FieldState.Empty ? WindowConsts.EmptyFieldBrush :
                        fs == GameBoard.FieldState.AboutToDisappear ? WindowConsts.ScoreFieldBrush :
                        WindowConsts.FilledFieldBrush, WindowConsts.StandardPen,
                        WindowConsts.xBoardBeginPoint + j * WindowConsts.BrickBoardOffset,
                        WindowConsts.yBoardBeginPoint + i * WindowConsts.BrickBoardOffset,
                        WindowConsts.BrickBoardSize, WindowConsts.BrickBoardSize);
                }
            }
        }

        #endregion Private methods
    }
}
