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
    class RegisteredUserEntityTypeConfigurator 
        : EntityTypeConfiguration<RegisteredUser>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RegisteredUserEntityTypeConfigurator()
        {
            this.HasMany(ru => ru.OrganizerEventDefinitions)
                .WithRequired()
                .HasForeignKey(ru => ru.OrganizerId)
                .WillCascadeOnDelete(false);
        }
    }
}
