
namespace MyCompany.Travel.Data.Infrastructure.Mapping
{
    using MyCompany.Travel.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// the entity type configuration for <see cref="MyCompany.Travel.Model.EmployeePicture"/>
    /// </summary>
    class EmployeePictureEntityTypeConfiguration
        : EntityTypeConfiguration<EmployeePicture>
    {
        private EmployeePictureEntityTypeConfiguration()
        {
            this.HasKey(ep => ep.EmployeePictureId);

            this.Property(ep => ep.EmployeePictureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.HasRequired(ep => ep.Employee)
                .WithMany(e => e.EmployeePictures)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}
