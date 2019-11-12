
namespace MyCompany.Travel.Data
{
    using MyCompany.Common.CrossCutting;
    using MyCompany.Travel.Data.Infrastructure;
    using MyCompany.Travel.Data.Infrastructure.Conventions;
    using MyCompany.Travel.Model;
    using System;
    using System.Data.Entity;


    /// <summary>
    /// Context to access to MyCompany entities
    /// </summary>
    public class MyCompanyContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MyCompanyContext()
            : base("MyCompany.Travel")
        {
            Database.Log = (s) =>
            {
                TraceManager.TraceInfo(new string[] { s });
            };
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but before the model has been locked down and used to initialize the context
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //add custom conventions
            modelBuilder.Conventions.Add<CLRDateTimeToSqlDateTime2>();
            modelBuilder.Conventions.Add<MaxStringLengthConvention>();

            modelBuilder.Entity<Question>()
                .Property(q => q.Answer).HasMaxLength(1024);

            //Add all entity type configurations defined in "this" assembly. With this
            //method the boilerplate code to add configurations is removed.
            modelBuilder.Configurations.AddFromAssembly(typeof(MyCompanyContext).Assembly);
        }

        /// <summary>
        /// Employee Collection
        /// </summary>
        public DbSet<Employee> Employees { get; set; }


        /// <summary>
        /// Team Collection
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Travel Request Collection
        /// </summary>
        public DbSet<TravelRequest> TravelRequests { get; set; }

        /// <summary>
        /// Travel Attachment Collection
        /// </summary>
        public DbSet<TravelAttachment> TravelAttachments { get; set; }

        /// <summary>
        /// Employee Picture Collection
        /// </summary>
        public DbSet<EmployeePicture> EmployeePictures { get; set; }

        /// <summary>
        /// IssuingAuthorityKeys
        /// </summary>
        public DbSet<IssuingAuthorityKey> IssuingAuthorityKeys { get; set; }

        /// <summary>
        /// Question Collection
        /// </summary>
        public DbSet<Question> Questions { get; set; }
    }
}
