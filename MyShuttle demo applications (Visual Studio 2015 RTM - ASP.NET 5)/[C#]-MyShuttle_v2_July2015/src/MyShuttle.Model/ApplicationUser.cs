namespace MyShuttle.Model
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public int CarrierId { get; set; }
    }
}