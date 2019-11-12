namespace MyCompany.Travel.Data.Infrastructure.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using MyCompany.Travel.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The entity type configuration <see cref="MyCompany.Travel.Model.Team"/>
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
