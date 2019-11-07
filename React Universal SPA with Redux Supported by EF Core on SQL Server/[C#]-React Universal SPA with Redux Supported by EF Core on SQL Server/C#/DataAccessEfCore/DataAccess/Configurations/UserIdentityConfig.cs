using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class UserIdentityConfig: IEntityTypeConfiguration<UserIdentity>
    {
        public void Configure(EntityTypeBuilder<UserIdentity> builder)
        {
            builder.HasKey(user => user.UserId);
        }
    }
}
