using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Tetris.Constants
{
    /// <summary>
    /// Window specific constants
    /// </summary>
    public static class WindowConsts
    {
        /// <summary>
        /// Window height (in pixels)
        /// </summary>
        public const int WindowHeight = 320;

        /// <summary>
        /// Window width (in pixels)
        /// </summary>
        public const int WindowWidth = 240;

        /// <summary>
        /// X coordinate of next brick box
        /// </summary>
        public const byte xInfoBeginPoint = 193;

        /// <summary>
        /// Y coordinate of next brick box
        /// </summary>
        public const byte yInfoBeginPoint = 2;

        /// <summary>
        /// X coordinate of score text
        /// </summary>
        public const byte xInfoScorePoint = 7;

        /// <summary>
        /// Y coordinate of score text
        /// </summary>
        public const byte yInfoScorePoint = 32;

        /// <summary>
        /// X coordinate of level text
        /// </summary>
        public const byte xInfoLevelPoint = 98;

        /// <summary>
        /// Y coordinate of level text
        /// </summary>
        public const int yInfoLevelPoint = 303;

        /// <summary>
        /// X coordinate of board field
        /// </summary>
        public const byte xBoardBeginPoint = 19;

        /// <summary>
        /// Y coordinate of board field
        /// </summary>
        public const byte yBoardBeginPoint = 55;

        /// <summary>
        /// Brick square size in next brick box (in pixels)
        /// </summary>
        public const byte BrickInfoSize = 14;

        /// <summary>
        /// Brick square offset in next brick box (in pixels)
        /// </summary>
        public const byte BrickInfoOffset = 15;

        /// <summary>
        /// Brick square size on board (in pixels)
        /// </summary>
        public const byte BrickBoardSize = 20;

        /// <summary>
        /// Brick square offset on board (in pixels)
        /// </summary>
        public const byte BrickBoardOffset = 20;

        /// <summary>
        /// X coordinte of score on game over screen
        /// </summary>
        public const byte xFinalScore = 145;

        /// <summary>
        /// Y coordinte of score on game over screen
        /// </summary>
        public const byte yFinalScore = 42;

        /// <summary>
        /// Brush for empty brick square
        /// </summary>
        public static readonly Brush EmptyFieldBrush = new SolidColorBrush(ColorUtility.ColorFromRGB(237, 28, 26));

        /// <summary>
        /// Brush for brick square when line is detected
        /// </summary>
        public static readonly Brush ScoreFieldBrush = new SolidColorBrush(ColorUtility.ColorFromRGB(166, 242, 23));

        /// <summary>
        /// Brush for square filled with brick
        /// </summary>
        public static readonly Brush FilledFieldBrush = new SolidColorBrush(ColorUtility.ColorFromRGB(74, 36, 230));

        /// <summary>
        /// Standard pen
        /// </summary>
        public static readonly Pen StandardPen = new Pen(Color.Black);
    }
}
