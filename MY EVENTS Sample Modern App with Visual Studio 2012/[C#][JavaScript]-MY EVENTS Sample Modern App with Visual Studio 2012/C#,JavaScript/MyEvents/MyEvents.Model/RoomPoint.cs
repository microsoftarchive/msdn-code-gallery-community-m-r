using System.ComponentModel.DataAnnotations;

namespace MyEvents.Model
{
    /// <summary>
    /// Store information about the area of the room
    /// </summary>
    public class RoomPoint
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int RoomPointId { get; set; }

        /// <summary>
        /// RoomNumber
        /// </summary>
        [Required]
        public int RoomNumber { get; set; }

        /// <summary>
        /// PointX
        /// </summary>
        [Required]
        public int PointX { get; set; }

        /// <summary>
        /// PointY
        /// </summary>
        [Required]
        public int PointY { get; set; }

        /// <summary>
        /// EventDefinitionId
        /// </summary>
        [Required]
        public int EventDefinitionId { get; set; }

        /// <summary>
        /// EventDefinition
        /// </summary>
        public EventDefinition EventDefinition { get; set; }
    }
}
