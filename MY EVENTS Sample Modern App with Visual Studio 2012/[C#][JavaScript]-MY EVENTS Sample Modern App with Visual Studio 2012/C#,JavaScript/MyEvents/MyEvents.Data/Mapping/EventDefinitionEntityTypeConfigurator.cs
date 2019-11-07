using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvents.Model;

namespace MyEvents.Data.Mapping
{
    /// <summary>
    /// The entity type configuration
    /// </summary>
    class EventDefinitionEntityTypeConfigurator 
        : EntityTypeConfiguration<EventDefinition>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EventDefinitionEntityTypeConfigurator()
        {
            this.HasRequired(p => p.Organizer)
                .WithMany(d => d.OrganizerEventDefinitions)
                .HasForeignKey(c => c.OrganizerId);

            this.HasMany(c => c.RegisteredUsers)
                .WithMany(p => p.AttendeeEventDefinitions)
                .Map(
                    m =>
                    {
                        m.MapLeftKey("EventDefinitionId");
                        m.MapRightKey("RegisteredUserId");
                        m.ToTable("EventDefinitionRegisteredUsers");
                    });


        }
    }
}
