namespace CIK.News.Mapping
{
    using CIK.News.Entities.NewsAgg;

    public class ItemMapping : EntityMappingBase<Item>
    {
        public ItemMapping()
        {
            this.Property(x => x.ItemContentId);

            this.HasRequired(x => x.Category).WithMany(y => y.Items);

            this.HasRequired(x => x.ItemContent).WithMany(x => x.Items).HasForeignKey(key => key.ItemContentId);

            this.ToTable("Item");
        }
    }
}