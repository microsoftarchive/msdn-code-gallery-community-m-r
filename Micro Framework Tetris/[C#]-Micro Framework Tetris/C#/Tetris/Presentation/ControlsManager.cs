using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Hardware;
using STM32F429I_Discovery.Netmf.Hardware;

using Tetris.Constants;

namespace Tetris.Presentation
{
    /// <summary>
    /// Class responsible for user input
    /// </summary>
    public class ControlsManager
    {
        #region Private fields

        /// <summary>
        /// Helper variable - on board button
        /// </summary>
        private InputPort button;

        #endregion Private fields

        #region Public properties

        /// <summary>
        /// Rapid fall button pressed
        /// </summary>
        public bool RapidFall { get; private set; }

        /// <summary>
        /// Move left button pressed
        /// </summary>
        public bool MoveLeft { get; private set; }

        /// <summary>
        /// Move right button pressed
        /// </summary>
        public bool MoveRight { get; private set; }

        /// <summary>
        /// Rotate left button pressed
        /// </summary>
        public bool RotateLeft { get; private set; }

        /// <summary>
        /// Rotate right button pressed
        /// </summary>
        public bool RotateRight { get; private set; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Public constructor
        /// </summary>
        public ControlsManager()
	    {
            button = new InputPort((Cpu.Pin)0, false, Port.ResistorMode.PullDown);
	    }

        /// <summary>
        /// Handling user input
        /// </summary>
        /// <param name="e"></param>
        /// <param name="ue"></param>
        public void HandleTouch(TouchEventArgs e,UIElement ue)
        {
            int x, y;
            e.GetPosition(ue, 0, out x, out y);
            if(y > WindowConsts.WindowHeight / 2)
            {
                RapidFall = true;
            }
            else
            {
                bool rotate = button.Read();
                if (x >= WindowConsts.WindowWidth / 2)
                {
                    if (rotate)
                    {
                        RotateLeft = true;
                    }
                    else
                    {
                        MoveLeft = true;
                    }
                }
                else
                {
                    if (rotate)
                    {
                        RotateRight = true;
                    }
                    else
                    {
                        MoveRight = true;
                    }
                }
            }
        }

        /// <summary>
        /// Clearing input flags (after handling them)
        /// </summary>
        public void TouchHandled()
        {
            RapidFall = false;
            RotateLeft = false;
            RotateRight = false;
            MoveRight = false;
            MoveLeft = false;
        }

        #endregion Public methods
    }
}
