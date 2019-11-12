
namespace MyCompany.Visitors.Client
{
    /// <summary>
    /// Visitor Picture Entity
    /// </summary>
    public class VisitorPicture
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int VisitorPictureId { get; set; }

        /// <summary>
        /// Picture Type
        /// </summary>
        public PictureType PictureType { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// VisitorId
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// Visitor
        /// </summary>
        public Visitor Visitor { get; set; }
    }
}
