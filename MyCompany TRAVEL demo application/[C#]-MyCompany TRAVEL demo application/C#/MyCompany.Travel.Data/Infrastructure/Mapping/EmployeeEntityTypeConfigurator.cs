
namespace MyCompany.Travel.Data.Infrastructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Travel.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Travel.Model.Employee"/>
    /// </summary>
    class EmployeeEntityTypeConfigurator
        : EntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private EmployeeEntityTypeConfigurator()
        {
            this.HasKey(e => e.EmployeeId);

            this.Property(e => e.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(e => e.FirstName)
                .IsRequired();

            this.Property(e => e.LastName)
                .IsRequired();

            this.Property(e => e.Email)
                .IsRequired();

            this.Ignore(e => e.IsRRHH);

            this.Ignore(e => e.IsManager);

            this.HasOptional(t => t.Team)
                .WithMany(d => d.Employees)
                .HasForeignKey(t => t.TeamId);
        }
    }
}
