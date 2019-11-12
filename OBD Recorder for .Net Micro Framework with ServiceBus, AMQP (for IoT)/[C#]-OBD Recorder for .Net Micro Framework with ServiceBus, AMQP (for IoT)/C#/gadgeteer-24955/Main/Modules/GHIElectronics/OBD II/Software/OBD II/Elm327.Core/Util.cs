using System;
using Microsoft.SPOT;

namespace Elm327.Core
{
    /// <summary>
    /// Contains useful utility methods.
    /// </summary>
    public static class Util
    {
        #region Event Definitions

        /// <summary>
        /// Delegate used to handle log events.
        /// </summary>
        /// <param name="logMessage">The message that is being logged.</param>
        public delegate void Elm327LogEventHandler(string logMessage);

        /// <summary>
        /// Fired when a message is being logged in the framework.  This may
        /// be changed or may go away completely, as it is just currently being
        /// used as an easy way for a hosting application to get debug
        /// output.
        /// </summary>
        public static event Elm327LogEventHandler Elm327MessageLogged;

        #endregion

        #region Internal Static Methods

        /// <summary>
        /// Converts celsius temperature to farenheit.
        /// </summary>
        /// <param name="celsiusTemperature">Temperature in celsius.</param>
        /// <returns>The farenheit temperature.</returns>
        internal static double ConvertCelsiusToFarenheit(int celsiusTemperature)
        {
            // F = (C x 1.8) + 32
            
            return celsiusTemperature * 1.8 + 32;
        }

        /// <summary>
        /// Accepts a hexadecimal string (such as "01AB") and returns its integer
        /// value.
        /// </summary>
        /// <param name="hexNumber">The string representation of the hexadecimal
        /// value.</param>
        /// <returns>The integer value represented by the hex string.</returns>
        internal static int ConvertHexToInt(string hexNumber)
        {
            try
            {
                return Convert.ToInt32(hexNumber, 16);
            }
            catch (Exception ex)
            {
                Util.Log(ex);
                return 0;
            }
        }

        /// <summary>
        /// Converts kilometers to miles.
        /// </summary>
        /// <param name="km">The value to convert.</param>
        /// <returns>The mileage value.</returns>
        internal static double ConvertKilometersToMiles(double km)
        {
            // 1 mile = 1.609 km

            return km / 1.609;
        }

        /// <summary>
        /// Logs a message for debugging purposes.  Big TODO here.
        /// </summary>
        /// <param name="logMessage">The message to write to the log.</param>
        internal static void Log(string logMessage)
        {
            Debug.Print(logMessage);

            if (Util.Elm327MessageLogged != null)
                Util.Elm327MessageLogged(logMessage);
        }

        /// <summary>
        /// Logs a message for debugging purposes.  Big TODO here.
        /// </summary>
        /// <param name="ex">The exception to write to the log.</param>
        internal static void Log(Exception ex)
        {
            Util.Log(ex.ToString());
        }

        #endregion
    }
}
