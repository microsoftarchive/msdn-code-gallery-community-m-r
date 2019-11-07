namespace MyCompany.Vacation.Data.Infrastructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Vacation.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Vacation.Model.Team"/>
    /// </summary>
     class TeamEntityTypeConfigurator
        : EntityTypeConfiguration<Team>
    {
        private TeamEntityTypeConfigurator()
        {
            this.HasKey(t => t.TeamId);

            this.Property(t => t.TeamId)
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.HasRequired(t => t.Manager)
                .WithMany(d => d.ManagedTeams)
                .HasForeignKey(t => t.ManagerId);
        }
    }
}
