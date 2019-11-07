using System;
using System.Threading;
using Microsoft.SPOT;

using Tetris.Presentation;
using Tetris.Constants;

namespace Tetris.Game
{
    /// <summary>
    /// Class mamaging all game classes and containg game logic
    /// </summary>
    public class GameController
    {
        #region Private variables

        /// <summary>
        /// Helper variable counting game ticks
        /// </summary>
        private int gameTicks;

        /// <summary>
        /// Helper variable informing wheter there is active brick on the board
        /// </summary>
        private bool brickActive;

        /// <summary>
        /// Helper variable informing wheter there is acrive detected line
        /// </summary>
        private bool linesActive;

        /// <summary>
        /// Helper variable - numer of ilned needed to level up
        /// </summary>
        private int linesToLevel;

        /// <summary>
        /// Helper variable informing wheter rapid fall button id pressed
        /// </summary>
        private bool rapidFall;

        #endregion Private variables

        #region Public fields

        /// <summary>
        /// Game state 
        /// </summary>
        public GameState GameState = new GameState();

        /// <summary>
        /// Game info
        /// </summary>
        public GameInfo GameInfo = new GameInfo();

        /// <summary>
        /// Game board
        /// </summary>
        public GameBoard GameBoard = new GameBoard();

        #endregion Public fields

        #region Public methods

        /// <summary>
        /// Public constructor
        /// </summary>
        public GameController()
        {
            GameState.RestartGame();
        }

        /// <summary>
        /// Game restart
        /// </summary>
        public void RestartGame()
        {
            GameState.RestartGame();
        }

        /// <summary>
        /// Game initialization
        /// </summary>
        public void GameInit()
        {
            GameState.InitGame();
            GameInfo.ResetInfo();
            GameBoard.InitBoard();
            brickActive = false;
            linesActive = false;
            gameTicks = 0;
            linesToLevel = 0;
        }

        /// <summary>
        /// Game start (start progress)
        /// </summary>
        public void StartGame()
        {
            GameState.ProgressGame();
        }

        /// <summary>
        /// Game tick method (main logic)
        /// </summary>
        /// <param name="cm"></param>
        public void GameTick(ControlsManager cm)
        {
            if (linesActive)
            {
                GameBoard.CleanUpDetectedLines();
                linesActive = false;
            }
            if (cm.RotateLeft)
            {
                GameBoard.RotateBrick(true);
            }
            else if (cm.RotateRight)
            {
                GameBoard.RotateBrick(false);
            }
            if (cm.RapidFall)
            {
                rapidFall = cm.RapidFall;
            }
            if (cm.MoveLeft)
            {
                GameBoard.MoveBrickHorizontal(true);
            }
            else if (cm.MoveRight)
            {
                GameBoard.MoveBrickHorizontal(false);
            }
            cm.TouchHandled();

            if (gameTicks-- == 0)
            {
                if (brickActive)
                {
                    if (rapidFall)
                    {
                        while (!GameBoard.MoveBrickDown()) ;
                    }
                    if (!rapidFall && !GameBoard.MoveBrickDown())
                    {
                    }
                    else
                    {
                        rapidFall = false;
                        if (GameBoard.IsGameOver())
                        {
                            EndGame();
                        }
                        int detectedLines = GameBoard.DetectLines();
                        if (detectedLines > 0)
                        {
                            for (int i = 0; i < ((1 + detectedLines) * detectedLines) / 2; i++)
                            {
                                GameInfo.ScoreUp();
                            }
                            if (++linesToLevel == GameConsts.LevelLines)
                            {
                                GameInfo.LevelUp();
                                linesToLevel = 0;
                            }
                            linesActive = true;
                        }
                        brickActive = false;
                    }
                }
                else
                {
                    GameBoard.NewBrick(GameInfo.GetBrick());
                    brickActive = true;
                }
                GameBoard.UpdateBoard = true;
                gameTicks = GameConsts.MaxLevel - GameInfo.Level;
            }
        }

        /// <summary>
        /// End game (after game over)
        /// </summary>
        public void EndGame()
        {
            GameState.FinishGame();
        }

        #endregion Public methods
    }
}
