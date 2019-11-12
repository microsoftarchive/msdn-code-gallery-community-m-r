using System;
using Microsoft.SPOT;

namespace Tetris.Helpers
{
    /// <summary>
    /// Helper class solving brick code issues
    /// </summary>
    static class BitHelper
    {
        /// <summary>
        /// Checing wheter brick squre is empty or filled
        /// </summary>
        /// <param name="num">brick code</param>
        /// <param name="bitNum">brick square nnumber</param>
        /// <returns>1 - filled, 0 - empty</returns>
        public static int GetBit(int num,int bitNum)
        {
            return (num >> bitNum) & 0x01;
        }

        /// <summary>
        /// Checing wheter brick squre is empty or filled
        /// </summary>
        /// <param name="num">brick code</param>
        /// <param name="bitNum">brick square nnumber</param>
        /// <returns>true - filled, false - empty</returns>
        public static bool GetBitBool(int num, int bitNum)
        {
            return GetBit(num, bitNum) == 1;
        }
    }
}
