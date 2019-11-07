
namespace MyCompany.Travel.Client
{
    /// <summary>
    /// Travel Attachment
    /// </summary>
    public class TravelAttachment
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int TravelAttachmentId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attachment Type
        /// </summary>
        public AttachmentType AttachmentType { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Travel Request Id
        /// </summary>
        public int TravelRequestId { get; set; }

        /// <summary>
        /// Travel Request
        /// </summary>
        public TravelRequest TravelRequest { get; set; }
    }
}
