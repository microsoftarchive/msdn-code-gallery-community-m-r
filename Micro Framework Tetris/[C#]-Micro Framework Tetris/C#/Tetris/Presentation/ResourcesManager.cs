using System;
using Microsoft.SPOT;

namespace Tetris.Presentation
{
    /// <summary>
    /// Helper class managing resources
    /// </summary>
    public static class ResourcesManager
    {
        /// <summary>
        /// Game initialization bitmap
        /// </summary>
        public static Bitmap GameInitImg = Resources.GetBitmap(Resources.BitmapResources.Game_Init);

        /// <summary>
        /// Game over bitmap
        /// </summary>
        public static Bitmap GameEndImg = Resources.GetBitmap(Resources.BitmapResources.Game_End);

        /// <summary>
        /// Game board bitmap
        /// </summary>
        public static Bitmap GameProgressImg = Resources.GetBitmap(Resources.BitmapResources.Game_Board);

        /// <summary>
        /// Game font
        /// </summary>
        public static Font GameFont = Resources.GetFont(Resources.FontResources.HomegirlGetLow);
    }
}
