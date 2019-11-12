using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Tests.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Tests.Data.Domain.Mapping
{
    public class CategoryMapping : EntityMappingBase<Category>
    {
        public CategoryMapping()
        {
            HasKey(x => x.Id);

            Property(x => x.Name).HasColumnName("Category Name");

            //HasMany(x => x.Products)
            //   .WithMany(y => y.Categories)
            //   .Map(m =>
            //   {
            //       m.ToTable("ProductsInCategories");
            //   });

            //HasMany(x => x.Products)
            //   .WithMany(y => y.Categories)               
            //   .Map(m =>
            //   {
            //       m.ToTable("ProductsInCategories");
            //       m.MapLeftKey("CategoryId"); // optional, to specify/override the column named ProductId (instead of auto-generated Product_Id) for a many-to-many relationship
            //       m.MapRightKey("ProductId"); // optional, to explicitly specify the column named "CategoryId" instead of auto-generated Category_Id for a many-to-many relationship
            //   });

            ToTable("Category");
        }
    }
}
