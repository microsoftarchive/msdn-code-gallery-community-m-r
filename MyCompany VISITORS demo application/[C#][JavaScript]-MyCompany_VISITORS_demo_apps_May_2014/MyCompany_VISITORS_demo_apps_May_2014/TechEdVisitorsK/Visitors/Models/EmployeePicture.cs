
namespace Visitors
{
    /// <summary>
    /// Employee Picture Entity
    /// </summary>
    public class EmployeePicture
    {
        /// <summary>
        /// The unique identifier for entity
        /// </summary>
        public int EmployeePictureId { get; set; }

        /// <summary>
        /// Picture Type
        /// </summary>
        public int PictureType { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee
        /// </summary>
        public Employee Employee { get; set; }
    }
}
