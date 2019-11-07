using System;
using Microsoft.SPOT;

namespace Tetris.Constants
{
    /// <summary>
    /// Time specific constants
    /// </summary>
    public static class TimeConsts
    {
        /// <summary>
        /// Interval before making calculations on board
        /// </summary>
        public const byte TimerTickMs = 20;
        
        /// <summary>
        /// Interfval between screen refreshing
        /// </summary>
        public const byte RefreshRateMs = 100;

        /// <summary>
        /// Helper variable counting timer tick for refreshing
        /// </summary>
        public const byte RefreshRateTicks = RefreshRateMs / TimerTickMs;
    }
}
