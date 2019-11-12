using System.ComponentModel.DataAnnotations;

namespace MyEvents.Model
{
    /// <summary>
    /// Material
    /// </summary>
    public class Material
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [Key]
        public int MaterialId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [Required]
        public byte[] Content { get; set; }

        /// <summary>
        /// Content type
        /// </summary>
        [Required]
        public string ContentType { get; set; }

        /// <summary>
        /// SessionId associated with the material
        /// </summary>
        [Required]
        public int SessionId { get; set; }

        /// <summary>
        /// Session associated with the material
        /// </summary>
        public Session Session { get; set; }
    }
}
