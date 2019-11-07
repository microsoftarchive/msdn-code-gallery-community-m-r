using System;
using Microsoft.SPOT;

namespace Tetris.Game
{
    /// <summary>
    /// Class repersenting state of the game
    /// </summary>
    public class GameState
    {
        #region Type definitions

        /// <summary>
        /// Helper enum type with state enumeration
        /// </summary>
        public enum State : byte { INIT, GAME_INIT, GAME_PROGRESS, GAME_OVER };

        #endregion Type definitions

        #region Public properties

        /// <summary>
        /// Current game state
        /// </summary>
        public State CurrentState { get; private set; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// State initialization
        /// </summary>
        public GameState()
        {
            CurrentState = State.INIT;
        }

        /// <summary>
        /// Game Over
        /// </summary>
        public void FinishGame()
        {
            CurrentState = State.GAME_OVER;
        }

        /// <summary>
        /// Class initialization
        /// </summary>
        public void InitGame()
        {
            CurrentState = State.GAME_INIT;
        }

        /// <summary>
        /// Stating game progress
        /// </summary>
        public void ProgressGame()
        {
            CurrentState = State.GAME_PROGRESS;
        }

        /// <summary>
        /// Restarting game
        /// </summary>
        public void RestartGame()
        {
            CurrentState = State.INIT;
        }

        #endregion Public methods
    }
}
