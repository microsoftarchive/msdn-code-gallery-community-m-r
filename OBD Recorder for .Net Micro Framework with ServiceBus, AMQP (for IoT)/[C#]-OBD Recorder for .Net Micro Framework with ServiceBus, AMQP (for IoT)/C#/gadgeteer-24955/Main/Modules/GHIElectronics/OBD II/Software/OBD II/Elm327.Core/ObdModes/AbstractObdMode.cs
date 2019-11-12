using System.Collections;

namespace Elm327.Core.ObdModes
{
    /// <summary>
    /// Base class for one of the OBD modes of operation (see
    /// http://en.wikipedia.org/wiki/OBD-II_PIDs for information).
    /// </summary>
    public abstract class AbstractObdMode
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of a derived class of <see cref="AbstractObdMode"/>.
        /// </summary>
        /// <param name="elm">A reference to the ELM327 core driver.</param>
        /// <param name="obdModeIdentifier">The OBD mode identifier as a 
        /// hexadecimal string (i.e., "01", "02", etc.).</param>
        internal AbstractObdMode(ElmDriver elm, string obdModeIdentifier)
        {
            // Note: In the full .NET framework, it would probably be better
            // to tag the derived class with a custom attribute that contained
            // the OBD mode identifier, but that functionality is not currently
            // available in MF

            this.Elm = elm;
            this.ModeIdentifier = obdModeIdentifier;
        }

        #endregion

        #region Private Instance Properties

        /// <summary>
        /// The ELM327 driver instance, which allows us to communicate with the
        /// chip.
        /// </summary>
        private ElmDriver Elm
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the the OBD mode identifier as a hexadecimal string 
        /// (i.e., "01", "02", etc.).
        /// </summary>
        private string ModeIdentifier
        {
            get;
            set;
        }

        #endregion

        #region Protected Instance Properties

        /// <summary>
        /// Gets the measuring unit currently being used.
        /// </summary>
        protected ElmDriver.ElmMeasuringUnitType MeasuringUnitType
        {
            get
            {
                return this.Elm.MeasuringUnitType;
            }
        }

        #endregion

        #region Private Instance Methods

        /// <summary>
        /// Parses a multiline PID response, returning only the data
        /// elements in the message.
        /// </summary>
        /// <param name="message">The message received from the ELM.</param>
        /// <returns>An array of values returned from the ELM.  This will usually
        /// be an array of hex-encoded bytes represented as strings.</returns>
        private string[] ParseMultilinePidResponse(string message)
        {
            // Split the response into lines

            string[] lines = message.Split(ElmDriver.MESSAGE_TERMINATOR_CHAR);

            if (lines.Length == 0)
                return new string[0];

            // If there's only one element in the first line, this is a
            // CAN response, which needs to be parsed differently than
            // the other protocols

            if (lines[0].Trim().Split(' ').Length == 1)
                return this.ParseMultilinePidResponseForCan(lines);
            else
            {
                // TODO
                // MarkusH - For now: return the lines, to allow handling of multiple responses for a regular PID request
                return lines;
                // return string[0];
            }
        }

        /// <summary>
        /// Parses a multiline PID CAN response, returning only the data
        /// elements in the message.
        /// </summary>
        /// <param name="lines">The message received from the ELM.</param>
        /// <returns>An array of values returned from the ELM.  This will usually
        /// be an array of hex-encoded bytes represented as strings.</returns>
        private string[] ParseMultilinePidResponseForCan(string[] lines)
        {
            // TODO: Improve the efficiency of this method (check number of
            // data elements so we don't need to use an ArrayList, for example)

            // See the ELM327 documentation for exactly how multiline CAN 
            // messages are reported

            ArrayList dataElements = new ArrayList();
            string[] lineElements;
            int firstValidLineElementIndex;

            // The first line contains the number of data elements 
            // in the message, but we're ignoring it for now

            for (int i = 1; i < lines.Length; i++)
            {
                lineElements = lines[i].Trim().Split(' ');

                // Ignore the first four data elements in the first line,
                // and ignore the first data element for each following
                // line

                firstValidLineElementIndex = (i == 1) ? 4 : 1;

                for (int j = firstValidLineElementIndex; j < lineElements.Length; j++)
                {
                    dataElements.Add(lineElements[j]);
                }
            }

            return (string[])dataElements.ToArray(typeof(string));
        }

        #endregion

        #region Protected Instance Methods

        /// <summary>
        /// Sends a request for a PID value from the ELM and returns the response.
        /// </summary>
        /// <param name="pid">The hexadecimal value of the PID to request.</param>
        /// <returns>An array of values returned from the ELM.  This will usually
        /// be an array of hex-encoded bytes represented as strings.</returns>
        protected string[] GetPidResponse(string pid)
        {
            // The PID request is sent to the ELM like "0102", where "01" is the
            // mode identifier in hex and the rest of the string is the PID

            string message = this.Elm.SendAndReceiveMessage(this.ModeIdentifier + pid);

            if (message == null)
                return new string[0];

            // Check to see if there's a line terminator in the message.  If so, it's a
            // multiline response that needs to be handled differently.

            if (message.IndexOf(ElmDriver.MESSAGE_TERMINATOR_CHAR) > -1)
            {
                string[] lines = this.ParseMultilinePidResponse(message);
                // MarkusH - Process multi-line responses due to multiple devices in the vehicle responding to a PID request
                string previousLine = null;
                foreach (var line in lines)
                {
                    if (previousLine != null
                         && (line.Length < 5
                              || line.TrimEnd(' ').Length != previousLine.TrimEnd(' ').Length
                              || !string.Equals(line.Substring(1, 5), previousLine.Substring(1, 5))
                        ))
                    {
                        // Fall back to previous behavior or just returning the lines
                        return lines;
                    }
                    previousLine = line;
                }
                // We have a multi-line response where each line has the same response header and response PID code (i.e. 41 0F): 
                // This seems to happen when the vehicle has more than one subsystem that can respond to the PID 
                // Seen on Toyota Prius 2007 for most PIDs, i.e. trace output
                //     SND -> 010F
                //     RCV <- 41 0F 41 
                //     41 0F 41
                // In this case just pick the first response and ignore the subsequent ones
                Util.Log("Ignored all but first line of multi-line response with same response header");
                message = lines[0];
            }
            //else
            {
                // TODO: The response must begin with a correct response header; need to
                // check this.  As an example, sending "0102" should return a response
                // that begins with "41 02".

                string[] hexBytes = message.Split(' ');

                if (hexBytes.Length > 2)
                {
                    string[] returnValues = new string[hexBytes.Length - 2];

                    for (int i = 0; i < hexBytes.Length - 2; i++)
                    {
                        returnValues[i] = hexBytes[i + 2];
                    }

                    return returnValues;
                }
                else
                    return new string[0];
            }
        }

        /// <summary>
        /// When overridden in a derived class, queries the ECU(s) for
        /// the PIDs that are supported for the current mode.
        /// </summary>
        protected virtual void GetSupportedPids()
        {
            // TODO: Implement this in derived classes and call it on
            // each derived class after a successful OBD connection
            // has been made
        }

        #endregion
    }
}
