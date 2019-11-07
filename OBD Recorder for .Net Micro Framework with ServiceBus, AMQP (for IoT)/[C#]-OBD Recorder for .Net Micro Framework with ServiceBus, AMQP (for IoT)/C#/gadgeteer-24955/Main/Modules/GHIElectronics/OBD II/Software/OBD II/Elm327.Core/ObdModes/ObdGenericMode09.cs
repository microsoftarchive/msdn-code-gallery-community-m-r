using System;

namespace Elm327.Core.ObdModes
{
    /// <summary>
    /// Contains methods and properties for retrieving generic
    /// OBD mode 09 PIDs.
    /// </summary>
    public class ObdGenericMode09 : AbstractObdMode
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of <see cref="ObdGenericMode09"/>.
        /// </summary>
        /// <param name="elm">A reference to the ELM327 driver.</param>
        internal ObdGenericMode09(ElmDriver elm)
            : base(elm, "09")
        {
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the VIN of the vehicle.
        /// </summary>
        public string VehicleIdentificationNumber
        {
            get
            {
                string[] reading = this.GetPidResponse("02");

                if (reading == null || reading.Length == 0)
                    return string.Empty;

                try
                {
                    char[] vinCharacters = new char[reading.Length];

                    for (int i = 0; i < reading.Length; i++)
                    {
                        vinCharacters[i] = (char)Util.ConvertHexToInt(reading[i]);
                    }

                    return new string(
                        vinCharacters,
                        0,
                        vinCharacters.Length);
                }
                catch (Exception ex)
                {
                    Util.Log(ex);
                    return string.Empty;
                }
            }
        }

        #endregion
    }
}
