using System;
using Microsoft.SPOT;

using Tetris.Helpers;
using Tetris.Constants;

namespace Tetris.Game
{
    /// <summary>
    /// Class representing game board
    /// </summary>
    public class GameBoard
    {
        #region Type definitions

        /// <summary>
        /// Helper enum with brick square states
        /// </summary>
        public enum FieldState : byte { Empty, Filled, AboutToDisappear, Brick };

        #endregion Type definitions

        #region Private fields

        /// <summary>
        /// Array with board buffer ( game board on which calculations are made)
        /// </summary>
        private FieldState[] BoardDataBuffer;

        /// <summary>
        /// Brick which is currently moving on the board
        /// </summary>
        private Brick currentBrick;

        /// <summary>
        /// Flag informing whether blick is currently moving (if false - brick is frozen)
        /// </summary>
        private bool brickActive;

        #endregion Private fields

        #region Public properties

        /// <summary>
        /// Array with display buffer
        /// </summary>
        public FieldState[] BoardDataDisplay { private set; get; }

        /// <summary>
        /// Blag informing whether update board display
        /// </summary>
        public bool UpdateBoard { set; get; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// public Constructor
        /// </summary>
        public GameBoard()
        {
            BoardDataDisplay = new FieldState[BoardConsts.BoardHeight * BoardConsts.BoardWidth];
            BoardDataBuffer = new FieldState[(BoardConsts.BoardHeight + BoardConsts.BoardHeightOffset) * BoardConsts.BoardWidth];
            currentBrick = new Brick();
        }

        /// <summary>
        /// Initialization of the board
        /// </summary>
        public void InitBoard()
        {
            //Clearing board buffer
            for (int i = 0; i < BoardConsts.BoardHeight + BoardConsts.BoardHeightOffset; i++)
            {
                for (int j = 0; j < BoardConsts.BoardWidth; j++)
                {
                    BoardDataBuffer[i * BoardConsts.BoardWidth + j] =
                        FieldState.Empty;
                }
            }
            //Clearing display buffer
            SwapBuffer();
            currentBrick.InitializeBrick(Brick.GenerateBrick());
            brickActive = false;
        }

        /// <summary>
        /// Coping buffer to display 
        /// </summary>
        public void SwapBuffer()
        {
            Array.Copy(BoardDataBuffer, BoardConsts.BoardWidth * BoardConsts.BoardHeightOffset,
                BoardDataDisplay, 0, 
                BoardDataDisplay.Length);
        }

        /// <summary>
        /// Generating and initialization of new brick
        /// </summary>
        /// <param name="code"></param>
        public void NewBrick(int code)
        {
            currentBrick.InitializeBrick(code);
            brickActive = true;
        }

        /// <summary>
        /// Moving brick down
        /// </summary>
        /// <returns>true if colision detected, false otherwise</returns>
        public bool MoveBrickDown()
        {
            bool colisionDetected =
                DetectColision(currentBrick.xPosition + 1,
                currentBrick.yPosition,
                currentBrick.BrickCode);

            if (!colisionDetected)
            {
                RemoveBrickFromBoard();
                currentBrick.MoveBrickDown();
                PlaceBrickOnBoard();
            }
            else
            {
                FrezeBrick();
            }

            return colisionDetected;
        }

        /// <summary>
        /// Moving brick horizontally
        /// </summary>
        /// <param name="left">true - left, false - right</param>
        /// <returns>true if colision detected, false otherwise</returns>
        public bool MoveBrickHorizontal(bool left)
        {
            bool colisionDetected =
                DetectColision(currentBrick.xPosition,
                currentBrick.yPosition + (left ? 1 : -1),
                currentBrick.BrickCode);

            if (!colisionDetected)
            {
                RemoveBrickFromBoard();
                currentBrick.MoveBrickHorizontal(left);
                PlaceBrickOnBoard();
            }

            return colisionDetected;
        }

        /// <summary>
        /// Rotating brick
        /// </summary>
        /// <param name="left">true - left, false - right</param>
        /// <returns>true if colision detected, false otherwise</returns>
        public bool RotateBrick(bool left)
        {
            int rotatedCode = currentBrick.RotateBrick(left);
            bool colisionDetected =
                DetectColision(currentBrick.xPosition,
                currentBrick.yPosition,
                rotatedCode);

            if (!colisionDetected)
            {
                RemoveBrickFromBoard();
                currentBrick.BrickCode = rotatedCode;
                PlaceBrickOnBoard();
            }

            return colisionDetected;
        }

        /// <summary>
        /// Detection and flagging lines
        /// </summary>
        /// <returns></returns>
        public int DetectLines()
        {
            int numberOfLines = 0;
            int j;
            for (int i = 0; i < BoardConsts.BoardHeight; i++)
            {
                if (BoardDataBuffer[(i + BoardConsts.BoardHeightOffset) * BoardConsts.BoardWidth] == FieldState.Filled)
                {
                    for (j = 1; j < BoardConsts.BoardWidth; j++)
                    {
                        if (BoardDataBuffer[(i + BoardConsts.BoardHeightOffset) * BoardConsts.BoardWidth + j] != FieldState.Filled)
                            break;
                    }
                    if (j == BoardConsts.BoardWidth)
                    {
                        numberOfLines++;
                        for (int k = 0; k < BoardConsts.BoardWidth; k++)
                        {
                            BoardDataBuffer[(i + BoardConsts.BoardHeightOffset) * BoardConsts.BoardWidth + k] = FieldState.AboutToDisappear;
                        }
                    }
                }
            }
            return numberOfLines;
        }

        /// <summary>
        /// Cleaning flagged lines
        /// </summary>
        public void CleanUpDetectedLines()
        {
            int linesToCheck = 0;
            for (int i = BoardConsts.BoardHeight - 1; i >= linesToCheck; i--)
            {
                if (BoardDataBuffer[(i + BoardConsts.BoardHeightOffset) * BoardConsts.BoardWidth] == FieldState.AboutToDisappear)
                {
                    for (int k = i; k > 0; k--)
                        for (int l = 0; l < BoardConsts.BoardWidth; l++)
                        {
                            int kk = k + BoardConsts.BoardHeightOffset;
                            if (BoardDataBuffer[kk * BoardConsts.BoardWidth + l] != FieldState.Brick)
                            {
                                if (BoardDataBuffer[(kk - 1) * BoardConsts.BoardWidth + l] == FieldState.Brick)
                                {
                                    BoardDataBuffer[kk * BoardConsts.BoardWidth + l] = FieldState.Empty;
                                }
                                else
                                {
                                    BoardDataBuffer[kk * BoardConsts.BoardWidth + l] = BoardDataBuffer[(kk - 1) * BoardConsts.BoardWidth + l];
                                }
                            }
                        }
                    for (int k = 0; k < BoardConsts.BoardWidth; k++)
                        if (BoardDataBuffer[k + BoardConsts.BoardHeightOffset] != FieldState.Brick)
                            BoardDataBuffer[k + BoardConsts.BoardHeightOffset] = FieldState.Empty;
                    linesToCheck++;
                    i++;
                }
            }
        }

        /// <summary>
        /// Flagging board to update display
        /// </summary>
        public void BoardUpdated()
        {
            UpdateBoard = false;
        }

        /// <summary>
        /// Checking Game Over requirements
        /// </summary>
        /// <returns>true is game is over, false otherwise</returns>
        public bool IsGameOver()
        {
            return !brickActive && currentBrick.xPosition < BoardConsts.BoardHeightOffset;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Helper function putting brick to board buffer
        /// </summary>
        private void PlaceBrickOnBoard()
        {
            FillBrickSpace(FieldState.Brick);
        }

        /// <summary>
        /// Helper function removing brick from board buffer
        /// </summary>
        private void RemoveBrickFromBoard()
        {
            FillBrickSpace(FieldState.Empty);
        }

        /// <summary>
        /// Helper function setting brick-shape fields to desired state
        /// </summary>
        /// <param name="fieldState"></param>
        private void FillBrickSpace(FieldState fieldState)
        {
            for (int i = 0; i < BrickConsts.BrickSize; i++)
            {
                for (int j = 0; j < BrickConsts.BrickSize; j++)
                {
                    if (BitHelper.GetBitBool(currentBrick.BrickCode,
                        i * BrickConsts.BrickSize + j))
                    {
                        BoardDataBuffer[(currentBrick.xPosition + i) * BoardConsts.BoardWidth + currentBrick.yPosition + j] = fieldState;
                    }
                }
            }
        }

        /// <summary>
        /// Detecting colision of brick
        /// </summary>
        /// <param name="xPosition">x brick position on board</param>
        /// <param name="yPosition">y brick position on board</param>
        /// <param name="code">brick code</param>
        /// <returns>true is colision detected, false otherwise</returns>
        private bool DetectColision(int xPosition, int yPosition, int code)
        {
            for (int i = 0; i < BrickConsts.BrickSize; i++)
            {
                for (int j = 0; j < BrickConsts.BrickSize; j++)
                {
                    if (BitHelper.GetBitBool(code,
                        i * BrickConsts.BrickSize + j))
                    {
                        try
                        {
                            if (yPosition + j < 0 || yPosition + j >= BoardConsts.BoardWidth ||
                                BoardDataBuffer[(xPosition + i) * BoardConsts.BoardWidth + yPosition + j] == FieldState.Filled)
                                return true;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Freezing brick (after colision is detected)
        /// </summary>
        private void FrezeBrick()
        {
            FillBrickSpace(FieldState.Filled);
            brickActive = false;
        }

        #endregion Private methods
    }
}
