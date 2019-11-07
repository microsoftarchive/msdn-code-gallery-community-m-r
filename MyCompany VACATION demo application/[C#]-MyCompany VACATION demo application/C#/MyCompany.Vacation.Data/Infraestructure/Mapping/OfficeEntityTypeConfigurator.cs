
namespace MyCompany.Vacation.Data.Infrastructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Vacation.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Vacation.Model.Office"/>
    /// </summary>
    class OfficeEntityTypeConfigurator
        : EntityTypeConfiguration<Office>
    {   
        private OfficeEntityTypeConfigurator()
        {
            this.HasKey(o => o.OfficeId);

            this.Property(o => o.OfficeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.HasMany(o => o.Teams)
                .WithRequired()
                .HasForeignKey(e => e.OfficeId)
                .WillCascadeOnDelete(false);
        }
    }
}
