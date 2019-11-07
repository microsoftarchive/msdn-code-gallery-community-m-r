using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to return information about the top speakers
    /// </summary>
    public class Speaker
    {
        /// <summary>
        /// Speaker name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Score
        /// </summary>
        public double Score { get; set; }
    }
}
