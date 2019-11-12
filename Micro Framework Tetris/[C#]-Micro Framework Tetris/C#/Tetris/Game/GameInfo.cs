using System;
using Microsoft.SPOT;

using Tetris.Constants;

namespace Tetris.Game
{
    /// <summary>
    /// Class representing game information (level, score ...)
    /// </summary>
    public class GameInfo
    {
        #region Public properties

        /// <summary>
        /// Flag informing wheter info display update is requires
        /// </summary>
        public bool UpdateInfo { private set; get; }

        /// <summary>
        /// Game level
        /// </summary>
        public byte Level { private set; get; }

        /// <summary>
        /// Game score
        /// </summary>
        public int Score { private set; get; }

        /// <summary>
        /// Next brick code
        /// </summary>
        public int NextBrick { private set; get; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Info initialization
        /// </summary>
        public void ResetInfo()
        {
            Level = 1;
            Score = 0;
            NextBrick = Brick.GenerateBrick();
            UpdateInfo = true;
        }

        /// <summary>
        /// Getting next brick and generating new one
        /// </summary>
        /// <returns></returns>
        public int GetBrick()
        {
            int temp = NextBrick;
            NextBrick = Brick.GenerateBrick();
            UpdateInfo = true;
            return temp;
        }

        /// <summary>
        /// Score after line is detected
        /// </summary>
        public void ScoreUp()
        {
            Score += Level;
            UpdateInfo = true;
        }

        /// <summary>
        /// Level up
        /// </summary>
        public void LevelUp()
        {
            if (Level < GameConsts.MaxLevel)
            {
                Level++;
                UpdateInfo = true;
            }
        }

        /// <summary>
        /// Clearing update display info flag
        /// </summary>
        public void InfoUpdated()
        {
            UpdateInfo = false;
        }

        #endregion Public methods
    }
}
