using System;
using Microsoft.SPOT;

using Tetris.Helpers;
using Tetris.Constants;

namespace Tetris.Game
{
    /// <summary>
    /// Class representing tetris brick
    /// </summary>
    class Brick
    {
        #region Static fields and methods

        /// <summary>
        /// Collection with brick types
        /// Coding concept ( 9 bit code )
        /// 0 1 2
        /// 3 4 5
        /// 6 7 8
        /// </summary>
        private static readonly int[] brickTypes = new int[] 
        {
            // x**
            // x**
            // x**
            0x49,
            // xx*
            // x**
            // x**
            0x4B, 
            // xx*
            // xx*
            // ***
            0x1B, 
            // x**
            // xx*
            // x**
            0x59, 
            // *xx
            // **x
            // **x
            0x126, 
            // *xx
            // xx*
            // ***
            0x1E, 
            // xx*
            // *xx
            // ***
            0x33
        };
        
        /// <summary>
        /// Class mapping brick bits to roteted to left
        /// (or roteted right if map in reversed way)
        /// </summary>
        private static readonly byte[] rotateLeftMap = new byte[]
        {
            6, 3, 0, 7, 4, 1, 8, 5, 2
        };

        /// <summary>
        /// Private random generator used in generating bricks 
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Generating new brick codes
        /// </summary>
        /// <returns></returns>
        public static int GenerateBrick()
        {
            return brickTypes[rand.Next(brickTypes.Length)];
        }

        #endregion Static fields and methods

        #region Public properties

        /// <summary>
        /// Posistion X of brick bit 0
        /// </summary>
        public int xPosition { private set; get; }

        /// <summary>
        /// Position Y of brick bit 0
        /// </summary>
        public int yPosition { private set; get; }

        /// <summary>
        /// Brick Code
        /// </summary>
        public int BrickCode { set; get; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Function maps current brick code to roteted one
        /// </summary>
        /// <param name="left">true - rotate left, false - rotate right</param>
        /// <returns>roteted brick code</returns>
        public int RotateBrick(bool left)
        {
            int newCode = 0;
            if (left)
            {
                for (int i = 0; i < rotateLeftMap.Length; i++)
                {
                    newCode |= BitHelper.GetBit(BrickCode, rotateLeftMap[i]) << i;
                }
            }
            else
            {
                for (int i = 0; i < rotateLeftMap.Length; i++)
                {
                    newCode |= BitHelper.GetBit(BrickCode, i) << rotateLeftMap[i];
                }
            }
            return newCode;
        }

        /// <summary>
        /// Function initialize Brick fields
        /// </summary>
        /// <param name="code"></param>
        public void InitializeBrick(int code)
        {
            xPosition = BrickConsts.xBeginBrickPosition;
            yPosition = BrickConsts.yBegonBrickPosition;
            BrickCode = code;
        }

        /// <summary>
        /// Function moved brick down the board
        /// </summary>
        public void MoveBrickDown()
        {
            xPosition++;
        }

        /// <summary>
        /// Function moves brick horizontally
        /// </summary>
        /// <param name="left">true - move left, false - move right</param>
        public void MoveBrickHorizontal(bool left)
        {
            if (left)
            {
                yPosition++;
            }
            else
            {
                yPosition--;
            }
        }

        #endregion Public methods
    }
}
