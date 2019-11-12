using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to return information about the top tags used in the sessión
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Tag 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Percentage of usage
        /// </summary>
        public double Value { get; set; }
    }
}
