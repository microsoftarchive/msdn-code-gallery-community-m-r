

namespace MyCompany.Travel.Data.Infrastructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Travel.Model;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Travel.Model.TravelAttachment"/>
    /// </summary>
    class TravelAttachmentEntityTypeConfigurator
        : EntityTypeConfiguration<TravelAttachment>
    {
        private TravelAttachmentEntityTypeConfigurator()
        {
            this.HasKey(e => e.TravelAttachmentId);

            this.Property(e => e.Name)
                .IsRequired();

            this.Property(e => e.FileName)
                 .IsRequired();

            this.Property(e => e.Content)
                .IsRequired();

            this.Property(e => e.TravelRequestId)
                .IsRequired();
        }
    }
}
