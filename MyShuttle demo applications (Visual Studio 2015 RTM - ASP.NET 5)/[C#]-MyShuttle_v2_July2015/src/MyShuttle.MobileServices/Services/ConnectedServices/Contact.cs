
namespace MyShuttle.MobileServices.Services.ConnectedServices
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class Contact
    {
        [Key]
        public string Id { get; set; }

        public string MasterRecordId { get; set; }

        public string AccountId { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; }

        public string Salutation { get; set; }

        [StringLength(121)]
        public string Name { get; set; }

        public string OtherStreet { get; set; }

        [StringLength(40)]
        public string OtherCity { get; set; }

        [StringLength(80)]
        public string OtherState { get; set; }

        [StringLength(20)]
        public string OtherPostalCode { get; set; }

        [StringLength(80)]
        public string OtherCountry { get; set; }

        public double? OtherLatitude { get; set; }

        public double? OtherLongitude { get; set; }

        public string MailingStreet { get; set; }

        [StringLength(40)]
        public string MailingCity { get; set; }

        [StringLength(80)]
        public string MailingState { get; set; }

        [StringLength(20)]
        public string MailingPostalCode { get; set; }

        [StringLength(80)]
        public string MailingCountry { get; set; }

        public double? MailingLatitude { get; set; }

        public double? MailingLongitude { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Phone]
        public string Fax { get; set; }

        [Phone]
        public string MobilePhone { get; set; }

        [Phone]
        public string HomePhone { get; set; }

        [Phone]
        public string OtherPhone { get; set; }

        [Phone]
        public string AssistantPhone { get; set; }

        public string ReportsToId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(80)]
        public string Department { get; set; }

        [StringLength(40)]
        public string AssistantName { get; set; }

        public string LeadSource { get; set; }

        public DateTimeOffset? Birthdate { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public string CreatedById { get; set; }

        public string LastModifiedById { get; set; }        

        public DateTimeOffset? LastActivityDate { get; set; }

        public DateTimeOffset? LastCURequestDate { get; set; }

        public DateTimeOffset? LastCUUpdateDate { get; set; }

        public DateTimeOffset? LastViewedDate { get; set; }

        public DateTimeOffset? LastReferencedDate { get; set; }

        [StringLength(255)]
        public string EmailBouncedReason { get; set; }

        public DateTimeOffset? EmailBouncedDate { get; set; }

        [Url]
        public string PhotoUrl { get; set; }

        [StringLength(20)]
        public string Jigsaw { get; set; }

        [StringLength(20)]
        public string JigsawContactId { get; set; }

        public string CleanStatus { get; set; }

        public string Level__c { get; set; }

        [StringLength(100)]
        public string Languages__c { get; set; }

    }
}
