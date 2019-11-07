using System;
using System.Data.Entity;

namespace OwinSignalR.Data.Models
{
    public interface IOwinSignalrDbContext 
    {
        DbSet<Application>            Applications            { get; set; }
        DbSet<ApplicationReferralUrl> ApplicationReferralUrls { get; set; }
    }

    public partial class OwinSignalrDbContext
        : IOwinSignalrDbContext
    {

    }
}
