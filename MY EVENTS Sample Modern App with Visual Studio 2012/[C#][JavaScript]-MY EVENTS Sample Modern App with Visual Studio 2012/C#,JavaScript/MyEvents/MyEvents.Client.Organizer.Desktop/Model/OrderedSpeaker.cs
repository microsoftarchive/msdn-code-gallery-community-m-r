using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents.Client.Organizer.Desktop.Model
{
    /// <summary>
    /// Todo only for designtime purpose delete on final release
    /// </summary>
    public class OrdererSpeaker
    {
        /// <summary>
        ///  Name and surname of the speaker
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Position in the list of the speaker
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Rating of the speaker
        /// </summary>
        public double Rating { get; set; }
    }
}
