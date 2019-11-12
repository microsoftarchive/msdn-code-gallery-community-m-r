using System.Data.Entity;
using MyEvents.Data.Mapping;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// Context to access to MyEvents entities
    /// </summary>
    public class MyEventsContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MyEventsContext()
            : base("MyEvents")
        {
            
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but before the model has been locked down and used to initialize the context
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.ProxyCreationEnabled = false;

            modelBuilder.Configurations.Add(new EventDefinitionEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new RegisteredUserEntityTypeConfigurator());
        }

        /// <summary>
        /// EventDefinition Collection
        /// </summary>
        public DbSet<EventDefinition> EventDefinitions { get; set; }

        /// <summary>
        /// RegisteredUser Collection
        /// </summary>
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }

        /// <summary>
        /// SessionRegisteredUser Collection
        /// </summary>
        public DbSet<SessionRegisteredUser> SessionRegisteredUsers { get; set; }


        /// <summary>
        /// Session Collection
        /// </summary>
        public DbSet<Session> Sessions { get; set; }

        /// <summary>
        /// Comment Collection
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Material Collection
        /// </summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>
        /// RoomPoint Collection
        /// </summary>
        public DbSet<RoomPoint> RoomPoints { get; set; }
    }
}
