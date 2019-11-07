namespace MyCompany.Vacation.Data.Infraestructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Vacation.Model.VacationRequest"/>
    /// </summary>
    class VacationRequestEntityTypeConfigurator
       : EntityTypeConfiguration<VacationRequest>
    {
        private VacationRequestEntityTypeConfigurator()
        {
            this.HasKey(v => v.VacationRequestId);

            this.Property(v => v.From)
                 .IsRequired();

            this.Property(v => v.To)
                .IsRequired();

            this.Property(v => v.NumDays)
                .IsRequired();

            this.Property(v => v.Status)
                .IsRequired();

            this.Property(v => v.CreationDate)
                .IsRequired();

            this.Property(v => v.LastModifiedDate)
                .IsRequired();

            this.Property(v => v.EmployeeId)
                .IsRequired();
        }
    }

}
