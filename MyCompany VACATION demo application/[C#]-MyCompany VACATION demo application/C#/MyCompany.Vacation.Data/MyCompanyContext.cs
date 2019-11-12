namespace MyCompany.Vacation.Data
{
    using System.Data.Entity;
    using MyCompany.Vacation.Data.Infrastructure.Mapping;
    using MyCompany.Vacation.Model;
    using System;
    using MyCompany.Common.CrossCutting;
    using MyCompany.Vacation.Data.Infrastructure.Conventions;
    using MyCompany.Visitors.Data.Infrastructure;


    /// <summary>
    /// Context to access to MyCompany entities
    /// </summary>
    public class MyCompanyContext 
        : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MyCompanyContext()
            : base("MyCompany.Vacation")
        {
            Database.Log = (s) =>
            {
                System.Diagnostics.Debug.WriteLine(s);
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

            //Add all entity type configurations defined in "this" assembly. With this
            //method the boilerplate code to add configurations is removed.
            modelBuilder.Configurations.AddFromAssembly(typeof(MyCompanyContext).Assembly);

        }

        /// <summary>
        /// Employee Collection
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// VacationRequest Collection
        /// </summary>
        public DbSet<VacationRequest> VacationRequests { get; set; }

        /// <summary>
        /// Team Collection
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Office Collection
        /// </summary>
        public DbSet<Office> Offices { get; set; }

        /// <summary>
        /// Calendars Collection
        /// </summary>
        public DbSet<Calendar> Calendars { get; set; }

        /// <summary>
        /// CalendarHolidays Collection
        /// </summary>
        public DbSet<CalendarHolidays> CalendarHolidays { get; set; }

        /// <summary>
        /// EmployeePictures Collection
        /// </summary>
        public DbSet<EmployeePicture> EmployeePictures { get; set; }

        /// <summary>
        /// IssuingAuthorityKeys
        /// </summary>
        public DbSet<IssuingAuthorityKey> IssuingAuthorityKeys { get; set; }
    }
}
