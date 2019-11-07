namespace MyShuttle.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using MyShuttle.Model;
    using MyShuttle.API;

    public class CarrierViewModel
    {
        [Required(ErrorMessage = "The Company Name field is required.")]
        public string CompanyName { get; set; }

        
        public string Address { get; set; }

        
        public string ZipCode { get; set; }

       
        public string City { get; set; }

       
        public string Country { get; set; }

       
        public string State { get; set; }

        [Required(ErrorMessage ="The ID field is required.")]
        public string CompanyID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Description { get; set; }

        public void CopyTo(Carrier carrier)
        {
            carrier.Name = this.CompanyName;
            carrier.Address = this.Address;
            carrier.ZipCode = this.ZipCode;
            carrier.City = this.City;
            carrier.Country = this.Country;
            carrier.State = this.State;
            carrier.CompanyID = this.CompanyID;
            carrier.Email = this.Email;
            carrier.Description = this.Description;

        }
    }
}