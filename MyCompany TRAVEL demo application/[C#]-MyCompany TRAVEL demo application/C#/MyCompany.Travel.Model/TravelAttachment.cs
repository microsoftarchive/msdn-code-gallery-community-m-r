
namespace MyCompany.Travel.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Travel Attachment
    /// </summary>
    [DataContract]
    public class TravelAttachment
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        [DataMember(IsRequired = true)]
        public int TravelAttachmentId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        [DataMember(IsRequired = true)]
        public string FileName { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [DataMember(IsRequired = true)]
        public byte[] Content { get; set; }

        /// <summary>
        /// Travel Request Id
        /// </summary>
        [DataMember(IsRequired = true)]
        public int TravelRequestId { get; set; }

        /// <summary>
        /// Travel Request
        /// </summary>
        [DataMember()]
        public TravelRequest TravelRequest { get; set; }
    }
}
