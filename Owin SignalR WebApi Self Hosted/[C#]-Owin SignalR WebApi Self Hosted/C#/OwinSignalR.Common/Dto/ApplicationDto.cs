using System.Collections.Generic;

namespace OwinSignalR.Common.Dto
{
    public class ApplicationDto
    {
        public int?     ApplicationId     { get; set; }
        public string   ApplicationName   { get; set; }
        public string   ApiToken          { get; set; }
        public string   ApplicationSecret { get; set; }
        
        public List<ApplicationReferralUrlDto>  ApplicationReferralUrls { get; set; }

        public ApplicationDto() 
        {
            ApplicationReferralUrls = new List<ApplicationReferralUrlDto>();
        }
    }
}
