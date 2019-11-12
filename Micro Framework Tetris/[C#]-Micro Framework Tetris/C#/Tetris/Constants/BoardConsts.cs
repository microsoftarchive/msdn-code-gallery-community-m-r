using System;
using Microsoft.SPOT;

namespace Tetris.Constants
{
    /// <summary>
    /// Static class with Board specific constants
    /// </summary>
    public static class BoardConsts
    {
        /// <summary>
        /// Board high offset - used only for buffer, not displayed
        /// </summary>
        public const byte BoardHeightOffset = 3;

        /// <summary>
        /// Board width (in brick squares)
        /// </summary>
        public const byte BoardWidth = 10;

        /// <summary>
        /// Board Height (in brick squares)
        /// </summary>
        public const byte BoardHeight = 12;
    }
}
