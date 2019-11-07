using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Store information about the area of the room
    /// </summary>
    public class RoomPoint
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int RoomPointId { get; set; }

        /// <summary>
        /// RoomNumber
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// PointX
        /// </summary>
        public int PointX { get; set; }

        /// <summary>
        /// PointY
        /// </summary>
        public int PointY { get; set; }

        /// <summary>
        /// EventDefinitionId
        /// </summary>
        public int EventDefinitionId { get; set; }

        /// <summary>
        /// EventDefinition
        /// </summary>
        public EventDefinition EventDefinition { get; set; }
    }
}
