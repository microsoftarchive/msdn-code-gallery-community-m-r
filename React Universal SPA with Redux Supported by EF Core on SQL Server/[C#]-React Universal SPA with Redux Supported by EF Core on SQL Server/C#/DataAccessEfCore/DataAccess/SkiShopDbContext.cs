using DataAccessEfCore.DataAccess.Configurations;
using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEfCore.DataAccess
{
    public class SkiShopDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Description> Descriptions { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<IdealFor> IdealFors { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Sku> Skus { get; set; }

        public DbSet<SpecAbility> SpecAbilities { get; set; }

        public DbSet<SpecBitValue> SpecBitValues { get; set; }

        public DbSet<SpecConstruction> SpecConstructions { get; set; }

        public DbSet<SpecCore> SpecCores { get; set; }

        public DbSet<SpecKey> SpecKeys { get; set; }

        public DbSet<SpecMultiValue> SpecMultiValues { get; set; }

        public DbSet<SpecRockerCamberProfile> SpecRockerCamberProfiles { get; set; }

        public DbSet<SpecSingleValue> SpecSingleValues { get; set; }

        public DbSet<SpecSnowCondition> SpecSnowConditions { get; set; }

        public DbSet<SpecTerrain> SpecTerrains { get; set; }

        public DbSet<SpecTextValue> SpecTextValues { get; set; }

        public DbSet<StyleAbility> StyleAbilities { get; set; }

        public DbSet<StyleIdealFor> StyleIdealFors { get; set; }

        public DbSet<StyleRockerCamberProfile> StyleRockerCamberProfiles { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<StyleSnowCondition> StyleSnowConditions { get; set; }

        public DbSet<StyleState> StyleStates { get; set; }

        public DbSet<StyleTerrain> StyleTerrains { get; set; }

        public DbSet<UserIdentity> UserIdentities { get; set; }

        public DbQuery<vwStyle> VwStyles { get; set; }

        public DbQuery<vwStyleIdealFor> VwStyleIdealFors { get; set; }

        public DbQuery<spSpec> SpSpecs { get; set; }

        public SkiShopDbContext (DbContextOptions<SkiShopDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DescriptionConfig());

            modelBuilder.ApplyConfiguration(new OrderConfig());

            modelBuilder.ApplyConfiguration(new OrderItemConfig());

            modelBuilder.ApplyConfiguration(new ProvinceConfig());

            modelBuilder.ApplyConfiguration(new ReviewConfig());

            modelBuilder.ApplyConfiguration(new SkuConfig());

            modelBuilder.ApplyConfiguration(new SpecAbilityConfig());

            modelBuilder.ApplyConfiguration(new SpecBitValueConfig());

            modelBuilder.ApplyConfiguration(new SpecCoreConfig());

            modelBuilder.ApplyConfiguration(new SpecConstructionConfig());

            modelBuilder.ApplyConfiguration(new SpecMultiSpecConfig());

            modelBuilder.ApplyConfiguration(new SpecRockerCamberProfileConfig());

            modelBuilder.ApplyConfiguration(new SpecSingleValueConfig());

            modelBuilder.ApplyConfiguration(new SpecSnowConditionConfig());

            modelBuilder.ApplyConfiguration(new SpecTerrainConfig());

            modelBuilder.ApplyConfiguration(new SpecTextValueConfig());

            modelBuilder.ApplyConfiguration(new StyleAbilityConfig());

            modelBuilder.ApplyConfiguration(new StyleConfig());

            modelBuilder.ApplyConfiguration(new StyleIdealForConfig());

            modelBuilder.ApplyConfiguration(new StyleRockerCamberProfileConfig());

            modelBuilder.ApplyConfiguration(new StyleSnowConditionConfig());

            modelBuilder.ApplyConfiguration(new StyleStateConfig());

            modelBuilder.ApplyConfiguration(new StyleTerrainConfig());

            modelBuilder.ApplyConfiguration(new UserIdentityConfig());

            // views
            modelBuilder.Query<vwStyle>().ToView("vwStyle");

            modelBuilder.Query<vwStyleIdealFor>().ToView("vwStyleIdealFor");

            // store procedures
            modelBuilder.Query<spSpec>();

        }

    }
}
