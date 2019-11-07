using System;
using Microsoft.SPOT;

namespace Tetris.Constants
{
    /// <summary>
    /// Brick specific constants
    /// </summary>
    public static class BrickConsts
    {
        /// <summary>
        /// Begin position for new brick (in buffer)
        /// </summary>
        public const byte xBeginBrickPosition = 0;

        /// <summary>
        /// Begin position for new brick (in buffer)
        /// </summary>
        public const byte yBegonBrickPosition = 4;

        /// <summary>
        /// Size of the brick (in squares)
        /// </summary>
        public const byte BrickSize = 3;
    }
}
