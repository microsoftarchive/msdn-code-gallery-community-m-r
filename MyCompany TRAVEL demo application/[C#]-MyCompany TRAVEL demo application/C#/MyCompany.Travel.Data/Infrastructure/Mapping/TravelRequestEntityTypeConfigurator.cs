
namespace MyCompany.Travel.Data.Infrastructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Travel.Model;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Travel.Model.TravelRequest"/>
    /// </summary>
    class TravelRequestEntityTypeConfigurator
        : EntityTypeConfiguration<TravelRequest>
    {
        private TravelRequestEntityTypeConfigurator()
        {
            this.HasKey(e => e.TravelRequestId);

            this.Property(e => e.Name)
                .IsRequired();

            this.Property(e => e.Description)
                .IsRequired();

            this.Property(e => e.TravelType)
                .IsRequired();

            this.Property(e => e.From)
                .IsRequired();

            this.Property(e => e.To)
                .IsRequired();

            this.Property(e => e.Depart)
                .IsRequired();

            this.Property(e => e.CreationDate)
                .IsRequired();

            this.Property(e => e.LastModifiedDate)
                .IsRequired();

            this.Property(e => e.Status)
                .IsRequired();

            this.Property(e => e.EmployeeId)
                .IsRequired();
        }
    }

}
